using API_them_sua_xoa_du_lieu.Entities;
using API_them_sua_xoa_du_lieu.Helper;
using API_them_sua_xoa_du_lieu.Helper;

namespace API_them_sua_xoa_du_lieu.IServices
{
    public interface APIIServices
    {
        public HoaDon ThemHoaDon(HoaDon hoaDon);
        public HoaDon SuaHoaDon(HoaDon hoaDon);
        public void XoaHoaDon(int hoaDonId);
        public string TaoMaGiaoDich();
        IEnumerable<HoaDon> LayHoaDon(string keyword,
                                      int? year = null,
                                      int? month = null,
                                      DateTime? tuNgay = null,
                                      DateTime? denNgay = null,
                                      int? giaTu = null,
                                      int? giaDen = null
                                      );
    }
}
