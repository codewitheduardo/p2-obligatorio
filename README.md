# p2-obligatorio
Prototipo de un sistema de reservas de vuelos desarrollado para una aerolínea ficticia. Implementado con ASP.NET 8.0 MVC, sin uso de bases de datos. Incluye control de acceso por roles, emisión de pasajes, cálculo dinámico de precios y validaciones de negocio completas.

# ✈️ Sistema de Reservas de Vuelos - ASP.NET MVC (Sin Base de Datos)

Este proyecto es un prototipo académico de sistema de ventas de pasajes para una aerolínea, desarrollado utilizando ASP.NET 8.0 MVC **sin persistencia en base de datos**. Todos los datos se gestionan en memoria, simulando el funcionamiento real del sistema.

## 🧭 Funcionalidades principales

- Gestión de usuarios: **administradores** y **clientes** (premium y ocasionales).
- Catálogo simulado de **aeronaves** y sus características.
- Registro de **aeropuertos**, **rutas** y **vuelos** con validaciones como alcance y frecuencia.
- **Emisión de pasajes** con cálculo de precios dinámico, teniendo en cuenta:
  - Costo por asiento (distancia, costos aeroportuarios y operación).
  - Tipo de cliente y equipaje.
  - Tasas de salida/llegada.
  - Margen de ganancia.
- Interfaz web en ASP.NET 8.0 MVC con **control de acceso por roles**.
- Simulación completa sin necesidad de SQL Server ni Entity Framework.

## 🔐 Roles de usuario

- **Administrador**:
  - Puede modificar puntos de clientes premium.
  - Cambia la elegibilidad de regalos para clientes ocasionales.
  - Accede a funcionalidades administrativas protegidas.

- **Cliente**:
  - Visualiza y compra pasajes.
  - Accede a su información personal y reservas.
  - Se diferencia en comportamiento según sea premium u ocasional.

## ⚙️ Tecnologías utilizadas

- ASP.NET 8.0 MVC
- C# .NET
- CSS
- JavaScript
- Bootstrap 5
- Lógica de negocio en memoria (sin SQL, sin Entity Framework)
- Autenticación y autorización con roles

## 🚀 Cómo ejecutar el proyecto

1. Clona el repositorio:
   ```bash
   git clone https://github.com/tu-usuario/reservas-aerolinea.git
2. Abre el proyecto con Visual Studio 2022 o superior.
3. Ejecuta la aplicación (F5 o dotnet run).
4. Se cargan usuarios y datos de ejemplo automáticamente desde código.
