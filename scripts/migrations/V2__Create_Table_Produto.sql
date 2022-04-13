IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Produto' and xtype='U')
    CREATE TABLE Produto (
		Id uniqueidentifier not null primary key,
        Nome varchar(100) not null,
		Descricao varchar(200) not null,
		Ativo bit not null,
		Preco decimal(18,2) not null,
		Quantidade integer not null,
		CategoriaId uniqueidentifier CONSTRAINT [FK_Produto_Categoria] FOREIGN KEY([CategoriaId]) REFERENCES [dbo].[Categoria] ([Id]),
		CodigoUsuarioCadastro varchar(10) not null,
        NomeUsuarioCadastro varchar(100) not null,
        DataCadastro datetime not null,
		CodigoUsuarioAlteracao varchar(10) null,
        NomeUsuarioAlteracao varchar(100) null,
        DataAlteracao datetime null
    )