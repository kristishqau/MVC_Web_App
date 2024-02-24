# BlogWebApp

## Overview

This project is aimed at building a blog using ASP.NET. Below are the key functionalities of the system:

## Functionality Overview

### Signup & Login
- Implemented using the Microsoft Identity solution.
- Signup requires: username, email, password, full name.
- Login requires email & password.

### Users Area
- Admins have exclusive access.
- Admin Dashboard includes functionalities like listing users, deleting users, and changing user roles.
- User roles: Admin, Editor, Member.
- Admin seed account details: Username: admin, FullName: Admin, Password: admin1234.

### Blog
- Index displays all posts with comments.
- Posts ordered by descending create date (latest created shows up first).
- Initially, only 5 latest posts are visible.
- "Next" and "Prev" buttons available when more than 5 posts.
- Buttons indicate the number of posts per page out of total posts.
- Editor Dashboard (visible only to logged-in editors) displays the editor's posts and provides a link to the "Add New Post" view.
- Editors can Create, Edit, & Delete Posts.
- Post requires: User, Created Date, Title, Description, Picture, and belongs to at least one category.
- Post Details View Page contains creator details and a delete button for editors.
- "Read-more" link for long descriptions.
- Categories added by seed and cannot be modified, deleted, or created by users.
- Categories: Nature, Sport, Politics, Economy, Art, Science, Entertainment, News, War, Showbiz.
- Members can comment on posts.
- Posts can have multiple comments.
- Comment includes only text and the User who created it.
- Comments cannot be deleted or edited (except when the Editor deletes the whole Post or the User who wrote the comment).
- Asynchronous calls (JQuery Ajax) used for adding comments without refreshing the page.
- Blog index has keyword filtering for posts.
- Post Categories Navigation: Provides buttons to filter posts by category.
- Download button for an Excel file: Rows are posts, and columns include Nr, Id, Date, Title, Description, Category, Full Name.

## Specifications

- Developed using .NET 7 & MVC.
- Used Entity Framework Code First for database management.
- Front-end designed with Bootstrap (using Razor).
