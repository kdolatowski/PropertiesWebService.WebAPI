# To run project:

1. make sure you have installed/access to PostgreSQL
2. create database: PropertiesWebServiceDemoDb with credentials taken from ***PropertiesWebService.DatabaseMigrator/appsettings.json*** (sectoin: *ConnectionStrings::Default*)
3. grant permissions for new user, you can use script below:
	- `create user demo with password 'qaz123';`
	- `grant all privileges on database "PropertiesWebServiceDemoDb" to demo;`
	- `grant all privileges on schema public to demo;`
4. try to connect to make sure everything is working well
5. go into: ***PropertiesWebService.DatabaseMigrator*** and run ***update-database.bat***
6. start Visual Studio and open ***PropertiesWebService.WebAPI.sln*** from root directory (make sure startup project is set to ***PropertiesWebService.WebAPI***) and run (type *F5*), browser should be opened on localhost:5001 and swagger should be displayed

# Solution projects
- ***PropertiesWebService.DAL*** - data access layer (class library), with database entities
- ***PropertiesWebService.DatabaseMigrator*** - database updater (Azure function, will update DB on startup, or from command line)
- ***PropertiesWebService.Models*** - data transfer models (class library)
- ***PropertiesWebService.Services*** - services with business logics (class library)
- ***PropertiesWebService.WebAPI*** - main executable WebAPI project with controllers, and configuration
- ***Tests/PropertiesWebService.ServicesTests*** - unit tests for services