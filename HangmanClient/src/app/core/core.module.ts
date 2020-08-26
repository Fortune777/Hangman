import { LoginService } from './login.service';
import { OAuthModule } from 'angular-oauth2-oidc';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LoginComponent } from './components/login/login.component';
import { CommonModule } from '@angular/common';
import {
  NgModule,
  SkipSelf,
  Optional,
  ModuleWithProviders,
} from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './components/home/home.component';

import { provideRoutes } from '@angular/router';

@NgModule({
  declarations: [
    LoginComponent,
    NavbarComponent,
    NotFoundComponent,
    HomeComponent,
  ],

  imports: [CommonModule, ReactiveFormsModule, OAuthModule.forRoot()],

  exports: [
    LoginComponent,
    NavbarComponent,
    NotFoundComponent,
    OAuthModule,
    HomeComponent,
  ],
  providers: [],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() coreModule: CoreModule) {
    if (coreModule) {
      throw new Error('CoreModule already loaded.');
    }
  }

  static forRoot(): ModuleWithProviders<CoreModule> {
    return {
      ngModule: CoreModule,
      providers: [LoginService],
    };
  }
}
