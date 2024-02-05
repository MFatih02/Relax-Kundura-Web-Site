using RelaxKundura3.Dtos;
using RelaxKundura3.Models;

namespace RelaxKundura3.Abstract
{
    public interface IKategoriService
    {
        List<Kategori> GetKategoriList();
        void KategoriEkleGuncelle(CreateOrEditKategoriDto input);
        void KategoriSil(Kategori input);
        Kategori GetKategoriById(int id);
        Dictionary<string, int> GetUrunSayilariByKategori();
    }
}
