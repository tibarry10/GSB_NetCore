namespace GSB_NetCore.Models.Metier
{
    public class Medicament
    {
        public int IdMedicament { get; set; }
        public int IdFamille { get; set; }
        public string DepotLegal { get; set; }
        public string NomCommercial { get; set; }
        public string Effets { get; set; }
        public string ContreIndication { get; set; }
        public double PrixEchantillon { get; set; }

        public Famille Famille { get; set; }
        public ICollection<StatsPrescription> Prescriptions { get; set; }
    }

}
