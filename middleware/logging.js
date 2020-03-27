/*eslint-disable no-console*/
'use strict';
const dateString = require('../components/dateString.js');

const logging = (req, res, next) => {
  let start = Date.now();

  res.on('finish', () => {
    //eslint-disable-next-line no-underscore-dangle
    let code = res._header ? String(res.statusCode) : String(-1);
    let duration = Date.now() - start;
    let source = req.get('X-Forwarded-For');
    if (req.originalUrl === '/insert') {
      console.log(dateString(), '-', req.method, `${req.originalUrl}/${req.body.name}`, duration, code, source);
    } else if (req.originalUrl.includes('keyword') && req.method == 'POST') {
      console.log(dateString(), '-', req.method, `${req.originalUrl}/${JSON.stringify(req.body)}`, duration, code, source);
    } else {
      console.log(dateString(), '-', req.method, req.originalUrl, duration, code, source);
    }
  });

  next();
}

module.exports = logging;