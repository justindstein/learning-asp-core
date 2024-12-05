-- Create a new database called 'aws'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'aws'
)
CREATE DATABASE aws
GO

-- Create a new table called '[workflow]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[workflow]', 'U') IS NOT NULL
DROP TABLE [dbo].[workflow]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[workflow]
(
    [id] INT NOT NULL PRIMARY KEY, -- Primary Key column
    [name] NVARCHAR(50) NOT NULL
    -- Specify more columns here
);
GO
