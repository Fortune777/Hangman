import { themeDto } from '../../../../game/models/themeDto';
import { HangmanService } from '../../../../game/hangman.service';
import {
  Component,
  OnInit,
  Self,
  Optional,
  SkipSelf,
  Input,
} from '@angular/core';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
  providers: [HangmanService],
})
export class CardComponent implements OnInit {
  @Input() Theme: themeDto;

  constructor(
    @Self() public serviceHangman: HangmanService,
    @Optional() @SkipSelf() public serviceHangmanRoot: HangmanService
  ) {}

  ngOnInit(): void {}
}
