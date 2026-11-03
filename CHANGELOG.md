# CHANGELOG.md — NAWWAR Project Changelog

All notable changes to the **NAWWAR (نوّار)** project are documented in this file.
Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.2.0] — 2026-11-03 (Phase 2 Step 1–3: Interactive Curriculum & `.glb` 3D Lesson Pipeline)

### Added
- **Educational Curriculum Domain & Persistence (`backend/`)**:
  - `LearningModule`, `Lesson`, and `LessonCheckpoint` domain entities with Arabic (with full diacritics), French, and English fields.
  - Database-level check constraint `ck_lessons_model_url_glb` (`model_url LIKE '%.glb'`) and domain validation `Lesson.IsValidGlbUrl` enforcing binary glTF 2.0 (`.glb`) 3D models.
  - `CurriculumCatalog` and `CurriculumInitializer` seeding 3 starter modules (*Optics and Physics of Light*, *Planetary Astronomy and Orbits*, *Atomic Structure and Molecular Geometry*), 3 `.glb` lessons, and 9 interactive 3D checkpoints into PostgreSQL on startup.
  - `GET /api/v1/curriculum/modules` and `GET /api/v1/curriculum/lessons/{slug}` endpoints in `CurriculumEndpoints.cs` with 5 new xUnit v3 integration tests in `CurriculumEndpointsTests.cs` (`12/12` total backend tests passing).
- **Binary `.glb` 3D Models & React Three Fiber Scene Loader (`frontend/`)**:
  - `frontend/scripts/generate-glb-models.mjs` generating spec-compliant binary glTF 2.0 (`.glb`) assets in `frontend/public/models/` (`lantern.glb`, `solar-system.glb`, `crystal-lattice.glb`) with named scene-graph nodes (`Light_Core`, `Aperture_Ring`, `Lantern_Base`, `Core_Sun`, `Planet_Earth`, `Moon_Satellite`, `Central_Atom`, `Bond_Axis`, `Vertex_Ligand`).
  - `WorldScene.tsx` and `WorldCanvas.tsx` updated to load `.glb` models via `useGLTF`, validate `.glb` URLs, highlight the selected checkpoint's 3D node, and render a 3D focus indicator ring.
  - `CurriculumExplorer.tsx` component for browsing localized modules, switching `.glb` 3D lessons, and inspecting 3D checkpoints across `ar` (`rtl`), `fr` (`ltr`), and `en` (`ltr`).
  - `nginx/default.conf` updated with dedicated `model/gltf-binary` MIME handling for `.glb` assets.
  - Documentation added in `docs/architecture/DATABASE_SCHEMA.md`, `docs/architecture/API_DESIGN.md`, and `ADR-0008` in `DECISIONS.md`.

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
  - `Nawwar.Api.Tests` xUnit v3 integration test suite (7 tests covering liveness, default Arabic RTL response, `Accept-Language` negotiation for `fr`/`en`/`ar`/fallback, and Problem Details 404 responses).
- **Frontend (`frontend/`)**:
  - React 19.3 + TypeScript 7.0 + Vite 8.3 application with Arabic-first (`lang="ar" dir="rtl"`) startup and self-hosted `@fontsource-variable/noto-sans-arabic` font.
  - `i18next` + `react-i18next` setup with compile-time translation key parity (`satisfies typeof ar`), `localStorage` persistence, and automatic `<html lang dir>` synchronization.
  - Lazy-loaded React Three Fiber (`@react-three/fiber` + `@react-three/drei`) 3D demonstration viewport (`WorldCanvas`, `WorldScene`) wrapped in `ErrorBoundary` with `prefers-reduced-motion` support.
  - `ApiStatus` component verifying live browser → reverse proxy → API connectivity and displaying resolved server culture.
  - Vitest + React Testing Library test suite (10 tests across `i18n.test.ts` and `App.test.tsx`).
- **Project Documentation**:
  - Root engineering and agent continuity documents: `AGENTS.md`, `GEMINI.md`, `ARCHITECTURE.md`, `PROGRESS.md`, `ROADMAP.md`, `DECISIONS.md`, `CHANGELOG.md`, `README.md`.
  - Focused technical references under `docs/infrastructure/`, `docs/architecture/`, and `docs/agent-handoffs/`.

