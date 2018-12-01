import Vue from 'vue'
import App from './App.vue'
import router from './router'
import MuseUI from 'muse-ui';
import 'muse-ui/dist/muse-ui.css';
import DateFilter from './shell/DateFilter'

Vue.config.productionTip = false
Vue.use(MuseUI);
Vue.filter('date', DateFilter );

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
