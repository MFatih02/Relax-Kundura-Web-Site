using System.ComponentModel.DataAnnotations.Schema;

namespace RelaxKundura3.Models
{
    public class Sepet
    {
        public Guid SepetId { get; set; }
        public Guid UrunId { get; set; }
        public string UrunAdi { get; set; }
        public decimal UrunFiyat { get; set; }
        public string UrunImage { get; set; }
    }
    public class Sepetler
    {
        public Guid SepetlerId { get; set; }
        public decimal SepetToplamFiyat { get; set; }
    }
}
