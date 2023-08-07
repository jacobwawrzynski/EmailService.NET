# EmailService.NET
Simple API service for sending emails build with .NET 6

## Description
You can try this API locally using SwaggerUI with Visual Studio 2022 or with Docker

```bash
docker build -f EmailService.WebAPI/Dockerfile -t emailservice.net .
```
```bash
docker run -d -p 127.0.0.1:8080:80 --name webapi_test_local emailservice.net
```

If you want to just test the API, you can use https://ethereal.email.
To use it with Gmail SMTP, you need to configure it in Gmail settings.
