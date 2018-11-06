#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["SALON_HAIR_API/SALON_HAIR_API.csproj", "SALON_HAIR_API/"]
COPY ["SALON_HAIR_ENTITY/SALON_HAIR_ENTITY.csproj", "SALON_HAIR_ENTITY/"]
COPY ["SALON_HAIR_CORE/SALON_HAIR_CORE.csproj", "SALON_HAIR_CORE/"]
RUN dotnet restore "SALON_HAIR_API/SALON_HAIR_API.csproj"
COPY . .
WORKDIR "/src/SALON_HAIR_API"
RUN dotnet build "SALON_HAIR_API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SALON_HAIR_API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SALON_HAIR_API.dll"]