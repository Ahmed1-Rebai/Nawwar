import { lazy, Suspense } from 'react';
import { useTranslation } from 'react-i18next';
import { ErrorBoundary } from './components/ErrorBoundary';
import { LanguageSwitcher } from './components/LanguageSwitcher';
import { ApiStatus } from './features/system/ApiStatus';

const WorldCanvas = lazy(() => import('./features/world/WorldCanvas'));

export default function App() {
  const { t } = useTranslation();

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
            <h2 id="world-title">{t('world.title')}</h2>
            <p>{t('world.description')}</p>
            <ErrorBoundary fallback={<p className="notice">{t('world.unavailable')}</p>}>
              <Suspense fallback={<div className="world-canvas world-canvas--loading">{t('world.loading')}</div>}>
                <WorldCanvas label={t('world.canvasLabel')} />
              </Suspense>
            </ErrorBoundary>
          </section>

          <ApiStatus />
        </div>
      </main>

      <footer className="app__footer">{t('footer.phase')}</footer>
    </div>
  );
}
