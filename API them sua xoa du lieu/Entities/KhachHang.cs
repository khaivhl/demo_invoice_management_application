namespace API_them_sua_xoa_du_lieu.Entities
{
    public class KhachHang
    {
        public int KhachHangId { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string SDT { get; set; }
        List<HoaDon>?hoaDons { get; set; }
    }
}
