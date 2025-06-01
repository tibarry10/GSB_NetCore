namespace GSB_NetCore.Models.Metier
{
    public class Specialite
    {
        public int IdPraticien { get; set; }
        public int IdSpecialite { get; set; }
        public string LibSpecialite { get; set; }

        public string Diplome { get; set; } = string.Empty;
        public double CoefPrescription { get; set; }

        public ICollection<Posseder> Praticiens { get; set; }
    }

}
