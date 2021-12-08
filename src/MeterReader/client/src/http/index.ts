import axios from "axios";
import store from "../store";

const baseUrl = "";

export function http() {
  return axios.create({
    baseURL: baseUrl
  });
}

export function secured() {
  return axios.create({
    baseURL: baseUrl,
    headers: { "Authorization": `Bearer ${store.state.auth.token}` }
  })
} 
