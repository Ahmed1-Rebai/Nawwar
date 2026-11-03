# DECISIONS.md — NAWWAR Architecture Decision Records (ADR)

This log records significant technical and architectural decisions made for NAWWAR.

---

## ADR-0001: Docker-First Development and Two-Tier Container Networking

- **Date**: 2026-11-03
- **Status**: Accepted
- **Context**: Section 10.1 requires a reproducible, isolated, documented development and production environment that runs consistently across machines without relying on host-installed runtimes (for example, `.NET SDK` is not installed on the primary host).
- **Decision**:
  - Standardize on `docker-compose.yml` as the canonical multi-service stack with `docker-compose.dev.yml` for hot-reload development (`dotnet watch` + Vite HMR) and `docker-compose.prod.yml` for hardened production (`read_only: true`, `cap_drop: [ALL]`, `no-new-privileges:true`).
  - Segment container traffic across `frontend_net` (`frontend` ↔ `api`) and `backend_net` (`api` ↔ `db`, `storage`) so `db` and `storage` are never reachable from the public web container.
  - Default the host-exposed web port to `NAWWAR_WEB_PORT=8090` because ports `80` and `8080` are commonly occupied on developer hosts.
- **Alternatives Considered**:
  - Host-native execution of `.NET` and `Node`: rejected because it violates reproducibility and fails on machines without the .NET 10 SDK.
  - Single flat Docker network: rejected because it unnecessarily exposes PostgreSQL and object storage to the public-facing proxy container.
  - Kubernetes / Helm: rejected per Section 10.1 ("Do not introduce Kubernetes or unnecessary infrastructure complexity at this stage").
- **Consequences**:
  - Every build, lint, test, and runtime verification runs identically inside containers.
  - Host port bindings are configurable via `.env`.

---

## ADR-0002: ASP.NET Core 10 Minimal API + EF Core 10 + PostgreSQL 18

- **Date**: 2026-11-03
- **Status**: Accepted
- **Context**: The backend requires high throughput, strong typing, first-class OpenAPI generation, health checks, and reliable Unicode/RTL localization backed by PostgreSQL.
- **Decision**:
  - Use `.NET 10` (`net10.0`) Minimal APIs organized by vertical feature slices (`Features/<Feature>/`) with Central Package Management (`Directory.Packages.props`) and `TreatWarningsAsErrors=true`.
  - Use `Npgsql.EntityFrameworkCore.PostgreSQL` (`10.0.3`) against `postgres:18-alpine` with persistent storage at `/var/lib/postgresql`.
  - Keep `InvariantGlobalization=false` and install `icu-libs` + `icu-data-full` in Alpine images so Arabic (`ar`) and French (`fr`) cultures work accurately.
- **Alternatives Considered**:
  - ASP.NET Core Controllers: more ceremony for small, focused vertical slices.
  - Invariant globalization for smaller images: rejected because it breaks Arabic/French culture formatting and `CultureInfo` rules.
- **Consequences**:
  - Fast startup, low memory footprint, compile-time warning enforcement, and full ICU culture support.

---

## ADR-0003: React 19 + TypeScript 7 + Vite 8 + React Three Fiber (`@react-three/fiber`)

- **Date**: 2026-11-03
- **Status**: Accepted
- **Context**: NAWWAR requires an interactive 3D browser experience integrated cleanly with a declarative, accessible, RTL-aware UI layer.
- **Decision**:
  - Build the frontend with React 19.3, TypeScript 7.0 (`strict: true`), Vite 8.3, `three` 0.187, `@react-three/fiber` 9.8, and `@react-three/drei` 10.7.
  - Code-split `WorldCanvas` via `React.lazy` so the 3D engine chunk loads asynchronously and never blocks initial Arabic RTL shell rendering.
  - Wrap the 3D canvas in an `ErrorBoundary` and respect `prefers-reduced-motion: reduce`.
- **Alternatives Considered**:
  - Imperative Three.js without React Three Fiber: harder to synchronize with React state, localization, and component lifecycles.
  - Eagerly bundling Three.js in the entry chunk: rejected because it inflates initial bundle size by ~250 kB gzipped.
- **Consequences**:
  - Clean separation between the 2D RTL/LTR UI shell and the 3D WebGL scene graph, with resilient fallback when WebGL is unavailable.

---

## ADR-0004: Arabic-First (`ar` / `rtl`) Architecture and Compile-Time Translation Parity

- **Date**: 2026-11-03
- **Status**: Accepted
- **Context**: NAWWAR is an Arabic-first educational product supporting Arabic (`ar`), French (`fr`), and English (`en`).
- **Decision**:
  - Set `ar` (`dir="rtl"`) as the default culture in `index.html`, `frontend/src/i18n/config.ts`, and `backend/src/Nawwar.Api/Infrastructure/Localization/LocalizationSetup.cs`.
  - Enforce key parity at compile time in `frontend/src/i18n/resources.ts` using `fr satisfies typeof ar` and `en satisfies typeof ar`, reinforced by unit tests in `src/i18n/i18n.test.ts`.
  - Author all CSS layout rules with logical properties (`margin-inline`, `padding-inline`, `border-inline-start`, `text-align: start`) and self-host `@fontsource-variable/noto-sans-arabic`.
- **Alternatives Considered**:
  - English default with RTL overrides: rejected because it treats Arabic as a bolt-on translation and leads to recurring RTL layout regressions.
- **Consequences**:
  - Missing translations fail TypeScript compilation immediately, and RTL/LTR switching requires zero layout overrides.

---

## ADR-0005: Same-Origin `/api` Reverse Proxy in Both Development and Production

- **Date**: 2026-11-03
- **Status**: Accepted
- **Context**: Browser-to-API communication across separate origins requires CORS configuration and complicates cookies, headers, and container port exposure.
- **Decision**:
  - Route all browser API calls to `/api/v1/...` on the same origin.
  - In production (`docker-compose.yml`), `nginxinc/nginx-unprivileged:1.29-alpine` serves the static SPA and reverse-proxies `/api/` to `http://api:8080` using Docker's embedded DNS resolver (`127.0.0.11`).
  - In development (`docker-compose.dev.yml`), Vite's dev server proxies `/api` to `http://api:8080`.
- **Alternatives Considered**:
  - Exposing the API on a separate public port with permissive CORS: rejected to minimize attack surface and environment divergence.
- **Consequences**:
  - Zero CORS configuration needed for the web client, and only one public port is required in production.

---

## ADR-0006: Least-Privilege PostgreSQL Application Role (`nawwar_app`)

- **Date**: 2026-11-03
- **Status**: Accepted
- **Context**: Connecting the web API to PostgreSQL as the bootstrap superuser (`postgres`) violates least-privilege security rules.
- **Decision**:
  - Provision a dedicated role (`NAWWAR_DB_USER`, default `nawwar_app`) with `NOSUPERUSER NOCREATEDB NOCREATEROLE` via `infra/postgres/init/10-create-app-role.sh` on first volume initialization, and make it the owner of `POSTGRES_DB` so it can run schema migrations without superuser privileges.
- **Alternatives Considered**:
  - Running the API as `postgres`: rejected due to excessive privilege risk.
- **Consequences**:
  - The API runs with minimal database privileges while retaining ownership of its application schema.

---

## ADR-0007: Optional S3-Compatible Object Storage via RustFS Compose Profile

- **Date**: 2026-11-03
- **Status**: Accepted
- **Context**: Section 10.1 specifies optional S3-compatible object storage (such as MinIO) for 3D assets, images, and audio. Upstream `minio/minio` community images are no longer publicly pullable from Docker Hub (`pull access denied`).
- **Decision**:
  - Provide an optional S3-compatible object storage service under the `storage` Docker Compose profile using `rustfs/rustfs:1.0.4` (a lightweight, non-root, S3-compatible MinIO drop-in replacement available on Docker Hub).
  - Keep it optional via `profiles: ["storage"]` so default `docker compose up --build -d` remains lean until Phase 3 asset streaming is active.
- **Alternatives Considered**:
  - Unauthenticated `minio/minio` image: unavailable on Docker Hub / Quay.
  - Always starting object storage in Phase 1: unnecessary resource consumption before asset upload/streaming endpoints exist.
- **Consequences**:
  - Standard AWS S3 / MinIO SDKs work unchanged against `http://storage:9000` when `--profile storage` is enabled.

---

## ADR-0008: Mandatory Binary glTF 2.0 (`.glb`) Standard for 3D Educational Assets

- **Date**: 2026-11-03
- **Status**: Accepted
- **Context**: Interactive 3D educational lessons require self-contained 3D models with named scene-graph nodes for interactive checkpoints, fast network transfer, and deterministic loading in React Three Fiber (`useGLTF`).
- **Decision**:
  - Standardize exclusively on binary glTF 2.0 (`.glb`, MIME type `model/gltf-binary`) for all 3D lesson assets.
  - Enforce the `.glb` extension across all three layers:
    1. PostgreSQL check constraint `ck_lessons_model_url_glb` (`model_url LIKE '%.glb'`) on the `lessons` table.
    2. Backend domain validation `Lesson.IsValidGlbUrl(url)`.
    3. Frontend validation `isValidGlbUrl(url)` in `api.ts` and `WorldScene.tsx`, plus explicit `model/gltf-binary` MIME configuration in `frontend/nginx/default.conf`.
- **Alternatives Considered**:
  - Multi-file `.gltf` + `.bin` + loose texture files: rejected because relative buffer/texture URI resolution is error-prone across proxies and increases HTTP round-trips.
  - Legacy `.obj` / `.fbx`: rejected due to larger payloads and lack of standardized PBR material definitions.
- **Consequences**:
  - Every lesson asset is a single, self-contained `.glb` binary with named nodes (`TargetNodeName`) that can be highlighted directly by curriculum checkpoints.

