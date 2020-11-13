/*eslint-disable no-console*/
'use strict';
const router = require('express').Router();
const triggers = require('../controllers/triggers');
const dateString = require('../components/dateString');

router.route('/').post(async (req, res) => {
  try {
    let data = await triggers(req.body);
    res.status(data.statuscode);
    if (data.statuscode == 200) res.json(data.inserted);
    res.end();
  } catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    res.sendStatus(500);
  }
});

module.exports = router;