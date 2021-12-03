const imgType = "data:image/png;base64,";

export const getImage = (byteArray: string | undefined) => {
  if(byteArray && byteArray !== '') {
    return imgType + byteArray;
  }
  return '../../../assets/caff_placeholder.jpg';
};
