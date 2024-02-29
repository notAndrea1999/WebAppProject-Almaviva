using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppWeek2.Interfaces;
using WebAppWeek2.Models;
using WebAppWeek2.Services;
using Newtonsoft.Json;
using Bogus.Bson;
using System.Text;

namespace WebAppWeek2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient _httpClient;

        private readonly IPersonaService<PersonaItaService> _personaItaService;

        private readonly IPersonaService<PersonaFrenchService> _personaFrService;

        List<PersonaModel> personaList = new List<PersonaModel>();


        public HomeController(ILogger<HomeController> logger, HttpClient httpClient,
            IPersonaService<PersonaItaService> personaItaService, IPersonaService<PersonaFrenchService> personaFrService)
        {
            _logger = logger;

            _httpClient = httpClient;

            _personaItaService = personaItaService;

            _personaFrService = personaFrService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> _listPersona()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7287/api/persona");
            _ = response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var persone = JsonConvert.DeserializeObject<List<PersonaModel>>(result);


            ViewBag.Count = persone.Count;


            return PartialView(persone);
        }

        public async Task<IActionResult> _formPersona(PersonaModel persona)
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7287");
            var json = JsonConvert.SerializeObject(persona);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/persona", content);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var postResponse = JsonConvert.DeserializeObject<PersonaModel>(responseContent);

                return RedirectToAction(nameof(Index));

            }
            return PartialView();
        }

        public async Task<IActionResult> UpdateVediDettaglio(PersonaModel persona, int id)
        {
            //_httpClient.BaseAddress = new Uri("https://localhost:7287");
            //var json = JsonConvert.SerializeObject(persona);
            //var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.GetAsync($"https://localhost:7287/api/persona/{id}");
            var result = await response.Content.ReadAsStringAsync();

            var persone = JsonConvert.DeserializeObject<PersonaModel>(result);

            return PartialView(persone);
        }

        public async Task<IActionResult> Update(PersonaModel persona, int id)
        {
            _httpClient.BaseAddress = new Uri("https://localhost:7287");
            var json = JsonConvert.SerializeObject(persona);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("/api/persona/" + id, content);


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7287/api/persona/{id}");
            var result = await response.Content.ReadAsStringAsync();

            var persone = JsonConvert.DeserializeObject<PersonaModel>(result);
            return RedirectToAction(nameof(Index));
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
