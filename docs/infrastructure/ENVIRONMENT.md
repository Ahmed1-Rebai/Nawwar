# Environment Variables Reference (`docs/infrastructure/ENVIRONMENT.md`)

All runtime configuration is supplied via environment variables loaded from `.env` (which is gitignored). Copy [`.env.example`](../../.env.example) to `.env` before starting Docker Compose.

---

## Security Rules

1. **Never commit `.env`** or any file containing real passwords, API keys, or certificates.
2. **Keep `.env.example` placeholder-only** (`CHANGE_ME_*`).
3. Rotate local or staging credentials immediately if ever exposed in terminal logs or screenshots.

---

## Variable Reference

| Variable | Default / Placeholder in `.env.example` | Used By | Description |
|---|---|---|---|
| `ASPNETCORE_ENVIRONMENT` | `Production` | `api` | ASP.NET Core runtime environment (`Production` in base/prod, `Development` in `docker-compose.dev.yml`) |
| `TZ` | `UTC` | `db`, `api` | Container timezone |
| `NAWWAR_WEB_PORT` | `8090` | `frontend` | Host port mapped to the nginx reverse proxy (`:8080` in container) |
| `NAWWAR_FRONTEND_DEV_PORT` | `5173` | `frontend` (dev) | Host port mapped to the Vite dev server in `docker-compose.dev.yml` |
| `NAWWAR_API_DEV_PORT` | `5080` | `api` (dev) | Localhost port mapped to `Nawwar.Api` in `docker-compose.dev.yml` |
| `NAWWAR_DB_DEV_PORT` | `54329` | `db` (dev) | Localhost port mapped to PostgreSQL in `docker-compose.dev.yml` |
| `POSTGRES_DB` | `nawwar` | `db`, `api` | Application database name |
| `POSTGRES_USER` | `postgres` | `db` | Bootstrap superuser used only during first-time volume initialization |
| `POSTGRES_PASSWORD` | `CHANGE_ME_postgres_superuser_password` | `db` | Bootstrap superuser password (required) |
| `NAWWAR_DB_USER` | `nawwar_app` | `db`, `api` | Least-privilege application database role |
| `NAWWAR_DB_PASSWORD` | `CHANGE_ME_nawwar_app_password` | `db`, `api` | Password for `NAWWAR_DB_USER` (required) |
| `S3_ACCESS_KEY` | `CHANGE_ME_s3_access_key` | `storage` | Root access key for optional S3-compatible object storage (`--profile storage`) |
| `S3_SECRET_KEY` | `CHANGE_ME_s3_secret_key` | `storage` | Root secret key for optional S3-compatible object storage (`--profile storage`) |
| `S3_REGION` | `us-east-1` | `storage`, `api` | S3 region identifier |
| `S3_BUCKET_ASSETS` | `nawwar-assets` | `api` | Default bucket name for 3D glTF/GLB, image, and audio assets |
| `NAWWAR_S3_API_PORT` | `9000` | `storage` | Localhost port for S3 API (`127.0.0.1:9000`) |
| `NAWWAR_S3_CONSOLE_PORT` | `9001` | `storage` | Localhost port for S3 web console (`127.0.0.1:9001`) |

