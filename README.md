# نوّار | NAWWAR

**NAWWAR (نوّار)** — *"Knowledge is Light" (العِلْمُ نُورٌ)* — is an Arabic-first, interactive 3D educational web platform built with React 19, Three.js (`@react-three/fiber`), ASP.NET Core 10, and PostgreSQL 18.

---

## Features (`v0.2.0`)

- **Arabic-First & Multi-Language (`ar` / `fr` / `en`)**:
  - Boots in Arabic (`lang="ar" dir="rtl"`) by default with full diacritics (tashkeel) and self-hosted `Noto Sans Arabic Variable` typography.
  - Instant switching between **العربية (`rtl`)**, **Français (`ltr`)**, and **English (`ltr`)** using CSS logical properties and compile-time translation parity.
- **Interactive `.glb` 3D Educational Scenes**:
  - Code-split Three.js + React Three Fiber (`@react-three/fiber` / `@react-three/drei`) 3D viewport loading binary glTF 2.0 (`.glb`) scientific models (`/models/lantern.glb`, `/models/solar-system.glb`, `/models/crystal-lattice.glb`).
  - Direct 3D mesh picking and checkpoint node highlighting (`TargetNodeName`), orbit controls, reduced-motion support, GPU material disposal on unmount, and an `ErrorBoundary` fallback when WebGL is unavailable.
- **ASP.NET Core 10 API & PostgreSQL 18 Curriculum + Progress Persistence**:
  - Culture-aware Minimal API (`/api/v1/system/info`, `/api/v1/curriculum/modules`, `/api/v1/curriculum/lessons/{slug}`, `/api/v1/curriculum/progress`, `POST /api/v1/curriculum/lessons/{slug}/checkpoints/{stepOrder}/complete`), liveness (`/health/live`) and database readiness (`/health/ready`) probes, RFC 9457 Problem Details, and least-privilege PostgreSQL role provisioning (`nawwar_app`).
- **Docker-First Reproducibility**:
  - Multi-stage Dockerfiles for both frontend and backend, non-root runtime containers, persistent volumes, health checks, and environment-specific Compose overrides (`dev` and `prod`).

---

## Technology Stack

- **Frontend**: React 19.3, TypeScript 7.0, Vite 8.3, Three.js 0.187, `@react-three/fiber` 9.8, `@react-three/drei` 10.7, `i18next` 26.7, `oxlint`, `vitest` 5.1
- **Reverse Proxy / Web Server**: `nginxinc/nginx-unprivileged:1.29-alpine`
- **Backend**: ASP.NET Core 10 (`net10.0`), Entity Framework Core 10, `Npgsql` 10.0.3, `xunit.v3` 3.2.2
- **Database**: PostgreSQL 18 (`postgres:18-alpine`)
- **Object Storage (optional profile)**: `rustfs/rustfs:1.0.4` (S3-compatible)

---

## Prerequisites

- **Docker Engine** `>= 24.0` and **Docker Compose** `v2` (`docker compose version`).
- *(No host installation of `.NET` or `Node.js` is required; all builds and tests run inside Docker containers.)*

---

## Quick Start (Docker Workflow)

1. **Create your local `.env` from the template** and set strong passwords for your machine:
   ```bash
   cp .env.example .env
   ```
   *(Edit `.env` to replace the `CHANGE_ME_*` placeholders with your local passwords.)*

2. **Build and start the complete stack**:
   ```bash
   docker compose up --build -d
   ```

3. **Check service health and status**:
   ```bash
   docker compose ps
   ```
   Open **`http://localhost:8090`** (or the `NAWWAR_WEB_PORT` configured in `.env`).

4. **Follow container logs**:
   ```bash
   docker compose logs -f
   ```

5. **Stop the stack** (PostgreSQL data persists in the `postgres_data` volume):
   ```bash
   docker compose down
   ```

---

## Development & Production Modes

- **Development Mode (Vite HMR + `dotnet watch` hot reload)**:
  ```bash
  docker compose -f docker-compose.yml -f docker-compose.dev.yml up --build -d
  ```
  - Frontend HMR: `http://localhost:5173`
  - Direct API + OpenAPI (`/openapi/v1.json`): `http://localhost:5080`
  - Localhost PostgreSQL port: `127.0.0.1:54329`

- **Production Hardened Mode (`read_only` rootfs, `cap_drop: ALL`, internal backend network)**:
  ```bash
  docker compose -f docker-compose.yml -f docker-compose.prod.yml up --build -d
  ```

- **Enable Optional S3 Object Storage (`--profile storage`)**:
  ```bash
  docker compose --profile storage up --build -d
  ```

---

## Running Tests & Quality Gates

All quality gates execute inside isolated Docker build stages:

```bash
# Backend: restore, build (TreatWarningsAsErrors), and run xUnit v3 tests
docker build --target test -t nawwar/api:test ./backend

# Frontend: oxlint (--deny-warnings), tsc -b, and Vitest unit/component tests
docker build --target test -t nawwar/frontend:test ./frontend
```

---

## Technical Documentation

| Document | Purpose |
|---|---|
| [`AGENTS.md`](AGENTS.md) | Mandatory instructions, conventions, and working protocol for AI agents and developers |
| [`ARCHITECTURE.md`](ARCHITECTURE.md) | Current system, frontend, 3D, localization, API, database, and container architecture |
| [`PROGRESS.md`](PROGRESS.md) | Truthful implementation progress, verification results, and next steps |
| [`ROADMAP.md`](ROADMAP.md) | Phased development roadmap and acceptance criteria |
| [`DECISIONS.md`](DECISIONS.md) | Architecture Decision Records (`ADR-0001` – `ADR-0008`) |
| [`CHANGELOG.md`](CHANGELOG.md) | Versioned project changelog |
| [`docs/product/PRODUCT_SPECIFICATION.md`](docs/product/PRODUCT_SPECIFICATION.md) | Product vision, personas, and implemented capabilities |
| [`docs/product/EDUCATIONAL_FRAMEWORK.md`](docs/product/EDUCATIONAL_FRAMEWORK.md) | Pedagogical principles and `.glb` 3D curriculum matrix |
| [`docs/design/DESIGN_SYSTEM.md`](docs/design/DESIGN_SYSTEM.md) | Color tokens, Arabic typography, and RTL layout rules |
| [`docs/architecture/DATABASE_SCHEMA.md`](docs/architecture/DATABASE_SCHEMA.md) | PostgreSQL 18 ER diagram, tables, and `.glb` check constraints |
| [`docs/architecture/API_DESIGN.md`](docs/architecture/API_DESIGN.md) | Versioned `/api/v1` REST contracts and sample payloads |
| [`docs/infrastructure/DOCKER.md`](docs/infrastructure/DOCKER.md) | Detailed Docker & Compose operations, volumes, networking, and troubleshooting |
| [`docs/infrastructure/ENVIRONMENT.md`](docs/infrastructure/ENVIRONMENT.md) | Reference for all `.env` variables and secret hygiene |
| [`docs/architecture/LOCALIZATION.md`](docs/architecture/LOCALIZATION.md) | Arabic-first RTL, typography, and client/server localization rules |
| [`docs/architecture/THREE_D_ARCHITECTURE.md`](docs/architecture/THREE_D_ARCHITECTURE.md) | React Three Fiber pipeline, lazy loading, accessibility, and performance guidelines |
| [`docs/agent-handoffs/README.md`](docs/agent-handoffs/README.md) | Multi-model handoff template and protocol |

