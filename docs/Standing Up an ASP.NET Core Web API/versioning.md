---
title: Versioning your Web API
description: Versioning your Web API
author: cwoodruff
---
# Versioning your Web API

## START FROM PREVIOUS MODULE'S END
[Identity in your Web API](identity.md)

## ADD VERSIONING NUGET PACKAGE TO API PROJECT

```dos
dotnet add package Microsoft.AspNetCore.Mvc.Versioning
```
![](versioning/Snag_1122b35c.png)

## ADD API VERSIONING TO STARTUP IN API PROJECT

```csharp
builder.Services.AddVersioning();
```

## ADD VERSIONING TO ServicesConfiguration IN API PROJECT

### ServicesConfiguration.cs

```csharp
public static void AddVersioning(this IServiceCollection services)
{
    services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        //options.DefaultApiVersion = new ApiVersion( new DateTime( 2020, 9, 22 ) );
        //options.DefaultApiVersion =
        //  new ApiVersion(new DateTime( 2020, 9, 22 ), "LetoII", 1, "Beta");
        options.ReportApiVersions = true;
        //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
    });
}
```

## ADD NEW CUSTOMER CONTROLLER FOR VERSION 2
![](versioning/2022-05-12_07-32-18.png)
```csharp
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[EnableCors("CorsPolicy")]
[ApiVersion("2.0")]
public class CustomerController : ControllerBase
```
![](versioning/2022-05-12_07-37-47.png)

## MODIFY CUSTOMER V1 CONTROLLER FOR VERSIONING IN API PROJECT

```csharp
[Route( "api/v{version:apiVersion}/[controller]" )]
[ApiController]
[EnableCors("CorsPolicy")]
[ApiVersion( "1.0", Deprecated = true)]
public class CustomerController : ControllerBase
```
![](versioning/2022-05-12_07-36-22.png)

## TEST IN POSTMAN

### Version 1 Endpoint
```dos
https://localhost:44320/api/v1/Customer
```

![](versioning/Snag_1122b36c.png)

### Version 2 Endpoint
```dos
https://localhost:44320/api/v2/Customer
```







