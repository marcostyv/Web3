using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Web3.Module.Models
{
    public class PersonPart : ContentPart // Creamos un content part llamado PersonPart
    {
        // Estos son sus content fields!
        public TextField Name { get; set; } // Nombre
        public Handedness Handedness { get; set; } // Mano dominante
        public DateTime? BirthDateUtc { get; set; }
        public TextField Biography { get; set; } // Instalamos el content field! (Desde paquetes nugget para este)

        public TextField Hobby { get; set; }
    }

    public enum Handedness
    {
        Left,
        Right,
    }
}
