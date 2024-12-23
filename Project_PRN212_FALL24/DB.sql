Create table SettingName(
	ID int IDENTITY(1,1) PRIMARY KEY,
	[Name] nvarchar(256) not null
);

Create table Setting(
	ID int Identity(1,1) Primary key,
	[Type] int not null ,
	Name NVARCHAR(256) NOT NULL,
	CONSTRAINT FK_Setting_Type FOREIGN KEY ([Type]) REFERENCES [SettingName](ID)
);
Create table Account (
	ID int Identity(1,1) Primary key,
	Email nvarchar(256) not null,
	[Password] nvarchar(256) not null ,
	[Role] int not null ,
	CONSTRAINT FK_Account_Role FOREIGN KEY ([Role]) REFERENCES Setting(ID)
);
Create table Book(
	ID int Identity(1,1) Primary key,
	[Name] nvarchar(512) not null,
	[Type] int not null ,
	[Image] nvarchar(256) not null,
	CreateBy int not null ,
	DateCreate date NOT NULL ,
	DateModify date ,
	Quantity int not null ,
	Description nvarchar(256),
	Author nvarchar(64) NOT NULL,
	CONSTRAINT FK_Book_Type FOREIGN KEY ([Type]) REFERENCES Setting(ID),
	CONSTRAINT FK_Book_CreateBy FOREIGN KEY (CreateBy) REFERENCES Account(ID)
);

Create table History(
	ID int Identity(1,1) Primary key,
	IDAccount int not null ,
	IDBook int not null ,
	BookLoanDate date NOT NULL,
	BookReturnDate date,
	Deadline date NOT NULL,
	CONSTRAINT FK_History_IDAccount FOREIGN KEY (IDAccount) REFERENCES Account(ID),
	CONSTRAINT FK_History_IDBook FOREIGN KEY (IDBook) REFERENCES Book(ID),
);
