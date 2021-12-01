import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-caff-modal',
  templateUrl: './caff-modal.component.html',
  styleUrls: ['./caff-modal.component.scss']
})
export class CaffModalComponent {

  @Input() title: string = '';
  @Input() modalId: string = '';

}
