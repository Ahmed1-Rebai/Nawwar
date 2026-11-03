# HANDOFF_PHASE_02_DOMAIN_AND_AUTH — Phase 2 Educational Domain & Authentication

- **Project name**: NAWWAR (نوّار)
- **Current project phase**: Phase 1 Completed (`v0.1.0`) → Ready for Phase 2 (`v0.2.0` Planning & Implementation)
- **Date**: 2026-11-03

---

## 1. Task Objective

Design and implement Phase 2 of NAWWAR:
1. Core PostgreSQL domain entities and EF Core 10 migrations for localized educational modules, interactive 3D lesson metadata, and learner progress.
2. Authentication and authorization for learners and educators.
3. Versioned API endpoints (`/api/v1/modules`, `/api/v1/lessons`) consumed by the React 19 frontend in Arabic (`ar` default), French (`fr`), and English (`en`).

---

## 2. Relevant Architectural Context

- `NawwarDbContext` ([`backend/src/Nawwar.Api/Infrastructure/Persistence/NawwarDbContext.cs`](../../backend/src/Nawwar.Api/Infrastructure/Persistence/NawwarDbContext.cs)) is already wired to PostgreSQL 18 using the least-privilege `nawwar_app` role (`NOSUPERUSER NOCREATEDB NOCREATEROLE`), which owns the `nawwar` database and can execute EF Core schema migrations.
- Both frontend and backend default to Arabic (`ar`, `rtl`) and support `fr` and `en`.
- Browser requests reach the backend via the same-origin `/api` reverse proxy (`frontend/nginx/default.conf` in prod, `frontend/vite.config.ts` in dev).

---

## 3. Files Inspected & Modified in Phase 1

- **Inspected/Created**:
  - Infrastructure: `docker-compose.yml`, `docker-compose.dev.yml`, `docker-compose.prod.yml`, `.env.example`, `infra/postgres/init/10-create-app-role.sh`
  - Backend: `backend/Dockerfile`, `backend/Nawwar.slnx`, `backend/Directory.Build.props`, `backend/Directory.Packages.props`, `backend/src/Nawwar.Api/**`, `backend/tests/Nawwar.Api.Tests/**`
  - Frontend: `frontend/Dockerfile`, `frontend/nginx/default.conf`, `frontend/package.json`, `frontend/vite.config.ts`, `frontend/src/**`
  - Documentation: `AGENTS.md`, `GEMINI.md`, `ARCHITECTURE.md`, `PROGRESS.md`, `ROADMAP.md`, `DECISIONS.md`, `CHANGELOG.md`, `README.md`, `docs/**`

---

## 4. Implementation Completed

- Full Docker-first foundation (`db`, `api`, `frontend`, and optional `storage` profile) built, tested, and verified healthy via `docker compose`.
- Backend liveness, readiness, and culture-aware `/api/v1/system/info` endpoints with 7 passing xUnit v3 integration tests.
- Frontend Arabic-first RTL shell, language switcher (`ar`/`fr`/`en`), lazy-loaded React Three Fiber 3D scene (`WorldCanvas`), and 10 passing Vitest tests.

---

## 5. Current Technical State & Outstanding Issues

- **Working**: All Phase 1 containers build cleanly, pass health checks, and pass unit/integration test suites.
- **Outstanding Issues / Blockers**: None for Phase 1. Domain entities and authentication are intentionally deferred to Phase 2 per [`ROADMAP.md`](../../ROADMAP.md).

---

## 6. Constraints and Decisions That Must Not Be Changed

- Do not weaken `TreatWarningsAsErrors=true`, TypeScript `strict: true`, or `oxlint --deny-warnings`.
- Do not switch the API database connection back to the `postgres` superuser; keep using `NAWWAR_DB_USER` (`nawwar_app`).
- Keep Arabic (`ar`, `rtl`) as the default language and preserve `satisfies typeof ar` compile-time translation parity.
- Keep all CSS layout rules in logical properties only.
- Never commit `.env` or add `Co-authored-by` trailers to Git commits.

---

## 7. Required Tests & Expected Deliverables for Next Task

- EF Core migration and PostgreSQL integration tests verifying schema creation and localized curriculum queries.
- Frontend component tests for module/lesson views in `ar` (`rtl`), `fr` (`ltr`), and `en` (`ltr`).
- Updated `ARCHITECTURE.md`, `DECISIONS.md`, `PROGRESS.md`, and `CHANGELOG.md`.

---

## 8. Model Routing Recommendation

- **Step 1 — Phase 2 Database & Auth Architecture Design**:
  - **Recommended model role**: Premium reasoning model (e.g., Gemini 4 Argon or Claude Opus)
  - **Recommended thinking level**: High
  - **Recommended execution mode**: Planning mode
- **Step 2 — Phase 2 Feature & Endpoint Implementation**:
  - **Recommended model role**: Strong coding model (e.g., Claude Sonnet or Gemini Pro)
  - **Recommended thinking level**: Medium / High
  - **Recommended execution mode**: Planning mode

