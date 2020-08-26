import { FormBuilder } from '@angular/forms';
import { HangmanService } from '../../hangman.service';
import { themeDto } from '../../models/themeDto';
import { Component, OnInit } from '@angular/core';
import { Self, Optional, SkipSelf, Input } from '@angular/core';
import { delay, share } from 'rxjs/operators';
import { WordDto } from '../../models/wordDto';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss'],
})
export class GameComponent implements OnInit {
  Alltheme: themeDto[] = [];
  wordResult: WordDto;
  selId = '0';

  constructor(public srv: HangmanService) {}

  // tslint:disable-next-line: typedef
  btnStartGame() {
    const id = Number.parseInt(this.selId, 10);
    this.srv
      .SelectWordsFromTheme(id)
      .subscribe((data) => (this.wordResult = data));
  }

  ngOnInit(): void {
    this.srv.getAllThemes().subscribe((data) => (this.Alltheme = data));
  }
}
