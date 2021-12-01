import { Component, Input } from '@angular/core';
import { faUser } from '@fortawesome/free-solid-svg-icons';
import { UserManagementService } from "../../services/user-management.service";

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent {
  faUser = faUser;

  @Input() activeLink: string = '';

  constructor(private userManagement: UserManagementService) { }

  getUsername(): string {
    return this.userManagement.getUsername();
  }

  logout(): void {
    this.userManagement.logout();
  }

}
