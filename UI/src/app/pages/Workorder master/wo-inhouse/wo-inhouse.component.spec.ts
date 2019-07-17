import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WoInhouseComponent } from './wo-inhouse.component';

describe('WoInhouseComponent', () => {
  let component: WoInhouseComponent;
  let fixture: ComponentFixture<WoInhouseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WoInhouseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WoInhouseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
