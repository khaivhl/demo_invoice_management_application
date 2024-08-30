namespace API_them_sua_xoa_du_lieu.Entities
{
    public class SanPham
    {
        public int SanPhamId { get; set; }
        public int LoaiSanPhamId { get; set; }
        public string TenSanPham { get; set; }
        public double GiaThanh { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string KyHieuSanPham { get; set; }
        List<ChiTietHoaDon>chiTietHoaDons { get; set; }
        public Loaisanpham LoaiSanPham { get; set; }
    }
}
