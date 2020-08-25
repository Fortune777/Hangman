import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PizzalistComponent } from './pizzalist.component';

describe('PizzalistComponent', () => {
  let component: PizzalistComponent;
  let fixture: ComponentFixture<PizzalistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PizzalistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PizzalistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
