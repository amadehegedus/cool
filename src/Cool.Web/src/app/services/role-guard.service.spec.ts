import { TestBed } from '@angular/core/testing';
import { RoleGuardService } from './role-guard.service';
import {Router} from "@angular/router";
import {RouterTestingModule} from "@angular/router/testing";
import {of} from "rxjs";

describe('RoleGuardService', () => {
  let service: RoleGuardService;
  let router;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule.withRoutes([]),
      ],
      providers: [RoleGuardService],
    });
    service = TestBed.inject(RoleGuardService);
  });

  test('canActivate should redirect to login page and return false when jwt was not set in localstorage', () => {
    router = TestBed.inject(Router);
    jest.spyOn(router, 'navigate').mockImplementation(() => of(true).toPromise());
    localStorage.clear();
    expect(service.canActivate()).toBeFalsy();
    expect(router.navigate).toHaveBeenCalledWith(["login"]);
  });

  test('canActivate should return true, if jwt was set in localstorage', () => {
    router = TestBed.inject(Router);
    jest.spyOn(router, 'navigate').mockImplementation(() => of(true).toPromise());
    localStorage.setItem('token', 'MOCKED_TOKEN');
    expect(service.canActivate()).toBeTruthy();
    expect(router.navigate).not.toHaveBeenCalled();
  });
});
