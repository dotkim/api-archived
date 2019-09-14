'use strict';
require('dotenv').config();
const dateString = require('./components/dateString.js');
const https = require('https');
const fs = require('fs');
const app = require('./app');

console.log('############### WEB SERVER START UP ###############');
console.log(dateString(), '- starting http server');

const httpsCert = process.env.HTTPS_CERT;        // gets the path for the certificate
const httpsKey = process.env.HTTPS_KEY;          // gets the path for the key
const httpsPort = process.env.HTTPS_PORT || 443;  // check if the port is set in .env, if not just use default.

const options = {
  key: fs.readFileSync(httpsKey),
  cert: fs.readFileSync(httpsCert)
};

https.createServer(options, app).listen(httpsPort);
console.log(dateString(), '- listening on port', httpsPort);