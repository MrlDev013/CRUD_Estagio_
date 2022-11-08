CREATE DATABASE CRUDClient
GO

CREATE TABLE CLIENT
(
	ClientID int PRIMARY KEY IDENTITY,
	Nome varchar(50),
	Email varchar(30),
	Telefone varchar(50),
	CPF varchar(30)
)
GO

SELECT * FROM CLIENT
