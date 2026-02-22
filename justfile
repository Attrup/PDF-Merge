run: 
    dotnet run --project src/pdf-merge/pdf-merge.csproj

test:
    dotnet test

build:
    dotnet publish src/pdf-merge/pdf-merge.csproj -c Release -o bin/pdf-merge --self-contained true /p:UseAppHost=true /p:PublishSingleFile=true