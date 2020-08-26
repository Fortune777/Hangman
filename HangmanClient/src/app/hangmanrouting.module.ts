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
  { path: 'home', component: HomeComponent },
  { path: 'game', component: GameComponent },
  { path: 'game/:id', component: GuessComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    CoreModule,
    UserModule,
    GameModule,
    RouterModule.forRoot(routes),
  ],
  exports: [RouterModule],
})
export class HangmanroutingModule {}
