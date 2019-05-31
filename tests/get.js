const Mongo = require('../data/mongo.js');
const db = new Mongo();

async function get(page) {
  let imgs = await db.getImages(page);
  console.log(imgs.length);
}

get(2);