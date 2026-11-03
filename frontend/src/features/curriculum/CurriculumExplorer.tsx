import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import {
  getCurriculumModules,
  getLessonBySlug,
  type LearningModule,
  type LessonCheckpoint,
  type LessonDetail,
} from '../../lib/api';

export interface ActiveSceneState {
  modelUrl: string;
  lessonTitle: string;
  lessonObjective: string;
  activeNodeName?: string;
  focusPoint?: [number, number, number];
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
 * Fetches localized curriculum modules and interactive 3D (.glb) lesson checkpoints
 * from the API and synchronizes the active 3D scene and selected checkpoint node.
 */
export function CurriculumExplorer({ onSceneChange }: CurriculumExplorerProps) {
  const { t, i18n } = useTranslation();
  const [selectedSlug, setSelectedSlug] = useState<string>('alhazen-optical-lantern');
  const [selectedStep, setSelectedStep] = useState<number>(1);
  const [state, setState] = useState<LoadState>({ status: 'loading' });

  useEffect(() => {
    const controller = new AbortController();
    const requestedLanguage = i18n.language;

    Promise.all([
      getCurriculumModules({ language: requestedLanguage, signal: controller.signal }),
      getLessonBySlug(selectedSlug, { language: requestedLanguage, signal: controller.signal }),
    ])
      .then(([overview, lesson]) => {
        setState({
          status: 'ready',
          requestedLanguage,
          modules: overview.modules,
          lesson,
        });
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
    const checkpoint: LessonCheckpoint | undefined =
      state.lesson.checkpoints.find((c) => c.stepOrder === selectedStep) ?? state.lesson.checkpoints[0];

    onSceneChange({
      modelUrl: state.lesson.asset.url,
      lessonTitle: state.lesson.title,
      lessonObjective: state.lesson.objective,
      activeNodeName: checkpoint?.targetNodeName,
      focusPoint: checkpoint?.focusPoint,
    });
  }, [state, selectedStep, onSceneChange]);

  const isLoading = state.status === 'loading' || (state.status === 'ready' && state.requestedLanguage !== i18n.language);

  return (
    <section className="card curriculum" aria-labelledby="curriculum-title" aria-live="polite">
      <h2 id="curriculum-title">{t('curriculum.title')}</h2>

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
                return (
                  <button
                    key={cp.stepOrder}
                    type="button"
                    role="tab"
                    aria-selected={isSelected}
                    className="checkpoint-tab"
                    onClick={() => setSelectedStep(cp.stepOrder)}
                  >
                    <span className="checkpoint-tab__step">{cp.stepOrder}</span>
                    <span>{cp.title}</span>
                  </button>
                );
              })}
            </div>

            {state.lesson.checkpoints
              .filter((cp) => cp.stepOrder === selectedStep)
              .map((cp) => (
                <div key={cp.stepOrder} className="checkpoint-detail" role="tabpanel">
                  <p className="checkpoint-detail__explanation">{cp.explanation}</p>
                  <p className="checkpoint-detail__node">
                    <span>{t('curriculum.targetNode')}: </span>
                    <code dir="ltr">{cp.targetNodeName}</code>
                  </p>
                </div>
              ))}
          </div>
        </>
      )}
    </section>
  );
}
