import { Component, OnInit } from '@angular/core';
import {CaffDto, CaffService, CommentDto, TagDto} from 'src/app/api/app.generated';
import { UserManagementService } from 'src/app/services/user-management.service';

@Component({
  selector: 'app-browser',
  templateUrl: './browser.component.html',
  styleUrls: ['./browser.component.scss']
})
export class BrowserComponent implements OnInit {
  caffs: CaffDto[];
  filterOptions: string;

  constructor(private userManagement: UserManagementService, private api: CaffService) {
    this.filterOptions = '';
    this.caffs = [];
    this.caffs = [
      new CaffDto({
        id: 3,
        creationTime: new Date(),
        creator: 'hello',
        previewBitmap: 'iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==',
        comments: [
          new CommentDto({id: 0, message: 'HELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLOHELLO', timeStamp: new Date(), userName: 'pista'}),
          new CommentDto({id: 0, message: 'HELLO1', timeStamp: new Date(), userName: 'pista1'}),
          new CommentDto({id: 0, message: 'HELLO2', timeStamp: new Date(), userName: 'pist2a'}),
          new CommentDto({id: 0, message: 'HELLO', timeStamp: new Date(), userName: 'pista'}),
          new CommentDto({id: 0, message: 'HELLO1', timeStamp: new Date(), userName: 'pista1'}),
          new CommentDto({id: 0, message: 'HELLO2', timeStamp: new Date(), userName: 'pist2a'}),
          new CommentDto({id: 0, message: 'HELLO', timeStamp: new Date(), userName: 'pista'}),
          new CommentDto({id: 0, message: 'HELLO1', timeStamp: new Date(), userName: 'pista1'}),
          new CommentDto({id: 0, message: 'HELLO2', timeStamp: new Date(), userName: 'pist2a'}),
          new CommentDto({id: 0, message: 'HELLO', timeStamp: new Date(), userName: 'pista'}),
          new CommentDto({id: 0, message: 'HELLO1', timeStamp: new Date(), userName: 'pista1'}),
          new CommentDto({id: 0, message: 'HELLO2', timeStamp: new Date(), userName: 'pist2a'}),
        ],
        tags: [
          new TagDto({id: 0, text: 'asd'}),
          new TagDto({id: 0, text: 'fsaf'}),
          new TagDto({id: 0, text: 'vsav'}),
          new TagDto({id: 0, text: 'asd'}),
          new TagDto({id: 0, text: 'fsaf'}),
          new TagDto({id: 0, text: 'vsav'}),
          new TagDto({id: 0, text: 'asd'}),
          new TagDto({id: 0, text: 'fsaf'}),
          new TagDto({id: 0, text: 'vsav'}),
          new TagDto({id: 0, text: 'asd'}),
          new TagDto({id: 0, text: 'fsaf'}),
          new TagDto({id: 0, text: 'vsav'}),
        ]
      })
    ];
    //this.api.downloadCaff(3).subscribe(result => console.log(result));
    //this.api.getAllCaffs().subscribe(result => console.log(result));
  }

  ngOnInit(): void {  }

  filterCaffs(): CaffDto[] {
    if (this.filterOptions === '') {
      return this.caffs;
    }
    return this.caffs.filter(c => {
      return c.tags?.filter(ct => ct.text === this.filterOptions || ct.text?.startsWith(this.filterOptions)).length !== 0;
    });
  }
}
