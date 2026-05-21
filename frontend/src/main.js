import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import './styles/main.css'
import ErrorHandler from './plugins/errorHandler'

const app = createApp(App)

app.use(router)
app.use(ErrorHandler)

app.mount('#app')