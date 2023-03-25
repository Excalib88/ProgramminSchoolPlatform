FROM node:alpine
WORKDIR /app
COPY programming-school/package.json ./
COPY programming-school/package-lock.json ./
RUN npm i
COPY ./programming-school ./
EXPOSE 81
CMD ["npm", "run", "prod"]