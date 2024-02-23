using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace UI.Controllers
{
    public class AgendaController:Controller
    { 
    public IActionResult Index()
    {
        // Initialisez votre DropDownList avec les mois de l'année
        ViewBag.MoisListe = Enumerable.Range(1, 12).Select(m => new SelectListItem
        {
            Value = m.ToString(),
            Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)
        });

        // Initialisez votre vue avec un mois par défaut (par exemple, le mois actuel)
        ViewBag.MoisSelectionne = DateTime.Now.Month;

        return View();
    }

    [HttpPost]
    public IActionResult AfficherAgenda(int? moisSelectionne)
    {
            ViewBag.MoisListe = Enumerable.Range(1, 12).Select(m => new SelectListItem
            {
                Value = m.ToString(),
                Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)
            });

            ViewBag.MoisSelectionne = moisSelectionne ?? DateTime.Now.Month;

            return View();
    }
}
}
