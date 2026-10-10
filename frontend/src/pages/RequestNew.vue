<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { items } from '../requestsStore.js'

const router = useRouter()

const newRequest = ref({
  title: '',
  type: 'Scheduled',
  description: '',
  dueDate: '',
  atmId: ''
})

function onCreate() {
  // Заглушка: на ЛР5 здесь будет POST /api/service-requests
  console.log('Создать (заглушка):', newRequest.value)

  items.value.push({
    id: items.value.length + 1,
    number: `SR-2026-${String(items.value.length + 1).padStart(3, '0')}`,
    title: newRequest.value.title,
    type: newRequest.value.type,
    status: 'New',
    dueDate: newRequest.value.dueDate,
    atmId: Number(newRequest.value.atmId),
    atmAddress: '— (уточнится на сервере)'
  })

  newRequest.value = {
    title: '',
    type: 'Scheduled',
    description: '',
    dueDate: '',
    atmId: ''
  }

  // Перейти обратно к списку
  router.push('/')
}
</script>

<template>
  <main class="page">
    <h2>Новая заявка на обслуживание</h2>

    <form class="card" @submit.prevent="onCreate">
      <div class="field">
        <label for="title">Тема <span class="req">*</span></label>
        <input
          id="title"
          v-model="newRequest.title"
          name="title"
          type="text"
          autofocus
          minlength="5"
          maxlength="80"
          placeholder="Например: Плановое ТО"
          required
        />
        <small class="hint">5–80 символов</small>
      </div>

      <div class="row">
        <div class="field">
          <label for="type">Тип работ <span class="req">*</span></label>
          <select id="type" v-model="newRequest.type" name="type" required>
            <option value="Scheduled">Плановое ТО</option>
            <option value="Repair">Ремонт</option>
            <option value="CashCollection">Инкассация</option>
          </select>
        </div>

        <div class="field">
          <label for="atmId">Банкомат (номер записи) <span class="req">*</span></label>
          <input
            id="atmId"
            v-model="newRequest.atmId"
            name="atmId"
            type="number"
            min="1"
            placeholder="12"
            required
          />
        </div>
      </div>

      <div class="field">
        <label for="dueDate">Плановый срок <span class="req">*</span></label>
        <input
          id="dueDate"
          v-model="newRequest.dueDate"
          name="dueDate"
          type="date"
          required
        />
      </div>

      <div class="field">
        <label for="description">Описание <span class="req">*</span></label>
        <textarea
          id="description"
          v-model="newRequest.description"
          name="description"
          rows="4"
          minlength="10"
          maxlength="500"
          placeholder="Опишите проблему или состав работ"
          required
        ></textarea>
        <small class="hint">10–500 символов</small>
      </div>

      <div class="actions">
        <button type="submit">Создать</button>
        <button type="button" class="btn-secondary" @click="router.push('/')">
          Отмена
        </button>
      </div>
    </form>
  </main>
</template>

<style scoped>
h2 {
  font-size: 1.15rem;
  letter-spacing: -0.01em;
  margin: 0 0 0.9rem;
}

.card {
  background: var(--color-surface);
  border: 1px solid var(--color-border);
  border-radius: var(--radius);
  box-shadow: var(--shadow-sm);
  padding: 1.25rem;
  display: flex;
  flex-direction: column;
  gap: 1rem;
  max-width: 720px;
}

.row {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1rem;
}

@media (max-width: 560px) {
  .row { grid-template-columns: 1fr; }
}

.field {
  display: flex;
  flex-direction: column;
  gap: 0.35rem;
}

.field label {
  font-weight: 600;
  font-size: 0.9rem;
}

.req {
  color: #dc2626;
}

.hint {
  color: var(--color-muted);
  font-size: 0.78rem;
}

.actions {
  display: flex;
  gap: 0.6rem;
  margin-top: 0.25rem;
}

.btn-secondary {
  background: transparent;
  color: var(--color-text);
  border: 1px solid var(--color-border);
}

.btn-secondary:hover {
  background: #f3f4f6;
  box-shadow: none;
}
</style>