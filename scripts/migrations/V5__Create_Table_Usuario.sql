IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Usuario' and xtype='U')
    CREATE TABLE Usuario (
		Id uniqueidentifier not null primary key,
        Login varchar(50) unique not null,
		Senha varchar(100) not null,
		Codigo varchar(8) unique not null,
		NomeCompleto varchar(200) not null,
		RefreshToken varchar(500) null,
		RefreshTokenExpiryTime datetime null,
		Ativo bit not null,
		CodigoUsuarioCadastro varchar(10) not null,
        NomeUsuarioCadastro varchar(100) not null,
        DataCadastro datetime not null,
		CodigoUsuarioAlteracao varchar(10) null,
        NomeUsuarioAlteracao varchar(100) null,
        DataAlteracao datetime null
    )