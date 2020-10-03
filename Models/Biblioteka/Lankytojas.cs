using System;

namespace Biblioteka_mvc.Models.Biblioteka
{
    public class Lankytojas
    {

        public Lankytojas(string vardas, string pavarde, int iD, int isdavimuSkc= 3)
        {
            this.Vardas = vardas;
            this.Pavarde = pavarde;
            this.ID = iD;
            this.IsdavimuSkc = isdavimuSkc;

        }

         public Lankytojas(string vardas_pavarde, int iD, int isdavimuSkc= 3)
        {
            this.Vardas_Pavarde = vardas_pavarde;
            this.ID = iD;
            this.IsdavimuSkc = isdavimuSkc;


        }
        public int IsdavimuSkc { get; set; }
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        private string vardas_pavarde;
        public string Vardas_Pavarde
        {
            get { return Vardas + " " + Pavarde; }
            set { if (value.Split().Length < 2) throw new Exception("TURI BUTI VARDAS IR PAVARDE!!"); Vardas = value.Split()[0]; Pavarde = value.Split()[1]; }
        }

        public int ID { get; set; }
    }
}