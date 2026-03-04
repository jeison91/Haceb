# Haceb

El proyecto fue realizado en la versión .Net 8

Para iniciar el proyecto
1. En el proyecto Haceb.Demand.Api el archivo appsettings.json está la conexión a la Base de datos.
2. Usar el comando "Update-Database" en la Consola de Administrador de paquetes para que se ejecute la migration (SQL server).
3. En la DbContext (HacebDbContext) se cargan algunos datos iniciales en las entidades (Type, User, Rating).
4. El proyecto Haceb.Demand.Api y Haceb.Demanda.Web son los proyectos de inicio.
5. Se uso swagger para realizar las pruebas.
6. El diseño del frontend se realizo en ASP.net