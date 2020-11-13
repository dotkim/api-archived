/* eslint-disable */
'use strict'
const fs = require('fs');
const app = require('../app');
const config = require('../models/configuration');
const chai = require('chai');
const chaiHttp = require('chai-http');
chai.use(chaiHttp);
chai.should();

describe('Images', () => {
  describe('GET /images', () => {

    it('should get first page with all images', (done) => {
      chai.request(app).
        get('/images').
        end((err, res) => {
          res.should.have.status(200);
          res.body.should.be.a('object');
          done();
        });
    });

    it('should get a random image', (done) => {
      chai.request(app).
        get('/images/getRandom').
        end((err, res) => {
          res.should.have.status(200);
          res.body.should.be.a('object');
          done();
        });
    });

  });

  describe('POST /images', () => {
    const name = 'test6.png';
    const imageData = fs.readFileSync('tests/data/sample.png', { encoding: 'base64' });
    const body = JSON.stringify({ name, imageData });

    describe('insert', () => {
      it('should insert an image', (done) => {
        chai.request(app).
          post('/images').
          auth(config.authUser, config.authPass).
          type('json').
          send(body).
          end((err, res) => {
            res.should.have.status(200);
            res.should.be.a('object');
            done();
          });
      });
    });

    describe('authentication', () => {
      it('should not authenticate, no auth provided', (done) => {
        chai.request(app).
          post('/images').
          send(body).
          end((err, res) => {
            res.should.have.status(401);
            done();
          });
      });
      it('should not authenticate, wrong username', (done) => {
        chai.request(app).
          post('/images').
          auth('ObviouslyWrongUsername', config.authPass).
          send(body).
          end((err, res) => {
            res.should.have.status(401);
            done();
          });
      });
      it('should not authenticate, wrong password', (done) => {
        chai.request(app).
          post('/images').
          auth(config.authUser, 'ThisIsNotTheCorrectPassword').
          send(body).
          end((err, res) => {
            res.should.have.status(401);
            done();
          });
      });
    });
  });
});