import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class RoleGuardService implements CanActivate {
  constructor(private router: Router) { }

  canActivate(): boolean {
    const jwt = localStorage.getItem('token');
    if (!jwt) {
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
}
