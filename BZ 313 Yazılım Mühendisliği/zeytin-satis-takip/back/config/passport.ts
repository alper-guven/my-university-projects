import bcryptjs from 'bcryptjs';
import passport, { PassportStatic } from "passport";
import passportLocal from "passport-local";
import User, { IUser } from '../models/User';

import { Request, Response, NextFunction } from "express";


const LocalStrategy = passportLocal.Strategy;

function configPassport(passport: PassportStatic) {

    passport.serializeUser((user: any, done) => {
        done(null, user.id);
    });

    passport.deserializeUser((id, done) => {
        User.findById(id, (err, user) => {
            done(err, user);
        });
    });

    /**
     * Sign in using Email and Password.
     */
    passport.use(new LocalStrategy({ usernameField: 'email' }, (email, password, done) => {

        console.log('LOGIN email: ' + email)

        User.findOne({ email: email.toLowerCase() }, (err, user : IUser) => {

            console.log(user)

            if (err) { 
                return done(err); 
            }
            if (!user) {
                return done(null, false, { message: `Email ${email} not found.` });
            }
            // if (!user.password) {
            //     return done(null, false, { message: 'Your account was registered using a sign-in provider. To enable password login, sign in using a provider, and then set a password under your user profile.' });
            // }
            user.comparePassword(password, (err: any, isMatch: boolean) => {
                if (err) { return done(err); }
                if (isMatch) {
                    return done(null, user);
                }
                return done(null, false, { message: 'Invalid email or password.' });
            });
        });
    }));



}


/**
 *  Accepts the request if the user is authenticated
 */
export function ensureAuthenticated(req: Request, res: Response, next: NextFunction) {
    if (req.isAuthenticated()) {
        return next();
    }
    res.sendStatus(401);
}

/**
 *  Accepts the request if the user is not authenticated
 */
export function forwardAuthenticated(req: Request, res: Response, next: NextFunction) {

    if (!req.isAuthenticated()) {

        return next();
    }
    res.json({ msg: 'You are logged in' });
}

export default configPassport;