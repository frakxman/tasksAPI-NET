# Tasks API

REST API built with .NET to manage tasks, complementing the Angular task list application.

## Requirements

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Structure

```
TasksAPI/
├── Controllers/      # Controladores de la API
├── Models/           # Modelos de datos
├── Data/             # Contexto y configuración de base de datos
├── Properties/       # Configuración de ejecución
└── README.md         # Este archivo
```

## Instalación

1. Instalar el [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Abrir una terminal en el directorio `TasksAPI`
3. Ejecutar `dotnet restore` para instalar dependencias

## Ejecución

```bash
cd TasksAPI
dotnet run
```

La API estará disponible en:
- http://localhost:3000/api/tasks (API)
- http://localhost:3000/swagger (Documentación)

## Endpoints

- `GET /api/tasks` - Obtener todas las tareas
- `GET /api/tasks/{id}` - Obtener una tarea por ID
- `POST /api/tasks` - Crear una nueva tarea
- `PUT /api/tasks/{id}` - Actualizar una tarea completa
- `PATCH /api/tasks/{id}` - Actualizar parcialmente una tarea
- `DELETE /api/tasks/{id}` - Eliminar una tarea

## Modelo de datos

```json
{
  "id": "string",
  "title": "string",
  "description": "string",
  "status": "string"
}
```

## Integración con Angular

La API está configurada para funcionar con la aplicación Angular en `http://localhost:4200` mediante políticas CORS.

Para que la aplicación Angular use esta API en lugar de json-server, actualiza la URL en el servicio de tareas: 
