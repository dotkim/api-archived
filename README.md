# API

## config template

Create a file here: `./Api/config/config.xml`

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
<AppConfig>
  <!-- Debug and logging -->
  <DebugMode></DebugMode>
  <SerilogSink></SerilogSink>
  
  <!-- Web server settings -->
  <WebPort></WebPort>
  <UseHTTPS></UseHTTPS>
  <CertificatePath>./path/to/cert.pfx</CertificatePath>
  <CertificateSecret>CertPassPhrase</CertificateSecret>

  <!-- Database connections -->
  <MongoConnectionstring></MongoConnectionstring>
  <MongoDatabase></MongoDatabase>
  <RedisConnectionstring></RedisConnectionstring>

  <!-- Paths for storing files -->
  <UploadsDir></UploadsDir>
  <ThumbnailsDir></ThumbnailsDir>
  <ThumbnailSize></ThumbnailSize>

  <!-- User for authentication -->
  <Email></Email>
  <Name></Name>
  <Password></Password>
</AppConfig>
```
