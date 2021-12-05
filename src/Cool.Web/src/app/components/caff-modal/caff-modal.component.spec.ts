import {ComponentFixture, TestBed} from "@angular/core/testing";
import {CaffModalComponent} from "./caff-modal.component";
import {By} from "@angular/platform-browser";

describe('CaffModalComponent', () => {
  let component: CaffModalComponent;
  let fixture: ComponentFixture<CaffModalComponent>;
  let modalDe;
  let modalEl: HTMLElement;
  let modalTitleEl: HTMLElement;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CaffModalComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CaffModalComponent);
    component = fixture.componentInstance;

    modalDe = fixture.debugElement.query(By.css('.modal'));
    modalEl = modalDe.nativeElement;

    modalTitleEl = fixture.debugElement.query(By.css('.modal-title')).nativeElement;

    component.title = 'TITLE';
    component.modalId = 'MODEL_ID';

    fixture.detectChanges();
  });

  test('input title is visible in content', () => {
    expect(modalTitleEl.textContent).toBe('TITLE');
  });

  test('input modelId is passed as modal id', () => {
    expect(modalEl.id).toBe('MODEL_ID');
  });
});
