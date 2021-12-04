import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserManagementService } from '../services/user-management.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor {
  constructor(private userManagementService: UserManagementService, private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const jwtToken = localStorage.getItem('token');
    if (jwtToken) {
      let expDate: Date = new Date(this.userManagementService.getPayload().exp * 1000);
      if (expDate <= new Date()) {
        this.router.navigate(['login']);
      }
      req = req.clone({
        setHeaders: {
          Authorization: `Bearer ${jwtToken}`,
        },
      });
    }
    return next.handle(req);
  }
}
