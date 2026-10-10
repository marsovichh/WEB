<script setup>
import { ref } from 'vue'

// Текущий экран: 'list' | 'new' | 'login'
const screen = ref('list')

// Заготовка списка заявок на ТО (имитация данных из ЛР2)
const requests = ref([
  {
    id: 1,
    number: 'SR-2026-001',
    title: 'Плановое ТО',
    type: 'Scheduled',
    status: 'New',
    dueDate: '2026-10-05',
    atmNumber: 'ATM-2026-001',
    atmId: 12
  },
  {
    id: 2,
    number: 'SR-2026-002',
    title: 'Замятие карты в диспенсере',
    type: 'Repair',
    status: 'InProgress',
    dueDate: '2026-09-30',
    atmNumber: 'ATM-2026-004',
    atmId: 15
  },
  {
    id: 3,
    number: 'SR-2026-003',
    title: 'Инкассация',
    type: 'CashCollection',
    status: 'Closed',
    dueDate: '2026-09-20',
    atmNumber: 'ATM-2026-002',
    atmId: 13
  }
])

// Заготовка для формы создания
const newRequest = ref({
  title: '',
  type: 'Scheduled',
  description: '',
  dueDate: '',
  atmId: ''
})

// Заглушка отправки — ничего не сохраняет (ЛР5)
function onCreate() {
  console.log('Создать (заглушка):', newRequest.value)
}

// Заглушка входа (ЛР5)
const login = ref({ username: '', password: '' })
function onLogin() {
  console.log('Вход (заглушка):', login.value)
}
</script>

<template>
  <div class="app">
    <header>
      <h1>Платформа учёта банкоматов и ТО</h1>
      <nav>
        <button type="button" @click="screen = 'list'">Список</button>
        <button type="button" @click="screen = 'new'">Создать</button>
        <button type="button" @click="screen = 'login'">Вход</button>
      </nav>
    </header>

    <!-- ЭКРАН 1: СПИСОК -->
    <main v-if="screen === 'list'">
      <h2>Заявки на обслуживание</h2>
      <table>
        <thead>
          <tr>
            <th>Номер</th>
            <th>Банкомат</th>
            <th>Тема</th>
            <th>Тип</th>
            <th>Статус</th>
            <th>Срок</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in requests" :key="item.id">
            <td>{{ item.number }}</td>
            <td>{{ item.atmNumber }}</td>
            <td>{{ item.title }}</td>
            <td>{{ item.type }}</td>
            <td>{{ item.status }}</td>
            <td>{{ item.dueDate }}</td>
          </tr>
          <tr v-if="requests.length === 0">
            <td colspan="6">Заявок пока нет</td>
          </tr>
        </tbody>
      </table>
    </main>

    <!-- ЭКРАН 2: СОЗДАТЬ -->
    <main v-else-if="screen === 'new'">
      <h2>Новая заявка на ТО</h2>
      <form @submit.prevent="onCreate">
        <label for="title">
          Тема (5–80 символов)
          <input
            id="title"
            v-model="newRequest.title"
            name="title"
            type="text"
            autofocus
            minlength="5"
            maxlength="80"
          />
        </label>

        <label for="type">
          Тип работ
          <select id="type" v-model="newRequest.type" name="type">
            <option value="Scheduled">Плановое ТО</option>
            <option value="Repair">Ремонт</option>
            <option value="CashCollection">Инкассация</option>
          </select>
        </label>

        <label for="atmId">
          Банкомат (номер записи)
          <input
            id="atmId"
            v-model="newRequest.atmId"
            name="atmId"
            type="number"
          />
        </label>

        <label for="dueDate">
          Плановый срок
          <input
            id="dueDate"
            v-model="newRequest.dueDate"
            name="dueDate"
            type="date"
          />
        </label>

        <label for="description">
          Описание (10–500 символов)
          <textarea
            id="description"
            v-model="newRequest.description"
            name="description"
            minlength="10"
            maxlength="500"
          ></textarea>
        </label>

        <button type="submit">Создать</button>
      </form>
    </main>

    <!-- ЭКРАН 3: ВХОД -->
    <main v-else>
      <h2>Вход в систему</h2>
      <form @submit.prevent="onLogin">
        <label for="username">
          Логин
          <input
            id="username"
            v-model="login.username"
            name="username"
            type="text"
            autofocus
          />
        </label>

        <label for="password">
          Пароль
          <input
            id="password"
            v-model="login.password"
            name="password"
            type="password"
          />
        </label>

        <button type="submit">Войти</button>
      </form>
      <p><em>Заглушка. Настоящая проверка появится в ЛР5.</em></p>
    </main>
  </div>
</template>

<style scoped>
.app {
  font-family: system-ui, sans-serif;
  max-width: 900px;
  margin: 0 auto;
  padding: 1rem;
}

header {
  border-bottom: 1px solid #ccc;
  padding-bottom: 0.5rem;
  margin-bottom: 1rem;
}

nav button {
  margin-right: 0.5rem;
  padding: 0.4rem 0.8rem;
  cursor: pointer;
}

table {
  width: 100%;
  border-collapse: collapse;
}

th, td {
  border: 1px solid #ccc;
  padding: 0.4rem 0.6rem;
  text-align: left;
}

th {
  background: #f0f0f0;
}

form {
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
  max-width: 500px;
}

form label {
  display: flex;
  flex-direction: column;
  font-weight: 600;
  gap: 0.2rem;
}

form input,
form select,
form textarea {
  font-weight: 400;
  padding: 0.4rem;
  font-size: 1rem;
}

form button {
  align-self: flex-start;
  padding: 0.5rem 1rem;
  cursor: pointer;
}
</style>