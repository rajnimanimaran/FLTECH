import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WoJobworkComponent } from './wo-jobwork.component';

describe('WoJobworkComponent', () => {
  let component: WoJobworkComponent;
  let fixture: ComponentFixture<WoJobworkComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WoJobworkComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WoJobworkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
