# PROGRESS.md — NAWWAR Implementation & Verification Log

> This file is the single source of truth for implementation status across development sessions. Never mark a feature as completed unless it has been implemented and verified.

---

## 1. Current Project Phase

- **Active Sequence**: **Preschool Product Realignment (`PHASE A` → `PHASE I`)**
- **PHASE A (Product Realignment & Repository Audit)**: **Completed (2026-11-03)**
- **PHASE B (Complete Age-Based Curriculum Design — Ages 3, 4, 5)**: **Completed (2026-11-03)**
- **PHASE C (Curriculum Data Model & Content Architecture)**: **Proposed & Awaiting Human Review Before Code Changes**
- **Postponed / Cancelled Directions**:
  - Learner authentication, session tokens, and account switching are postponed until **PHASE H**.
  - Secondary-school scientific prototype topics (optics, astronomy, crystal lattices) and complex `.glb` scene-node quizzes are superseded by the preschool (ages 3, 4, 5) curriculum (`ADR-0009`).

---

## 2. Phase A Audit Summary: What Exists vs. Product Divergence

### 2.1 Reusable Engineering Assets (Preserved)
- [x] **Docker-First Infrastructure**: `docker-compose.yml`, `docker-compose.dev.yml`, `docker-compose.prod.yml`, non-root multi-stage `backend/Dockerfile` and `frontend/Dockerfile`, PostgreSQL 18 least-privilege `nawwar_app` provisioning (`infra/postgres/init/10-create-app-role.sh`).
- [x] **Arabic-First Localization Foundation**: `<html lang="ar" dir="rtl">`, self-hosted `@fontsource-variable/noto-sans-arabic` WOFF2 font, CSS logical properties (`frontend/src/index.css`), `i18next` with compile-time key parity (`satisfies typeof ar`), and ASP.NET Core `RequestLocalizationMiddleware`.
- [x] **Reusable 3D Rendering Engine**: Lazy-loaded `WorldCanvas.tsx` and `WorldScene.tsx` (`@react-three/fiber` + `@react-three/drei` `useGLTF`), mesh click handling (`onSelectNode`), material disposal on unmount, `ErrorBoundary`, and `prefers-reduced-motion` support.

### 2.2 Identified Product Divergences (To Be Replaced in `PHASE C` – `PHASE F`)
1. **Target Audience & Content Divergence**:
   - `CurriculumCatalog.cs` currently seeds 3 secondary-school science modules (Optics, Planetary Astronomy, Molecular Geometry) instead of the preschool curriculum for **Ages 3, 4, and 5** (*عَالَمُ الاِكْتِشَافِ*، *عَالَمُ الاِسْتِكْشَافِ*، *عَالَمُ التَّعَلُّمِ*).
2. **Data Model Coupling Divergence**:
   - `LearningModule -> Lesson -> LessonCheckpoint` lacks the 7-level preschool hierarchy (`AgeGroup -> LearningDomain -> CourseCollection -> Course -> Lesson -> Activity -> LearningObjective`) and tightly couples every `Lesson` row to a mandatory `.glb` file (`ck_lessons_model_url_glb`), preventing multi-modal activities (audio listening, sorting/matching, counting, letter forms, short vowels *الفتحة/الكسرة/الضمة*, and syllable/word/sentence reading).
3. **Entry UX Divergence**:
   - `App.tsx` currently opens a split view with a 3D model card and a module list sidebar instead of prioritizing the **Age-World Selection Portal** (*3 سنوات / 4 سنوات / 5 سنوات*).

---

## 3. Completed Deliverables in Current Session (`PHASE A` & `PHASE B`)

- [x] **Complete Age-Based Preschool Curriculum Specification**:
  - Created [`docs/product/AGE_BASED_CURRICULUM.md`](docs/product/AGE_BASED_CURRICULUM.md) covering all 10 required sections (educational philosophy, Ages 3/4/5 worlds, learning objectives, domains, course collections including Age 5's 6-course `مَسَارُ تَعَلُّمِ القِرَاءَةِ`, lesson progression, 6 interactive activity types, playful 3D concepts, Arabic-first tashkeel/audio rules, and future `fr`/`en` localization strategy).
- [x] **Revised User Journey & Product Documentation**:
  - Created [`docs/product/USER_FLOWS.md`](docs/product/USER_FLOWS.md) detailing the Age-World Selection portal and navigation flow.
  - Updated [`docs/product/PRODUCT_SPECIFICATION.md`](docs/product/PRODUCT_SPECIFICATION.md), [`docs/product/EDUCATIONAL_FRAMEWORK.md`](docs/product/EDUCATIONAL_FRAMEWORK.md), [`AGENTS.md`](AGENTS.md), [`ARCHITECTURE.md`](ARCHITECTURE.md), [`ROADMAP.md`](ROADMAP.md), and [`DECISIONS.md`](DECISIONS.md) (`ADR-0009`).
- [x] **Existing Working Stack Preserved**:
  - All existing containers (`nawwar-db-1`, `nawwar-api-1`, `nawwar-frontend-1`) and automated tests (`14/14` backend xUnit v3 tests, `12/12` frontend Vitest tests) remain intact and passing pending approval of `PHASE C`.

---

## 4. Next Steps (Awaiting Operator Review & Approval)

1. **Human Review Gate**:
   - Review [`docs/product/AGE_BASED_CURRICULUM.md`](docs/product/AGE_BASED_CURRICULUM.md), [`docs/product/USER_FLOWS.md`](docs/product/USER_FLOWS.md), and the proposed `PHASE C` data architecture in [`ARCHITECTURE.md`](ARCHITECTURE.md).
2. **Upon Approval — Execute `PHASE C` (Curriculum Data Model & Content Architecture)**:
   - Implement the 7-level preschool curriculum entities (`AgeGroup`, `LearningDomain`, `CourseCollection`, `Course`, `Lesson`, `Activity`, `LearningObjective`, `EducationalAsset`, `AudioAsset`, `ContentTranslation`, `ProgressDefinition`) in `Nawwar.Api` and PostgreSQL 18.
3. **Execute `PHASE D` (Main Age-Selection Experience & Educational Navigation)**:
   - Build the Age-World Selection landing experience (*عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ*، *عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ*، *عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ*) and domain/collection/course navigation in React.
