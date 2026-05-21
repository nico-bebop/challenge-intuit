import api from './axios'

export const sucursalesApi = {
    getAll: () => api.get('/sucursales')
}