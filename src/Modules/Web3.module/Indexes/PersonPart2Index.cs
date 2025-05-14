using Web3.Module.Models;
using OrchardCore.ContentManagement;
using System;
using YesSql.Indexes;
/*
 Este script es para crear listas sobre las que haremos consultas. Esto es porque hacer consultas osbre la base de datos
de Orchard es demasiado costoso y hay muchos elementos, es mejor listar antes. 
 */
namespace Web3.Module.Indexes
{
    public class PersonPart2Index : MapIndex // Datos que guardamos para cada PersonPart2
    {
        public string ContentItemId { get; set; } // Nos interesa el ID para poder saber el elemento exacto
        public DateTime? BirthDateUtc { get; set; } // Y vamos a guardar el cumpleaños para filtrar despues por la edad
    }


    public class PersonPart2IndexProvider : IndexProvider<ContentItem> 
    {
        public override void Describe(DescribeContext<ContentItem> context) =>
            context.For<PersonPart2Index>().Map(contentItem =>
            {
                var personPart2 = contentItem.As<PersonPart2>();

                /*return personPart2 == null
                    ? null
                    : new PersonPart2Index
                    {
                        ContentItemId = contentItem.ContentItemId,
                        BirthDateUtc = personPart2.BirthDateUtc
                    };*/

                if (personPart2 == null || personPart2.BirthDateUtc == null)
                {
                    return null; // No creamos el índice si el contenido no tiene el part
                }

                return new PersonPart2Index
                {
                    ContentItemId = contentItem.ContentItemId,
                    BirthDateUtc = personPart2.BirthDateUtc
                };
            });
    }
}
