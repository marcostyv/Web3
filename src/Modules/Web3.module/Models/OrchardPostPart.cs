using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Web3.module.Models
{
    public class OrchardPostPart : ContentPart // Creamos un content part llamado PersonPart
    {
        // Estos son sus content fields!
        public string TitlePost { get; set; } // Nombre

    }

    /*public enum Handedness
    {
        Left,
        Right,
    }*/

}
