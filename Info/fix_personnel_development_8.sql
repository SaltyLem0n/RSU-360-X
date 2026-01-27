-- Fix for personnel_development_8 table - Add missing 'id' column
-- Run this SQL script on your SQL Server database to fix the "Invalid column name 'id'" error

USE [Ev];
GO

-- Step 1: Add the 'id' column as the primary key identity column
ALTER TABLE [ev].[personnel_development_8]
ADD [id] INT IDENTITY(1,1) NOT NULL;
GO

-- Step 2: Set it as the primary key
ALTER TABLE [ev].[personnel_development_8]
ADD CONSTRAINT PK_personnel_development_8 PRIMARY KEY CLUSTERED ([id]);
GO

-- Verify the change
SELECT TOP 5 * FROM [ev].[personnel_development_8];
GO
