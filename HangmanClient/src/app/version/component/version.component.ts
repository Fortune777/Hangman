import { Component } from '@angular/core';

@Component({
  selector: 'app-version',
  templateUrl: './version.component.html',
  styleUrls: ['./version.component.scss'],
})
export class VersionComponent {
  inputValue = '';
  title = 'title dynamic11';
  Toggle: any = true;
  arr = [1, 2, 4, 8, 16, 32, 64];
  now: Date = new Date();

  objs = [
    {
      title: 'Post1',
      author: 'Vladilen',
      comments: [
        { name: 'Max', text: '11lorem1' },
        { name: 'Max', text: '22lorem2' },
        { name: 'Max', text: '33lorem3' },
      ],
    },
    {
      title: 'Post2',
      author: 'Vladilen2',
      comments: [
        { name: 'Max2', text: '11lorem1222' },
        { name: 'Max2', text: '22lorem2222' },
        { name: 'Max2', text: '33lorem3222' },
      ],
    },
    {
      title: 'Post3',
      author: 'Vladilen3',
      comments: [
        { name: 'Max3', text: '11lorem133333' },
        { name: 'Max3', text: '22lorem2333333' },
        { name: 'Max3', text: '33lorem3333333' },
      ],
    },
  ];

  // tslint:disable-next-line: typedef
  onInput(event?): void {
    this.inputValue = event;
  }
}
