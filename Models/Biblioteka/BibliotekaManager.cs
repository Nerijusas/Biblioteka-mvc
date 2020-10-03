namespace Biblioteka_mvc.Models.Biblioteka
{
    public class BibliotekaManager
    {
        private static Biblioteka biblioteka = new Biblioteka();
        public static Biblioteka Biblioteka
        {
            get { return biblioteka; }
        }
        
        
    }
}