import api from './axios'

export const pacientesApi = {
    getAll: () => api.get('/pacientes'),

    getAllIncludingInactive: () => api.get('/pacientes/all'),

    getById: (id) =>
        api.get(`/pacientes/${id}`),

    create: (data) =>
        api.post('/pacientes', data),

    update: (id, data) =>
        api.put(`/pacientes/${id}`, data),

    delete: (id) =>
        api.delete(`/pacientes/${id}`),

    activate: (id) =>
        api.put(`/pacientes/${id}/activar`)
}