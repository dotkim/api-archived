require('dotenv').config();
const dateString = require('../components/dateString.js');
const mongo = require('../data/mongo.js');

const db = new mongo();

async function images(page) {
  try {
    page = Number(page);
    let qryPage = page - 1;
    let data = await db.getImages(qryPage);
    if (!data) return { statuscode: 404 };

    let count = Math.ceil(Number(data.imageCount) / 30);
    let obj = {
      current_page: page,
      number_of_pages: count,
      next: undefined,
      previous: undefined,
      images: data.images
    };

    if (data.images.length === 30) obj.next = page + 1;
    if (page > 1) obj.previous = page - 1;

    return { content: obj, statuscode: 200 };
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    return { statuscode: 500 };
  }
}

module.exports = images;