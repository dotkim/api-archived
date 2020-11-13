/*eslint-disable no-console*/
'use strict';
const config = require('./models/configuration');
const dateString = require('./components/dateString.js');
const fs = require('fs');
const app = require('./app');

console.log('############### WEB SERVER START UP ###############');
console.log(dateString(), '- starting http server');

if (config.useHttps == 'false') {
  const http = require('http');
  const { httpPort } = config;

  http.createServer(app).listen(httpPort);
  console.log(dateString(), '- listening on port', httpPort);
} else {
  const https = require('https');
  const { httpsCert, httpsKey, httpsPort } = config;
  
  //No-sync disabled, these should be the only synchronous methods called.
  const options = {
    //eslint-disable-next-line no-sync
    key: fs.readFileSync(httpsKey),
    //eslint-disable-next-line no-sync
    cert: fs.readFileSync(httpsCert)
  };
  
  https.createServer(options, app).listen(httpsPort);
  console.log(dateString(), '- listening on port', httpsPort);
}