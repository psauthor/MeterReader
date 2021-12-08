import { createRouter, RouteRecordRaw, createWebHashHistory } from 'vue-router';
import Home from '../views/Home.vue';
import store from "../store";

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'home',
    component: Home
  },
  {
    path: '/login',
    name: 'login',
    component: () => import('../views/Login.vue')
  }
];

const router = createRouter({
  routes,
  history: createWebHashHistory()
});

router.beforeEach(async (to, from, next) => {
  let loggedIn = store.getters.isLoggedIn;
  if (to.name === 'login' || loggedIn) {
    next();
  } else {
    next("/login");
  }
});

export default router;
