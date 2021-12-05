import { TestBed } from '@angular/core/testing';
import {UserManagementService} from "./user-management.service";


let returnObj : any = { role: 'User' };
jest.mock('jwt-decode', () => jest.fn().mockImplementation(() => { return returnObj } ));

describe('UserManagementService', () => {
  let service: UserManagementService;
  const original = window.location;

  function setup() {
    TestBed.configureTestingModule({
      providers: [UserManagementService],
    });
    service = TestBed.inject(UserManagementService);
  }

  beforeAll(() => {
    Object.defineProperty(window, 'location', {
      configurable: true,
      value: { reload: jest.fn() },
    });
  });

  afterAll(() => {
    Object.defineProperty(window, 'location', { configurable: true, value: original });
  });

  test('getPayload should return null when localstorage was not set', () => {
    setup();
    localStorage.clear();
    expect(service.getPayload()).toBe(null);
  });

  test('getPayload should return decodedToken when localstorage was set', () => {
    returnObj = 'DECODED';
    setup();
    localStorage.setItem('token', 'MOCKED_TOKEN');
    expect(service.getPayload()).toBe( 'DECODED');
  });

  test('isAdmin should return false if role is User', () => {
    returnObj = { role: 'User' };
    setup();
    localStorage.setItem('token', 'MOCKED_TOKEN');
    expect(service.isAdmin()).toBeFalsy();
  });

  test('isAdmin should return true if role is Admin', () => {
    returnObj = { role: 'Admin' };
    setup();
    localStorage.setItem('token', 'MOCKED_TOKEN');
    expect(service.isAdmin()).toBeTruthy();
  });

  test('getUserName should return decoded nameid', () => {
    returnObj = { nameid: 'nameid' };
    setup();
    localStorage.setItem('token', 'MOCKED_TOKEN');
    expect(service.getUsername()).toBe('nameid');
  });

  test('logout should remove token from localstorage and should reload location', () => {
    localStorage.setItem('token', 'MOCKED_TOKEN');
    setup();
    service.logout();
    expect(localStorage.getItem('token')).toBe(null);
    expect(window.location.reload).toHaveBeenCalled();
  });

});
