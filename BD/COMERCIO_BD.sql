USE MASTER 
GO
CREATE DATABASE COMERCIO
GO
USE COMERCIO 
GO
create table Marca
(
	idMarca int not null primary key identity(1,1),
	Nombre varchar(20) not null,
)
go
create table Categoria
(
	idCategoria int not null primary key identity(1,1),
	Nombre varchar(20) not null,
)
go
drop table Articulos
create table Articulos
(
	Id int not null primary key identity(1,1),
	Modelo varchar(20) not null,
	Descripcion varchar(50) null,
	IdMarca int not null foreign key references marca(idMarca),
	IdCategoria int not null foreign key references categoria(idCategoria),
	Imagen varchar(100) null,
	Costo float null,
	Precio float not null,
	Dolar bit not null,
	Iva float not null,
)
insert into Articulos (Modelo,Descripcion,IdMarca,IdCategoria,Imagen,Costo,Precio,Dolar,Iva)
values ('bg178','i5 8gb',1,1,'C:\Users\Rafa\source\repos\comercio\Imagenes\Articulos\note.jpg',300,400,1,10.5)
insert into Articulos (Modelo,Descripcion,IdMarca,IdCategoria,Imagen,Costo,Precio,Dolar,Iva)
values ('7 plus','12px,touchid',2,3,'C:\Users\Rafa\source\repos\comercio\Imagenes\Articulos\iphone7.jpg',700,900,1,21)

insert into Marca(Nombre)
values('samsung')
insert into Marca(Nombre)
values('apple')

insert into Categoria
values ('notebook')
insert into Categoria
values ('pc')
insert into Categoria
values ('smartphone')


	Select A.Id,A.Modelo,A.Descripcion,M.IdMarca,C.IdCategoria,A.Imagen,A.Costo,A.Precio,Dolar,Iva from articulos A, marca m,categoria c  where A.idMarca=M.idMarca and A.IdCategoria=C.idCategoria
