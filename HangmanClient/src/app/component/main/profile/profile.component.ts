import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  profileGroup: FormGroup;
  addressArray: FormArray;

  constructor(private fb: FormBuilder) {
    this.addressArray = this.fb.array([]);
    this.profileGroup = this.fb.group({
      email: [''],
      password: [''],
      address: this.addressArray,
    });
  }

  ngOnInit(): void {}

  // tslint:disable-next-line: typedef
  addAddress() {
    this.addressArray.push(this.fb.control(''));
  }
}
