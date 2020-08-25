import { LoginComponent } from './../component/main/login/login.component';
import { LoginService } from './../services/login.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class HangmanGuard implements CanActivate {
  constructor(private loginSrv: LoginService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    if (this.loginSrv.LoggedOn) {
      return true;
    }

    this.router.navigate(['login']);
    return false;
  }
}
