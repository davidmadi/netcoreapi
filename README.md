# Tax api built with netcore (net6.0)
## Installation
Step 1
<br/>
Remove obj folder if exists
```
rm -rf ./TaxApi/obj
```
Step 2
<br/>
Install dependencies
```
docker pull ptsdocker16/interview-test-server
```
Step 3
```
docker build -t taxapi ./TaxApi
docker-compose up
```

Step 4
### Open browser
http://localhost:8000/index.html

<br/>

# QA
## Unit test

```
netcore test
```