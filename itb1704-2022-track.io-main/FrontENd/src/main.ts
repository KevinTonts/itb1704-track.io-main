import { createApp } from 'vue';
import App from './App.vue';
import 'virtual:windi.css';
import PrimeVue from 'primevue/config';
import { createPinia } from 'pinia';
import 'primevue/resources/themes/saga-blue/theme.css'; //theme
import 'primevue/resources/primevue.min.css'; //core css
import 'primeicons/primeicons.css'; //icons
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import router from './router';
import { setApiUrl } from './modules/api';
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';


const getRuntimeConf = async () => {
  const runtimeConf = await fetch('/config/runtime-config.json');
  return await runtimeConf.json();
};

getRuntimeConf().then((json) => {
  setApiUrl(json.API_URL);

  let app = createApp(App);

  app.use(createPinia());
  app.use(PrimeVue);
  app.use(router);

  app.component('DataTable', DataTable);
  app.component('Column', Column);
  app.component('Datepicker', Datepicker);

  app.mount('#app');
});
