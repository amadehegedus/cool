import {ComponentFixture, fakeAsync, TestBed, tick} from "@angular/core/testing";
import {CaffCardComponent} from "./caff-card.component";
import {CaffDto, CaffService, FileResponse, TagDto} from "../../api/app.generated";
import {Observable, of} from "rxjs";
import {DebugElement} from "@angular/core";
import {By} from "@angular/platform-browser";
import * as dateTimeUtil from "../../utils/dateTimeUtil";
import * as imageUtil from "../../utils/imageUtil";
import * as fileSaver from "file-saver";

jest.spyOn(dateTimeUtil, 'getDateString').mockImplementation(() => '2000-01-01' );
jest.spyOn(fileSaver, 'saveAs').mockImplementation(() => {});
jest.spyOn(imageUtil, 'getImage').mockImplementation(() => 'IMAGE_URL' );

const FILE_RESPONSE_OBJECT:FileResponse = {data: new Blob([""], { type: 'text/html' }), status: 0};
class MockCaffService {
  public downloadCaff(): Observable<FileResponse> {
    return of(FILE_RESPONSE_OBJECT);
  }
}

describe('CaffModalComponent', () => {
  let component: CaffCardComponent;
  let fixture: ComponentFixture<CaffCardComponent>;
  let caffService: CaffService;

  let cardEl: HTMLElement;
  let creatorEl: HTMLElement;
  let tagDe: DebugElement[];
  let creationTimeEl: HTMLElement;
  let imgEl: HTMLImageElement;
  let button: HTMLButtonElement;
  const mockCaff = new CaffDto({
    previewBitmap: 'BITMAP',
    creationTime: new Date('11/12/2010 2:39:28 PM'),
    creator: 'CREATOR',
    id: 0,
    tags: [
      new TagDto({id: 1, text: 'TEXT1'}),
      new TagDto({id: 2, text: 'TEXT2'}),
      new TagDto({id: 3, text: 'TEXT3'}),
    ],
    comments: []
  });

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        CaffCardComponent,
        { provide: CaffService, useClass: MockCaffService }
      ],
    }).compileComponents();

    caffService = TestBed.inject(CaffService);
    fixture = TestBed.createComponent(CaffCardComponent);
    component = fixture.componentInstance;

    component.caff = mockCaff;
    fixture.detectChanges();

    cardEl = fixture.debugElement.query(By.css('.card')).nativeElement;
    creatorEl = fixture.debugElement.query(By.css('.creator')).nativeElement;
    creationTimeEl = fixture.debugElement.query(By.css('.creation-time')).nativeElement;
    button = fixture.debugElement.query(By.css('.btn')).nativeElement;
    imgEl = fixture.debugElement.query(By.css('.img-fluid')).nativeElement;
    tagDe = fixture.debugElement.queryAll(By.css('.tag-element'));
  });

  test('creator name is visible on card', () => {
    expect(creatorEl.textContent).toContain(mockCaff.creator);
  });

  test('img src is image url got by imageUtil', () => {
    expect(imgEl.src).toContain('IMAGE_URL');
  });

});
