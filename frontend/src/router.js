import { createRouter, createWebHistory } from 'vue-router'
import RequestList from './pages/RequestList.vue'
import RequestNew from './pages/RequestNew.vue'
import Login from './pages/Login.vue'

export const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/',        name: 'list',  component: RequestList },
    { path: '/new',     name: 'new',   component: RequestNew },
    { path: '/login',   name: 'login', component: Login }
  ]
})