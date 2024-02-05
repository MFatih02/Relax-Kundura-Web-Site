using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RelaxKundura3.Abstract;
using RelaxKundura3.Dtos;
using RelaxKundura3.Extentions;
using RelaxKundura3.Models;
using RelaxKundura3.Services;
using System.Diagnostics;

namespace RelaxKundura3.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUrunService _urunService;
		private readonly IKategoriService _kategoriService;
        private readonly ISepetService _sepetService;
		public KullaniciController(UserManager<IdentityUser> userManager, IUrunService urunService, IKategoriService kategoriService,ISepetService sepetService)
		{
			_urunService = urunService;
			_kategoriService = kategoriService;
            _userManager = userManager;
            _sepetService = sepetService;
		}
		

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Alisveris(int selectedCategoryId = -1)
        {
			var kategoriler = _kategoriService.GetKategoriList();

			// Se�ilen kategoriyi view'a g�nder
			ViewBag.SelectedCategoryId = selectedCategoryId;

			// �r�nleri filtreleme
			var urunler = _urunService.GetUrunList(new Dtos.GetUrunlerInput { UrunKategoriId = selectedCategoryId });

			// Kategorileri view'a g�nder
			ViewBag.Kategoriler = new SelectList(kategoriler, "KategoriId", "KategoriAd");

			return View(urunler);
		}
        [Authorize]
        public IActionResult Detay(Guid id)
        {
            var urunDto = _urunService.GetUrunDtoById(id);

            if (urunDto == null)
            {
                return NotFound(); // �r�n bulunamad�ysa 404 hatas� d�nebilirsiniz.
            }

            return View(urunDto);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult Sepet()
        {
            var sepetUrunDto = new SepetUrunDto
            {
                GetSepet = _sepetService.GetSepets(),
                GetSepetler = _sepetService.GetSepetlers()
            };

            return View(sepetUrunDto);
        }

        [HttpPost]
        public IActionResult SepeteEkle(Guid urunId)
        {
            var urunDto = _urunService.GetUrunDtoById(urunId);

            if (urunDto == null)
            {
                return NotFound(); // �r�n bulunamad�ysa 404 hatas� d�nebilirsiniz.
            }

            // Sepet servisini kullanarak �r�n� sepete ekle
            var sepetItem = new Sepet
            {
                UrunId = urunDto.UrunId,
                UrunAdi = urunDto.UrunAd,
                UrunFiyat = urunDto.UrunFiyat,
                UrunImage = urunDto.UrunImageUrl
            };

            _sepetService.SepeteEkleGuncelle(sepetItem);

            return RedirectToAction("Sepet");
        }

        [HttpPost]
        public IActionResult SepettenCikar(Guid urunId)
        {
            try
            {
                _sepetService.Sil(urunId);

                return RedirectToAction("Sepet");
            }
            catch (Exception ex)
            {
                TempData["HataMesaji"] = "�r�n� sepetten ��karma i�lemi s�ras�nda bir hata olu�tu.";
                return RedirectToAction("Sepet");
            }
        }
        [HttpPost]
        public IActionResult SepetiBosalt()
        {
            _sepetService.SepetiSil();
            return RedirectToAction("Index");
        }


    }
}
