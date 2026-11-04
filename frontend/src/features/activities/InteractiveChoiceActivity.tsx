import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { AudioPromptButton } from '../../components/AudioPromptButton';
import type { CurriculumActivity } from '../../lib/api';
import { CommonWordsShowcase } from './CommonWordsShowcase';
import { CountingPlayground } from './CountingPlayground';
import { LearningFlashcards } from './LearningFlashcards';
import { LetterVowelStudio } from './LetterVowelStudio';
import { SyllableBuilderPlayground } from './SyllableBuilderPlayground';
import {
  getEffectiveLearningCards,
  getEffectiveWordExamples,
  parseActivityPayload,
  pickLocalizedText,
  speakLocalizedText,
} from './types';

interface InteractiveChoiceActivityProps {
  activity: CurriculumActivity;
  isCompleted: boolean;
  onComplete: (activitySlug: string) => void;
}

type CourseJourneyStep = 1 | 2 | 3;

/**
 * PHASE E — Guides the preschool child through a 3-step pedagogical journey in every course:
 * - Step 1 (`١. نَتَعَلَّمُ الدَّرْسَ وَالصَّوْتَ`): Learn the new letter/concept and listen to its sound.
 * - Step 2 (`٢. كَلِمَاتٌ مَأْلُوفَةٌ — حَيَوَانَاتٌ وَأَشْيَاءُ`): Discover the concept in familiar words (animals, fruits, objects).
 * - Step 3 (`٣. تَمْرِينٌ لَطِيفٌ`): Friendly interactive exercise at the end.
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
  const wordExamples = getEffectiveWordExamples(payload);

  const [journeyStep, setJourneyStep] = useState<CourseJourneyStep>(1);
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

  const step1Label = pickLocalizedText(
    i18n.language,
    '١. نَتَعَلَّمُ الدَّرْسَ وَالصَّوْتَ 🔊',
    '1. Leçon et son 🔊',
    '1. Learn & Sound 🔊',
  );

  const step2Label = pickLocalizedText(
    i18n.language,
    '٢. كَلِمَاتٌ مَأْلُوفَةٌ (حَيَوَانَاتٌ وَأَشْيَاءُ) 🦁🍎',
    '2. Mots familiers (animaux et objets) 🦁🍎',
    '2. Common Words (Animals & Objects) 🦁🍎',
  );

  const step3Label = pickLocalizedText(
    i18n.language,
    '٣. تَمْرِينٌ لَطِيفٌ 🎯',
    '3. Exercice amical 🎯',
    '3. Friendly Exercise 🎯',
  );

  const teacherIntroTitle = pickLocalizedText(
    i18n.language,
    '🦉 هَيَّا نَتَعَلَّمُ أَوَّلًا! اِسْتَمِعْ إِلَى الصَّوْتِ وَاضْغَطْ عَلَى البِطَاقَاتِ:',
    '🦉 Apprenons d’abord ensemble ! Écoute le son et touche les cartes :',
    '🦉 Let’s learn first! Listen to the sound and tap each card:',
  );

  const goToStep2Label = pickLocalizedText(
    i18n.language,
    'الخُطْوَةُ التَّالِيَةُ (٢): الكَلِمَاتُ المَأْلُوفَةُ (حَيَوَانَاتٌ وَأَشْيَاءُ) 🦁🍎 ⬅',
    'Étape suivante (2) : Mots familiers (animaux et objets) 🦁🍎 ⬅',
    'Next Step (2): Common Words (Animals & Objects) 🦁🍎 ⬅',
  );

  const goToStep3Label = pickLocalizedText(
    i18n.language,
    'الخُطْوَةُ التَّالِيَةُ (٣): هَيَّا نَلْعَبْ التَّمْرِينَ اللَّطِيفَ! 🎯 ⬅',
    'Étape suivante (3) : Jouons à l’exercice amical ! 🎯 ⬅',
    'Next Step (3): Let’s Play the Friendly Exercise! 🎯 ⬅',
  );

  const backToStep1Label = pickLocalizedText(
    i18n.language,
    '➡ العَوْدَةُ إِلَى الدَّرْسِ وَالصَّوْتِ (١)',
    '➡ Retour à la leçon (1)',
    '➡ Back to Lesson & Sound (1)',
  );

  const backToStep2Label = pickLocalizedText(
    i18n.language,
    '➡ مُرَاجَعَةُ الكَلِمَاتِ المَأْلُوفَةِ (٢)',
    '➡ Revoir les mots familiers (2)',
    '➡ Review Common Words (2)',
  );

  const quizPromptHeading = pickLocalizedText(
    i18n.language,
    '🎯 الخُطْوَةُ الثَّالِثَةُ: التَّمْرِينُ اللَّطِيفُ — بَعْدَ أَنْ تَعَلَّمْنَا، اِخْتَرِ الإِجَابَةَ الصَّحِيحَةَ:',
    '🎯 Étape 3 : Exercice amical — Maintenant que tu as appris, choisis la bonne réponse :',
    '🎯 Step 3: Friendly Exercise — Now that we learned, pick the right answer:',
  );

  return (
    <div className="activity-stage activity-stage--interactive">
      <div className="activity-stage__header">
        <div>
          <span className="activity-stage__badge">🌟 {activity.objectiveCode}</span>
          <h4>{activity.title}</h4>
          <p className="activity-stage__prompt">{activity.spokenText}</p>
        </div>
        <AudioPromptButton text={activity.spokenText} />
      </div>

      {/* 3-Step Course Pedagogical Stepper: 1. Learn & Sound -> 2. Common Words -> 3. Friendly Exercise */}
      <div
        className="course-journey-stepper"
        role="group"
        aria-label={t('curriculum.activitiesHeading')}
      >
        <button
          type="button"
          className={`course-journey-step ${journeyStep === 1 ? 'course-journey-step--active' : ''} ${
            journeyStep > 1 ? 'course-journey-step--done' : ''
          }`}
          aria-current={journeyStep === 1 ? 'step' : undefined}
          onClick={() => setJourneyStep(1)}
        >
          {step1Label}
        </button>

        <button
          type="button"
          className={`course-journey-step ${journeyStep === 2 ? 'course-journey-step--active' : ''} ${
            journeyStep > 2 ? 'course-journey-step--done' : ''
          }`}
          aria-current={journeyStep === 2 ? 'step' : undefined}
          onClick={() => setJourneyStep(2)}
        >
          {step2Label}
        </button>

        <button
          type="button"
          className={`course-journey-step ${journeyStep === 3 ? 'course-journey-step--active' : ''} ${
            isCompleted ? 'course-journey-step--done' : ''
          }`}
          aria-current={journeyStep === 3 ? 'step' : undefined}
          onClick={() => setJourneyStep(3)}
        >
          {step3Label}
        </button>
      </div>

      {/* STEP 1: Learn the Concept & Sound First */}
      {journeyStep === 1 && (
        <div className="course-journey-panel" data-testid="journey-step-learn">
          <div className="lesson-teaching-banner">
            <span className="lesson-teaching-banner__text">{teacherIntroTitle}</span>
          </div>

          {payload.letterStudio && <LetterVowelStudio studio={payload.letterStudio} />}

          {payload.countingItems && <CountingPlayground config={payload.countingItems} />}

          {syllables.length > 0 && (
            <SyllableBuilderPlayground syllables={syllables} spokenText={activity.spokenText} />
          )}

          {learningCards.length > 0 && <LearningFlashcards cards={learningCards} />}

          <div className="course-journey-actions">
            <button
              type="button"
              className="journey-next-button"
              data-testid="go-to-step-2"
              onClick={() => setJourneyStep(2)}
            >
              {goToStep2Label}
            </button>
          </div>
        </div>
      )}

      {/* STEP 2: Common Words in Context (Animals, Fruits, Everyday Objects) */}
      {journeyStep === 2 && (
        <div className="course-journey-panel" data-testid="journey-step-words">
          <CommonWordsShowcase examples={wordExamples} />

          <div className="course-journey-actions">
            <button
              type="button"
              className="journey-back-button"
              onClick={() => setJourneyStep(1)}
            >
              {backToStep1Label}
            </button>
            <button
              type="button"
              className="journey-next-button"
              data-testid="go-to-step-3"
              onClick={() => setJourneyStep(3)}
            >
              {goToStep3Label}
            </button>
          </div>
        </div>
      )}

      {/* STEP 3: Friendly Exercise at the End */}
      {journeyStep === 3 && (
        <div className="course-journey-panel" data-testid="journey-step-exercise">
          <div className="quiz-challenge-box">
            <div className="quiz-challenge-box__header">
              <div>
                <p className="quiz-challenge-box__heading">{quizPromptHeading}</p>
                <p className="quiz-challenge-box__question">{activity.prompt}</p>
              </div>
              <AudioPromptButton text={activity.prompt} />
            </div>

            <div className="option-cards">
              {options.map((opt) => {
                const isChosen = selectedOptionId === opt.id;
                const isOptionCorrect = isChosen && isCorrect;
                const isOptionWrong = isChosen && isWrong;
                const label = pickLocalizedText(
                  i18n.language,
                  opt.labelAr,
                  opt.labelFr,
                  opt.labelEn,
                );
                return (
                  <button
                    key={opt.id}
                    type="button"
                    className={`option-card ${isChosen ? 'option-card--selected' : ''} ${
                      isOptionCorrect ? 'option-card--correct' : ''
                    } ${isOptionWrong ? 'option-card--wrong' : ''}`}
                    style={
                      { '--option-accent': opt.accentColor || '#f59e0b' } as React.CSSProperties
                    }
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

          <div className="course-journey-actions">
            <button
              type="button"
              className="journey-back-button"
              onClick={() => setJourneyStep(2)}
            >
              {backToStep2Label}
            </button>
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
      )}
    </div>
  );
}
