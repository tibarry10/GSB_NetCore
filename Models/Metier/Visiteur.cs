namespace GSB_NetCore.Models.Metier
{
    public class Visiteur
    {
        public string IdVisiteur { get; set; }
        public string NomVisiteur { get; set; }
        public string PrenomVisiteur { get; set; }
        public string Login { get; set; }
        public string MotDePasse { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }

        public Visiteur() { }
    }
}
