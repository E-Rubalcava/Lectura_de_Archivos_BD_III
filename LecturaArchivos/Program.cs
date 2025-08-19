using Cocona;
using LecturaArchivos.Parsers;

CoconaApp.Run((
    [Argument(Description = "Ruta del archivo a procesar")] string archivo
) =>
{
    var content = File.ReadAllText(archivo);
    var formato = Path.GetExtension(archivo).TrimStart('.');
    Console.WriteLine($"Se ha detectado el formato: {formato.ToUpper()}");

    // Procesar contenido según formato
    switch (formato.ToLower())
    {
        case "txt":
            Console.WriteLine("Procesando archivo TXT...");
            Migrador.Migrar(Parser.ParseFromTxt(content));
            break;
        case "json":
            Console.WriteLine("Procesando archivo JSON...");
            Migrador.Migrar(Parser.ParseFromJson(content));
            break;

        case "xml":
            Console.WriteLine("Procesando archivo XML...");
            Migrador.Migrar(Parser.ParseFromXml(content));
            break;
        default:
            Console.WriteLine($"Formato no soportado: {formato}");
            break;
    }
});