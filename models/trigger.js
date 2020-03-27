'use strict';
const { Schema } = require('mongoose');
const triggerSchema = new Schema({
  'form': Object
});

module.exports = triggerSchema;