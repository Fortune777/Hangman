import { UserDto } from '../../models/userDto';
import { LoginService } from '../../../core/login.service';
import { Component, OnInit, NgModule, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit, OnDestroy {
  message = 'msgForm';
  email: string;
  user$: Observable<UserDto>;
  isLogged = false;
  // private subscriptions: Subscription;

  constructor(private loginSrv: LoginService) {}

  ngOnInit(): void {
    this.user$ = this.loginSrv.LoggedOn$.pipe(
      tap((u) => (this.isLogged = u !== null || u !== undefined))
    );
  }

  ngOnDestroy(): void {
    // this.subscriptions.unsubscribe();
  }

  // tslint:disable-next-line: typedef
  logout() {
    this.loginSrv.logout();
  }
}
