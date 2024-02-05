using System.ComponentModel.DataAnnotations;

namespace RelaxKundura3.Dtos
{
    public class CreateOrEditUrun
    {
        public Guid? UrunId { get; set; }
        [Required(ErrorMessage = "Lütfen Ürün Adını Kontrol Edin")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ürün Adı 2 Karakterden Az, 50 Karakterden Fazla Olamaz")]
        public string UrunAd { get; set; }

        public int UrunKategoriId { get; set; }
        [Required(ErrorMessage = "Lütfen Ürün Cinsiyetini Kontrol Edin")]
        public string UrunCinsiyet { get; set; }
        [Required(ErrorMessage = "Lütfen Ürün Stok Sayısını Kontrol Edin")]
        public int UrunStokDurumu { get; set; }
        [Required(ErrorMessage = "Lütfen Ürün Fiyatını Kontrol Edin")]
        public decimal UrunFiyat { get; set; }
        [Required(ErrorMessage = "Lütfen Ürün Resmini Kontrol Edin")]
        public IFormFile UrunImageUrl { get; set; }
    }
}
