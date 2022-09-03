import { Application } from "express";

import { UserRouter } from "./user";
import { SaleRouter } from "./sale";
// import { MonitorRouter } from "../controllers/monitor";

// import { RouteDebugger } from '../debuggers';
// import { ImageRouter } from "../controllers/image";
// import { DocumentRouter } from "../controllers/document";
// import { VideoRouter } from "../controllers/video";
// import { AnnouncementRouter } from "../controllers/announcement";

/**
 * Sets all routes of application
 */
export function setRoutes( app: Application ){

    // console.log(UserRouter)

    app.use('/users', UserRouter );
    app.use('/sales', SaleRouter );
    // app.use('/monitors', MonitorRouter );
    // app.use('/images', ImageRouter );
    // app.use('/documents', DocumentRouter );
    // app.use('/videos', VideoRouter );

    // RouteDebugger('Routes has been set.');

    console.log('Routes has been set.');

    // app.post("/contact", contactController.postContact);
    // app.get("/account", passportConfig.isAuthenticated, userController.getAccount);

}

export default setRoutes;