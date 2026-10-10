<script setup>
import AppHeader from './components/AppHeader.vue'
</script>

<template>
  <div class="app-shell">
    <AppHeader />
    <RouterView />
  </div>
</template>

<style scoped>
.app-shell {
  max-width: 1080px;
  margin: 0 auto;
  padding: 1.5rem 1rem 3rem;
}
</style>