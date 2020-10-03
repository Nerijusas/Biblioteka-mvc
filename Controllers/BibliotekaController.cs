using System.Collections.Generic;
using System.Linq;
using Biblioteka_mvc.Models.Biblioteka;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteka_mvc.Controllers
{
    public class BibliotekaController : Controller
    {


        public IActionResult Lankytojai(){

            IList<Lankytojas> keys = BibliotekaManager.Biblioteka.Lankytojai;
            return View(Enumerable.Range(0,keys.Count).ToDictionary(i => keys[i],i => BibliotekaManager.Biblioteka.RastiVisusNegrazintusIsdavimus(keys[i]).Count));
        }

         public IActionResult Isdavimai(){
             return View(BibliotekaManager.Biblioteka.Isdavimai);
         }

          public IActionResult Knygos(){
             return View(BibliotekaManager.Biblioteka.GautiVisasKnygasSuLikuciu());
         }
        [HttpPost]
         public IActionResult PridetiLankytoja(string vardas, string pavarde){
             BibliotekaManager.Biblioteka.Lankytojai.Add(new Lankytojas(vardas,pavarde,BibliotekaManager.Biblioteka.GeneruotiID()));
             System.Console.WriteLine($"{vardas} {pavarde}");
             TempData["success"] = "Sekmingai pridetas lankytojas!";
             return RedirectToAction("Lankytojai");
         }

         public IActionResult PasalintiLankytoja(int id){
             string response =BibliotekaManager.Biblioteka.PasalintiLankytojaByID(id);
             if(response ==null) TempData["success"] = "Sekmingai pasalintas lankytojas!";
             else TempData["error"] = response;
             return RedirectToAction("Lankytojai");
             
         }

         public IActionResult GrazintiKnyga(int id, string isbn){
             BibliotekaManager.Biblioteka.GrazintiKnyga(id,isbn);
             TempData["success"] = "Sekmingai pazymetas, kad grazino knyga!";
             return RedirectToAction("Isdavimai");
         }

         public IActionResult PridetiIsdavima(int id, long isbn){
            string response = BibliotekaManager.Biblioteka.PasiimtiKnyga(isbn.ToString(),id);
            if(response == null) TempData["success"] = "Sekmingai knyga isduota!";
            else TempData["error"] = response;
            return RedirectToAction("Isdavimai");
         }

        public IActionResult PridetiKnyga(string pavadinimas, long isbn){
            string response;
            if(pavadinimas == null) response =BibliotekaManager.Biblioteka.IdetiKnyga(isbn.ToString());
            else response = BibliotekaManager.Biblioteka.IdetiKnyga(new Knyga(pavadinimas,isbn.ToString()));

            if(response == null) TempData["success"] = "Sekmingai knyga buvo prideta";
            else TempData["error"] = response;
            return RedirectToAction("Knygos");
        }

        public IActionResult IsimtiKnyga(string isbn){
            string response = BibliotekaManager.Biblioteka.IsimtiKnyga(isbn);
            if(response == null) TempData["success"] = "Sekmingai knyga buvo isimta!";
            else TempData["error"] = response;
            return RedirectToAction("Knygos");
        }
    }
}