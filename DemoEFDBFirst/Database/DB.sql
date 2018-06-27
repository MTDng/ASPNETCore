CREATE DATABASE EFCoreDBFirstDemo 
GO

USE [EFCoreDBFirstDemo]
GO

CREATE TABLE [dbo].[Student](
 [StudentId] [bigint] IDENTITY(1,1) NOT NULL,
 [FirstName] [varchar](30) NULL,
 [LastName] [varchar](30) NULL,
 [Gender] [varchar](10) NULL,
 [DateOfBirth] [datetime] NULL,
 [DateOfRegistration] [datetime] NULL,
 [PhoneNumber] [varchar](20) NULL,
 [Email] [varchar](50) NULL,
 [Address1] [varchar](50) NULL,
 [Address2] [varchar](50) NULL,
 [City] [varchar](30) NULL,
 [State] [varchar](30) NULL,
 [Zip] [nchar](10) NULL,
CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
 [StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
 IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
 ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SELECT * FROM Student

INSERT INTO Student VALUES('Toan','Minh', 'Male', '19850724', '20180620','84915073444','tnmtoan@yahoo.com', 'Da Nang', '', 'Da Nang', '', '')