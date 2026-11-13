<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { createTicket } from '../api/tickets.js'

const router = useRouter()

const form = ref({
  title: '',
  type: 'Scheduled',
  description: '',
  atmId: null
})

const errors = ref({ title: '', atmId: '', description: '' })
const globalError = ref('')
const isSubmitting = ref(false)

function validate() {
  errors.value = { title: '', atmId: '', description: '' }
  let isValid = true

  if (form.value.title.trim().length < 5) {
    errors.value.title = 'Тема: от 5 до 80 символов'
    isValid = false
  }
  if (!Number.isInteger(form.value.atmId) || form.value.atmId <= 0) {
    errors.value.atmId = 'Укажите целое число больше нуля'
    isValid = false
  }
  if (form.value.description.trim().length < 10) {
    errors.value.description = 'Описание: от 10 до 500 символов'
    isValid = false
  }
  return isValid
}

async function onCreate() {
  if (isSubmitting.value) return
  if (!validate()) return

  isSubmitting.value = true
  globalError.value = ''

  try {
    await createTicket({
      title: form.value.title.trim(),
      atmId: form.value.atmId,
      description: form.value.description.trim(),
      type: form.value.type
    })

    // Если успешно — переходим на список
    router.push('/')
  } catch (error) {
    // Показываем ошибку сервера (400, 500, network)
    globalError.value = error.message
  } finally {
    // Снимаем блокировку кнопки в любом случае
    isSubmitting.value = false
  }
}
</script>

<template>
  <main class="page">
    <h2>Новая заявка на обслуживание</h2>

    <!-- Блок для вывода ошибок от сервера -->
    <div v-if="globalError" class="global-error">
      {{ globalError }}
    </div>

    <form class="card" @submit.prevent="onCreate">
      <div class="field">
        <label for="title">Тема <span class="req">*</span></label>
        <input
          id="title"
          v-model="form.title"
          name="title"
          type="text"
          autofocus
          placeholder="Например: Плановое ТО"
        />
        <small v-if="errors.title" class="error">{{ errors.title }}</small>
      </div>

      <div class="row">
        <div class="field">
          <label for="type">Тип работ <span class="req">*</span></label>
          <select id="type" v-model="form.type" name="type">
            <option value="Scheduled">Плановое ТО</option>
            <option value="Repair">Ремонт</option>
            <option value="CashCollection">Инкассация</option>
          </select>
        </div>

        <div class="field">
          <label for="atmId">Банкомат (номер записи) <span class="req">*</span></label>
          <input
            id="atmId"
            v-model.number="form.atmId"
            name="atmId"
            type="number"
            min="1"
            placeholder="12"
          />
          <small v-if="errors.atmId" class="error">{{ errors.atmId }}</small>
        </div>
      </div>

      <div class="field">
        <label for="description">Описание <span class="req">*</span></label>
        <textarea
          id="description"
          v-model="form.description"
          name="description"
          rows="4"
          placeholder="Опишите проблему или состав работ"
        ></textarea>
        <small v-if="errors.description" class="error">{{ errors.description }}</small>
      </div>

      <div class="actions">
        <button type="submit" :disabled="isSubmitting">
          {{ isSubmitting ? 'Создаём...' : 'Создать' }}
        </button>
        <button 
          type="button" 
          class="btn-secondary" 
          @click="router.push('/')" 
          :disabled="isSubmitting"
        >
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

.global-error {
  background: #fee2e2;
  color: #991b1b;
  padding: 0.8rem;
  border-radius: 8px;
  margin-bottom: 1rem;
  max-width: 720px;
  font-weight: 500;
  border: 1px solid #fca5a5;
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

.req { color: #dc2626; }
.hint { color: var(--color-muted); font-size: 0.78rem; }
.error { color: #dc2626; font-size: 0.78rem; font-weight: 500; }

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

button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}
</style>