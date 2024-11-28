# Car Rental System REST API

The Car Rental System REST API is a robust, RESTful web service developed using **C#** and **ASP.NET Core**. It provides essential functionalities for managing a car rental business, including car management, user registration, role-based authentication, car rental processing, and notification services.

The API is secured with **JWT (JSON Web Token)-based authentication** and supports role-based authorization for **Admin** and **User** roles.

---

## Features

### 🚗 Car Management
- Add, update, and delete cars (**Admin-only access**).
- Retrieve the list of available cars.
- Comprehensive car detail management.

### 👤 User Management
- User registration and login functionality.
- Role-based authorization (**Admin/User roles**).

### 📅 Car Rentals
- Check car availability and rent a car.
- Automatic rental price calculation based on duration.

### 🔐 Authentication
- Secure endpoints with **JWT-based authentication**.
- Role-based policies to restrict access.

### 📧 Email Notifications
- Sends booking confirmation emails upon successful car rental using **SendGrid**.

---

## Tools & Technologies
- **Backend:** C#, ASP.NET Core
- **Database:** Entity Framework Core with SQL Server
- **Authentication:** JWT (JSON Web Tokens)
- **Email Service:** SendGrid for notification emails
- **Testing:** Postman for manual testing of endpoints
- **API Documentation:** Swagger UI for seamless testing and documentation

---

## API Endpoints

![z1](https://github.com/user-attachments/assets/db44bccc-ddb1-40fe-ba42-428481424fc3)

### User Management

1. **Register a New User**  
   **Endpoint:** `POST /api/users/register`  
   **Description:** Register a new user with either an Admin or User role.

   ![z2](https://github.com/user-attachments/assets/041017cd-15f7-4f8d-a2a5-582b3f0582a1)

2. **User Login**  
   **Endpoint:** `POST /api/users/login`  
   **Description:** Authenticate a user and generate a JWT token for secure access.
   ![z3](https://github.com/user-attachments/assets/bb5a6732-7d15-4c2b-87c4-ae3e4d777b85)
   

### Car Management

1. **Get All Cars**  
   **Endpoint:** `GET /api/cars`  
   **Description:** Retrieve a list of all available cars in the system.  
   **Access:** Open to all users.
   ![z13](https://github.com/user-attachments/assets/6da2dc09-6010-4a9b-8851-f17658738f3e)

2. **Get Cars By Id**  
   **Endpoint:** `GET /api/cars/{id}`  
   **Description:** Retrieve an available car in the system with help of id.  
   **Access:** Open to all users.
   ![z8](https://github.com/user-attachments/assets/a3620116-28e1-4696-8a5b-b5269c167c49)
   
3. **Add a Car**  
   **Endpoint:** `POST /api/cars`  
   **Description:** Add a new car to the fleet.  
   **Access:** Admin only.
   ![z4](https://github.com/user-attachments/assets/2d05131d-8630-466a-9a6a-1120af47235c)
   ![z5](https://github.com/user-attachments/assets/89041080-1b2e-4987-bc50-fd23ab0bda70)

4. **Update Car Details**  
   **Endpoint:** `PUT /api/cars/{id}`  
   **Description:** Update the details and availability of a specific car .  
   **Access:** Admin only.
   ![z7](https://github.com/user-attachments/assets/d142fa63-3e32-4557-8c05-d8e4aecfdefd)
   ![z8](https://github.com/user-attachments/assets/a3620116-28e1-4696-8a5b-b5269c167c49)
   ![z9](https://github.com/user-attachments/assets/ee29df03-9cc6-40ac-9fb6-c56940f822e0)

5. **Delete a Car**  
   **Endpoint:** `DELETE /api/cars/{id}`  
   **Description:** Remove a car from the system ).  
   **Access:** Admin only.
   ![z10](https://github.com/user-attachments/assets/cc183833-f286-40e3-b2ae-39d299a105f8)
   ![z11](https://github.com/user-attachments/assets/4531ff63-82c7-49d8-a5d4-0f51fe89e4d6)

---

### Car Rentals

1. **Rent a Car**  
   **Endpoint:** `POST /api/rentals`  
   **Description:** Book a car rental after checking availability.  
   **Access:** Authenticated users only.  
   **Notification:** A confirmation email is sent upon successful booking.

   ![z12](https://github.com/user-attachments/assets/5ac79efa-fb67-4e7f-b021-b9f28de790f8)



