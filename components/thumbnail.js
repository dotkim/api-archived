'use strict';
// create the image thumbnail
require('dotenv').config();
const sharp = require('sharp');
const fs = require('fs');

const path = process.env.FILEPATH;
const thumbpath = process.env.THUMBPATH;
let options = { width: 200, fit: 'inside' };

module.exports = async function (name) {
  try {
    const file = path + name;
    const thumbfile = thumbpath + name;
    let thumbnail = await sharp(file).resize(options).toBuffer();
    fs.writeFileSync(thumbfile, thumbnail);
  }
  catch (error) {
    console.error(error);
  }
}