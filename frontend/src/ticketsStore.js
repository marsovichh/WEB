import { ref } from 'vue'

// Общий массив заявок. Живёт вне экранов, чтобы не сбрасываться при переходах.
// atmAddress — только для превью; на сервере адрес лежит в Atm (см. ЛР2).
export const items = ref([
  {
    id: 1,
    number: 'SR-2026-001',
    title: 'Плановое ТО',
    type: 'Scheduled',
    status: 'New',
    dueDate: '2026-10-05',
    atmId: 12,
    atmAddress: 'г. Москва, ул. Тверская, 12'
  },
  {
    id: 2,
    number: 'SR-2026-002',
    title: 'Замятие карты в диспенсере',
    type: 'Repair',
    status: 'InProgress',
    dueDate: '2026-09-30',
    atmId: 15,
    atmAddress: 'г. Москва, Ленинградский пр-т, 78'
  },
  {
    id: 3,
    number: 'SR-2026-003',
    title: 'Инкассация',
    type: 'CashCollection',
    status: 'Closed',
    dueDate: '2026-09-20',
    atmId: 13,
    atmAddress: 'г. Казань, ул. Баумана, 5'
  }
])

// Учебный счётчик для новых id (в ЛР6 id будет приходить с сервера)
let nextId = items.value.length + 1

// Функция добавления новой заявки в общий массив
export function addItem({ title, atmId, description, type }) {
  const newItem = {
    id: nextId,
    number: `SR-2026-${String(nextId).padStart(3, '0')}`, // Формируем красивый номер
    title,
    type: type || 'Scheduled',
    status: 'New', // По методичке статус всегда New
    dueDate: '—',  // В ЛР5 срок в форме не вводим
    atmId,
    atmAddress: '— (уточнится позже)' // Заглушка для столбца таблицы
  }

  items.value.push(newItem)
  nextId += 1
  return newItem
}