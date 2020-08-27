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
  id: number;
  constructor(
    private route: ActivatedRoute,

    private service: HangmanService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(async (params) => {
        this.id = +params.get('id');
        return this.service
          .selectWordsFromTheme(this.id)
          .subscribe((x) => (this.WordResult = x));
      })
    );
    // .subscribe((x) => (this.WordResult = x));

    this.route.paramMap
      .pipe(switchMap((params) => params.getAll('id')))
      .subscribe((data) => (this.id = +data));

    alert(this.WordResult.Word);
  }
}
