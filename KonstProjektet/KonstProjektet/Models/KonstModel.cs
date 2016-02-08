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
        public int ArtworkID { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
    }
}