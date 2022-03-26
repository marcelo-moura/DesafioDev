IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Pedido' and xtype='U')
    CREATE TABLE Pedido (
		Id uniqueidentifier not null primary key,
        Codigo varchar(15) not null,
		UsuarioId uniqueidentifier not null CONSTRAINT [FK_Pedido_Usuario] FOREIGN KEY([UsuarioId]) REFERENCES [dbo].[Usuario] ([Id]),
		ValorTotal decimal(18,2) not null,
        PedidoStatus int not null,
		CodigoUsuarioCadastro varchar(10) not null,
        NomeUsuarioCadastro varchar(100) not null,
        DataCadastro datetime not null
    )