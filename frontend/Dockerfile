# syntax=docker/dockerfile:1
#
# NAWWAR Frontend — multi-stage build.
#   dev      : Node 24 running Vite dev server with HMR (compose dev, non-root `node` UID 1000)
#   test     : runs oxlint, tsc, and vitest (`docker build --target test`)
#   runtime  : nginx-unprivileged serving the static bundle + reverse-proxying /api to `api:8080`
#
# Stage order matters for the legacy (non-BuildKit) builder: `dev` comes before
# `test` and `build` so dev builds only install dependencies.

ARG NODE_VERSION=24
ARG NGINX_VERSION=1.29

# ---------------------------------------------------------------------------
FROM node:${NODE_VERSION}-alpine AS deps
WORKDIR /app
COPY package.json package-lock.json ./
RUN npm ci --no-audit --no-fund

# ---------------------------------------------------------------------------
# Development: source is bind-mounted at /app by docker-compose.dev.yml while
# /app/node_modules lives in a named volume seeded from this stage.
FROM deps AS dev
RUN chown -R node:node /app
USER node
ENV NODE_ENV=development
EXPOSE 5173
CMD ["npm", "run", "dev", "--", "--host", "0.0.0.0", "--port", "5173"]

# ---------------------------------------------------------------------------
FROM deps AS source
COPY . .

# ---------------------------------------------------------------------------
FROM source AS test
RUN npm run lint && npm run typecheck && npm run test

# ---------------------------------------------------------------------------
FROM test AS build
ENV NODE_ENV=production
RUN npm run build

# ---------------------------------------------------------------------------
# Unprivileged nginx runs as UID 101 (`nginx`) and listens on port 8080.
FROM nginxinc/nginx-unprivileged:${NGINX_VERSION}-alpine AS runtime
COPY nginx/default.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist /usr/share/nginx/html
USER 101
EXPOSE 8080
HEALTHCHECK --interval=15s --timeout=3s --start-period=5s --retries=3 \
  CMD wget -qO- http://127.0.0.1:8080/healthz >/dev/null || exit 1

