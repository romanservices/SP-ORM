SPORM
Stored Procedure Object-Relational Mapping

SPORM is intended to simplify the mapping of objects to stored procedures without the overhead of the larger more robust ORM.  This solves a very specific scenario where business logic layer is the database and all DML fires through stored procedures.  

Getting Started:
There are two sample database scripts DataBaseA and DataBaseB, these contain the schema and data to make this sample project work.

setup the databases where convenient and edit the connection string in the app.config found in the test project  

Dependencies you may need:
Microsoft.Practices.EnterpriseLibrary.Common
Microsoft.Practices.EnterpriseLibrary.Data