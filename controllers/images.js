require('dotenv').config();
const fs = require('fs');
const dateString = require('../components/dateString.js');

async function images() {
  try {
    let data = await fs.readdirSync(process.env.IMAGEDIR);

    if (!data) return { statuscode: 404 };

    let obj = {
      current_page: 1,
      number_of_pages: 5,
      next: "",
      previous: ""
    };
    let imageArr = [];

    data.forEach((image) => {
      let extension = image.split('.').pop();
      imageArr.push({
        "file": {
          "fileName": image,
          "contentType": "image/" + extension,
          "url": process.env.IMAGEDIR + image
        }
      });
    });

    obj["images"] = imageArr;

    return { content: obj, statuscode: 200 };
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    return { statuscode: 500 };
  }
}

module.exports = images;