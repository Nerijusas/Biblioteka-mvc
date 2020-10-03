using System;

namespace Biblioteka_mvc.Models.Biblioteka
{
    public class Isdavimas
    {
        public Lankytojas Lankytojas { get; set; }

        public Knyga IsduotaKnyga { get; set; }
        public bool Grazinta { get; set; }
        public DateTime IsdavimoData { get; set; }
        public DateTime GrazinimoData { get; set; }

        


        public Isdavimas(Lankytojas lankytojas, Knyga isduotaKnyga, int GrazinimoTerminas = 14)
        {
            Lankytojas = lankytojas;
            IsduotaKnyga = isduotaKnyga;
            Grazinta = false;
            IsdavimoData = DateTime.Now;
            GrazinimoData = DateTime.Now.AddDays(GrazinimoTerminas);
        }

    }
}