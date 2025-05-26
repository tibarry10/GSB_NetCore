namespace GSB_NetCore.Models.Metier
{
    public class StatsPrescription
    {
        public int IdPraticien { get; set; }
        public int IdMedicament { get; set; }
        public string AnneeMois { get; set; }
        public int Quantite { get; set; }

        public Praticien Praticien { get; set; }
        public Medicament Medicament { get; set; }
    }

}
