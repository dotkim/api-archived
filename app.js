'use strict';
const express = require('express');
const jsonParser = require('body-parser').json({
  type: 'application/json', extended: true, limit: '50mb'
});
const authentication = require('./security/authentication');
const app = new express();

// clean up powered by header
app.disable('x-powered-by');

// Use middleware and other
app.use(jsonParser);
app.use(require('./middleware/headers'));
app.use(require('./middleware/logging'));

// Routes without authtentication
app.use('/images', require('./routes/images'));
app.use('/triggers', require('./routes/triggers'));
app.use('/videos', require('./routes/videos'));

// Routes that require authentication
app.use('/insert', authentication(), require('./routes/insert'));
app.use('/keyword', authentication(), require('./routes/keywords'));

module.exports = app;