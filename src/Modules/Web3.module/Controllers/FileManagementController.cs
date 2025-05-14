using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.Media;

namespace Web3.module.Controllers
{
    public class FileManagementController : Controller
    {
        private readonly IMediaFileStore _mediaFileStore; // Dependencia de Dojo para media files


        public FileManagementController(IMediaFileStore mediaFileStore) // Clase para media files de la dependencia
        {
            _mediaFileStore = mediaFileStore; // Variable de uso de media files de la dependencia 
        }


        public async Task<string> CreateFile() // Crea un fichero y lo guarda en la carpeta MEDIA del proyecto !!!!
        {
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello world!")); // Contiene esa frase y bytes
            await _mediaFileStore.CreateFileFromStreamAsync("Demo.txt", stream); // Lo crea

            return "OK";
        }

        public async Task<string> ReadFile() // Lee ficheros y muestra unos datos en pantalla
        {
            var fileInfo = await _mediaFileStore.GetFileInfoAsync("Demo.txt"); // Saca informacion del fichero (Length, LastModifiedUtc)

            if (fileInfo == null)
            {
                return "Not found :(";
            }

            using var stream = await _mediaFileStore.GetFileStreamAsync("Demo.txt"); // Sacamos flujo de datos
            using var streamReader = new StreamReader(stream); // Creamos objeto para leer este fichero
            var content = await streamReader.ReadToEndAsync(); // Lo usamos para sacar el contenido del fichero

            return $"File info: size: {fileInfo.Length}, last modification UTC: {fileInfo.LastModifiedUtc}. Content: {content}";
        }
    }
}
