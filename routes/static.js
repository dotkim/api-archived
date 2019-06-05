const router = require('express').Router();
const static = require('../controllers/images.js');
const dateString = require('../components/dateString.js');

router.route('/Discord').get(async (req, res) => {
  try {
    let data = await static(req.originalUrl);
    res.status(data.statuscode);
    if (data.statuscode === 200) res.type(data.type);
    res.end();
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    res.sendStatus(500);
  }
});

router.route('/thumbnails').get(async (req, res) => {
  try {
    let data = await static(req.originalUrl);
    res.status(data.statuscode);
    if (data.statuscode === 200) res.type(data.type);
    res.end();
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    res.sendStatus(500);
  }
});

module.exports = router;