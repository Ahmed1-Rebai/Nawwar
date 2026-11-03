import ar from './locales/ar.json';
import en from './locales/en.json';
import fr from './locales/fr.json';

/**
 * Translation resources. Arabic is the reference shape: `satisfies typeof ar`
 * makes the type checker reject French/English files with missing keys.
 */
export const resources = {
  ar: { translation: ar },
  fr: { translation: fr satisfies typeof ar },
  en: { translation: en satisfies typeof ar },
} as const;

export type TranslationResources = typeof ar;

