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
  WordResult: WordDto;

  constructor(
    private route: ActivatedRoute,
    private HangmanSrv: HangmanService
  ) {}

  ngOnInit(): void {
    // this.route.paramMap
    //   .pipe(
    //     switchMap((params) => {
    //       const id = +params.get('id');
    //       return this.HangmanSrv.SelectWordsFromTheme(id);
    //     })
    //   )
    //   .subscribe((data) => (this.WordResult = data));

    alert(this.WordResult.Word);
  }
}
