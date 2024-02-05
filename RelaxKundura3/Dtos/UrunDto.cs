namespace RelaxKundura3.Dtos
{
    public class UrunDto
    {
        public Guid UrunId { get; set; }
        public string UrunAd { get; set; }
        public string UrunKategoriAdi { get; set; }
        public string UrunCinsiyet { get; set; }
        public int UrunStokDurumu { get; set; }
        public decimal UrunFiyat { get; set; }
        public string UrunImageUrl { get; set; }
    }
}
