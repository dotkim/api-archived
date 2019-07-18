const dateString = require('../components/dateString.js');
const Mongo = require('../data/mongo.js');

const db = new Mongo();

function createObject(data, page) {
  let count = Math.ceil(Number(data.imageCount) / data.limit);
  let obj = {
    current_page: page,
    number_of_pages: count,
    next: undefined,
    previous: undefined,
    images: data.images
  };

  if (data.images.length === data.limit) obj.next = page + 1;
  if (page > 1) obj.previous = page - 1;

  return obj;
}

async function images(page, mode) {
  try {
    page = Number(page);
    mode = Number(mode);
    if ((typeof page !== 'number') || (!page)) page = Number(1);
    //if ((typeof mode !== 'number') || (!mode)) mode = Number(1);

    let qryPage = page - 1;
    let data = await db.getImages(qryPage, mode);
    if (!data) return { statuscode: 404 };

    let obj = createObject(data, page);

    return { content: obj, statuscode: 200 };
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    return { statuscode: 500 };
  }
}

module.exports = images;