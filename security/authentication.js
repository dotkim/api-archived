'use strict';
const config = require('../models/configuration');
const basicAuth = require('express-basic-auth');
const getUnauthorizedResponse = require('./getUnauthorizedResponse');

const { authUser, authPass } = config;

let basicAuthOptions = {
  users: {},
  unauthorizedResponse: getUnauthorizedResponse,
  challenge: true
};

//insert the authorization user into the users object.
basicAuthOptions.users[authUser] = authPass;

module.exports = () => basicAuth(basicAuthOptions);