require('dotenv').config();
const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const imageSchema = new Schema(
  {
    "fileName": String,
    "contentType": String,
    "extension": String,
    "url": String
  },
  { timestamps: true }
);

class MongoDb {
  constructor() {
    this.conn = mongoose.connect(
      process.env.MONGOOSE_MONGOURI, {
        useNewUrlParser: true,
        useCreateIndex: true,                 // use this to remove the warning: DeprecationWarning: collection.ensureIndex is deprecated. Use createIndexes instead.
        //user: process.env.MONGOOSE_USERNAME,
        //pass: process.env.MONGOOSE_PASSWORD,
        dbName: process.env.MONGOOSE_DBNAME
      },
      function (err) {
        if (err) console.error('Failed to connect to mongo', err);    // this might be changed to do some better errorhandling later...
      }
    );

    this.images = mongoose.model('image', imageSchema);
  }

  // this function takes an object in the form of the Schema.
  async addImage(obj) {
    console.log(obj);
    return this.images.create(obj);
  }

  async getImage(image) {
    return this.images.find({ image })
  }

  // get all images, 
  async getImages(page) {
    let skip = 0;
    if (page !== 0) skip = process.env.MAXIMAGEAMOUNT * page;
    return this.images
      .find({})
      .skip(skip)
      .sort({ createdAt: -1 })
      .limit(30);
  }
}

module.exports = MongoDb;