import { TestBed } from '@angular/core/testing';

import {UserManagementService} from "./user-management.service";

import 'jasmine';


describe('TokenDecoderService', () => {
  let localStore: any;
  let service: UserManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserManagementService);
    localStore = {};

    spyOn(localStorage, 'getItem').and.callFake((key) =>
      key in localStore ? localStore[key] : null
    );
    spyOn(localStorage, 'setItem').and.callFake(
      (key, value) => (localStore[key] = value + '')
    );
    spyOn(localStorage, 'clear').and.callFake(() => (localStore = {}));
  });

  it('', () => {
    const result = service.getPayload();
    expect(result).toBe('');
  });

});
