const dateString = require('../components/dateString.js');

module.exports = function (url) {
  let ext = url.split('/').pop();
  if (ext.length > 5) return { statuscode: 409 };
  return { type: 'image/' + ext, statuscode: 200 };
}