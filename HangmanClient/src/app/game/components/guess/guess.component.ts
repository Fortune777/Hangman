import { routes } from './../../../hangmanrouting.module';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { HangmanService, WordDto } from '../../hangman.service';

@Component({
  selector: 'app-guess',
  templateUrl: './guess.component.html',
  styleUrls: ['./guess.component.scss'],
})
export class GuessComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private service: HangmanService,
    private router: Router
  ) {}

  WordResult: WordDto;
  idRoute: number;
  cntProbe = 7;
  showWord: Array<string>;

  //
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

    this.service.selectWordsFromTheme(this.idRoute).subscribe((x) => {
      this.WordResult = x;
      this.showWord = '-'.repeat(x.Word.length).split('');
    });
  }

  // tslint:disable-next-line: typedef
  getsymbol(letter: string, id: string) {
    this.WordResult.SendChar = letter;
    this.service.isLetterExistWord(this.WordResult).subscribe((model) => {
      this.logic(model, id);
    });
    this.WordResult.SendChar = null;
  }

  logic(model: WordDto, id: string): void {
    this.WordResult = model;
    if (this.WordResult.HasChar) {
      document.getElementById(id).classList.add('bg-success');

      for (let index = 0; index < this.WordResult.Word.length; index++) {
        if (this.WordResult.Word[index] === this.WordResult.SendChar) {
          this.showWord[index] = this.WordResult.SendChar;
          // const fstr = this.showWord.substr(0, index);
          // const sstr = this.showWord.substr(index, this.showWord.length);

          // this.showWord = fstr + this.WordResult.SendChar + sstr;
        }
      }

      if (this.WordResult.IsWin) {
        // сделать обратиться к бд и записать победу данного юзера
        return;
      }
    } else {
      document.getElementById(id).classList.add('bg-danger');
      this.cntProbe--;
      // if (this.cntProbe === 0) {
      //   this.router.navigate(['home']);
      // }
    }
    document.getElementById(id).setAttribute('disabled', 'true');
  }

  GenerateNewWord() {
    this.router.navigate(['game']);
  }
}
