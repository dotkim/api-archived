module.exports = function (mongoose) {
  const Schema = mongoose.Schema;
  const keywordSchema = new Schema({
    'keyword': String,
    'values': Array
  });

  return mongoose.model('keyword', keywordSchema);
}