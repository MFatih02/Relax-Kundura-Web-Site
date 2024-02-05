using System.ComponentModel.DataAnnotations;

namespace RelaxKundura3.Dtos
{
    public class CreateOrEditKategoriDto
    {
        public int? KategoriId { get; set; }
        [Required(ErrorMessage = "Lütfen Kategori Adını Kontrol Edin")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Kategori Adı 2 Karakterden Az, 50 Karakterden Fazla Olamaz")]
        public string KategoriAd { get; set; }
    }
}
