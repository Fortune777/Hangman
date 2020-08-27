import { filter, switchMap, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Routes } from '@angular/router';
import { UserDto } from './models/userDto';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, from } from 'rxjs';
import {
  FormControl,
  Validators,
  FormGroup,
  FormBuilder,
} from '@angular/forms';
import { OAuthService, AuthConfig, OAuthEvent } from 'angular-oauth2-oidc';
import { Router } from '@angular/router';
import { ValueConverter } from '@angular/compiler/src/render3/view/template';

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
  oidc: false,
  postLogoutRedirectUri: window.location.origin + '/login',
};

@Injectable({ providedIn: 'root' })
export class LoginService {
  private loggedOnSubject: BehaviorSubject<UserDto> = new BehaviorSubject<
    UserDto
  >(null);
  private user: UserDto;

  constructor(private router: Router, private oauth: OAuthService) {
    this.oauth.configure(oauthConfig);
    this.oauth.loadDiscoveryDocumentAndTryLogin();
    this.oauth.events
      .pipe(
        filter((value) => value.type === 'token_received'),
        map((_) => Object.assign({} as UserDto, this.oauth.getIdentityClaims()))
      )
      .subscribe((u) => this.loggedOnSubject.next(u));
  }

  // tslint:disable-next-line: typedef
  get LoggedOn$(): Observable<UserDto> {
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

    // Promise -> Observable
    this.oauth
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(userName, password)
      .then((userInfo) => {
        this.user = Object.assign({} as UserDto, userInfo);
        this.loggedOnSubject.next(this.user);
      })
      .catch((reason) => console.error(reason));
  }

  // tslint:disable-next-line: typedef
  logout() {
    this.user = null;
    this.loggedOnSubject.next(null);
    this.oauth.logOut(true);
    this.router.navigate(['home']);
  }
}
