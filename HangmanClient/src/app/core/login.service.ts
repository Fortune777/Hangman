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
  // responseType: 'code',
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
  loginGroup = new FormGroup({});

  // emailControl = new FormControl('', [Validators.required]);
  // passwordControl = new FormControl();

  private loggedOnSubject: BehaviorSubject<UserDto> = new BehaviorSubject<
    UserDto
  >(null);
  private user: UserDto;

  constructor(
    private router: Router,
    private oauth: OAuthService,
    private fb: FormBuilder
  ) {
    this.loginGroup = this.fb.group({
      username: [''],
      password: [''],
      remember: [true],
    });

    this.oauth.configure(oauthConfig);
    this.oauth.loadDiscoveryDocumentAndTryLogin();
  }

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
      this.oauth.events
        .pipe(
          filter((value) => value.type === 'token_received'),
          map((_) =>
            Object.assign({} as UserDto, this.oauth.getIdentityClaims())
          )
        )
        .subscribe((u) => this.loggedOnSubject.next(u));
    }

    this.oauth
      .fetchTokenUsingPasswordFlowAndLoadUserProfile(userName, password)
      .then((userInfo) => {
        this.user = Object.assign({} as UserDto, userInfo);
        this.loggedOnSubject.next(this.user);
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
    this.loggedOnSubject.next(this.user);
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
