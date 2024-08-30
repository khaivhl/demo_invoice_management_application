namespace API_them_sua_xoa_du_lieu.Entities
{
    public class Loaisanpham
    {
        public int LoaiSanPhamId { get; set; }
        public string TenLoaiSanPham { get; set; }
        List<SanPham>sanPhams { get; set; }
    }
}
