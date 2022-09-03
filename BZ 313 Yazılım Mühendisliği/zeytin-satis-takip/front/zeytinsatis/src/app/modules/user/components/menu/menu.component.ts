import { Component, OnInit } from '@angular/core';
import { User } from '../../user.model';
import { AuthService } from 'src/app/core/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  loggedUser: User.LoggedUser;

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit() {
    
    this.getLoggedUser();

  }

  getLoggedUser() {

    // this.loggedUser = this.userService.getLoggedUser();

    setTimeout(() => {
      // this.loggedUser = ;


      // console.log(this.loggedUser);
      
    }, 2000);


    this.loggedUser = this.authService.getLoggedUser();

  }

  onClickLogout() {

    this.authService.logout().subscribe(data => {

      console.log(data)
      this.router.navigate(['/login'])

    });

  }

}
