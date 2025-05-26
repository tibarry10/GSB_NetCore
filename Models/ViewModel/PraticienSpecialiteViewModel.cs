namespace GSB_NetCore.Models.ViewModel
{
    public class PraticienSpecialiteViewModel
    {
        public int IdPraticien { get; set; }
        public string NomComplet { get; set; }
        public string Ville { get; set; }
        public List<string> Specialites { get; set; }
    }

}
