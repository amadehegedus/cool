import { Component, OnInit } from '@angular/core';
import { CaffDto, TagDto } from 'src/app/api/app.generated';
import { TokenDecoderService } from 'src/app/services/token-decoder.service';

@Component({
  selector: 'app-browser',
  templateUrl: './browser.component.html',
  styleUrls: ['./browser.component.scss']
})
export class BrowserComponent implements OnInit {
  caffs: CaffDto[];
  filterOptions: string;

  constructor(private decoder: TokenDecoderService) {
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
    console.log(this.decoder.getPayload().role)
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
