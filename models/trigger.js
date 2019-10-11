module.exports = function(mongoose) {
  const Schema = mongoose.Schema;
  const triggerSchema = new Schema({
    'form': Object
  });

  return mongoose.model('trigger', triggerSchema);
}