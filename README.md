# Introduction
This project was conceived as part of a coding challenge and has the objective to demonstrate my abilities on .NET Core, Angular, Docker, Cloud services, among other less relevant subjects.

## Architecture description
The project is composed by a .NET Core API along with a very simple Angular application submodule meant just to demonstrate a possible usage of the API. 

## Live demo
Links to the application can be found below:
- Front-end: https://snowman-tourist.matheusbertazzo.com
- Back-end API: https://snowman-tourist.api.matheusbertazzo.com

Links will be available only for 7 days after the release just to allow challenge folks to play with it. After that, this subdomains will be deleted.

### Known issues
 - The back-end API, currently doesn't use any form of authentication to allow testers to freely explore the API
 - The front-end will only work through https and no automatic redirects are configured

# Cloning the project
- Clone this repo using ```git clone <repo url>```
- Don't forget to clone the front-end submodule using ```git submodule update --recursive --init```

# Local Configuration
## Environment variables
You'll need to set the following environment variables:
- SQL_SERVER_SA_PASSWORD: Local SQL Server password;
- TOURIST_API_DB_CONNECTION_STRING: "Server=localhost;Database=touristdb;User Id=<YOUR_USER_ID>;Password=<YOUR_PASSWORD>";
- TOURIST_API_DB_CONNECTION_STRING_DOCKER: "Server=sqlserver;Database=touristdb;User Id=<YOUR_USER_ID>;Password=<YOUR_PASSWORD>"
- TOURIST_API_DB_CONNECTION_STRING_CLOUD: "Server=localhost;Database=touristdb;User Id=<YOUR_CLOUD_USER_ID>;Password=<YOUR_CLOUD_PASSWORD>"

## Running locally through docker-compose:
Once you've setup the environment variables above, just run a simple ```docker-compose up``` and the following should happen:
- A local instance of the front-end application should start at your localhost:8080
- A local instance of SQL Server should start at your localhost:1443
- A local instance of the back-end API should start at your localhost:8081

### Initializing the database
Just ```cd ./api/TouristAPI.Api``` and run ```dotnet ef database update```

## Running applications locally without docker compose:
### Back-end application
Just ```cd ./api/TouristAPI.Api``` and run ```dotnet restore; dotnet run;```

### Front-end application
Just ```cd ./front-end/touristApp``` and run ```npm run start```. Please note that:
- Due to Facebook API connections the application will only be functional on https://localhost:4200;
- The application depends on the back-end API that should be up and running as described on last section;

# Deploying to GKE
This process should be as simple as running the following:
```
#Uploads images to private resgistry on google cloud
./upload-to-registry

#Restart the pods
kubectl get pods
kubectl delete pod <name of the pod to be restarted>
```

Please note that this will require access to the project's GKE cluster.