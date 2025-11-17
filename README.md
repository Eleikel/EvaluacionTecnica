# 📋 Proyecto ASP.NET 8 - MVC + API

## 📖 Descripción del Proyecto

Este es un proyecto completo desarrollado en **ASP.NET 8** que combina dos arquitecturas:
- **MVC (Model-View-Controller)**: Para la interfaz web con vistas Razor
- **Web API RESTful**: Para servicios de backend

El proyecto implementa una arquitectura moderna y escalable, ideal para aplicaciones empresariales que requieren tanto una interfaz de usuario web como endpoints API para integración con otras aplicaciones o clientes móviles.

---

## 🛠️ Tecnologías Utilizadas

- **.NET 8** - Framework principal
- **ASP.NET Core MVC** - Interfaz web
- **ASP.NET Core Web API** - Servicios REST
- **Entity Framework Core** - ORM para acceso a datos
- **SQL Server** - Base de datos relacional
- **C#** - Lenguaje de programación

---

## 📋 Requisitos Previos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) o superior
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (Express, Developer o superior)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) (recomendado)

---

## 🚀 Instalación y Configuración

### 1. Clonar el Repositorio

```bash
git clone https://github.com/Eleikel/Prueba-Backend-GestorUsuarios-Simetrica
```

### 2. ⚠️ **IMPORTANTE: Configurar la Base de Datos**

#### **PASO CRÍTICO: Ejecutar el Script de Base de Datos**

**Antes de ejecutar la aplicación, DEBES crear la base de datos ejecutando el script SQL proporcionado.**

1. Abre **SQL Server Management Studio (SSMS)**
2. Conéctate a tu instancia de SQL Server
3. Localiza el archivo de script: `EvaluacionTecnicaDB.sql`
4. Abre el script en SSMS
5. **Ejecuta el script completo** (F5 o botón "Ejecutar")
6. Verifica que la base de datos y todas las tablas se hayan creado correctamente

> **⚠️ NOTA IMPORTANTE**: El proyecto **NO funcionará** sin ejecutar primero este script. La base de datos debe estar completamente configurada antes de iniciar la aplicación.



### 3. Configurar la Cadena de Conexión

Edita el archivo `appsettings.json` y actualiza la cadena de conexión con tu información de SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=NOMBRE_BD;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 4. Restaurar Dependencias

```bash
dotnet restore
```

### 5. Compilar el Proyecto

```bash
dotnet build
```

### 6. Ejecutar la Aplicación

```bash
dotnet run
```

O desde Visual Studio: presiona **F5** o haz clic en el botón "▶ Iniciar"

---

## 🔧 Características Principales

### MVC (Interfaz Web)
- ✅ Interfaz de usuario intuitiva con Razor Pages
- ✅ Validación de formularios del lado del cliente y servidor
- ✅ Manejo de autenticación y autorización
- ✅ Diseño responsive

### Web API
- ✅ Endpoints RESTful
- ✅ Respuestas en formato JSON
- ✅ Documentación automática con Swagger
- ✅ Validación de modelos
- ✅ Manejo de errores centralizado

---

## 📝 Endpoints Principales de la API

### Auth
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/Auth/Login` | Loguearse |


### USUARIO
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/User` | Obtener todos los usuarios |
| GET | `/api/User/{id}` | Obtener un usuario específico |
| POST | `/api/User` | Crear un nuevo usuario |
| PUT | `/api/User/{id}` | Actualizar un usuario existente |
| DELETE | `/api/User/{id}` | Eliminar un usuario |

### ROL
| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/Role` | Obtener todos los roles |
| GET | `/api/Role/{id}` | Obtener un rol específico |
| POST | `/api/Role` | Crear un nuevo rol |
| PUT | `/api/Role/{id}` | Actualizar un rol existente |
| DELETE | `/api/Role/{id}` | Eliminar un rol |

---

## 🐛 Solución de Problemas

### Error de conexión a la base de datos
- ✅ Verifica que SQL Server esté ejecutándose
- ✅ **Confirma que ejecutaste el script de base de datos**
- ✅ Revisa la cadena de conexión en `appsettings.json`
- ✅ Verifica que el usuario tenga permisos suficientes

### El proyecto no compila
- ✅ Ejecuta `dotnet clean` seguido de `dotnet restore`
- ✅ Verifica que tengas .NET 8 SDK instalado: `dotnet --version`

### Puerto ya en uso
- ✅ Cambia el puerto en `launchSettings.json` (carpeta Properties)
- ✅ O detén el proceso que está usando el puerto


## ⚠️ Recordatorio Final

**Antes de iniciar la aplicación por primera vez:**
1. ✅ Ejecuta el script SQL para crear la base de datos
2. ✅ Configura la cadena de conexión en `appsettings.json`
3. ✅ Verifica que SQL Server esté ejecutándose
4. ✅ Ejecuta `dotnet restore` y `dotnet build`

**¡Ahora estás listo para ejecutar el proyecto!** 🚀


## MVC Interfaz Web
<img width="1536" height="1163" alt="image" src="https://github.com/user-attachments/assets/f456123c-63a5-43ac-a69d-3c466e57611e" />
<img width="2544" height="1312" alt="image" src="https://github.com/user-attachments/assets/0eb690df-cb24-4eff-883e-46b656143af4" />
<img width="2546" height="1311" alt="image" src="https://github.com/user-attachments/assets/a33c0401-5e49-4f07-a5ac-e108fd0ec326" />
<img width="2545" height="1314" alt="image" src="https://github.com/user-attachments/assets/31d3ed87-98bc-49d4-a5e0-23a46598ccbf" />
<img width="2544" height="1312" alt="image" src="https://github.com/user-attachments/assets/9277fa02-815d-4bea-9c1d-b9e77c91f758" />
<img width="2542" height="1318" alt="image" src="https://github.com/user-attachments/assets/68310518-124d-4926-b6d1-832d149d9d30" />




## Endpoints WEB API
<img width="1608" height="1186" alt="image" src="https://github.com/user-attachments/assets/209e1207-ce83-47a0-a5c1-b48b53a65431" />
<img width="1575" height="1198" alt="image" src="https://github.com/user-attachments/assets/14a3c174-a138-4265-9d16-3ce4260c0df7" />
<img width="1573" height="1290" alt="image" src="https://github.com/user-attachments/assets/972c62e1-de0f-4c11-8ffd-11950a41db48" />













