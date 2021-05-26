# Medium

Medium is an open platform where readers find dynamic thinking, and where expert and undiscovered voices can share their writing on any topic.

## Prerequisites 🔨

1. Install Windows 10 or Linux
2. Install dotnet version >= 5.x.x (suggested: 5.0.201)
3. Install MongoDb version >= 4.4 (suggested: 4.4)
4. Install RabbitMQ version >= 3.8.x (suggested: 3.8.14)

### Optional install prerequisites in docker

Follow these instructions if you are using docker.

1. Install Docker version 20.10.x (suggested: 20.10.5)
2. Install PostgreSql 10 in docker container.
3. Install MongoDb 4.4 in docker container.
4. Install RabbitMQ 3.8.x in docker container. (suggested: 3.8.14)

#### 1. Install Docker version 20.10.5
- Verify docker is already installed.
``` docker --version ```

#### 2. Install MongoDb 4.1 in docker container.
1. Dowload the image:
``` docker pull mongo:4.4 ```
2. Create and start mongo db docker container with:
``` docker run -d --name mongodb1 -p 27017:27017 mongo:4.4 ```
3. Enter docker bash:
``` docker exec -it mongodb1 bash ```
4. enter mongo shell.
``` mongo```
5. Create db
``` use medium_publications```
``` use medium_posts```
``` use medium_accounts```
``` use medium_search```
6. Exit:
``` exit ```
7. Credentials:
```
host: localhost/ip
port: 27017
dbname: medium_publications
user: root
pass: root
```

#### 3. Install MySQL 5 in docker container.
1. Dowload the image:
``` docker pull mysql:5 ```
2. Create and start mongo db docker container with:
``` docker run -d --name mysql -e MYSQL_ROOT_PASSWORD=secret123 -p 3306:3306 mysql:5 ```
3. Create data base: ``` medium_accounts ```
4. Credentials:
```
host: localhost/ip
port: 3306
dbname: medium_accounts
user: root
pass: secret123
```

#### 4. Install cassandra:3.11.4 in docker container.
1. Dowload the image: ``` docker pull cassandra:3.11.4 ```
2. Create and start mongo db docker container with: ``` docker run -d --name cassandradb  -p 7000:7000  cassandra:3.11.4 ```
3. Enter cassandra bash: ```docker exec -it some-cassandra bash```
3. Credentials:
```
host: localhost/ip
port: 7000
dbname: posts_accounts
user: 
pass:
```

#### 5. Install RabbitMQ 3.8.14 in docker container.
1. Dowload the image:
``` docker pull rabbitmq:3.8.14-management ```
2. Start container with:
``` docker run -d --name rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3.8.14-management ```
3. Credentials:
```
host: localhost/ip
port: 15672
management: localhost:15672
user: guest
pass: guest
```

## Configuration 🔧

## Deploy 🚀

### Order

Follow this order of deployment when running the application: 
1.	Run Gateway microservices ```Medium.Gateway```.
2.	Run Publications microservices ```Medium.Publications```.
3.	Run Posts microservices ```Medium.Posts```.
4.	Run Accounts microservices ```Medium.Accounts```.
5.	Run Search microservices ```Medium.Search```.

### Run App
1. Enter Medium.Gateway and execute  ```dotnet run```.
2. Enter Medium.Publications/Medium.Publications.APIRest and execute  ```dotnet run```.
3. Enter Medium.Posts/Medium.Posts.APIRest and execute  ```dotnet run```.
4. Enter Medium.Accounts/Medium.Accounts.APIRest and execute  ```dotnet run```.
5. Enter Medium.Search and/Medium.Search.APIRest execute  ```dotnet run```.


## Documentation 🔍

After running backend services you can see endpoints documentation on the following URLs:
```
Publications microservices  : https://localhost:5001/swagger/index.html
Posts microservices  : https://localhost:5002/swagger/index.html
Accounts microservices  : https://localhost:5003/swagger/index.html
Search microservices  : https://localhost:5004/swagger/index.html
```

### Publications microservices endpoints:  

- GET: ```https://localhost:5001/publication```
- GET: ```https://localhost:5001/publication/{id}```
- GET: ```https://localhost:5001/publication/author/{author}```
- DELETE: ```https://localhost:5001/publication/{id}```
- POST: ```https://localhost:5001/publication```
Body:
```
{
  "Title": "programming pillars",
  "Author": "srichard"
}
```
- PUT: ```https://localhost:5001/publication/{id}```
Body:
```
{
  "Title": "programming pillars",
  "Author": "cjuan"
}
```

## Contributing 💡

1. Clone it!
2. Create your feature branch: `git checkout -b FeatureSomeFeatureName`
3. Commit your changes: `git commit -m 'FeatureSomeFeatureName: Add some feature'`
4. Push to the branch: `git push origin FeatureSomeFeatureName`
5. Submit a mergue request.
