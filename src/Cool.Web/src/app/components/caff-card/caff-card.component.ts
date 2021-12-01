import { Component, Input, OnInit } from '@angular/core';
import { CaffDto, CaffService } from 'src/app/api/app.generated';
import { getImage } from '../../utils/imageUtil';
import {TokenDecoderService} from "../../services/token-decoder.service";
import {getDateString} from "../../utils/dateTimeUtil";
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-caff-card',
  templateUrl: './caff-card.component.html',
  styleUrls: ['./caff-card.component.scss']
})
export class CaffCardComponent implements OnInit {

  @Input() caff: CaffDto = new CaffDto();
  imageSrc: string = '';
  newComment: string = '';

  constructor(private api: CaffService, private decoder: TokenDecoderService,) {}

  ngOnInit(): void {
    this.imageSrc = getImage(this.caff.previewBitmap);
  }

  downloadCaff(): void {
    this.api.downloadCaff(this.caff.id).subscribe(fileData => {
      saveAs(fileData!.data, fileData!.fileName)
    });
  }

  isAdmin() {
    return this.decoder.getPayload().role === 'User';
  }

  getDateString(timeStamp: Date) {
    return getDateString(timeStamp);
  }

  addComment() : void {
    this.api.addComment(this.caff.id, this.newComment).subscribe(r => {});
  }

  deleteComment(commentId: number): void {
    this.api.removeComment(commentId).subscribe(r => {});
  }

  deleteCaff(): void {
    this.api.deleteCaff(this.caff.id).subscribe(r => {

      }, error => {

    });
  }

  public onAddTag(ev: any) {
    console.log('tag added: value is ' + ev);
  }

  public onRemoveTag(ev: any) {
    console.log('tag removed: value is ' + ev);
  }
}
