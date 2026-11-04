import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import type { LessonDetail } from '../../lib/api';
import { ActivityRenderer } from '../activities/ActivityRenderer';
import { pickLocalizedText } from '../activities/types';

interface LessonStageViewProps {
  lesson: LessonDetail;
  completedActivitySlugs: string[];
  onCompleteActivity: (activitySlug: string) => void;
  nextLessonSlug?: string | null;
  onSelectLesson?: (lessonSlug: string) => void;
}

/**
 * Displays the active preschool Lesson, its visual star progress bar,
 * its ordered multi-modal Activities, and smooth Next Activity / Next Lesson navigation.
 */
export function LessonStageView({
  lesson,
  completedActivitySlugs,
  onCompleteActivity,
  nextLessonSlug,
  onSelectLesson,
}: LessonStageViewProps) {
  const { t, i18n } = useTranslation();
  const [selectedStep, setSelectedStep] = useState<number>(1);

  const activeActivity =
    lesson.activities.find((a) => a.stepOrder === selectedStep) ?? lesson.activities[0];

  const completedInLesson = lesson.activities.filter((a) =>
    completedActivitySlugs.includes(a.slug),
  ).length;
  const totalInLesson = lesson.activities.length;
  const progressPercent =
    totalInLesson > 0 ? Math.round((completedInLesson / totalInLesson) * 100) : 0;

  const isCurrentActivityDone = Boolean(
    activeActivity && completedActivitySlugs.includes(activeActivity.slug),
  );
  const nextStepActivity = lesson.activities.find(
    (a) => a.stepOrder === (activeActivity?.stepOrder ?? 1) + 1,
  );

  const nextActivityLabel = pickLocalizedText(
    i18n.language,
    'النَّشَاطُ التَّالِي ⬅',
    'Activité suivante ⬅',
    'Next Activity ⬅',
  );

  const nextLessonLabel = pickLocalizedText(
    i18n.language,
    'الدَّرْسُ التَّالِي ⬅',
    'Leçon suivante ⬅',
    'Next Lesson ⬅',
  );

  return (
    <section
      id="lesson-stage-anchor"
      className="card lesson-stage"
      aria-labelledby="active-lesson-title"
    >
      <div className="lesson-stage__header">
        <div className="lesson-stage__title-group">
          <span className="lesson-stage__course">📚 {lesson.courseTitle}</span>
          <h3 id="active-lesson-title">{lesson.title}</h3>
          <p>{lesson.summary}</p>
        </div>

        <div className="lesson-stage__progress-pill" aria-label={t('curriculum.progressLabel')}>
          <span className="lesson-stage__stars">
            {'⭐'.repeat(Math.max(1, completedInLesson))}
          </span>
          <span className="lesson-stage__progress-count">
            {completedInLesson}/{totalInLesson}
          </span>
        </div>
      </div>

      <div className="lesson-progress-track" aria-hidden="true">
        <div className="lesson-progress-fill" style={{ inlineSize: `${progressPercent}%` }} />
      </div>

      {lesson.activities.length > 1 && (
        <div
          className="checkpoint-tabs"
          role="tablist"
          aria-label={t('curriculum.activitiesHeading')}
        >
          {lesson.activities.map((act) => {
            const isSelected = act.stepOrder === (activeActivity?.stepOrder ?? 1);
            const isDone = completedActivitySlugs.includes(act.slug);
            return (
              <button
                key={act.slug}
                type="button"
                role="tab"
                aria-selected={isSelected}
                className={`checkpoint-tab ${isDone ? 'checkpoint-tab--done' : ''}`}
                onClick={() => setSelectedStep(act.stepOrder)}
              >
                <span className="checkpoint-tab__step">{isDone ? '⭐' : act.stepOrder}</span>
                <span>{act.title}</span>
              </button>
            );
          })}
        </div>
      )}

      {activeActivity && (
        <ActivityRenderer
          key={activeActivity.slug}
          activity={activeActivity}
          isCompleted={isCurrentActivityDone}
          onComplete={onCompleteActivity}
        />
      )}

      {isCurrentActivityDone && (nextStepActivity || (nextLessonSlug && onSelectLesson)) && (
        <div className="lesson-stage__next-bar">
          {nextStepActivity ? (
            <button
              type="button"
              className="next-step-button"
              onClick={() => setSelectedStep(nextStepActivity.stepOrder)}
            >
              {nextActivityLabel}
            </button>
          ) : (
            nextLessonSlug &&
            onSelectLesson && (
              <button
                type="button"
                className="next-step-button"
                onClick={() => onSelectLesson(nextLessonSlug)}
              >
                {nextLessonLabel}
              </button>
            )
          )}
        </div>
      )}
    </section>
  );
}
