import {getImage} from "./imageUtil";

describe('getImageUtil', () => {
  test('getImage should return converted string', () => {
    const imageInBytes = 'IMAGE_IN_BYTES';
    expect(getImage(imageInBytes)).toBe('data:image/png;base64,' + imageInBytes);
  });
  test('getImage should return placeholder image src if parameter is undefined or empty', () => {
    const imageInBytes = '';
    expect(getImage(imageInBytes)).toBe('../../../assets/caff_placeholder.jpg');
  });
});
