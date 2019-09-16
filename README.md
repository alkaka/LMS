
# Welcome to the LMS wiki!

**Learning Management System (LMS)** is a web application that manages courses offered by a department in a school and the course materials. The LMS database records the lecture notes and the assignments of courses offered such that instructors.

# Platform
.Net Core 2.2, Entity Framework Core, and SQL Server. I used Visual Studio Community Edition 2019 as the IDE. The pages are very lightly styled using Bootstrap. Tools Github and slack. Working with Scrum (Product backlog, ER Diagram, Wireframes , Sprint backlog)

Project requirement and details //  https://drive.google.com/file/d/1L7hNlOhYA_ItaMXAH3jSQEghfT-viIp8/view

Project completed within 3 Sprint (3 weeks)

# Installation
you need to run migration with Package Manager Console 

PM> Enable-migrations

PM> Add-migration Init

PM> Update-database

then with cmd commands user project folder path ex:  C:\Users\User\source\repos\LMS1\LMS1

C:\Users\alkak>cd C:\Users\alkak\source\repos\LMS1\LMS1

C:\Users\alkak\source\repos\LMS1\LMS1>dotnet user-secrets set "mail:AdminPW" "Az*1234"

after you get this message (Successfully saved mail:AdminPW = Az*1234 to the secret store.)

your Admin User id will be  ID: AdminPW@mail.se // and Password: Az*1234

Installation details // https://drive.google.com/file/d/1JXwnmKezXCLK2kV9dYmp4K6XjnW5MnbC/view
