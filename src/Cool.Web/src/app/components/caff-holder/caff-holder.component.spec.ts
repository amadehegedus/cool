import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CaffHolderComponent } from './caff-holder.component';

describe('CaffHolderComponent', () => {
  let component: CaffHolderComponent;
  let fixture: ComponentFixture<CaffHolderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CaffHolderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CaffHolderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
