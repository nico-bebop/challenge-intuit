<template>
  <div>
    <nav class="navbar">
      <span class="brand">Turnos Médicos</span>
      <div class="links">
        <router-link to="/turnos">Turnos</router-link>
        <router-link to="/turnos/nuevo">Nuevo turno</router-link>
        <router-link to="/pacientes">Pacientes</router-link>
      </div>
    </nav>
    <main class="main-content">
      <router-view />
    </main>
  </div>
</template>

<script>
export default {
  name: 'App'
}
</script>
