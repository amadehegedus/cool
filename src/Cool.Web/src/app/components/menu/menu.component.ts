import {Component, Input, OnInit} from '@angular/core';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import {UserManagementService} from "../../services/user-management.service";

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  faUser = faUser;

  @Input() activeLink: string = '';

  constructor(private userManagement: UserManagementService) { }

  ngOnInit(): void {
  }

  logout(): void {

  }

  getUsername(): string {
    const userId: string = this.userManagement.getPayload().nameid;
    return userId? userId : 'username';
  }

}
