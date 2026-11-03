# Localization & Arabic-First RTL Architecture (`docs/architecture/LOCALIZATION.md`)

NAWWAR is designed **Arabic-first (`ar`, RTL)** with full support for **French (`fr`, LTR)** and **English (`en`, LTR)**.

---

## 1. Supported Languages & Directionality

| Code | Native Name | Direction | Role |
|---|---|---|---|
| `ar` | العربية | `rtl` | Default & reference language |
| `fr` | Français | `ltr` | Supported secondary language |
| `en` | English | `ltr` | Supported secondary language |

Both the frontend ([`frontend/src/i18n/config.ts`](../../frontend/src/i18n/config.ts)) and backend ([`LocalizationSetup.cs`](../../backend/src/Nawwar.Api/Infrastructure/Localization/LocalizationSetup.cs)) share this exact list and default to `ar`.

---

## 2. Frontend Localization Rules

1. **Initial HTML State**: [`frontend/index.html`](../../frontend/index.html) declares `<html lang="ar" dir="rtl">` so first paint before JavaScript execution is already right-to-left.
2. **Compile-Time Translation Parity**:
   - [`frontend/src/i18n/resources.ts`](../../frontend/src/i18n/resources.ts) types `fr` and `en` with `satisfies typeof ar`.
   - Adding or renaming a key in `ar.json` without updating `fr.json` and `en.json` fails `npm run typecheck` (`tsc -b`) and `src/i18n/i18n.test.ts`.
3. **Document Direction Synchronization**:
   - `applyDocumentLanguage()` in [`frontend/src/i18n/index.ts`](../../frontend/src/i18n/index.ts) sets `document.documentElement.lang` and `document.documentElement.dir` on startup and on every `languageChanged` event.
4. **CSS Logical Properties**:
   - All layout rules in [`frontend/src/index.css`](../../frontend/src/index.css) use CSS logical properties (`margin-inline`, `padding-inline`, `border-inline-start`, `text-align: start`).
   - Physical direction properties (`left`, `right`, `margin-left`, `padding-right`, `text-align: left`) are forbidden for layout flow.
5. **Arabic Typography & Diacritics (Tashkeel)**:
   - `@fontsource-variable/noto-sans-arabic` is bundled locally (`woff2`) so Arabic glyph shaping does not depend on external CDNs.
   - `:lang(ar)` sets `line-height: 1.8` and `letter-spacing: normal` to prevent clipping of diacritics (`العِلْمُ نُورٌ`) or broken cursive ligatures.
   - Embedded LTR values (version numbers, technical codes) use `dir="ltr"` and `unicode-bidi: isolate`.

---

## 3. Backend Localization Rules

- `AddNawwarLocalization()` configures `RequestLocalizationOptions` with `DefaultRequestCulture("ar")`, `SupportedCultures = ["ar", "fr", "en"]`, and `ApplyCurrentCultureToResponseHeaders = true`.
- Full ICU globalization data (`icu-libs` + `icu-data-full`) is installed in the Alpine container image and `InvariantGlobalization` is explicitly `false`.

