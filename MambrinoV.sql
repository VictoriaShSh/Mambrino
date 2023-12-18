-- CREACION BASE DE DATOS MAMBRINO

CREATE DATABASE IF NOT EXISTS mambrino;
USE mambrino;

-- ELIMINACION DE TABLAS

DROP TABLE IF EXISTS patologia;
DROP TABLE IF EXISTS hoja_prescripcion;
DROP TABLE IF EXISTS notas_enfermeria;
DROP TABLE IF EXISTS usuarios;
DROP TABLE IF EXISTS alertas;
DROP TABLE IF EXISTS alta;
DROP TABLE IF EXISTS ingresos;
DROP TABLE IF EXISTS pacientes;
DROP TABLE IF EXISTS camas;
DROP TABLE IF EXISTS centros;
DROP TABLE IF EXISTS provincia;
DROP TABLE IF EXISTS comunidadautonoma;
DROP TABLE IF EXISTS zonaBasicaSalud;
DROP TABLE IF EXISTS areaSalud;
DROP TABLE IF EXISTS roles_ambitos;
DROP TABLE IF EXISTS roles;
DROP TABLE IF EXISTS ambitos;


-- CREACION DE TABLAS

CREATE TABLE ambitos (
    id_ambito INT PRIMARY KEY NOT NULL,
    nombre VARCHAR(300) DEFAULT NULL
);

CREATE TABLE roles (
    id_rol INT PRIMARY KEY NOT NULL,
    nombre VARCHAR(300) DEFAULT NULL
);

CREATE TABLE roles_ambitos (
    ID_ROL INT NOT NULL,
    ID_AMBITO INT NOT NULL,
    CONSTRAINT roles_ambitos_ibfk_1 FOREIGN KEY (ID_ROL) REFERENCES roles (id_rol),
    CONSTRAINT roles_ambitos_ibfk_2 FOREIGN KEY (ID_AMBITO) REFERENCES ambitos (id_ambito)
);

CREATE TABLE areaSalud (
    id_area_salud INT PRIMARY KEY NOT NULL,
    nombre VARCHAR(100) DEFAULT NULL
);

CREATE TABLE zonaBasicaSalud (
    id_zona_basica_salud INT PRIMARY KEY NOT NULL,
    nombre VARCHAR(100) DEFAULT NULL,
    cap VARCHAR(100) DEFAULT NULL,
    id_area_salud INT DEFAULT NULL,
    id_ambito INT DEFAULT NULL,
    CONSTRAINT zonaBasicaSalud_ibfk_1 FOREIGN KEY (id_area_salud) REFERENCES areaSalud (id_area_salud),
    CONSTRAINT zonaBasicaSalud_ibfk_2 FOREIGN KEY (id_ambito) REFERENCES ambitos (id_ambito)
);

CREATE TABLE comunidadautonoma (
    id_cautonoma INT PRIMARY KEY NOT NULL,
    nombre VARCHAR(100) DEFAULT NULL
);

CREATE TABLE provincia (
    id_provincia INT PRIMARY KEY NOT NULL,
    nombre VARCHAR(100) DEFAULT NULL,
    id_cautonoma INT DEFAULT NULL,
    CONSTRAINT provincia_ibfk_1 FOREIGN KEY (id_cautonoma) REFERENCES comunidadautonoma (id_cautonoma)
);

CREATE TABLE centros (
    id_centro INT PRIMARY KEY NOT NULL,
    nombre VARCHAR(100) DEFAULT NULL,
    id_area_salud INT DEFAULT NULL,
    CONSTRAINT centros_ibfk_1 FOREIGN KEY (id_area_salud) REFERENCES areaSalud (id_area_salud)
);

CREATE TABLE camas (
    id_cama INT PRIMARY KEY NOT NULL,
    id_planta INT DEFAULT NULL,
    id_habitacion INT DEFAULT NULL,
    letra VARCHAR(1) DEFAULT NULL,
    bloqueada CHAR,
    estado VARCHAR(50) DEFAULT NULL,
    id_centro INT NOT NULL,
    CONSTRAINT camas_ibfk_1 FOREIGN KEY (id_centro) REFERENCES centros (id_centro)
);

CREATE TABLE pacientes (
    nhc INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    NIF VARCHAR(20) NOT NULL,
    NumeroSS INT NOT NULL UNIQUE,
    Nombre VARCHAR(100) DEFAULT NULL,
    Apellido1 VARCHAR(100) DEFAULT NULL,
    Apellido2 VARCHAR(100) DEFAULT NULL,
    Sexo VARCHAR(50) DEFAULT NULL,
    FechaNacimiento DATE DEFAULT NULL,
    Telefono1 INT DEFAULT NULL,
    Telefono2 INT DEFAULT NULL,
    Movil INT DEFAULT NULL,
    EstadoCivil VARCHAR(20) DEFAULT NULL,
    Estudios VARCHAR(100) DEFAULT NULL,
    Fallecido VARCHAR(3) DEFAULT NULL,
    Nacionalidad VARCHAR(20) DEFAULT NULL,
    CAutonoma VARCHAR(100) DEFAULT NULL,
    Provincia VARCHAR(100) DEFAULT NULL,
    Poblacion VARCHAR(100) DEFAULT NULL,
    CP INT DEFAULT NULL,
    Direccion VARCHAR(200) DEFAULT NULL,
    Titular VARCHAR(100) DEFAULT NULL,
    Regimen VARCHAR(100) DEFAULT NULL,
    TIS VARCHAR(20) UNIQUE DEFAULT NULL,
    Medico VARCHAR(100) DEFAULT NULL,
    CAP VARCHAR(100) DEFAULT NULL,
    Zona VARCHAR(100) DEFAULT NULL,
    Area VARCHAR(100) DEFAULT NULL,
    Nacimiento VARCHAR(20) DEFAULT NULL,
    CAutonomaNacimiento VARCHAR(100) DEFAULT NULL,
    ProvinciaNacimiento VARCHAR(100) DEFAULT NULL,
    PoblacionNacimiento VARCHAR(100) DEFAULT NULL
);

CREATE TABLE ingresos (
    id_alta INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    estado_hnc VARCHAR(20) NOT NULL,
    id_ambito VARCHAR(20) NOT NULL,
    Fecha DATE DEFAULT NULL,
    Hora TIME DEFAULT NULL,
    id_cama INT DEFAULT NULL,
    nhc INT NOT NULL,
    CONSTRAINT ingresos_ibfk_1 FOREIGN KEY (id_cama) REFERENCES camas (id_cama),
    CONSTRAINT ingresos_ibfk_2 FOREIGN KEY (nhc) REFERENCES pacientes (nhc)
);

CREATE TABLE alta (
    id_alta INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    fecha DATE DEFAULT NULL,
    hora TIME DEFAULT NULL,
    motivo VARCHAR(300) DEFAULT NULL,
    tipo VARCHAR(50) DEFAULT NULL,
    nhc INT NOT NULL,
    CONSTRAINT altas_ibfk_1 FOREIGN KEY (nhc) REFERENCES pacientes (nhc)
);

CREATE TABLE alertas (
    codigo INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    descripcion VARCHAR(200) DEFAULT NULL,
    observaciones VARCHAR(400) DEFAULT NULL,
    fecha DATE DEFAULT NULL,
    nhc INT DEFAULT NULL,
    CONSTRAINT alertas_ibfk_1 FOREIGN KEY (nhc) REFERENCES pacientes (nhc)
);

CREATE TABLE usuarios (
    id_usuario INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    apellidos VARCHAR(200) NOT NULL,
    email VARCHAR(200) NOT NULL,
    id_rol INT NOT NULL,
    password VARCHAR(50) NOT NULL,
    id_centro INT NOT NULL,
    CONSTRAINT usuarios_ibfk_2 FOREIGN KEY (id_rol) REFERENCES roles (id_rol),
    CONSTRAINT usuarios_ibfk_3 FOREIGN KEY (id_centro) REFERENCES centros (id_centro)
);

CREATE TABLE notas_enfermeria (
    id_notas INT PRIMARY KEY AUTO_INCREMENT,
    fecha_toma DATE,
    hora_toma TIME,
    temperatura DECIMAL(5,2),
    tension_arterial_sistolica INT,
    tension_arterial_diastolica INT,
    frecuencia_cardiaca INT,
    frecuencia_respiratoria INT,
    peso DECIMAL(5,2),
    talla DECIMAL(5,2),
    indice_masa_corporal DECIMAL(5,2),
    glucemia_capilar DECIMAL(5,2),
    ingesta_alimentos VARCHAR(100) DEFAULT NULL,
    tipo_deposicion VARCHAR(100) DEFAULT NULL,
    nauseas VARCHAR(100) DEFAULT NULL,
    prurito VARCHAR(100) DEFAULT NULL,
    observaciones VARCHAR(100) DEFAULT NULL,
    balance_hidrico_entrada_alimentos INT,
    balance_hidrico_entrada_liquidos INT,
    balance_hidrico_fluidoterapia INT,
    balance_hidrico_hemoderivados INT,
    balance_hidrico_otros_entrada INT,
    balance_hidrico_diuresis INT,
    balance_hidrico_vomitos INT,
    balance_hidrico_heces INT,
    balance_hidrico_sonda_nasogastrica INT,
    balance_hidrico_drenajes INT,
    balance_hidrico_otras_perdidas INT,
    total_balance_hidrico INT,
    nhc INT,
    CONSTRAINT notas_enfermeria_ibfk_1 FOREIGN KEY (nhc) REFERENCES pacientes (nhc)
);

CREATE TABLE hoja_prescripcion (
    id_hoja INT PRIMARY KEY AUTO_INCREMENT,
    especialidad VARCHAR(255),
    principio_activo VARCHAR(255),
    dosis VARCHAR(50),
    via_administracion VARCHAR(50),
    frecuencia VARCHAR(50),
    fecha_fin_tratamiento DATE,
    nhc INT,
    CONSTRAINT patologia_ibfk_1 FOREIGN KEY (nhc) REFERENCES pacientes (nhc)
);

CREATE TABLE patologia (
    id_patologia INT PRIMARY KEY AUTO_INCREMENT,
    fecha_inicio DATE,
    fecha_diagnostico DATE,
    sintomas VARCHAR(255),    
    diagnostico VARCHAR(255),
    especialidad VARCHAR(50),
    codificacion VARCHAR(50),
    nhc INT,
    CONSTRAINT fk_patologia_nhc FOREIGN KEY (nhc) REFERENCES pacientes (nhc)
);

-- INSERTS

INSERT INTO roles (id_rol, nombre) VALUES
(1, 'Profesor'),
(2, 'TCAE'),
(3, 'Administrativo'),
(4, 'Auxiliar de Farmacia y Parafarmacia'),
(5, 'Técnico en Imagen para el Diagnóstico'),
(6, 'Técnico en Laboratorio Clínico y Biomédico');

INSERT INTO ambitos (id_ambito, nombre) VALUES
(0, ' '),
(1, 'Hospitalización'),
(2, 'Urgencias'),
(3, 'Consultas');

INSERT INTO roles_ambitos (ID_ROL, ID_AMBITO) VALUES
(1, 1), (2, 1), (3, 1), (4, 1), (5, 1), (6, 1),
(1, 2), (2, 2), (3, 2), (4, 2), (5, 2), (6, 2),
(1, 3), (2, 3), (3, 3), (4, 3), (5, 3), (6, 3);

INSERT INTO areaSalud (id_area_salud, nombre) VALUES
(0, ' '),
(1, 'Albacete'),
(2, 'Almansa (Albacete)'),
(3, 'Hellín (Albacete)'),
(4, 'Villarrobledo (Albacete)'),
(5, 'Alcázar de San Juan (La Mancha Centro)'),
(6, 'Tomelloso (La Mancha Centro)'),
(7, 'Ciudad Real'),
(8, 'Manzanares (Ciudad Real)'),
(9, 'Valdepeñas (Ciudad Real)'),
(10, 'Cuenca'),
(11, 'Guadalajara'),
(12, 'Talavera'),
(13, 'Toledo'),
(14, 'Puertollano');

-- ALBACETE
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(1, 'Alcadozo', 'Alcadozo (Albacete)', 1),
(2, 'Alcaraz', 'Alcaraz (Albacete)', 1),
(3, 'Balazote', 'Balazote (Albacete)', 1),
(4, 'Bogarra', 'Bogarra (Albacete)', 1),
(5, 'Casas de Juan Núñez', 'Casas de Juan Núñez (Albacete)', 1),
(6, 'Casas Ibáñez', 'Casas Ibáñez (Albacete)', 1),
(7, 'Casasimarro', 'Casasimarro (Cuenca)', 1),
(8, 'Chinchilla de Montearagón', 'Chinchilla de Montearagón (Albacete)', 1),
(9, 'Iniesta', 'Iniesta (Cuenca)', 1),
(10, 'La Roda', 'La Roda (Albacete)', 1),
(11, 'Madrigueras', 'Madrigueras (Albacete)', 1),
(12, 'Quintanar del Rey', 'Quintanar del Rey (Cuenca)', 1),
(13, 'Tarazona de la Mancha', 'Tarazona de la Mancha (Albacete)', 1),
(14, 'Villamalea', 'Villamalea (Albacete)', 1),
(15, 'Zona 1-Hospital', 'Zona 1-Hospital (Albacete)', 1),
(16, 'Zona 2-Municipal', 'Zona 2-Municipal (Albacete)', 1),
(17, 'Zona 3-Villacerrada', 'Zona 3-Villacerrada (Albacete)', 1),
(18, 'Zona 4-Residencia', 'Zona 4-Residencia (Albacete)', 1),
(19, 'Zona 5', 'Zona 5 (Albacete)', 1),
(20, 'Zona V-B', 'Zona V-B (Albacete)', 1),
(21, 'Zona 6', 'Zona 6 (Albacete)', 1),
(22, 'Zona 7-Feria', 'Zona 7-Feria (Albacete)', 1),
(23, 'Zona 8', 'Zona 8 (Albacete)', 1);

-- ALMANSA
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(24, 'Almansa', 'Almansa (Albacete)', 2),
(25, 'Bonete', 'Bonete (Albacete)', 2),
(26, 'Caudete', 'Caudete (Albacete)', 2);

-- HELLIN
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(27, 'Elche de la Sierra', 'Elche de la Sierra (Albacete)', 3),
(28, 'Hellín 1', 'Hellín 1 (Albacete)', 3),
(29, 'Hellín 2', 'Hellín 2 (Albacete)', 3),
(30, 'Nerpio', 'Nerpio (Albacete)', 3),
(31, 'Ontur', 'Ontur (Albacete)', 3),
(32, 'Riopar', 'Riopar (Albacete)', 3),
(33, 'Socovos', 'Socovos (Albacete)', 3),
(34, 'Tobarra', 'Tobarra (Albacete)', 3),
(35, 'Yeste', 'Yeste (Albacete)', 3);

-- VILLARROBLEDO
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(36, 'El Bonillo', 'El Bonillo (Albacete)', 4),
(37, 'Ossa de Montiel', 'Ossa de Montiel (Albacete)', 4),
(38, 'Villarrobledo', 'Villarrobledo (Albacete)', 4),
(39, 'Munera', 'Munera (Albacete)', 4),
(40, 'Las Pedroñeras', ' ', 4),
(41, 'San Clemente', ' ', 4),
(42, 'Sisante', ' ', 4);

-- ALCAZAR DE SAN JUAN
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(43, 'Alcázar de San Juan 1', 'Alcázar de San Juan 1 (Ciudad Real)', 5),
(44, 'Alcázar de San Juan 2', 'Alcázar de San Juan 2 (Ciudad Real)', 5),
(45, 'Campo de Criptana', 'Campo de Criptana (Ciudad Real)', 5),
(46, 'Herencia', 'Herencia (Ciudad Real)', 5),
(47, 'Madridejos', 'Madridejos (Ciudad Real)', 5),
(48, 'Mota del Cuervo', 'Mota del Cuervo (Ciudad Real)', 5),
(49, 'Quintanar de la Orden', 'Quintanar de la Orden (Ciudad Real)', 5),
(50, 'Villacañas', 'Villacañas (Ciudad Real)', 5),
(51, 'Villafranca de los Caballeros', 'Villafranca de los Caballeros (Ciudad Real)', 5),
(52, 'Villarta de San Juan', 'Villarta de San Juan (Ciudad Real)', 5);

-- TOMELLOSO
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(53, 'Argamasilla de Alba', 'Argamasilla de Alba (Ciudad Real)', 6),
(54, 'Pedro Muñoz', 'Pedro Muñoz (Ciudad Real)', 6),
(55, 'Socuéllamos', 'Socuéllamos (Ciudad Real)', 6),
(56, 'Tomelloso 1', 'Tomelloso 1 (Ciudad Real)', 6),
(57, 'Tomelloso 2', 'Tomelloso 2 (Ciudad Real)', 6);

-- CIUDAD REAL
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(58, 'Abenójar', 'Abenójar (Ciudad Real)', 7),
(59, 'Agudo', 'Agudo (Ciudad Real)', 7),
(60, 'Alcoba de los Montes', 'Alcoba de los Montes (Ciudad Real)', 7),
(61, 'Almagro', 'Almagro (Ciudad Real)', 7),
(62, 'Bolaños', 'Bolaños (Ciudad Real)', 7),
(63, 'Calzada de Calatrava', 'Calzada de Calatrava (Ciudad Real)', 7),
(64, 'Carrión de Calatrava', 'Carrión de Calatrava (Ciudad Real)', 7),
(65, 'Ciudad Real 1', 'Ciudad Real 1 (Ciudad Real)', 7),
(66, 'Ciudad Real 2', 'Ciudad Real 2 (Ciudad Real)', 7),
(67, 'Ciudad Real 3', 'Ciudad Real 3 (Ciudad Real)', 7),
(68, 'Corral de Calatrava', 'Corral de Calatrava (Ciudad Real)', 7),
(69, 'Daimiel 1', 'Daimiel 1 (Ciudad Real)', 7),
(70, 'Daimiel II Cedt', 'Daimiel II Cedt (Ciudad Real)', 7),
(71, 'Malagón', 'Malagón (Ciudad Real)', 7),
(72, 'Miguelturra', 'Miguelturra (Ciudad Real)', 7),
(73, 'Piedrabuena', 'Piedrabuena (Ciudad Real)', 7),
(74, 'Porzuna', 'Porzuna (Ciudad Real)', 7),
(75, 'Retuerta del Bullaque', 'Retuerta del Bullaque (Ciudad Real)', 7),
(76, 'Villarrubia de los Ojos', 'Villarrubia de los Ojos (Ciudad Real)', 7);

-- MANZANARES
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(77, 'La Solana', 'La Solana (Ciudad Real)', 8),
(78, 'Manzanares I', 'Manzanares I (Ciudad Real)', 8),
(79, 'Manzanares II', 'Manzanares II (Ciudad Real)', 8);

-- VALDEPEÑAS
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(80, 'Albaladejo', 'Albaladejo (Ciudad Real)', 9),
(81, 'Moral de Calatrava', 'Moral de Calatrava (Ciudad Real)', 9),
(82, 'Santa Cruz de Mudela', 'Santa Cruz de Mudela (Ciudad Real)', 9),
(83, 'Torre de Juan Abad', 'Torre de Juan Abad (Ciudad Real)', 9),
(84, 'Valdepeñas I', 'Valdepeñas I (Ciudad Real)', 9),
(85, 'Valdepeñas II', 'Valdepeñas II (Ciudad Real)', 9),
(86, 'Villahermosa', 'Villahermosa (Ciudad Real)', 9),
(87, 'Villanueva de los Infantes', 'Villanueva de los Infantes (Ciudad Real)', 9);

-- CUENCA
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(88, 'Belmonte', 'Belmonte (Cuenca)', 10),
(89, 'Beteta', 'Beteta (Cuenca)', 10),
(90, 'Campillo de Altobuey', 'Campillo de Altobuey (Cuenca)', 10),
(91, 'Cañaveras', 'Cañaveras (Cuenca)', 10),
(92, 'Cañete', 'Cañete (Cuenca)', 10),
(93, 'Carboneras de Guadazaón', 'Carboneras de Guadazaón (Cuenca)', 10),
(94, 'Cardenete', 'Cardenete (Cuenca)', 10),
(95, 'Carrascosa del Campo', 'Carrascosa del Campo (Cuenca)', 10),
(96, 'Cuenca 1', 'Cuenca 1 (Cuenca)', 10),
(97, 'Cuenca 2', 'Cuenca 2 (Cuenca)', 10),
(98, 'Cuenca 3', 'Cuenca 3 (Cuenca)', 10),
(99, 'Honrubia', 'Honrubia (Cuenca)', 10),
(100, 'Horcajo de Santiago', 'Horcajo de Santiago (Cuenca)', 10),
(101, 'Huete', 'Huete (Cuenca)', 10),
(102, 'Landete', 'Landete (Cuenca)', 10),
(103, 'Minglanilla', 'Minglanilla (Cuenca)', 10),
(104, 'Mira', 'Mira (Cuenca)', 10),
(105, 'Montalbo', 'Montalbo (Cuenca)', 10),
(106, 'Motilla del Palancar', 'Motilla del Palancar (Cuenca)', 10),
(107, 'Priego', 'Priego (Cuenca)', 10),
(108, 'San Lorenzo de la Parrilla', 'San Lorenzo de la Parrilla (Cuenca)', 10),
(109, 'Talayuelas', 'Talayuelas (Cuenca)', 10),
(110, 'Tarancón', 'Tarancón (Cuenca)', 10),
(111, 'Torrejoncillo del Rey', 'Torrejoncillo del Rey (Cuenca)', 10),
(112, 'Tragacete', 'Tragacete (Cuenca)', 10),
(113, 'Valverde del Júcar', 'Valverde del Júcar (Cuenca)', 10),
(114, 'Villalba de la Sierra', 'Villalba de la Sierra (Cuenca)', 10),
(115, 'Villalba del Rey', 'Villalba del Rey (Cuenca)', 10),
(116, 'Villamayor de Santiago', 'Villamayor de Santiago (Cuenca)', 10),
(117, 'Villares del Saz', 'Villares del Saz (Cuenca)', 10),
(118, 'Villas de la Ventosa', 'Villas de la Ventosa (Cuenca)', 10);

-- GUADALAJARA
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(119, 'Alcolea del Pinar', 'Alcolea del Pinar (Guadalajara)', 11),
(120, 'Atienza', 'Atienza (Guadalajara)', 11),
(121, 'Azuqueca de Henares', 'Azuqueca de Henares (Guadalajara)', 11),
(122, 'Brihuega', 'Brihuega (Guadalajara)', 11),
(123, 'Cabanillas del Campo', 'Cabanillas del Campo (Guadalajara)', 11),
(124, 'Campiña', 'Campiña (Guadalajara)', 11),
(125, 'Checa zona especial', 'Checa zona especial (Guadalajara)', 11),
(126, 'Cifuentes', 'Cifuentes (Guadalajara)', 11),
(127, 'Cogolludo', 'Cogolludo (Guadalajara)', 11),
(128, 'El Casar de Talamanca', 'El Casar de Talamanca (Guadalajara)', 11),
(129, 'El Pobo de Dueñas', 'El Pobo de Dueñas (Guadalajara)', 11),
(130, 'Galve de Sorbe', 'Galve de Sorbe (Guadalajara)', 11),
(131, 'Guadalajara 1-Sur', 'Guadalajara 1-Sur (Guadalajara)', 11),
(132, 'Guadalajara 2-Balconcill', 'Guadalajara 2-Balconcill (Guadalajara)', 11),
(133, 'Guadalajara 3-Alamin', 'Guadalajara 3-Alamin (Guadalajara)', 11),
(134, 'Guadalajara 4-Cervantes', 'Guadalajara 4-Cervantes (Guadalajara)', 11),
(135, 'Guadalajara 5-Manantiales', 'Guadalajara 5-Manantiales (Guadalajara)', 11),
(136, 'Guadalajara-Periférica', 'Guadalajara-Periférica (Guadalajara)', 11),
(137, 'Hiendelaencina', 'Hiendelaencina (Guadalajara)', 11),
(138, 'Horche', 'Horche (Guadalajara)', 11),
(139, 'Jadraque', 'Jadraque (Guadalajara)', 11),
(140, 'Maranchón', 'Maranchón (Guadalajara)', 11),
(141, 'Molina de Aragón', 'Molina de Aragón (Guadalajara)', 11),
(142, 'Mondéjar', 'Mondéjar (Guadalajara)', 11),
(143, 'Pastrana', 'Pastrana (Guadalajara)', 11),
(144, 'Sacedón', 'Sacedón (Guadalajara)', 11),
(145, 'Sigüenza', 'Sigüenza (Guadalajara)', 11),
(146, 'Villanueva de Alcorón', 'Villanueva de Alcorón (Guadalajara)', 11),
(147, 'Yunquera de Henares', 'Yunquera de Henares (Guadalajara)', 11);

-- TALAVERA
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(148, 'Aldeanueva de San Bartolomé', 'Aldeanueva de San Bartolomé (Toledo)', 12),
(149, 'Belvis de la Jara', 'Belvis de la Jara (Toledo)', 12),
(150, 'Cebolla', 'Cebolla (Toledo)', 12),
(151, 'La Nava de Ricomalillo', 'La Nava de Ricomalillo (Toledo)', 12),
(152, 'La Pueblanueva', 'La Pueblanueva (Toledo)', 12),
(153, 'Los Navalmorales', 'Los Navalmorales (Toledo)', 12),
(154, 'Navamorcuende', 'Navamorcuende (Toledo)', 12),
(155, 'Oropesa', 'Oropesa (Toledo)', 12),
(156, 'Puente del Arzobispo', 'Puente del Arzobispo (Toledo)', 12),
(157, 'Santa Olalla', 'Santa Olalla (Toledo)', 12),
(158, 'Sierra de San Vicente', 'Sierra de San Vicente (Toledo)', 12),
(159, 'Talavera 1 Centro', 'Talavera 1 Centro (Toledo)', 12),
(160, 'Talavera 2 Estación', 'Talavera 2 Estación (Toledo)', 12),
(161, 'Talavera 3 La Solana', 'Talavera 3 La Solana (Toledo)', 12),
(162, 'Talavera 4 La Algodonera', 'Talavera 4 La Algodonera (Toledo)', 12),
(163, 'Talavera 5 Río Tajo', 'Talavera 5 Río Tajo (Toledo)', 12),
(164, 'Velada', 'Velada (Toledo)', 12);

-- TOLEDO
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(165, 'Añover de Tajo', 'Añover de Tajo (Toledo)', 13),
(166, 'Bargas', 'Bargas (Toledo)', 13),
(167, 'Camarena', 'Camarena (Toledo)', 13),
(168, 'Consuegra', 'Consuegra (Toledo)', 13),
(169, 'Corral de Almaguer', 'Corral de Almaguer (Toledo)', 13),
(170, 'Escalona', 'Escalona (Toledo)', 13),
(171, 'Esquivias', 'Esquivias (Toledo)', 13),
(172, 'Fuensalida', 'Fuensalida (Toledo)', 13),
(173, 'Illescas', 'Illescas (Toledo)', 13),
(174, 'La Puebla de Montalbán', 'La Puebla de Montalbán (Toledo)', 13),
(175, 'Los Yébenes', 'Los Yébenes (Toledo)', 13),
(176, 'Menasalbas', 'Menasalbas (Toledo)', 13),
(177, 'Mora', 'Mora (Toledo)', 13),
(178, 'Navahermosa Noblejas', 'Navahermosa Noblejas (Toledo)', 13),
(179, 'Ocaña', 'Ocaña (Toledo)', 13),
(180, 'Polán', 'Polán (Toledo)', 13),
(181, 'Santa Cruz de la Zarza', 'Santa Cruz de la Zarza (Toledo)', 13),
(182, 'Seseña', 'Seseña (Toledo)', 13),
(183, 'Sonseca', 'Sonseca (Toledo)', 13),
(184, 'Tembleque', 'Tembleque (Toledo)', 13),
(185, 'Toledo 1-Sillería', 'Toledo 1-Sillería (Toledo)', 13),
(186, 'Toledo 2-Palomarejos', 'Toledo 2-Palomarejos (Toledo)', 13),
(187, 'Toledo 3-Benquerencia', 'Toledo 3-Benquerencia (Toledo)', 13),
(188, 'Toledo 4-Santa Bárbara', 'Toledo 4-Santa Bárbara (Toledo)', 13),
(189, 'Toledo 5-Buenavista', 'Toledo 5-Buenavista (Toledo)', 13),
(190, 'Torrijos', 'Torrijos (Toledo)', 13),
(191, 'Valmojado', 'Valmojado (Toledo)', 13),
(192, 'Villaluenga de la Sagra', 'Villaluenga de la Sagra (Toledo)', 13),
(193, 'Yepes', 'Yepes (Toledo)', 13);

-- PUERTOLLANO
INSERT INTO zonaBasicaSalud (id_zona_basica_salud, nombre, cap, id_area_salud) VALUES
(194, 'Almadén', 'Almadén (Ciudad Real)', 14),
(195, 'Argamasilla de Calatrava', 'Argamasilla de Calatrava (Ciudad Real)', 14),
(196, 'Almodóvar del Campo', 'Almodóvar del Campo (Ciudad Real)', 14),
(197, 'Fuencaliente', 'Fuencaliente (Ciudad Real)', 14),
(198, 'Puertollano 1 – Barataria', 'Puertollano 1 – Barataria (Ciudad Real)', 14),
(199, 'Puertollano 2', 'Puertollano 2 (Ciudad Real)', 14),
(200, 'Puertollano 3 – Carlos Mestre', 'Puertollano 3 – Carlos Mestre (Ciudad Real)', 14),
(201, 'Puertollano 4', 'Puertollano 4 (Ciudad Real)', 14),
(202, 'Solana del Pino', 'Solana del Pino (Ciudad Real)', 14);

INSERT INTO comunidadautonoma (id_cautonoma, nombre)
VALUES
(0, ' '),
(1, 'Andalucía'),
(2, 'Aragón'),
(3, 'Asturias'),
(4, 'Cantabria'),
(5, 'Castilla-La Mancha'),
(6, 'Castilla y León'),
(7, 'Cataluña'),
(8, 'Ceuta'),
(9, 'Extremadura'),
(10, 'Galicia'),
(11, 'Islas Baleares'),
(12, 'Islas Canarias'),
(13, 'La Rioja'),
(14, 'Madrid'),
(15, 'Melilla'),
(16, 'Murcia'),
(17, 'Navarra'),
(18, 'Comunidad Valenciana'),
(19, 'País Vasco');

-- Andalucia
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(1, 'Almería', 1),
(2, 'Cádiz', 1),
(3, 'Córdoba', 1),
(4, 'Granada', 1),
(5, 'Huelva', 1),
(6, 'Jaén', 1),
(7, 'Málaga', 1),
(8, 'Sevilla', 1);

-- Aragon
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(9, 'Huesca', 2),
(10, 'Teruel', 2),
(11, 'Zaragoza', 2);

-- Asturias
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(12, 'Asturias', 3);

-- Cantabria
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(13, 'Cantabria', 4);

-- Castilla-La Mancha
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(14, 'Albacete', 5),
(15, 'Ciudad Real', 5),
(16, 'Cuenca', 5),
(17, 'Guadalajara', 5),
(18, 'Toledo', 5);

-- Castilla y Leon
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(19, 'Ávila', 6),
(20, 'Burgos', 6),
(21, 'León', 6),
(22, 'Palencia', 6),
(23, 'Salamanca', 6),
(24, 'Segovia', 6),
(25, 'Soria', 6),
(26, 'Valladolid', 6),
(27, 'Zamora', 6);

-- Cataluña
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(28, 'Barcelona', 7),
(29, 'Girona', 7),
(30, 'Lleida', 7),
(31, 'Tarragona', 7);

-- Ceuta
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(32, 'Ceuta', 8);

-- Extremadura
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(33, 'Badajoz', 9),
(34, 'Cáceres', 9);

-- Galicia
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(35, 'A Coruña', 10),
(36, 'Lugo', 10),
(37, 'Ourense', 10),
(38, 'Pontevedra', 10);

-- Islas Baleares
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(39, 'Illes Balears', 11);

-- Islas Canarias
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(40, 'Las Palmas', 12),
(41, 'Santa Cruz de Tenerife', 12);

-- La Rioja
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(42, 'La Rioja', 13);

-- Madrid
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(43, 'Madrid', 14);

-- Melilla
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(44, 'Melilla', 15);

-- Murcia
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(45, 'Murcia', 16);

-- Navarra
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(46, 'Navarra', 17);

-- Comunidad Valenciana
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(47, 'Alicante', 18),
(48, 'Castellón', 18),
(49, 'Valencia', 18);

-- Pais Vasco
INSERT INTO provincia (id_provincia, nombre, id_cautonoma)
VALUES
(50, 'Álava', 19),
(51, 'Gipuzkoa', 19),
(52, 'Bizkaia', 19);

INSERT INTO centros (id_centro, nombre, id_area_salud) VALUES
(1, 'Complejo Hospitalario Universitario de Albacete', 1),
(2, 'Hospital general de Almansa', 2),
(3, 'Hospital de Hellín', 3),
(4, 'Hospital General de Villarrobledo', 4),
(5, 'Hospital General La Mancha Centro', 5),
(6, 'Hospital General de Tomelloso', 6),
(7, 'Hospital General Universitario de Ciudad Real', 7),
(8, 'Hospital Virgen de Altagracia', 8),
(9, 'Hospital Gutiérrez Ortega', 9),
(10, 'Hospital Virgen de la Luz', 10),
(11, 'Hospital General Universitario de Guadalajara', 11),
(12, 'Hospital Nuestra Señora del Prado', 12),
(13, 'Hospital Universitario de Toledo', 13),
(14, 'Hospital de Santa Bárbara', 14);

-- Planta 1
INSERT INTO camas (id_cama, id_planta, id_habitacion, letra, bloqueada, estado, id_centro)
VALUES
(1, 1, '01', 'A', 'N', 'Disponible', 10),
(2, 1, '01', 'B', 'N', 'Disponible', 10),
(3, 1, '02', 'A', 'N', 'Disponible', 10),
(4, 1, '02', 'B', 'N', 'Disponible', 10),
(5, 1, '03', 'A', 'N', 'Disponible', 10),
(6, 1, '03', 'B', 'N', 'Disponible', 10),
(7, 1, '04', 'A', 'N', 'Disponible', 10),
(8, 1, '04', 'B', 'N', 'Disponible', 10),
(9, 1, '05', 'A', 'N', 'Disponible', 10),
(10, 1, '05', 'B', 'N', 'Disponible', 10),
(11, 1, '06', 'A', 'N', 'Disponible', 10),
(12, 1, '06', 'B', 'N', 'Disponible', 10),
(13, 1, '07', 'A', 'N', 'Disponible', 10),
(14, 1, '07', 'B', 'N', 'Disponible', 10),
(15, 1, '08', 'A', 'N', 'Disponible', 10),
(16, 1, '08', 'B', 'N', 'Disponible', 10),
(17, 1, '09', 'A', 'N', 'Disponible', 10),
(18, 1, '09', 'B', 'N', 'Disponible', 10),
(19, 1, '10', 'A', 'N', 'Disponible', 10),
(20, 1, '10', 'B', 'N', 'Disponible', 10);

-- Planta 2
INSERT INTO camas (id_cama, id_planta, id_habitacion, letra, bloqueada, estado, id_centro)
VALUES
(21, 2, '01', 'A', 'N', 'Disponible', 10),
(22, 2, '01', 'B', 'N', 'Disponible', 10),
(23, 2, '02', 'A', 'N', 'Disponible', 10),
(24, 2, '02', 'B', 'N', 'Disponible', 10),
(25, 2, '03', 'A', 'N', 'Disponible', 10),
(26, 2, '03', 'B', 'N', 'Disponible', 10),
(27, 2, '04', 'A', 'N', 'Disponible', 10),
(28, 2, '04', 'B', 'N', 'Disponible', 10),
(29, 2, '05', 'A', 'N', 'Disponible', 10),
(30, 2, '05', 'B', 'N', 'Disponible', 10),
(31, 2, '06', 'A', 'N', 'Disponible', 10),
(32, 2, '06', 'B', 'N', 'Disponible', 10),
(33, 2, '07', 'A', 'N', 'Disponible', 10),
(34, 2, '07', 'B', 'N', 'Disponible', 10),
(35, 2, '08', 'A', 'N', 'Disponible', 10),
(36, 2, '08', 'B', 'N', 'Disponible', 10),
(37, 2, '09', 'A', 'N', 'Disponible', 10),
(38, 2, '09', 'B', 'N', 'Disponible', 10),
(39, 2, '10', 'A', 'N', 'Disponible', 10),
(40, 2, '10', 'B', 'N', 'Disponible', 10);

-- Planta 3
INSERT INTO camas (id_cama, id_planta, id_habitacion, letra, bloqueada, estado, id_centro)
VALUES
(41, 3, '01', 'A', 'N', 'Disponible', 10),
(42, 3, '01', 'B', 'N', 'Disponible', 10),
(43, 3, '02', 'A', 'N', 'Disponible', 10),
(44, 3, '02', 'B', 'N', 'Disponible', 10),
(45, 3, '03', 'A', 'N', 'Disponible', 10),
(46, 3, '03', 'B', 'N', 'Disponible', 10),
(47, 3, '04', 'A', 'N', 'Disponible', 10),
(48, 3, '04', 'B', 'N', 'Disponible', 10),
(49, 3, '05', 'A', 'N', 'Disponible', 10),
(50, 3, '05', 'B', 'N', 'Disponible', 10),
(51, 3, '06', 'A', 'N', 'Disponible', 10),
(52, 3, '06', 'B', 'N', 'Disponible', 10),
(53, 3, '07', 'A', 'N', 'Disponible', 10),
(54, 3, '07', 'B', 'N', 'Disponible', 10),
(55, 3, '08', 'A', 'N', 'Disponible', 10),
(56, 3, '08', 'B', 'N', 'Disponible', 10),
(57, 3, '09', 'A', 'N', 'Disponible', 10),
(58, 3, '09', 'B', 'N', 'Disponible', 10),
(59, 3, '10', 'A', 'N', 'Disponible', 10),
(60, 3, '10', 'B', 'N', 'Disponible', 10);

-- Planta 4
INSERT INTO camas (id_cama, id_planta, id_habitacion, letra, bloqueada, estado, id_centro)
VALUES
(61, 4, '01', 'A', 'N', 'Disponible', 10),
(62, 4, '01', 'B', 'N', 'Disponible', 10),
(63, 4, '02', 'A', 'N', 'Disponible', 10),
(64, 4, '02', 'B', 'N', 'Disponible', 10),
(65, 4, '03', 'A', 'N', 'Disponible', 10),
(66, 4, '03', 'B', 'N', 'Disponible', 10),
(67, 4, '04', 'A', 'N', 'Disponible', 10),
(68, 4, '04', 'B', 'N', 'Disponible', 10),
(69, 4, '05', 'A', 'N', 'Disponible', 10),
(70, 4, '05', 'B', 'N', 'Disponible', 10),
(71, 4, '06', 'A', 'N', 'Disponible', 10),
(72, 4, '06', 'B', 'N', 'Disponible', 10),
(73, 4, '07', 'A', 'N', 'Disponible', 10),
(74, 4, '07', 'B', 'N', 'Disponible', 10),
(75, 4, '08', 'A', 'N', 'Disponible', 10),
(76, 4, '08', 'B', 'N', 'Disponible', 10),
(77, 4, '09', 'A', 'N', 'Disponible', 10),
(78, 4, '09', 'B', 'N', 'Disponible', 10),
(79, 4, '10', 'A', 'N', 'Disponible', 10),
(80, 4, '10', 'B', 'N', 'Disponible', 10);

-- Resto hospitales
INSERT INTO camas (id_cama, id_planta, id_habitacion, letra, bloqueada, estado, id_centro)
VALUES
(81, 1, '01', 'A', 'N', 'Disponible', 1),
(82, 1, '01', 'B', 'N', 'Disponible', 1),
(83, 2, '01', 'A', 'N', 'Disponible', 1),
(84, 2, '01', 'B', 'N', 'Disponible', 1),
(85, 1, '01', 'A', 'N', 'Disponible', 2),
(86, 1, '01', 'B', 'N', 'Disponible', 2),
(87, 2, '01', 'A', 'N', 'Disponible', 2),
(88, 2, '01', 'B', 'N', 'Disponible', 2),
(89, 1, '01', 'A', 'N', 'Disponible', 3),
(90, 1, '01', 'B', 'N', 'Disponible', 3),
(91, 2, '01', 'A', 'N', 'Disponible', 3),
(92, 2, '01', 'B', 'N', 'Disponible', 3),
(93, 1, '01', 'A', 'N', 'Disponible', 4),
(94, 1, '01', 'B', 'N', 'Disponible', 4),
(95, 2, '01', 'A', 'N', 'Disponible', 4),
(96, 2, '01', 'B', 'N', 'Disponible', 4),
(97, 1, '01', 'A', 'N', 'Disponible', 5),
(98, 1, '01', 'B', 'N', 'Disponible', 5),
(99, 2, '01', 'A', 'N', 'Disponible', 5),
(100, 2, '01', 'B', 'N', 'Disponible', 5),
(101, 1, '01', 'A', 'N', 'Disponible', 6),
(102, 1, '01', 'B', 'N', 'Disponible', 6),
(103, 2, '01', 'A', 'N', 'Disponible', 6),
(104, 2, '01', 'B', 'N', 'Disponible', 6),
(105, 1, '01', 'A', 'N', 'Disponible', 7),
(106, 1, '01', 'B', 'N', 'Disponible', 7),
(107, 2, '01', 'A', 'N', 'Disponible', 7),
(108, 2, '01', 'B', 'N', 'Disponible', 7),
(109, 1, '01', 'A', 'N', 'Disponible', 8),
(110, 1, '01', 'B', 'N', 'Disponible', 8),
(111, 2, '01', 'A', 'N', 'Disponible', 8),
(112, 2, '01', 'B', 'N', 'Disponible', 8),
(113, 1, '01', 'A', 'N', 'Disponible', 9),
(114, 1, '01', 'B', 'N', 'Disponible', 9),
(115, 2, '01', 'A', 'N', 'Disponible', 9),
(116, 2, '01', 'B', 'N', 'Disponible', 9),
(121, 1, '01', 'A', 'N', 'Disponible', 11),
(122, 1, '01', 'B', 'N', 'Disponible', 11),
(123, 2, '01', 'A', 'N', 'Disponible', 11),
(124, 2, '01', 'B', 'N', 'Disponible', 11),
(125, 1, '01', 'A', 'N', 'Disponible', 12),
(126, 1, '01', 'B', 'N', 'Disponible', 12),
(127, 2, '01', 'A', 'N', 'Disponible', 12),
(128, 2, '01', 'B', 'N', 'Disponible', 12),
(129, 1, '01', 'A', 'N', 'Disponible', 13),
(130, 1, '01', 'B', 'N', 'Disponible', 13),
(131, 2, '01', 'A', 'N', 'Disponible', 13),
(132, 2, '01', 'B', 'N', 'Disponible', 13),
(133, 1, '01', 'A', 'N', 'Disponible', 14),
(134, 1, '01', 'B', 'N', 'Disponible', 14),
(135, 2, '01', 'A', 'N', 'Disponible', 14),
(136, 2, '01', 'B', 'N', 'Disponible', 14);

-- USUARIO ADMINISTRADOR
INSERT INTO usuarios (id_usuario, nombre, apellidos, email, id_rol, password, ID_CENTRO)
VALUES (1, 'admin', 'admin', 'admin', '1', '1234', 10);

-- EJEMPLO PACIENTES
INSERT INTO pacientes (NIF, NumeroSS, Nombre, Apellido1, Apellido2, Sexo, FechaNacimiento, Telefono1, Telefono2, Movil, EstadoCivil, Estudios,
  Fallecido, Nacionalidad, CAutonoma, Provincia, Poblacion, CP, Direccion, Titular, Regimen, TIS, Medico, CAP, Zona, Area, Nacimiento,
  CAutonomaNacimiento, ProvinciaNacimiento, PoblacionNacimiento) VALUES 
('11111111A', 1600000001, 'Maria', 'Lopez', 'Ruiz', 'Mujer', '1994-12-31', NULL, NULL, NULL, 'Soltero/a', NULL, 'No', 'Nacional', 
	'Castilla-La Mancha', 'Cuenca', 'Cuenca', 16003, NULL, '', 'Seguridad Social', 'RL311200000001', 'Roberto Blanco Grande', 
    'Cuenca 2 (Cuenca)', 'Cuenca 2', 'Cuenca', 'Nacional', 'Castilla-La Mancha', 'Albacete', 'Albacete'),
('22222222B', 1600000002, 'Juan', 'Gomez', 'Perez', 'Hombre', '2003-02-01', NULL, NULL, NULL, 'Soltero/a', NULL, 'No', 'Nacional', 
	'Castilla-La Mancha', 'Toledo', 'Toledo', 45001, NULL, '', 'Seguridad Social', 'PG010200000001', 'Pedro Rojas Peña', 
    'Cuenca 2 (Cuenca)', 'Cuenca 2', 'Cuenca', 'Nacional', 'Castilla-La Mancha', 'Toledo', 'Toledo'),
('33333333C', 1600000003, 'Rodrigo', 'Garcia', 'Rubio', 'Hombre', '1985-09-13', NULL, NULL, NULL, 'Casado/a', NULL, 'No', 'Nacional', 
	'Castilla-La Mancha', 'Guadalajara', 'Guadalajara', 19001, NULL, '', 'Seguridad Social', 'RG130900000001', 'Pedro Rojas Peña', 
    'Cuenca 2 (Cuenca)', 'Cuenca 2', 'Cuenca', 'Nacional', 'Castilla-La Mancha', 'Guadalajara', 'Guadalajara'
);
