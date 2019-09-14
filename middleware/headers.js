module.exports = function(req, res, next) {

  res.setHeader('Access-Control-Allow-Origin', '*');          // Website you wish to allow to connect
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST'); // Request methods you wish to allow
  res.setHeader('Access-Control-Allow-Headers', '*');         // Request headers you wish to allow
  res.setHeader('Access-Control-Allow-Credentials', false);   // Set to true if you need the website to include cookies

  next();
}