import { LoginService } from './../../../services/login.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  message = 'msgForm';
  isLogged = false;

  constructor(public loginSrv: LoginService) {
    this.loginSrv.LoggedOn$.subscribe((flag) => (this.isLogged = flag));
  }

  ngOnInit(): void {}
}
