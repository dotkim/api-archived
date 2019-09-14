'use strict';
require('dotenv').config();
const dateString = require('../components/dateString');
const thumbnail = require('../components/thumbnail');
const fileHandler = require('../data/fileHandler');
const md5 = require('md5');

const Mongo = require('../data/mongo');
const db = new Mongo();

const imgPath = process.env.IMGPATH;

const allowedExt = [
  'png',
  'jpg',
  'jpeg',
  'gif'
];


module.exports = async function (body) {
  try {
    const name = body.name;

    // get the base64 image data from the body.
    // also do a check to see if the data is a number.
    // this is incase of an exploit by allocating an empty buffer.
    // https://snyk.io/vuln/npm:ws:20160104
    let imageData = body.imageData;
    if (typeof imageData == 'number') return { statuscode: 409 }

    const imgBuffer = Buffer.from(imageData, 'base64');

    if (!name) return { statuscode: 404 };
    if (!imgBuffer) return { statuscode: 404 };

    let extension = name.split('.').pop();
    if (!extension || extension.length < 2) return { statuscode: 400 };
    if (!allowedExt.includes(extension)) return { statuscode: 415 };

    const thumbBuffer = await thumbnail(imgBuffer);
    if (!thumbBuffer) return { statuscode: 500 };

    const checksum = md5(imgBuffer);
    if (!checksum) return { statuscode: 500 };

    let path = imgPath.slice(-1) == '/' ? imgPath : imgPath + '/';

    const fullImage = fileHandler(path + 'images/' + name, imgBuffer);
    if (!fullImage) return { statuscode: 500 };

    const thumbIamge = fileHandler(path + 'thumbnails/' + name, thumbBuffer);
    if (!thumbIamge) return { statuscode: 500 };

    let obj = {
      fileName: name,
      contentType: 'image/' + extension,
      extension: extension,
      url: '/' + name,
      thumbnail: 'thumbnails/' + name,
      tags: [ 'tagme' ],
      checksum: checksum
    };

    let data = await db.addImage(obj);
    if (!data) return { statuscode: 404 };
    if (data === 'err') return { statuscode: 500 };

    return { inserted: data, statuscode: 200 };
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    return { statuscode: 500 };
  }
}