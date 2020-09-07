import { FormsModule } from '@angular/forms';
import { GameModule } from './game/game.module';
import { UserModule } from './user/user.module';
import { CoreModule } from './core/core.module';
import { HangmanroutingModule } from './hangmanrouting.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';

import { SignalRConfiguration } from 'ng2-signalr';

export function initConfig(): SignalRConfiguration {
  const cfg = new SignalRConfiguration();
  cfg.hubName = 'sample';
  cfg.url = 'https://localhost:44313/';
  // cfg.transport = [ConnectionTransport.webSockets,ConnectionTransport.longPull
  // ],
  return cfg;
}

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    HangmanroutingModule,
    FormsModule,
    CoreModule,
    UserModule,
    GameModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
