import api from './axios'

export const turnosApi = {
    getAll: () => api.get('/turnos'),

    getById: (id) =>
        api.get(`/turnos/${id}`),

    create: (data) =>
        api.post('/turnos', data),

    cancelar: (id) =>
        api.post(`/turnos/${id}/cancelar`),

    marcarAusencia: (id) =>
        api.post(`/turnos/${id}/ausencia`),

    actualizarEstado: (id, data) =>
        api.put(`/turnos/${id}/estado`, data)
}