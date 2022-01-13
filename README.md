# Chatbot API

The backbone of the [chatbot](https://github.com/dotkim/chatbot), this API serves images, videos, audio and keywords to the bot. All responses are received in JSON, as the chatbot uses the json client, but the API support other formats such as XML as well.

There is also a static path for actually fetching the files.

## Installing

First clone the repo, as you'll have to build the project.

```
git clone https://github.com/dotkim/api.git
```

Currently i dont have any clever solution for running it "as is" as a service, i run this API with docker, so i will provide how to do that.

Create/edit the config file as you see fit, but it should also work with the template provided below.

Then run with docker-compose:

```
docker-compose up -d
```

The API should then be reachable on port 8080,

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
