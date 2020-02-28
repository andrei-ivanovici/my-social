CREATE TABLE [dbo].[Audit]
(
    [Id]    INT          NOT NULL PRIMARY KEY,
    [User]  VARCHAR(500) NULL,
    [Event] VARCHAR(500) NULL,
    [Date]  DATETIME2    NULL
)
