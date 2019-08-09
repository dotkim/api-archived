'use strict';
const dateString = require('../components/dateString.js');

module.exports = function (url) {
  try {
    let ext = url.split('/').pop().split('.').pop();
    if (ext.length > 5) return { statuscode: 409 };
    return { type: 'image/' + ext, statuscode: 200 };
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    return { statuscode: 500 };
  }
}