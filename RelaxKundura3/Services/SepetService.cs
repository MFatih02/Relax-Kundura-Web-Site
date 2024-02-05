using RelaxKundura3.Abstract;
using RelaxKundura3.Data;
using RelaxKundura3.Data.Migrations;
using RelaxKundura3.Models;

namespace RelaxKundura3.Services
{
    public class SepetService:ISepetService
    {
        private readonly ApplicationDbContext _context;

        public SepetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Sepet GetSepetById(Guid id)
        {
            return _context.Sepetts.Where(x => x.UrunId == id).FirstOrDefault();
        }

        public List<Sepet> GetSepets()
        {
            return _context.Sepetts.OrderBy(x=>x.UrunAdi).ToList();
            
        }
        public List<Sepetler> GetSepetlers() 
        {
            return _context.Sepetlers.OrderBy(x=>x.SepetlerId).ToList();
        }

        public void SepeteEkleGuncelle(Sepet Guncelle)
        {
            SepetGuncelle(Guncelle);
        }

        public void SepetGuncelle(Sepet Sepet) 
        {
            var mevcutSepet = _context.Sepetts
                .Where(x => x.UrunId == Sepet.UrunId)
                .FirstOrDefault();

            if (mevcutSepet != null)
            {
                throw new Exception("Bu ürün sepette mevcut");

            }
            else
            {
                _context.Sepetts.Add(new Sepet
                {
                    SepetId = Guid.NewGuid(),
                    UrunId = Sepet.UrunId,
                    UrunAdi = Sepet.UrunAdi,
                    UrunFiyat = Sepet.UrunFiyat,
                    UrunImage = Sepet.UrunImage,
                });
            }

            _context.SaveChanges();
        }

        public void SepetiSil()
        {
            var sepetItems = _context.Sepetts.ToList();

            foreach (var item in sepetItems)
            {
                _context.Sepetts.Remove(item);
            }

            _context.SaveChanges();
        }

        public void Sil(Guid id)
        {

            var sepet = _context.Sepetts.FirstOrDefault(x => x.UrunId== id);

            if (sepet != null)
            {
                _context.Sepetts.Remove(sepet);
                _context.SaveChanges();
            }
        }

    }
}
