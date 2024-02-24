using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

   
    public async Task<IActionResult> Index(string nomPraticien, int? annee, int? mois)
    {
        ViewData["NomPraticien"] = nomPraticien;

        // Récupérer l'année et le mois actuels par défaut
        int anneeActuelle = DateTime.Now.Year;
        int moisActuel = DateTime.Now.Month;

        // Utiliser les valeurs fournies ou les valeurs actuelles par défaut
        int anneeSelectionnee = annee ?? anneeActuelle;
        int moisSelectionne = mois ?? moisActuel;

        // Remplir les listes d'années et de mois pour les menus déroulants
        List<int> anneesDisponibles = Enumerable.Range(anneeActuelle, 10).ToList(); // 10 années à partir de l'année actuelle
        List<int> moisDisponibles = Enumerable.Range(1, 12).ToList(); // Les 12 mois

        // Calculer le nombre de jours dans le mois sélectionné
        //int nombreDeJoursDansMois = DateTime.DaysInMonth(anneeSelectionnee, moisSelectionne);
        // Calculer le nombre de jours dans le mois sélectionné
        int nombreDeJoursDansMois = moisSelectionne == 2 && EstBissextile(anneeSelectionnee) ? 29 : DateTime.DaysInMonth(anneeSelectionnee, moisSelectionne);


        // Créer une liste de dates pour le mois sélectionné
        List<DateTime> datesDuMois = new List<DateTime>();
        for (int jour = 1; jour <= nombreDeJoursDansMois; jour++)
        {
            DateTime date = new DateTime(anneeSelectionnee, moisSelectionne, jour);
            datesDuMois.Add(date);
        }

        //  ViewBag.Rdvs = listeRdvs;

        // Passer les données aux menus déroulants dans la vue
        ViewBag.Annees = new SelectList(anneesDisponibles, anneeSelectionnee);
        ViewBag.Mois = new SelectList(moisDisponibles, moisSelectionne);
        ViewBag.NombreDeJoursDansMois = nombreDeJoursDansMois;


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
                // Trier les rendez-vous par ordre chronologique
                listeRdvs = listeRdvs.OrderBy(rdv => rdv.Date).ToList();


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
    private bool EstBissextile(int annee)
    {
        return (annee % 4 == 0 && annee % 100 != 0) || (annee % 400 == 0);
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



