# Database Deployment

This is a console app that deploys a SQL Server database using migrations. Migrations are often preferred to state-based database changes for deployment automation.

It uses [DbUp](https://dbup.readthedocs.io/en/latest/), a .NET library that helps you to deploy changes to SQL Server databases. It tracks which SQL scripts have been run already, and runs the change scripts that are needed to get your database up to date.

SQL Server database projects are not so popular for automated deployments for a number of reasons, but they still have a lot of benefits.

The approach used here is based on [this approach](), which tries to get the best of both worlds:
* [Using Database Project and DbUp for database management](http://www.kamilgrzybek.com/database/using-database-project-and-dbup-for-database-management/)

Related projects
* https://github.com/danielwertheim/dotnet-sqldb