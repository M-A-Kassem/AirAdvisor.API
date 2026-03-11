# Graduation_Project
A backend system built with ASP.NET Core Web API for an Air Conditioner platform that helps users browse AC products, calculate the suitable cooling capacity for their rooms, submit purchase requests, and interact with a simple chatbot assistant.
The project uses **ASP.NET Core Identity**, **JWT Authentication**, **Entity Framework Core**, and **SQL Server**.
## Features
- User registration and login
- JWT-based authentication
- Role-based authorization with `Admin` and `User`
- Air Conditioner product management
- Room cooling load calculation
- Product recommendation based on room calculation
- Purchase request workflow with admin approval
- Chatbot for basic AC guidance
- Admin dashboard with analytics
- Swagger/OpenAPI for API testing
## Tech Stack
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- AutoMapper
- Swagger / Swashbuckle
### Domain
Contains:
- Entities
- Repository interfaces
- Unit of Work interface
### Application
Contains:
- DTOs
- Service interfaces
- Business logic
- Mapping profiles
### Infrastructure
Contains:
- EF Core DbContext
- Fluent API configurations
- Identity user model
- Repository implementations
### API
Contains:
- Controllers
- Dependency injection setup
- Authentication configuration
- Swagger configuration
- Application startup
## Main Modules
### Authentication
- Register
- Login
- JWT token generation
### Products
The system is specialized in **Air Conditioners only**.
Each product includes:
- ProductId
- Brand
- Model
- CoolingCapacity
- Price
- Description
- StockQuantity
### Room Calculation
Users can calculate the required AC capacity using:
- Length
- Width
- Height
- ThermalFactor
Formula:
- If `ThermalFactor = true`  
  `CoolingLoad = Length × Width × Height × 300`
- If `ThermalFactor = false`  
  `CoolingLoad = Length × Width × Height × 250`
The API returns:
- RoomVolume
- CoolingLoad
- RecommendedCapacity
### Sales / Orders
Users do not directly purchase products instantly.
Instead:
1. The user creates a purchase request
2. The order is stored with status `Pending`
3. Admin reviews the request
4. Admin can:
   - Accept
   - Reject
5. User sees the updated status in their orders list
Possible statuses:
- Pending
- Accepted
- Rejected
### Chatbot
Users can send questions to the chatbot and receive responses.
Messages are stored in the database for later analysis.
### Admin Dashboard
The admin can access:
- Total users
- Total products
- Total sales
- Total revenue
- Best selling products
- Sales per month
- Top customers
- Pending purchase requests
## Roles
### Admin
Can:
- Create users
- Delete users
- Manage products
- View all sales
- View sales by user
- Review pending orders
- Accept or reject orders
- View dashboard analytics
### User
Can:
- Register
- Login
- View products
- Calculate room cooling needs
- Create purchase requests
- View their order history
- Chat with the chatbot
## Database Entities
- `ApplicationUser`
- `Product`
- `RoomCalculation`
- `Sale`
- `ChatMessage`
## API Endpoints
## AuthController
- `POST /api/auth/register`
- `POST /api/auth/login`
## ProductController
- `GET /api/product`
- `GET /api/product/{id}`
- `GET /api/product/brand/{brand}`
- `GET /api/product/recommended?coolingLoad=value`
- `POST /api/product` Admin only
- `PUT /api/product/{id}` Admin only
- `DELETE /api/product/{id}` Admin only
## RoomController
- `POST /api/room/calculate`
- `GET /api/room/my-calculations`
## SalesController
- `POST /api/sales`
- `GET /api/sales/my`
- `GET /api/sales` Admin only
- `GET /api/sales/user/{userId}` Admin only
## ChatbotController
- `POST /api/chatbot/message`
- `GET /api/chatbot/history`
## AdminController
- `GET /api/admin/dashboard`
- `GET /api/admin/users`
- `POST /api/admin/users`
- `DELETE /api/admin/users/{userId}`
- `GET /api/admin/sales`
- `GET /api/admin/sales/user/{userId}`
- `GET /api/admin/orders/pending`
- `PUT /api/admin/orders/{saleId}/accept`
- `PUT /api/admin/orders/{saleId}/reject`
## Getting Started
### Prerequisites
Make sure you have installed:
- .NET 8 SDK
- SQL Server
- Visual Studio 2022 or VS Code
### Configuration
Update your connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=GraduationProjectDb;Trusted_Connection=True;TrustServerCertificate=True"
}
Also configure JWT settings if needed:


"Jwt": {
  "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
  "Issuer": "GraduationProjectAPI",
  "Audience": "GraduationProjectClient",
  "ExpireHours": "24"
}
