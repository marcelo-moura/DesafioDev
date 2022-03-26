IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PedidoItem' and xtype='U')
    CREATE TABLE PedidoItem (
		Id uniqueidentifier not null primary key,
		PedidoId uniqueidentifier not null CONSTRAINT [FK_PedidoItem_Pedido] FOREIGN KEY([PedidoId]) REFERENCES [dbo].[Pedido] ([Id]),
		ProdutoId uniqueidentifier not null CONSTRAINT [FK_PedidoItem_Produto] FOREIGN KEY([ProdutoId]) REFERENCES [dbo].[Produto] ([Id]),
        NomeProduto varchar(300) not null,
		Quantidade int not null,
		ValorUnitario decimal(18,2) not null		
    )