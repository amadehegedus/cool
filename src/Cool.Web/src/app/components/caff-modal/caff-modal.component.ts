import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-caff-modal',
  templateUrl: './caff-modal.component.html',
  styleUrls: ['./caff-modal.component.scss']
})
export class CaffModalComponent implements OnInit {

  @Input() title: string = '';
  @Input() modalId: string = '';
  constructor() { }

  ngOnInit(): void {
  }

}
