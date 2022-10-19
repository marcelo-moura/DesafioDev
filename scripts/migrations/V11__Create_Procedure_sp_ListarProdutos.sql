IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='sp_ListarProdutos' and xtype='P')
	BEGIN
		EXEC('CREATE PROCEDURE [dbo].[sp_ListarProdutos]	
				@SortOrder int,
				@SortDirection varchar(10)
				AS
		
				IF  @SortOrder NOT IN (1) OR @SortDirection NOT IN (''asc''' + ', ' + '''desc'')
				BEGIN
				  RAISERROR(''Parâmetros inválidos!'', 1, 1);
				  RETURN;
				END

				SELECT 
					*
				FROM Produto p

				ORDER BY 
					CASE WHEN @SortDirection = ''asc'' AND @SortOrder  = 1 THEN p.Nome END,
		
					CASE WHEN @SortDirection = ''desc'' AND @SortOrder  = 1 THEN p.Nome END DESC;'
			)
	END