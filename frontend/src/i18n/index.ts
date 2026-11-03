import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import {
  type AppLanguage,
  DEFAULT_LANGUAGE,
  isSupportedLanguage,
  LANGUAGE_META,
  LANGUAGE_STORAGE_KEY,
  SUPPORTED_LANGUAGES,
} from './config';
import { resources, type TranslationResources } from './resources';

declare module 'i18next' {
  interface CustomTypeOptions {
    defaultNS: 'translation';
    resources: { translation: TranslationResources };
  }
}

/** Applies `lang` and `dir` on <html> so CSS logical properties and the browser bidi algorithm follow the language. */
export function applyDocumentLanguage(language: AppLanguage): void {
  const root = document.documentElement;
  root.lang = language;
  root.dir = LANGUAGE_META[language].dir;
}

function readStoredLanguage(): AppLanguage {
  try {
    const stored = window.localStorage.getItem(LANGUAGE_STORAGE_KEY);
    return isSupportedLanguage(stored) ? stored : DEFAULT_LANGUAGE;
  } catch {
    return DEFAULT_LANGUAGE; // storage unavailable (privacy mode, etc.)
  }
}

export function resolveLanguage(value: string | undefined): AppLanguage {
  return isSupportedLanguage(value) ? value : DEFAULT_LANGUAGE;
}

i18n.on('languageChanged', (language) => {
  const resolved = resolveLanguage(language);
  applyDocumentLanguage(resolved);
  try {
    window.localStorage.setItem(LANGUAGE_STORAGE_KEY, resolved);
  } catch {
    // Persisting the preference is best-effort.
  }
});

void i18n.use(initReactI18next).init({
  resources,
  lng: readStoredLanguage(),
  fallbackLng: DEFAULT_LANGUAGE,
  supportedLngs: [...SUPPORTED_LANGUAGES],
  initAsync: false,
  interpolation: { escapeValue: false }, // React already escapes output
});

applyDocumentLanguage(resolveLanguage(i18n.language));

export default i18n;

