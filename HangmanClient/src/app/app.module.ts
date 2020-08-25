import { NotFoundComponent } from './component/main/not-found/not-found.component';
import { HangmanroutingModule } from './hangmanRouting/hangmanrouting.module';
import { HangmanService } from './services/hangman.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { NavbarComponent } from './component/main/navbar/navbar.component';
import { LoginComponent } from './component/main/login/login.component';
import { CardComponent } from './component/core/card/card/card.component';
import { GameComponent } from './component/main/game/game.component';
import { HomeComponent } from './component/main/home/home.component';
import { ProfileComponent } from './component/main/profile/profile.component';
import { GuessComponent } from './component/main/guess/guess.component';

import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { OAuthModule } from 'angular-oauth2-oidc';
import {
  SignalRModule,
  SignalRConfiguration,
  ConnectionTransport,
} from 'ng2-signalr';

export function initConfig(): SignalRConfiguration {
  const cfg = new SignalRConfiguration();
  cfg.hubName = 'sample';
  cfg.url = 'https://localhost:44313/';
  // cfg.transport = [ConnectionTransport.webSockets,ConnectionTransport.longPull
  // ],
  return cfg;
}

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    CardComponent,
    HomeComponent,
    NotFoundComponent,
    GameComponent,
    ProfileComponent,
    GuessComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    HangmanroutingModule,
    OAuthModule.forRoot(),
  ],

  providers: [HangmanService],
  bootstrap: [AppComponent],
})
export class AppModule {}
