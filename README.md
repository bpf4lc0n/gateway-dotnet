# gateway-master-dotnet

.Net Core API Sln

## Tools

Node.js 
Sql
Visual Studio
.Net Core 3.1

## Configuration

One you clone the repository:
Install dotnet ef tools `dotnet tool install --global dotnet-ef --version 3.1`

## Connection String

Go to the following path:
	Gateway.Web.Host
You will find a file called “appsettings.json”, update the connection string inside the file.

Or run updateSettingsFile.ps1

## Create the database and apply the migrations

In the VisualStudio Package Manager Console select the project “EntityFrameworkCore”:
And then use the following command:
`Update-Database`

The project is ready to execute

Login Info>> us: Gateway01@gmail.com /  ps: Gateway1!
