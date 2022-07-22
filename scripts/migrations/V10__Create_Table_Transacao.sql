IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Transacao' and xtype='U')
    CREATE TABLE Transacao (
		Id uniqueidentifier not null primary key,
		PedidoId uniqueidentifier not null CONSTRAINT [FK_Transacao_Pedido] FOREIGN KEY([PedidoId]) REFERENCES [dbo].[Pedido] ([Id]),
		PagamentoId uniqueidentifier not null CONSTRAINT [FK_Transacao_Pagamento] FOREIGN KEY([PagamentoId]) REFERENCES [dbo].[Pagamento] ([Id]),
		Total decimal(18,2) not null,
		StatusTransacao int not null
    )