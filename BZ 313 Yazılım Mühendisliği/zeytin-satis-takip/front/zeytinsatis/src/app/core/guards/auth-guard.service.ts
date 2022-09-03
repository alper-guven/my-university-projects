import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from 'src/app/modules/user/user.model';
import { AuthService } from '../auth/auth.service';

// import { LoggedUser } from '@app/modules/user/user.model';

@Injectable()
export class AuthGuardService implements CanActivate{

  // getState: Observable<any>;
  isAuthenticated: boolean = false;
  user: User.LoggedUser | null = null;

  constructor(
    private authService: AuthService,
    public router: Router, 
  ) {

    authService.isAuthenticated();
    this.isAuthenticated = authService.isLoggedIn();
    
  }


  canActivate(route: ActivatedRouteSnapshot): boolean {

    if ( ! this.isAuthenticated ) {

      this.router.navigate(['login']);
      return false;

    }

    return true;
  }

  

}