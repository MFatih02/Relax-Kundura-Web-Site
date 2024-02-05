using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RelaxKundura3.Models
{
    public class Urun
    {
        public Guid UrunId { get; set; }
        [Required]
        public string UrunAd { get; set; }

        public int UrunKategoriId { get; set; }
		[Required]
		public string UrunCinsiyet { get; set; }
		[Required]
		public int UrunStokDurumu { get; set; }
		[Required]
		public decimal UrunFiyat { get; set; }
		[Required]
		public string UrunImageUrl { get; set; }

        [ForeignKey("UrunKategoriId")]
        public Kategori KategoriFk { get; set; }

    }
}
