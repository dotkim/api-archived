'use strict';
const express = require('express');
const jsonParser = require('body-parser').json({
  type: 'application/json', extended: true, limit: '50mb'
});
const authentication = require('./security/authentication');
const headers = require('./middleware/headers');
const logging = require('./middleware/logging');

const app = new express();
app.disable('x-powered-by');
app.use(jsonParser);

// Global logging
app.use(headers);
app.use(logging);

app.use('/images', require('./routes/images'));
app.use('/insert', authentication(), require('./routes/insert'));
app.use('/keyword', authentication(), require('./routes/keywords'));

module.exports = app;