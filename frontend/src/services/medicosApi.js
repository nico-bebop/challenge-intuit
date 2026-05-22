import api from './axios'

export const medicosApi = {
    getAll: () => api.get('/medicos')
}