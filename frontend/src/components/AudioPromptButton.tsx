import { useState } from 'react';
import { useTranslation } from 'react-i18next';

interface AudioPromptButtonProps {
  text: string;
  label?: string;
}

/**
 * Child-friendly audio replay button. Uses the browser's SpeechSynthesis API
 * when available (with Arabic `ar-SA` as default for `ar`) and provides visual feedback.
 */
export function AudioPromptButton({ text, label }: AudioPromptButtonProps) {
  const { t, i18n } = useTranslation();
  const [speaking, setSpeaking] = useState(false);

  const handlePlay = () => {
    setSpeaking(true);
    try {
      if (typeof window !== 'undefined' && 'speechSynthesis' in window) {
        window.speechSynthesis.cancel();
        const utterance = new SpeechSynthesisUtterance(text);
        utterance.lang = i18n.language === 'fr' ? 'fr-FR' : i18n.language === 'en' ? 'en-US' : 'ar-SA';
        utterance.rate = 0.9;
        utterance.onend = () => setSpeaking(false);
        utterance.onerror = () => setSpeaking(false);
        window.speechSynthesis.speak(utterance);
        return;
      }
    } catch {
      // Fallback for headless test environments without SpeechSynthesis
    }
    setTimeout(() => setSpeaking(false), 450);
  };

  return (
    <button
      type="button"
      className="audio-prompt-button"
      aria-pressed={speaking}
      onClick={handlePlay}
    >
      <span aria-hidden="true">🔊</span>
      <span>{speaking ? t('curriculum.playingAudio') : (label ?? t('curriculum.listenPrompt'))}</span>
    </button>
  );
}
