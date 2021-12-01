import { Component } from '@angular/core';
import { CaffDto, CaffService, FileParameter, ICaffDto } from './api/app.generated';

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

  selectFile(event: any) {
    alert("select");
    let f: FileParameter = { data: event.target.files[0], fileName: "barmi" };
    this.caffService.uploadCaff(f, []).subscribe(res => {
      console.log(res);
    },
      err => {
        console.log(err);
      });
  }
}
