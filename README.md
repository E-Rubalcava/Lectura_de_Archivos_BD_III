# Leer archivos

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
dotnet ef database update
```

