### STAGE 1: Build ###
FROM node:12.7-alpine AS build
WORKDIR /usr/src/app
COPY touristApp/package.json ./
RUN npm install
COPY ./touristApp .

ARG cloud

RUN if [ -z "$cloud" ] ; then npm run build-local ; else npm run build-cloud ; fi

### STAGE 2: Run ###
FROM nginx:1.17.1-alpine
COPY ./nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/touristApp /usr/share/nginx/html