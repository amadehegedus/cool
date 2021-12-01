import { Injectable } from '@angular/core';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class UserManagementService {

  constructor() { }

  public getPayload(): any {
    let token: string | null = localStorage.getItem('token');
    if (!token)
      return null;
    return jwt_decode(token);
  }

  public isAdmin(): boolean {
    return this.getPayload().role === 'User';
  }
}
