import { WordDto } from './../models/wordDto';
import { themeDto } from './../models/themeDto';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class HangmanService {
  serviceId: string;

  constructor(private http: HttpClient) {
    this.serviceId = Date.now().toPrecision(21).toString();
  }

  // tslint:disable-next-line: typedef
  getAllThemes() {
    return this.http.get<themeDto[]>(
      `${environment.backendUrl}/api/Hangman/GetAllThemesAsync`
    );
  }

  // tslint:disable-next-line: typedef
  SelectWordsFromTheme(id: number) {
    return this.http.get<WordDto>(
      `${environment.backendUrl}/api/Hangman/SelectWordsFromThemeAsync/${id}`
    );
  }
}
