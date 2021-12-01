import { Component } from '@angular/core';
import { CaffDto, CaffService, ICaffDto, UploadCaffDto } from './api/app.generated';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'cool';
  listFile = [{}];

  constructor(public caffService: CaffService) {

  }

  public fileChange(event: any, item: any) {
    item.binary = event;
    var r = new FileReader();
    var that = this;
    r.onload = function (e) {
      item.binary = r.result;
      let ar: any = item.binary;
      console.log(item.binary);
      let dto: UploadCaffDto = new UploadCaffDto();
      dto.caffString = ar;
      dto.tags = [];
      console.log(dto);
      that.caffService.uploadCaff(dto).subscribe(
        (result) => {
          console.log(result);
        },
        (err) => {
          console.log(err);
        }
      )
    };
    r.readAsBinaryString(event.target.files[0]);
  }
}
