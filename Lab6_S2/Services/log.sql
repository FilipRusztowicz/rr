

--Metoda: zawodnicy z danego klubu
SELECT [z].[ZawodnikId], [z].[KlubId], [z].[StatystykaId], [z].[czyKontuzja], [z].[imie], [z].[kondycja], [z].[nrKoszulki]
      FROM [Zawodnicy] AS [z]
      WHERE [z].[KlubId] = @__klubId_0

--Metoda: liczba goli klubu
SELECT [s].[zdobyteGole]
      FROM [Statystyki] AS [s]
      INNER JOIN [Zawodnicy] AS [z] ON [s].[ZawodnikId] = [z].[ZawodnikId]
      WHERE [z].[KlubId] = @__klubId_0

--Metoda: Srednia kondycja
SELECT AVG([z].[kondycja])
      FROM [Zawodnicy] AS [z]
      WHERE [z].[KlubId] = @__klubId_0 AND [z].[czyKontuzja] = CAST(0 AS bit)

 
