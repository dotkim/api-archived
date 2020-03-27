/*eslint-disable no-console*/
'use strict';
const router = require('express').Router();
const Images = require('../controllers/images');
const dateString = require('../components/dateString');
const url = require('url');
const querystring = require('querystring');

const images = new Images();

router.route('/').get(async (req, res) => {
  try {
    let parsedUrl = url.parse(req.url);
    let parsedQuery = querystring.parse(parsedUrl.query);
    let data = await images.getImagePage(parsedQuery.page, parsedQuery.filter);
    
    res.status(data.statuscode);
    if (data.statuscode === 200) res.json(data.content);
    res.end();
  } catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    res.sendStatus(500);
  }
});

router.route('/getRandom').get(async (req, res) => {
  try {
    let data = await images.getRandom();
    
    res.status(data.statuscode);
    if (data.statuscode === 200) res.json(data.content);
    res.end();
  } catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    res.sendStatus(500);
  }
});

router.route('/').post(async (req, res) => {
  try {
    let data = await images.Insert(req.body);
    
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