// using DojoCourse.Module.Indexes;
using Web3.Module.Models;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web3.Module.Migrations
{
    public class PersonMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;


        public PersonMigrations(IContentDefinitionManager contentDefinitionManager) =>
            _contentDefinitionManager = contentDefinitionManager;


        public int Create() // Esto se ejecuta automaticamente. HACEMOS EL CONTENT PART ATTACHABLE (el de PersonPart)
        {
            // Esto es para definir el content part
            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(PersonPart), part => part // Definimos el tipo de Part
                .Attachable()
                .WithField(nameof(PersonPart.Hobby), field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Hobby")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "Write your hobby"
                    })
                    .WithEditor("TextArea")
                )

                .WithField("Name", field => field
                    .OfType("TextField")
                    .WithDisplayName("Name")
                    .WithSettings(new TextFieldSettings { Hint = "Enter your name" })
                )


                .WithField(nameof(PersonPart.Biography), field => field
                    .OfType(nameof(TextField)) // Definimos que tipo de field es!
                    .WithDisplayName("Biography") // Nombre "Biography
                    .WithSettings(new TextFieldSettings // Descripción
                    {
                        Hint = "The person's biography."
                    })

                    .WithEditor("TextArea")) // Para editar el valor un TextArea. Se pone .withEditor para añadir estos campos
            );

            // Definicion de las Type definitions (por lo visto se pone así)
            _contentDefinitionManager.AlterTypeDefinitionAsync("PersonPageV2", type => type
                .Creatable() // Que se puedan crear instancias
                .Listable() // que se puedan listar
                .WithPart(nameof(PersonPart))
            );

            return 3;
        }
            /*
            _contentDefinitionManager.AlterTypeDefinition("PersonWidget", type => type
                .WithPart(nameof(PersonPart))
                .Stereotype("Widget")
            );

            SchemaBuilder.CreateMapIndexTable(nameof(PersonPartIndex), table => table
                .Column<string>(nameof(PersonPartIndex.ContentItemId), column => column.WithLength(26))
                .Column<DateTime>(nameof(PersonPartIndex.BirthDateUtc))
            );

            SchemaBuilder.AlterTable(nameof(PersonPartIndex), table => table
                .CreateIndex(
                    $"IDX_{nameof(PersonPartIndex)}_{nameof(PersonPartIndex.BirthDateUtc)}",
                    nameof(PersonPartIndex.BirthDateUtc))
            );

            return 3;
        }

        public int UpdateFrom1()
        {
            _contentDefinitionManager.AlterTypeDefinition("PersonWidget", type => type
                .WithPart(nameof(PersonPart))
                .Stereotype("Widget")
            );

            return 2;
        }

        public int UpdateFrom2()
        {
            SchemaBuilder.CreateMapIndexTable(nameof(PersonPartIndex), table => table
                .Column<string>(nameof(PersonPartIndex.ContentItemId), column => column.WithLength(26))
                .Column<DateTime>(nameof(PersonPartIndex.BirthDateUtc))
            );

            SchemaBuilder.AlterTable(nameof(PersonPartIndex), table => table
                .CreateIndex(
                    $"IDX_{nameof(PersonPartIndex)}_{nameof(PersonPartIndex.BirthDateUtc)}",
                    nameof(PersonPartIndex.BirthDateUtc))
            );

            return 3;
        }*/
    }
}
