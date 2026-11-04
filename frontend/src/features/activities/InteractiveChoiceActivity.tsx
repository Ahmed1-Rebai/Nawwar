import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { AudioPromptButton } from '../../components/AudioPromptButton';
import type { CurriculumActivity } from '../../lib/api';
import { CountingPlayground } from './CountingPlayground';
import { LearningFlashcards } from './LearningFlashcards';
import { LetterVowelStudio } from './LetterVowelStudio';
import { SyllableBuilderPlayground } from './SyllableBuilderPlayground';
import {
  getEffectiveLearningCards,
  parseActivityPayload,
  pickLocalizedText,
  speakLocalizedText,
} from './types';

interface InteractiveChoiceActivityProps {
  activity: CurriculumActivity;
  isCompleted: boolean;
  onComplete: (activitySlug: string) => void;
}

/**
 * PHASE E — Renders multi-modal interactive preschool activities decoupled from 3D models:
 * - `audio_listen_pick` (listen and pick the illustrated card)
 * - `visual_match_sort` (visual association, shape/habitat matching, logical sequences)
 * - `counting_interactive` (counting 1–10 and simple addition stories with CountingPlayground)
 * - `letter_forms_vowels` (Arabic positional letter forms بـ/ـبـ/ـب and short vowels with LetterVowelStudio)
 * - `syllable_word_builder` (syllable blending كَـ + تَـ + بَ = كَتَبَ with SyllableBuilderPlayground)
 */
export function InteractiveChoiceActivity({
  activity,
  isCompleted,
  onComplete,
}: InteractiveChoiceActivityProps) {
  const { t, i18n } = useTranslation();
  const payload = parseActivityPayload(activity.interactionPayloadJson);
  const options = payload.options ?? [];
  const syllables = payload.syllables ?? [];
  const learningCards = getEffectiveLearningCards(payload);

  const [selectedOptionId, setSelectedOptionId] = useState<string | null>(null);

  const isCorrect = Boolean(selectedOptionId && selectedOptionId === payload.correctOptionId);
  const isWrong = Boolean(
    selectedOptionId && payload.correctOptionId && selectedOptionId !== payload.correctOptionId,
  );

  const handleSelectOption = (optionId: string, spokenLabel: string) => {
    setSelectedOptionId(optionId);
    if (optionId === payload.correctOptionId) {
      speakLocalizedText(`${spokenLabel}. ${t('curriculum.correctFeedback')}`, i18n.language);
      if (!isCompleted) {
        onComplete(activity.slug);
      }
    } else {
      speakLocalizedText(t('curriculum.tryAgainFeedback'), i18n.language);
    }
  };

  const quizPromptHeading = pickLocalizedText(
    i18n.language,
    '🎯 التَّحَدِّي المَرِحُ — اِخْتَرِ الإِجَابَةَ الصَّحِيحَةَ:',
    '🎯 Défi ludique — Choisis la bonne réponse :',
    '🎯 Playful Challenge — Pick the right answer:',
  );

  return (
    <div className="activity-stage activity-stage--interactive">
      <div className="activity-stage__header">
        <div>
          <span className="activity-stage__badge">🌟 {activity.objectiveCode}</span>
          <h4>{activity.title}</h4>
          <p className="activity-stage__prompt">{activity.prompt}</p>
        </div>
        <AudioPromptButton text={activity.spokenText} />
      </div>

      {payload.countingItems && <CountingPlayground config={payload.countingItems} />}

      {payload.letterStudio && <LetterVowelStudio studio={payload.letterStudio} />}

      {syllables.length > 0 && (
        <SyllableBuilderPlayground syllables={syllables} spokenText={activity.spokenText} />
      )}

      {!payload.countingItems && !payload.letterStudio && learningCards.length > 0 && (
        <LearningFlashcards cards={learningCards} />
      )}

      <div className="quiz-challenge-box">
        <p className="quiz-challenge-box__heading">{quizPromptHeading}</p>
        <div className="option-cards">
          {options.map((opt) => {
            const isChosen = selectedOptionId === opt.id;
            const isOptionCorrect = isChosen && isCorrect;
            const isOptionWrong = isChosen && isWrong;
            const label = pickLocalizedText(i18n.language, opt.labelAr, opt.labelFr, opt.labelEn);
            return (
              <button
                key={opt.id}
                type="button"
                className={`option-card ${isChosen ? 'option-card--selected' : ''} ${
                  isOptionCorrect ? 'option-card--correct' : ''
                } ${isOptionWrong ? 'option-card--wrong' : ''}`}
                style={{ '--option-accent': opt.accentColor || '#f59e0b' } as React.CSSProperties}
                aria-pressed={isChosen}
                onClick={() => handleSelectOption(opt.id, label)}
              >
                <span className="option-card__emoji" aria-hidden="true">
                  {opt.emoji}
                </span>
                <span className="option-card__label">{label}</span>
              </button>
            );
          })}
        </div>
      </div>

      {isCorrect && (
        <p className="feedback-banner feedback-banner--success" role="status">
          🎉 🌟 {t('curriculum.correctFeedback')}
        </p>
      )}
      {isWrong && (
        <p className="feedback-banner feedback-banner--retry" role="status">
          💡 {t('curriculum.tryAgainFeedback')}
        </p>
      )}

      <div className="activity-stage__footer">
        <button
          type="button"
          className="checkpoint-complete-button"
          disabled={isCompleted}
          onClick={() => onComplete(activity.slug)}
        >
          {isCompleted ? `⭐ ${t('curriculum.completed')}` : `✓ ${t('curriculum.markComplete')}`}
        </button>
      </div>
    </div>
  );
}
