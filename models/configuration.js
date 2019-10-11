require('dotenv').config();

function Config() {
  this.config = {};

  this.config.mongooseUri = process.env.MONGOOSE_MONGOURI || 'mongodb://localhost:27017';
  this.config.mongooseDbname = process.env.MONGOOSE_DBNAME || 'Default';
  this.config.mongooseUsername = process.env.MONGOOSE_USERNAME || 'user';
  this.config.mongoosePassword = process.env.MONGOOSE_PASSWORD || 'pass';

  this.config.maxImageAmount = process.env.MAXIMAGEAMOUNT || '30';

  this.config.useHttps = process.env.USE_HTTPS || 'false';
  this.config.httpPort = process.env.HTTP_PORT || '8080';
  this.config.httpsPort = process.env.HTTPS_PORT || '8443';
  this.config.httpsCert = process.env.HTTPS_CERT;
  this.config.httpsKey = process.env.HTTPS_KEY; 

  this.config.imgPath = process.env.IMGPATH;
  this.config.thumbPath = process.env.THUMBPATH;
  this.config.imagePage = process.env.IMAGEPAGE;

  this.config.authUser = process.env.AUTH_USER;
  this.config.authPass = process.env.AUTH_PASS;
  
  this.config.accessControlOrigin = process.env.ACCESS_CTRL_ORIGIN || '*';
  this.config.accessControlMethods = process.env.ACCESS_CTRL_METHODS || 'GET, POST, DELETE, PUT';
  this.config.accessControlHeaders = process.env.ACCESS_CTRL_HEADERS || '*';
  this.config.accessControlCredentials = process.env.ACCESS_CTRL_CREDENTIALS || false;
}

Config.call(this);
module.exports = this.config;