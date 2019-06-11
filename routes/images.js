const router = require('express').Router();
const images = require('../controllers/images.js');
const dateString = require('../components/dateString.js');
const url = require('url');
const querystring = require('querystring');

router.route('/').get(async (req, res) => {
  try {
    let parsedUrl = url.parse(req.url);
    let parsedQuery = querystring.parse(parsedUrl.query);
    console.log('parsed:', parsedQuery);

    let data = await images(parsedQuery.page, parsedQuery.filter);
    
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