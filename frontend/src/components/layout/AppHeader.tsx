import { useTranslation } from 'react-i18next';
import { LanguageSwitcher } from '../LanguageSwitcher';

interface AppHeaderProps {
  selectedAge: number | null;
  onSelectAge: (age: number | null) => void;
  totalCompleted: number;
  totalActivities: number;
  onToggleProgress: () => void;
}

/**
 * Top navigation header displaying the NAWWAR brand, quick age-world switcher
 * (بَوَّابَةُ الأَعْمَارِ: 3 / 4 / 5 سنوات), parent progress button, and language switcher.
 */
export function AppHeader({
  selectedAge,
  onSelectAge,
  totalCompleted,
  totalActivities,
  onToggleProgress,
}: AppHeaderProps) {
  const { t } = useTranslation();

  return (
    <header className="app__header">
      <div className="brand-group">
        <button
          type="button"
          className="brand brand--button"
          onClick={() => onSelectAge(null)}
        >
          <span className="brand__name">{t('app.name')}</span>
          <span className="brand__latin" lang="en" dir="ltr">
            {t('app.latinName')}
          </span>
        </button>

        <nav className="age-quick-nav" aria-label={t('nav.chooseAge')}>
          <button
            type="button"
            className="age-quick-nav__item"
            aria-pressed={selectedAge === null}
            onClick={() => onSelectAge(null)}
          >
            {t('nav.allAges')}
          </button>
          <button
            type="button"
            className="age-quick-nav__item"
            aria-pressed={selectedAge === 3}
            onClick={() => onSelectAge(3)}
          >
            🌱 {t('nav.agePill3')}
          </button>
          <button
            type="button"
            className="age-quick-nav__item"
            aria-pressed={selectedAge === 4}
            onClick={() => onSelectAge(4)}
          >
            🧭 {t('nav.agePill4')}
          </button>
          <button
            type="button"
            className="age-quick-nav__item"
            aria-pressed={selectedAge === 5}
            onClick={() => onSelectAge(5)}
          >
            🌟 {t('nav.agePill5')}
          </button>
        </nav>
      </div>

      <div className="header-actions">
        <button
          type="button"
          className="progress-pill progress-pill--button"
          data-testid="progress-summary"
          onClick={onToggleProgress}
        >
          <span aria-hidden="true">⭐</span>
          <span>{t('nav.parentProgress')}: </span>
          <strong dir="ltr">
            {totalCompleted}/{totalActivities}
          </strong>
        </button>
        <LanguageSwitcher />
      </div>
    </header>
  );
}
