<template>
  <div>
    <h2>Pacientes</h2>
    <table v-if="pacientes.length">
      <thead>
        <tr>
          <th>#</th>
          <th>Nombre</th>
          <th>DNI</th>
          <th>Email</th>
          <th>Teléfono</th>
          <th>No-shows</th>
          <th>Bloqueado</th>
          <th>Activo</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="p in pacientes" :key="p.id">
          <td>{{ p.id }}</td>
          <td>{{ p.nombreCompleto }}</td>
          <td>{{ p.dni }}</td>
          <td>{{ p.email }}</td>
          <td>{{ p.telefono }}</td>
          <td>{{ p.noShowCount }}</td>
          <td>
            <span v-if="p.bloqueado" style="color: #d32f2f; font-weight: 600">Sí</span>
            <span v-else style="color: #388e3c">No</span>
          </td>
          <td>
            <span v-if="p.isActive" style="color: #d32f2f; font-weight: 600">Sí</span>
            <span v-else style="color: #388e3c">No</span>
          </td>          
          <td> 
           <button v-if="p.isActive" class="btn-danger" @click="eliminar(p.id)">Eliminar</button>
           <button v-else class="btn-primary" @click="activar(p.id)">Activar</button>
          </td>
        </tr>
      </tbody>
    </table>
    <p v-else>No hay pacientes registrados.</p>
  </div>
</template>

<script>
import { pacientesApi } from '../services/pacientesApi'

export default {
  name: 'PacientesList',
  data() {
    return {
      pacientes: []
    }
  },
  async mounted() {
    try {
      await this.cargarPacientes()
    } catch (error) {
      this.$error(error)
    }
  },
  methods: {
    async cargarPacientes() {
      const res = await pacientesApi.getAllIncludingInactive()
      this.pacientes = res.data
    },
    async eliminar(id) {
      try {
        await pacientesApi.delete(id)
        await this.cargarPacientes()
      } catch (error) {
      this.$error(error)
    }
    },
    async activar(id) {
      try {
        await pacientesApi.activate(id)
        await this.cargarPacientes()
      } catch (error) {
        this.$error(error)
      }
    }
  }
}
</script>
