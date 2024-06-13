
Overview
This monolithic web application is designed to streamline and manage the operations of a dog grooming company. It efficiently handles information related to customers, dogs, groomers, dog vaccines, appointments, and payments for services.

Key Features
•	Customer Management: Displays all clients in a card format on the homepage. Clicking on a client shows detailed information, including their dogs. Clicking on any of the dogs displays detailed dog profiles including vaccination records.
•	Add Customer button: Allows you to add a customer to the application and add dogs to a customer’s account.
•	Search customer Button Functionality: Allows users to search for customers using various dropdown filters, presenting results in a list format for easy navigation.
•	Navigation Menu: Provides quick access to all managed entities, including dogs, groomers, appointments, and the checkout process.

Technology Stack
Frontend: 
•	Blazor Server (current) transitioning to Blazor Auto (combination of Blazor Server and Blazor WebAssembly in one solution)
•	Radzen components
•	MatBlazor
•	JavaScript
•	HTML, CSS, Bootstrap

Backend:
•	ASP.NET class library
•	Web APIs
•	C#

Database: 
•	MS SQL Server

Services:
•	Azure B2C for authentication and authorization
•	Stripe API for managing payments
•	Azure Web Apps for deployment

Version Control and CI/CD:
•	GitHub for version control and team collaboration
•	GitHub Actions for continuous integration and continuous deployment (CI/CD)

User Interface
•	Home Page: Lists all clients in a card format. Provides buttons to add a customer and search for customers.
•	Client Details: Detailed information about each client, including their dogs and the dogs' vaccination records.
•	Search Results: Displays customer information in a list format, allowing easy filtering and navigation to detailed views.
•	Navigation Menu: Links to manage dogs, groomers, appointments, and the checkout process.


