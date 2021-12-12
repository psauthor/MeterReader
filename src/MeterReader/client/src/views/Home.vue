<template>
  <div class="row">
    <div class="col-12">
      <h2 class="text-center">Customers</h2>
      <div class="alert alert-warning" v-if="error">{{ error }}</div>
      <div class="col-8 offset-2 text-left">
        <div class="table-responsive table-sm">
          <table class="table">
            <thead>
              <tr>
                <th>Name</th>
                <th>Company</th>
                <th>City</th>
              </tr>
            </thead>
            <tbody v-for="c in customers" :key="c.id">
              <tr>
                <td>{{ c.name }}</td>
                <td>{{ c.companyName }}</td>
                <td>{{ c.address.cityTown }}</td>
              </tr>
              <tr
                v-for="r in c.readings"
              >
              <td>&nbsp;</td>
                <td>
                  <small>&nbsp;&nbsp;{{ date(r.readingDate, "hh:mm a") }}</small>
                </td>
                <td><em>{{ r.value }}</em></td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import store from "../store";
import { computed, onMounted } from "vue";
import filters from "../filters";

export default {
  setup() {
    const customers = computed(() => store.state.customers);
    const error = computed(() => store.state.error);

    onMounted(async () => await store.dispatch("loadCustomers"));

    return {
      customers,
      error,
      ...filters,
    };
  },
};
</script>
