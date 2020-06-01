docker build -t tourist-api .
docker tag tourist-api gcr.io/snowman-tourist/tourist-api
docker push gcr.io/snowman-tourist/tourist-api