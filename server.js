require('dotenv').config();
const http = require('http');
const express = require('express');
const dateString = require('./components/dateString.js');
const jsonParser = require('body-parser').json({ type: 'application/json' });

console.log('############### WEB SERVER START UP ###############');
console.log(dateString(), '- starting http server');

const port = process.env.HTTPPORT || 80;

const app = new express();
app.disable('x-powered-by');
app.use(jsonParser);

app.use(express.static('public'));
app.use(express.static(process.env.IMGPATHSTATIC));

app.get('/', function (req, res) {
  res.sendfile('public/index.html');
});

app.use('/images', require('./routes/images.js'));
app.use('/insert', require('./routes/insert.js'));

http.createServer(app).listen(port);
console.log(dateString(), '- listening on port', port);