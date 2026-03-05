# Blog Backend Api - Project Overview

## Project Goal

Create A backend for blog applications

- Support full crud operations
- All Users to create accounts and logins
- Deploy to Azure
- Use SCRUM workflow concepts
-Introduces Azure DevOps practices

## Stack

- Backend will be in .Net 9, ASP.NET Core, EF core, SQL Server
- FrontEnd will be done in Next.js with TypeScript Flowbite{has tailwind} Deploy with Vercel Or Azure.

## Applications Features

### User Capabilities  

- Create An Account
- Login
- Delete Account

### Blog Features

- View all publishing blog posts
- Filter Blog Posts
- Create new posts
- Edits existing posts
- Delete posts
- publish/unpublish posts

### Pages {FrontEnd connected to API}

- Create Account page
- blog view posts of published items
-dashboard pages (this is the profile page will edit, and publish and unpublish our blog posts)

- **Blog Page**
-Display all published blog items

- **Dashboard**
- User profile page
- create blog posts
- edit blog posts
- delete blog posts

## Project folder Structure

### COntrollers

### User Controller 

Handles all our users interactions

Endpoints:

- login 
- Add user
- Update users
- delete users

#### Blog Controller
handles all our blog operations 

Endpoints:
- Create Blog item (C)
- Get All Blog Items (R)
- Get blog items by category(R)
- get blog items by tags(R)
- get blog items by date(R)
- Get published Blog Items (R)
- Update Blog Items (U)
- Delete Blog Item (D)
- Get blog item by user id

> Delete will be use for soft delete / publish logic


---

## Models

### UserModel

```csharp

int id

string Username
string salt
string Hash

### Blog Item Model

int Id 
int userId
string Publisher Name
string Title 
string Image
string Description 
string Date
string Category
string Tags
bool IsPublished
bool Is Deleted

### LoginModel DTO

string Username
string Password

### CreateAccountModel DTO

### PasswordModel DTO
 
string salt

string Hash

```
### Services

Context/Folder
- DataContext
- UserService/file
- GetUserByUserName
- Login
- AddUser
- UpdateUser
- DeleteUser
### BlogItemsService
- AddBlogItems
- GetAllBlogItems
- GetBlogItemsByCategory
- GetBlogItemsByTag
- GetBlogItemsByDate
- GetPublishedBlogItems
- UpdateBlogitems
- delteblogitems
- getuserbyid
### PasswordService
- Hash Password
- Very HasPassword






