IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PedidoItem' and xtype='U')
    CREATE TABLE PedidoItem (
		Id uniqueidentifier not null primary key,
		PedidoId uniqueidentifier not null,
		ProdutoId uniqueidentifier not null,
        NomeProduto varchar(300) not null,
		Quantidade int not null,
		ValorUnitario decimal not null		
    )