'use strict';
// create the image thumbnail
require('dotenv').config();
const sharp = require('sharp');
const options = { width: 200, fit: 'inside' };

module.exports = async function (imgBuffer) {
  try {
    return await sharp(imgBuffer).resize(options).toBuffer();
  }
  catch (error) {
    console.error(error);
    return;
  }
}