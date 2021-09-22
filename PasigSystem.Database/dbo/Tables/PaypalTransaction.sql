CREATE TABLE [dbo].[PaypalTransaction]
(
	[PaypalTransactionId] BIGINT NOT NULL CONSTRAINT PK_PaypalTransaction PRIMARY KEY IDENTITY, 
    [OrderId] NVARCHAR(50) NOT NULL, 
    [OrderAmount] MONEY NOT NULL, 
    [UserEmail] NVARCHAR(250) NOT NULL,
    [RowCreationDateTime] DATETIMEOFFSET NOT NULL
)

GO

CREATE UNIQUE INDEX [UI_PaypalTransaction_OrderId] ON [dbo].[PaypalTransaction] ([OrderId])