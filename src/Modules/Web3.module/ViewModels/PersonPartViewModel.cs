using Web3.Module.Models;
using System;
using System.ComponentModel.DataAnnotations;
using OrchardCore.ContentFields.Fields;

namespace Web3.Module.ViewModels
{
    public class PersonPartViewModel
    {
        [Required] // Propiedad requerida, el usuario tiene que tener un nombre
        public TextField Name { get; set; }

        [Required]
        public Handedness Handedness { get; set; } // Propiedad requerida, el usuario tiene que tener un Handedness

        [Required]
        public DateTime? BirthDateUtc { get; set; } // Propiedad requerida, el usuario tiene que tener un BirthDateUtc

        // public TextField Hobby { get; set; }
    }
}
