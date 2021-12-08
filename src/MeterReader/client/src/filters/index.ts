import moment from 'moment';

export default {
  date(value: Date | string, fmt: string) {
    if (!fmt) fmt = "L";
    return moment(value).format(fmt);
  }
}