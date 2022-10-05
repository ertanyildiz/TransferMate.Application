
**TransferMate Application TSD**
 1. What does this task do? 
 2. Prerequisites 
 3. Tech Stack 
 4. How to run it  
 
 This task can list, add, edit and remove tasks and comments. It can also filter comments that belong to a task. The landing page has a list that shows tasks. They can be edited on this page or details can be checked by clicking the details button. The detail page can show details of the task and its comments under the details section. Tasks can be edited on this page. Comments can be edited and deleted in this page. On the form page each field has a validation rule that are defined in the API project like minimum length, required etc. Please see the TSD folder for screenshots and required db.

**Prerequisites**
 In order to run this task, please make sure you have these requirements below 
 - .NET Core 6
 - Node.js 
 - npm package manager 
 - Angular CLI
- MS SQL

**Tech Stack** 
This task has been developed using .NET Core for the backend and Angular for the Frontend. Backend project has 3 layers which are Data, Business and WEB. The Web layer has API endpoints to communicate with the UI. Angular has components and services to show UI by fetching date from the API

**How to run it** 
1. Restore Database 
	- Restore the database file in the zip to your database 
	- If you have issues restoring the backup file, please run the database creation script to create it manually. 
2. Run API project 
	- Go to `TransferMate.Web` folder and set your database connection in the file `appsettings.js` 
	- Go to `TransferMate.Web` folder and run the following command to start the API project
	- `dotnet run` 
3. Edit API URL port 
	 - Go to the `TransferMate.UI\src\environments` folder and edit the file environment `environment.ts` and set the URL port from the API project. This URL is used to communicate with the API endpoints.
	 - example: `https://localhost:{port}/api/`
4. Install npm packages 
	 - Go to the `TransferMate.UI` folder and run the following command to install the packages. 
	 - `npm install` 
5. Start UI 
	- Go to `TransferMate.UI` folder and the following command to start the UI project 
	- `ng serve --open` 
6. Test the application by adding task and comment