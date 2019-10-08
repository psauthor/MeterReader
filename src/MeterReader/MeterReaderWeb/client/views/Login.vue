<template>
  <div class="login">
    <h1>Login</h1>
    <div class="text-left col-6 offset-3">
      <validation-observer ref="observer" v-slot="{ invalid }">
        <div class="alert alert-danger" v-if="error">{{ error }}</div>
        <form novalidate @submit.prevent="onLogin()">
          <div class="form-group">
            <label for="username">Username:</label>
            <validation-provider name="username" rules="required" v-slot="{ errors }">
              <input name="username" class="form-control" v-model="username" />
              <span>{{ errors[0] }}</span>
            </validation-provider>
          </div>
          <div class="form-group">
            <label for="password">Password:</label>
            <validation-provider name="password" rules="required" v-slot="{ errors }">
              <input name="password" type="password" class="form-control" v-model="password" />
              <span>{{ errors[0] }}</span>
            </validation-provider>
          </div>
          <div class="form-group">
            <input type="submit" class="btn btn-success" :disabled="invalid" value="Submit" />
          </div>
        </form>
      </validation-observer>
    </div>
  </div>
</template>
<script>
  import { ValidationProvider, ValidationObserver } from "vee-validate/dist/vee-validate.full";

  export default {
    data: () => {
      return { username: "", password: "", error: "" };
    },
    components: {
      ValidationProvider,
      ValidationObserver
    },
    methods: {
      async onLogin() {
        const isValid = await this.$refs.observer.validate();
        if (isValid) {
          const result = await this.$store.dispatch("login", { username: this.username, password: this.password });
          if (result) {
            try {
              this.$router.push("/");
            } catch {
              this.error = "FAILED";
            }
          } else {
            this.error = "Failed to login";
          }
        } else {
          this.error = "Validation Issue";
        }


      }
    }
  }
</script>
