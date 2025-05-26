namespace GSB_NetCore.Models.Metier
{
    public class Posseder
    {
        public int IdPraticien { get; set; }
        public int IdSpecialite { get; set; }
        public string Diplome { get; set; }
        public double CoefPrescription { get; set; }

        public Praticien Praticien { get; set; }
        public Specialite Specialite { get; set; }
    }

}
