# StoreApp Microservices

## Microservicios

Hemos creado los siguientes microservicios:
We have created the following microservices:

### 1. Product Microservice
- CRUD básico para productos.
- Cada producto incluye: nombre, descripción, precio y cantidad en stock.

- Basic CRUD for products.
- Each product includes: name, description, price, and stock quantity.

### 2. Customer Microservice
- CRUD para clientes.
- Campos: nombre, email, dirección, fecha de registro.

- CRUD for customers.
- Fields: name, email, address, registration date.

### 3. Order Microservice
- Permite crear órdenes a partir de un Customer y Productos seleccionados.
- Calcula el total de la compra y actualiza el stock de los productos.
- Permite listar órdenes históricas.
- Si no hay stock suficiente, limita la compra a la cantidad disponible.

- Allows creating orders from a Customer and selected Products.
- Calculates the total purchase amount and updates product stock.
- Allows listing historical orders.
- If there’s not enough stock, limits the purchase to available quantity.

### 4. Identity Microservice (opcional)
- Permite registrar y loguear usuarios.
- Al registrar un usuario, se crea automáticamente un customer asociado.

- Allows registering and logging in users.
- When registering a user, a linked customer is automatically created.

## Tecnologías utilizadas / Technologies used

- .NET 8
- Entity Framework Core
- SQL Server
- Visual Studio 2022
- Swagger

## Estado actual / Current status

✅ Product API funcionando
✅ Customer API funcionando
✅ Order API funcionando
✅ Identity API funcionando
✅ Se crean Customers automáticamente al registrar usuarios
✅ Microservicios corriendo simultáneamente
✅ Se actualiza el stock al crear órdenes
✅ CRUD completo en Products, Customers, Orders

## Cómo correr los microservicios / How to run the microservices

1. Abrir solución en Visual Studio
2. Establecer todos los proyectos API como **Startup Projects** (Multiple startup projects)
3. Ejecutar la solución (F5)
4. Acceder a cada API en Swagger:

1. Open solution with Visual Studio
2. Set all projects as **Startup Projects** (Multiple startup projects)
3. Build the solution by pressing (F5)
4. Access the API using the Swagger interface:

- Product API: `https://localhost:7154/swagger`
- Customer API: `https://localhost:7022/swagger`
- Order API: `https://localhost:7013/swagger`
- Identity API: `https://localhost:7053/swagger`

## Notas / Notes

- Cada API usa su propia base de datos (ProductsDB, CustomersDB, OrdersDB, IdentityDB).
- Al eliminar registros desde SQL Server, se recomienda limpiar ambas tablas (ej: Customers + Users).
- Se utilizó HttpClient para comunicación entre microservicios (ProductClient, CustomerClient).

- Each API has its own DB (ProductsDB, CustomersDB, OrdersDB, IdentityDB).
- When deleting items using SQL server its recommended to clean the other tables aswell (ej: Customers + Users).
- Http was used to communicate the different services (ProductClient, CustomerClient).
