import Vue from "vue";
import moment from "moment";

export default function () {
  Vue.filter("date", (value, fmt) => {
    if (!fmt) fmt = "L";
    return moment(value).format(fmt);
  });
}