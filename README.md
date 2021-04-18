## Title of the Project: Quickkart Online Shopping

## Description: 
Quickkart is an e-commerce web application, which retails in various dailylife products. This application allows viewing various products available to the registered users who can add desired products to cart and purchase instantly using different payment methods. This also provides an easy access to Administrators and customer to view placed orders. 

## Technologies: 
This is a web-based application which is developed using ASP.NET MVC having SQl server as backend.
Database Design - Microsoft SQL Server Management Studio 2018
Languages/Framework - C#, ADO.NET, ASP.NET, LINQ, ASP.NET MVC

## Tools Used: 
Microsoft Visual Studio, Microsoft SQL server Management Studio 2018, Postman, Google Chrome.

## Technical Description: 
The application is made to utilize the CRUD(Create, Read, Update, Delete) funtionality using different technologies and mechanism such as Session management and token based authentication for users. It contains several below functions available to the users:
1. Home
2. View Products
3. My Cart
4. View Order
5. Login
6. Register
7. Checkout
8. Confirmation
9. Contact Us

New user can be registered and can login to the application to view and buy products. After ordering the products, checkout page will show different methods of payment and redirect to confirmation page. Application uses AdminAuthorize attribute to allow user to access any functionality. Also it uses session state, query string and cookies to save the information on server. Presentation and Service layer both can be created in .NET technology. HTML encoding and validation is done to ensure application security. HTML.encoding and HTML.AntiForgeryToken will prevent from any SQL injection.
