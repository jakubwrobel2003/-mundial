#!/bin/sh
# Zastępuje ${BACKEND_INTERNAL_URL} w nginx.conf rzeczywistą wartością zmiennej środowiskowej,
# a następnie uruchamia nginx. Dzięki temu ten sam obraz działa w każdym środowisku.

set -e

: "${BACKEND_INTERNAL_URL:=http://localhost:5000}"

echo "[entrypoint] BACKEND_INTERNAL_URL=${BACKEND_INTERNAL_URL}"

# envsubst zastępuje tylko naszą zmienną (nie zaburza składni nginx $host itp.)
envsubst '${BACKEND_INTERNAL_URL}' < /etc/nginx/templates/nginx.conf.template \
    > /etc/nginx/conf.d/default.conf

exec nginx -g "daemon off;"
