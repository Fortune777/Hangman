import { GuessComponent } from './../component/main/guess/guess.component';
import { HangmanGuard } from './hangman.guard';
import { GameComponent } from './../component/main/game/game.component';
import { CardComponent } from './../component/core/card/card/card.component';
import { NotFoundComponent } from './../component/main/not-found/not-found.component';
import { LoginComponent } from './../component/main/login/login.component';
import { HomeComponent } from './../component/main/home/home.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from '../component/main/profile/profile.component';

/*
маршруты

 /home
 /pizzas
 /login
 /logout

 / -root
 /xxx - not found (не существующией в системе)
//{ path: 'game', component: GameComponent, canActivate: [HangmanGuard] },
*/

export const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'game', component: GameComponent },
  { path: 'game/:id', component: GuessComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'card', component: CardComponent },
  { path: 'login', component: LoginComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  declarations: [],
  imports: [CommonModule, RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class HangmanroutingModule {}
