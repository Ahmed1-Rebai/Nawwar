# API Design — NAWWAR (`Nawwar.Api`)

## 1. Conventions

- **Base Path**: `/api/v1`
- **Localization**: Reads `Accept-Language` (`ar`, `fr`, `en`; defaults to `ar`) and echoes `Content-Language` on every response.
- **Error Format**: RFC 9457 Problem Details (`application/problem+json`).

---

## 2. Endpoints

### `GET /health/live`
Liveness probe (`200 OK`).

### `GET /health/ready`
Readiness probe (`200 OK` when PostgreSQL connectivity check succeeds, `503 Service Unavailable` otherwise).

### `GET /api/v1/system/info`
Returns API identity, version, resolved culture, text direction (`rtl` or `ltr`), default culture (`ar`), and supported cultures.

### `GET /api/v1/curriculum/age-groups`
Returns localized summaries for the three preschool age worlds (**3, 4, and 5 years**):
- **Age 3 (`ageYears: 3`)**: `عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ`
- **Age 4 (`ageYears: 4`)**: `عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ`
- **Age 5 (`ageYears: 5`)**: `عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ`

```json
{
  "culture": "ar",
  "direction": "rtl",
  "ageGroups": [
    {
      "ageYears": 3,
      "slug": "age-3-discovery",
      "themeKey": "discovery",
      "iconEmoji": "🌱",
      "title": "عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ",
      "subtitle": "الاِكْتِشَافُ الحِسِّيُّ وَالتَّعَرُّفُ الأَسَاسِيُّ بِالصَّوْتِ وَاللَّعِبِ",
      "description": "أَنْشِطَةٌ قَصِيرَةٌ وَمَرِحَةٌ تَعْتَمِدُ عَلَى الصَّوْتِ وَالصُّورَةِ وَالمَشَاهِدِ ثُلَاثِيَّةِ الأَبْعَادِ لِلتَّعَرُّفِ عَلَى الأَلْوَانِ وَالأَشْكَالِ وَالحَيَوَانَاتِ وَالأَعْدَادِ (1–3).",
      "domainCount": 3,
      "courseCount": 4,
      "activityCount": 5
    }
  ]
}
```

### `GET /api/v1/curriculum/age-groups/{ageYears:int}`
Returns the full 7-level curriculum tree (`AgeGroup` → `LearningDomain` → `CourseCollection` → `Course` → `LearningObjective` + `Lesson` → `Activity`) for `{ageYears}` (`3`, `4`, or `5`), including the dedicated Age 5 Arabic reading pathway (`isReadingPathway: true`, **`مَسَارُ تَعَلُّمِ القِرَاءَةِ`**) with its 6 sequential reading courses. Returns `404 Not Found` (`application/problem+json`) if `{ageYears}` is outside `3..5`.

### `GET /api/v1/curriculum/lessons/{slug}`
Returns a single localized preschool lesson and its ordered multi-modal activities (`scene_3d_explore`, `audio_listen_pick`, `visual_match_sort`, `counting_interactive`, `letter_forms_vowels`, `syllable_word_builder`), including optional `.glb` model URL (`glbModelUrl`), 3D node target (`targetNodeName`), focus coordinates (`focusPoint`), spoken Arabic prompt (`spokenText`), and structured JSON payload (`interactionPayloadJson`). Returns `404 Not Found` (`application/problem+json`) if `{slug}` does not exist.

### `GET /api/v1/curriculum/progress?learnerKey={learnerKey}`
Returns completed curriculum activities across all three preschool age groups (`3`, `4`, `5`) for `learnerKey` (defaults to `"default"`).

```json
{
  "learnerKey": "default",
  "totalCompletedActivities": 1,
  "totalActivities": 19,
  "completedActivitySlugs": ["age5-rd4-build-kataba"],
  "byAge": [
    {
      "ageYears": 3,
      "title": "عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ",
      "completedActivities": 0,
      "totalActivities": 5
    },
    {
      "ageYears": 4,
      "title": "عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ",
      "completedActivities": 0,
      "totalActivities": 5
    },
    {
      "ageYears": 5,
      "title": "عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ",
      "completedActivities": 1,
      "totalActivities": 9
    }
  ]
}
```

### `POST /api/v1/curriculum/activities/{activitySlug}/complete`
Idempotently records that `learnerKey` completed activity `{activitySlug}` and returns the updated `LearnerProgressResponse`. Returns `404 Not Found` (`application/problem+json`) if `{activitySlug}` does not exist.
