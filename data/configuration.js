require('dotenv').config();

function Config() {
  this.config = {};

  this.config.mongooseUri = process.env.MONGOOSE_MONGOURI;
  this.config.mongooseDbname = process.env.MONGOOSE_DBNAME;
  this.config.mongooseUsername = process.env.MONGOOSE_USERNAME;
  this.config.mongoosePassword = process.env.MONGOOSE_PASSWORD;
  
  this.config.maxImageAmount = process.env.MAXIMAGEAMOUNT;
  
  this.config.useHttps = process.env.USE_HTTPS;
  this.config.httpPort = process.env.HTTP_PORT;
  this.config.httpsPort = process.env.HTTPS_PORT;
  this.config.httpsCert = process.env.HTTPS_CERT;
  this.config.httpsKey = process.env.HTTPS_KEY;

  this.config.imgPath = process.env.IMGPATH;
  this.config.thumbPath = process.env.THUMBPATH;
  this.config.imagePage = process.env.IMAGEPAGE;

  this.config.authUser = process.env.AUTH_USER;
  this.config.authPass = process.env.AUTH_PASS;
}

Config.call(this);
module.exports = this.config;