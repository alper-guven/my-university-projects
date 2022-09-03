var result = require('dotenv').config();

if (result.error) {
    throw result.error
}

import express from 'express';
import morgan from 'morgan';
import chalk from 'chalk';
import session from 'express-session';

const MongoStore = require('connect-mongo')(session);

import passport from 'passport';
import configPassport from './config/passport';
import cors, { CorsOptions } from 'cors';
import setRoutes from './controllers'
import bodyParser from 'body-parser';
import mongoose from "mongoose";

const app = express();

var corsOptions: CorsOptions = {
    origin: 'http://localhost:4200',
    optionsSuccessStatus: 200 // some legacy browsers (IE11, various SmartTVs) choke on 204
    ,
    // allowedHeaders: ['Access-Control-Allow-Credentials']
    credentials: true
}

app.use(cors(corsOptions));

// Passport Config
configPassport(passport);

// Morgan Middleware
const morgan1 = morgan(function (tokens, req, res) {
    return [
        chalk.white.bgBlue.bold(' ' + tokens.method(req, res) + ' '),
        tokens.url(req, res),
        tokens.status(req, res),
        tokens.res(req, res, 'content-length'), '-',
        chalk.hex('#FF6600')(tokens['response-time'](req, res), 'ms')
    ].join(' ')
})

app.use(morgan1);

// Express body parser
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

/**
 * Connect to MongoDB.
 */
mongoose.set('useFindAndModify', false);
mongoose.set('useCreateIndex', true);
mongoose.set('useNewUrlParser', true);
mongoose.set('useUnifiedTopology', true);
mongoose.connect(process.env.MONGODB_URI || 'BAGUWIX');
mongoose.connection.on('error', (err) => {
  console.error(err);
  console.log('%s MongoDB connection error. Please make sure MongoDB is running.', chalk.red('✗'));
  process.exit();
});


// Express session

app.use(session({
    name: 'yazmuh.sid',
    resave: true,
    saveUninitialized: true,
    secret: process.env.SESSION_SECRET || 'BAGUWIX',
    cookie: { maxAge: 604800000 }, // one week in milliseconds
    store: new MongoStore({
        url: process.env.MONGODB_URI,
        autoReconnect: true,
    })
}));

// Passport middleware
app.use(passport.initialize());
app.use(passport.session());

// Set Routes
setRoutes(app);

app.get('/', function (req, res) {
    res.sendStatus(403);
});


// Create Server

const PORT = process.env.PORT;

app.listen(PORT, () => {
    console.log(`Server started on port ${PORT}`);
});


export default app;