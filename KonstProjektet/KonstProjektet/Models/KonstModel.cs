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

    public class inventory
    {
        static List<KonstModel> Konstverk = new List<KonstModel>() {
            new KonstModel() { ArtworkID = 1, Artist = "Brutus Östling", Title = "Tre pingviner" },
            new KonstModel() { ArtworkID = 2, Artist = "Brutus Östling", Title = "En örn som dyker" },
            new KonstModel() { ArtworkID = 3, Artist = "Picasso", Title = "Mona Lisa" },
            new KonstModel() { ArtworkID = 4, Artist = "Sefik Mehicic", Title = "Selfie" },
            new KonstModel() { ArtworkID = 5, Artist = "Brutus Östling", Title = "En sovande räv" }
        };

        public IEnumerable<KonstModel> GetList
        {
            get { return Konstverk; }
        }

        public void remove(int id)
        {
            Konstverk.Remove(Konstverk.Where(x => x.ArtworkID == id).FirstOrDefault());
        }

        public void add(KonstModel k)
        {
            Konstverk.Add(k);
        }
    }
}