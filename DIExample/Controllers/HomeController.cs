using DIExample.Lifetimes;
using DIExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TransientService transientService;
        private readonly ScopeService scopeService;
        private readonly SingletonService singletonService;

        public HomeController(ILogger<HomeController> logger, TransientService transientService, ScopeService scopeService, SingletonService singletonService)
        {
            _logger = logger;
            this.transientService = transientService;
            this.scopeService = scopeService;
            this.singletonService = singletonService;
        }

        public IActionResult Index()
        {
            var messages = new List<string>()
            {
                HttpContext.Items["MiddlewareTransientService"].ToString(),
                $"Transient Controller - {transientService.GetGuid()}",
                 HttpContext.Items["MiddlewareScopeService"].ToString(),
                $"Transient Controller - {scopeService.GetGuid()}",
                 HttpContext.Items["MiddlewareSingletonService"].ToString(),
                $"Transient Controller - {singletonService.GetGuid()}"

            };
            
            return View(messages);
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