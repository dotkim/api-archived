const config = require('../models/configuration');

module.exports = function (req, res, next) {

  // Website you wish to allow to connect
  res.setHeader(
    'Access-Control-Allow-Origin',
    config.accessControlOrigin
  );

  // Request methods you wish to allow
  res.setHeader(
    'Access-Control-Allow-Methods',
    config.accessControlMethods
  );

  // Request headers you wish to allow
  res.setHeader(
    'Access-Control-Allow-Headers',
    config.accessControlHeaders
  );

  // Set to true if you need the website to include cookies
  res.setHeader(
    'Access-Control-Allow-Credentials',
    config.accessControlCredentials
  );

  next();
}