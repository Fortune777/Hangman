import { HangmanService } from '../../game/hangman.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pizzalist',
  templateUrl: './pizzalist.component.html',
  styleUrls: ['./pizzalist.component.scss']
})
export class PizzalistComponent implements OnInit {

  // pizzas:pizzaDto[] = [];


  constructor(private service: HangmanService ) {


  }

  ngOnInit(): void {
  }

}
