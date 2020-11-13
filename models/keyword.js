'use strict';
const { Schema } = require('mongoose');
  const keywordSchema = new Schema({
    'keyword': String,
    'values': Array
  });

  module.exports = keywordSchema;