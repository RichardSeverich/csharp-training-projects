# ToDo App

The ToDo App is an application that allows the user to create, store, modify tasks and show them to the client, it was created using React for the frontend and C# for the backend side of the application. 

## Getting Started

Please Download both repositories frontend and backend to have a development environment, for better work on the development is strongly suggested to have Visual Studio 2019 and Microsoft SQL Server Management Studio

### Prerequisites

The following software is required for working on development

```
dotnet --version 5.0.201
Microsoft Visual Studio
Microsoft SQL Server Management Studio
```

### Installing

To work with this application you have to follow the following steps after downloading the repository

Open the backend project in Microsoft Visual Studio and open the appsettings.json file and modify the server value of the following key and add the name of your local server:

```
{porject_path}\backend\Dev31.TodoApp.API\appsettings.json
"AppContextConnectionString"
```

To create the database and tables run the following command in Nugget Package Manager Console

```
Update-Database
```
Or you can use the following command in the .Net CLI

```
dotnet ef database update
```
To have data in the database run the query included in the file db-insert-data.sql in the folder frontend/Dependencies/DB Queries

```
db-insert-data.sql
```



