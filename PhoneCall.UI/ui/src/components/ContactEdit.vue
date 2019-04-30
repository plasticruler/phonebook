<template>
  <v-dialog max-width="600px" v-model="dialog">
    <v-btn v-if="IsUserAuthenticated" flat slot="activator" class="success">Edit Contact</v-btn>
    <v-card>
      <v-card-title>
        <h2>Edit contact</h2>
      </v-card-title>
      <v-card-text>
        <v-form class="px-3" ref="form">
          <v-text-field
            v-model="FirstName"
            label="FirstName"
            prepend-icon="folder"
            :rules="inputRules"
          ></v-text-field>
          <v-text-field v-model="Surname" label="Surname" prepend-icon="edit" :rules="inputRules"></v-text-field>

          <v-spacer></v-spacer>

          <v-btn flat @click="submit" class="success mx-0 mt-3" :loading="loading">Save</v-btn>
        </v-form>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>
<script>
import axios from "axios"
export default {
  data() {
    return {
      FirstName: "",
      Surname: "",
      UserId: 102, //hardcoded
      due: null,
      menu: false,
      inputRules: [
        v => !!v || "This field is required.",
        v => v.length >= 3 || "Minimum length of this field is 3 characters."
      ],
      loading: false,
      dialog: false
    };
  },
  methods: {   
    
    submit() {
      if (this.$refs.form.validate()) {
        this.loading = true;
        const contact = {
          UserID: 102,
          FirstName: this.FirstName,
          Surname: this.Surname
        };     
        var url = "https://localhost:5001/api/1.0/contacts/";
        axios
          .post(url, contact)
          .then(()=> {            
            this.$emit("contactAdded");
          })
          .catch(function(error) {              
            // handle error 
            console.log(error);
          })
          .then(()=> {
              this.loading=false;
            // always executed
          });
      }
    }
  },
  computed: {    
    IsUserAuthenticated() {
      return (
        this.$store.getters.user !== null &&
        this.$store.getters.user !== undefined
      );
    }
  }
};
</script>