IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Pagamento' and xtype='U')
    CREATE TABLE Pagamento (
		Id uniqueidentifier not null primary key,
		PedidoId uniqueidentifier not null CONSTRAINT [FK_Pagamento_Pedido] FOREIGN KEY([PedidoId]) REFERENCES [dbo].[Pedido] ([Id]),
		Valor decimal(18,2) not null,
		Parcelas integer not null,
		NomeCartao varchar(250) not null,
		NumeroCartao varchar(16) not null,
		ExpiracaoCartao varchar(10) not null,
		CvvCartao varchar(4) not null
    )