<script setup>
import { ref, onMounted } from 'vue'
import { getTickets } from '../api/tickets.js'
import RequestTable from '../components/RequestTable.vue'

// Одно состояние: 'loading' | 'empty' | 'data' | 'error'
const state = ref('loading')
const items = ref([])
const errorMessage = ref('')

async function loadData() {
  state.value = 'loading'
  errorMessage.value = ''

  try {
    const data = await getTickets()
    items.value = data
    
    // Проверяем на пустоту
    if (data.length === 0) {
      state.value = 'empty'
    } else {
      state.value = 'data'
    }
  } catch (error) {
    state.value = 'error'
    errorMessage.value = error.message
  }
}

// Загружаем при входе на страницу
onMounted(() => {
  loadData()
})
</script>

<template>
  <main class="page">
    <header class="page-header">
      <h2>Заявки на обслуживание</h2>
    </header>

    <!-- 1. СОСТОЯНИЕ ЗАГРУЗКИ -->
    <div v-if="state === 'loading'" class="status-box">
      Загрузка данных...
    </div>

    <!-- 2. СОСТОЯНИЕ ОШИБКИ -->
    <div v-else-if="state === 'error'" class="status-box error-box">
      <p>Ошибка загрузки: {{ errorMessage }}</p>
      <button @click="loadData">Повторить</button>
    </div>

    <!-- 3. ПУСТОЙ СПИСОК -->
    <div v-else-if="state === 'empty'" class="status-box">
      Записей пока нет.
    </div>

    <!-- 4. ДАННЫЕ (ТАБЛИЦА) -->
    <RequestTable v-else :items="items" />
  </main>
</template>

<style scoped>
.status-box {
  padding: 2rem;
  text-align: center;
  background: var(--color-surface);
  border: 1px solid var(--color-border);
  border-radius: var(--radius);
  color: var(--color-muted);
}
.error-box {
  color: #dc2626;
  border-color: #fca5a5;
  background: #fef2f2;
}
.error-box button {
  margin-top: 1rem;
  background: #dc2626;
}
.error-box button:hover {
  background: #b91c1c;
}
.page-header { margin-bottom: 1rem; }
</style>