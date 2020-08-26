import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameComponent } from './components/game/game.component';
import { GuessComponent } from './components/guess/guess.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [GameComponent, GuessComponent],

  imports: [CommonModule, RouterModule, ReactiveFormsModule],

  exports: [GameComponent, GuessComponent, HttpClientModule],
})
export class GameModule {}
