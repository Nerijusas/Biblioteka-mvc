using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteka_mvc.Models.Biblioteka
{
    public class Biblioteka
    {


        private Dictionary<Knyga,int> Knygos { get; set; }
        public List<Lankytojas> Lankytojai { get; set; }
        public List<Isdavimas> Isdavimai { get; set; }

        private int IDS; 
        public Biblioteka(){
            Knygos = new Dictionary<Knyga,int>();
            Lankytojai = new List<Lankytojas>();
            Isdavimai = new List<Isdavimas>();
            IDS = 0;
        }

        public string PasiimtiKnyga(Knyga knyga, Lankytojas lankytojas){
            if(RastiVisusIsdavimus(lankytojas).Count>=lankytojas.IsdavimuSkc) return "Per daug knygu pasiemes zmogus!!";
            if(RastiKnygosLikuti(knyga)==0) return "Neliko knygu!";
            Isdavimai.Add(new Isdavimas(lankytojas,knyga));
            IsimtiKnyga(knyga);
            return null;
        }

        public string PasiimtiKnyga(Knyga knyga, int ID){
            Lankytojas lankytojas = RastiLankytojaByID(ID);
            if(lankytojas == null) return "Lankytojas nerastas!";
            return PasiimtiKnyga(knyga,lankytojas);
        }

        public string PasiimtiKnyga(string ISBN, int ID){
            Knyga knyga = RastiKnygaByISBN(ISBN);
            if(knyga == null) return "Knyga nerasta!";
            return PasiimtiKnyga(knyga,ID);
        }

        public List<Isdavimas> RastiVisusIsdavimus(Lankytojas lankytojas){
            List<Isdavimas> isdavimai = new List<Isdavimas>();
             foreach (var item in Isdavimai)
            {
                if(item.Lankytojas.Equals(lankytojas)) isdavimai.Add(item);
            }
             return isdavimai;
        }

        public List<Isdavimas> RastiVisusNegrazintusIsdavimus(Lankytojas lankytojas){
            List<Isdavimas> isdavimai = new List<Isdavimas>();
             foreach (var item in Isdavimai)
            {
                if(item.Lankytojas.Equals(lankytojas) && !item.Grazinta) isdavimai.Add(item);
            }
             return isdavimai;
        }

        public List<Knyga> GautiVisasKnygas(){
            return new List<Knyga>(Knygos.Keys);
        }

        public void GrazintiKnyga(int ID, string ISBN){
            Isdavimas isdavimas = Isdavimai.Find(x => x.Lankytojas.ID == ID && x.IsduotaKnyga.ISBN == ISBN && !x.Grazinta);
            isdavimas.Grazinta=true;
            isdavimas.GrazinimoData=DateTime.Now;
            Knyga RKnyga = RastiKnygaByISBN(ISBN);
            Knygos[RKnyga] ++;
        }

        public Dictionary<Knyga,int> GautiVisasKnygasSuLikuciu(){
            return Knygos;
        }

        public int RastiKnygosLikuti(Knyga knyga){
            int likutis;
            Knygos.TryGetValue(knyga, out likutis);
            return likutis;
        }

        public int GeneruotiID(){
            IDS++;
            return IDS; 
        }

        public string IdetiKnyga(Knyga knyga){
            Knyga RKnyga = RastiKnygaByISBN(knyga.ISBN);
            if(RKnyga==null) Knygos.Add(knyga,1);
            else{ 
                if(!knyga.Pavadinimas.Equals(RKnyga.Pavadinimas)) return "Nesutampa knygos pavadinimas su buvusia knyga";
                Knygos[RKnyga]++;
            }
            return null;
        }

        public string IdetiKnyga(string isbn){
            Knyga knyga = RastiKnygaByISBN(isbn);
            if(knyga==null) return "Nerasta knyga. Norint prideti nauja knyga butina irasyti pavadinima";
            else{ 
                Knygos[knyga]++;
            }
            return null;
        }

        public string IsimtiKnyga(Knyga knyga){
            if(knyga == null) return "Knyga nerasta!";
            if(Knygos[knyga]==0)
                return "Knygu jau neliko!";
            else
                Knygos[knyga]--;
        return null;
        }

        public string IsimtiKnyga(string ISBN){
            return IsimtiKnyga(RastiKnygaByISBN(ISBN));
        }

        public int RastiKnygosKieki(string ISBN){
            Knyga RKnyga = RastiKnygaByISBN(ISBN);
            int kiekis = Knygos[RKnyga];
            kiekis+=Isdavimai.Where(x => x.IsduotaKnyga.ISBN == ISBN && !x.Grazinta).Count();
            return kiekis;
        }

        public Knyga RastiKnygaByISBN(String ISBN){
            foreach (var item in GautiVisasKnygas())
            {
                if(item.ISBN.Equals(ISBN)) return item;
            }
            return null;
        }

        public Lankytojas RastiLankytojaByID(int ID){
            return Lankytojai.FirstOrDefault(x => x.ID == ID);
        }

        public string PasalintiLankytojaByID(int ID){
            IList<Isdavimas> find = RastiVisusNegrazintusIsdavimus(RastiLankytojaByID(ID));
                if(find.Count==0){
                Lankytojai = Lankytojai.Where(x => x.ID != ID).ToList();
                return null;
                }
                return "Negalima pasalinti, nes lankytojas dar neatidaves knygos!";
        }

        // public void IsspausdintiIsdavimus(){
        //     string border = new string('-',110);
        //     System.Console.WriteLine(border);
        //     System.Console.WriteLine($"| {"Knygos pavadinimas",-26} | {"Knygos ISBN",-15} | {"Kas pasieme",-25} | {"Pasiimimo data",-14} | {"Grazinimo data",-14} |");
        //     System.Console.WriteLine(border);
        //     foreach (var item in Isdavimai)
        //     {
        //         System.Console.WriteLine($"| {item.IsduotaKnyga.Pavadinimas,-26} | {item.IsduotaKnyga.ISBN,-15} | {item.Lankytojas.Vardas_Pavarde,-25} | {item.PasiimimoData,14:yyyy-MM-dd} | {item.GrazinimoData,14:yyyy-MM-dd} |");
        //     }
        //     System.Console.WriteLine(border);
        // }

        // public void IsspausdintiIsdavimus(int id)
        // {
        //     string border = new string('-', 110);
        //     System.Console.WriteLine(border);
        //     System.Console.WriteLine($"| {"Knygos pavadinimas",-26} | {"Knygos ISBN",-15} | {"Kas pasieme",-25} | {"Pasiimimo data",-14} | {"Grazinimo data",-14} |");
        //     System.Console.WriteLine(border);
        //     List<Isdavimas> isdavimai = RastiVisusIsdavimus(RastiLankytojaByID(id));
        //     foreach (var item in isdavimai)
        //     {
        //         System.Console.WriteLine($"| {item.IsduotaKnyga.Pavadinimas,-26} | {item.IsduotaKnyga.ISBN,-15} | {item.Lankytojas.Vardas_Pavarde,-25} | {item.PasiimimoData,14:yyyy-MM-dd} | {item.GrazinimoData,14:yyyy-MM-dd} |");
        //     }
        //     System.Console.WriteLine(border);
        // }

        // public void IsspausdintiLankytojus()
        // {   

        //     string border = new string('-', 65);
        //     System.Console.WriteLine(border);
        //     System.Console.WriteLine($"| {"Lankytojo vardas pavarde",-27} | {"Lankytojo ID",-15} | {"Knygu isduota",-10} |");
        //     System.Console.WriteLine(border);
        //     foreach (var item in Lankytojai)
        //     {
        //         System.Console.WriteLine($"| {item.Vardas_Pavarde,-27} | {item.ID,15:D4} | {RastiVisusIsdavimus(item).Count,13} |");
        //     }
        //     System.Console.WriteLine(border);
        // }

        // public void IsspausdintiKnygas()
        // {

        //     string border = new string('-', 65);
        //     System.Console.WriteLine(border);
        //     System.Console.WriteLine($"| {"Knygos pavadinimas",-27} | {"knygos ISBN",-15} | {"Knygu likutis",-10} |");
        //     System.Console.WriteLine(border);
        //     foreach (KeyValuePair<Knyga, int> item in Knygos)
        //     {
          
        //     System.Console.WriteLine($"| {item.Key.Pavadinimas,-27} | {item.Key.ISBN,15} | {item.Value,13} |");
        //     }
        //     System.Console.WriteLine(border);
        // }

    }
}