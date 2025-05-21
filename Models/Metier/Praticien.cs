namespace GSB_NetCore.Models.Metier
{
    public class Praticien
    {
        public int IdPraticien { get; set; }
        public string NomPraticien { get; set; }
        public string PrenomPraticien { get; set; }
        public string VillePraticien { get; set; }
        public int IdTypePraticien { get; set; }
        public string CpPraticien { get; set; }
        public TypePraticien Type { get; set; }

        public ICollection<Posseder> SpecialitesPossedees { get; set; }
        public ICollection<Inviter> Invitations { get; set; }
    }
}
