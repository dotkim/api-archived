'use strict';
const config = require('../data/configuration');
const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const dateString = require('../components/dateString.js');

const imageSchema = new Schema({
  'fileName': String,
  'contentType': String,
  'extension': String,
  'url': String,
  'thumbnail': String,
  'tags': Array,
  'checksum': String
}, {
  timestamps: true
});

const keywordSchema = new Schema({
  'keyword': String,
  'values': Array
});

const triggerSchema = new Schema({
  'form': Object
});

module.exports = class {
  constructor() {
    this.conn = mongoose.connect(
      config.mongooseUri, {
      useNewUrlParser: true,
      // use this to remove the warning:
      // DeprecationWarning: collection.ensureIndex is deprecated. Use createIndexes instead.
      useCreateIndex: true,
      // use this to remove the warning:
      // DeprecationWarning: Mongoose: `findOneAndUpdate()` and `findOneAndDelete()` without the `useFindAndModify` option set to false are deprecated.
      useFindAndModify: false,
      // DeprecationWarning: current Server Discovery and Monitoring engine is deprecated, and will be removed in a future version.
      //To use the new Server Discover and Monitoring engine, pass option { useUnifiedTopology: true } to the MongoClient constructor.
      useUnifiedTopology: true,
      //user: process.env.MONGOOSE_USERNAME,
      //pass: process.env.MONGOOSE_PASSWORD,
      dbName: config.mongooseDbname
    },
      function (err) {
        if (err) console.error('Failed to connect to mongo', err); // this might be changed to do some better errorhandling later...
      }
    );

    this.images = mongoose.model('image', imageSchema);
    this.keywords = mongoose.model('keyword', keywordSchema);
    this.triggers = mongoose.model('trigger', triggerSchema);
  }

  // this function takes an object in the form of the Schema
  async addImage(obj) {
    try {
      return await this.images.create(obj);
    }
    catch (error) {
      console.error(dateString(), '- got error');
      console.error(error);
      return 'err';
    }
  }

  // get all images, this returns a pagewise response
  // mode 0 allows all
  // mode 1 filters NSFW images
  // Currently all images with the tagme tag are NSFW.
  // this is to stop new uploads to apear if they are nsfw...
  async getImages(page, mode) {
    try {
      let skip = 0;
      let limit = Number(config.maxImageAmount);
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
    catch (error) {
      console.error(dateString(), '- got error');
      console.error(error);
      return 'err';
    }
  }

  async getKeyword(word) {
    try {
      let keyword = await this.keywords
        .find({ 'keyword': word })
        .select("-_id -__v");
      return keyword[0];
    }
    catch (error) {
      console.error(dateString(), '- got error');
      console.error(error);
      return 'err';
    }
  }

  async addKeyword(word, newMessage) {
    try {
      return await this.keywords
        .findOneAndUpdate(
          { 'keyword': word },
          { $push: { 'values': newMessage } },
          { upsert: true, new: true }
        )
        .select("-_id -__v");
    }
    catch (error) {
      console.error(dateString(), '- got error');
      console.error(error);
      return 'err';
    }
  }

  async randomImage() {
    try {
      const count = await this.images.countDocuments();
      const random = Math.floor(Math.random() * count);

      return await this.images
        .findOne()
        .skip(random);
    }
    catch (error) {
      console.error(dateString(), '- got error');
      console.error(error);
      return 'err';
    }
  }

  async addTriggerEvent(obj) {
    try {
      return await this.triggers.create({ form: obj });
    }
    catch (error) {
      console.error(dateString(), '- got error');
      console.error(error);
      return 'err';
    }
  }
}