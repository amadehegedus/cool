import { Component, OnInit } from '@angular/core';
import { CaffDto, TagDto } from 'src/app/api/app.generated';
import { UserManagementService } from 'src/app/services/user-management.service';

@Component({
  selector: 'app-browser',
  templateUrl: './browser.component.html',
  styleUrls: ['./browser.component.scss']
})
export class BrowserComponent implements OnInit {
  caffs: CaffDto[];
  filterOptions: string;

  constructor(private userManagement: UserManagementService) {
    this.filterOptions = '';
    this.caffs = [];
    this.caffs = [
      new CaffDto({
        id: 0,
        creationTime: new Date(),
        creator: 'hello',
        tags: [
          new TagDto({ id: 0, text: 'asd' })
        ]
      }),
      new CaffDto({
        id: 1,
        creationTime: new Date(),
        creator: 'hello',
        tags: [
          new TagDto({ id: 0, text: 'bfc' })
        ]
      }),
      new CaffDto({
        id: 0,
        creationTime: new Date(),
        creator: 'hello',
        tags: [
          new TagDto({ id: 0, text: 'asd' })
        ]
      }),
      new CaffDto({
        id: 1,
        creationTime: new Date(),
        creator: 'hello',
        tags: [
          new TagDto({ id: 0, text: 'bfc' })
        ]
      }),
      new CaffDto({
        id: 0,
        creationTime: new Date(),
        creator: 'hello',
        tags: [
          new TagDto({ id: 0, text: 'asd' })
        ]
      }),
      new CaffDto({
        id: 1,
        creationTime: new Date(),
        creator: 'hello',
        tags: [
          new TagDto({ id: 0, text: 'bfc' })
        ]
      }),
      new CaffDto({
        id: 0,
        creationTime: new Date(),
        creator: 'hello',
        tags: [
          new TagDto({ id: 0, text: 'asd' })
        ]
      }),
      new CaffDto({
        id: 1,
        creationTime: new Date(),
        creator: 'hello',
        tags: [
          new TagDto({ id: 0, text: 'bfc' })
        ]
      }),
      new CaffDto({
        id: 0,
        creationTime: new Date(),
        creator: 'hello',
        tags: [
          new TagDto({ id: 0, text: 'asd' })
        ]
      }),
      new CaffDto({
        id: 1,
        creationTime: new Date(),
        creator: 'hello',
        tags: [
          new TagDto({ id: 0, text: 'bfc' })
        ]
      }),
    ]
  }

  ngOnInit(): void {
    console.log(this.userManagement.getPayload().role)
  }

  filterCaffs(): CaffDto[] {
    if (this.filterOptions === '') {
      return this.caffs;
    }
    return this.caffs.filter(c => {
      if (c.tags?.filter(ct => ct.text === this.filterOptions || ct.text?.startsWith(this.filterOptions)).length === 0) {
        return false;
      }
      return true;
    });
  }
}
