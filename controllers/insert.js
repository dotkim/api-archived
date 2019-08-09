'use strict';
require('dotenv').config();
const dateString = require('../components/dateString');
const thumbnail = require('../components/thumbnail');

const Mongo = require('../data/mongo');
const db = new Mongo();

const allowedExt = [
  'png',
  'jpg',
  'jpeg',
  'gif'
];

const path = process.env.IMGPATH;

module.exports = async function (name) {
  try {
    if (!name) return { statuscode: 404 };

    let extension = name.split('.').pop();
    if (!extension) return { statuscode: 400 };
    if (!allowedExt.includes(extension)) return { statuscode: 409 };

    let obj = {
      fileName: name,
      contentType: 'image/' + extension,
      extension: extension,
      url: path + name,
      thumbnail: path + 'thumbnails/' + name,
      tags: [ 'tagme' ]
    };

    let data = await db.addImage(obj);
    if (!data) return { statuscode: 404 };
    return { inserted: data, statuscode: 200 };
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    return { statuscode: 500 };
  }
}