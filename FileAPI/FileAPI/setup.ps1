dotnet publish -o .\publish; 
docker stop fileapi; 
docker container rm fileapi;
docker build -f .\App.dockerfile -t fileapi:0.1 --build-arg URL_PORT=7909 .
docker run --name fileapi -p 5000:7909 -it fileapi:0.1