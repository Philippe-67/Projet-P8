using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UI.Models;

public class RdvController : Controller
{
    private readonly HttpClient _httpClient;


    public RdvController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:5001"); // Assurez-vous de mettre le bon port pour votre API Rdv
    }

   
    public async Task<IActionResult> Index(string nomPraticien)
    {
        ViewData["NomPraticien"] = nomPraticien;
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/Rdv");

            if (response.IsSuccessStatusCode)
            {
                // Lecture et traitement des données
                string responseData = await response.Content.ReadAsStringAsync();
                // Désérialise la chaîne JSON en une liste d'objets
                var listeRdvs = JsonConvert.DeserializeObject<List<Rdv>>(responseData);

                // Filtrer les rendez-vous en fonction de nomPraticien
                if (!string.IsNullOrEmpty(nomPraticien))
                {
                    listeRdvs = listeRdvs.Where(rdv => rdv.NomPraticien == nomPraticien).ToList();
                }

                // Utilise les données comme nécessaire, peut-être passer à la vue
                ViewBag.Rdvs = listeRdvs;
                return View();
            }
            else
            {
                return StatusCode((int)response.StatusCode, $"Erreur HTTP: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erreur lors de la requête : {ex.Message}");
        }
    }

    public IActionResult Create(int praticienId, string nomPraticien)
    {
        ViewData["PraticienId"] = praticienId;
        ViewData["NomPraticien"] = nomPraticien;
        // Affiche le formulaire de création de rendez-vous
        return View();
    }

   
    [HttpPost]
    public async Task<IActionResult> Create(Rdv rdv)
    {
        try
        {
            await CreateRendezVousAsync(rdv);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return View("Error", ex.Message);
        }
    }
    // Ajoutez d'autres actions selon les besoins de votre application

    private async Task CreateRendezVousAsync(Rdv rdv)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Rdv", rdv); // Assurez-vous que l'URL est correcte

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception(errorMessage);
        }
    }
 
}



