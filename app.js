'use strict';
require('dotenv').config();
const express = require('express');
const dateString = require('./components/dateString.js');
const jsonParser = require('body-parser').json({ type: 'application/json' });
const authentication = require('./security/authentication');

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
      console.log(dateString(), '-', req.method, `${req.originalUrl}/${JSON.stringify(req.body)}`, duration, code, source);
    }
    else if (req.originalUrl.includes('keyword') && req.method == 'POST') {
      console.log(dateString(), '-', req.method, `${req.originalUrl}/${JSON.stringify(req.body)}`, duration, code, source);
    }
    else {
      console.log(dateString(), '-', req.method, req.originalUrl, duration, code, source);
    }
  });

  // Pass to next layer of middleware
  next();
});

app.use('/images', require('./routes/images'));
app.use('/insert', authentication(), require('./routes/insert'));
app.use('/keyword', authentication(), require('./routes/keywords'));

module.exports = app;