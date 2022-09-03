import Sale, { ISale } from '../models/sale';

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

router.post('/', ensureAuthenticated, (req: Request, res: Response, next: NextFunction) => {
    // If this function gets called, authentication was successful.
    // `req.user` contains the authenticated user.

    // let user: Partial<UserModel.LoggedUser>;

    console.log('TEST postSale')
    // console.log(req.body)

    const sale : ISale = new Sale( req.body );
    console.log(sale)

    sale.save((err, product) => {
        if (err) { return next(err); }

        res.status(201).json({
            success: true,
            msg: 'Satış takibi oluşturuldu.'
        })

    });

});



export { router as SaleRouter };