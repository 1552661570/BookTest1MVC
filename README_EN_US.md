English/英文 | [Chinese/中文](README.md)

#### Important: This project needs to work with BookStoreDB. Modify the database connection string before everything begins.
# BookTest1MVC
IS 360 Design in the course

## Environment: use VS 2022 and install ASP.NET CORE related components, and Entity Framework 5 is required.

# Screenshot & Existing functionality
#### It realizes the function of creating, reading, updating and deleting (CRUD) three tables, and uses two storage structures to display how it can protect the database structure from leakage.

#### The project implements a login and logout page with authentication capability.

![图片](https://user-images.githubusercontent.com/91865157/145325065-b29e1fd2-c893-48c0-9e84-333c28f08328.png)

#### And specify roles for logged-in users through permissions in the user.detail table.

![图片](https://user-images.githubusercontent.com/91865157/145325082-3405cd36-b874-4ded-a7fa-8d610eb91622.png)

#### Two stored procedures in the database are used to borrow and return books.

![图片](https://user-images.githubusercontent.com/91865157/145325118-6b40b02e-c52e-4143-a5bf-2b5086e7f522.png)
![图片](https://user-images.githubusercontent.com/91865157/145325126-e09f4a57-132e-4f8c-8672-e61a8d0353c2.png)

# PS:
This project is my first ASP.NET CORE MVC project. Although there is a newer version, it has been out for less than a month, so I used 5.0 for fear of many problems. It is a good memory about IS360 and a kind professor. The creation, which started on Thanksgiving Day 2021, leaves so much to be desired that it is now only basic. I miss working with Mr. S. because I don't know how to do the front end. It's worth mentioning that during the process I discovered that ASP.NET CORE MVC does not support views.
