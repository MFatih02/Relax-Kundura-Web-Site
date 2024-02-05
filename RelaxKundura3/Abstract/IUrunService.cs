using RelaxKundura3.Dtos;
using RelaxKundura3.Models;

namespace RelaxKundura3.Abstract
{
    public interface IUrunService
    {
        List<UrunDto> GetUrunList(GetUrunlerInput input);
        void UrunEkleGuncelle(Dtos.CreateOrEditUrun input);
        Urun GetUrunById(Guid id);
        void UrunSil(Urun input);
		List<Kategori> GetKategoriler();
        public UrunDto GetUrunDtoById(Guid id);
        public Tuple<int, int> GetCinsiyetStatistics();
    }
}
