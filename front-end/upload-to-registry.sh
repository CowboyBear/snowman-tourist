#Build image + Upload to registry

docker build --build-arg cloud=true -t tourist-front-end .
docker tag tourist-front-end gcr.io/snowman-tourist/tourist-front-end
docker push gcr.io/snowman-tourist/tourist-front-end