docker build -f Dockerfile -t demo-hplusspotsapi:v1 ..

docker run -i -p 8080:80  demo-hplusspotsweb:v1

REM http://localhost:8080/api/product