using RelaxKundura3.Models;

namespace RelaxKundura3.Abstract
{
    public interface ISepetService
    {
        List<Sepet> GetSepets();
        void SepeteEkleGuncelle(Sepet Guncelle);
        void Sil(Guid id);
        void SepetiSil();
        Sepet GetSepetById(Guid id);
        public List<Sepetler> GetSepetlers();
    }
}
