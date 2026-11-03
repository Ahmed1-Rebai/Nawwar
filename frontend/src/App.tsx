import { lazy, Suspense, useCallback, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { ErrorBoundary } from './components/ErrorBoundary';
import { LanguageSwitcher } from './components/LanguageSwitcher';
import { type ActiveSceneState, CurriculumExplorer } from './features/curriculum/CurriculumExplorer';
import { ApiStatus } from './features/system/ApiStatus';

const WorldCanvas = lazy(() => import('./features/world/WorldCanvas'));

export default function App() {
  const { t } = useTranslation();
  const [scene, setScene] = useState<ActiveSceneState>({
    modelUrl: '/models/lantern.glb',
    lessonTitle: '',
    lessonObjective: '',
  });

  const handleSceneChange = useCallback((next: ActiveSceneState) => {
    setScene(next);
  }, []);

  return (
    <div className="app">
      <header className="app__header">
        <div className="brand">
          <span className="brand__name">{t('app.name')}</span>
          <span className="brand__latin" lang="en" dir="ltr">
            {t('app.latinName')}
          </span>
        </div>
        <LanguageSwitcher />
      </header>

      <main className="app__main">
        <section className="hero">
          <h1>{t('app.name')}</h1>
          <p className="hero__tagline">{t('app.tagline')}</p>
          <p className="hero__motto">{t('app.motto')}</p>
        </section>

        <div className="grid">
          <section className="card card--world" aria-labelledby="world-title">
            <div className="world-header">
              <h2 id="world-title">{scene.lessonTitle || t('world.title')}</h2>
              <span className="world-badge" dir="ltr">
                {scene.modelUrl}
              </span>
            </div>
            <p>{scene.lessonObjective || t('world.description')}</p>
            <ErrorBoundary fallback={<p className="notice">{t('world.unavailable')}</p>}>
              <Suspense fallback={<div className="world-canvas world-canvas--loading">{t('world.loading')}</div>}>
                <WorldCanvas
                  label={t('world.canvasLabel')}
                  modelUrl={scene.modelUrl}
                  activeNodeName={scene.activeNodeName}
                  focusPoint={scene.focusPoint}
                />
              </Suspense>
            </ErrorBoundary>
          </section>

          <div className="sidebar-stack">
            <CurriculumExplorer onSceneChange={handleSceneChange} />
            <ApiStatus />
          </div>
        </div>
      </main>

      <footer className="app__footer">{t('footer.phase')}</footer>
    </div>
  );
}
