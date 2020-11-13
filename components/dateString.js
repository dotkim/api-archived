'use strict';
const getDateString = () => {
  const date = new Date();
  //eslint-disable-next-line no-extra-parens
  const dateString = new Date(date - (date.getTimezoneOffset() * 60000)).
    toISOString().
    replace('T', ' ');
  return dateString;
}

module.exports = getDateString;