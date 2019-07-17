import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FlowTechDialogComponent } from './flow-tech-dialog.component';

describe('FlowTechDialogComponent', () => {
  let component: FlowTechDialogComponent;
  let fixture: ComponentFixture<FlowTechDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FlowTechDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FlowTechDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
