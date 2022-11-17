IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='sp_ListarProdutosVitrine' and xtype='P')
	BEGIN
		EXEC('CREATE PROCEDURE [dbo].[sp_ListarProdutosVitrine]	
				@SortOrder int,
				@SortDirection varchar(10)
				AS
		
				IF  @SortOrder NOT IN (1) OR @SortDirection NOT IN (''asc''' + ', ' + '''desc'')
				BEGIN
				  RAISERROR(''Parâmetros inválidos!'', 1, 1);
				  RETURN;
				END

				SELECT 
					p.Id,
					p.Nome,
					p.Descricao,
					p.Preco,
					p.Quantidade,
					p.Ativo,
					p.CodigoUsuarioCadastro,
					p.NomeUsuarioCadastro,
					p.DataCadastro,
					p.CodigoUsuarioAlteracao,
					p.NomeUsuarioAlteracao,
					p.DataAlteracao,
					p.CategoriaId,
					c.Nome as NomeCategoria
				FROM Produto p
				JOIN Categoria c
				ON c.Id = p.CategoriaId

				ORDER BY 
					CASE WHEN @SortDirection = ''asc'' AND @SortOrder  = 1 THEN p.Nome END,
		
					CASE WHEN @SortDirection = ''desc'' AND @SortOrder  = 1 THEN p.Nome END DESC;'
			)
	END