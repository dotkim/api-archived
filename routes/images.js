const router = require('express').Router();
const images = require('../controllers/images.js');
const dateString = require('../components/dateString.js');

router.route('/').get(async (req, res) => {
  try {
    let page = req.query.page;
    let filter = req.query.filter;

    let data = await images(page, filter);
    
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