namespace API_them_sua_xoa_du_lieu.Entities
{
    public class HoaDon
    {
        public int HoaDonId { get; set; }
        public int KhachHangId { get; set; }    
        public string TenHangHoa { get; set; }
        public string? MaGiaoDich { get; set; }
        public DateTime ThoiGianTao { get; set; }
        public DateTime? ThoiGianCapNhat { get; set; }
        public string? GhiChu { get; set; }
        public double? TongTien { get; set; }
        public KhachHang? KhachHang { get; set; }
        public IEnumerable<ChiTietHoaDon> chiTietHoaDons { get; set; }
       
    }
}
