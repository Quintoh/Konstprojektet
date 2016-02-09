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
        static List<KonstModel> Konstverk = new List<KonstModel>();


       
        public ActionResult KonstView()
        {

            //Konstverk.Add(new KonstModel() { ArtworkID = 1, Artist = "Brutus Östling", Title = "Tre pingviner" });
            //Konstverk.Add(new KonstModel() { ArtworkID = 2, Artist = "Brutus Östling", Title = "En örn som dyker" });
            //Konstverk.Add(new KonstModel() { ArtworkID = 3, Artist = "Picasso", Title = "Mona Lisa" });
            //Konstverk.Add(new KonstModel() { ArtworkID = 4, Artist = "Sefik Mehicic", Title = "Selfie" });
            //Konstverk.Add(new KonstModel() { ArtworkID = 5, Artist = "Brutus Östling", Title = "En sovande räv" });

            return View(Konstverk);    
                
            
        }


        //Lägga till

        public ActionResult AddKonst()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult AddKonst(KonstModel k)
        {
            if (!ModelState.IsValid)
            {
                return View("AddKonst", k);
            }
            
            Konstverk.Add(k);
           
            return RedirectToAction("KonstView");
        }







        //Ta bort

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(KonstModel k)
        {
            return RedirectToAction("KonstView");
        }
    }
}