<h1>PayCore Product Catalogue</h1>


The project has been created as a Final Project of  **Patika-PayCore .Net Bootcamp**.  Project aims to provide a decent web application where user can publish products as well as make offer on existing products. 

:arrow_right: User is allowed to insert products and make offers

:arrow_right: User is allowed to create new category, color, brand for products to be inserted.

:arrow_right: User is allowed to make his/her product to be not offerable and buyable only.

:arrow_right: User is allowed to buy a product without make an offer on it.

:arrow_right: Users need to be registered to use web application.


<h2>Technologies</h2>

Technology    | Version
------------- | -------------
Visual Studio |  2019
.Net          |  5.0
PostgreSql    |  14.5 

Libraries     | 
------------- | 
FluentValidation |
AutoMapper          | 
NHibernate    |
Hangfire    |
SeriLog    | 
Nunit    |
Moq    |
Mailkit    | 


<h2>Getting Started</h2>

:one: Arrange NHibernate Configuration

Change password and database name in configuration with your own settings

 ```"ConnectionStrings": {
    "PostgreSqlConnection": "User ID=postgres;Password=YourMasterPasswordForPostgre;Server=localhost;Port=5432;Database=YourDatabaseName;Integrated Security=true;Pooling=true;"
  }, ```
                       