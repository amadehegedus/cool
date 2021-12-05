import {getDateString} from "./dateTimeUtil";

describe('dateTimeUtils', () => {
  test('covert date to correct string', () => {
    expect(getDateString(new Date('11/12/2010 2:39:28 PM'))).toBe('2010-11-12 14:39');
  });
  test('covert single digits date elements to correct string', () => {
    expect(getDateString(new Date('2/4/2010 2:9:28 AM'))).toBe('2010-02-04 02:09');
  });
});
