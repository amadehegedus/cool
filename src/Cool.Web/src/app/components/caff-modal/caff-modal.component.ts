import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-caff-modal',
  templateUrl: './caff-modal.component.html',
  styleUrls: []
})
export class CaffModalComponent {

  @Input() title: string = '';
  @Input() modalId: string = '';

}
