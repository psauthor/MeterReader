import Vue from 'vue';
import Router from 'vue-router';
import Home from './views/Home.vue';
import store from "./store";

Vue.use(Router);

var router = new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('./views/Login.vue')
    }
  ]
});

router.beforeEach(async (to, from, next) => {
  let loggedIn = await store.dispatch("isLoggedIn");
  if (to.name === 'login' || loggedIn) {
    next();
  } else {
    next("/login");
  }
});

export default router;
