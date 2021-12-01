import { Component, OnInit } from '@angular/core';
import {CaffService, IUploadCaffDto, UploadCaffDto} from 'src/app/api/app.generated';

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.scss']
})
export class UploaderComponent implements OnInit {

  caff: IUploadCaffDto = { tags: [], caffBytes: ''};
  static showSuccessMessage: boolean = false;
  static showFailedMessage: boolean = false;

  constructor(private api: CaffService) {}

  ngOnInit(): void {
  }

  uploadCaff(): void {
    this.api.uploadCaff(new UploadCaffDto(this.caff)).subscribe(r => {
      UploaderComponent.showSuccessMessage = true;
      setTimeout(() => { UploaderComponent.showSuccessMessage = false;  } , 3000);
      this.caff = { tags: [], caffBytes: '' };
    }, e => {
      UploaderComponent.showFailedMessage = true;
      setTimeout(() => { UploaderComponent.showFailedMessage = false;  } , 3000);
    });
  }

  fileChange(event: any) {
    this.caff.caffBytes = 'asd';
  }

  getSuccessEnabled() {
    return UploaderComponent.showSuccessMessage;
  }

  getFailedEnabled() {
    return UploaderComponent.showFailedMessage;
  }
}
