FROM node:20-alpine AS build
WORKDIR /app

COPY package*.json ./
RUN npm ci --prefer-offline

COPY . .
# Budujemy z produkcyjną konfiguracją; API_URL nie jest potrzebne w build-time
# bo nginx proxy przekierowuje /api/ do backendu w runtime
RUN npm run build

FROM nginx:1.27-alpine AS runtime

RUN apk add --no-cache gettext

# Skopiuj zbudowaną aplikację Angular
COPY --from=build /app/dist/mundial-frontend/browser /usr/share/nginx/html

# Szablon nginx – entrypoint.sh podmieni BACKEND_INTERNAL_URL
COPY nginx.conf /etc/nginx/templates/nginx.conf.template

# Skrypt startowy
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

EXPOSE 80
ENTRYPOINT ["/entrypoint.sh"]
