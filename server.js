'use strict';
const config = require('./models/configuration');
const dateString = require('./components/dateString.js');
const fs = require('fs');
const app = require('./app');

console.log('############### WEB SERVER START UP ###############');
console.log(dateString(), '- starting http server');

if (config.useHttps == "false") {
  const http = require('http');
  const httpPort = config.httpPort;

  http.createServer(app).listen(httpPort);
  console.log(dateString(), '- listening on port', httpPort);
}
else {
  const https = require('https');
  const httpsCert = config.httpsCert;
  const httpsKey = config.httpsKey;
  const httpsPort = config.httpsPort;
  
  const options = {
    key: fs.readFileSync(httpsKey),
    cert: fs.readFileSync(httpsCert)
  };
  
  https.createServer(options, app).listen(httpsPort);
  console.log(dateString(), '- listening on port', httpsPort);
}
