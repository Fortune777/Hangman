import { Routes } from '@angular/router';
// import { LoginService, oauthConfig } from './login.service';
import { UserDto } from './../models/userDto';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { OAuthService, AuthConfig } from 'angular-oauth2-oidc';
import { routes } from '../hangmanRouting/hangmanrouting.module';
import { Router } from '@angular/router';

export const oauthConfig: AuthConfig = {
  issuer: 'http://localhost:50698',
  redirectUri: window.location.origin + 'index.html',
  clientId: '',
  responseType: 'code',
  dummyClientSecret: 'secret',
  scope: 'openid profile email api',
  requireHttps: false,
  skipIssuerCheck: true,
  skipSubjectCheck: true,
  strictDiscoveryDocumentValidation: false,
  oidc: false,
};

@Injectable({ providedIn: 'root' })
export class LoginService {
  logingroup = new FormGroup({});

  emailControl = new FormControl('', [Validators.required]);
  passwordControl = new FormControl();

  private loggedOnSubject = new BehaviorSubject<boolean>(true);
  private user: UserDto;

  constructor(private router: Router, private oauth: OAuthService) {
    this.oauth.configure(oauthConfig);
    this.oauth.loadDiscoveryDocumentAndTryLogin();
  }

  // tslint:disable-next-line: typedef
  get LoggedOn$() {
    return this.loggedOnSubject.asObservable();
  }

  // tslint:disable-next-line: typedef
  get LoggedOn() {
    return this.loggedOnSubject.value;
  }

  // tslint:disable-next-line: typedef
  login(userName?: string, password?: string) {
    this.oauth.initCodeFlow();

    // setTimeout(() => {
    //   this.user = { uid: '111-111-111', fullName: 'Ivan Ivanov' };
    //   this.loggedOnSubject.next(true);
    // }, 2000);
  }

  // tslint:disable-next-line: typedef
  logout() {
    this.user = null;
    this.loggedOnSubject.next(false);
    this.router.navigate(['home']);
  }
}
