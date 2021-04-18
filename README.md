## Title of the Project: Quickkart Online Shopping

## Description: 
Quickkart is an e-commerce web application, which retails in various dailylife products. This application allows viewing various products available to the registered users who can add desired products to cart and purchase instantly using different payment methods. This also provides an easy access to Administrators and customer to view placed orders. 

## Technologies: 
This is a web-based application which is developed in Angular having SQl server as backend.
Database Design - Microsoft SQL Server Management Studio 2018
Languages/Framework - C#, ADO.NET, ASP.NET, LINQ
Frontend - Angular, HTML, Bootstrap, Javascript, CSS

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

New user can be registered and can login to the application to view and buy products. After ordering the products, checkout page will show different methods of payment and redirect to confirmation page. Application uses token method to authorize the user for accessing any functionality. It is done using Microsoft OWIN(Open Web Interface for .NET) token and Authorize attribute to all certain type of users for using the functionality. Since, Presentation layer is made in different technology, CORS(Cross Origin Resource Sharing) was enabled in Services to allow any incoming requests from client/browser. It is done in Webconfig file. Routing to different components is made in Angular using ActivatedRoute class. Users are allocated tokens for limited time to use the aplication and is done using Session Management. Proper validation and authentication is done to check application security. 
