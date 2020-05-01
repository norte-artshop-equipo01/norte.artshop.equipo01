CREATE TRIGGER tr_after_new_prod_add
   ON  [edu-spark-art].[dbo].[Product]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;
	declare @artista int
	Select @artista=inserted.Artistid from inserted
	update Artist set TotalProducts=TotalProducts+1
	where Artist.Id=@artista
END
