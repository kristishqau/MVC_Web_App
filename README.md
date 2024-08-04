# BlogWebApp

## Overview

This project is aimed at building a blog using ASP.NET. Below are the key functionalities of the system:

![BlogIntro](https://github.com/user-attachments/assets/a3fd6764-fae9-46cf-a95a-052d25949713)

## Functionality Overview

### Signup & Login
- Implemented using the Microsoft Identity solution.
- Signup requires: username, email, password, full name.
- Login requires email & password.

![BlogSignUp](https://github.com/user-attachments/assets/266f3698-ca2e-412b-b902-b8ebd882cd8f)

### Users Area
- Admins have exclusive access.
- Admin Dashboard includes functionalities like listing users, deleting users, and changing user roles.
- User roles: Admin, Editor, Member.
- Admin seed account details: Username: admin, FullName: Admin, Password: admin1234.

![adminview](https://github.com/user-attachments/assets/bdc7f8ea-cee8-4ff7-8a6a-68c42c8bb4e2)

### Blog
- Index displays all posts with comments.
- Posts ordered by descending create date (latest created shows up first).
- Initially, only 5 latest posts are visible.
- "Next" and "Prev" buttons available when more than 5 posts.
- Buttons indicate the number of posts per page out of total posts.

![BlogTopMenu](https://github.com/user-attachments/assets/ddff8185-9dd2-4569-ba12-00eac875e249)

- Editor Dashboard (visible only to logged-in editors) displays the editor's posts and provides a link to the "Add New Post" view.
- Editors can Create, Edit, & Delete Posts.

![editorposts](https://github.com/user-attachments/assets/d0062f0d-c8cb-492a-b2e3-fd0a7d80a9bc)

- Post requires: User, Created Date, Title, Description, Picture, and belongs to at least one category.
- Post Details View Page contains creator details and a delete button for editors.

![detailsofpost](https://github.com/user-attachments/assets/07cc382e-a10f-4699-8f15-3a5b101a0016)

- "Read-more" link for long descriptions.
- Categories added by seed and cannot be modified, deleted, or created by users.
- Categories: Nature, Sport, Politics, Economy, Art, Science, Entertainment, News, War, Showbiz.

![PostExample](https://github.com/user-attachments/assets/fbb539a5-445c-4fa1-936f-9babe61e303a)

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
