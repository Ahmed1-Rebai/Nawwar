# ARCHITECTURE.md — NAWWAR Technical Architecture

> This document describes both the **currently running infrastructure/codebase** and the **realigned preschool educational architecture (`ADR-0009`, Phases A–I)** for **NAWWAR (نَوَّر)** — an Arabic-first, immersive 3D educational platform for children aged **3, 4, and 5 years**.

---

## 1. System Overview

NAWWAR is a containerized, Arabic-first 3D educational web application for preschool children (ages 3, 4, and 5) composed of three core services and one optional object-storage service:

```mermaid
flowchart LR
    Browser["Browser (RTL/LTR React 19 + R3F)"]
    Frontend["frontend (nginx-unprivileged :8080)"]
    Api["api (ASP.NET Core 10 :8080)"]
    Db[("db (PostgreSQL 18 :5432)")]
    Storage[("storage (RustFS S3 :9000, optional profile)")]

    Browser -->|"HTTP :8090 (prod) / :5173 (dev)"| Frontend
    Frontend -->|"Static SPA + /assets/*"| Browser
    Frontend -->|"Reverse proxy /api/* -> http://api:8080"| Api
    Api -->|"EF Core 10 / Npgsql (role: nawwar_app)"| Db
    Api -.->|"S3 HTTP (when storage profile enabled)"| Storage
```

---

## 2. Key Architecture Decisions

All architectural choices are recorded as ADRs in [`DECISIONS.md`](DECISIONS.md):
- **ADR-0001**: Docker-first development and validation across two isolated bridge networks (`frontend_net`, `backend_net`).
- **ADR-0002**: ASP.NET Core 10 Minimal API (`Nawwar.Api`) with EF Core 10 and PostgreSQL 18.
- **ADR-0003**: React 19 + TypeScript 7 + Vite 8 + React Three Fiber (`@react-three/fiber`) with lazy-loaded 3D canvas chunking.
- **ADR-0004**: Arabic (`ar`, `rtl`) as the default culture on both frontend and backend, backed by CSS logical properties and compile-time translation key parity (`satisfies typeof ar`).
- **ADR-0005**: Same-origin `/api` reverse proxy (nginx in production, Vite dev server in development) eliminating cross-origin credential and CORS fragility.
- **ADR-0006**: Least-privilege PostgreSQL role (`nawwar_app`) created automatically on first volume initialization.
- **ADR-0007**: Optional S3-compatible object storage using `rustfs/rustfs:1.0.4` under a Compose profile (`storage`), replacing discontinued upstream MinIO Docker Hub images.
- **ADR-0008**: Mandatory binary glTF 2.0 (`.glb`) standard (`model/gltf-binary`) for 3D scene assets.
- **ADR-0009**: Product realignment to the preschool age-based curriculum (Ages 3, 4, 5 — *عَالَمُ الاِكْتِشَافِ*، *عَالَمُ الاِسْتِكْشَافِ*، *عَالَمُ التَّعَلُّمِ*), decoupling lessons/activities from single `.glb` files, and deferring authentication until `PHASE H`.

---

## 3. Frontend Architecture (`frontend/`)

- **Entry & bootstrapping** ([`src/main.tsx`](frontend/src/main.tsx)):
  - Loads self-hosted `@fontsource-variable/noto-sans-arabic` WOFF2 font subsets.
  - Initializes `src/i18n` before first render so `<html lang="..." dir="...">` matches the active language immediately.
- **Organized UI Structure**:
  - [`src/components/layout/AppHeader.tsx`](frontend/src/components/layout/AppHeader.tsx) & [`AppFooter.tsx`](frontend/src/components/layout/AppFooter.tsx): Brand header, age-world return button, parent progress drawer toggle, and language switcher.
  - [`src/components/AudioPromptButton.tsx`](frontend/src/components/AudioPromptButton.tsx): Child-friendly Arabic speech synthesis (`window.speechSynthesis`, `ar-SA`) for vocalized prompts.
  - [`src/features/age-selector/AgeWorldSelector.tsx`](frontend/src/features/age-selector/AgeWorldSelector.tsx): Primary landing screen presenting the three preschool age worlds (**3 سنوات**، **4 سنوات**، **5 سنوات**).
  - [`src/features/curriculum/`](frontend/src/features/curriculum/CurriculumWorkspace.tsx): `CurriculumWorkspace.tsx`, `DomainGrid.tsx`, `CoursePathwayView.tsx` (highlighting `مَسَارُ تَعَلُّمِ القِرَاءَةِ`), and `LessonStageView.tsx` (with lesson progress bar and next-activity/next-lesson progression).
  - [`src/features/activities/`](frontend/src/features/activities/ActivityRenderer.tsx): `ActivityRenderer.tsx`, `Scene3DExploreActivity.tsx`, `InteractiveChoiceActivity.tsx`, `LearningFlashcards.tsx`, `CountingPlayground.tsx`, `LetterVowelStudio.tsx`, `SyllableBuilderPlayground.tsx`, and `types.ts`.
  - [`src/features/progress/ParentProgressPanel.tsx`](frontend/src/features/progress/ParentProgressPanel.tsx): Parent progress overview across Ages 3, 4, and 5.
  - [`src/styles/`](frontend/src/styles/tokens.css): Modular stylesheets (`tokens.css`, `layout.css`, `age-worlds.css`, `activities.css`) using CSS logical properties exclusively.
- **API client** ([`src/lib/api.ts`](frontend/src/lib/api.ts)):
  - Issues same-origin requests to `/api/v1/...` with an `Accept-Language` header matching `i18n.language`.

---

## 4. 3D Rendering Architecture (`frontend/src/features/world/`)

- **Code splitting**: [`WorldCanvas.tsx`](frontend/src/features/world/WorldCanvas.tsx) is loaded via `React.lazy` in `Scene3DExploreActivity.tsx`, keeping the `three` + `@react-three/fiber` + `@react-three/drei` bundle out of the initial critical path.
- **Playful Preschool Binary `.glb` Pipeline** ([`WorldScene.tsx`](frontend/src/features/world/WorldScene.tsx)):
  - Enforces `.glb` format (`isValidGlbUrl(modelUrl)`) and loads binary glTF 2.0 preschool models (`/models/garden-colors.glb`, `/models/friendly-farm.glb`, `/models/counting-blocks.glb`, `/models/arabic-letters-stage.glb`) via `useGLTF` from `@react-three/drei`.
  - Clones the loaded scene graph so material highlight state stays isolated per instance, highlights the mesh matching the active activity's `TargetNodeName`, supports direct 3D mesh picking (`onClick` → `onSelectNode`), disposes cloned materials on unmount, and renders a focus ring at the activity's 3D `FocusPoint`.
  - Checks `window.matchMedia('(prefers-reduced-motion: reduce)')` and pauses frame rotation when reduced motion is requested.
- **Fault isolation**: [`ErrorBoundary.tsx`](frontend/src/components/ErrorBoundary.tsx) catches WebGL or asset errors and displays a localized fallback notice.

---

## 5. Educational Content Architecture (`Features/Curriculum` & `features/curriculum`)

### 5.1 Implemented 7-Level Preschool Curriculum (`PHASE C` – `PHASE G`)

Per [`docs/product/AGE_BASED_CURRICULUM.md`](docs/product/AGE_BASED_CURRICULUM.md) and `ADR-0009`, educational content is separated from presentation and 3D scene implementation across a 7-level hierarchy in PostgreSQL 18:

```mermaid
erDiagram
    AGE_GROUPS ||--o{ LEARNING_DOMAINS : organizes
    LEARNING_DOMAINS ||--o{ COURSE_COLLECTIONS : groups
    COURSE_COLLECTIONS ||--o{ COURSES : contains
    COURSES ||--o{ LEARNING_OBJECTIVES : defines
    COURSES ||--o{ LESSONS : sequences
    LESSONS ||--o{ ACTIVITIES : delivers
    ACTIVITY_COMPLETIONS }o--|| ACTIVITIES : tracks_slug
```

- **Core Curriculum Entities (`backend/src/Nawwar.Api/Features/Curriculum/Domain/`)**:
  - `AgeGroup`: `3` (*عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ*), `4` (*عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ*), `5` (*عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ*).
  - `LearningDomain`: Domain within an age group (e.g., *الأَلْوَانُ وَالأَشْكَالُ*، *الأَصْوَاتُ وَالحَيَوَانَاتُ*، *الحُرُوفُ الهِجَائِيَّةُ*، *الأَعْدَادُ وَالحِسَابُ*، *اللُّغَةُ العَرَبِيَّةُ وَالقِرَاءَةُ*).
  - `CourseCollection`: Ordered pathway within a domain, including the dedicated Age 5 Arabic reading pathway (**`مَسَارُ تَعَلُّمِ القِرَاءَةِ`**, `IsReadingPathway = true`) with 6 sequential courses:
    1. `اِكْتِشَافُ الحُرُوفِ`
    2. `أَشْكَالُ الحُرُوفِ`
    3. `الحَرَكَاتُ القَصِيرَةُ` (*الفَتْحَةُ، الكَسْرَةُ، الضَّمَّةُ*)
    4. `بِنَاءُ المَقَاطِعِ`
    5. `قِرَاءَةُ الكَلِمَاتِ`
    6. `قِرَاءَةُ الجُمَلِ البَسِيطَةِ`
  - `Course`: Structured course with localized `TitleAr`/`Fr`/`En`, `SummaryAr`/`Fr`/`En`, `IconEmoji`, and linked `LearningObjectives` + `Lessons`.
  - `LearningObjective`: Measurable early-childhood learning outcome with unique code (`AGE3-CLR-1`, `AGE5-RD-4`, etc.).
  - `Lesson`: Pedagogical lesson unit within a course (decoupled from any single `.glb` file).
  - `Activity`: Multi-modal interactive step (`scene_3d_explore`, `audio_listen_pick`, `visual_match_sort`, `counting_interactive`, `letter_forms_vowels`, `syllable_word_builder`) with optional `.glb` scene asset (`GlbModelUrl`) and JSON interaction payload (`InteractionPayloadJson`).
  - `ActivityCompletion`: Tracks completion across activities and age worlds (`AgeYears`) without requiring authentication (`PHASE G`), ready to bind to learner profiles in `PHASE H`.

---

## 6. Localization Architecture

- **Frontend** ([`frontend/src/i18n/`](frontend/src/i18n/index.ts)):
  - Supported languages: `ar` (default, `rtl`), `fr` (`ltr`), `en` (`ltr`).
  - `resources.ts` enforces compile-time key completeness of `fr.json` and `en.json` against `ar.json` using TypeScript's `satisfies typeof ar`.
  - Language changes update `document.documentElement.lang`, `document.documentElement.dir`, and `localStorage['nawwar.language']`.
  - Layout styles in [`frontend/src/index.css`](frontend/src/index.css) exclusively use CSS logical properties (`margin-inline`, `padding-inline`, `border-inline-start`, `text-align: start`).
- **Backend** ([`LocalizationSetup.cs`](backend/src/Nawwar.Api/Infrastructure/Localization/LocalizationSetup.cs)):
  - Configures ASP.NET Core `RequestLocalizationMiddleware` with default culture `ar` and supported cultures `["ar", "fr", "en"]`.
  - Sets `ApplyCurrentCultureToResponseHeaders = true` so every response includes `Content-Language`.

---

## 7. Backend Architecture (`backend/`)

- **Solution**: [`backend/Nawwar.slnx`](backend/Nawwar.slnx) containing `src/Nawwar.Api` and `tests/Nawwar.Api.Tests`.
- **Pipeline** ([`Program.cs`](backend/src/Nawwar.Api/Program.cs)):
  1. `UseForwardedHeaders()` — honors `X-Forwarded-For` and `X-Forwarded-Proto` from the nginx reverse proxy.
  2. `UseExceptionHandler()` & `UseStatusCodePages()` — emits RFC 9457 `application/problem+json` error payloads.
  3. `UseRequestLocalization()` — resolves `ar` (default), `fr`, or `en`.
  4. `MapOpenApi()` — exposes `/openapi/v1.json` in `Development`.
  5. Health endpoints (`/health/live`, `/health/ready`) and versioned API group (`/api/v1`).

---

## 8. Database Model and Persistence (`NawwarDbContext`)

- **Engine**: PostgreSQL 18 (`postgres:18-alpine`) with persistent data stored in the `postgres_data` volume at `/var/lib/postgresql`.
- **Context**: [`NawwarDbContext.cs`](backend/src/Nawwar.Api/Infrastructure/Persistence/NawwarDbContext.cs) registered with Npgsql (`EnableRetryOnFailure()`) in [`PersistenceSetup.cs`](backend/src/Nawwar.Api/Infrastructure/Persistence/PersistenceSetup.cs).
- **Provisioning**: On first initialization of an empty volume, [`infra/postgres/init/10-create-app-role.sh`](infra/postgres/init/10-create-app-role.sh) creates the non-superuser role `NAWWAR_DB_USER` (`nawwar_app`), transfers ownership of database `POSTGRES_DB` (`nawwar`) to it, and revokes `PUBLIC` connect privileges.
- **Schema & Seeding**: [`CurriculumInitializer.cs`](backend/src/Nawwar.Api/Infrastructure/Persistence/CurriculumInitializer.cs) ensures the 7-level preschool curriculum tables (`age_groups`, `learning_domains`, `course_collections`, `courses`, `learning_objectives`, `lessons`, `activities`, and `activity_completions`) exist on startup and seeds Ages 3, 4, and 5 (see [`docs/architecture/DATABASE_SCHEMA.md`](docs/architecture/DATABASE_SCHEMA.md)).

---

## 9. API Structure

| Method & Path | Purpose | Auth | Response |
|---|---|---|---|
| `GET /health/live` | Process liveness probe (no DB check) | Public | `200 OK` (`Healthy`) |
| `GET /health/ready` | Readiness probe (verifies PostgreSQL connectivity via `NawwarDbContext`) | Internal / Probe | `200 OK` (`Healthy`) or `503 Service Unavailable` |
| `GET /api/v1/system/info` | Service metadata and resolved request culture/direction | Public | `200 OK` JSON (`SystemInfoResponse`) + `Content-Language` header |
| `GET /api/v1/curriculum/age-groups` | Localized summaries of the 3 preschool age worlds (`3`, `4`, `5` years) | Public | `200 OK` JSON (`AgeGroupsOverviewResponse`) |
| `GET /api/v1/curriculum/age-groups/{ageYears:int}` | Full 7-level curriculum tree for age `3`, `4`, or `5` (domains, collections, `مَسَارُ تَعَلُّمِ القِرَاءَةِ`, courses, objectives, lessons, activities) | Public | `200 OK` JSON (`AgeGroupDetailResponse`) or `404` Problem Details |
| `GET /api/v1/curriculum/lessons/{slug}` | Localized preschool lesson detail and ordered multi-modal activities | Public | `200 OK` JSON (`LessonDetailResponse`) or `404` Problem Details |
| `GET /api/v1/curriculum/progress` | Completed activities across all 3 preschool age worlds for a learner | Public | `200 OK` JSON (`LearnerProgressResponse`) |
| `POST /api/v1/curriculum/activities/{activitySlug}/complete` | Idempotently marks an activity as completed for a learner | Public | `200 OK` JSON (`LearnerProgressResponse`) or `404` Problem Details |
| `GET /openapi/v1.json` | OpenAPI 3 document (`Development` environment only) | Dev only | `200 OK` JSON |

---

## 10. Authentication, Authorization & Asset Management

- **Authentication & Authorization**: Not yet implemented in Phase 1. Planned for Phase 2 using ASP.NET Core authentication and role/claim policies.
- **Asset Management**:
  - Static UI fonts and icons are bundled by Vite and served with `Cache-Control: public, max-age=31536000, immutable` from `/assets/`.
  - Optional S3-compatible object storage (`rustfs/rustfs:1.0.4`) is pre-configured on `backend_net` (`http://storage:9000`) under the `storage` Compose profile for Phase 3 3D assets and audio.

---

## 11. Docker & Deployment Architecture

| Service | Image / Target | Internal Port | Default Host Port | User | Networks |
|---|---|---|---|---|---|
| `db` | `postgres:18-alpine` | `5432` | None (`127.0.0.1:54329` in dev) | `postgres` (UID 70) | `backend_net` |
| `api` | `nawwar/api:latest` (`runtime`) | `8080` | None (`127.0.0.1:5080` in dev) | `app` (UID 1654) | `backend_net`, `frontend_net` |
| `frontend` | `nawwar/frontend:latest` (`runtime`) | `8080` (`5173` in dev) | `${NAWWAR_WEB_PORT:-8090}` (`5173` in dev) | `nginx` (UID 101) / `node` (UID 1000) | `frontend_net` |
| `storage` | `rustfs/rustfs:1.0.4` (`--profile storage`) | `9000`, `9001` | `127.0.0.1:9000`, `127.0.0.1:9001` | `rustfs` (UID 10001) | `backend_net` |

- **Network isolation**: `db` and `storage` reside exclusively on `backend_net`; `frontend` resides only on `frontend_net`; `api` bridges the two. In `docker-compose.prod.yml`, `backend_net` is marked `internal: true` and `api`/`frontend` run with `read_only: true`, `cap_drop: [ALL]`, and `no-new-privileges:true`.

