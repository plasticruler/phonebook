import Vue from 'vue'
import './plugins/vuetify'
const  App  = () => import('./App')
import router from './router/router'
import {store} from './store/'
/*
import Axios from 'axios';
Axios.interceptors.request(function(config){
  store.commit('spinnerStatus',true)
  return config;
}, function(error){
  return Promise.reject(error)
})
*/
const AlertCmp = () => import('./components/Shared/Alert.vue')

Vue.config.productionTip = false
Vue.component('app-alert', AlertCmp)

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
