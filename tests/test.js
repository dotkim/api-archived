const Mongo = require('../data/mongo.js');
const fs = require('fs');
const db = new Mongo();

(async function insertImg() {
  let images = await fs.readdirSync('', { withFileTypes: true });
  images.forEach((img) => {
    if (img.isFile()) {
      let ext = img.name.split('.').pop();
      let obj = {
        fileName: img.name,
        contentType: 'image/' + ext,
        extension: ext,
        url: '' + img.name
      };
      db.addImage(obj);
    }
  });
})();