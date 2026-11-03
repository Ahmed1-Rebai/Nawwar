import { useTranslation } from 'react-i18next';
import type { LearnerProgress } from '../../lib/api';

interface ParentProgressPanelProps {
  progress: LearnerProgress;
  onClose: () => void;
}

/**
 * PHASE G — Parent & educator progress overview showing activity mastery across
 * the three preschool age groups (3, 4, and 5 years).
 */
export function ParentProgressPanel({ progress, onClose }: ParentProgressPanelProps) {
  const { t } = useTranslation();

  return (
    <section className="card parent-progress" aria-labelledby="parent-progress-title">
      <div className="parent-progress__header">
        <h2 id="parent-progress-title">{t('progress.title')}</h2>
        <button type="button" className="secondary-button" onClick={onClose}>
          {t('progress.close')}
        </button>
      </div>

      <p className="parent-progress__summary">
        <span>{t('progress.totalCompleted')}: </span>
        <strong dir="ltr">
          {progress.totalCompletedActivities} / {progress.totalActivities}
        </strong>
      </p>

      <div className="parent-progress__grid">
        {progress.byAge.map((ageItem) => {
          const percent =
            ageItem.totalActivities > 0
              ? Math.round((ageItem.completedActivities / ageItem.totalActivities) * 100)
              : 0;
          return (
            <div key={ageItem.ageYears} className="parent-progress__item">
              <div className="parent-progress__item-header">
                <strong>{ageItem.title}</strong>
                <span dir="ltr">
                  {ageItem.completedActivities}/{ageItem.totalActivities} ({percent}%)
                </span>
              </div>
              <div className="progress-bar" aria-hidden="true">
                <div className="progress-bar__fill" style={{ inlineSize: `${percent}%` }} />
              </div>
            </div>
          );
        })}
      </div>
    </section>
  );
}
