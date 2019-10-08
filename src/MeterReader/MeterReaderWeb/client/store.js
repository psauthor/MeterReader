import Vue from 'vue';
import Vuex from 'vuex';
import axios from 'axios';
import moment from "moment";
import VuexPersist from 'vuex-persist'

Vue.use(Vuex); 

const vuexPersist = new VuexPersist({
  key: 'meter-reader-state',
  storage: window.localStorage
});

export default new Vuex.Store({
  plugins: [vuexPersist.plugin],
  state: {
    auth: {
      token: "",
      expiration: new Date()
    },
    customers: [],
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
        var result = await axios.post("/api/security/token", { username: creds.username, passcode: creds.password });
        commit("setAuth", result.data);
        return true;
      } catch (e) {
        return false;
      }
    },
    isLoggedIn({ state }) {
      return state.auth.token  && moment().isBefore(state.auth.expiration);
    },
    async loadCustomers({ state, commit }) {
      try {
        var result = await axios.get("/api/customers", {
          headers: { "Authorization": `Bearer ${state.auth.token}` }
        });

        commit("setCustomers", result.data);

      } catch (e) {
        commit("setError", "Failed to get customers");
      }
    }
  }
});
