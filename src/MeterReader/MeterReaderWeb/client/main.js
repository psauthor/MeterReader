import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import filters from "./filters";

Vue.config.productionTip = false;
filters();

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('theApp');
