import { Injectable, OnInit } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from './../../../environments/environment';
import { User } from 'src/app/modules/user/user.model';
// import { LoggedUser } from '@app/modules/user/user.model';
// import { async } from 'q';
// import { Observable, of } from 'rxjs';


interface authTestResponse {
  success?: boolean
}

@Injectable()

export class AuthService {

  // authToken: any;

  constructor(
    private http: HttpClient,
    // private httpOld:Http,
    // public jwtHelper: JwtHelperService
  ) {

  }

  registerUser(user) {

    return this.http.post(environment.API.endpointURL + 'users/register', user);

  }

  loginUser(user) {

    return this.http.post(environment.API.endpointURL + 'users/login', user);

  }

  storeUserData(user) {

    this.setLoggedin('true');
    localStorage.setItem('user', JSON.stringify(user));

    
  }

  setLoggedin(x: string) {
    localStorage.setItem('isLoggedin', x);
  }

  logout() {

    localStorage.clear();

    return this.http.get(environment.API.endpointURL + 'users/logout');

  }

  isLoggedIn(): boolean {

    return localStorage.getItem('isLoggedin') === 'true';

  }


  isAuthenticated() {
    return this.http.get<authTestResponse>(environment.API.endpointURL + 'users/status').subscribe((data) => {
      if (data.success) {
        this.setLoggedin('true');
      } else {
        this.setLoggedin('false');
      }
    });

  }

  getLoggedUser(): User.LoggedUser {
    return JSON.parse(localStorage.getItem('user'));
  }

}
