﻿namespace ApiRdv.Models.Rdvs
{
    public class Rdv
    {
        public int Id { get; set; }
        public string NomPatient { get; set; }
        public string NomPraticien { get; set; }
        public DateTime Date { get; set; }
        public int IdPraticien { get; set; }
       
       
    }
}
