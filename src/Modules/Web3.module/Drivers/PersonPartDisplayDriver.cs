using Web3.Module.Models;
using Web3.Module.ViewModels;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;
using OrchardCore.ContentFields.Fields;

// LOS DRIVERS sirven para arquestar todo el display de un contenido, el codigo que se ejecuta al confirmar, etc. Hace casi todo
namespace Web3.Module.Drivers // HAY QUE AÑADIR EL DRIVER AL STARTUP para que este disponible !!
{ // Todas estas configuraciones vienen definidas en drivers de Orchard, para añadirlas al codigo.
    public class PersonPartDisplayDriver : ContentPartDisplayDriver<PersonPart>
    {   
        // Para como mostrar el PersonPart en el layout (front end)
        public override IDisplayResult Display(PersonPart part, BuildPartDisplayContext context) =>
            Initialize<PersonPartViewModel>(
                GetDisplayShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Detail", "Content:5")
            .Location("Summary", "Content:5");

        /* CÓDIGO DISTINTO !!!*/
        
        public override IDisplayResult Edit(PersonPart part, BuildPartEditorContext context)
        {
            return Initialize<PersonPartViewModel>(
                GetEditorShapeType(context),
                async viewModel =>
                {
                    if (context.Updater != null)
                    {
                        await context.Updater.TryUpdateModelAsync(viewModel, Prefix);

                        part.BirthDateUtc = viewModel.BirthDateUtc;
                        part.Handedness = viewModel.Handedness;
                        part.Name = viewModel.Name;
                        // part.Hobby = new TextField { Text = viewModel.Hobby?.Text };
                    }
                    
                    else
                    {
                        viewModel.BirthDateUtc = part.BirthDateUtc;
                        viewModel.Handedness = part.Handedness;
                        viewModel.Name = part.Name;
                        // viewModel.Hobby = part.Hobby;
                    }
                }).Location("Content:5");
        }
        


        /*
        // Cuando abrimos el editor del PersonPart
        public override IDisplayResult Edit(PersonPart part, BuildPartEditorContext context) =>
            Initialize<PersonPartViewModel>(
                GetEditorShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Content:5");

        // Esto para cuando le damos a submit o publicar. Handle the post back
        public override async Task<IDisplayResult> UpdateAsync(PersonPart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var viewModel = new PersonPartViewModel();

            await updater.TryUpdateModelAsync(viewModel, Prefix);

                // Guarda los datos introducidos en la base de datos del PersonPart
            part.BirthDateUtc = viewModel.BirthDateUtc;
            part.Handedness = viewModel.Handedness;
            part.Name = viewModel.Name;

            return await EditAsync(part, context); // Vuelve a mostrar el formulario tras haber guardado los datos
        }
        */

        // Esto es solo para visualizar los datos, para si queremos mostrarlos en algun sitio. Los datos YA ESTAN 
        // GUARDADOS EN LA BASE DE DATOS, por el metodo anterior.
        private static void PopulateViewModel(PersonPart part, PersonPartViewModel viewModel)
        {
            viewModel.BirthDateUtc = part.BirthDateUtc;
            viewModel.Handedness = part.Handedness;
            viewModel.Name = part.Name;
            // viewModel.Hobby = part.Hobby;
        }
    }
}
