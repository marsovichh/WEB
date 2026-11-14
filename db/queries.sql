-- ==========================================
-- 6.1. Фильтр и сортировка
-- ==========================================
SELECT number, title, status, created_at
FROM service_requests
WHERE status = 'New'
ORDER BY created_at DESC;

-- ==========================================
-- 6.2. JOIN
-- (Выводим название участка, а не только его ID)
-- ==========================================
SELECT 
    sr.number, 
    sr.title, 
    sz.name AS site_name, 
    sr.status
FROM service_requests AS sr
JOIN atms AS a ON a.id = sr.atm_id
JOIN service_zones AS sz ON sz.id = a.zone_id
ORDER BY sr.created_at DESC;

-- ==========================================
-- 6.3. GROUP BY
-- (Считаем количество заявок в каждом статусе)
-- ==========================================
SELECT status, COUNT(*) AS count
FROM service_requests
GROUP BY status
ORDER BY status;