require('dotenv').config();
const mongoose = require('mongoose');
const Schema = mongoose.Schema;

const imageSchema = new Schema({
  'fileName': String,
  'contentType': String,
  'extension': String,
  'url': String,
  'thumbnail': String,
  'tags': Array
}, {
  timestamps: true
});

module.exports = class {
  constructor() {
    this.conn = mongoose.connect(
      process.env.MONGOOSE_MONGOURI, {
        useNewUrlParser: true,
        useCreateIndex: true, // use this to remove the warning: DeprecationWarning: collection.ensureIndex is deprecated. Use createIndexes instead.
        //user: process.env.MONGOOSE_USERNAME,
        //pass: process.env.MONGOOSE_PASSWORD,
        dbName: process.env.MONGOOSE_DBNAME
      },
      function (err) {
        if (err) console.error('Failed to connect to mongo', err); // this might be changed to do some better errorhandling later...
      }
    );

    this.images = mongoose.model('image', imageSchema);
  }

  // this function takes an object in the form of the Schema
  async addImage(obj) {
    return await this.images.create(obj);
  }

  // get all images, this returns a pagewise response
  // mode 0 allows all
  // mode 1 filters NSFW images
  // Currently all images with the tagme tag are NSFW.
  // this is to stop new uploads to apear if they are nsfw...
  async getImages(page, mode) {
    console.log(typeof mode, mode);
    let skip = 0;
    let limit = Number(process.env.MAXIMAGEAMOUNT);
    if (page !== 0) skip = limit * page;

    let imgs;
    let imageCount;

    if (mode === 0) {
      imgs = await this.images
        .find()
        .select("-_id -__v")
        .skip(skip)
        .sort({
          createdAt: -1
        })
        .limit(limit);
      imageCount = await this.images.countDocuments();
    } else if (mode === 1) {
      imgs = await this.images
        .find({
          'tags': {
            $nin: ['tagme']
          }
        })
        .select("-_id -__v")
        .skip(skip)
        .sort({
          createdAt: -1
        })
        .limit(limit);
      imageCount = await this.images.countDocuments({
        'tags': {
          $nin: ['tagme']
        }
      });
    }

    return {
      imageCount: imageCount,
      images: imgs,
      limit: limit
    };
  }
}