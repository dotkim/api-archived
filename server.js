const http = require('http');
const express = require('express');
const dateString = require('./components/dateString.js');
const jsonParser = require('body-parser').json({ type: 'application/json' });

console.log('############### WEB SERVER START UP ###############');
console.log(dateString(), '- starting http server')

const app = new express();
app.disable('x-powered-by');
app.use(jsonParser);

app.use('/images', require('./routes/images.js'));

http.createServer(app).listen('80');
console.log(dateString(), '- listening on port', '80');