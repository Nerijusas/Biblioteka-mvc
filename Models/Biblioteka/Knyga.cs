using System;

namespace Biblioteka_mvc.Models.Biblioteka
{
    public class Knyga
    {

        public string Pavadinimas { get; set; }

        private string isbn;
        public string ISBN
        {
            get { return isbn; }
            set { if(value.Length!=13) throw new Exception("ISBN turi sudaryti 13 skaiciu!"); long skc;if(!long.TryParse(value,out skc)) throw new Exception("ISBN turi sudaryti TIK skaiciai"); isbn = value; }
        }
        
        public Knyga(string pavadinimas, string isbn){
            Pavadinimas = pavadinimas;
            ISBN = isbn;
        }
        
    }
}