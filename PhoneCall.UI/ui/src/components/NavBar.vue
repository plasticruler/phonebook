<template>
<nav>
    <v-snackbar v-model="contactAdded" :timeout="4000" top color="success">
      <span>Awesome! You added a new phone number.</span>
      <v-btn color="white" flat @click="contactAdded = false">Close</v-btn>
    </v-snackbar>
    <v-toolbar app>
        <v-toolbar-side-icon class="grey--text" @click="drawer = !drawer"></v-toolbar-side-icon>

        <v-toolbar-title class="text-uppercase grey--text">
            <span class="font-weight-light">PhoneCall</span>
            <span>UI</span>
        </v-toolbar-title>
   
        <v-spacer></v-spacer>
        <v-btn v-if="IsUserAuthenticated" flat color="grey" @click="logout">
            <span>Signout {{user.email}}</span>
            <v-icon>exit_to_app</v-icon>
            <!-- <v-icon right>exit_to_app</v-icon> -->
        </v-btn>
        <div v-else>
            <v-btn flat color="grey" @click="login" >
                <span>Login</span>
                <v-icon>how_to_reg</v-icon>
            </v-btn>
        </div>
        

    </v-toolbar>
    <v-navigation-drawer v-model="drawer" app class="indigo"  v-if="user">
        <v-layout column align-center>
            <v-flex class="mt-5">
                <v-avatar size="100">
                    <img class="text-lg-center" src="a.png">
                </v-avatar>
                <p class="white--text subheading mt-1">{{user.email}}</p>
            </v-flex>
            <v-flex class="mt-4 mb-3">                
                <ContactEdit @contactAdded="contactAdded=true"/>
            </v-flex>
        </v-layout>
        <v-list>
            <v-list-tile v-for="link in links" :key="link.text" router :to="link.route">
                <v-list-tile-action>
                    <v-icon class="white--text">{{link.icon}}</v-icon>
                </v-list-tile-action>
                <v-list-tile-content>
                    <v-list-tile-title class="white--text">
                        {{link.text}}
                    </v-list-tile-title>
                </v-list-tile-content>
            </v-list-tile>
        </v-list>
    </v-navigation-drawer>
</nav>
   
</template>

<script>
import Popup from './Popup'
import ContactEdit from './ContactEdit'
import {user} from '@/store/user' 
export default {
components:{
    Popup,
    ContactEdit
},
  data() {
    return {
      drawer: false,
      links:[
          {icon:'dashboard', text:'Dashboard', route:'/'},
          {icon:'folder', text:'My Phone Book', route:'/phonebook'}
      ],
      contactAdded:false
    };
  },
  methods:{
    logout(){
      this.$store.dispatch('logout')
      this.$router.push('/')
    },
    login(){
        this.$router.push('/phonebook')
    }
  },
  computed: {
    IsUserAuthenticated(){
      return this.$store.getters.user !== null && this.$store.getters.user !== undefined
    },
    user(){
        return this.$store.getters.user;
    }
  }
};
</script>