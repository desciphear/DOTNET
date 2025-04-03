# 📌 Project Name: DealMate

## 🚀 Overview
This project provides **Vendors** with a platform to **display, manage, and control their offers** in one place.
It also allows **Admins** to monitor activities and approve or reject offers while **Customers** can browse and redeem offers.

---

## 🛠 Tech Stack
- **Backend:** .NET MVC (C#)
- **Frontend:** HTML, CSS, JavaScript
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core (Scaffolding)

---

## 📥 Installation Guide

### 1️⃣ **Setup Database**
1. Install **Microsoft SQL Server** and **SQL Server Management Studio (SSMS)**.
2. Create a new database and set up the following schema:
   - `Users` Table (User Management)
   - `Usedoffer` Table (Track used offers)
   - `AdminCred` Table (Admin credentials)
   - `Customer` Table (Customer login details)
   - `Requests` Table (Offer requests and approval tracking)

3. Execute the SQL script to create tables and relationships as per the shared schema.

### 2️⃣ **Connect Database to Project**
1. Open the project in **Visual Studio**.
2. Update the **connection string** in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;"
   }
   ```
3. Use **Entity Framework Core scaffolding** to generate models:
   ```bash
   dotnet ef dbcontext scaffold "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
   ```

### 3️⃣ **Run the Project**
1. Build the project in Visual Studio.
2. Run it using **IIS Express** or **.NET CLI**:
   ```bash
   dotnet run
   ```
3. Open a browser and navigate to `http://localhost:PORT_NUMBER`

---

## 📌 Usage & Features

### 🔹 Features & Roles
✅ **Vendors** can:
- Upload and manage offers
- Control the availability and details of their offers

✅ **Admins** can:
- View and approve offers
- Deny suspicious users from logging in and uploading offers
- Control the **Offer Page**

✅ **Customers** can:
- View all available offers on the Offer Page
- Scan a **QR code** to get the offer code

---

## 📸 Database Screenshot
![image](https://github.com/user-attachments/assets/37c04416-71eb-4fe4-be23-5b04d06e7933)
![image](https://github.com/user-attachments/assets/6eec40db-e8c1-4abf-93f0-ee2c16f74180)

---

## 👀 Demo
![Project Output](https://github.com/user-attachments/assets/1956e7ef-bb55-4e2d-8ad4-605f6f0d8cd2)

---



## 💡 Contributors
- Piyush

For any questions or contributions, feel free to open an issue or pull request! 🚀
**[Click To Mail: 📧](mailto:piyush9891779219@gmail.com)**
