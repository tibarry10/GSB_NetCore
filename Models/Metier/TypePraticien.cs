namespace GSB_NetCore.Models.Metier
{
    public class TypePraticien
    {
        public int IdTypePraticien { get; set; }
        public string LibTypePraticien { get; set; }
        public string LieuTypePraticien { get; set; }

        public ICollection<Praticien> Praticiens { get; set; }
    }

}
