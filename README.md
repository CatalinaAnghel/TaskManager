# TaskManager

This is a project implemented using ASP.Net Core 3.1 with Entity Framework Core. This project is a web application which is using the authorization and authentication system called Identity.
Task Manager is a WEB application that can be used for the management of a project and its tasks. This WEB application can be used by the students (regardless of their technical knowledgements) and it can also be used by a company's employees (but it is not limited to these types of users).


*Functionalities:*
- login
- register
- Any logged in user can view the dashboard page where he/she can see general information regarding his/her projects and the number of open/urgent/finished tasks
- Any logged in user can view a project's details
- Any user (admin or regular user) can add a new project
- The users can edit a project if they work on that project
- A project can be deleted only if the user that performs the action is a PM in a team that works on that project.
- The users can create new teams for their projects and they will automatically become a PM.
- A new team member can be added only by the team's PM.
- A user can remove a team member only if he/she is the team's PM.
- The users can add tasks (if they are PMs) and they can edit tasks if the tasks are assigned to them or if they are the team's PM.
- The users can see their tasks
- The user will receive a number of points every time a task is completed.
- If the task is deleted or its status is updated to other task than "Done", then the points will be subtracted from the user's total score
- A user will receive a new badge based on his/her score
- The administrator can add/delete/edit badges.
- The users can see their badges.
