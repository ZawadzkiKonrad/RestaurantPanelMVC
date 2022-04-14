using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestaurantPanelMVC.Web.Models;
using RestaurantPanelMVC.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPanelMVC.Web.Controllers
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
            return View();
        } 
        public async Task<IActionResult> LoginUser(UserInfo user)
        {
            using (var httpClient= new HttpClient())
            {
                StringContent stringContent= new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8,"application/json");
                using (var response = await httpClient.PostAsync("https://localhost:5001/api/account/login",stringContent))
                {
                    string token = await response.Content.ReadAsStringAsync();
                    if (token=="Invalid credentials")
                    {
                        ViewBag.Message = "Zły email i/lub hasło";
                        return Redirect("~/Home/Index");
                    }
                    HttpContext.Session.SetString("JWToken", token);
                }
            }
            return Redirect("~/Menu/Index");
        } 
        
        public IActionResult Menu()
        {
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
