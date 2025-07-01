# 🎓 Classroom Reservation System

A classroom reservation system – a web application built with ASP.NET Core Razor Pages.

## 🚀 Features

- 🔐 **User Management** – Login and registration with ASP.NET Core Identity  
- 🛡 **Role-Based Authorization** – Admin and Instructor permissions  
- 🕒 **Classroom Reservation** – Date & time-based scheduling with conflict prevention  
- ✉️ **Email Notifications** – Automatic email sending via SMTP  
- 💬 **Feedback System** – User feedback delivered to admin  
- 📊 **Reporting** – Detailed usage and reservation reports in admin panel  


## Technologies

| Layer     | Technology                              |
|-----------|------------------------------------------|
| Backend   | ASP.NET Core 9.0, Entity Framework Core |
| Frontend  | Razor Pages, Bootstrap 5, jQuery        |
| Database  | SQL Server                              |
| Identity  | ASP.NET Core Identity                   |

## ⚙️ Installation

### 💡 Requirements

- .NET 9.0 SDK  
- SQL Server (LocalDB or Full Edition)  
- Visual Studio 2022 (recommended)

### 🔧 Steps

1. Clone the project:
```bash
git clone https://github.com/gokaycetin/ClassroomReservation.git
cd ClassroomReservation
```

2. Configure the `appsettings.json` file:
```bash
cp appsettings.example.json appsettings.json
```

3. Update the following settings in `appsettings.json`:
   - **ConnectionStrings**: Set your SQL Server connection string
   - **SmtpSettings**: Set your SMTP email server settings

4. Apply the database migrations:
```bash
dotnet ef database update
```

5. Run the application:
```bash
dotnet run
```

## 🔐 Configuration

### Database Connection

Update the `ConnectionStrings` section in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "ClassroomDbConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 📧 Email Settings

Configure your SMTP settings:

```json
{
  "SmtpSettings": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "User": "your-email@gmail.com",
    "Password": "your-app-password"
  }
}
```

**Note**: If you're using Gmail, 2FA must be enabled and you must use an App Password.

### 👥 User Roles

- **Admin**: Full system management, user management, reporting  
- **Instructor**: Reserve classrooms, view their own reservations  
- **User**: Basic user operations

## 🗂 Project Structure

```
ClassroomReservation/
├── Data/                   # Entity Framework DbContext
├── Models/                 # Data models
├── Pages/                  # Razor Pages
│   ├── Admin/             # Admin panel pages
│   ├── Instructor/        # Instructor panel pages
│   └── Shared/            # Shared layouts and components
├── Services/              # Business logic services
├── Migrations/            # EF Core migrations
└── wwwroot/              # Static files (CSS, JS, images)
```

## 📄 Project Report

Project Report:  
[Classroom-Reservation Report](./CENG382_Report.pdf)

## 📬 Contact

Gökay Çetinakdoğan – gokaycetin44@gmail.com  
LinkedIn – [https://www.linkedin.com/in/gokay-cetinakdogan/](https://www.linkedin.com/in/gokay-cetinakdogan/)  
Project Link – [https://github.com/gokaycetin/ClassroomReservation](https://github.com/gokaycetin/ClassroomReservation)

## 🛡 Security Notes

- `appsettings.json` is included in `.gitignore`  
- Use environment variables for sensitive data  
- Use strong passwords in production  
- HTTPS is required in production environments
