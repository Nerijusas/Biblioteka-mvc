using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteka_mvc.Models.Biblioteka;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Biblioteka_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Lankytojas As = new Lankytojas("Nerijus Viluckas", BibliotekaManager.Biblioteka.GeneruotiID());
            Lankytojas draugas = new Lankytojas("Markas Garuolis", BibliotekaManager.Biblioteka.GeneruotiID());    

            Knyga knyga = new Knyga("Rozemis klotas danugs","1111111111111");
            Knyga knyga2 = new Knyga("Alisa stebuklu salyje","1111111111112");
            Knyga knyga3 = new Knyga("C# pamokos, noob edition","1111111111113");

            BibliotekaManager.Biblioteka.Lankytojai.Add(As);
            BibliotekaManager.Biblioteka.Lankytojai.Add(draugas);

            BibliotekaManager.Biblioteka.IdetiKnyga(knyga);
            BibliotekaManager.Biblioteka.IdetiKnyga(knyga2);
            BibliotekaManager.Biblioteka.IdetiKnyga(knyga3);

            BibliotekaManager.Biblioteka.PasiimtiKnyga(knyga,As.ID);
            BibliotekaManager.Biblioteka.PasiimtiKnyga(knyga2,As.ID);
            BibliotekaManager.Biblioteka.PasiimtiKnyga(knyga3,draugas.ID);
            BibliotekaManager.Biblioteka.Isdavimai[0].GrazinimoData = DateTime.Now.AddDays(-2);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
