### Junior test task program
This is my testing task to junior full-stack programmer.  
   
#### Build
For build you must create database:
**Default:** using MSSQL Server, database "ClientInformation"
For simple add use: Add-Migration, Update-Database in Package Manager Console (Visual Studio);
**Important** Change database connection settings in _appsettings.json_ before migration and updating database.

#### Additional project plugins
* Microsoft.EntityFrameworkCore v5.0.6
    * Microsoft.EntityFrameworkCore.SqlServer v5.0.6 - For database providing
* Flurl v3.0.2 - For create HTTP requests
    * Flurl.Http v3.2.0