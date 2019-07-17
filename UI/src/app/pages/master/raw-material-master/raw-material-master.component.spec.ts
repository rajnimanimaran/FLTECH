import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RawMaterialMasterComponent } from './raw-material-master.component';

describe('RawMaterialMasterComponent', () => {
  let component: RawMaterialMasterComponent;
  let fixture: ComponentFixture<RawMaterialMasterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RawMaterialMasterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RawMaterialMasterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
