namespace UI.Models
{
   
        public class Calendrier
        {
            public int Id { get; set; }
            public int IdPraticien { get; set; }
            public List<Rdv> RendezVous { get; set; } = new List<Rdv>();
        }
    
}
