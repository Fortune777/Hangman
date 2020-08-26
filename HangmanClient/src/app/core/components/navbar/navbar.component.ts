import { UserDto } from '../../models/userDto';
import { OAuthService } from 'angular-oauth2-oidc';
import { LoginService } from '../../../core/login.service';
import { Component, OnInit, NgModule } from '@angular/core';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  message = 'msgForm';
  isLogged = false;
  email: string;

  constructor(public loginSrv: LoginService, private oauth: OAuthService) {
    this.loginSrv.LoggedOn$.pipe(
      map((_) => this.oauth.getIdentityClaims())
    ).subscribe((obj) => {
      this.isLogged = obj !== null;
      const user = Object.assign({} as UserDto, obj);
      this.email = user.email;
    });
  }

  ngOnInit(): void {}
}
