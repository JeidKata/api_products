# 🛒 API REST de Productos con .NET Core 7 y Dapper

Este proyecto es una Prueba Técnica que consiste en el desarrollo de una API REST simple para gestionar productos (CRUD), utilizando **.NET Core 7** y **C#**. La implementación del acceso a datos sigue el patrón de repositorio con la micro-ORM **Dapper**.


![C#](https://img.shields.io/badge/C%23-v14-68217A.svg)
![.NET](https://img.shields.io/badge/.NET-v10.0-c82333.svg)
![Dapper](https://img.shields.io/badge/Dapper-v2.1.35-5a433f.svg)
![Microsoft.Data.Sqlite](https://img.shields.io/badge/SQLite-v10.0-blue.svg)
![Swashbuckle.AspNetCore](https://img.shields.io/badge/Swagger-v10.0-d3b18c.svg)

---

## 🚀 Requisitos para la Ejecución

Para poder clonar, construir y ejecutar este proyecto localmente, necesitas tener instalado el siguiente software:

* **SDK de .NET 8.0 o superior:** Requerido para compilar y ejecutar la aplicación.
* **Editor de Código:** Visual Studio, VS Code.
* **Base de Datos:** El proyecto está configurado para usar:
    * Una base de datos en **memoria** (SQLite) para simplificar la prueba, **O**
    * Una instancia de **SQL Server** en tu máquina local.

---
## 📁 Estructura del Proyecto

```
proyecto_backend/
├── 📄 appsettings.json                  # Contiene la cadena de conexión y variables de entorno
├── 📄 ProductDB.db                      # Base de datos SQLite (auto-generada)
├── 📄 README.md                         # Documentación del proyecto
├── 📄 .gitignore                        # Archivos excluidos del control de versiones
├── 📄 Program.cs                        # Es el punto de entrada de la aplicación
├── 📄 API_Product.csproj                # Es el archivo XML del proyecto C# (dependencias, configuraciones)
│
├── 📁 Api_Product/      
│   ├── 📁 Repositories/ 
│   |   ├── 📄 IProductRepository.cs     # Interfaz
│   │   └── 📄 ProductRepository.cs      # Implementación con Dapper
|   |
│   ├── 📁 Properties/ 
│   │   └── 📄 launchSettings.json       # Configuración del lanzamiento
|   |
│   ├── 📁 Models/ 
│   │   └── 📄 Product.cs                # Modelo de productos con sus propiedades
│   │
│   └── 📁 Controllers/                  # Controladores de API
│       └── 📄 ProductsController.cs     # Controlador Productos con Swagger ⭐

└── 📁 .vs                               # ignorado por Git
```

---
## 🛠️ Configuración e Instalación

Sigue estos pasos para poner en marcha el proyecto:

1.  **Clonar el Repositorio:**
    ```bash
    git clone  https://github.com/JeidKata/api_products.git
    ```

2.  **Configurar la Base de Datos:**
    * **Opción A (Base de Datos en Memoria):** No requiere configuración adicional. La base de datos se inicializará al iniciar la aplicación.
    * **Opción B (SQL Server):**
        * Asegúrate de que tu instancia de SQL Server esté corriendo.
        * Actualiza el *connection string* en el archivo `appsettings.json` con tus credenciales y nombre de base de datos.

3.  **Restaurar Dependencias:**
    ```bash
    dotnet restore
    ```
    Si no funciona el comando anterior puedes intentar con los siguientes:

    ```bash
    # valida que paquetes tiene el proyecto
    dotnet list API_Product package

    # Paquete de nivel superior                               Solicitado            Resuelto
    # Dapper                                                2.1.35                2.1.35
    # Microsoft.Data.Sqlite                                 10.0.0                10.0.0
    # Microsoft.VisualStudio.Web.CodeGeneration.Design      10.0.0-rc.1.25458.5   10.0.0-rc.1.25458.5
    # Swashbuckle.AspNetCore                                10.0.1                10.0.1
    
    # Si tiene Visual Studio 2026
    # puede instalar Dapper con el comando
    Install-Package Dapper
    
    #Puede agregar el paquete para el Swagger.
    dotnet add API_Product package Swashbuckle.AspNetCore
    ```

5.  **Ejecutar la Aplicación:**
    ```bash
    dotnet run
    ```
    La API estará disponible en `https://localhost:7083`. El puerto por defecto puede variar, pero se mostrará en la consola.

---

## 📚 Modelo de Datos

La API gestiona la entidad `Product`. La clase C# utilizada para el modelo de datos es la siguiente:

| Propiedad | Tipo | Descripción |
| :--- | :--- | :--- |
| **Id** | `int` | Identificador único del producto. |
| **Name** | `string` | Nombre del producto. |
| **Description** | `string` | Descripción detallada del producto. |
| **Price** | `decimal` | Precio unitario del producto. |
| **Category** | `string` | Categoría a la que pertenece el producto. |

---

## 📌 Endpoints de la API

La API implementa las operaciones **CRUD** completas para la entidad `Product`.

| Método | Endpoint | Descripción | Operación CRUD |
| :--- | :--- | :--- | :--- |
| **GET** | `/api/products` | Devuelve una lista de todos los productos. | Read (Todo)  |
| **GET** | `/api/products/{id}` | Devuelve un solo producto basado en su ID. | Read (Por Id) |
| **POST** | `/api/products` | Crea un nuevo producto. (Requiere estructura JSON) | Create |
| **PUT** | `/api/products/{id}` | Actualiza un producto existente. (Requiere estructura JSON) | Update (Por Id) |
| **DELETE** | `/api/products/{id}` | Elimina un producto basado en su ID. | Delete (Por Id) |

### Ejemplo de JSON para POST/PUT (`/api/products`)

```json
{
  "name": "Laptop Pro",
  "description": "Portátil de alto rendimiento.",
  "price": 1250.99,
  "category": "Electrónica"
}
```
# 🧪 Probando la API

Esta API puede ser probada utilizando herramientas como **Postman**, **cURL**, directamente desde el navegador si estás utilizando los endpoints GET, pero con los siguientes comandos puedes realizar pruebas manuales en tu consola/terminal o PowerShell.
#### Puedes crearlo en tu PowerShell

```Bash
Invoke-RestMethod -Method Post -Uri 'http://localhost:5157/api/products' -Headers @{ 'Content-Type' = 'application/json' } -Body '{ "name":"TV LG", "price":29.99, "description":"An LG brand TV.", "category":"Electronics" }'
```
o de la siguiente forma, esto depende de como este configurado tu PowerShell

```Bash
curl -v -H "Content-Type: application/json" -d '{"name":"TV LG","price":29.99,"description":"An LG brand TV.","category":"Electronics"}' http://localhost:5157/api/products

