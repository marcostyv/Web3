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
using Web3.module.Models;
//using Web3.Module.Indexes;
//using Web3.Module.Models;
using YesSql.Sql;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace Web3.module.Migrations
{
    public class OrchardPostMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;


        public OrchardPostMigrations(IContentDefinitionManager contentDefinitionManager) =>
            _contentDefinitionManager = contentDefinitionManager;


        public async Task<int> CreateAsync() // Esto se ejecuta automaticamente. HACEMOS EL CONTENT PART ATTACHABLE (el de PersonPart)
        {

            await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(OrchardPostPart), part => part
                .Attachable()
                /*.WithField(nameof(PersonPart2.Biography), field => field
                    .OfType(nameof(TextField)) // Definimos que tipo de field es!
                    .WithDisplayName("Biography") // Nombre "Biography
                    .WithSettings(new TextFieldSettings // Descripción
                    {
                        Hint = "The person's biography."
                    })
                    
                    .WithEditor("TextArea"))*/ // Para que el campo de entrada sea un textArea que es mas grande para una biografia   

                );
                // Definimos como se vera en el admin (nombre, propiedades,...)
            await _contentDefinitionManager.AlterTypeDefinitionAsync("OrchardPost", type => type
                .Creatable() // Que se puedan crear instancias
                .Listable() // que se puedan listar
                .WithPart(nameof(OrchardPostPart))
            );

            return 1;

        }

        // Because I changed some names accidentally
        public int UpdateFrom1() 
        {
            _contentDefinitionManager.AlterTypeDefinitionAsync("OrchardPost", type => type
                .Creatable() // Que se puedan crear instancias
                .Listable() // que se puedan listar
                .WithPart(nameof(OrchardPostPart))
            );

            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(OrchardPostPart), part => part
                .Attachable()
                );

            return 2;
        }
        public int UpdateFrom2()
        {

            _contentDefinitionManager.AlterTypeDefinitionAsync("OrchardPost", type => type
                .WithPart("HtmlBodyPart", part => part
                    .WithPosition("1")
                )
            );

            return 3;
        }

        public int UpdateFrom3()
        {

            _contentDefinitionManager.AlterTypeDefinitionAsync("OrchardPost", type => type
                .WithPart("HtmlBodyPart", part => part
                    .WithPosition("1")
                    .WithEditor("Wysiwyg") // Type
                )
            );

            return 4;
        }

        public int UpdateFrom4()
        {

            _contentDefinitionManager.AlterPartDefinitionAsync(nameof(OrchardPostPart), part => part
                    .WithField("VideoUrl", field => field
                        .OfType("TextField")
                        .WithDisplayName("Video URL")
                        .WithSettings(new OrchardCore.ContentFields.Settings.TextFieldSettings
                        {
                            Hint = "Video URL"
                        })
                    )
                );

            return 5;
        }

        public int UpdateFrom5() // Con esto lo que hacemos es acutalizar este content type al volver a lanzar el proyecto
                                 // Lo mejor seria que todo estuviera en el create() pero si alguna vez se nos olvida algo simplemente ponemos esto y así
                                 // ignorará el create y solo añadira esta parte. HAY QUE PONER RETURN + OTRO_NUMERO
        {
            _contentDefinitionManager.AlterTypeDefinitionAsync("OrchardPostWidget", type => type
                .WithPart(nameof(OrchardPostPart))
                .Stereotype("Widget")
            );

            return 6;
        }

    }
}
