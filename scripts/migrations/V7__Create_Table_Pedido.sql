IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Pedido' and xtype='U')
    CREATE TABLE Pedido (
		Id uniqueidentifier not null primary key,
        Codigo int not null,
		UsuarioId uniqueidentifier not null,
		ValorTotal decimal not null,
        PedidoStatus int not null,
		CodigoUsuarioCadastro varchar(10) not null,
        NomeUsuarioCadastro varchar(100) not null,
        DataCadastro datetime not null
    )