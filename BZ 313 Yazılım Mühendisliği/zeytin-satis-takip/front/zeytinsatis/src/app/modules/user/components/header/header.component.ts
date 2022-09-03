import { Component, OnInit } from '@angular/core';
import { User } from '../../user.model';
import { AuthService } from 'src/app/core/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  loggedUser: User.LoggedUser;

  constructor(private authService: AuthService,
    private router: Router
  ) { 
    this.getLoggedUser();
  }

  ngOnInit() {
  }

  async getLoggedUser() {

      this.loggedUser = this.authService.getLoggedUser();

  }
  

}
