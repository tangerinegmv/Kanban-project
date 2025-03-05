# Kanban Project

Este es un proyecto de Kanban desarrollado en ASP.NET Core. El proyecto permite a los usuarios gestionar tareas en diferentes tableros, asignar tareas a usuarios y cambiar el estado de las tareas.

## Características

- Crear, modificar y eliminar tareas.
- Asignar tareas a usuarios.
- Cambiar el estado de las tareas.
- Visualizar tareas en un tablero Kanban.
- Soporte para tema oscuro.
- Niveles de acceso y autorizaciones.
- Validaciones de datos.
- Implementación de excepciones.
- Uso de ViewModels para la gestión de datos.

## Requisitos

- .NET 8.0
- SQLite

## Instalación

1. Clona el repositorio:
    ```bash
    git clone https://github.com/TallerDeLenguajes2/tl2-proyecto-2024-tangerinegmv.git
    ```

2. Navega al directorio del proyecto:
    ```bash
    cd tl2-proyecto-2024-tangerinegmv
    ```

3. Restaura las dependencias:
    ```bash
    dotnet restore
    ```

4. Aplica las migraciones de la base de datos:
    ```bash
    dotnet ef database update
    ```

5. Ejecuta la aplicación:
    ```bash
    dotnet run
    ```

## Uso

### Usuarios de Prueba

- **Administrador**
  - Usuario: `tangerine`
  - Contraseña: `12345`

### Funcionalidades

- **Crear Tarea**: Permite crear una nueva tarea en un tablero.
- **Modificar Tarea**: Permite modificar los detalles de una tarea existente.
- **Eliminar Tarea**: Permite eliminar una tarea existente.
- **Asignar Tarea**: Permite asignar una tarea a un usuario.
- **Cambiar Estado de Tarea**: Permite cambiar el estado de una tarea (Ideas, ToDo, Doing, Review, Done).

## Niveles de Acceso y Autorizaciones

El proyecto implementa diferentes niveles de acceso y autorizaciones para asegurar que solo los usuarios autorizados puedan realizar ciertas acciones. Los roles de usuario incluyen:

- **Administrador**: Tiene acceso completo a todas las funcionalidades del sistema.
- **Usuario**: Puede ver y modificar las tareas asignadas a él.

## Validaciones

El proyecto incluye validaciones de datos tanto en el lado del cliente como en el lado del servidor para asegurar la integridad de los datos. Las validaciones se implementan utilizando atributos de anotación de datos en los modelos.

## Implementación de Excepciones

El proyecto maneja excepciones de manera centralizada para asegurar que los errores se gestionen de manera adecuada y se proporcionen mensajes de error útiles a los usuarios. Las excepciones se registran utilizando un sistema de logging.

## ViewModels

El proyecto utiliza ViewModels para gestionar los datos que se pasan entre las vistas y los controladores. Los ViewModels permiten estructurar y validar los datos de manera eficiente.

## Estructura del Proyecto

- **Controllers**: Contiene los controladores de la aplicación.
- **Models**: Contiene las clases de modelo de datos.
- **Views**: Contiene las vistas de la aplicación.
- **ViewModels**: Contiene las clases de modelo de vista.
- **Repositorios**: Contiene las clases de repositorio para acceder a la base de datos.
- **wwwroot**: Contiene los archivos estáticos como CSS, JavaScript e imágenes.

## Contribuir

Si deseas contribuir a este proyecto, por favor sigue los siguientes pasos:

1. Haz un fork del repositorio.
2. Crea una nueva rama (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y haz commit (`git commit -am 'Agrega nueva funcionalidad'`).
4. Sube tus cambios a tu fork (`git push origin feature/nueva-funcionalidad`).
5. Abre un Pull Request en el repositorio original.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo LICENSE para más detalles.

## Contacto

Si tienes alguna pregunta o sugerencia, por favor abre un issue en el repositorio o contacta al administrador del proyecto.

---

¡Gracias por usar nuestro proyecto de Kanban!