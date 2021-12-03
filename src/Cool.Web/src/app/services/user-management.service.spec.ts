import { TestBed } from '@angular/core/testing';

import {UserManagementService} from "./user-management.service";
import {mock, verify, when} from "ts-mockito";


function setup() {
  const userManagementService = new UserManagementService();
  return { userManagementService };
}

describe('TokenDecoderService', () => {
  beforeEach(() => {
    let store:any = {
      token: 'asd',
    };

    spyOn(localStorage, 'getItem').and.callFake( (key:string):string | null => {
      return store[key] || null;
    });
    spyOn(localStorage, 'removeItem').and.callFake((key:string):void =>  {
      delete store[key];
    });
    spyOn(localStorage, 'setItem').and.callFake((key:string, value:string):string =>  {
      return store[key] = <string>value;
    });
    spyOn(localStorage, 'clear').and.callFake(() =>  {
      store = {};
    });
  });

  it('test getPayload', () => {
    
    const userManagementService = new UserManagementService();
    const result = userManagementService.getPayload();
    expect(result).toBe('myToken');
    //expect(jwtDecode).toHaveBeenCalledWith('myToken');
  });

});
