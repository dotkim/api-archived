const router = require('express').Router();
const insert = require('../controllers/insert.js');
const dateString = require('../components/dateString.js');

router.route('/:name/:folder').post(async (req, res) => {
  try {
    console.log(dateString(), '-', req.method, req.originalUrl, req.headers.host);

    let data = await insert(req.params.name, req.params.folder);
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