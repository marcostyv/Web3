using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using Web3.Module.Indexes;
using Web3.Module.Models;
using YesSql.Sql;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace Web3.module.Migrations
{
    public class Person2Migrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;


        public Person2Migrations(IContentDefinitionManager contentDefinitionManager) =>
            _contentDefinitionManager = contentDefinitionManager;


        public async Task<int> CreateAsync() // Esto se ejecuta automaticamente. HACEMOS EL CONTENT PART ATTACHABLE (el de PersonPart)
        {

            await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(PersonPart2), part => part
                .Attachable()
                .WithField(nameof(PersonPart2.Biography), field => field
                    .OfType(nameof(TextField)) // Definimos que tipo de field es!
                    .WithDisplayName("Biography") // Nombre "Biography
                    .WithSettings(new TextFieldSettings // Descripción
                    {
                        Hint = "The person's biography."
                    })

                    .WithEditor("TextArea")) // Para que el campo de entrada sea un textArea que es mas grande para una biografia   
            
                );

            await _contentDefinitionManager.AlterTypeDefinitionAsync("PersonPage2", type => type
                .Creatable() // Que se puedan crear instancias
                .Listable() // que se puedan listar
                .WithPart(nameof(PersonPart2))
            );

            // Creamos la tabla de índices para PersonPart2
            await SchemaBuilder.CreateMapIndexTableAsync<PersonPart2Index>(table => table
                .Column<string>(nameof(PersonPart2Index.ContentItemId), column => column.WithLength(26))
                .Column<DateTime>(nameof(PersonPart2Index.BirthDateUtc))
            );

            // Creamos un índice sobre la fecha de nacimiento
            await SchemaBuilder.AlterTableAsync(nameof(PersonPart2Index), table => table
                .CreateIndex(
                    $"IDX_{nameof(PersonPart2Index)}_{nameof(PersonPart2Index.BirthDateUtc)}",
                    nameof(PersonPart2Index.BirthDateUtc))
            );

            return 1;

        }

        public int UpdateFrom1() // Con esto lo que hacemos es acutalizar este content type al volver a lanzar el proyecto
            // Lo mejor seria que todo estuviera en el create() pero si alguna vez se nos olvida algo simplemente ponemos esto y así
            // ignorará el create y solo añadira esta parte. HAY QUE PONER RETURN + OTRO_NUMERO
        {
            _contentDefinitionManager.AlterTypeDefinitionAsync("Person2Widget", type => type
                .WithPart(nameof(PersonPart2))
                .Stereotype("Widget")
            );  

            return 2;
        }



    }
}
