using System.Diagnostics;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using SummerizeMVC.Models;

namespace SummerizeMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var homeModel = new HomeModel();

            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://b9xup5upy7.execute-api.eu-central-1.amazonaws.com/DEV/text");
            //httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Method = "POST";

            //using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //{
            //    string json = "{\"modelId\":\"amazon.titan-text-express-v1\"," +
            //                  "\"prompt\":\"Sophie Hilbrand gaat een aantal nieuwe afleveringen van het programma Nu we er toch zijn maken, bevestigt een woordvoerder van BNNVARA na berichtgeving van Tina Nijkamp. Het programma werd tussen 2004 en 2012 ook op televisie uitgezonden.n Nu we er toch zijn wordt de gastvrijheid van mensen in Nederland getest. De 49-jarige Hilbrand gaat samen met een geluidsman en een cameraman op pad en belt bij mensen aan voor een hapje, een drankje of een slaapplek. De persoonlijke verhalen die de mensen dan vertellen, staan centraal in het programma.Het programma werd jarenlang gepresenteerd door Eddy Zoëy, later door Filemon Wesselink en Hilbrand. In 2020 werden nog vijf afleveringen gemaakt die in het teken stonden van Pride. Die werden door wisselende presentatoren gemaakt.De nieuwe reeks met Hilbrand draait om de aanstaande Tweede Kamerverkiezingen en de politieke keuzes van de mensen bij wie wordt aangebeld. In Nu we er toch zijn: De Verkiezingen gaat Hilbrand langs in gemeenten waar de vier grootste partijen in 2023 de meeste stemmen behaalden. BNNVARA noemt het programma 'een menselijke, nieuwsgierige blik op Nederland, vlak voor de stembus opengaat'. Hilbrand kondigde in mei haar vertrek bij Bar Laat aan, destijds de late talkshow op NPO 1. Het besluit om te stoppen werd in samenspraak met BNNVARA genomen.\"}";

            //    streamWriter.Write(json);
            //}
            
            //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            //{
            //    homeModel.Result = streamReader.ReadToEnd();
            //}
            return View(homeModel);
        }

        [HttpPost]
        public ActionResult Summerize(TranscribeModel transcribeModel)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://b9xup5upy7.execute-api.eu-central-1.amazonaws.com/DEV/text");
            var test = Request;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"modelId\":\"amazon.titan-text-express-v1\"," +
                              "\"prompt\":\"Sophie Hilbrand gaat een aantal nieuwe afleveringen van het programma Nu we er toch zijn maken, bevestigt een woordvoerder van BNNVARA na berichtgeving van Tina Nijkamp. Het programma werd tussen 2004 en 2012 ook op televisie uitgezonden.n Nu we er toch zijn wordt de gastvrijheid van mensen in Nederland getest. De 49-jarige Hilbrand gaat samen met een geluidsman en een cameraman op pad en belt bij mensen aan voor een hapje, een drankje of een slaapplek. De persoonlijke verhalen die de mensen dan vertellen, staan centraal in het programma.Het programma werd jarenlang gepresenteerd door Eddy Zoëy, later door Filemon Wesselink en Hilbrand. In 2020 werden nog vijf afleveringen gemaakt die in het teken stonden van Pride. Die werden door wisselende presentatoren gemaakt.De nieuwe reeks met Hilbrand draait om de aanstaande Tweede Kamerverkiezingen en de politieke keuzes van de mensen bij wie wordt aangebeld. In Nu we er toch zijn: De Verkiezingen gaat Hilbrand langs in gemeenten waar de vier grootste partijen in 2023 de meeste stemmen behaalden. BNNVARA noemt het programma 'een menselijke, nieuwsgierige blik op Nederland, vlak voor de stembus opengaat'. Hilbrand kondigde in mei haar vertrek bij Bar Laat aan, destijds de late talkshow op NPO 1. Het besluit om te stoppen werd in samenspraak met BNNVARA genomen.\"}";

                streamWriter.Write(json);
            }
            var homeModel = new HomeModel();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                homeModel.Result = streamReader.ReadToEnd();
            }
            return Json(homeModel);
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
