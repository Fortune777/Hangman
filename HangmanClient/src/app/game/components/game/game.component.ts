import { HangmanService, WordDto, ThemeDto } from './../../hangman.service';
import { FormBuilder } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Self, Optional, SkipSelf, Input } from '@angular/core';
import { delay, share } from 'rxjs/operators';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss'],
})
export class GameComponent implements OnInit {
  Alltheme: ThemeDto[] = [];
  wordResult: WordDto;
  selId = '0';

  constructor(public hangmanClient: HangmanService) {}

  ngOnInit(): void {
    this.hangmanClient
      .getAllThemes()
      .subscribe((data) => (this.Alltheme = data));
  }
}
