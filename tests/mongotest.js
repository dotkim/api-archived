require('dotenv').config();
const mongoose = require('mongoose');
const Schema = mongoose.Schema;

let conn = mongoose.connect(
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


const imageSchema = new Schema(
  {
    'fileName': String,
    'contentType': String,
    'extension': String,
    'url': String,
    'thumbnail': String,
    'tags': Array
  },
  { timestamps: true }
);

let images = mongoose.model('image', imageSchema);

images.find({ 'tags': { $nin: [ 'tagme'] } }).then(console.log).catch(console.error);