using RelaxKundura3.Dtos;

namespace RelaxKundura3.Models
{
    public class UrunEkleGuncelleView
    {
        public Dtos.CreateOrEditUrun Urun { get; set; }
        public List<Kategori> Kategoriler { get; set; }
    }
}

