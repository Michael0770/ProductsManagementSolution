1. Use Latest version of Visual Studio 2022
2. The project is targeted at .Net 6.0 
3. Project is using Asp.net Core Apis with EF Code First and Sql server as database

For running project, do below steps
1. Open solution in Visual Studio 2022
2. Change connection strings for your database server in appsettings.config
3. in package manager console, run command "update-database". This will create database
4. Run the project by pressing Ctrl+F5. This will run project at your local
5. using some web test tools like fiddler, you can invoke api end-points
6. 

Technical Description
1. There is only one controller that is product controller and it has 5 methods(endpoints)
2. The database model and controller model are represented by same class "Product"
3. Dependency Injection has been used to access db context
4. As EF Code first has been used, so in future if user want to modify db classes, then they can make change and run "add-migrations" and "update-database" commands.
