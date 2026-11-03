import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { AudioPromptButton } from '../../components/AudioPromptButton';
import {
  getAgeGroupDetail,
  getLessonBySlug,
  type AgeGroupDetail,
  type LessonDetail,
} from '../../lib/api';
import { CoursePathwayView } from './CoursePathwayView';
import { DomainGrid } from './DomainGrid';
import { LessonStageView } from './LessonStageView';

interface CurriculumWorkspaceProps {
  ageYears: number;
  completedActivitySlugs: string[];
  onCompleteActivity: (activitySlug: string) => void;
  onBackToAges: () => void;
}

type WorkspaceLoadState =
  | { status: 'loading' }
  | { status: 'error' }
  | {
      status: 'ready';
      requestedLanguage: string;
      ageYears: number;
      ageDetail: AgeGroupDetail;
      lesson: LessonDetail;
    };

/**
 * Orchestrates browsing an Age World (`AgeGroupDetail`):
 * LearningDomain -> CourseCollection -> Course -> Lesson -> Multi-Modal Activity.
 */
export function CurriculumWorkspace({
  ageYears,
  completedActivitySlugs,
  onCompleteActivity,
  onBackToAges,
}: CurriculumWorkspaceProps) {
  const { t, i18n } = useTranslation();
  const [selectedDomainSlug, setSelectedDomainSlug] = useState<string>('');
  const [selectedLessonSlug, setSelectedLessonSlug] = useState<string>('');
  const [state, setState] = useState<WorkspaceLoadState>({ status: 'loading' });

  useEffect(() => {
    const controller = new AbortController();
    const requestedLanguage = i18n.language;

    getAgeGroupDetail(ageYears, { language: requestedLanguage, signal: controller.signal })
      .then(async (ageDetail) => {
        const defaultDomain = ageDetail.domains[0];
        const firstLessonSlug =
          defaultDomain?.collections[0]?.courses[0]?.lessons[0]?.slug ?? 'red-apple-yellow-sun-garden';
        const targetLessonSlug = selectedLessonSlug || firstLessonSlug;

        const lesson = await getLessonBySlug(targetLessonSlug, {
          language: requestedLanguage,
          signal: controller.signal,
        });

        setState({
          status: 'ready',
          requestedLanguage,
          ageYears,
          ageDetail,
          lesson,
        });
      })
      .catch((error: unknown) => {
        if (!controller.signal.aborted) {
          console.warn('Age world curriculum load failed', error);
          setState({ status: 'error' });
        }
      });

    return () => controller.abort();
  }, [ageYears, i18n.language, selectedLessonSlug]);

  const isLoading =
    state.status === 'loading' ||
    (state.status === 'ready' &&
      (state.requestedLanguage !== i18n.language || state.ageYears !== ageYears));

  if (isLoading) {
    return (
      <section className="card" aria-live="polite">
        <p>{t('curriculum.loading')}</p>
      </section>
    );
  }

  if (state.status === 'error' || state.status !== 'ready') {
    return (
      <section className="card" aria-live="polite">
        <p className="notice">{t('curriculum.unavailable')}</p>
      </section>
    );
  }

  const { ageDetail, lesson } = state;
  const activeDomain =
    ageDetail.domains.find((d) => d.slug === selectedDomainSlug) ?? ageDetail.domains[0];

  const handleDomainSelect = (domainSlug: string) => {
    setSelectedDomainSlug(domainSlug);
    const domain = ageDetail.domains.find((d) => d.slug === domainSlug);
    const firstLesson = domain?.collections[0]?.courses[0]?.lessons[0]?.slug;
    if (firstLesson) {
      setSelectedLessonSlug(firstLesson);
    }
  };

  return (
    <div className="workspace">
      <section className={`world-banner world-banner--${ageDetail.themeKey}`}>
        <div className="world-banner__content">
          <div className="world-banner__title-row">
            <span className="world-banner__emoji" aria-hidden="true">
              {ageDetail.iconEmoji}
            </span>
            <h2>{ageDetail.title}</h2>
          </div>
          <p className="world-banner__subtitle">{ageDetail.subtitle}</p>
          <p className="world-banner__description">{ageDetail.description}</p>
        </div>

        <div className="world-banner__actions">
          <AudioPromptButton text={`${ageDetail.title}. ${ageDetail.subtitle}`} />
          <button type="button" className="secondary-button" onClick={onBackToAges}>
            {t('nav.allAges')}
          </button>
        </div>
      </section>

      <DomainGrid
        domains={ageDetail.domains}
        selectedDomainSlug={activeDomain?.slug ?? ''}
        onSelectDomain={handleDomainSelect}
      />

      <div className="workspace-split">
        <div className="workspace-split__pathway">
          {activeDomain && (
            <CoursePathwayView
              collections={activeDomain.collections}
              activeLessonSlug={lesson.slug}
              onSelectLesson={(slug) => setSelectedLessonSlug(slug)}
            />
          )}
        </div>

        <div className="workspace-split__stage">
          <LessonStageView
            key={lesson.slug}
            lesson={lesson}
            completedActivitySlugs={completedActivitySlugs}
            onCompleteActivity={onCompleteActivity}
          />
        </div>
      </div>
    </div>
  );
}
