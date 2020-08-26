import { HangmanroutingModule } from './hangmanrouting.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';

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
  declarations: [AppComponent],
  imports: [BrowserModule, HangmanroutingModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
