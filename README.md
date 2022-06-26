# Tax api built with netcore (net6.0)
## Installation
1 - Remove obj folder if exists
<br/>
```
rm -rf ./TaxApi/obj
```
2 - Install dependencies
<br/>
```
docker pull ptsdocker16/interview-test-server
```
3 - Create api image
```
docker build -t taxapi ./TaxApi
```
4 - Start docker
```
docker-compose up
```
5 - Open swagger url
<br/>
http://localhost:8000/index.html

<br/>

# QA
## Unit test

```
netcore test
```

# Documentation

### 1. Endpoint scalable cache service
1. Api Controller receives request
2. Factory create/get service
3. Service get from cache or search online, returns brackets
4. Calculator receives brackets and execute calculation
![Endpoing image](/Documentation/Endpoint.jpg)
<br/>

### 2. Async Logging / Tracing
1. LogManager receives enqueue
2. Enqueue record in memory
3. Async timer dequeue record
4. Timer saves record in database
![Logging image](/Documentation/Logging.jpg)

### 3. Http proxy trace
1. Http proxy receives request
2. Call http method
3. Handle exceptions
4. Enqueue trace record
![Logging image](/Documentation/HttpProxy.jpg)
