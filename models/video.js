module.exports = function (mongoose) {
  const Schema = mongoose.Schema;
  const videoSchema = new Schema({
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

  return mongoose.model('video', videoSchema);
}