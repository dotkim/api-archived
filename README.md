# API

## config template

Create a file here: `./Api/web.config`

The template should work "out of the box" and is set up for use with docker.

Here is a list of things that should be changed if it is not used with the docker-compose, and has local databases set up.

- MongoConnectionstring - Set this to `mongodb://localhost:27017`
- RedisConnectionstring - Set this to `localhost:6379`

Things that should be changed if you are setting this in a production environment:

- Email - Should be set to something else than the default.
- Name - Should be set to something else than the default.
- Password - Should be set to something else than the default.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <!-- <add key="DebugMode" value="true"/> -->
    <!-- Web server settings -->
    <add key="WebPort" value="8080"/>
    <add key="UseHTTPS" value="false"/>
    <add key="CertificatePath" value="./path/to/cert.pfx"/>
    <add key="CertificateSecret" value="CertPassPhrase"/>

    <!-- Database connections -->
    <add key="MongoConnectionstring" value="mongodb://database:27017"/>
    <add key="MongoDatabase" value="chatbot"/>
    <add key="RedisConnectionstring" value="auth:6379"/>

    <!-- Paths for storing files -->
    <add key="UploadsDir" value="./uploads"/>
    <add key="ThumbnailsDir" value="./uploads/thumbnails"/>
    <add key="ThumbnailSize" value="100"/>

    <!-- User for authentication -->
    <add key="Email" value="admin@email.com"/>
    <add key="Name" value="Admin user"/>
    <add key="Password" value="secret"/>
  </appSettings>
</configuration>
```
