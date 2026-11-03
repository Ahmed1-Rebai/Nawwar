import { afterAll, describe, expect, it } from 'vitest';
import i18n from './index';
import { DEFAULT_LANGUAGE, LANGUAGE_STORAGE_KEY } from './config';
import { resources } from './resources';

function flattenKeys(value: object, prefix = ''): string[] {
  return Object.entries(value).flatMap(([key, child]) =>
    typeof child === 'object' && child !== null ? flattenKeys(child as object, `${prefix}${key}.`) : [`${prefix}${key}`],
  );
}

describe('i18n', () => {
  afterAll(async () => {
    await i18n.changeLanguage(DEFAULT_LANGUAGE);
  });

  it('is Arabic and right-to-left by default', () => {
    expect(i18n.language).toBe('ar');
    expect(document.documentElement.lang).toBe('ar');
    expect(document.documentElement.dir).toBe('rtl');
  });

  it.each([
    ['fr', 'ltr'],
    ['en', 'ltr'],
    ['ar', 'rtl'],
  ])('switching to %s sets dir=%s and persists the choice', async (language, dir) => {
    await i18n.changeLanguage(language);
    expect(document.documentElement.lang).toBe(language);
    expect(document.documentElement.dir).toBe(dir);
    expect(window.localStorage.getItem(LANGUAGE_STORAGE_KEY)).toBe(language);
  });

  it('has the same translation keys in every language', () => {
    const reference = flattenKeys(resources.ar.translation).sort();
    expect(flattenKeys(resources.fr.translation).sort()).toEqual(reference);
    expect(flattenKeys(resources.en.translation).sort()).toEqual(reference);
  });

  it('keeps Arabic diacritics intact', () => {
    expect(resources.ar.translation.app.motto).toBe('العِلْمُ نُورٌ');
  });
});

