# Design System — NAWWAR (نوّار)

## 1. Aesthetic Direction: *"Nocturnal Observatory & Warm Lantern Light"*

NAWWAR pairs a deep nocturnal observatory palette (`#0b1424`–`#1a2c4b`) with warm amber lantern highlights (`#f4c76b`) inspired by classical Arabic optics treatises (*Kitāb al-Manāẓir*).

---

## 2. Color Tokens (`frontend/src/index.css`)

| Token | Value | Role |
|---|---|---|
| `--color-bg` | `#0b1424` | Page background |
| `--color-surface` | `#13213a` | Card & header surface |
| `--color-surface-raised` | `#1a2c4b` | Interactive module items, badges, and checkpoint panels |
| `--color-border` | `#2a4066` | Subtle structural borders |
| `--color-text` | `#f2f5fa` | Primary high-contrast text |
| `--color-text-muted` | `#a9b7cf` | Secondary text and descriptions |
| `--color-accent` | `#f4c76b` | Active selection, focus ring, and 3D checkpoint highlight |
| `--color-success` | `#4cc38a` | Online indicator and completed checkpoint badge |
| `--color-warning` | `#f0b34a` | Degraded state / notice border |
| `--color-danger` | `#ef6b6b` | Offline / error state |

---

## 3. Typography & RTL Layout Rules

- **Primary Font**: `@fontsource-variable/noto-sans-arabic` (`Noto Sans Arabic Variable`, WOFF2 self-hosted).
- **Arabic Script (`:lang(ar)`)**:
  - `line-height: 1.8` so full vowel marks (tashkeel) never collide or clip.
  - `letter-spacing: normal` (never apply positive tracking to Arabic text).
- **CSS Logical Properties**:
  - All spacing, borders, and alignment use logical properties (`margin-inline`, `padding-inline`, `border-inline-start`, `text-align: start`) so switching `document.documentElement.dir` between `rtl` and `ltr` flips the entire layout automatically.
