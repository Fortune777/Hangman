import { LoginService } from './../../../services/login.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import {
  FormControl,
  Validators,
  FormGroup,
  FormBuilder,
} from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})

export class LoginComponent implements OnInit {
  loginGroup: FormGroup;
  // ({
  //   email: new FormControl('', [Validators.required, Validators.email]),
  //   psw: new FormControl('', [Validators.required]),
  //   remember: new FormControl(true),
  // });

  constructor(
    private loginSrv: LoginService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.loginGroup = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      psw: ['', [Validators.required]],
      remember: [true],
    });
  }

  ngOnInit(): void {
    this.loginSrv.LoggedOn$.pipe(filter((_) => _)).subscribe((_) => {
      this.router.navigate(['game']);
    });
  }

  // tslint:disable-next-line: typedef
  login() {
    this.loginSrv.login();
  }
}
