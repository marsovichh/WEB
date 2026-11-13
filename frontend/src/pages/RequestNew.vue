<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { addItem } from '../ticketsStore.js'

const router = useRouter()

// Состояние формы (atmId: null для корректной работы v-model.number)
const form = ref({
  title: '',
  type: 'Scheduled',
  description: '',
  atmId: null
})

// Состояние ошибок
const errors = ref({
  title: '',
  atmId: '',
  description: ''
})

function validate() {
  // Сбрасываем старые ошибки
  errors.value = { title: '', atmId: '', description: '' }
  let isValid = true

  // 1. Проверка темы
  const title = form.value.title.trim()
  if (title.length < 5 || title.length > 80) {
    errors.value.title = 'Тема: от 5 до 80 символов'
    isValid = false
  }

  // 2. Проверка банкомата (должно быть целое число > 0)
  if (
    form.value.atmId === null ||
    form.value.atmId === '' ||
    !Number.isInteger(form.value.atmId) ||
    form.value.atmId <= 0
  ) {
    errors.value.atmId = 'Укажите целое число больше нуля'
    isValid = false
  }

  // 3. Проверка описания
  const description = form.value.description.trim()
  if (description.length < 10 || description.length > 500) {
    errors.value.description = 'Описание: от 10 до 500 символов'
    isValid = false
  }

  return isValid
}

function onCreate() {
  // Если проверка не пройдена — выходим, ничего не добавляя
  if (!validate()) return

  // Добавляем в общий store
  addItem({
    title: form.value.title.trim(),
    atmId: form.value.atmId,
    description: form.value.description.trim(),
    type: form.value.type
  })

  // Очищаем форму и ошибки
  form.value = { title: '', type: 'Scheduled', description: '', atmId: null }
  errors.value = { title: '', atmId: '', description: '' }

  // Переход на список
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
          v-model="form.title"
          name="title"
          type="text"
          autofocus
          placeholder="Например: Плановое ТО"
        />
        <small v-if="errors.title" class="error">{{ errors.title }}</small>
        <small v-else class="hint">5–80 символов</small>
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
          <small v-else class="hint">Целое число &gt; 0</small>
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
        <small v-else class="hint">10–500 символов</small>
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
</style>