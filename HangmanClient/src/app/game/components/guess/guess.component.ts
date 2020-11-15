import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { HangmanService, WordDto } from '../../hangman.service';

@Component({
  selector: 'app-guess',
  templateUrl: './guess.component.html',
  styleUrls: ['./guess.component.scss'],
})
export class GuessComponent implements OnInit {
  constructor(private route: ActivatedRoute, private service: HangmanService) {}
  WordResult: WordDto;
  idRoute: number;
  cntProbe = 7;

  firstKeyBoard: Array<string> = [
    'а',
    'б',
    'в',
    'г',
    'д',
    'е',
    'ё',
    'ж',
    'з',
    'и',
    'й',
  ];

  secondKeyBoard: Array<string> = [
    'к',
    'л',
    'м',
    'н',
    'о',
    'п',
    'р',
    'с',
    'т',
    'у',
    'ф',
  ];

  threeKeyBoard: Array<string> = [
    'х',
    'ц',
    'ч',
    'ш',
    'щ',
    'ъ',
    'ы',
    'ь',
    'э',
    'ю',
    'я',
  ];

  ngOnInit(): void {
    this.route.paramMap
      .pipe(switchMap((params) => params.getAll('id')))
      .subscribe((data) => (this.idRoute = +data));

    this.service
      .selectWordsFromTheme(this.idRoute)
      .subscribe((x) => (this.WordResult = x));
  }

  // tslint:disable-next-line: typedef
  getsymbol(letter: string, id: string) {
    this.WordResult.SendChar = letter;
    this.service.isLetterExistWord(this.WordResult).subscribe((model) => {
      this.logic(model, id);
    });
  }

  logic(model: WordDto, id: string): void {
    this.WordResult = model;
    if (this.WordResult.HasChar) {
      document.getElementById(id).setAttribute('style', 'background:green');
      if (this.WordResult.IsWin) {
        // сделать обратиться к бд и записать победу данного юзера
        return;
      }
    } else {
      document.getElementById(id).setAttribute('style', 'background:red');
      this.cntProbe--;
      if (this.cntProbe === 0) {
            
      }
    }
    document.getElementById(id).setAttribute('disabled', 'true');
  }
}
