# CHANGELOG.md — NAWWAR Project Changelog

All notable changes to the **NAWWAR (نوّار)** project are documented in this file.
Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.3.1] — 2026-11-04 (Expanded 13-Domain Preschool Courses, Interactive JS/React Learning Exercises & Child-Friendly Responsive UI)

### Added
- **Complete 13-Domain Preschool Course Catalog (`backend/src/Nawwar.Api/Features/Curriculum/CurriculumCatalog.cs` & `CurriculumInitializer.cs`)**:
  - Expanded preschool curriculum to cover all **13 learning domains** (`20` courses, `20` lessons, and `24` interactive activities) across Ages 3, 4, and 5:
    - **Age 3 (`5` domains, `6` courses, `8` activities)**: Added `الإِصْغَاءُ وَالكَلِمَاتُ اللَّطِيفَةُ` (*Arabic Listening & Kind Words*), `جِسْمِي وَمَشَاعِرِي` (*My Body & Basic Emotions*), and interactive star counting (`age3-count-stars-interactive`).
    - **Age 4 (`4` domains, `5` courses, `6` activities)**: Added `الأَلْوَانُ، الأَشْكَالُ، وَالأَنْمَاطُ المَنْطِقِيَّةُ` (*Color Mixing, Shapes & Patterns*) and enriched counting (`age4-count-five-apples`) with interactive tap-to-count items.
    - **Age 5 (`4` domains, `9` courses, `10` activities)**: Added `الطَّبِيعَةُ وَعَجَائِبُ السَّمَاءِ وَالمَاءِ` (*Wonders of Nature, Day, Night & Water*) and enriched the 6-course Arabic reading pathway (`مَسَارُ تَعَلُّمِ القِرَاءَةِ`) and early addition (`age5-math-add-3-plus-2`) with interactive studio and counting payloads.
  - Updated `CurriculumInitializer` to automatically re-synchronize catalog updates while preserving `activity_completions`.
- **Interactive JavaScript/React 3-Step Course Pedagogical Journey (`frontend/src/features/activities/` & `curriculum/`)**:
  - Updated `InteractiveChoiceActivity.tsx` to guide the child through 3 progressive steps instead of jumping straight into a quiz:
    1. **Step 1 (`١. نَتَعَلَّمُ الدَّرْسَ وَالصَّوْتَ 🔊`)**: Learn the letter/number/color/concept and listen to its sound via `LearningFlashcards.tsx`, `LetterVowelStudio.tsx`, `CountingPlayground.tsx`, and `SyllableBuilderPlayground.tsx`.
    2. **Step 2 (`٢. كَلِمَاتٌ مَأْلُوفَةٌ — حَيَوَانَاتٌ وَأَشْيَاءُ 🦁🍎`)**: New `CommonWordsShowcase.tsx` displaying the learned letter/concept in familiar everyday words (animals `🐾 حَيَوَانٌ`, fruits `🍎 فَاكِهَةٌ`, objects & nature `🏠 شَيْءٌ مَأْلُوفٌ`) with spoken words and sentences.
    3. **Step 3 (`٣. تَمْرِينٌ لَطِيفٌ 🎯`)**: Friendly illustrated exercise at the end of the lesson.
  - Enhanced `LessonStageView.tsx` and `CurriculumWorkspace.tsx` with visual lesson progress bars, star counters, and **"النَّشَاطُ التَّالِي ⬅" / "الدَّرْسُ التَّالِي ⬅"** progression buttons.
- **Warm, Child-Friendly, Fully Responsive Storybook Design (`frontend/src/styles/`)**:
  - Redesigned `tokens.css`, `layout.css`, `age-worlds.css`, and `activities.css` with a bright, warm storybook color palette (`#fffaf0` / `#eef9ff` / `#fef3c7`), tactile 3D push-buttons, and responsive mobile/tablet/desktop breakpoints using CSS logical properties exclusively.
- **Removed**:
  - Deleted obsolete pre-realignment `.glb` assets (`lantern.glb`, `solar-system.glb`, `crystal-lattice.glb`) from `frontend/public/models/`.

---

## [0.3.0] — 2026-11-03 (PHASES A–G: Preschool Ages 3, 4, and 5 Curriculum, Organized UI, Multi-Modal Activities & Playful 3D `.glb` Scenes)

### Added
- **Product & Curriculum Specifications (`PHASE A` & `PHASE B`)**:
  - [`docs/product/AGE_BASED_CURRICULUM.md`](docs/product/AGE_BASED_CURRICULUM.md): Complete preschool curriculum specification for Ages 3 (*عَالَمُ الاِكْتِشَافِ*), 4 (*عَالَمُ الاِسْتِكْشَافِ*), and 5 (*عَالَمُ التَّعَلُّمِ*), including the 7-level hierarchy (`AgeGroup -> LearningDomain -> CourseCollection -> Course -> Lesson -> Activity -> LearningObjective`), the 6-course Age 5 Arabic reading pathway (`مَسَارُ تَعَلُّمِ القِرَاءَةِ`), 6 multi-modal activity types, playful 3D interaction concepts, and Arabic-first vowelization/audio rules.
  - [`docs/product/USER_FLOWS.md`](docs/product/USER_FLOWS.md) and `ADR-0009` in [`DECISIONS.md`](DECISIONS.md).
- **7-Level Preschool Curriculum Backend & Persistence (`PHASE C` & `PHASE G`, `backend/`)**:
  - Domain entities `AgeGroup`, `LearningDomain`, `CourseCollection`, `Course`, `LearningObjective`, `Lesson`, `Activity`, and `ActivityCompletion` in `Features/Curriculum/Domain/`.
  - `CurriculumCatalog` and `CurriculumInitializer` seeding 3 preschool age worlds (**3, 4, and 5 years**), 9 learning domains, 9 course collections, 16 courses (including all 6 sequential courses of Age 5's **`مَسَارُ تَعَلُّمِ القِرَاءَةِ`**), 16 lessons, and 19 multi-modal activities into PostgreSQL 18.
  - REST endpoints: `GET /api/v1/curriculum/age-groups`, `GET /api/v1/curriculum/age-groups/{ageYears:int}`, `GET /api/v1/curriculum/lessons/{slug}`, `GET /api/v1/curriculum/progress`, and `POST /api/v1/curriculum/activities/{activitySlug}/complete` (`14/14` xUnit v3 integration tests passing).
- **Playful Preschool Binary `.glb` 3D Assets (`PHASE F`, `frontend/public/models/`)**:
  - `garden-colors.glb`, `friendly-farm.glb`, `counting-blocks.glb`, and `arabic-letters-stage.glb` generated via `frontend/scripts/generate-glb-models.mjs`.
- **Organized Frontend UI & Multi-Modal Activity Engine (`PHASE D`, `PHASE E`, `PHASE G`, `frontend/src/`)**:
  - Layout & audio components: `components/layout/AppHeader.tsx`, `components/layout/AppFooter.tsx`, `components/AudioPromptButton.tsx`.
  - Age-World Selector: `features/age-selector/AgeWorldSelector.tsx`.
  - Curriculum navigation: `features/curriculum/DomainGrid.tsx`, `CoursePathwayView.tsx`, `LessonStageView.tsx`, `CurriculumWorkspace.tsx`.
  - Multi-modal activities: `features/activities/ActivityRenderer.tsx`, `Scene3DExploreActivity.tsx`, `InteractiveChoiceActivity.tsx`, `types.ts`.
  - Parent progress panel: `features/progress/ParentProgressPanel.tsx`.
  - Modular CSS logical-property stylesheets: `styles/tokens.css`, `styles/layout.css`, `styles/age-worlds.css`, `styles/activities.css` (`12/12` Vitest tests passing).

---

## [0.2.0] — 2026-11-03 (Phase 2: Interactive Curriculum, `.glb` 3D Lesson Pipeline & Progress Persistence)

### Added
- **Educational Curriculum Domain & Persistence (`backend/`)**:
  - `LearningModule`, `Lesson`, `LessonCheckpoint`, and `CheckpointCompletion` domain entities with Arabic (with full diacritics), French, and English fields.
  - Database-level check constraint `ck_lessons_model_url_glb` (`model_url LIKE '%.glb'`) and domain validation `Lesson.IsValidGlbUrl` enforcing binary glTF 2.0 (`.glb`) 3D models.
  - `CurriculumCatalog` and `CurriculumInitializer` seeding 3 starter modules, 3 `.glb` lessons, and 9 interactive 3D checkpoints into PostgreSQL on startup.
- **Binary `.glb` 3D Models, Direct Mesh Picking & Progress UI (`frontend/`)**:
  - `WorldScene.tsx` and `WorldCanvas.tsx` updated to load `.glb` models via `useGLTF`, validate `.glb` URLs, highlight the selected checkpoint's 3D node, support direct 3D mesh picking (`onClick` → `onSelectNode`), dispose cloned materials on unmount, and render a 3D focus indicator ring.
  - `nginx/default.conf` updated with dedicated `model/gltf-binary` MIME handling for `.glb` assets.

---

## [0.1.0] — 2026-11-03 (Phase 1: Docker-First Infrastructure, Documentation & Foundational Shell)

### Added
- **Docker-First Infrastructure**:
  - `docker-compose.yml` orchestrating PostgreSQL 18 (`db`), ASP.NET Core 10 (`api`), React 19 + nginx reverse proxy (`frontend`), and optional S3-compatible object storage (`storage`, `rustfs/rustfs:1.0.4` under `--profile storage`).
  - `docker-compose.dev.yml` for bind-mounted hot reload (`dotnet watch` + Vite HMR) and localhost inspection ports.
  - `docker-compose.prod.yml` for production container hardening (`read_only: true`, `cap_drop: [ALL]`, `no-new-privileges:true`, internal backend network).
  - Multi-stage `backend/Dockerfile` (`sdk`, `dev`, `restore`, `build`, `test`, `publish`, `runtime`) running as non-root `app` (`UID 1654`) with full ICU globalization data.
  - Multi-stage `frontend/Dockerfile` (`deps`, `dev`, `source`, `test`, `build`, `runtime`) running as non-root `nginx` (`UID 101`) in production and `node` (`UID 1000`) in development.
  - `.env.example` with documented placeholder variables only and `.dockerignore` files at root, `frontend/`, and `backend/`.
  - `infra/postgres/init/10-create-app-role.sh` to provision the least-privilege `nawwar_app` database role on first volume initialization.
- **Backend (`backend/`)**:
  - `.NET 10` solution (`Nawwar.slnx`) with `Directory.Build.props` (`TreatWarningsAsErrors=true`, `Nullable=enable`) and Central Package Management (`Directory.Packages.props`).
  - `Nawwar.Api` with Arabic-first request localization (`ar` default, `fr`, `en`), `NawwarDbContext` (EF Core 10 + Npgsql), `/health/live`, `/health/ready`, `/api/v1/system/info`, and RFC 9457 Problem Details error handling.
- **Frontend (`frontend/`)**:
  - React 19.3 + TypeScript 7.0 + Vite 8.3 application with Arabic-first (`lang="ar" dir="rtl"`) startup and self-hosted `@fontsource-variable/noto-sans-arabic` font.
  - `i18next` + `react-i18next` setup with compile-time translation key parity (`satisfies typeof ar`), `localStorage` persistence, and automatic `<html lang dir>` synchronization.
- **Project Documentation**:
  - Root engineering and agent continuity documents: `AGENTS.md`, `GEMINI.md`, `ARCHITECTURE.md`, `PROGRESS.md`, `ROADMAP.md`, `DECISIONS.md`, `CHANGELOG.md`, `README.md`.
