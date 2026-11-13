import { mockGetTickets, mockCreateTicket } from './mockTickets.js'

// ПЕРЕКЛЮЧАТЕЛЬ: true = Mock (без сервера), false = реальный fetch
const USE_MOCK = false

// Учебный флаг для имитации 500 ошибки (для сдачи отчета)
export const TEST_FORCE_500 = false

const baseUrl = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000'

export async function getTickets() {
  if (USE_MOCK) {
    const res = await mockGetTickets(TEST_FORCE_500)
    if (!res.ok) throw new Error(`HTTP ${res.status}: ${res.data.message}`)
    return res.data
  }

  // Реальная ветка (когда появится бэкенд)
  const response = await fetch(`${baseUrl}/api/service-requests`)
  if (!response.ok) {
    throw new Error(`HTTP ${response.status}`)
  }
  return await response.json()
}

export async function createTicket(body) {
  if (USE_MOCK) {
    const res = await mockCreateTicket(body, TEST_FORCE_500)
    if (!res.ok) throw new Error(`HTTP ${res.status}: ${res.data.message}`)
    return res.data
  }

  // Реальная ветка
  const response = await fetch(`${baseUrl}/api/service-requests`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body)
  })
  
  if (!response.ok) {
    // Считываем ошибку от сервера, если есть
    let errorMsg = `HTTP ${response.status}`
    try {
      const errData = await response.json()
      if (errData.message) errorMsg = errData.message
    } catch (e) { /* ignore */ }
    
    throw new Error(errorMsg)
  }
  
  return await response.json()
}