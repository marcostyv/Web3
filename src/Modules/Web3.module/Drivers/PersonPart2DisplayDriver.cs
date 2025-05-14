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
    public class PersonPart2DisplayDriver : ContentPartDisplayDriver<PersonPart2>
    {
        // Para como mostrar el PersonPart en el layout (front end)
        public override IDisplayResult Display(PersonPart2 part, BuildPartDisplayContext context) =>
            Initialize<PersonPart2ViewModel>(
                GetDisplayShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Detail", "Content:5")
            .Location("Summary", "Content:5");
        /*
        public override IDisplayResult Edit(PersonPart2 part, BuildPartEditorContext context)
        {
            // throw new Exception("El driver sí se está ejecutando");

            return Initialize<PersonPart2ViewModel>(
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
                        Console.WriteLine($"GUARDANDO: Name={part.Name}, Date={part.BirthDateUtc}, Handedness={part.Handedness}");


                    }

                    else
                    {
                        viewModel.BirthDateUtc = part.BirthDateUtc;
                        viewModel.Handedness = part.Handedness;
                        viewModel.Name = part.Name;
                        // viewModel.Hobby = part.Hobby;
                    }
                }).Prefix(nameof(PersonPart2)).Location("Content:5");
        }

        */
        // Cuando abrimos el editor del PersonPart
        public override IDisplayResult Edit(PersonPart2 part, BuildPartEditorContext context) =>
            Initialize<PersonPart2ViewModel>(
                GetEditorShapeType(context),
                viewModel => PopulateViewModel(part, viewModel))
            .Location("Content:5");

        // Esto para cuando le damos a submit o publicar. Handle the post back
        public override async Task<IDisplayResult> UpdateAsync(PersonPart2 part, UpdatePartEditorContext context)
        {

            var viewModel = new PersonPart2ViewModel();

            if (await context.Updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                // Solo se copian los datos si el binding fue correcto
                part.BirthDateUtc = viewModel.BirthDateUtc;
                part.Name = viewModel.Name;
                part.Handedness = viewModel.Handedness;
            }

            return await EditAsync(part, context); // Vuelve a mostrar el formulario tras haber guardado los datos
        }
        

        // Esto es solo para visualizar los datos, para si queremos mostrarlos en algun sitio. Los datos YA ESTAN 
        // GUARDADOS EN LA BASE DE DATOS, por el metodo anterior.
        private static void PopulateViewModel(PersonPart2 part, PersonPart2ViewModel viewModel)
        {
            viewModel.BirthDateUtc = part.BirthDateUtc;
            viewModel.Handedness = part.Handedness;
            viewModel.Name = part.Name; 
            // viewModel.Hobby = part.Hobby;
        }
    }
}

