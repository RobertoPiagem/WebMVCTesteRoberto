using Firebase.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebMVCTesteRoberto.Models;


namespace WebMVCTesteRoberto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            var currentUserLogin = new ControleWinServices();
            var firebaseClient = new FirebaseClient("https://teste---vaga-de-net-default-rtdb.firebaseio.com");

            var dbControle = await firebaseClient
              .Child("ControleWinServices")
              .OnceAsync<ControleWinServices>();

            var listCWS = new List<ControleWinServices>();

            foreach (var controle in dbControle)
            {
                var con = new ControleWinServices()
                {
                    NmMaquina = controle.Object.NmMaquina,
                    NmServico = controle.Object.NmServico,
                    DtUltimaExecucao = controle.Object.DtUltimaExecucao
                };

                listCWS.Add(con);
            }

            ViewBag.Controles = listCWS.OrderByDescending(x => x.DtUltimaExecucao);
            return View();
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
