/*eslint-disable no-console*/
'use strict';
const sharp = require('sharp');
const options = {
  width: 200,
  fit: 'inside'
};

module.exports = async (imgBuffer) => {
  try {
    return await sharp(imgBuffer).
      resize(options).
      toBuffer();
  } catch (error) {
    console.error(error);
  }
}