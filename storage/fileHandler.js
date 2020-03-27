'use strict';
const fs = require('fs');

const fileHandler = async (path, data) => {
  try {
    //eslint-disable-next-line no-sync
    return await fs.writeFileSync(path, data);
  } catch (error) {
    //eslint-disable-next-line no-console
    console.error(error);
  }
}

module.exports = fileHandler;