/*eslint-disable no-console*/
'use strict';
const md5 = require('md5');

const Mongo = require('../storage/db');
const config = require('../models/configuration');
const thumbnail = require('../components/thumbnail');
const dateString = require('../components/dateString.js');
const fileHandler = require('../storage/fileHandler');

const { imgPath, thumbPath } = config;
const allowedExt = config.allowedImageExtensions.split(',');

const db = new Mongo();

class Images {
  createObject(data, page) {
    let count = Math.ceil(Number(data.imageCount) / data.limit);
    let obj = {
      //eslint-disable-next-line camelcase
      current_page: page,
      //eslint-disable-next-line camelcase
      number_of_pages: count,
      //eslint-disable-next-line no-undefined
      next: undefined,
      //eslint-disable-next-line no-undefined
      previous: undefined,
      images: data.images
    };
  
    if (data.images.length === data.limit) obj.next = page + 1;
    if (page > 1) obj.previous = page - 1;
  
    return obj;
  }

  async getImagePage(page, mode) {
    try {
      let pageNumber = Number(page);
      let modeNumber = Number(mode);
      if (typeof pageNumber !== 'number' || !pageNumber) pageNumber = Number(1);
      if ((typeof modeNumber !== 'number' || !modeNumber) && modeNumber != 0) modeNumber = Number(1);
  
      let qryPage = pageNumber - 1;
      let data = await db.getImages(qryPage, modeNumber);
      if (!data) return { statuscode: 404 };
      if (data === 'err') return { statuscode: 500 };
  
      let obj = this.createObject(data, pageNumber);
  
      return {
        content: obj,
        statuscode: 200
      };
    } catch (error) {
      console.error(dateString(), '- got error');
      console.error(error);
      return { statuscode: 500 };
    }
  }

  async getRandom() {
    try {
      let data = await db.randomImage();
      if (!data) return { statuscode: 404 };
      if (data === 'err') return { statuscode: 500 };

      data.url = config.imagePage + data.url;
      data.thumbnail = config.imagePage + data.thumbnail;

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

  /*
   * Note when using buffer:
   * Get the base64 image data from the body.
   * Also do a check to see if the data is a number.
   * This is incase of an exploit by allocating an empty buffer.
   * https://snyk.io/vuln/npm:ws:20160104
   */
  async Insert(body) {
    try {
      const { name, imageData } = body;
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
      let tpath = thumbPath.slice(-1) == '/' ? thumbPath : thumbPath + '/';
      const fullImage = fileHandler(path + name, imgBuffer);
      if (!fullImage) return { statuscode: 500 };
      const thumbIamge = fileHandler(tpath + name, thumbBuffer);
      if (!thumbIamge) return { statuscode: 500 };
  
      let obj = {
        fileName: name,
        contentType: 'image/' + extension,
        extension,
        url: 'i/' + name,
        thumbnail: 'thumbnails/' + name,
        tags: ['tagme'],
        checksum
      };
      let data = await db.addImage(obj);
      if (!data) return { statuscode: 404 };
      if (data === 'err') return { statuscode: 500 };
  
      return {
        inserted: data,
        statuscode: 200
      };
    } catch (error) {
      console.error(dateString(), '- got error');
      console.error(error);
      return { statuscode: 500 };
    }
  }
}

module.exports = Images;