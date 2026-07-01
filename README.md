# PhotoStore API

A simple and scalable ASP.NET Core Web API for managing and selling photos.  
This project demonstrates a real-world backend structure including file upload, CRUD operations, and clean architecture principles.

---

## 🚀 Features

- Upload photos with file storage (wwwroot/images)
- Retrieve all photos
- Get photo by ID
- Update photo (including image replacement)
- Delete photo (including file removal from server)
- Structured logging with ILogger
- Clean service-based architecture

---

## 🏗️ Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (Npgsql)
- C#
- File System Storage (wwwroot)
- Swagger for API testing

---

## 📁 Project Structure
PhotoStore
│
├── Controllers
├── Application
│ ├── DTOs
│ └── Interfaces
├── Infrastructure
│ ├── Services
│ └── Data
├── Domain
│ └── Entities
├── wwwroot
│ └── images
└── Program.cs

---

## 📸 Photo Model

Each photo contains:

- Id
- Title
- Price
- FileName
- FilePath (for serving images)

---

## 📤 Upload Example

### Request
POST /api/photos/upload
Content-Type: multipart/form-data


### Body
- Title
- Price
- File (image)

---

## 📥 Response Example

```json
{
  "id": 1,
  "title": "sunset",
  "fileName": "guid.jpg",
  "price": 150,
  "filePath": "/images/guid.jpg"
}
🗑️ Delete Behavior
* Deletes database record
* Deletes image file from server (wwwroot/images)

⚙️ Setup & Run

1. Clone repo

git clone https://github.com/your-username/PhotoStore.git

2. Restore packages

dotnet restore

3. Update database

dotnet ef database update

4. Run project

dotnet run

📌 Notes
Designed with separation of concerns (Controller / Service / DTO)
File system storage used instead of cloud storage for simplicity

📈 Future Improvements
Order system (photo purchasing)
Authentication & Authorization
Cloud storage (Azure / AWS S3)
CQRS architecture
Global error handling middleware
👩‍💻 Author

Mahsa Mehrabi
Backend Developer (.NET / ASP.NET Core)