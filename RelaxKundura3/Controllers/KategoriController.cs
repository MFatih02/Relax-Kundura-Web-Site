using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelaxKundura3.Abstract;
using RelaxKundura3.Dtos;
using RelaxKundura3.Services;

namespace RelaxKundura3.Controllers
{

    [Authorize(Roles = "Admin")]
    public class KategoriController : Controller
    {
        private readonly IKategoriService _kategoriService;
        public KategoriController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }

        public IActionResult Index()
        {
            var kategoriler = _kategoriService.GetKategoriList();
            return View(kategoriler);
        }
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ekle(CreateOrEditKategoriDto input)
        {
            if (ModelState.IsValid)
            {
                _kategoriService.KategoriEkleGuncelle(input);
                return RedirectToAction("Index");
            }
            return View(input);
        }

        public IActionResult Guncelle(int id)
        {
            var kategori = _kategoriService.GetKategoriById(id);
            var model = new CreateOrEditKategoriDto
            {
                KategoriId = kategori.KategoriId,
                KategoriAd = kategori.KategoriAd,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(CreateOrEditKategoriDto input)
        {
            if (ModelState.IsValid)
            {
                _kategoriService.KategoriEkleGuncelle(input);
                return RedirectToAction("Index");
            }
            return View(input);
        }

        public IActionResult Sil(int id)
        {
            var urun = _kategoriService.GetKategoriById(id);

            if (urun == null)
            {
                return NotFound();
            }

            _kategoriService.KategoriSil(urun);

            return RedirectToAction("Index");
        }
    }
}

