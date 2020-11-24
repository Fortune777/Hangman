import { DocmComponent } from './document/docm/docm.component';
import { VersionComponent } from './version/component/version.component';
import { GameModule } from './game/game.module';
import { UserModule } from './user/user.module';
import { CoreModule } from './core/core.module';
import { NotFoundComponent } from './core/components/not-found/not-found.component';
import { LoginComponent } from './core/components/login/login.component';
import { ProfileComponent } from './user/profile/profile.component';
import { GuessComponent } from './game/components/guess/guess.component';
import { GameComponent } from './game/components/game/game.component';
import { HomeComponent } from './core/components/home/home.component';
import { AuthGuard } from './core/guards/hangman.guard';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';

/*
маршруты
 /home
 /pizzas
 /login
 /logout

 / -root
 /xxx - not found (не существующией в системе)
//{ path: 'game', component: GameComponent,  canActivate: [AuthGuard] },
*/

export const routes: Routes = [
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'game', component: GameComponent, canActivate: [AuthGuard] },
  { path: 'game/:id', component: GuessComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: ProfileComponent },
  { path: 'login', component: LoginComponent },
  { path: 'index.html', component: HomeComponent },
  { path: 'version', component: VersionComponent },
  { path: 'docm', component: DocmComponent },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes),
    CoreModule,
    UserModule,
    GameModule,
  ],
  exports: [RouterModule],
})
export class HangmanroutingModule {}
