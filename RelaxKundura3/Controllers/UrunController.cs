using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelaxKundura3.Abstract;
using RelaxKundura3.Dtos;
using RelaxKundura3.Models;
using System;

namespace RelaxKundura3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UrunController : Controller
    {
        private readonly IUrunService _urunService;
        private readonly IKategoriService _kategoriService;

        public UrunController(IUrunService urunService, IKategoriService kategoriService)
        {
            _urunService = urunService;
            _kategoriService = kategoriService;
        }

        public IActionResult Index()
        {
            var urunler = _urunService.GetUrunList(new GetUrunlerInput { UrunKategoriId = -1 });
            return View(urunler);
        }

        public IActionResult Ekle()
        {
            var model = new UrunEkleGuncelleView
            {
                Kategoriler = _kategoriService.GetKategoriList(),
                Urun = new CreateOrEditUrun()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ekle(UrunEkleGuncelleView input)
        {

            _urunService.UrunEkleGuncelle(input.Urun);
            return RedirectToAction("Index");
        }

        public IActionResult Guncelle(Guid id)
        {
            var urun = _urunService.GetUrunById(id);
            var urunDto = new CreateOrEditUrun
            {
                UrunId = urun.UrunId,
                UrunAd = urun.UrunAd,
                UrunFiyat = urun.UrunFiyat,
                UrunKategoriId = urun.UrunKategoriId,
                UrunCinsiyet = urun.UrunCinsiyet,
                UrunStokDurumu = urun.UrunStokDurumu,
            };
            var model = new UrunEkleGuncelleView()
            {
                Kategoriler = _kategoriService.GetKategoriList(),
                Urun = urunDto
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Guncelle(UrunEkleGuncelleView input)
        {

            _urunService.UrunEkleGuncelle(input.Urun);
            return RedirectToAction("Index");
        }

        public IActionResult Sil(Guid id)
        {
            var urun = _urunService.GetUrunById(id);

            if (urun == null)
            {
                return NotFound();
            }

            _urunService.UrunSil(urun);

            return RedirectToAction("Index");
        }
    }
}
