'use strict';
const { Schema } = require('mongoose');
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

module.exports = videoSchema;