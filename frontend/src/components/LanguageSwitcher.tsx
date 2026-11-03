import { useTranslation } from 'react-i18next';
import { type AppLanguage, LANGUAGE_META, SUPPORTED_LANGUAGES } from '../i18n/config';
import { resolveLanguage } from '../i18n';

export function LanguageSwitcher() {
  const { t, i18n } = useTranslation();
  const current = resolveLanguage(i18n.language);

  return (
    <nav className="language-switcher" aria-label={t('language.label')}>
      {SUPPORTED_LANGUAGES.map((language: AppLanguage) => (
        <button
          key={language}
          type="button"
          lang={language}
          dir={LANGUAGE_META[language].dir}
          aria-pressed={language === current}
          onClick={() => void i18n.changeLanguage(language)}
        >
          {LANGUAGE_META[language].nativeName}
        </button>
      ))}
    </nav>
  );
}

