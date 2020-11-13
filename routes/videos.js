/*eslint-disable no-console*/
'use strict';
const router = require('express').Router();
const Videos = require('../controllers/videos');
const dateString = require('../components/dateString');
const url = require('url');
const querystring = require('querystring');

const videos = new Videos();

router.route('/').get(async (req, res) => {
  try {
    let parsedUrl = url.parse(req.url);
    let parsedQuery = querystring.parse(parsedUrl.query);
    let data = await videos.getVideos(parsedQuery.page, parsedQuery.filter);
    
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
    let data = await videos.getRandom();
    
    res.status(data.statuscode);
    if (data.statuscode === 200) res.json(data.content);
    res.end();
  } catch (error) {
    console.error(dateString(), '- got error');
    console.error(error);
    res.sendStatus(500);
  }
});

module.exports = router;