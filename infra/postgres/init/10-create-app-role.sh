#!/bin/sh
# Creates the least-privileged application role used by the NAWWAR API and makes it
# the owner of the application database (so it can run EF Core migrations) without
# granting superuser rights. Runs ONLY on first initialisation of an empty data volume.
set -eu

: "${NAWWAR_DB_USER:?NAWWAR_DB_USER must be set}"
: "${NAWWAR_DB_PASSWORD:?NAWWAR_DB_PASSWORD must be set}"

psql -v ON_ERROR_STOP=1 \
  --username "$POSTGRES_USER" \
  --dbname "$POSTGRES_DB" \
  -v app_user="$NAWWAR_DB_USER" \
  -v app_password="$NAWWAR_DB_PASSWORD" \
  -v db_name="$POSTGRES_DB" <<'EOSQL'
CREATE ROLE :"app_user" WITH LOGIN PASSWORD :'app_password' NOSUPERUSER NOCREATEDB NOCREATEROLE;
ALTER DATABASE :"db_name" OWNER TO :"app_user";
REVOKE CONNECT ON DATABASE :"db_name" FROM PUBLIC;
GRANT CONNECT ON DATABASE :"db_name" TO :"app_user";
EOSQL

echo "nawwar-init: application role '$NAWWAR_DB_USER' created and owns database '$POSTGRES_DB'."

