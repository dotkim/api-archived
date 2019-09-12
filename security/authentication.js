require('dotenv').config();
const basicAuth = require('express-basic-auth');
const getUnauthorizedResponse = require('./getUnauthorizedResponse');

const authUser = process.env.AUTH_USER;
const authPass = process.env.AUTH_PASS;

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