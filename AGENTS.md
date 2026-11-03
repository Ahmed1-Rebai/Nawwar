# AGENTS.md — NAWWAR Canonical AI Agent & Engineering Protocol

> **MANDATORY FIRST STEP**: Every AI coding agent and developer working on **NAWWAR (نوّار)** must read `AGENTS.md`, `ARCHITECTURE.md`, and `PROGRESS.md` before modifying any file in this repository.

---

## 1. Project Vision and Objectives

**NAWWAR (نَوَّر)** — *"Knowledge is Light" (العِلْمُ نُورٌ)* — is an Arabic-first, immersive 3D educational platform for **preschool children aged 3, 4, and 5 years** (*عُمْرُ 3 وَ4 وَ5 سَنَوَاتٍ*).

**What NAWWAR IS**:
- A playful, beautifully designed educational universe where young children (ages 3, 4, and 5) learn through age-appropriate courses, interactive activities, spoken Arabic audio, illustrations, animations, and supportive 3D exploration.
- **Central Product Objective**: **Build a complete, structured educational curriculum organized by age (3, 4, and 5), with interactive 3D experiences and multi-modal activities supporting each learning objective.**

**What NAWWAR IS NOT**:
- NOT a general scientific visualization platform or physics/astronomy simulator.
- NOT a collection of abstract 3D experiments or a raw `.glb` model inspector.
- NOT an authentication-first application (authentication comes last, in `PHASE H`).

Its core engineering objectives are:
1. Organize all educational content around the three preschool age groups (**عَالَمُ الاِكْتِشَافِ — 3 سَنَوَاتٍ**, **عَالَمُ الاِسْتِكْشَافِ — 4 سَنَوَاتٍ**, **عَالَمُ التَّعَلُّمِ — 5 سَنَوَاتٍ**) using the 7-level hierarchy (`AgeGroup` → `LearningDomain` → `CourseCollection` → `Course` → `Lesson` → `Activity` → `LearningObjective`), including the dedicated Age 5 Arabic reading pathway (`مَسَارُ تَعَلُّمِ القِرَاءَةِ`).
2. Separate educational content from presentation and 3D scene implementation so activities can combine 3D scenes, 2D illustrations, audio, animations, sorting/matching, and interactive quizzes.
3. Treat **Arabic (`ar`, RTL)** as the primary, first-class language and user experience — with full diacritics (tashkeel) and child-friendly audio — while supporting future **French (`fr`, LTR)** and **English (`en`, LTR)** localization cleanly.
4. Maintain a reproducible, Docker-first full-stack architecture backed by ASP.NET Core 10 and PostgreSQL 18, and preserve strict continuity across AI agent sessions.

---

## 2. Product Scope & Required Execution Sequence (`PHASE A` → `PHASE I`)

All development must follow this exact sequence (see [`ROADMAP.md`](ROADMAP.md) and [`docs/product/AGE_BASED_CURRICULUM.md`](docs/product/AGE_BASED_CURRICULUM.md)):
- **PHASE A — Product Realignment & Repository Audit** *(Completed)*
- **PHASE B — Complete Age-Based Curriculum Design (`docs/product/AGE_BASED_CURRICULUM.md`)** *(Completed)*
- **PHASE C — Curriculum Data Model & Content Architecture** *(Proposed — Awaiting Approval)*
- **PHASE D — Main Age-Selection Experience & Educational Navigation**
- **PHASE E — Implement Actual Courses & Age-Appropriate Activities**
- **PHASE F — Integrate Playful 3D Scenes into Educational Lessons**
- **PHASE G — Progress Tracking & Parent Experience**
- **PHASE H — Authentication & Learner Accounts** *(Deferred until Curriculum & Activities are complete)*
- **PHASE I — Final Localization, Optimization & Production Readiness**

- **Currently Implemented Infrastructure Baseline**:
  - Reproducible Docker Compose stack (`db`, `api`, `frontend`, and optional `storage` profile).
  - React 19 + TypeScript 7 + Vite 8 frontend shell with Arabic-first (`ar` / `rtl`) localization (`i18next`), CSS logical layout, lazy-loaded React Three Fiber 3D `.glb` scene (`WorldCanvas`, `WorldScene`), direct 3D mesh node picking, material disposal on unmount, WebGL `ErrorBoundary`, and live backend connectivity status.
  - ASP.NET Core 10 Minimal API (`Nawwar.Api`) with Arabic-first request localization (`ar`, `fr`, `en`), `/health/live`, `/health/ready`, `/api/v1/system/info`, and initial prototype curriculum endpoints (scheduled to be replaced by the `PHASE C` age-based curriculum schema upon approval).
  - PostgreSQL 18 database with automatic least-privilege application role provisioning (`infra/postgres/init/10-create-app-role.sh`) and persistent volume storage.

Never describe planned features as already implemented.

---

## 3. Technology Stack

| Layer | Technology | Version / Image |
|---|---|---|
| Frontend UI | React + TypeScript + Vite | React 19.3, TypeScript 7.0, Vite 8.3 |
| 3D Engine | Three.js + React Three Fiber + Drei | `three` 0.187, `@react-three/fiber` 9.8, `@react-three/drei` 10.7 |
| Localization (Frontend) | `i18next` + `react-i18next` | 26.7 / 17.1 (`ar` default, `fr`, `en`) |
| Typography | `@fontsource-variable/noto-sans-arabic` | Self-hosted WOFF2 variable font |
| Frontend Quality | `oxlint`, `tsc -b`, `vitest` + Testing Library | `oxlint` 1.89, `vitest` 5.1 |
| Production Web / Proxy | `nginxinc/nginx-unprivileged` | `1.29-alpine` (non-root UID `101`) |
| Backend API | ASP.NET Core Minimal API (`.NET 10`) | `mcr.microsoft.com/dotnet/sdk:10.0-alpine` & `aspnet:10.0-alpine` (non-root UID `1654`) |
| ORM & Database Driver | EF Core 10 + Npgsql | `Npgsql.EntityFrameworkCore.PostgreSQL` 10.0.3 |
| Backend Testing | xUnit v3 + `WebApplicationFactory` | `xunit.v3` 3.2.2 |
| Relational Database | PostgreSQL | `postgres:18-alpine` |
| Object Storage (optional) | RustFS (S3-compatible, MinIO drop-in) | `rustfs/rustfs:1.0.4` (`--profile storage`, non-root UID `10001`) |

---

## 4. Repository Structure

```text
.
├── AGENTS.md                        # Canonical instructions for AI agents and developers
├── GEMINI.md                        # Tool-specific entry pointer to AGENTS.md
├── ARCHITECTURE.md                  # Current technical architecture (truthful, synced with code)
├── PROGRESS.md                      # Session-by-session implementation & verification log
├── ROADMAP.md                       # Phased roadmap, milestones, and acceptance criteria
├── DECISIONS.md                     # Architecture Decision Records (ADR-0001..ADR-0007)
├── CHANGELOG.md                     # Versioned record of meaningful changes
├── README.md                        # Project overview and quickstart guide
├── .env.example                     # Documented environment variable placeholders ONLY
├── docker-compose.yml               # Canonical full-stack Compose definition
├── docker-compose.dev.yml           # Development hot-reload overrides (Vite HMR + dotnet watch)
├── docker-compose.prod.yml          # Production hardening overrides (read-only rootfs, cap_drop)
├── frontend/                        # React 19 + TypeScript + R3F SPA
│   ├── Dockerfile                   # Multi-stage: deps -> dev -> source -> test -> build -> runtime
│   ├── nginx/default.conf           # SPA routing, gzip, security headers, /api reverse proxy
│   └── src/
│       ├── components/              # Shared UI components (LanguageSwitcher, ErrorBoundary)
│       ├── features/                # Feature slices (system/ApiStatus, world/WorldCanvas & WorldScene)
│       ├── i18n/                    # Arabic-first i18next config, resources, and tests
│       └── lib/                     # Typed HTTP client (api.ts)
├── backend/                         # .NET 10 solution (Nawwar.slnx)
│   ├── Dockerfile                   # Multi-stage: sdk -> dev -> restore -> build -> test -> publish -> runtime
│   ├── Directory.Build.props        # Shared compiler settings (TreatWarningsAsErrors, Nullable, ICU)
│   ├── Directory.Packages.props     # Central Package Management (CPM)
│   ├── src/Nawwar.Api/              # ASP.NET Core 10 Minimal API
│   └── tests/Nawwar.Api.Tests/      # xUnit v3 integration test suite
├── infra/
│   └── postgres/init/               # First-boot DB role provisioning (10-create-app-role.sh)
└── docs/
    ├── architecture/                # Deep-dive localization and 3D architecture notes
    ├── infrastructure/              # Docker & environment setup reference (DOCKER.md, ENVIRONMENT.md)
    └── agent-handoffs/              # Multi-model handoff briefs (HANDOFF_<TASK_NAME>.md)
```

---

## 5. Coding Conventions & Architectural Rules

### General
- Keep changes minimal, cohesive, and well-typed. Do not introduce speculative abstractions or unused dependencies.
- Never commit secrets, `.env` files, credentials, tokens, or build artifacts (`node_modules/`, `dist/`, `bin/`, `obj/`).
- Every commit must be clean, focused, and authored by the repository owner **without `Co-authored-by` trailers**.

### Frontend (`frontend/`)
- **Strict TypeScript**: `strict: true`, `noUnusedLocals: true`, `noUnusedParameters: true`, `erasableSyntaxOnly: true`.
- **Linting**: `npm run lint` (`oxlint --deny-warnings`) must pass with zero warnings and zero errors.
- **Feature-based organization**: Place domain features under `src/features/<feature>/`, reusable UI primitives under `src/components/`, and shared utilities under `src/lib/`.
- **API calls**: Always route browser API calls through `src/lib/api.ts` using relative `/api/v1/...` paths (proxied by Vite in dev and nginx in prod) and pass the active language in `Accept-Language`.

### Backend (`backend/`)
- **Compiler strictness**: `TreatWarningsAsErrors=true`, `Nullable=enable`, and `AnalysisLevel=latest-recommended` are enforced in `Directory.Build.props`.
- **Central Package Management**: Define NuGet package versions only in `backend/Directory.Packages.props`.
- **Vertical slices**: Group endpoints and request/response contracts under `Features/<FeatureName>/` and cross-cutting concerns under `Infrastructure/`.
- **Globalization**: Never enable `InvariantGlobalization`. Full ICU data (`icu-libs` + `icu-data-full`) is required for Arabic and French culture formatting.
- **Error format**: Return RFC 9457 Problem Details (`application/problem+json`) for API errors.

---

## 6. 3D Development Guidelines (`@react-three/fiber`)

1. **Lazy loading**: Always code-split the 3D canvas entry (`React.lazy(() => import('./features/world/WorldCanvas'))`) so `three` does not block initial HTML/RTL paint.
2. **Graceful degradation**: Wrap `<WorldCanvas>` in `<ErrorBoundary>` with a localized fallback message so devices or headless environments without WebGL remain functional.
3. **Accessibility & reduced motion**:
   - Provide an accessible `role="img"` and localized `aria-label` on the canvas container.
   - Respect `prefers-reduced-motion: reduce` inside `useFrame` loops.
4. **Performance**: Cap `dpr` to `[1, 2]`, dispose custom geometries/textures when unmounting, and keep the 3D coordinate system direction-agnostic (RTL/LTR applies to the HTML overlay, not WebGL world coordinates).

---

## 7. Arabic-First Localization & RTL Implementation Rules

1. **Arabic is the default and reference language**:
   - `index.html` starts with `<html lang="ar" dir="rtl">`.
   - `DEFAULT_LANGUAGE` in `frontend/src/i18n/config.ts` and `LocalizationSetup.DefaultCulture` in `backend/src/Nawwar.Api/Infrastructure/Localization/LocalizationSetup.cs` are both `"ar"`.
   - `frontend/src/i18n/resources.ts` uses `satisfies typeof ar` so TypeScript fails compilation if `fr.json` or `en.json` is missing any key defined in `ar.json`.
2. **Synchronize document attributes**: Changing the language via `i18n.changeLanguage(...)` must immediately update both `document.documentElement.lang` and `document.documentElement.dir` (`rtl` for `ar`, `ltr` for `fr` and `en`).
3. **CSS Logical Properties ONLY**:
   - Use `margin-inline-start`, `margin-inline-end`, `padding-inline`, `padding-block`, `border-inline-start`, `inset-inline-end`, `inline-size`, `block-size`, and `text-align: start`.
   - Never use physical `left` / `right` properties for layout, spacing, or alignment.
4. **Arabic Typography & Diacritics (Tashkeel)**:
   - Preserve Arabic diacritics (e.g., `العِلْمُ نُورٌ`) intact in JSON resources and database UTF-8 columns.
   - Keep `letter-spacing: normal` and generous `line-height` (`1.8` on `:lang(ar)`) so vowel marks never clip or disconnect Arabic cursive ligatures.
   - Isolate embedded LTR tokens (version strings, codes, Latin brand labels) with `dir="ltr"` or `unicode-bidi: isolate`.

---

## 8. Docker-First Development & Validation Workflow

All builds, tests, and runtime checks can and must be executed through Docker so host toolchain differences never break reproducibility.

```bash
# 1. Prepare environment file once
cp .env.example .env

# 2. Start the canonical stack
docker compose up --build -d

# 3. Inspect service status & health checks
docker compose ps

# 4. Follow container logs
docker compose logs -f

# 5. Stop the stack (database volume persists)
docker compose down
```

- **Development mode (HMR + `dotnet watch`)**:
  ```bash
  docker compose -f docker-compose.yml -f docker-compose.dev.yml up --build -d
  ```
- **Production hardened mode**:
  ```bash
  docker compose -f docker-compose.yml -f docker-compose.prod.yml up --build -d
  ```
- **Optional S3 storage profile**:
  ```bash
  docker compose --profile storage up --build -d
  ```

---

## 9. Testing Requirements

Before marking any task complete, run and pass:
1. **Backend build & integration tests**:
   ```bash
   docker build --target test -t nawwar/api:test ./backend
   ```
2. **Frontend lint, typecheck, unit tests, and production bundle**:
   ```bash
   docker build --target test -t nawwar/frontend:test ./frontend
   ```
3. **End-to-end Docker Compose validation**:
   - `docker compose up --build -d`
   - Confirm `db`, `api`, and `frontend` reach `healthy` in `docker compose ps`.
   - Verify HTTP responses through the reverse proxy (`http://localhost:${NAWWAR_WEB_PORT:-8090}/healthz` and `/api/v1/system/info`).

---

## 10. Security and Privacy Rules

- Never store secrets in Dockerfiles, Compose files, source code, or Markdown documentation.
- `.env.example` contains only documented placeholders (`CHANGE_ME_*`).
- The API connects to PostgreSQL as the least-privilege role `NAWWAR_DB_USER` (`NOSUPERUSER NOCREATEDB NOCREATEROLE`), never as `postgres`.
- All application containers run as non-root users (`app` UID `1654` for API, `nginx` UID `101` for frontend runtime, `node` UID `1000` for frontend dev, `rustfs` UID `10001` for object storage).

---

## 11. Mandatory AI Agent Working Protocol & Multi-Model Orchestration

### Before Implementation
1. Read `AGENTS.md`, `ARCHITECTURE.md`, and `PROGRESS.md`.
2. Inspect the relevant source files and any active handoff document in `docs/agent-handoffs/`.
3. Identify dependencies, risks, and regression surfaces before editing.

### During Implementation
- Reuse existing modules and adhere to established architecture.
- Keep Dockerfiles and Compose files synchronized with code changes.
- Write or update automated tests for every behavioral change.

### After Implementation
1. Run backend and frontend tests and build checks.
2. Validate the complete stack with `docker compose`.
3. Update `PROGRESS.md` (truthful status, tests run, next steps).
4. Update `ARCHITECTURE.md` if any component, port, or data flow changed.
5. Record a new ADR in `DECISIONS.md` if a significant architectural decision was made.
6. Update `CHANGELOG.md` for meaningful deliverables.

### Multi-Model Routing & Handoffs
- **Premium reasoning models** (e.g., Gemini 4 Argon, Claude Opus — Planning mode, High thinking): system architecture, 3D engine architecture, database & security design, complex cross-module debugging, and independent technical audits.
- **Strong coding models** (e.g., Claude Sonnet, Gemini Pro — Planning/Fast mode, Medium/High thinking): main React/TypeScript, R3F, ASP.NET Core, EF Core, and test implementation.
- **Fast models** (e.g., Gemini Flash — Fast mode, Low/Medium thinking): isolated UI tweaks, simple bug fixes, translations, and routine documentation updates.
- When a task requires switching model tiers, stop cleanly, update `PROGRESS.md`, and write `docs/agent-handoffs/HANDOFF_<TASK_NAME>.md` detailing context, files modified, constraints, required tests, and recommended model/mode. Never claim a model switch happened automatically; wait for the human operator when manual selection is needed.

