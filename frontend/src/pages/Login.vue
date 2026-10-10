<script setup>
import { ref } from 'vue'

const login = ref({ username: '', password: '' })

function onLogin() {
  console.log('Вход (заглушка):', login.value)
}
</script>

<template>
  <main class="page">
    <h2>Вход в систему</h2>

    <form class="card" @submit.prevent="onLogin">
      <div class="field">
        <label for="username">Логин <span class="req">*</span></label>
        <input
          id="username"
          v-model="login.username"
          name="username"
          type="text"
          autofocus
          required
        />
      </div>

      <div class="field">
        <label for="password">Пароль <span class="req">*</span></label>
        <input
          id="password"
          v-model="login.password"
          name="password"
          type="password"
          required
        />
      </div>

      <div class="actions">
        <button type="submit">Войти</button>
      </div>

      <p class="hint"><em>Заглушка. Настоящая проверка появится в ЛР5.</em></p>
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
  max-width: 420px;
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
  font-size: 0.82rem;
  margin: 0;
}

.actions {
  display: flex;
  gap: 0.6rem;
}
</style>