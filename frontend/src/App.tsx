import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { AppFooter } from './components/layout/AppFooter';
import { AppHeader } from './components/layout/AppHeader';
import { AgeWorldSelector } from './features/age-selector/AgeWorldSelector';
import { CurriculumWorkspace } from './features/curriculum/CurriculumWorkspace';
import { ParentProgressPanel } from './features/progress/ParentProgressPanel';
import { ApiStatus } from './features/system/ApiStatus';
import {
  completeActivity,
  getAgeGroups,
  getLearnerProgress,
  type AgeGroupSummary,
  type LearnerProgress,
} from './lib/api';

const DEFAULT_LEARNER_KEY = 'default';

type OverviewState =
  | { status: 'loading' }
  | { status: 'error' }
  | {
      status: 'ready';
      requestedLanguage: string;
      ageGroups: AgeGroupSummary[];
    };

export default function App() {
  const { t, i18n } = useTranslation();
  const [selectedAge, setSelectedAge] = useState<number | null>(null);
  const [showParentProgress, setShowParentProgress] = useState<boolean>(false);
  const [overview, setOverview] = useState<OverviewState>({ status: 'loading' });
  const [progress, setProgress] = useState<LearnerProgress | null>(null);

  useEffect(() => {
    const controller = new AbortController();
    const requestedLanguage = i18n.language;

    Promise.all([
      getAgeGroups({ language: requestedLanguage, signal: controller.signal }),
      getLearnerProgress(DEFAULT_LEARNER_KEY, {
        language: requestedLanguage,
        signal: controller.signal,
      }).catch(() => null),
    ])
      .then(([ageGroupsResponse, learnerProgress]) => {
        setOverview({
          status: 'ready',
          requestedLanguage,
          ageGroups: ageGroupsResponse.ageGroups,
        });
        if (learnerProgress) {
          setProgress(learnerProgress);
        }
      })
      .catch((error: unknown) => {
        if (!controller.signal.aborted) {
          console.warn('Age groups fetch failed', error);
          setOverview({ status: 'error' });
        }
      });

    return () => controller.abort();
  }, [i18n.language]);

  const handleCompleteActivity = (activitySlug: string) => {
    completeActivity(activitySlug, DEFAULT_LEARNER_KEY, { language: i18n.language })
      .then((updated) => {
        setProgress(updated);
      })
      .catch((error: unknown) => {
        console.warn('Activity completion failed', error);
      });
  };

  const isLoading =
    overview.status === 'loading' ||
    (overview.status === 'ready' && overview.requestedLanguage !== i18n.language);

  return (
    <div className="app">
      <AppHeader
        selectedAge={selectedAge}
        onSelectAge={(age) => setSelectedAge(age)}
        totalCompleted={progress?.totalCompletedActivities ?? 0}
        totalActivities={progress?.totalActivities ?? 0}
        onToggleProgress={() => setShowParentProgress((prev) => !prev)}
      />

      <main className="app__main">
        <section className="hero">
          <h1>{t('app.name')}</h1>
          <p className="hero__tagline">{t('app.tagline')}</p>
          <p className="hero__motto">{t('app.motto')}</p>
        </section>

        {showParentProgress && progress && (
          <ParentProgressPanel
            progress={progress}
            onClose={() => setShowParentProgress(false)}
          />
        )}

        {isLoading && <p>{t('curriculum.loading')}</p>}
        {overview.status === 'error' && <p className="notice">{t('curriculum.unavailable')}</p>}

        {overview.status === 'ready' && !isLoading && selectedAge === null && (
          <AgeWorldSelector
            ageGroups={overview.ageGroups}
            onSelectAge={(ageYears) => setSelectedAge(ageYears)}
          />
        )}

        {selectedAge !== null && (
          <CurriculumWorkspace
            key={selectedAge}
            ageYears={selectedAge}
            completedActivitySlugs={progress?.completedActivitySlugs ?? []}
            onCompleteActivity={handleCompleteActivity}
            onBackToAges={() => setSelectedAge(null)}
          />
        )}

        <div className="status-footer-section">
          <ApiStatus />
        </div>
      </main>

      <AppFooter />
    </div>
  );
}
