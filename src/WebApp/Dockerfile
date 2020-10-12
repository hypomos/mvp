FROM node:alpine as node
ARG mode

WORKDIR /src
COPY . .

RUN pwd && ls -al
RUN npm install && npm run build -- -mode $mode

FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=node /src/dist /usr/share/nginx/html