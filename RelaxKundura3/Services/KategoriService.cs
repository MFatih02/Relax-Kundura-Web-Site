using RelaxKundura3.Abstract;
using RelaxKundura3.Data;
using RelaxKundura3.Dtos;
using RelaxKundura3.Models;

namespace RelaxKundura3.Services
{
    public class KategoriService : IKategoriService
    {
        private readonly ApplicationDbContext _context;
        public KategoriService(ApplicationDbContext context)
        {
            _context = context;
        }


        List<Kategori> IKategoriService.GetKategoriList()
        {
            var kategoriler = _context.Kategoris.OrderBy(x => x.KategoriAd).ToList();
            return kategoriler;
        }

        void IKategoriService.KategoriEkleGuncelle(CreateOrEditKategoriDto input)
        {
            if (!input.KategoriId.HasValue)
            {
                KategoriEkle(input);
            }
            else
            {
                KategoriGuncelle(input);
            }
        }

        private void KategoriGuncelle(CreateOrEditKategoriDto input)
        {
            var mevcutkategori = _context.Kategoris.Where(x => x.KategoriId == input.KategoriId.Value).FirstOrDefault();
            if (mevcutkategori != null)
            {
                mevcutkategori.KategoriAd = input.KategoriAd;
                _context.Kategoris.Update(mevcutkategori);
                _context.SaveChanges();

            }
        }
        private void KategoriEkle(CreateOrEditKategoriDto input)
        {
            _context.Kategoris.Add(new Kategori
            {
                KategoriAd = input.KategoriAd,
            });
            _context.SaveChanges();
        }
        public void KategoriSil(Kategori input)
        {

            _context.Kategoris.Remove(input);
            _context.SaveChanges();
        }
        Kategori IKategoriService.GetKategoriById(int id)
        {
            return _context.Kategoris.Where(x => x.KategoriId == id).FirstOrDefault();
        }
        public Dictionary<string, int> GetUrunSayilariByKategori()
        {
            var urunSayilari = _context.Kategoris
                .Select(k => new
                {
                    KategoriAd = k.KategoriAd,
                    UrunSayisi = _context.Uruns.Count(u => u.UrunKategoriId == k.KategoriId)
                })
                .ToDictionary(x => x.KategoriAd, x => x.UrunSayisi);

            return urunSayilari;
        }
    }
}
