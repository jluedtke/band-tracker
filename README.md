# Band Tracker
### Jared Luedtke 06.16.17

## Description
A NANCY and SQL app that allows users to input multiple venues and bands, then set up relationships between them in order to determine what band played where and what venue hosted which bands.

### Specs
| Spec | Input | Output | Description |
| :-------------     | :------------- | :------------- | :------------- |
| Program starts with an empty database. | N/A | N/A | Program must start with an empty database before any tests are run so data is not corrupted. |
| Program database values equal each other. | Venue1:"Knitting Factory" == Venue1:"Knitting Factory" | true | Program must recognize equal values in databases before any manipulation can be completed. |
| Program can add a venue to the database. | Venue1:"Knitting Factory" | Venue1:"Knitting Factory" | Before the program can manipulate and organize data, it must be able to save initial data inserted in to it. |
| Program can add bands to database. | Band1:"Parappa the Rappa"  Band2:"Cage the Elephant" | Band1:"Parappa the Rappa"  Band2:"Cage the Elephant" | Once the program is able to add venues to the database, adding bands is simply another step. |
| Program can add bands to multiple venues. | Band1: Venue1, Venue3 | Band1: Venue1, Venue3 | It's the creation of the many to many relationship between bands and venues. |
| Program can add venues to multiple bands. | Venue1: Band1, Band3 | Venue1: Band1, Band3 | It's a further application of the many to many relationship. |
| Program can update a venue's information | Venue1:"Mouse Trap House" | Venue1:"Mouse Trap House" NOT Venue1:"Knitting Factory" | Program must be able to manipulate venues' data within a database. |
| Program can delete a venue within a database. | DELETE "Mouse Trap House" | Venue2:"The Bar Across the Way" | Along with adding and changing data, the program must be able to remove data completely. |
| Program can List out the venues a band is attached to. | Band1 | Venue1, Venue3 | Visualization of the many to many relationship. |




## Setup/Installation Requirements
1. Go to the <a href="https://github.com/jluedtke/band-tracker">GitHub Repository</a>
2. Clone repository on to your machine
3. Install <a href="https://www.asp.net/">ASP.NET</a>, <a href="https://www.visualstudio.com/">Visual Studio</a>, <a href="https://www.nuget.org/packages/Nancy/">Nancy</a>, <a href="https://xunit.github.io/">xUnit</a>, and <a href="https://www.mysql.com/downloads/">SQL</a>
4. Open with your choice of text editor
5. Open Windows PowerShell
6. In PowerShell, input the commands >>sqlcmd -S "(localdb)\mssqllocaldb">>CREATE DATABASE band_tracker>>GO>>USE band_tracker>>GO>>CREATE TABLE venues(id INT IDENTITY(1,1), name VARCHAR(100));>>GO>>CREATE TABLE bands(id INT IDENTITY(1,1), name VARCHAR(50));>>GO>>CREATE TABLE bands_venues (id INT IDENTITY(1,1), band_id INT, venue_id INT)>>GO>>CREATE TABLE venues_source (id INT IDENTITY(1,1), name VARCHAR(100))>>GO>>QUIT
7. Inside of SQL Management Studio, right click the database band_tracker, navigate to Tasks, then Back Up. Click Okay.
8. Right click the database again, navigate to Tasks, then Restore. Make the database name band_tracker_test. Click Okay.
9. Navigate to the repository file path (usually /Users/[UserName]/Desktop/band-tracker)
10. Start local server with "dnx kestrel" command in PowerShell
11. Go-to local server address (localhost:5004) to view webpage
12. Follow on-screen instructions

## Known Bugs
* No known bugs at this time.

## Technologies Used
* C#
  * Nancy framework
  * Razor View Engine
  * ASP.NET Kestrel HTTP server
  * xUnit

* SQL

* HTML

## Legal
Copyright (c) 2017 **_Jared Luedtke_** All Rights Reserved.
Licensed under the MIT license.
