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
        inventory MyInventory = new inventory();

       
        public ActionResult KonstView()
        {
            return View(MyInventory.GetList);
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
            
            MyInventory.GetList.Add(k);
           
            return RedirectToAction("KonstView");
        }


        //Ta bort

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            KonstModel k = new KonstModel();

            foreach (var item in MyInventory.GetList)
            {
                if (item.ArtworkID == id)
                {
                    k.Artist = item.Artist;
                    k.ArtworkID = item.ArtworkID;
                    k.Title = item.Title;
                }
            }
            return View(k);
        }

        [HttpPost]        
        public ActionResult Delete(KonstModel k)
        {     
        
            foreach (var pn in MyInventory.GetList)
            {
                if ((pn.ArtworkID == k.ArtworkID))
                {
                    MyInventory.GetList.Remove(pn);
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            return RedirectToAction("Index");

            //MyInventory.GetList.RemoveAt(k.ArtworkID);

            //return RedirectToAction("KonstView");
        }

        
   

    //Redigera
    [HttpGet]
        public ActionResult Edit(int? id)
        {
            KonstModel k = new KonstModel();

            foreach (var item in MyInventory.GetList)
            {
                if (item.ArtworkID == id)
                {
                    k.Artist = item.Artist;
                    k.ArtworkID = item.ArtworkID;
                    k.Title = item.Title;
                }
            }
            return View(k);
        }

        [HttpPost]
        public ActionResult Edit(KonstModel k)
        {
            //MyInventory.GetList.Add(k);
            //MyInventory.GetList.RemoveAt(k.ArtworkID);

            return RedirectToAction("KonstView");
        }
    }
}
    
