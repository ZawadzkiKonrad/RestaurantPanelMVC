using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestaurantPanelMVC.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RestaurantPanelMVC.Web.Controllers
{
    public class MenuController : Controller
    {
        public static string baseUrl = "http://ekelnerapp.hostingasp.pl/api/menu/1";
        public static string baseUrl2 = "http://ekelnerapp.hostingasp.pl/api/menu/addDish/1";
        public static string baseUrl3 = "http://ekelnerapp.hostingasp.pl/api/menu/getDish/";
        public static string baseUrl4 = "http://ekelnerapp.hostingasp.pl/api/menu/updateDish/";
        public static string baseUrl5 = "http://ekelnerapp.hostingasp.pl/api/menu/getCategories";
        public async Task<IActionResult> Index()
        {
            var menu = await GetMenu();

            return View(menu);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl3 + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpUtility.UrlEncode(accessToken));

            string jsonStr = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<CreateDishVm>(jsonStr);

            var categories = await GetCategories();
            res.Categories = new SelectList(categories, nameof(CategoryVm.Id), nameof(CategoryVm.Name));
            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateDishVm vm)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl4 + vm.Id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpUtility.UrlEncode(accessToken));

            var stringContent = new StringContent(JsonConvert.SerializeObject(vm), Encoding.UTF8, "application/json");
            await client.PutAsync(url, stringContent);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Create()
        {
            var categories = await GetCategories();
            var create = new CreateDishVm();
            create.Categories = new SelectList(categories, nameof(CategoryVm.Id), nameof(CategoryVm.Name));
            return View(create);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDishVm vm)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl2;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpUtility.UrlEncode(accessToken));
            var stringContent = new StringContent(JsonConvert.SerializeObject(vm), Encoding.UTF8, "application/json");
            await client.PostAsync(url, stringContent);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IEnumerable<MenuVm>> GetMenu()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpUtility.UrlEncode(accessToken));
            string jsonStr = await client.GetStringAsync(url);

            var res = JsonConvert.DeserializeObject<List<MenuVm>>(jsonStr).AsEnumerable();
            return res;
        }
        [HttpGet]
        public async Task<IEnumerable<CategoryVm>> GetCategories()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseUrl5;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpUtility.UrlEncode(accessToken));
            string jsonStr = await client.GetStringAsync(url);

            var res = JsonConvert.DeserializeObject<List<CategoryVm>>(jsonStr).AsEnumerable();
            return res;
        }
    }
}
