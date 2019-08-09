'use strict';
function getDateString() {
  const date = new Date();
  const dateString = new Date(date - (date.getTimezoneOffset() * 60000))
    .toISOString()
    .replace("T", ' ');
  return dateString;
}

module.exports = getDateString;