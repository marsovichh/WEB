-- 1. Участки
INSERT INTO service_zones (name, city) VALUES
('Центральный', 'Москва'),
('Северный', 'Санкт-Петербург');

-- 2. Пользователи (1 диспетчер, 2 инженера)
INSERT INTO users (login, full_name, role) VALUES
('dispatcher1', 'Иванов И.И.', 'Dispatcher'),
('engineer1', 'Петров П.П.', 'Engineer'),
('engineer2', 'Сидоров С.С.', 'Engineer');

-- 3. Банкоматы
INSERT INTO atms (number, model, address, zone_id) VALUES
('ATM-001', 'NCR SelfServ', 'ул. Тверская, 1', 1),
('ATM-002', 'Diebold Nixdorf', 'Невский пр., 10', 2);

-- 4. Заявки (обязательно используем все 4 статуса)
INSERT INTO service_requests (number, title, description, status, atm_id, created_by_user_id, assignee_user_id) VALUES
('SR-2026-001', 'Плановое ТО', 'Замена картриджей', 'Closed', 1, 1, 2),
('SR-2026-002', 'Ремонт диспенсера', 'Замятие карты', 'InProgress', 1, 1, 3),
('SR-2026-003', 'Инкассация', 'Пополнение наличных', 'New', 2, 1, NULL),
('SR-2026-004', 'Ремонт экрана', 'Разбит сенсор', 'Cancelled', 2, 1, NULL);