'use strict';
const express = require('express');

const jsonParser = require('body-parser').json({
  type: 'application/json', extended: true, limit: '50mb'
});

const authentication = require('./security/authentication');

const app = new express();
app.disable('x-powered-by');
app.use(jsonParser);

// Global logging
app.use(require('./middleware/headers'));
app.use(require('./middleware/logging'));

app.use('/images', require('./routes/images'));
app.use('/insert', authentication(), require('./routes/insert'));
app.use('/keyword', authentication(), require('./routes/keywords'));
app.use('/triggers', require('./routes/triggers'));
app.use('/videos', require('./routes/videos'));

module.exports = app;