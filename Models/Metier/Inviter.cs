
namespace GSB_NetCore.Models.Metier
{
    public class Inviter
    {
        public int IdPraticien { get; set; }
        public int IdActiviteCompl { get; set; }
        public string Specialiste { get; set; }

        public Praticien Praticien { get; set; }
        public ActiviteCompl ActiviteCompl { get; set; }
    }

}
