using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KonstProjektet.Models;

namespace KonstProjektet.Controllers
{
    public class KonstController : Controller
    {
        // GET: Konst
        public ActionResult KonstView()
        {
            List<KonstModel> Konstverk = new List<KonstModel>();

            Konstverk.Add(new KonstModel() { ArtworkID = 1, Artist = "Brutus Östling", Title = "Tre pingviner" });
            Konstverk.Add(new KonstModel() { ArtworkID = 2, Artist = "Brutus Östling", Title = "En örn som dyker" });
            Konstverk.Add(new KonstModel() { ArtworkID = 3, Artist = "Picasso", Title = "Mona Lisa" });
            Konstverk.Add(new KonstModel() { ArtworkID = 4, Artist = "Sefik Mehicic", Title = "Selfie" });
            Konstverk.Add(new KonstModel() { ArtworkID = 5, Artist = "Brutus Östling", Title = "En sovande räv" });

            return View(Konstverk);
        }
    }
}