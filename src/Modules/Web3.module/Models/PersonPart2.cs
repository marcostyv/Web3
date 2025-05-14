using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Web3.Module.Models
{
    public class PersonPart2 : ContentPart // Creamos un content part llamado PersonPart
    {
        // Estos son sus content fields!
        public string Name { get; set; } // Nombre
        public Handedness2 Handedness { get; set; } // Mano dominante
        public DateTime? BirthDateUtc { get; set; }
        public TextField Biography { get; set; } // Instalamos el content field! (Desde paquetes nugget para este)
    
    }

    public enum Handedness2
    {
        Left,
        Right
    }
}
