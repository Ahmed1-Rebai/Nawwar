import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import {
  completeCheckpoint,
  getCurriculumModules,
  getLearnerProgress,
  getLessonBySlug,
  type LearnerProgress,
  type LearningModule,
  type LessonCheckpoint,
  type LessonDetail,
} from '../../lib/api';

const DEFAULT_LEARNER_KEY = 'default';

export interface ActiveSceneState {
  modelUrl: string;
  lessonTitle: string;
  lessonObjective: string;
  activeNodeName?: string;
  focusPoint?: [number, number, number];
  onSelectNode?: (nodeName: string) => void;
}

interface CurriculumExplorerProps {
  onSceneChange: (scene: ActiveSceneState) => void;
}

type LoadState =
  | { status: 'loading' }
  | { status: 'error' }
  | {
      status: 'ready';
      requestedLanguage: string;
      modules: LearningModule[];
      lesson: LessonDetail;
    };

/**
 * Fetches localized curriculum modules, interactive 3D (.glb) lesson checkpoints,
 * and persistent learner progress from the API, synchronizing the active 3D scene
 * and supporting both tab selection and direct 3D mesh picking.
 */
export function CurriculumExplorer({ onSceneChange }: CurriculumExplorerProps) {
  const { t, i18n } = useTranslation();
  const [selectedSlug, setSelectedSlug] = useState<string>('alhazen-optical-lantern');
  const [selectedStep, setSelectedStep] = useState<number>(1);
  const [state, setState] = useState<LoadState>({ status: 'loading' });
  const [progress, setProgress] = useState<LearnerProgress | null>(null);

  useEffect(() => {
    const controller = new AbortController();
    const requestedLanguage = i18n.language;

    Promise.all([
      getCurriculumModules({ language: requestedLanguage, signal: controller.signal }),
      getLessonBySlug(selectedSlug, { language: requestedLanguage, signal: controller.signal }),
      getLearnerProgress(DEFAULT_LEARNER_KEY, { language: requestedLanguage, signal: controller.signal }).catch(
        () => null,
      ),
    ])
      .then(([overview, lesson, learnerProgress]) => {
        setState({
          status: 'ready',
          requestedLanguage,
          modules: overview.modules,
          lesson,
        });
        if (learnerProgress) {
          setProgress(learnerProgress);
        }
      })
      .catch((error: unknown) => {
        if (!controller.signal.aborted) {
          console.warn('Curriculum fetch failed', error);
          setState({ status: 'error' });
        }
      });

    return () => controller.abort();
  }, [i18n.language, selectedSlug]);

  useEffect(() => {
    if (state.status !== 'ready') return;
    const checkpoints = state.lesson.checkpoints;
    const checkpoint: LessonCheckpoint | undefined =
      checkpoints.find((c) => c.stepOrder === selectedStep) ?? checkpoints[0];

    onSceneChange({
      modelUrl: state.lesson.asset.url,
      lessonTitle: state.lesson.title,
      lessonObjective: state.lesson.objective,
      activeNodeName: checkpoint?.targetNodeName,
      focusPoint: checkpoint?.focusPoint,
      onSelectNode: (nodeName: string) => {
        const matched = checkpoints.find((c) => c.targetNodeName === nodeName);
        if (matched) {
          setSelectedStep(matched.stepOrder);
        }
      },
    });
  }, [state, selectedStep, onSceneChange]);

  const handleCompleteCheckpoint = (lessonSlug: string, stepOrder: number) => {
    completeCheckpoint(lessonSlug, stepOrder, DEFAULT_LEARNER_KEY, { language: i18n.language })
      .then((updated) => {
        setProgress(updated);
      })
      .catch((error: unknown) => {
        console.warn('Checkpoint completion failed', error);
      });
  };

  const isLoading = state.status === 'loading' || (state.status === 'ready' && state.requestedLanguage !== i18n.language);

  const completedStepsForLesson =
    state.status === 'ready' && progress
      ? (progress.lessons.find((l) => l.lessonSlug === state.lesson.slug)?.completedSteps ?? [])
      : [];

  return (
    <section className="card curriculum" aria-labelledby="curriculum-title" aria-live="polite">
      <div className="curriculum__header">
        <h2 id="curriculum-title">{t('curriculum.title')}</h2>
        {progress && (
          <span className="progress-pill" data-testid="progress-summary">
            <span>{t('curriculum.progressLabel')}: </span>
            <strong dir="ltr">
              {progress.totalCompletedSteps}/{progress.totalCheckpoints}
            </strong>
          </span>
        )}
      </div>

      {isLoading && <p>{t('curriculum.loading')}</p>}
      {state.status === 'error' && <p className="notice">{t('curriculum.unavailable')}</p>}

      {state.status === 'ready' && !isLoading && (
        <>
          <div className="module-list" role="list">
            {state.modules.map((module) => (
              <article key={module.slug} className="module-item" role="listitem">
                <div className="module-item__header">
                  <h3>{module.title}</h3>
                  <p>{module.summary}</p>
                </div>
                <div className="lesson-buttons">
                  {module.lessons.map((lesson) => {
                    const isActive = lesson.slug === state.lesson.slug;
                    return (
                      <button
                        key={lesson.slug}
                        type="button"
                        className="lesson-button"
                        aria-pressed={isActive}
                        onClick={() => {
                          setSelectedSlug(lesson.slug);
                          setSelectedStep(1);
                        }}
                      >
                        <span className="lesson-button__title">{lesson.title}</span>
                        <span className="lesson-button__badge" dir="ltr">
                          {lesson.modelUrl}
                        </span>
                      </button>
                    );
                  })}
                </div>
              </article>
            ))}
          </div>

          <div className="checkpoints">
            <h3>{t('curriculum.checkpointsTitle')}</h3>
            <div className="checkpoint-tabs" role="tablist" aria-label={t('curriculum.checkpointsTitle')}>
              {state.lesson.checkpoints.map((cp) => {
                const isSelected = cp.stepOrder === selectedStep;
                const isCompleted = completedStepsForLesson.includes(cp.stepOrder);
                return (
                  <button
                    key={cp.stepOrder}
                    type="button"
                    role="tab"
                    aria-selected={isSelected}
                    className="checkpoint-tab"
                    onClick={() => setSelectedStep(cp.stepOrder)}
                  >
                    <span className="checkpoint-tab__step">{isCompleted ? '✓' : cp.stepOrder}</span>
                    <span>{cp.title}</span>
                  </button>
                );
              })}
            </div>

            {state.lesson.checkpoints
              .filter((cp) => cp.stepOrder === selectedStep)
              .map((cp) => {
                const isCompleted = completedStepsForLesson.includes(cp.stepOrder);
                return (
                  <div key={cp.stepOrder} className="checkpoint-detail" role="tabpanel">
                    <p className="checkpoint-detail__explanation">{cp.explanation}</p>
                    <div className="checkpoint-detail__footer">
                      <p className="checkpoint-detail__node">
                        <span>{t('curriculum.targetNode')}: </span>
                        <code dir="ltr">{cp.targetNodeName}</code>
                      </p>
                      <button
                        type="button"
                        className="checkpoint-complete-button"
                        disabled={isCompleted}
                        onClick={() => handleCompleteCheckpoint(state.lesson.slug, cp.stepOrder)}
                      >
                        {isCompleted ? t('curriculum.completed') : t('curriculum.markComplete')}
                      </button>
                    </div>
                  </div>
                );
              })}
          </div>
        </>
      )}
    </section>
  );
}
