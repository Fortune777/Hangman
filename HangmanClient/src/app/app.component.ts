import { themeDto } from './models/themeDto';
import { HangmanService } from './services/hangman.service';
import { Component, OnInit } from '@angular/core';
import { templateJitUrl } from '@angular/compiler';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'HangmanClient';
  // Alltheme: themeDto[] = [];

  constructor(private srv: HangmanService) {}
  ngOnInit(): void {
    // this.srv.getAllThemes().subscribe((data) => (this.Alltheme = data));
  }
}
