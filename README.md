
# StoreAppMicroservices

Este proyecto implementa una arquitectura de microservicios utilizando **ASP.NET Core 8**, siguiendo buenas prácticas como **DDD (Domain-Driven Design)** y **Clean Architecture**.

## 📦 Microservicios implementados

✅ **Product Microservice** (completo):

- CRUD de productos con los campos:
  - `Id` (Guid)
  - `Name` (string)
  - `Description` (string)
  - `Price` (decimal)
  - `Stock` (int)
- Comunicación con base de datos SQL Server usando **Entity Framework Core (Code First)**.
- Exposición de API REST mediante **ASP.NET Core Web API**.
- Validaciones con **FluentValidation**.
- Mapeo de modelos con **AutoMapper**.
- Documentación con **Swagger UI**.

---

## 🗄️ Base de datos

La base de datos **`ProductDb`** se crea automáticamente mediante **EF Core Migrations**.

✔️ Para aplicar la migración inicial, ya fue ejecutado:

```bash
Add-Migration InitialCreate -StartupProject Product.API -Project Product.Infrastructure
Update-Database -StartupProject Product.API -Project Product.Infrastructure
```

---

## 🖥️ Ejecutar el microservicio

1. Abrir la solución `StoreAppMicroservices.sln` en **Visual Studio 2022**.
2. Seleccionar **Product.API** como proyecto de inicio.
3. Ejecutar con **F5** (Debug).
4. Acceder a **Swagger UI** en:

```
https://localhost:7154/swagger/index.html
```

---

## 📚 Endpoints disponibles

| Método  | Ruta               | Descripción              |
|---------|--------------------|------------------------|
| GET     | /api/products       | Obtener todos los productos |
| GET     | /api/products/{id}  | Obtener producto por Id |
| POST    | /api/products       | Crear un nuevo producto |
| PUT     | /api/products/{id}  | Actualizar producto     |
| DELETE  | /api/products/{id}  | Eliminar producto       |


---

## ⚙️ Tecnologías utilizadas

- ASP.NET Core 8
- Entity Framework Core
- SQL Server LocalDB
- AutoMapper
- FluentValidation
- Swagger / OpenAPI
