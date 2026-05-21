export default {
  install(app) {
    app.config.globalProperties.$error = (error) => {
      const msg =
        error.response?.data?.message ||
        'Error al procesar la solicitud'

      alert(msg)
    }
  }
}