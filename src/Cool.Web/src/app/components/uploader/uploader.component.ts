import { Component, OnInit } from '@angular/core';
import {CaffService, FileParameter} from 'src/app/api/app.generated';

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.scss']
})
export class UploaderComponent implements OnInit {

  tags: string[] = [];
  file: FileParameter = { data: '', fileName: '' };
  static showSuccessMessage: boolean = false;
  static showFailedMessage: boolean = false;
  isLoading: boolean = false;

  constructor(private api: CaffService) {}

  ngOnInit(): void {}


  uploadCaff(): void {
    this.isLoading = true;
    this.tags = this.tags.map(t => t.toLowerCase());
    this.api.uploadCaff(this.file, this.tags).subscribe(res => {
        UploaderComponent.showSuccessMessage = true;
        this.tags = [];
        this.file = { data: '', fileName: '' };
        this.isLoading = false;
        setTimeout(() => { UploaderComponent.showSuccessMessage = false;  } , 3000);
      },
      err => {
        this.isLoading = false;
        UploaderComponent.showFailedMessage = true;
        setTimeout(() => { UploaderComponent.showFailedMessage = false;  } , 3000);
      });
  }

  getSuccessEnabled() {
    return UploaderComponent.showSuccessMessage;
  }

  getFailedEnabled() {
    return UploaderComponent.showFailedMessage;
  }

  selectFile(event: any) {
    this.file = { data: event.target.files[0], fileName: 'new_caff' };
  }
}
