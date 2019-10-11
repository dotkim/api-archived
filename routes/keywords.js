'use strict';
const router = require('express').Router();
const keywordController = require('../controllers/keywords');
const dateString = require('../components/dateString');
const keyword = new keywordController();

router.route('/:word').get(async (req, res) => {
  try {
    let data = await keyword.getKeyword(req.params.word);
    res.status(data.statuscode);
    if (data.statuscode === 200) res.json(data.content);
    res.end();
  }
  catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    res.sendStatus(500);
  }
}).post(async (req, res) => {
  try {
    let data = await keyword.addKeyword(req.params.word, req.body.message);
    
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