namespace GSB_NetCore.Models.Metier
{
    public class Famille
    {
        public int IdFamille { get; set; }
        public string LibFamille { get; set; }

        public ICollection<Medicament> Medicaments { get; set; }
    }

}
