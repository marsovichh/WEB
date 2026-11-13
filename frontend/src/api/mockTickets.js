// Имитация базы данных на сервере
let mockDb = [
  { id: 1, number: 'SR-2026-001', title: 'Плановое ТО', type: 'Scheduled', status: 'New', atmId: 12 },
  { id: 2, number: 'SR-2026-002', title: 'Ремонт диспенсера', type: 'Repair', status: 'InProgress', atmId: 15 }
]

// Вспомогательная функция задержки
const delay = (ms) => new Promise(resolve => setTimeout(resolve, ms))

export async function mockGetTickets(force500 = false) {
  await delay(500) // Имитируем долгую загрузку сети

  if (force500) {
    return { ok: false, status: 500, data: { message: 'Внутренняя ошибка сервера' } }
  }

  return { ok: true, status: 200, data: mockDb }
}

export async function mockCreateTicket(body, force500 = false) {
  await delay(500)

  // Проверка на сервере (имитация)
  if (!body.title || body.title.trim().length < 5) {
    return { ok: false, status: 400, data: { message: 'Тема слишком короткая' } }
  }

  if (force500) {
    return { ok: false, status: 500, data: { message: 'Сервер не смог обработать запрос' } }
  }

  // Формируем ответ от сервера (id и number берет сервер, а не клиент)
  const newTicket = {
    id: mockDb.length + 1,
    number: `SR-2026-${String(mockDb.length + 1).padStart(3, '0')}`,
    title: body.title,
    description: body.description,
    atmId: body.atmId,
    type: body.type || 'Scheduled',
    status: 'New' // Сервер всегда ставит New
  }

  mockDb.push(newTicket)
  return { ok: true, status: 201, data: newTicket }
}