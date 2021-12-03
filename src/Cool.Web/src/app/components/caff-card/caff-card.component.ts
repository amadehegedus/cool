import { Component, Input, OnInit } from '@angular/core';
import { CaffDto, CaffService } from 'src/app/api/app.generated';
import { getImage } from '../../utils/imageUtil';
import { getDateString } from "../../utils/dateTimeUtil";
import { saveAs } from 'file-saver';
import { faFileDownload, faUser } from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-caff-card',
  templateUrl: './caff-card.component.html',
  styleUrls: ['./caff-card.component.scss']
})
export class CaffCardComponent implements OnInit {

  @Input() caff: CaffDto = new CaffDto();
  imageSrc: string = '';
  faSave = faFileDownload;
  faUser = faUser;


  constructor(private api: CaffService) { }

  ngOnInit(): void {
    this.imageSrc = getImage(this.caff.previewBitmap);
  }

  downloadCaff(): void {
    this.api.downloadCaff(this.caff.id).subscribe((fileData) => {
      saveAs(fileData!.data, fileData!.fileName)
    })
  }

  getDateString(timeStamp: Date) {
    return getDateString(timeStamp);
  }

}
