import { Request, Response, Application } from "express";
import express = require('express')
const bodyParser = require('body-parser');
import * as jwt from 'jsonwebtoken';
import * as fs from "fs";
var cors = require('cors');

const app: Application = express();

app.use(cors());

app.options('*', cors()) // include before other routes

app.use(bodyParser.json());

app.route('/api/login')
  .post(loginRoute);



const RSA_PRIVATE_KEY = fs.readFileSync('./key/private.key');

export function loginRoute(req: Request, res: Response) {

  const email = req.body.email,
    password = req.body.password;

  if (validateEmailAndPassword()) {
    const userId = findUserIdForEmail(email);

    const jwtBearerToken = jwt.sign({}, RSA_PRIVATE_KEY, {
      algorithm: 'RS256',
      expiresIn: 120,
      subject: userId
    });

    // send the JWT back to the user
    // TODO - multiple options available     
    var today = new Date();

    res.status(200).json({
      idToken: jwtBearerToken,
      expiresIn: today.setHours(today.getHours() + 1)
    });
  }
  else {
    // send status 401 Unauthorized
    res.sendStatus(401);
  }
}

function validateEmailAndPassword(): boolean {
  return true;
}

function findUserIdForEmail(email: string) {
  return '123';
}

const server = app.listen(3000, () => {
  // console.log(`server listening on port ${server.address().port}`)
});