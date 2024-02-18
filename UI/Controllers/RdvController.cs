using Microsoft.AspNetCore.Mvc;
using UI.Models;

public class RdvController : Controller
{
    private readonly HttpClient _httpClient;

    public RdvController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://localhost:5001"); // Assurez-vous de mettre le bon port pour votre API Rdv
    }

    public IActionResult Index()
    {
        // Code pour afficher la liste des rendez-vous
        return View();
    }

    public IActionResult Create()
    {
        // Affiche le formulaire de création de rendez-vous
        return View();
    }

    //[HttpPost]
    //public async Task<IActionResult> Create(Rdv rdv)
    //{
    //    try
    //    {
    //        await CreateRendezVousAsync(rdv);
    //        return RedirectToAction("Index");
    //    }
    //    catch (Exception ex)
    //    {
    //        return View("Error", ex.Message);
    //    }
    //}
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

