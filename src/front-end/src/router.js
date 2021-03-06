import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import ProductCreate from './components/products/ProductCreate.vue'
import ProductEdit from './components/products/ProductEdit.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/products',
      name: 'products',
      component: () => import('./views/Products.vue')
    }
    ,
    {
      path: '/productcreate',
      name: 'ProductCreate',
      component: ProductCreate
    },
    {
      path: '/productedit/:id',
      name: 'ProductEdit',
      component: ProductEdit
    }
  ]
})
