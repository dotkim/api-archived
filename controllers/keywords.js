'use strict';
const dateString = require('../components/dateString.js');
const Mongo = require('../data/mongo.js');

const db = new Mongo();

module.exports = class {
  async getKeyword(word) {
    try {
      let data = await db.getKeyword(word);

      if (!data) return { statuscode: 500 };
      if (data === 'err') return { statuscode: 500 };
      if (data.message.length <= 0) return { statuscode: 404 };

      return { content: data, statuscode: 200 };
    }
    catch (error) {
      console.error(dateString(), '- got error');
      console.error(error);
      return { statuscode: 500 };
    }
  }
  
  async addKeyword(word, message) {
    try {
      if (!message) return { statuscode: 409 }

      let data = await db.addKeyword(word, message);

      if (!data) return { statuscode: 404 };
      if (data === 'err') return { statuscode: 500 };
      
      return { content: data, statuscode: 200 };
    }
    catch (error) {
      console.error(dateString(), '- got error');
      console.error(error);
      return { statuscode: 500 };
    }
  }
}
