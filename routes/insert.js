const router = require('express').Router();
const insert = require('../controllers/insert.js');
const dateString = require('../components/dateString.js');

router.route('/:name').post(async (req, res) => {
  try {
    console.log(dateString(), '-', req.method, req.originalUrl, req.headers.host);

    let name = req.params.name
    let data = await insert(name);
    res.status(data.statuscode);
    if (data.statuscode) res.json(data.inserted);
    res.end();
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    res.sendStatus(500);
  }
});

module.exports = router;