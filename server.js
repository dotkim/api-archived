'use strict';
require('dotenv').config();
const http = require('http');
const express = require('express');
const basicAuth = require('express-basic-auth');
const dateString = require('./components/dateString.js');
const jsonParser = require('body-parser').json({ type: 'application/json' });
const getUnauthorizedResponse = require('./components/getUnauthorizedResponse.js');

console.log('############### WEB SERVER START UP ###############');
console.log(dateString(), '- starting http server');

const port = process.env.HTTPPORT || 80;
const authUser = process.env.AUTH_USER;
const authPass = process.env.AUTH_PASS;

let basicAuthOptions = {
  users: {},                                      // we create the user later, so we can keep the user in .env
  unauthorizedResponse: getUnauthorizedResponse,  // generic 401 error when username/pw is wrong
  challenge: true                                 // challenge the user to authenticate
};

basicAuthOptions.users[authUser] = authPass;      // insert the authorization user into the users object.

const app = new express();
app.disable('x-powered-by');
app.use(jsonParser);

// Global logging
app.use(function(req, res, next) {
  let start = Date.now();

  res.setHeader('Access-Control-Allow-Origin', '*');          // Website you wish to allow to connect
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST'); // Request methods you wish to allow
  res.setHeader('Access-Control-Allow-Headers', '*');         // Request headers you wish to allow
  res.setHeader('Access-Control-Allow-Credentials', false);   // Set to true if you need the website to include cookies
  
  // listener for logging the requests that come in
  res.on('finish', function() {
    let code = res._header ? String(res.statusCode) : String(-1);
    let duration = Date.now() - start;
    let source = req.get('X-Forwarded-For');
    if (req.originalUrl === '/insert') {
      console.log(dateString(), '-', req.method, `${req.originalUrl}/${req.body.name}`, duration, code, source);
    }
    else {
      console.log(dateString(), '-', req.method, req.originalUrl, duration, code, source);
    }
  });

  // Pass to next layer of middleware
  next();
});

app.use('/images', require('./routes/images.js'));
app.use('/insert', basicAuth(basicAuthOptions), require('./routes/insert.js'));

http.createServer(app).listen(port);
console.log(dateString(), '- listening on port', port);