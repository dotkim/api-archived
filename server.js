'use strict';
require('dotenv').config();
const dateString = require('./components/dateString.js');
const http = require('http');
const app = require('./app');

console.log('############### WEB SERVER START UP ###############');
console.log(dateString(), '- starting http server');

const port = process.env.HTTPPORT || 80;

http.createServer(app).listen(port);
console.log(dateString(), '- listening on port', port);