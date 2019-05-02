<template>
  <v-dialog max-width="600px" v-model="dialog">
    <v-btn v-if="IsUserAuthenticated" flat slot="activator" class="success">Edit Phone Number</v-btn>
    <v-card>
      <v-card-title>
        <h2>Edit Phone Number</h2>
      </v-card-title>
      <v-card-text>
        <v-form class="px-3" ref="form">
          <v-text-field v-model="PhoneNumber" label="Number" prepend-icon="phone" :rules="phoneNumberValidation"
          ></v-text-field>
          <v-combobox
          v-model="PhoneNumberType"
          :items="PhoneNumberTypes"
          label="Select a phone number type"
        ></v-combobox>
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
      PhoneNumberTypes:['Home','Work','Mobile_1','Mobile_2'],    
      PhoneNumber:'',  
      UserID: 102, //hardcoded
      ContactID : 401, //rami
      menu: false,
      phoneNumberValidation:[
            v => new RegExp("^[0-9]{10}$").test(v)||"Phone number must be in form 0112341111" //overly simplified
      ],
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
        const phoneNumber = {
          UserID: 101,
          ContactId: 401,
          Number: this.PhoneNumber,
          PhoneNumberType : this.PhoneNumberTypes.indexOf(this.PhoneNumberType)+1
        };
        console.log(phoneNumber);     
        var url = "https://localhost:5001/api/1.0/phonenumbers/";
        axios
          .post(url, phoneNumber)
          .then(()=> {      
                
            this.$emit("phoneNumberAdded");
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