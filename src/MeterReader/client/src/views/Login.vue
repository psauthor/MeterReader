<template>
  <div class="login">
    <h1>Login</h1>
    <div class="text-left col-6 offset-3">
      <div class="alert alert-danger" v-if="error">{{ error }}</div>
      <form novalidate @submit.prevent="onLogin()">
        <div class="form-group">
          <label for="username">Username:</label>
          <input name="username" class="form-control" v-model="username" />
          <span>{{ usernameError }}</span>
        </div>
        <div class="form-group">
          <label for="password">Password:</label>
          <input
            name="password"
            type="password"
            class="form-control"
            v-model="password"
          />
          <span>{{ passwordError }}</span>
        </div>
        <div class="form-group">
          <input
            type="submit"
            class="btn btn-success"
            :disabled="!form.valid"
            value="Submit"
          />
        </div>
      </form>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from "vue";
import { useForm, useField } from "vee-validate";
import * as yup from "yup";
import store from "../store";
import router from "../router";

export default defineComponent({
  name: "login-view",
  setup() {

    const schema = yup.object({
      username: yup.string().required(),
      password: yup.string().required(),
    });

    const { meta: form } = useForm({
      validationSchema: schema,
    });

    const { value: username, errorMessage: usernameError } =
      useField("username");
    const { value: password, errorMessage: passwordError } =
      useField("password");
    const error = ref("");

    async function onLogin() {
      if (form.value.valid) {
        const result = await store.dispatch("login", {
          username: username.value,
          password: password.value,
        });
        if (result) {
          try {
            router.push("/");
          } catch {
            error.value = "FAILED";
          }
        } else {
          error.value = "Failed to login";
        }
      } else {
        error.value = "Validation Issue";
      }
    }

    return {
      username,
      password,
      usernameError,
      passwordError,
      error,
      onLogin,
      form,
    };
  },
});
</script>
