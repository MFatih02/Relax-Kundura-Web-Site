using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RelaxKundura3.Abstract;

namespace RelaxKundura3.ViewComponents
{
    public class AdminKullanicilarComponent : ViewComponent
    {
        private readonly IKategoriService _kategoriService;
        private readonly IUrunService _urunService;
        private readonly ISepetService _sepetService;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminKullanicilarComponent(IKategoriService kategoriService, IUrunService urunService, ISepetService sepetService, UserManager<IdentityUser> userManager)
        {
            _kategoriService = kategoriService;
            _urunService = urunService;
            _sepetService = sepetService;
            _userManager = userManager;
        }
        public IViewComponentResult Invoke()
        {
            var userSayisi = _userManager.Users.Count();
            var urunSayisi = _urunService.GetUrunList(new Dtos.GetUrunlerInput { UrunKategoriId = -1 }).Count();
            var kategoriSayisi = _kategoriService.GetKategoriList().Count();

            ViewBag.UserSayisi = userSayisi;
            ViewBag.UrunSayisi = urunSayisi;
            ViewBag.KategoriSayisi = kategoriSayisi;

            var cinsiyetStatistics = _urunService.GetCinsiyetStatistics();
            ViewBag.ErkekUrunSayisi = cinsiyetStatistics.Item1;
            ViewBag.KadinUrunSayisi = cinsiyetStatistics.Item2;

            var kategoriUrunSayilari = _kategoriService.GetUrunSayilariByKategori();
            ViewBag.KategoriUrunSayilari = kategoriUrunSayilari;

            return View();
        }
    }
}
