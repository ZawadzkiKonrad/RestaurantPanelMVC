using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantPanelMVC.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestaurantPanelMVC.Web.Controllers
{
    public class MenuController : Controller
    {
        public static string baseUrl = "https://localhost:5001/api/menu/2";
        public async Task<IActionResult> Index()
        {
            var menu = await GetMenu();
            return View(menu);
        }

        [HttpGet]
        public async Task<IEnumerable<MenuVm>> GetMenu()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr= await client.GetStringAsync(url);

            var res= JsonConvert.DeserializeObject<List<MenuVm>>(jsonStr).AsEnumerable();
            return res;
        }
    }
}
