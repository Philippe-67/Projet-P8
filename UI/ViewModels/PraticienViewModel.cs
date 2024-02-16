using UI.Models;

namespace UI.ViewModels
{
    public class PraticienViewModel
    {
        public List<Praticien> PraticienList { get; set; }
        public Praticien SelectedPraticien { get; set; }
        public List<Rdv> SelectedRdvList { get; set; }
    }
}
