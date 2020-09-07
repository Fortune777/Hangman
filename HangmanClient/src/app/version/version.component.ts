import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-version',
  templateUrl: './version.component.html',
  styleUrls: ['./version.component.scss'],
})
export class VersionComponent {
  inputValue = '';
  title = 'title dynamic11';
  Toggle = true;

  // tslint:disable-next-line: typedef
  onInput(event?): void {
    this.inputValue = event;
  }
}
