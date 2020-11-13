/*eslint-disable no-console*/
'use strict';
const config = require('../models/configuration');
const dateString = require('../components/dateString.js');
const Mongo = require('../storage/db');

const db = new Mongo();

class Videos {
  createObject(data, page) {
    let count = Math.ceil(Number(data.videoCount) / data.limit);
    let obj = {
      //eslint-disable-next-line camelcase
      current_page: page,
      //eslint-disable-next-line camelcase
      number_of_pages: count,
      //eslint-disable-next-line no-undefined
      next: undefined,
      //eslint-disable-next-line no-undefined
      previous: undefined,
      videos: data.videos
    };
  
    if (data.videos.length === data.limit) obj.next = page + 1;
    if (page > 1) obj.previous = page - 1;
  
    return obj;
  }

  async getVideos(page, mode) {
    try {
      let videoPage = Number(page);
      let videoMode = Number(mode);
      if (typeof videoPage !== 'number' || !videoPage) videoPage = Number(1);
      //eslint-disable-next-line no-extra-parens
      if (typeof videoMode !== 'number' || (!videoMode && videoMode != 0)) videoMode = Number(1);
  
      let qryPage = videoPage - 1;
      let data = await db.getVideos(qryPage, videoMode);
      if (!data) return { statuscode: 404 };
      if (data === 'err') return { statuscode: 500 };
  
      let obj = this.createObject(data, videoPage);
  
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
      let data = await db.randomVideo();
      if (!data) return { statuscode: 404 };
      if (data === 'err') return { statuscode: 500 };

      data.url = config.videoPage + data.url;
      data.thumbnail = config.videoPage + data.thumbnail;

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
}

module.exports = Videos;