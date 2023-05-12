# POD B  - ONLINE BOOK LIBRARY



## Table of contents

- [Overview](#overview)
  - [The challenge](#the-challenge)
  - [Screenshot](#screenshot)
  - [Links](#links)
- [My process](#my-process)
  - [Built with](#built-with)
  - [What I learned](#what-i-learned)
  - [Continued development](#continued-development)
  - [Useful resources](#useful-resources)
- [Author](#author)
- [Acknowledgments](#acknowledgments)



## Overview

### The Task

Users should be able to:

- Implement Identity membership system for authentication and user record management
- Implement Photo storage on cloudinary
- Implement JWT authentication system for authentication
- The data storage required for this project is either SqlLite or SQL-Server database.
- Use EFCore and LINQ to abstract ADO.NET data access
- Each team is to write a documentation for their api end-points
- Each team is to design the ERD for their application
- 
User Roles
- Admin
- Regular
Functional Requirements
As an Admin User
- Must be logged in before is allowed to access the dashboard
- Should be able to manage book records (add, update, delete, fetch)
- Should be able to get book(s) either by isbn, title, author, publisher, year published
- Should be able to manage user records (add, update, delete, fetch)
- Should be able to get user by email, username
- Should not be able to add review to a book

As a Regular User
- Must be logged in before is allowed to view more details of a particular book
- Should be able to see available book, recent books and most popular books on the landing page
- Should be able view more detail about the book on a details page
- Should be able to add review to a book


### Links

- Solution URL: [Add solution URL here](https://your-solution-url.com)
- Live Site URL: [Add live site URL here](https://your-live-site-url.com)

## My process

### Built with

- Semantic HTML5 markup
- CSS custom properties
- Flexbox
- CSS Grid
- Entity Framework Core
- Swagger
- Visual Studio
- JWT
- Identity library
- Mailkit
- Blazor Server
- 


### What I learned

I learned how to use the following libraries(Nugget packages)

1.AutoMapper
2.AutoMapper.Extensions.Microsoft.DependencyInjection
3.CloudinaryDotNet
4.Microsoft.AspNetCore.Authentication.JwtBearer
5.Microsoft.AspNetCore.Authorization
6.Microsoft.AspNetCore.Identity.EntityFrameworkCore
7.Microsoft.EntityFrameworkCore
8.Microsoft.EntityFrameworkCore.Design
9.Mailkit.
I got a very good understanding of dependency injection and how controllers work.The project made me understand the repository pattern  and the MVC architectural pattern.

The endpoints that were used  in the project are listed below

Controllers For Account

1.api/account/signup
2.api/account/login
3.api/account/forgot-password.
4.api/account/reset-password
5.api/account/confirm-email
6.api/account/search/email
7.api/account/search/id
8.api/account/UpdateRole
9.api/account/update/email
10.api/account/upload
11.api/account/delete/email

Controllers For Books
1.api/book
2.api/book/id
3.api/book/search/title
4.api/book/search/author
5.api/book/popular
6.api/book/recent
7.api/book/add
8.api/book/update/id
9.api/book/photos/id
10.api/book/delete/id

Controllers For Review
1.api/review/book/id
2.api/review/book/add


## Author

1- Okorie Izuchukwu Leonard
2- Ifedayo Adedeji
3- Babatunde Saheed
4- Imo Ebere
5- Ndubuisi Charles Austin


## Acknowledgments

We will like to thank the Stack Lead of the dotnet stack Squad 14 Mr Francis Ibe for the instructor led classes.
Special thanks to Mr Nwizu Kaosilichukwu for his reviews and guidance in the development phase of this project.


