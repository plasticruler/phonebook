<template>
  <div class="dashboard">
    <h1 class="subheading grey--text">Contacts</h1>
    <v-container fluid class="my-5">      
      <v-layout row justify-start class="mb-3">
        <v-btn small flat color="grey" @click="sortBy('title')">
          <v-icon small left>person</v-icon>
          <span class="caption text-lowercase">First Name</span>
        </v-btn>
        <v-btn small flat color="grey" @click="sortBy('person')">
          <v-icon small left>person</v-icon>
          <span class="caption text-lowercase">Surname</span>
        </v-btn>
      </v-layout>

      <v-card flat v-for="contact in contacts" :key="contact.id">
        <v-layout row wrap :class="`pa-3 project ${contact.firstName}`">
          <v-flex xs12 md5>
            <div class="caption grey--text">First name</div>
            <div>{{ contact.firstName }}</div>
          </v-flex>
          <v-flex xs6 sm4 md2>
            <div class="caption grey--text">Surname</div>
            <div>{{ contact.surname }}</div>
          </v-flex>
          <v-flex xs21 sm4 md1>
            <v-btn flat icon color="pink" @click="deleteItem(contact.id)">
              <v-icon>add</v-icon>
            </v-btn>

          </v-flex>
      <v-flex xs6 sm4 md3>
        <div class="right">
          <v-btn flat icon color="pink" @click="deleteItem(contact.id)">
              <v-icon>delete</v-icon>
            </v-btn>
        </div>
      </v-flex>
          <v-flex xs2 sm4 md2>
            <div class="right">
              <v-chip
                small
                :class="`${contact.phoneNumbers.length} white--text my-2 caption`"
              >{{ contact.phoneNumbers.length }}</v-chip>
            </div>
          </v-flex>
        </v-layout>
        <v-divider></v-divider>
      </v-card>
    </v-container>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      contacts: []
    };
  },
  methods: {
    deleteItem(id){
      this.contacts = this.contacts.filter(contact=>{
        return contact.id != id;
      })
      //delete from the db
      var url = 'https://localhost:5001/api/1.0/contacts/'+id;
      axios.delete(url).then(result=>{
        console.log(result);
      })
    },
    sortBy(prop) {
      this.contact.sort((a, b) => (a[prop] < b[prop] ? -1 : 1));
    }
  },
  created() {
        //load initial data jhere
        var url = 'https://localhost:5001/api/1.0/contacts/user/102';    
        axios.get(url)
          .then(result => {            
            this.contacts = result.data;
          })

  }
}
</script>
<style>
.project.complete {
  border-left: 4px solid #3cd1c2;
}
.project.ongoing {
  border-left: 4px solid orange;
}
.project.overdue {
  border-left: 4px solid tomato;
}
.v-chip.complete {
  background: #3cd1c2;
}
.v-chip.ongoing {
  background: #ffaa2c;
}
.v-chip.overdue {
  background: #f83e70;
}
</style>
