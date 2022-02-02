# Introduction

### Why is this here?

This is here to spark creativity and give inspiration to new developers.

### What is this document?

This is the documentation of how I implemented the project. You should have your own code, design, implementation etc.

### Who is it for?

This is for new developers.

### How to use it?

There are three main ways you can go about this:

- Read the requirements and go wild.
- Read this documentation and implement it by yourself.
- Read this documentation and the [code](https://github.com/OrkunAvci/Presenter), and then improve on it.

You can freely jump between these approaches. Documentation is written with as little code snippets as possible for this reason. And you can use whatever tech stack you want.

---

---

# Presenter

## Requirements

### Administrative Screen

- Users should be able to login.

- Through a menu they should be able to:

    - Enter an image description (Max 100 char)
    - Enter start date of when to display image
    - Enter end date of when to display image
    - Enter order number of image
    - Enter which screen to show it on
    - Where images…
        - Can be an image or a video
        - Can be local or remote

    - Define screens in the system
    - Where screens…
        - Has a screen no
        - Has description
        - Has location (Descriptive not GPS)
        - Refresh rate

### Display Screen

- Displayed screen is a TV so system should be responsive
- Separate refresh timers for screens
- Screens should be able to display both images and videos
- Presentation should start over at the end

This is intended to be a Web App.

---

## Tech Stack

- ASP .NET Core 5.0
- C#
- Razor Pages
- MySQL
- Vanilla JavaScript

---

## Environment

- Visual Studio 2019 (IDE)
- MySQL Workbench (DB Management)
- Typora (Documentation)
- Google Chrome (Target Browser)

---

## NuGet Packages

I would advise building this list up as you go. For example, you won’t need MySQL stuff if your DB is Oracle.

- Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation (5.0.9)
- Microsoft.AspNetCore.Session (2.2.0)
- Microsoft.EntityFrameworkCore (5.0.9)
- Microsoft.EntityFrameworkCore. Relational (5.0.9)
- Microsoft.EntityFrameworkCore.Tools (5.0.9)
- Microsoft.Extensions.Options.ConfigurationExtensions (5.0.0)
- Microsoft.VisualStudio.Web.Codegeneration. Design (5.0.2)
- MySql.Data (8.0.26)
- Pomelo.EntityFrameworkCore.MySql (5.0.1)


---

## Database Design

### User

```c#
public class User
	{
		public int ID { get; set; }
		public string username { get; set; }
    	//	Non-hashed passwords are being used here.
		public string password { get; set; }
	}
```

### Screen

```c#
public class Screen
	{
		public int ID { get; set; }
    	//	A short description.
		public string description { get; set; }
    	//	Where this screen is located.
		public string location { get; set; }
    	//	Refresh rate in seconds.
		public uint refresh { get; set; }
	}
```

### Images

```c#
public class Images
	{
		public int ID { get; set; }
    	//	A short description.
		public string description { get; set; }
    	//	When to start displaying this image.
		public DateTime start { get; set; }
    	//	When to stop displaying this image.
		public DateTime finish { get; set; }
    	//	Link to source.
		public string link { get; set; }
    	//	Which screen to display it on.
		public int screen_no { get; set; }
    	//	Is it a video or an image?
		public bool is_video { get; set; }
	}
```

Note: **Images**.screen_no is a foreign key to **Screen**.ID

---

## Backend Design

### Models

In **Model** folder we have definitions for:

- Images
- Screen
- User

And their respective database connection classes:

- ImagesContext
- ScreenContext
- UserContext

All context classes have the same structure to them. First they map to tables. Second they define keys. And lastly they link columns with class properties.

Example shown through **UserContext**:

```C#
protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Map entities to tables.
			modelBuilder.Entity<User>().ToTable("user");

			//	Configure primary keys.
			modelBuilder.Entity<User>().HasKey(u => u.ID).HasName("id");

			//	Configure columns.
			modelBuilder.Entity<User>().Property(u => u.ID).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
			modelBuilder.Entity<User>().Property(u => u.username).HasColumnType("nvarchar(64)").IsRequired();
			modelBuilder.Entity<User>().Property(u => u.password).HasColumnType("nvarchar(64)").IsRequired();
		}
```

Note: **CLL **and **CLLNode **are no longer used. They stand for Circular Linked List and Circular Linked List Node respectively. This behavior has been moved to JavaScript. Refer to Display.js.

----

### Pages

#### Dashboard Folder

Folder exposes *Create, Read, Update, and Delete(CRUD)* functionality for **Images** entities. Authorized folder. Mostly auto generated.

#### Screens Folder

If session is authorized, folder exposes CRUD functionality for **Screen** entities. If unauthorized, it simply lists screens with a watch link to their respective Display pages. This is the default page.

#### Index

Redirects to Screens. Not used.

#### Login

When a new request is received,

- Signs out of current user (if there is one),
- Searches for user in database, throws **BadRequest()** if not found,
- Creates **Principal** needed for authentication:
    - Create a **Claim** with User.ID, User.username, and “auth”,
    - Create an **Identity** from **Claim**,
    - Create a **Principal** from **Identity**,
- Signs in with default **AuthenticationScheme**, **Principal**, and non-persistent **AuthenticationProperties**,
- Redirects to **Dashboard**.

#### Logout

Signs out of current user and redirects to default page.

#### Register

Can only be accessed if session is authorized. Auto generated.

Note: Since there is no distinction between users, exposing deletion or listing functionality to a user would expose it to all users. In the case of a security breach, this would be disastrous. Therefor it is not implemented.

#### Display

Since the refresh is calculated separately for each host we outsourced this task to host machines. To minimize the footprint of the program on the server and on the network, all session data is sent with the initial request and no further interaction between host and the server. There are no credentials in a watching session so this safe.

#### Display Controller Behavior (.cs)

Collects:

- Screen

    ID of the **Screen** is passed through URL parameters. Pulls **Screen** object from database. Only refresh property is used in here.

- Session

    List of **Images** to display. Images has to fulfill these conditions:

    - **Images**.screen_no == **Screen**.ID
    - Start Date < Now
    - Now < Finish Date

- UpdateTime

    Keeps track of when to update the session. Earlier of:

    - Start date of next **Images** to add to the session
    - Finish date of next **Images** to remove from the session

#### Display View Behavior (.cshtml)

Disables layout. Pulls Controller properties and injects them into a script tag so they can be used in JavaScript. Has an img tag for images and an iframe for videos.

---

## Frontend Design

### Display.js

Gets img and iframe tags into a constant variables and pulls refresh rate from **Screen** to a local variable.

#### window.onload()

Makes sure session is an array. Makes the first *refresh_content()* call. And sets up a timer to refresh the page when session update time arrives. Which in return automatically calls the controller and gets the new session.

#### refresh_content(index)

Makes sure index to session elements is in bound. Pulls a copy the current element into a local variable and calls appropriate handle function with **Images**.link. Finally sets up a timer to call itself after {refresh rate} seconds.

#### handle_img(link) and handle_vid(link)

Alternates the classes of tags between “Hidden” and “Displayed”. Sets up the src property of tag to the link but does not clean up the src of other. Classes are defined in default **site.css**.

---

## Final Thoughts

I didn’t have any ASP .NET or ASP .NET Core experience before starting this project. I had some C# knowledge but it was in game development  far from web development. So this was an absolute beginner project for me. I learned a lot from it. And hopefully, you will too.
