<script setup>
import { items } from '../requestsStore.js'
import RequestTable from '../components/RequestTable.vue'
</script>

<template>
  <main class="page">
    <header class="page-header">
      <h2>Заявки на обслуживание банкоматов</h2>
      <p class="muted">Всего: {{ items.length }}</p>
    </header>
    <RequestTable :items="items" />
  </main>
</template>

<style scoped>
.page-header {
  display: flex;
  align-items: baseline;
  justify-content: space-between;
  margin-bottom: 0.9rem;
}

.page-header h2 {
  margin: 0;
  font-size: 1.15rem;
  letter-spacing: -0.01em;
}

.muted {
  color: var(--color-muted);
  font-size: 0.9rem;
  margin: 0;
}
</style>