<script setup>
// Таблица НЕ хранит массив — получает его через props сверху
defineProps({
  items: {
    type: Array,
    required: true
  }
})

const typeLabels = {
  Scheduled: 'Плановое ТО',
  Repair: 'Ремонт',
  CashCollection: 'Инкассация'
}

const statusLabels = {
  New: 'Новая',
  InProgress: 'В работе',
  Closed: 'Закрыта',
  Cancelled: 'Отменена'
}

function statusClass(status) {
  return {
    'badge': true,
    'badge-new': status === 'New',
    'badge-progress': status === 'InProgress',
    'badge-closed': status === 'Closed',
    'badge-cancelled': status === 'Cancelled'
  }
}

function typeClass(type) {
  return {
    'badge': true,
    'badge-type-scheduled': type === 'Scheduled',
    'badge-type-repair': type === 'Repair',
    'badge-type-collection': type === 'CashCollection'
  }
}
</script>

<template>
  <div class="table-wrap">
    <table>
      <thead>
        <tr>
          <th>Номер</th>
          <th>Банкомат (id)</th>
          <th>Адрес банкомата</th>
          <th>Тема</th>
          <th>Тип</th>
          <th>Статус</th>
          <th>Срок</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in items" :key="item.id">
          <td class="col-num">{{ item.number }}</td>
          <td class="col-id">#{{ item.atmId }}</td>
          <td class="col-addr">{{ item.atmAddress }}</td>
          <td class="col-title">{{ item.title }}</td>
          <td>
            <span :class="typeClass(item.type)">
              {{ typeLabels[item.type] || item.type }}
            </span>
          </td>
          <td>
            <span :class="statusClass(item.status)">
              {{ statusLabels[item.status] || item.status }}
            </span>
          </td>
          <td class="col-date">{{ item.dueDate }}</td>
        </tr>
        <tr v-if="items.length === 0">
          <td colspan="7" class="empty">Заявок пока нет</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style scoped>
.table-wrap {
  background: var(--color-surface);
  border: 1px solid var(--color-border);
  border-radius: var(--radius);
  box-shadow: var(--shadow-sm);
  overflow: hidden;
}

table {
  width: 100%;
  border-collapse: collapse;
  font-size: 0.94rem;
}

thead th {
  text-align: left;
  font-weight: 600;
  font-size: 0.8rem;
  letter-spacing: 0.03em;
  text-transform: uppercase;
  color: var(--color-muted);
  background: #f8fafc;
  padding: 0.7rem 0.9rem;
  border-bottom: 1px solid var(--color-border);
  white-space: nowrap;
}

tbody td {
  padding: 0.7rem 0.9rem;
  border-bottom: 1px solid var(--color-border);
  vertical-align: middle;
}

tbody tr:last-child td {
  border-bottom: none;
}

tbody tr:nth-child(even) {
  background: #fbfcfe;
}

tbody tr:hover {
  background: var(--color-primary-soft);
}

.col-num {
  font-variant-numeric: tabular-nums;
  color: var(--color-muted);
  white-space: nowrap;
}

.col-id {
  font-variant-numeric: tabular-nums;
  color: var(--color-muted);
  white-space: nowrap;
}

.col-addr {
  color: var(--color-text);
}

.col-title {
  font-weight: 500;
}

.col-date {
  font-variant-numeric: tabular-nums;
  color: var(--color-muted);
  white-space: nowrap;
}

.empty {
  text-align: center;
  color: var(--color-muted);
  padding: 1.5rem 1rem;
}

.badge {
  display: inline-block;
  padding: 0.15rem 0.55rem;
  border-radius: 999px;
  font-size: 0.78rem;
  font-weight: 600;
  white-space: nowrap;
}

.badge-new      { background: var(--status-new-bg);        color: var(--status-new-fg); }
.badge-progress { background: var(--status-progress-bg);   color: var(--status-progress-fg); }
.badge-closed   { background: var(--status-closed-bg);     color: var(--status-closed-fg); }
.badge-cancelled{ background: var(--status-cancelled-bg);  color: var(--status-cancelled-fg); }

.badge-type-scheduled  { background: #eef2ff; color: #3730a3; }
.badge-type-repair     { background: #fff1f2; color: #9f1239; }
.badge-type-collection { background: #ecfeff; color: #155e75; }
</style>