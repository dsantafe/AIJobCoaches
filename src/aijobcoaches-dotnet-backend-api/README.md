# AI for supported employment job coaches

This project provides a robust API interface to interact with employee training and evaluation data through natural language queries. It leverages CosmosDB and SQL Server, orchestrating interactions for employee enrollments, training sessions, quizzes, evaluations, and feedback management.

## Table of Contents
- [Project Overview](#project-overview)
- [Prerequisites](#prerequisites)
- [Environment Variables](#environment-variables)
- [Setup and Installation](#setup-and-installation)
- [Running the Project](#running-the-project)
- [API Documentation](#api-documentation)

## Project Overview
The system manages employee training, evaluations, and feedback, ensuring efficient tracking of enrollments, quiz results, and learning progress. It integrates CosmosDB and SQL Server to store and process training-related data, facilitating structured access to courses, topics, and assessments. Key components include:
- **Azure SQL Database**: Stores structured and relational data related to employees, roles, training enrollments, quiz results, and responses. It is ideal for maintaining referential integrity and executing queries with JOINs across multiple tables.
- **Azure CosmosDB**: Manages semi-structured data, such as course content, quiz questions, and answer options. Its flexibility allows storing these data as JSON documents, enabling scalability and fast queries without a rigid relational structure.
- **Swagger API Documentation**: Provides API interaction documentation.

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) (latest version)
- [Azure account](https://azure.microsoft.com/) with CosmosDB resource
- [SQL Server](https://www.microsoft.com/en-us/sql-server) for local database setup

## Environment Variables
Configure these variables in your development environment:

```json
// AI for supported employment job coaches Web API 
{
  "ConnectionStrings": {
    "AI_JOB_COACHES_CONNECTION_STRING": "Server=<servername>.database.windows.net;Database=<databasename>;User Id=<username>; Password=<password>;"
  },
  "AppSettings": {
    "COSMOS_CONNECTION_STRING": "AccountEndpoint=<servername>;AccountKey=<key>",
    "COSMOS_DATABASE": "<databasename>"
  }
}

// AI for supported employment job coaches Worker
{
  "ConnectionStrings": {
    "AI_JOB_COACHES_CONNECTION_STRING": "Server=<servername>.database.windows.net;Database=<databasename>;User Id=<username>; Password=<password>;"
  },
  "AppSettings": {
    "SCRAPING_URL": "<url>",
    "COSMOS_CONNECTION_STRING": "AccountEndpoint=<servername>;AccountKey=<key>",
    "COSMOS_DATABASE": "<databasename>"
  }
}
```

Replace placeholder values (`YOUR_SERVER`, `YOUR_USER_ID`, `YOUR_PASSWORD` and `YOUR_COSMOS_KEY`) with your actual credentials.

# Database Setup

This guide provides the SQL scripts required to set up the necessary database tables for managing user tokens and tracking token usage history.

## Prerequisites

- SQL Server
- A database (e.g., `AIJobCoachesDB`) where these tables will be created

## Required Database Scripts

- MSSQL

```sql
-- Accounts Table
CREATE TABLE [dbo].[Accounts](
    [AccountID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Username] [varchar](50) NOT NULL UNIQUE,
      NOT NULL,
    [EmployeeID] [int] NOT NULL UNIQUE,
    FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID]) ON DELETE CASCADE
);

-- Employees Table
CREATE TABLE [dbo].[Employees](
    [EmployeeID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FirstName] [varchar](50) NOT NULL,
      NOT NULL,
      NOT NULL UNIQUE,
    [RoleID] [int] NOT NULL,
    FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles] ([RoleID]) ON DELETE CASCADE
);

-- Enrollments Table
CREATE TABLE [dbo].[Enrollments](
    [EnrollmentID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [EmployeeID] [int] NOT NULL,
    [TrainingID] [int] NOT NULL,
    [EnrollmentDate] [datetime] DEFAULT (getdate()),
    [Status] [varchar](20) NULL,
    FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID]) ON DELETE CASCADE,
    FOREIGN KEY ([TrainingID]) REFERENCES [dbo].[Trainings] ([TrainingID]) ON DELETE CASCADE
);

-- Items Table
CREATE TABLE [dbo].[Items](
    [ItemID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [TopicID] [int] NOT NULL,
    [ItemName] [varchar](100) NOT NULL,
    FOREIGN KEY ([TopicID]) REFERENCES [dbo].[Topics] ([TopicID]) ON DELETE CASCADE
);

-- QuizResponses Table
CREATE TABLE [dbo].[QuizResponses](
    [QuizResponseID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [QuizResultID] [int] NULL,
    [Question] [varchar](max) NULL,
    [SelectedOption] [varchar](max) NULL,
    [CorrectOption] [varchar](max) NULL,
    [IsCorrect] [bit] NULL
);

-- QuizResults Table
CREATE TABLE [dbo].[QuizResults](
    [QuizResultID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [QuizID] [varchar](50) NULL,
    [EmployeeID] [int] NOT NULL,
    [TrainingID] [int] NOT NULL,
    [TopicID] [int] NOT NULL,
    [ResponseDate] [datetime] NOT NULL,
    [Score] [decimal](18,2) NULL
);

-- Roles Table
CREATE TABLE [dbo].[Roles](
    [RoleID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [RoleName] [varchar](50) NOT NULL UNIQUE
);

-- Topics Table
CREATE TABLE [dbo].[Topics](
    [TopicID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [TrainingID] [int] NOT NULL,
    [TopicName] [varchar](100) NOT NULL,
    FOREIGN KEY ([TrainingID]) REFERENCES [dbo].[Trainings] ([TrainingID]) ON DELETE CASCADE
);

-- Trainings Table
CREATE TABLE [dbo].[Trainings](
    [TrainingID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [TrainingName] [varchar](100) NOT NULL,
    [Description] [text] NULL,
    [Attachment] [text] NULL
);
```

- CosmosDB

```json

// Quizzes Container
{
	"type": "object",
	"properties": {
		"id": {
			"type": "string",
			"format": "uuid"
		},
		"TrainingID": {
			"type": "string"
		},
		"TopicID": {
			"type": "string"
		},
		"Questions": {
			"type": "array",
			"items": {
				"type": "object",
				"properties": {
					"QuestionID": {
						"type": "integer"
					},
					"Question": {
						"type": "string"
					},
					"Options": {
						"type": "array",
						"items": {
							"type": "string"
						}
					},
					"CorrectAnswer": {
						"type": "integer"
					}
				},
				"required": [
					"QuestionID",
					"Question",
					"Options",
					"CorrectAnswer"
				]
			}
		}
	},
	"required": [
		"id",
		"TrainingID",
		"TopicID",
		"Questions"
	]
}

// Courses Container
{
	"type": "object",
	"properties": {
		"id": {
			"type": "string",
			"format": "uuid"
		},
		"TrainingID": {
			"type": "integer"
		},
		"Title": {
			"type": "string"
		},
		"Url": {
			"type": "string",
			"format": "uri"
		},
		"Instructor": {
			"type": "string"
		},
		"Rating": {
			"type": "string",
			"pattern": "^[0-5](,[0-9])?$"
		}
	},
	"required": [
		"id",
		"TrainingID",
		"Title",
		"Url",
		"Instructor",
		"Rating"
	]
}
```

## Setup and Installation
1. **Clone the repository**:
   ```bash
   git clone https://github.com/dsantafe/AIJobCoaches.git
   cd src/aijobcoaches-dotnet-backend-api
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the project**:
   ```bash
   dotnet build
   ```

4. **Configure environment variables**:
   Set up the environment variables listed above in your `.env` file or directly in your development environment.

## Running the Project
1. **Run the application**:
   ```bash
   dotnet run
   ```

2. **Access the API**:
   - Open the Swagger UI to explore and test endpoints: [Swagger Documentation](https://ai-jobs-coaches-api.azurewebsites.net/swagger/index.html)

## API Documentation
- **Swagger UI**: The API endpoints are documented and can be tested interactively [here](https://ai-jobs-coaches-api.azurewebsites.net/swagger/index.html).

## Links
- Azure Cosmos DB emulator: https://aka.ms/cosmosdb-emulator