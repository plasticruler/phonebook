import Vue from 'vue'
import Router from 'vue-router'
import AuthGaurd from './auth-gaurd'
const Dashboard = () => import('@/views/Dashboard');
const Team = () => import('@/views/Team')
const PhoneBook = ()=> import( '@/views/PhoneBook')
const Join = () => import('@/views/Join')
const Profile = () => import('@/components/User/Profile')
const Signup = () => import('@/components/User/Signup')
const Signin = () => import('@/components/User/Signin')


Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Dashboard
    },
    {
      path:'/team',
      name:'team',
      component: Team
    },
    {
      path:'/phonebook',
      name:'phonebook',
      component:PhoneBook,
      beforeEnter:AuthGaurd
    },
    {
      path:'/join',
      name:'join',
      component:Join
    },
    {
      path: '/profile',
      name: 'Profile',
      component: Profile,
      beforeEnter: AuthGaurd
    },
    {
      path: '/signup',
      name: 'Signup',
      component: Signup
    },
    {
      path: '/signin',
      name: 'Signin',
      component: Signin
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
    }
  ]
})