//using Microsoft.AspNetCore.Mvc;
//using UI.Models;
//public class PraticienController : Controller
//{
//    private readonly HttpClient _httpClient;

//    public PraticienController(IHttpClientFactory httpClientFactory)
//    {
//        _httpClient = httpClientFactory.CreateClient();

//        _httpClient.BaseAddress = new Uri("https://LocalHost:5000");

//    }

//    public async Task<IActionResult> Index(int? praticienId)
//    {
//        try
//        {
//            // Si aucun praticien n'est sélectionné, afficher simplement la vue sans données
//            if (!praticienId.HasValue)
//            {
//                ViewBag.PraticienList = await GetPraticienListAsync();
//                return View();
//            }

//            // Si un praticien est sélectionné, récupérer les données filtrées
//            var filteredPraticienList = await GetPraticienListAsync();

//            // Filtrer les praticiens
//            filteredPraticienList = praticienId.HasValue
//                ? filteredPraticienList.Where(p => p.Id == praticienId).ToList()
//                : filteredPraticienList;

//            ViewBag.PraticienList = await GetPraticienListAsync(); //dropdownlist
//            return View(filteredPraticienList);
//        }
//        catch (Exception ex)
//        {
//            return View("Error", ex.Message);
//        }
//    }

//    private async Task<List<Praticien>> GetPraticienListAsync()
//    {
//        var response = await _httpClient.GetAsync("/api/praticien");

//        if (response.IsSuccessStatusCode)
//        {
//            return await response.Content.ReadFromJsonAsync<List<Praticien>>();
//        }
//        else
//        {
//            var errorMessage = await response.Content.ReadAsStringAsync();
//            throw new Exception(errorMessage);
//        }
//    }

//}


using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using UI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UI.ViewModels;

public class PraticienController : Controller
{
    private readonly HttpClient _httpClient;

    public PraticienController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://LocalHost:5000");
    }

    public async Task<IActionResult> Index(int? praticienId)
    {
        try
        {
            var viewModel = new PraticienViewModel
            {
                PraticienList = await GetPraticienListAsync(),
                SelectedPraticien = null, // Initialiser à null ou récupérer le praticien sélectionné
                SelectedRdvList = null // Initialiser à null ou récupérer les rendez-vous
            };

            // Logique pour récupérer les données du praticien et des rendez-vous
            if (praticienId.HasValue)
            {
                viewModel.SelectedPraticien = viewModel.PraticienList.FirstOrDefault(p => p.Id == praticienId);
                viewModel.SelectedRdvList = await GetRdvListAsync(praticienId);
            }

            return View(viewModel);
        }
        catch (Exception ex)
        {
            return View("Error", ex.Message);
        }
        //    try
        //    {
        //        //          Si aucun praticien n'est sélectionné, afficher simplement la vue sans données
        //        if (!praticienId.HasValue)
        //        {
        //            ViewBag.PraticienList = await GetPraticienListAsync();
        //            return View();
        //        }

        //        //         Si un praticien est sélectionné, récupérer les données filtrées
        //        var filteredPraticienList = await GetPraticienListAsync();

        //        //         Filtrer les praticiens
        //        filteredPraticienList = praticienId.HasValue
        //            ? filteredPraticienList.Where(p => p.Id == praticienId).ToList()
        //            : filteredPraticienList;

        //        ViewBag.PraticienList = filteredPraticienList; // dropdownlis
        //        //       Récupérer les rendez - vous du praticien sélectionné
        //        var rendezVousResponse = await _httpClient.GetAsync($"/api/rdv/Praticien/{praticienId}");

        //        if (rendezVousResponse.IsSuccessStatusCode)
        //        {
        //            var rendezVousList = await rendezVousResponse.Content.ReadFromJsonAsync<List<Rdv>>();
        //            ViewBag.SelectedRdvList = rendezVousList; // stocker les rendez-vous dans ViewBag
        //        return View(rendezVousList);

        //        }
        //        else
        //        {
        //            var errorMessage = await rendezVousResponse.Content.ReadAsStringAsync();
        //            throw new Exception(errorMessage);
        //        }

        //        return View(filteredPraticienList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error", ex.Message);
        //    }
        }

        private async Task<List<Praticien>> GetPraticienListAsync()
    {
        var response = await _httpClient.GetAsync("/api/praticien");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<Praticien>>();
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception(errorMessage);
        }
    }
    private async Task<List<Rdv>> GetRdvListAsync(int? praticienId)
    {
        var response = await _httpClient.GetAsync($"/api/rdv/Praticien/{praticienId}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<Rdv>>();
        }
        else
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception(errorMessage);
        }
    }
}


