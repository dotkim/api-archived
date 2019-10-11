const config = require('../models/configuration');
const basicAuth = require('express-basic-auth');
const getUnauthorizedResponse = require('./getUnauthorizedResponse');

const authUser = config.authUser;
const authPass = config.authPass;

let basicAuthOptions = {
  users: {},
  unauthorizedResponse: getUnauthorizedResponse,
  challenge: true
};

// insert the authorization user into the users object.
basicAuthOptions.users[authUser] = authPass;

module.exports = function() {
  return basicAuth(basicAuthOptions);
}