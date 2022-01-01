using Instagram_Publish.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Publish.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> SendPost(string caption)
        {
            
            var userName = configuration["SiteSettings:Username"];
            var password = configuration["SiteSettings:Password"];
            var imageUrl = configuration["SiteSettings:ImageURL"];
            var uri = $"graph.facebook.com/{userName}/media?image_url = {imageUrl}&caption ={caption}";
            
            var client = new HttpClient();
            
            var containerId = await client.PostAsync(uri, null);

            var uriForPost = $"graph.facebook.com/{userName}/media_publish?creation_id={containerId}";

            await client.PostAsync(uriForPost, null);
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
