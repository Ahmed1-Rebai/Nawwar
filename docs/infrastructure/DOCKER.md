# Docker Setup & Operations Guide (`docs/infrastructure/DOCKER.md`)

This document describes the containerized development, testing, and production workflows for **NAWWAR**.

---

## 1. Files Overview

| File | Purpose |
|---|---|
| [`docker-compose.yml`](../../docker-compose.yml) | Canonical multi-service stack (`db`, `api`, `frontend`, and optional `storage` profile) |
| [`docker-compose.dev.yml`](../../docker-compose.dev.yml) | Development overrides: Vite HMR (`:5173`), `dotnet watch` (`:5080`), localhost PostgreSQL (`:54329`) |
| [`docker-compose.prod.yml`](../../docker-compose.prod.yml) | Production hardening overrides: `read_only: true`, `cap_drop: [ALL]`, `no-new-privileges:true`, internal `backend_net` |
| [`backend/Dockerfile`](../../backend/Dockerfile) | Multi-stage `.NET 10` image (`sdk` → `dev` → `restore` → `build` → `test` → `publish` → `runtime`) |
| [`frontend/Dockerfile`](../../frontend/Dockerfile) | Multi-stage Node 24 / nginx-unprivileged image (`deps` → `dev` → `source` → `test` → `build` → `runtime`) |
| [`.env.example`](../../.env.example) | Documented placeholder environment variables |

---

## 2. Standard Workflow

```bash
# 1. Create .env from template and set local passwords
cp .env.example .env

# 2. Build and start all core services in detached mode
docker compose up --build -d

# 3. Verify container health
docker compose ps

# 4. Stream logs
docker compose logs -f

# 5. Stop containers (volumes remain intact)
docker compose down
```

---

## 3. Service Architecture, Users & Health Checks

| Service | Base Image | Runtime User | Internal Port | Health Check |
|---|---|---|---|---|
| `db` | `postgres:18-alpine` | `postgres` (`UID 70`) | `5432` | `pg_isready -U "$NAWWAR_DB_USER" -d "$POSTGRES_DB" -h 127.0.0.1` |
| `api` | `mcr.microsoft.com/dotnet/aspnet:10.0-alpine` | `app` (`UID 1654`) | `8080` | `wget -qO- http://127.0.0.1:8080/health/ready` |
| `frontend` | `nginxinc/nginx-unprivileged:1.29-alpine` | `nginx` (`UID 101`) | `8080` | `wget -qO- http://127.0.0.1:8080/healthz` |
| `storage` (profile `storage`) | `rustfs/rustfs:1.0.4` | `rustfs` (`UID 10001`) | `9000`, `9001` | `curl -sf http://127.0.0.1:9000/health` |

---

## 4. Persistent Volumes & Database Initialization

- **`postgres_data`** (`/var/lib/postgresql`):
  - Persists PostgreSQL 18 cluster files across `docker compose down`, `docker compose restart`, and image rebuilds.
  - On first initialization of an empty volume, `/docker-entrypoint-initdb.d/10-create-app-role.sh` creates the non-superuser role `NAWWAR_DB_USER` (`nawwar_app`) and grants it ownership of `POSTGRES_DB` (`nawwar`).
  - To completely reset the database volume during local development ONLY:
    ```bash
    docker compose down -v
    ```
- **`storage_data`** (`/data`):
  - Persists S3 buckets and objects when the `storage` profile is active.
- **Development cache volumes** (`docker-compose.dev.yml`):
  - `frontend_node_modules`, `api_nuget_cache`, `api_bin`, `api_obj`, `api_tests_bin`, `api_tests_obj` prevent host/container permission conflicts and speed up incremental rebuilds.

