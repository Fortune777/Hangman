import { AuthInterceptor } from './core/interceptor/Auth.Interceptor';
import { VersionComponent } from './version/component/version.component';
import { FormsModule } from '@angular/forms';
import { GameModule } from './game/game.module';
import { UserModule } from './user/user.module';
import { CoreModule } from './core/core.module';
import { HangmanroutingModule } from './hangmanrouting.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';

import { SignalRConfiguration } from 'ng2-signalr';
import { DocmComponent } from './document/docm/docm.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

export function initConfig(): SignalRConfiguration {
  const cfg = new SignalRConfiguration();
  cfg.hubName = 'sample';
  cfg.url = 'https://localhost:44313/';
  // cfg.transport = [ConnectionTransport.webSockets,ConnectionTransport.longPull
  // ],
  return cfg;
}

@NgModule({
  declarations: [AppComponent, VersionComponent, DocmComponent],
  imports: [
    BrowserModule,
    HangmanroutingModule,
    FormsModule,
    CoreModule,
    UserModule,
    GameModule,
  ],
  exports: [VersionComponent, DocmComponent],
  providers: [
    // { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
