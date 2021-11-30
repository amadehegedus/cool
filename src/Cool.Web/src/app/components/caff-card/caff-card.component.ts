import { Component, Input, OnInit } from '@angular/core';
import { CaffDto } from 'src/app/api/app.generated';
import { getImage } from '../../utils/imageUtil';

@Component({
  selector: 'app-caff-card',
  templateUrl: './caff-card.component.html',
  styleUrls: ['./caff-card.component.scss']
})
export class CaffCardComponent implements OnInit {

  @Input() caff: CaffDto = new CaffDto();
  image: any;
  constructor() {
    getImage().then(img => this.image = img);
   }

  ngOnInit(): void {
  }

}
