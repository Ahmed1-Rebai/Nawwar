/**
 * Language configuration — single source of truth for the frontend.
 * Keep in sync with the API (backend/src/Nawwar.Api/Infrastructure/Localization/LocalizationSetup.cs).
 */
export const SUPPORTED_LANGUAGES = ['ar', 'fr', 'en'] as const;

export type AppLanguage = (typeof SUPPORTED_LANGUAGES)[number];

export type TextDirection = 'rtl' | 'ltr';

/** Arabic is the default and primary language. */
export const DEFAULT_LANGUAGE: AppLanguage = 'ar';

export const LANGUAGE_STORAGE_KEY = 'nawwar.language';

export const LANGUAGE_META: Record<AppLanguage, { nativeName: string; dir: TextDirection }> = {
  ar: { nativeName: 'العربية', dir: 'rtl' },
  fr: { nativeName: 'Français', dir: 'ltr' },
  en: { nativeName: 'English', dir: 'ltr' },
};

export function isSupportedLanguage(value: unknown): value is AppLanguage {
  return typeof value === 'string' && (SUPPORTED_LANGUAGES as readonly string[]).includes(value);
}

