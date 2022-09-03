import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  credentials = {
    email: null,
    password: null
  };

  errorMessages: Array<String> = [];

  constructor(
    // private userService: UserService,
    private authService: AuthService,
    private router: Router,
  ) {

  }

  ngOnInit() {

    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/panel']);
    }

    // this.getState.subscribe((state) => {
    //   this.errorMessages = state.errorMessages;
    // });

  };

  onLoginSubmit(): void {
    // this.store.dispatch(LoginPageActions.login({ credentials: this.credentials }));

    this.authService.loginUser(this.credentials).subscribe( ( res ) => {


      console.log(typeof(res['success']))

      if (res['success'] === true) {

        console.log('navigated')

        this.authService.storeUserData(res['user']);

        this.router.navigate(['/panel']);
        
      }

    })

  }



}
