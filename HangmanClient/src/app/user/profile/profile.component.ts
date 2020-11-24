import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { HangmanService } from 'src/app/game/hangman.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  profileGroup: FormGroup;
  addressArray: FormArray;

  constructor(private fb: FormBuilder, public hangmanClient: HangmanService) {
    this.addressArray = this.fb.array([]);
    this.profileGroup = this.fb.group({
      email: [''],
      password: [''],
      address: this.addressArray,
    });
  }

  ngOnInit(): void {
    this.hangmanClient.getUserById
  }

  // tslint:disable-next-line: typedef
  addAddress() {
    this.addressArray.push(this.fb.control(''));
  }
}
