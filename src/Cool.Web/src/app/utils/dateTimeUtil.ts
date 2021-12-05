export const getDateString = (date: Date) => {
  return date.getFullYear() + '-' +
    to2digits(date.getMonth()+1) + '-' +
    to2digits(date.getDate()) + ' ' +
    to2digits(date.getHours()) + ':' +
    to2digits(date.getMinutes());
};

const to2digits = (n: number) => {
  return n > 9 ? "" + n: "0" + n;
};
