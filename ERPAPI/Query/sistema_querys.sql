USE [ERP]
GO
/****** Object:  UserDefinedFunction [dbo].[GetNivelEspacio]    Script Date: 7/10/2019 17:28:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetNivelEspacio] 
(
	-- Add the parameters for the function here
	@nivel int 
)
RETURNS NVARCHAR(255)
AS
BEGIN
	DECLARE @salida nvarchar(255)
	 SELECT @salida
	 = CHOOSE(@nivel, '  ', '    ', '      ','        ','          ','            ','              ','                ')
	RETURN  @salida
END

GO
/****** Object:  StoredProcedure [dbo].[ArbolBalance]    Script Date: 7/10/2019 17:28:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- execute [dbo].[GenerarBanlance]	 '2019-09-10' , '2019-09-29'

CREATE PROCEDURE [dbo].[ArbolBalance]
	-- Add the parameters for the stored procedure here
	 @FechaInicio DATETIME,
     @FechaFin DATETIME,
	 @padre_id int

AS
BEGIN
	 
	declare @au_id int
	set rowcount 0
	select  a.*, dbo.[SumaDebito]( @FechaInicio , @FechaFin ,a.AccountId ) as Debit, dbo.[SumaCredito]( @FechaInicio , @FechaFin ,a.AccountId ) as Credit into #mytempn from  Accounting as a   where  ParentAccountId = @padre_id
	set rowcount 1
	select @au_id = AccountId from #mytempn
	while @@rowcount <> 0
	begin
		set rowcount 0
	 
		declare @cuenta bigint
		declare @codigo bigint
		declare @saldo float
		declare @padre bigint
		declare @nivel bigint
		declare @credito float=0
		declare @debito float=0
		declare @descripcion nvarchar(255)

		 
		select @cuenta= AccountId,@codigo=AccountCode,@nivel=HierarchyAccount,@saldo=AccountBalance,@descripcion= t.Description ,@debito=Debit, @credito=Credit    from #mytempn as t where AccountId = @au_id
		insert into  #Balance(parent,codigo,nivel,cuenta,saldo,descripcion,debito,credito) values (@padre_id,@codigo,@nivel,@cuenta,@saldo, concat([dbo].[GetNivelEspacio](@nivel), @descripcion),@debito,@credito)
		execute ArbolBalance  @FechaInicio ,@FechaFin, @cuenta
		
		if EXISTS  (select * from Accounting where ParentAccountId=@cuenta)
			begin 
				-- SELECT  
				--	 @debito=ISNULL(  sum ([Debit]),0)
				--	  , @credito=ISNULL(  sum(Credit),0)
				--  FROM [ERP].[dbo].[JournalEntryLine] as j 
				--  inner join JournalEntry as e on e.JournalEntryId =j.JournalEntryId
				--  inner join Accounting as a on a.AccountId= j.AccountId
				--	where a.ParentAccountId=@cuenta
		
				--update 	#Balance
				--set credito=@credito,
				--	debito=@debito
				--where  cuenta=@cuenta
				SELECT  
					 @debito= ISNULL( sum (debito),0)
					  , @credito= ISNULL( sum(credito),0)
				  FROM #balance
				  where parent=@cuenta
		
				update 	#Balance
				set credito= isnull(credito,0)+@credito,
					debito= isnull(debito,0)+ @debito
					,balance= @debito-@credito

				where  cuenta=@cuenta
			end 
		else
			begin
				SELECT  
				  @debito= ISNULL( sum ([Debit]),0)
				 ,@credito=ISNULL(  sum(Credit),0)
				 FROM [ERP].[dbo].[JournalEntryLine] as j inner join JournalEntry as e on e.JournalEntryId =j.JournalEntryId
				 where j.[AccountId]=@cuenta
				update 	#Balance
				set credito=@credito,
					debito=@debito,
					balance= @debito-@credito
				where  cuenta=@cuenta
			end
		
		
		delete #mytempn where AccountId = @au_id
		set rowcount 1
		select @au_id = AccountId from #mytempn 

	end
	set rowcount 0
    -- Insert statements for procedure here
	 
END

GO
/****** Object:  StoredProcedure [dbo].[GenerarBanlance]    Script Date: 7/10/2019 17:28:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

-- execute [dbo].[GenerarBanlance]	 '2019-09-22' , '2019-09-29'


alter PROCEDURE [dbo].[GenerarBanlance]

    @FechaInicio DATETIME,
   @FechaFin DATETIME,
   @NivelVer DATETIME

	-- Add the parameters for the stored procedure here 
AS
BEGIN
	 
	drop table if exists #Balance
	drop table if exists #mytemp
 

	CREATE TABLE  #Balance(id int primary key identity(1,1), codigo bigint,   parent int , nivel int, cuenta INT ,saldo float, descripcion NVARCHAR(400) , credito float default 0,debito float default 0 ,balance float default 0)
	declare @au_id int
	set rowcount 0
	select * into #mytemp from  Accounting  where len(AccountCode) =1
	set rowcount 1
	select @au_id = AccountId from #mytemp
	while @@rowcount <> 0
	begin
		set rowcount 0
		declare @cuenta bigint
		declare @codigo bigint
		declare @saldo float
		declare @padre bigint
		declare @nivel bigint
		declare @credito float=0
		declare @debito float=0
		declare @descripcion nvarchar(255)

		 
		select @cuenta= AccountId,@codigo=AccountCode,@nivel=HierarchyAccount,@saldo=AccountBalance,@descripcion= t.Description   from #mytemp as t where AccountId = @au_id
		insert into  #Balance(parent,codigo, nivel,cuenta,saldo,descripcion,debito,credito) values (0,@codigo,@nivel,@cuenta,@saldo,@descripcion,0,0)
		execute ArbolBalance @FechaInicio, @FechaFin, @cuenta
		
		

		 SELECT  
			 @debito= ISNULL( sum (debito),0)
			  , @credito= ISNULL( sum(credito),0)
			
		  FROM #balance
		  where parent=@cuenta
		
		update 	#Balance
		set credito=@credito,
			debito=@debito ,
			balance = @debito-@credito
		where  cuenta=@cuenta

		--SELECT  
		--  @debito= sum ([Debit])
		-- ,@credito= sum(Credit)
		-- FROM [ERP].[dbo].[JournalEntryLine] as j inner join JournalEntry as e on e.JournalEntryId =j.JournalEntryId
		-- where j.[AccountId]=@cuenta
		--update 	#Balance
		--set credito=@credito,
		--	debito=@debito
		--where  cuenta=@cuenta


	 
		delete #mytemp where AccountId = @au_id
		set rowcount 1
		select @au_id = AccountId from #mytemp 

	end
	set rowcount 0
	declare @totalCredito float
	declare @totalDebito float
	declare @totalBalance float

	select @totalDebito=  sum(debito) , @totalCredito= sum(credito)  from #Balance where nivel =1
	set @totalBalance =@totalDebito-@totalCredito 
	insert into  #Balance(parent,codigo, nivel,cuenta,saldo,descripcion,debito,credito,balance) values (0,0,0,0,0,'Total Balance',isnull(@totalDebito,0),isnull(@totalCredito,0), ISNULL(@totalBalance,0))
	
	 delete  from   #Balance where nivel> @NivelVer
	select * from  #Balance
-- execute [dbo].[GenerarBanlance]	 '2019-09-22' , '2019-09-29'  ,1


END
											
GO






/****** Object:  StoredProcedure [dbo].[Nivelar]    Script Date: 7/10/2019 17:28:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[Nivelar] 
	-- Add the parameters for the stored procedure here
	 @parametro int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	 
declare @au_id int

set rowcount 0
select * into #mytemp from  Accounting

set rowcount 1

select @au_id = AccountId from #mytemp

while @@rowcount <> 0
begin
    set rowcount 0
    select * from #mytemp where AccountId = @au_id
    delete #mytemp where AccountId = @au_id

    set rowcount 1
    select @au_id = AccountId from #mytemp 
end
set rowcount 0



    -- Insert statements for procedure here
	 
END

GO
