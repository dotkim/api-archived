const router = require('express').Router();
const images = require('../controllers/images.js');
const dateString = require('../components/dateString.js');

router.route('/').get(async (req, res) => {
  try {
    let data = await images(req.query.page, req.query.filter);
    res.status(data.statuscode);
    if (data.statuscode === 200) res.json(data.content);
    res.end();
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    res.sendStatus(500);
  }
});

module.exports = router;