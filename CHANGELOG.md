# CHANGELOG.md — NAWWAR Project Changelog

All notable changes to the **NAWWAR (نوّار)** project are documented in this file.
Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

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

