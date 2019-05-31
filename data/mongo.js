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

  // this function takes an object in the form of the Schema
  async addImage(obj) {
    return this.images.create(obj);
  }

  // get all images, this returns a pagewise response
  async getImages(page) {
    let skip = 0;
    let limit = Number(process.env.MAXIMAGEAMOUNT);
    if (page !== 0) skip = limit * page;

    let imgs = await this.images
      .find({})
      .select("-_id -__v")
      .skip(skip)
      .sort({ createdAt: -1 })
      .limit(limit);

    let imageCount = await this.images.countDocuments({});
    return { imageCount: imageCount, images: imgs, limit: limit };
  }
}

module.exports = MongoDb;