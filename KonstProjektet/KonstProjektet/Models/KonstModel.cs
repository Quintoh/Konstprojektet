using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KonstProjektet.Models
{
    public class KonstModel
    {
        [Required(ErrorMessage = "Konstverket måste ha ett id.")]
        public int ArtworkID { get; set; }

        [Required(ErrorMessage = "Namn på Artist saknas.")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Titel saknas.")]
        public string Title { get; set; }
    }
}