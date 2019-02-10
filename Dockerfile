FROM microsoft/dotnet:2.2-sdk-alpine as builder  
 
RUN mkdir -p /root/src/app
WORKDIR /root/src/app
 
COPY ./src ./src
COPY ./build ./build
RUN dotnet restore ./src/Web/Web.csproj 
RUN dotnet publish ./src/Web/Web.csproj -c release -o /root/src/app/published 

FROM microsoft/dotnet:2.2-runtime-alpine

WORKDIR /root/  
COPY --from=builder /root/src/app/published .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
CMD ["dotnet", "./Web.dll"]