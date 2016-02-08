using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KonstProjektet
{
    public class Konstverk
    {
        List<Artwork> inventory;

        public Konstverk()
        {
            inventory = new List<Artwork>();

            inventory.Add(new Artwork() { ArtworkID = 1, Artist = "Brutus Östling", Title = "Tre pingviner" });
            inventory.Add(new Artwork() { ArtworkID = 2, Artist = "Brutus Östling", Title = "En örn som dyker" });
            inventory.Add(new Artwork() { ArtworkID = 3, Artist = "Picasso", Title = "Mona Lisa" });
            inventory.Add(new Artwork() { ArtworkID = 4, Artist = "Sefik Mehicic", Title = "Selfie" });
            inventory.Add(new Artwork() { ArtworkID = 5, Artist = "Brutus Östling", Title = "En sovande räv" });
        }
    }

    public class Artwork
    {
        public int ArtworkID { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
    }
}