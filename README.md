## API/Microservice Template

The project is a template for building API/microservice application using [dotnet core](https://github.com/aspnet).
The main goal of the template is to have a bare minimum of dependencies and keep the code as clean as possible so it will be easy to use for wide range of scenarious.


## Build

|OS      |Build       |Coverage    |
|--------|:----------:|:----------:|
|Docker  |[![Build status](https://travis-ci.org/jenyayel/MicroServiceTemplate.svg?branch=master)](https://travis-ci.org/jenyayel/MicroServiceTemplate/branches)|[![Coverage Status](https://coveralls.io/repos/github/jenyayel/MicroServiceTemplate/badge.svg?branch=master)](https://coveralls.io/github/jenyayel/MicroServiceTemplate?branch=master)|
|Windows |[![Build status](https://ci.appveyor.com/api/projects/status/kxhlbhoxt2hauxxf?svg=true)](https://ci.appveyor.com/project/jenyayel/microservicetemplate)      ||

The project can be build from CLI:
```
dotnet build ./src/Web/Web.csproj
```

Or by using multi-stage Docker build (with small runtime image):
```
docker build -f ./Dockerfile
```

And tests can be run by:
```
dotnet test tests/Integration/Integration.csproj
```

## Dependencies

The only dependency is [.NET Core 2.0](https://www.microsoft.com/net/download/archives):
* If you need to build and developer get SDK here
* If you only need to run (outside of Docker), then get only runtime

## What it has

The main motto of the project is not to have everything that the API/microservice *might* need, but instead to have what every API/microservice will definitely require:

* Clean code (minimal amount of lines with good structure)
* A way to write integration tests (see project `Integration`) with coverage report
* Stylecop (AKA lint) for C# (see `stylecop*` files)
* Docker image build (see `Docker` file)
* CI (see `travis.yml` and `appveyor.yml`) 
* MVC core packages and not `Microsoft.AspNetCore.All` meta package, so it is more suitable for API development
* Logging configurations, filter for unhandled exceptions and usage of structured logs of Serilog
* Standard aspnet core configuration (environment variables, files per environment, etc)
* Swagger with UI (and the way to disable it)
* An example controller (see `status`)

## What it hasn't 

* A way to access databases 
* Authentication or authorization
* Caching
* Many other things you may need for *your* API



