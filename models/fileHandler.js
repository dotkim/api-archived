const fs = require('fs');

module.exports = async function(path, data) {
  try {
    return await fs.writeFileSync(path, data);
  } catch (error) {
    console.error(error);
    return;
  }
}