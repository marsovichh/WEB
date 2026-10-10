<script setup>
// только разметка + router-link
</script>

<template>
  <header class="app-header">
    <div class="brand">
      <span class="brand-mark" aria-hidden="true">🏧</span>
      <div class="brand-text">
        <strong>Управление банкоматной сетью</strong>
        <small>обслуживание · инкассация · аналитика</small>
      </div>
    </div>

    <nav class="nav">
      <RouterLink to="/" class="nav-link">Список</RouterLink>
      <RouterLink to="/new" class="nav-link">Создать</RouterLink>
      <RouterLink to="/login" class="nav-link">Вход</RouterLink>
    </nav>
  </header>
</template>

<style scoped>
.app-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 1rem;
  padding: 0.9rem 1.1rem;
  background: var(--color-surface);
  border: 1px solid var(--color-border);
  border-radius: var(--radius);
  box-shadow: var(--shadow-sm);
  margin-bottom: 1.25rem;
}

.brand {
  display: flex;
  align-items: center;
  gap: 0.7rem;
  min-width: 0;
}

.brand-mark {
  font-size: 1.6rem;
  line-height: 1;
}

.brand-text {
  display: flex;
  flex-direction: column;
  min-width: 0;
}

.brand-text strong {
  font-size: 1.05rem;
  letter-spacing: -0.01em;
}

.brand-text small {
  color: var(--color-muted);
  font-size: 0.78rem;
}

.nav {
  display: flex;
  gap: 0.25rem;
  flex-wrap: wrap;
}

.nav-link {
  color: var(--color-text);
  padding: 0.45rem 0.85rem;
  border-radius: 8px;
  font-weight: 500;
  text-decoration: none;
  transition: background-color 0.15s ease, color 0.15s ease;
}

.nav-link:hover {
  background: var(--color-primary-soft);
  color: var(--color-primary);
  text-decoration: none;
}

.nav-link.router-link-active {
  background: var(--color-primary);
  color: #fff;
}
</style>