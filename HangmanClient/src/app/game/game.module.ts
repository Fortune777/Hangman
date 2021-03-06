import { environment } from './../../environments/environment.prod';
import { HangmanService, API_BASE_URL } from './hangman.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameComponent } from './components/game/game.component';
import { GuessComponent } from './components/guess/guess.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [GameComponent, GuessComponent],

  imports: [CommonModule, RouterModule, ReactiveFormsModule, FormsModule],

  exports: [GameComponent, GuessComponent, HttpClientModule],
  providers: [
    HangmanService,
    { provide: API_BASE_URL, useValue: environment.backendUrl },
  ],
})
export class GameModule {}
