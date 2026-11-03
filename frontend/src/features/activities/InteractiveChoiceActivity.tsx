import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { AudioPromptButton } from '../../components/AudioPromptButton';
import type { CurriculumActivity } from '../../lib/api';
import { parseActivityPayload, pickLocalizedText } from './types';

interface InteractiveChoiceActivityProps {
  activity: CurriculumActivity;
  isCompleted: boolean;
  onComplete: (activitySlug: string) => void;
}

/**
 * PHASE E — Renders multi-modal interactive preschool activities decoupled from 3D models:
 * - `audio_listen_pick` (listen and pick the illustrated card)
 * - `visual_match_sort` (visual association, shape/habitat matching, logical sequences)
 * - `counting_interactive` (counting 1–10 and simple addition stories)
 * - `letter_forms_vowels` (Arabic positional letter forms بـ/ـبـ/ـب and short vowels الفتحة/الكسرة/الضمة)
 * - `syllable_word_builder` (syllable blending كَـ + تَـ + بَ = كَتَبَ, word reading, and vocalized sentences)
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

  const [selectedOptionId, setSelectedOptionId] = useState<string | null>(null);

  const isCorrect = Boolean(selectedOptionId && selectedOptionId === payload.correctOptionId);
  const isWrong = Boolean(
    selectedOptionId && payload.correctOptionId && selectedOptionId !== payload.correctOptionId,
  );

  const handleSelectOption = (optionId: string) => {
    setSelectedOptionId(optionId);
    if (optionId === payload.correctOptionId && !isCompleted) {
      onComplete(activity.slug);
    }
  };

  return (
    <div className="activity-stage activity-stage--interactive">
      <div className="activity-stage__header">
        <div>
          <span className="activity-stage__badge">{activity.objectiveCode}</span>
          <h4>{activity.title}</h4>
          <p className="activity-stage__prompt">{activity.prompt}</p>
        </div>
        <AudioPromptButton text={activity.spokenText} />
      </div>

      {syllables.length > 0 && (
        <div className="syllable-strip" aria-label={t('curriculum.syllableBlendLabel')}>
          <span className="syllable-strip__label">{t('curriculum.syllableBlendLabel')}</span>
          <div className="syllable-tiles">
            {syllables.map((part, idx) => (
              <span key={`${part}-${idx}`} className="syllable-tile">
                {part}
              </span>
            ))}
          </div>
        </div>
      )}

      <div className="option-cards">
        {options.map((opt) => {
          const isChosen = selectedOptionId === opt.id;
          const label = pickLocalizedText(i18n.language, opt.labelAr, opt.labelFr, opt.labelEn);
          return (
            <button
              key={opt.id}
              type="button"
              className={`option-card ${isChosen ? 'option-card--selected' : ''}`}
              aria-pressed={isChosen}
              onClick={() => handleSelectOption(opt.id)}
            >
              <span className="option-card__emoji" aria-hidden="true">
                {opt.emoji}
              </span>
              <span className="option-card__label">{label}</span>
            </button>
          );
        })}
      </div>

      {isCorrect && (
        <p className="feedback-banner feedback-banner--success" role="status">
          {t('curriculum.correctFeedback')}
        </p>
      )}
      {isWrong && (
        <p className="feedback-banner feedback-banner--retry" role="status">
          {t('curriculum.tryAgainFeedback')}
        </p>
      )}

      <div className="activity-stage__footer">
        <button
          type="button"
          className="checkpoint-complete-button"
          disabled={isCompleted}
          onClick={() => onComplete(activity.slug)}
        >
          {isCompleted ? t('curriculum.completed') : t('curriculum.markComplete')}
        </button>
      </div>
    </div>
  );
}
