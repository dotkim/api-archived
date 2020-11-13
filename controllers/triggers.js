/*eslint-disable no-console*/
'use strict';
const dateString = require('../components/dateString.js');
const Mongo = require('../storage/db');

const db = new Mongo();

const triggers = async (obj) => {
  try {
    if (typeof obj !== 'object') return { statuscode: 400 };

    let data = await db.addTriggerEvent(obj);
    if (!data) return { statuscode: 404 };
    if (data === 'err') return { statuscode: 500 };

    return {
      content: data,
      statuscode: 200
    };
  } catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    return { statuscode: 500 };
  }
}

module.exports = triggers;