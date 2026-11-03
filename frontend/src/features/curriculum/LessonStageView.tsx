import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import type { LessonDetail } from '../../lib/api';
import { ActivityRenderer } from '../activities/ActivityRenderer';

interface LessonStageViewProps {
  lesson: LessonDetail;
  completedActivitySlugs: string[];
  onCompleteActivity: (activitySlug: string) => void;
}

/**
 * Displays the active preschool Lesson and its ordered multi-modal Activities.
 */
export function LessonStageView({
  lesson,
  completedActivitySlugs,
  onCompleteActivity,
}: LessonStageViewProps) {
  const { t } = useTranslation();
  const [selectedStep, setSelectedStep] = useState<number>(1);

  const activeActivity =
    lesson.activities.find((a) => a.stepOrder === selectedStep) ?? lesson.activities[0];

  return (
    <section className="card lesson-stage" aria-labelledby="active-lesson-title">
      <div className="lesson-stage__header">
        <div>
          <span className="lesson-stage__course">{lesson.courseTitle}</span>
          <h3 id="active-lesson-title">{lesson.title}</h3>
          <p>{lesson.summary}</p>
        </div>
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
                className="checkpoint-tab"
                onClick={() => setSelectedStep(act.stepOrder)}
              >
                <span className="checkpoint-tab__step">{isDone ? '✓' : act.stepOrder}</span>
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
          isCompleted={completedActivitySlugs.includes(activeActivity.slug)}
          onComplete={onCompleteActivity}
        />
      )}
    </section>
  );
}
