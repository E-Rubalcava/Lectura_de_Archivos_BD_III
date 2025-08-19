# Leer archivos

El proyecto requiere .NET 9 para funcionar.

Se usó EntityFramework Core para la base de datos.
```bash
# Hay que instalar la CLI para poder migrar 
dotnet tool install --global dotnet-ef

# Verificamos que funcione
dotnet ef --help

# En el proyecto hay una carpeta llamada Migrations. Son codigo generado de las clases y que sirve para generara la base de datos.
# En caso de hacer un cambio, migrarlo

# Generamos la migracion, que genera un nuevo archivo en Migrations
dotnet ef migrations add Descripcion_Cambio

# Aplicamos los cambios en la base de datos
# Tambien la crea si es que no existe, aplicando todas las migraciones preparadas
# Si se usa una nueva instancia de BD, usar este comando 
dotnet ef database update
```

Para la base de datos se usa un contenedor, para facilitar pruebas durante el desarrollo
```bash
# Para ejecutar el contenedor
podman-compose up -d
```
