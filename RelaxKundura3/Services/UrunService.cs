using Microsoft.EntityFrameworkCore;
using RelaxKundura3.Abstract;
using RelaxKundura3.Data;
using RelaxKundura3.Dtos;
using RelaxKundura3.Models;

namespace RelaxKundura3.Services
{
    public class UrunService : IUrunService
    {
        private readonly ApplicationDbContext _context;
        public UrunService(ApplicationDbContext context)
        {
            _context = context;

        }


        public List<UrunDto> GetUrunList(GetUrunlerInput input)
        {
            var urunler = _context.Uruns.Include(x => x.KategoriFk).Where(x => (input.UrunKategoriId <= 0 || input.UrunKategoriId == x.UrunKategoriId)).
                OrderBy(x => x.UrunId).
                Select(x => new UrunDto
                {
                    UrunId = x.UrunId,
                    UrunAd = x.UrunAd,
                    UrunFiyat = x.UrunFiyat,
                    UrunKategoriAdi = x.KategoriFk.KategoriAd,
                    UrunStokDurumu = x.UrunStokDurumu,
                    UrunCinsiyet = x.UrunCinsiyet,
                    UrunImageUrl = x.UrunImageUrl,
                }).ToList();
            return urunler;
        }

        public void UrunEkleGuncelle(CreateOrEditUrun input)
        {
            if (!input.UrunId.HasValue)
            {
                UrunEkle(input);
            }
            else
            {
                UrunGuncelle(input);
            }
        }
        private void UrunEkle(CreateOrEditUrun input)
        {

            var urun = new Urun
            {
				UrunAd = input.UrunAd,
				UrunFiyat = input.UrunFiyat,
				UrunKategoriId = input.UrunKategoriId,
				UrunCinsiyet = input.UrunCinsiyet,
				UrunStokDurumu = input.UrunStokDurumu,
            };

			if (input.UrunImageUrl != null)
			{
				// Resmi kaydetmek için bir dosya adı oluştur
				var fileName = Guid.NewGuid().ToString() + Path.GetExtension(input.UrunImageUrl.FileName);
				var imagePath = Path.Combine("wwwroot/Images/", fileName);

				// Resmi wwwroot/images klasörüne kaydet
				using (var stream = new FileStream(imagePath, FileMode.Create))
				{
					input.UrunImageUrl.CopyTo(stream);
				}

				// Resmin dosya adını veritabanına kaydet
				urun.UrunImageUrl = fileName;
			}

			_context.Uruns.Add(urun);
            _context.SaveChanges();
        }
        public void UrunSil(Urun input)
        {

            _context.Uruns.Remove(input);
            _context.SaveChanges();
        }
        private void UrunGuncelle(CreateOrEditUrun input)
        {
            var mevcuturun = _context.Uruns.Where(x => x.UrunId == input.UrunId.Value).FirstOrDefault();
            if (mevcuturun != null)
            {
                mevcuturun.UrunAd = input.UrunAd;
                mevcuturun.UrunFiyat = input.UrunFiyat;
                mevcuturun.UrunCinsiyet = input.UrunCinsiyet;
                mevcuturun.UrunStokDurumu = input.UrunStokDurumu;
                mevcuturun.UrunKategoriId = input.UrunKategoriId;

                _context.Uruns.Update(mevcuturun);
                _context.SaveChanges();
            }
        }
        public Urun GetUrunById(Guid id)
        {
            return _context.Uruns.Where(x => x.UrunId == id).FirstOrDefault();
        }
		public List<Kategori> GetKategoriler()
		{
			return _context.Kategoris.OrderBy(x => x.KategoriAd).ToList();
		}
        public UrunDto GetUrunDtoById(Guid id)
        {
            var urun = _context.Uruns
                .Include(x => x.KategoriFk)
                .Where(x => x.UrunId == id)
                .Select(x => new UrunDto
                {
                    UrunId = x.UrunId,
                    UrunAd = x.UrunAd,
                    UrunFiyat = x.UrunFiyat,
                    UrunKategoriAdi = x.KategoriFk.KategoriAd,
                    UrunStokDurumu = x.UrunStokDurumu,
                    UrunCinsiyet = x.UrunCinsiyet,
                    UrunImageUrl = x.UrunImageUrl,
                })
                .FirstOrDefault();

            return urun;
        }

        public Tuple<int, int> GetCinsiyetStatistics()
        {
            int erkekSayisi = _context.Uruns.Count(u => u.UrunCinsiyet == "Erkek");
            int kadinSayisi = _context.Uruns.Count(u => u.UrunCinsiyet == "Kadın");

            return Tuple.Create(erkekSayisi, kadinSayisi);
        }

    }
}
