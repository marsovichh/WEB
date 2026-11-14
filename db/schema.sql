-- Безопасное пересоздание (удаляем в правильном порядке)
DROP TABLE IF EXISTS service_requests CASCADE;
DROP TABLE IF EXISTS atms CASCADE;
DROP TABLE IF EXISTS service_zones CASCADE;
DROP TABLE IF EXISTS users CASCADE;

-- 1. Справочник участков
CREATE TABLE service_zones (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    city VARCHAR(100) NOT NULL
);

-- 2. Пользователи (диспетчеры, инженеры)
CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    login VARCHAR(50) UNIQUE NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    role VARCHAR(20) NOT NULL CHECK (role IN ('Dispatcher', 'Engineer'))
);

-- 3. Банкоматы
CREATE TABLE atms (
    id SERIAL PRIMARY KEY,
    number VARCHAR(50) UNIQUE NOT NULL,
    model VARCHAR(100) NOT NULL,
    address TEXT NOT NULL,
    zone_id INT NOT NULL REFERENCES service_zones(id)
);

-- 4. Заявки на обслуживание (главный объект)
CREATE TABLE service_requests (
    id SERIAL PRIMARY KEY,
    number VARCHAR(50) UNIQUE NOT NULL,
    title VARCHAR(80) NOT NULL CHECK (char_length(title) >= 5),
    description TEXT NOT NULL,
    status VARCHAR(20) NOT NULL CHECK (status IN ('New', 'InProgress', 'Closed', 'Cancelled')),
    atm_id INT NOT NULL REFERENCES atms(id),
    created_by_user_id INT NOT NULL REFERENCES users(id),
    assignee_user_id INT REFERENCES users(id), -- NULL допускается
    created_at TIMESTAMP NOT NULL DEFAULT NOW()
);

-- Индексы для ускорения поиска
-- Ускоряет запрос: SELECT * FROM service_requests WHERE status = 'New';
CREATE INDEX requests_status_idx ON service_requests (status);

-- Ускоряет запрос: SELECT * FROM service_requests WHERE atm_id = 5;
CREATE INDEX requests_atm_id_idx ON service_requests (atm_id);