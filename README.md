# Kanban Project

## Índice / Table of Contents
- [Versión en Español](#versión-en-español)
- [English Version](#english-version)

## Versión en Español

Este es un proyecto de Kanban desarrollado en ASP.NET Core. Permite a los usuarios gestionar tareas en diferentes tableros, asignar tareas a usuarios y cambiar el estado de las tareas. El proyecto incluye características como soporte para tema oscuro, niveles de acceso y autorizaciones, validaciones de datos, implementación de excepciones y uso de ViewModels para la gestión de datos.



---



### Características

- Crear, modificar y eliminar tareas.
- Asignar tareas a usuarios.
- Cambiar el estado de las tareas.
- Visualizar tareas en un tablero Kanban.
- Soporte para tema oscuro.
- Niveles de acceso y autorizaciones.
- Validaciones de datos.
- Implementación de excepciones.
- Uso de ViewModels para la gestión de datos.

### Requisitos

- .NET 8.0
- SQLite

### Instalación

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

### Uso

#### Usuarios de Prueba

- **Administrador**
  - Usuario: `tangerine`
  - Contraseña: `12345`

#### Funcionalidades

- **Crear Tarea**: Permite crear una nueva tarea en un tablero.
- **Modificar Tarea**: Permite modificar los detalles de una tarea existente.
- **Eliminar Tarea**: Permite eliminar una tarea existente.
- **Asignar Tarea**: Permite asignar una tarea a un usuario.
- **Cambiar Estado de Tarea**: Permite cambiar el estado de una tarea (Ideas, ToDo, Doing, Review, Done).

### Niveles de Acceso y Autorizaciones

El proyecto implementa diferentes niveles de acceso y autorizaciones para asegurar que solo los usuarios autorizados puedan realizar ciertas acciones. Los roles de usuario incluyen:

- **Administrador**: Tiene acceso completo a todas las funcionalidades del sistema.
- **Usuario**: Puede ver y modificar las tareas asignadas a él.

### Validaciones

El proyecto incluye validaciones de datos tanto en el lado del cliente como en el lado del servidor para asegurar la integridad de los datos. Las validaciones se implementan utilizando atributos de anotación de datos y otras técnicas.

### Implementación de Excepciones

El proyecto maneja excepciones de manera centralizada para asegurar que los errores se gestionen de manera adecuada y se proporcionen mensajes de error útiles a los usuarios. Las excepciones se registran y manejan según sea necesario.

### ViewModels

El proyecto utiliza ViewModels para gestionar los datos que se pasan entre las vistas y los controladores. Los ViewModels permiten estructurar y validar los datos de manera eficiente.

### Estructura del Proyecto

- **Controllers**: Contiene los controladores de la aplicación.
- **Models**: Contiene las clases de modelo de datos.
- **Views**: Contiene las vistas de la aplicación.
- **ViewModels**: Contiene las clases de modelo de vista.
- **Repositorios**: Contiene las clases de repositorio para acceder a la base de datos.
- **wwwroot**: Contiene los archivos estáticos como CSS, JavaScript e imágenes.

### Librerías Externas

El proyecto utiliza varias librerías externas para mejorar la funcionalidad y la apariencia de la aplicación. Algunas de las librerías utilizadas incluyen:

- **Bootstrap**: Para el diseño y la estructura de la interfaz de usuario.
  - [Documentación de Bootstrap](https://getbootstrap.com/docs/5.1/getting-started/introduction/)
- **Font Awesome**: Para los iconos utilizados en la aplicación.
  - [Documentación de Font Awesome](https://fontawesome.com/v5.15/how-to-use/on-the-web/setup/getting-started)

### Contribuir

Si deseas contribuir a este proyecto, por favor sigue los siguientes pasos:

1. Haz un fork del repositorio.
2. Crea una nueva rama (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza tus cambios y haz commit (`git commit -am 'Agrega nueva funcionalidad'`).
4. Sube tus cambios a tu fork (`git push origin feature/nueva-funcionalidad`).
5. Abre un Pull Request en el repositorio original.

### Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo LICENSE para más detalles.

### Contacto

Si tienes alguna pregunta o sugerencia, por favor abre un issue en el repositorio o contacta al administrador del proyecto.

---

## English Version

This is a Kanban project developed in ASP.NET Core. It allows users to manage tasks on different boards, assign tasks to users, and change the status of tasks. The project includes features such as dark theme support, access levels and authorizations, data validations, exception handling, and the use of ViewModels for data management.

### Features

- Create, modify, and delete tasks.
- Assign tasks to users.
- Change the status of tasks.
- View tasks on a Kanban board.
- Dark theme support.
- Access levels and authorizations.
- Data validations.
- Exception handling.
- Use of ViewModels for data management.

### Requirements

- .NET 8.0
- SQLite

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/TallerDeLenguajes2/tl2-proyecto-2024-tangerinegmv.git
    ```

2. Navigate to the project directory:
    ```bash
    cd tl2-proyecto-2024-tangerinegmv
    ```

3. Restore dependencies:
    ```bash
    dotnet restore
    ```

4. Apply database migrations:
    ```bash
    dotnet ef database update
    ```

5. Run the application:
    ```bash
    dotnet run
    ```

### Usage

#### Test Users

- **Administrator**
  - Username: `tangerine`
  - Password: `12345`

#### Functionalities

- **Create Task**: Allows creating a new task on a board.
- **Modify Task**: Allows modifying the details of an existing task.
- **Delete Task**: Allows deleting an existing task.
- **Assign Task**: Allows assigning a task to a user.
- **Change Task Status**: Allows changing the status of a task (Ideas, ToDo, Doing, Review, Done).

### Access Levels and Authorizations

The project implements different access levels and authorizations to ensure that only authorized users can perform certain actions. User roles include:

- **Administrator**: Has full access to all system functionalities.
- **User**: Can view and modify tasks assigned to them.

### Validations

The project includes data validations on both the client and server sides to ensure data integrity. Validations are implemented using data annotation attributes and other techniques.

### Exception Handling

The project handles exceptions centrally to ensure that errors are managed appropriately and useful error messages are provided to users. Exceptions are logged and handled as necessary.

### ViewModels

The project uses ViewModels to manage the data passed between views and controllers. ViewModels allow for efficient structuring and validation of data.

### Project Structure

- **Controllers**: Contains the application's controllers.
- **Models**: Contains data model classes.
- **Views**: Contains the application's views.
- **ViewModels**: Contains view model classes.
- **Repositories**: Contains repository classes for database access.
- **wwwroot**: Contains static files such as CSS, JavaScript, and images.

### External Libraries

The project uses several external libraries to improve the functionality and appearance of the application. Some of the libraries used include:

- **Bootstrap**: For UI design and structure.
  - [Bootstrap Documentation](https://getbootstrap.com/docs/5.1/getting-started/introduction/)
- **Font Awesome**: For the icons used in the application.
  - [Font Awesome Documentation](https://fontawesome.com/v5.15/how-to-use/on-the-web/setup/getting-started)

### Contributing

If you wish to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/new-feature`).
3. Make your changes and commit (`git commit -am 'Add new feature'`).
4. Push your changes to your fork (`git push origin feature/new-feature`).
5. Open a Pull Request in the original repository.

### License

This project is licensed under the MIT License. See the LICENSE file for more details.

### Contact

If you have any questions or suggestions, please open an issue in the repository or contact the project administrator.

---

¡Gracias por usar nuestro proyecto de Kanban!
Thank you for using our Kanban project!
