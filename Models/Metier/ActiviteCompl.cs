namespace GSB_NetCore.Models.Metier
{
    public class ActiviteCompl
    {
        public int IdActiviteCompl { get; set; }
        public DateTime DateActivite { get; set; }
        public string LieuActivite { get; set; }
        public string ThemeActivite { get; set; }
        public string MotifActivite { get; set; }

        public ICollection<Inviter> Invitations { get; set; }
    }

}
