import { createStore } from 'vuex';
import moment from 'moment';
import { Customer } from "../models";
import { http, secured } from "../http";


export default createStore({
  state: {
    auth: {
      token: "",
      expiration: new Date()
    },
    customers: new Array<Customer>(),
    error: ""
  },
  mutations: {
    setAuth: (state, auth) => state.auth = auth,
    setError: (state, msg) => state.error = msg,
    setCustomers: (state, custs) => state.customers = custs
  },
  actions: {
    async login({ commit }, creds) {
      try {
        const result = await http().post("/api/token", { username: creds.username, passcode: creds.password });
        commit("setAuth", result.data);
        return true;
      } catch (e) {
        return false;
      }
    },
    async loadCustomers({ commit }) {
      try {
        const result = await secured().get("/api/customers");
        commit("setCustomers", result.data);
      } catch (e) {
        commit("setError", "Failed to get customers");
      }
    }
  },
  getters: {
    isLoggedIn(state) {
      return state.auth.token  && moment().isBefore(state.auth.expiration);
    },
  }
});
