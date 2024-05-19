CREATE DATABASE dbPuellaWallet
GO

USE dbPuellaWallet
GO

---[Tabla usuario]---
CREATE TABLE tbl_User(
	IdUser INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	UserName VARCHAR(100) NOT NULL,
	UserAge INT NOT NULL,
	UserEMail VARCHAR(50) NOT NULL
)
GO

INSERT INTO tbl_User VALUES('Juan', 17, 'juan@hotmail.com');
GO

CREATE OR ALTER PROC dbo.spUser_Insert --INSERT
(@UserName VARCHAR(100), @UserAge INT, @UserEMail VARCHAR(50))
AS
BEGIN
	INSERT INTO tbl_User VALUES(@UserName, @UserAge, @UserEMail)
END
GO

CREATE OR ALTER PROC dbo.spUser_Update --Update
(@IdUser INT, @UserName VARCHAR(100), @UserAge INT, @UserEMail VARCHAR(50))
AS
BEGIN
	UPDATE tbl_User SET UserName = @UserName, UserAge = @UserAge, UserEMail = @UserEMail WHERE IdUser = @IdUser 
END
GO

CREATE OR ALTER PROC dbo.spUser_Delete
(@IdUser INT)
AS
BEGIN
	DELETE FROM tbl_User WHERE IdUser = @IdUser
END
GO

CREATE OR ALTER PROC dbo.spUser_GetAll
AS
BEGIN
	SELECT IdUser, UserName, UserAge, UserEMail FROM tbl_User
END
GO

CREATE OR ALTER PROC dbo.spUser_GetById
(@IdUser INT)
AS
BEGIN
	SELECT IdUser, UserName, UserAge, UserEMail FROM tbl_User WHERE IdUser = @IdUser
END
GO

---[Tabla Wallet]---
CREATE TABLE tbl_Wallet(
	IdWallet INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	WalletUSD MONEY NOT NULL,
	WalletBTC FLOAT,
	IdUser INT NOT NULL,

	FOREIGN KEY (IdUser) REFERENCES tbl_User(IdUser)
)
GO

INSERT INTO tbl_Wallet(WalletUSD, IdUser) VALUES(5000, 1);
GO

CREATE OR ALTER PROC dbo.spWallet_Insert --INSERT
(@WalletUSD MONEY, @IdUser INT)
AS
BEGIN
	--Suponiendo que 1 BTC = 65000 USD
	INSERT INTO tbl_Wallet VALUES (@WalletUSD, (@WalletUSD/65000), @IdUser)
END
GO

CREATE OR ALTER PROC dbo.spWallet_Update --Update
(@IdWallet INT, @WalletUSD MONEY, @IdUser INT)
AS
BEGIN
	DECLARE @WalletBTC FLOAT
	--Actualizando USD
	UPDATE tbl_Wallet SET WalletUSD = @WalletUSD
	WHERE IdWallet = @IdWallet 

	SELECT @WalletBTC = (WalletUSD/65000) FROM tbl_Wallet WHERE IdWallet = @IdWallet
	
	--Actualizando BTC
	UPDATE tbl_Wallet SET WalletBTC = @WalletBTC
	WHERE IdWallet = @IdWallet 
END
GO

CREATE OR ALTER PROC dbo.spWallet_Delete --Delete
(@IdWallet INT)
AS
BEGIN
	DELETE FROM tbl_Wallet WHERE IdWallet = @IdWallet
END
GO

CREATE OR ALTER PROC dbo.spWallet_GetAll --GetAll
AS
BEGIN
	SELECT IdWallet, U.userName, WalletUSD, WalletBTC  FROM tbl_Wallet W
	INNER JOIN tbl_User U on W.IdUser = U.IdUser
END
GO

CREATE OR ALTER PROC dbo.spWallet_GetById --GetById
(@IdWallet INT)
AS
BEGIN
	SELECT IdWallet, U.userName, WalletUSD, WalletBTC  FROM tbl_Wallet W
	INNER JOIN tbl_User U on W.IdUser = U.IdUser
	WHERE IdWallet = @IdWallet
END
GO

---[Tabla Transaction]---
CREATE TABLE tbl_Transaction(
	IdTransaction INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	TransactionInfo VARCHAR(50) NOT NULL,
	TransactionUSD MONEY NOT NULL,
	TransactionBTC FLOAT,
	IdWallet INT,

	FOREIGN KEY (IdWallet) REFERENCES tbl_Wallet(IdWallet)
)
GO

CREATE OR ALTER PROC dbo.spTransaction_Insert --INSERT
(@TransactionInfo VARCHAR(50), @TransactionUSD MONEY, @IdWallet INT)
AS
BEGIN
	--Suponiendo que 1 BTC = 65000 USD
	INSERT INTO tbl_Transaction VALUES(@TransactionInfo, @TransactionUSD, (@TransactionUSD/65000), @IdWallet)
	
	--Suponiendo que la transacción es una recarga de fondos
	UPDATE tbl_Wallet SET WalletUSD += @TransactionUSD, WalletBTC += (@TransactionUSD/65000)
	WHERE IdWallet = @IdWallet
END
GO

CREATE OR ALTER PROC dbo.spTransaction_Update --Update
(@IdTransaction INT, @TransactionInfo VARCHAR(50), @TransactionUSD MONEY, @IdWallet INT)
AS
BEGIN
	--Actualizando USD
	UPDATE tbl_Transaction SET TransactionInfo = @TransactionInfo, TransactionUSD = @TransactionUSD,
	TransactionBTC = (@TransactionUSD/65000), IdWallet = @IdWallet WHERE IdTransaction = @IdTransaction
	
	--Suponiendo que la transacción es una recarga de fondos

	UPDATE tbl_Wallet SET WalletUSD += @TransactionUSD, WalletBTC += (@TransactionUSD/65000)
	WHERE IdWallet = @IdWallet

END
GO

CREATE OR ALTER PROC dbo.spTransaction_Delete --Delete
(@IdTransaction INT)
AS
BEGIN
	DELETE FROM tbl_Transaction WHERE IdTransaction = @IdTransaction
END
GO

CREATE OR ALTER PROC dbo.spTransaction_GetAll --GetAll
AS
BEGIN
	SELECT IdTransaction, U.UserName, TransactionInfo, TransactionUSD, TransactionBTC, T.IdWallet  FROM tbl_Transaction T
	INNER JOIN tbl_Wallet W ON W.IdWallet = T.IdWallet
	INNER JOIN tbl_User U on W.IdUser = U.IdUser
END
GO

CREATE OR ALTER PROC dbo.spTransaction_GetById --GetById
(@IdTransaction INT)
AS
BEGIN
	SELECT IdTransaction, U.UserName, TransactionInfo, TransactionUSD, TransactionBTC, T.IdWallet  FROM tbl_Transaction T
	INNER JOIN tbl_Wallet W ON W.IdWallet = T.IdWallet
	INNER JOIN tbl_User U on W.IdUser = U.IdUser
	WHERE IdTransaction = @IdTransaction
END
GO


