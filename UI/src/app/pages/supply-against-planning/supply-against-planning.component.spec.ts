import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SupplyAgainstPlanningComponent } from './supply-against-planning.component';

describe('SupplyAgainstPlanningComponent', () => {
  let component: SupplyAgainstPlanningComponent;
  let fixture: ComponentFixture<SupplyAgainstPlanningComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplyAgainstPlanningComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplyAgainstPlanningComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
