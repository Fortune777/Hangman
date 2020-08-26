import { LoginService } from '../../../core/login.service';
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
    private loginService: LoginService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.loginGroup = this.fb.group({
      username: [''],
      psw: [''],
      remember: [true],
    });
  }

  ngOnInit(): void {
    this.loginService.LoggedOn$.pipe(filter((_) => _)).subscribe((_) => {
      this.router.navigate(['game']);
    });
  }

  // tslint:disable-next-line: typedef
  login() {
    this.loginService.login();
  }

  // tslint:disable-next-line: typedef
  loginWithPassword() {
    console.log(this.loginGroup.value);
    this.loginService.login(
      this.loginGroup.value.username,
      this.loginGroup.value.password
    );
  }
}
