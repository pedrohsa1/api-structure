SELECT cp.Numero AS ContasPaga_NumeroProcessoPagamento, 
	   p.Nome AS NomeFornecedor,
	   cap.DataVencimento As ContaAPagar_DataVencimento,
	   cp.DataVencimento As ContaPagas_DataVencimento,
	   cp.DataPagamento As ContasPagas_DataPagamento,
	   cap.Valor As ContasAPagar_ValorLiquido,
	   cp.Valor As ContasPagas_ValorLiquido,
  FROM dbo.Pessoas p 
  LEFT JOIN dbo.ContasAPagar cap ON p.Codigo = cap.CodigoFornecedor 
  LEFT JOIN dbo.ContasPagas cp ON p.Codigo = cp.CodigoFornecedor;