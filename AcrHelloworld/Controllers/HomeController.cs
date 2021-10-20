using AcrHelloworld.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AcrHelloworld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var registryURL = Environment.GetEnvironmentVariable("DOCKER_REGISTRY");
                ViewData["REGISTRYURL"] = registryURL;
                if (registryURL != "<acrName>.azurecr.io")
                {
                    var hostEntry = await System.Net.Dns.GetHostEntryAsync(registryURL);
                    ViewData["HOSTENTRY"] = hostEntry.HostName;

                    string region = hostEntry.HostName.Split('.')[1];
                    ViewData["REGION"] = region;

                    var registryIp = System.Net.Dns.GetHostAddresses(registryURL)[0].ToString();
                    ViewData["REGISTRYIP"] = registryIp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return View();
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
