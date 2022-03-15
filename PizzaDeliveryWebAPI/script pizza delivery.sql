use master
go

create database pizzadelivery;
go

use pizzadelivery;
go

create table cliente(
	idCliente int primary key identity(1,1),
	primerNombre varchar(20) not null,
	segundoNombre varchar(20) null,
	primerApellido varchar(20) not null,
	segundoApellido varchar(20) null,
	noTelefono varchar(8) not null,
);

create table usuario(
	idUsuario int primary key identity(1,1),
	email varchar(50) not null unique,
	contrasenia varbinary(max) not null,
	fechaUltimoAcceso datetime null
)