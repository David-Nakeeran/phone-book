## Phonebook

Console application that performs CRUD operations on contact records.
Developed using C#, Spectre.Console and Entity Framework.

For testing of email sending functionality, please create a MailTrap free trail.
Follow below steps to retrieve settings

- Navigate to Email testing
- Under Action field, click settings
- Make a note of the Username and Password for setting User Secrets

## How to run application

- Ensure you have .NET SDK latest version

```
git clone https://github.com/your-username/phone-book.git
cd phone-book
```

- Database set up
  Before running the application for the first time, you need to create a database and apply migrations

1. Ensure SQL Server is running
2. Apply migrations by running the step below, in project directory via terminal

```
dotnet ef database update
```

- Set up User Secrets

```
dotnet user-secrets init
dotnet user-secrets set "MailSettings:UserName" "<your-mailtrap-username>"
dotnet user-secrets set "MailSettings:Password" "<your-mailtrap-password>"
```

- These secrets will override the UserName and Password values in appsettings.json

- Connection Strings: Provide your database connection details

- Open a terminal or command prompt into the project directory.
- Build the app:
  `dotnet build`
- Run the app:
  `dotnet run`

## Requirements

1. **Phonebook**:

- This is an application where you should record contacts with their phone numbers.

- Users should be able to Add, Delete, Update and Read from a database, using the console.

- You need to use Entity Framework, raw SQL isn't allowed.

- Your code should contain a base Contact class with AT LEAST {Id INT, Name STRING, Email STRING and Phone Number(STRING)}

- You should validate e-mails and phone numbers and let the user know what formats are expected

- You should use Code-First Approach, which means EF will create the database schema for you.

- You should use SQL Server, not SQLite

2. **Error Handling**:

   - Able to handle all possible errors so that the application never crashes.

3. **Follow DRY Principle**:

   - Avoid code repetition.

4. **Separation of Concerns**:

   - Object-Oriented Programming

## Features

- User Secrets

  - Securely stores sensitive information like MailTrap settings.
  - Prevents sensitive data from being exposed in the codebase by overriding appsettings.json values.

- Email Sending:

  - Includes email-sending functionality using MailTrap in a sandbox environment.
  - Users can compose and send emails directly from the application.

- Category Management:

  - Includes Categories table linked to contacts via a foreign key.
  - Enables users to assign categories to contacts (e.g., Friends, Family, Work).
