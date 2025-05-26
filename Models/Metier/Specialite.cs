namespace GSB_NetCore.Models.Metier
{
    public class Specialite
    {
        public int IdSpecialite { get; set; }
        public string LibSpecialite { get; set; }

        public ICollection<Posseder> Praticiens { get; set; }
    }

}
