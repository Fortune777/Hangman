import { Routes } from '@angular/router';
// import { LoginService, oauthConfig } from './login.service';
import { UserDto } from './models/userDto';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { OAuthService, AuthConfig } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';

export const oauthConfig: AuthConfig = {
  issuer: 'http://localhost:50698',
  redirectUri: window.location.origin + '/index.html',
  clientId: 'HangmanClient',
  responseType: 'code',
  dummyClientSecret: 'secret',
  scope: 'openid profile email',
  requireHttps: false,
  skipIssuerCheck: true,
  showDebugInformation: true,
  disablePKCE: true,
  // postLogoutRedirectUri: window.location.origin + '/login',
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
    if (!userName || !password) {
      this.oauth.initLoginFlow();
    }

    this.oauth
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(userName, password)
      .then((userInfo) => {
        this.user = { uid: userInfo.sub, fullName: 'Ivan Ivanov' };
        this.loggedOnSubject.next(true);
      })
      .catch((reason) => console.error(reason));

    // setTimeout(() => {
    //   this.user = { uid: '111-111-111', fullName: 'Ivan Ivanov' };
    //   this.loggedOnSubject.next(true);
    // }, 2000);
  }

  // tslint:disable-next-line: typedef
  logout() {
    this.user = null;
    this.loggedOnSubject.next(false);
    this.oauth.logOut();
    this.router.navigate(['home']);
  }

  // tslint:disable-next-line: typedef
  loadUserInfo() {
    const claims = this.oauth.getIdentityClaims();

    //   this.oauth
    //     .loadUserProfile()
    //     .then((userInfo) => {
    //       this.user = { uid: userInfo.sub, fullName: 'Ivan Ivanov' };
    //       this.loggedOnSubject.next(true);
    //     })
    //     .catch((reason) => console.error(reason));
    // }
  }
}
