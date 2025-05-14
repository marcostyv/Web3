using Web3.Module.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web3.Module.ViewModels
{
    public class PersonPart2ViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? BirthDateUtc { get; set; }
        
        [Required]
        public Handedness2 Handedness { get; set; }
    }
}
