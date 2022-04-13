IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categoria' and xtype='U')
    CREATE TABLE Categoria (
		Id uniqueidentifier not null primary key,
		Codigo integer not null,
        Nome varchar(100) not null,
		Ativo bit not null,
		CodigoUsuarioCadastro varchar(10) not null,
        NomeUsuarioCadastro varchar(100) not null,
        DataCadastro datetime not null,
		CodigoUsuarioAlteracao varchar(10) null,
        NomeUsuarioAlteracao varchar(100) null,
        DataAlteracao datetime null
    )