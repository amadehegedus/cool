import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CaffModalComponent } from './caff-modal.component';

describe('CaffModalComponent', () => {
  let component: CaffModalComponent;
  let fixture: ComponentFixture<CaffModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CaffModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CaffModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

});
