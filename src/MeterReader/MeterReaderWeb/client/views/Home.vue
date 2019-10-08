<template>
  <div class="row">
    <div class="col-12">
      <h2 class="text-center">Customers</h2>
      <div class="alert alert-warning" v-if="error">{{ error }}</div>
      <div class="table-responsive">
        <table class="table col-8 offset-2 text-left w-100">
          <thead>
            <tr>
              <th></th>
              <th>Name</th>
              <th>Company</th>
              <th>City</th>
            </tr>
          </thead>
          <tbody v-for="c in customers">
            <tr>
              <td><span class="fas fa-plus" data-toggle="collapse" :data-target="`#customer-readings-${c.id}`"></span></td>
              <td>{{ c.name }}</td>
              <td>{{ c.companyName }}</td>
              <td>{{ c.address.cityTown }}</td>
            </tr>
            <tr v-for="r in c.readings" class="collapse" :id="`customer-readings-${c.id}`">
              <td></td>
              <td colspan="2">{{ r.readingDate | date('MM/DD/YYYY hh:mm a') }}</td>
              <td>{{ r.value }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>
<script>
  import { mapState, mapActions } from "vuex";
  export default {
    computed: mapState(["customers", "error"]),
    async created() {
      await this.loadCustomers();
    },
    methods: {
      ...mapActions(["loadCustomers"])
    }
  }
</script>