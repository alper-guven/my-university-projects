import User, { IUser } from '../models/user';

import express from "express";
import { Request, Response, NextFunction, Router } from "express";
import { ensureAuthenticated, forwardAuthenticated } from '../config/passport';
import passport from 'passport';

const router: Router = express.Router();


// router.post('/', forwardAuthenticated, (req: Request, res: Response) => {



// })



/**
 * Register
 */

router.post('/', (req: Request, res: Response, next: NextFunction) => {
    // If this function gets called, authentication was successful.
    // `req.user` contains the authenticated user.

    // let user: Partial<UserModel.LoggedUser>;

    console.log('TEST postRegister')
    console.log(req.body.user)

    const user : IUser = new User({
        email: req.body.user.email,
        password: req.body.user.password,
        fullName: req.body.user.fullName,
        company: req.body.user.company
    });
    console.log(user)

    User.findOne({ email: req.body.email }, (err, existingUser) => {

        if (err) { return next(err); }

        if (existingUser) {
            res.json({
                msg: 'Account with that email address already exists.'
            });

        }

        user.save((err) => {
            if (err) { return next(err); }
            req.logIn(user, (err) => {
                if (err) {
                    return next(err);
                }
                res.status(201).json({
                    msg: 'Account successfully created.'
                })
            });
        });

    });

});

/**
 * Login
 */

router.post('/login', passport.authenticate('local'), (req: Request, res: Response) => {
    // If this function gets called, authentication was successful.
    // `req.user` contains the authenticated user.

    // let user: Partial<UserModel.LoggedUser>;

    console.log(req.user)

    let user: IUser;

    if (req.user) {

        user = req.user;

        res.json({
            success: true,
            msg: 'Giriş başarılı.',
            user: {
                email: user.email,
                fullName: user.fullName,
                company: user.company
            }
        });


    }


});

// Check if session is active

router.get('/status', ensureAuthenticated, (req: Request, res: Response) => {

    let reqUser;
    let exposableUser;

    if (req.user) {

        reqUser = req.user;

        exposableUser = {
            fullName: reqUser.fullName,
            email: reqUser.email,
            company: reqUser.company
        }

        res.status(200).json({
            success: true,
            user: exposableUser
        })

    }

});


/**
 * Logout
 */

router.get('/logout', (req: Request, res: Response) => {

    req.logout();
    res.json({ success: !req.isAuthenticated() });

});

export { router as UserRouter };