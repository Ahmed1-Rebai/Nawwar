import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { pickLocalizedText, speakLocalizedText } from './types';

interface SyllableBuilderPlaygroundProps {
  syllables: string[];
  spokenText: string;
}

/**
 * Interactive Syllable & Word Builder (`syllable_word_builder`).
 * Children tap each syllable tile in sequence to hear it and assemble the complete
 * vocalized word or sentence inside the interactive word tray.
 */
export function SyllableBuilderPlayground({
  syllables,
  spokenText,
}: SyllableBuilderPlaygroundProps) {
  const { t, i18n } = useTranslation();
  const [assembledCount, setAssembledCount] = useState<number>(syllables.length);

  if (syllables.length === 0) {
    return null;
  }

  const handleTapTile = (part: string, idx: number) => {
    setAssembledCount(Math.max(assembledCount, idx + 1));
    speakLocalizedText(part, 'ar');
  };

  const handleBlendAll = () => {
    setAssembledCount(syllables.length);
    speakLocalizedText(spokenText || syllables.join(' '), 'ar');
  };

  const blendButtonLabel = pickLocalizedText(
    i18n.language,
    '🔊 اِسْمَعِ الدَّمْجَ الكَامِلَ',
    '🔊 Écouter le mot complet',
    '🔊 Hear Full Blended Word',
  );

  return (
    <div className="syllable-strip" aria-label={t('curriculum.syllableBlendLabel')}>
      <div className="syllable-strip__header">
        <span className="syllable-strip__label">🧩 {t('curriculum.syllableBlendLabel')}</span>
        <button type="button" className="syllable-strip__blend-btn" onClick={handleBlendAll}>
          {blendButtonLabel}
        </button>
      </div>

      <div className="syllable-tiles">
        {syllables.map((part, idx) => (
          <button
            key={`${part}-${idx}`}
            type="button"
            className="syllable-tile"
            onClick={() => handleTapTile(part, idx)}
          >
            <span className="syllable-tile__text" lang="ar" dir="rtl">
              {part}
            </span>
            <span className="syllable-tile__audio" aria-hidden="true">
              🔊
            </span>
          </button>
        ))}
      </div>
    </div>
  );
}

