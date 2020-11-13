'use strict';
const express = require('express');
const jsonParser = require('body-parser').json({
  type: 'application/json',
  extended: true,
  limit: '50mb'
});
const authentication = require('./security/authentication');
const app = new express();

//clean up powered by header
app.disable('x-powered-by');

/*
 * Use middleware and authentication.
 * Auth is only used for POST requests.
 */
app.use(jsonParser);
app.use(require('./middleware/headers'));
app.use(require('./middleware/logging'));
app.post('*', authentication(), (req, res, next) => next());

//Routes
app.use('/images', require('./routes/images'));
app.use('/triggers', require('./routes/triggers'));
app.use('/videos', require('./routes/videos'));
app.use('/keywords', require('./routes/keywords'));

module.exports = app;