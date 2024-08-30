using API_them_sua_xoa_du_lieu.IServices;
using Microsoft.EntityFrameworkCore;
using API_them_sua_xoa_du_lieu.Context;
using API_them_sua_xoa_du_lieu.Entities;
using System.Runtime.Intrinsics.Arm;

namespace API_them_sua_xoa_du_lieu.Services
{
    public class APIServices : APIIServices
    {
        private readonly AppDbContext dbContext;

        public APIServices()
        {
            dbContext = new AppDbContext();
        }

        public HoaDon SuaHoaDon(HoaDon hoaDon)
        {
            using (var tran = dbContext.Database.BeginTransaction())
            {
                if (hoaDon.chiTietHoaDons == null || hoaDon.chiTietHoaDons.Count() == 0)//sua thanh 1 hoa don rong // nhu la xoa
                {
                    var lstCTHDHienTai = dbContext.ChiTietHoaDon.Where(x => x.HoaDonId == hoaDon.HoaDonId);
                    dbContext.RemoveRange(lstCTHDHienTai);//????????????
                    dbContext.SaveChanges();
                }
                else// chi tiet hoa don ko rong
                {
                    var lstCTHDHienTai = dbContext.ChiTietHoaDon.Where(x => x.HoaDonId == hoaDon.HoaDonId);// kiem trads chi tiet hoa don co id
                                                                                                           // trung vs hoa don them vao
                    var lstCTHDDelete = new List<ChiTietHoaDon>();
                    foreach (var chitiet in lstCTHDHienTai)
                    {
                        if (!hoaDon.chiTietHoaDons.Any(x => x.HoaDonId == chitiet.HoaDonId))//nhung cthd them vao ko trung voi cthd co san thi them 
                                                                                            //vao chi tiet hoa don
                        {
                            lstCTHDDelete.Add(chitiet);
                        }
                        else// con` cthd can them vao trung` thi
                        {
                            var chiTietMoi = hoaDon.chiTietHoaDons.FirstOrDefault(x => x.HoaDonId == chitiet.HoaDonId);//thay thong tin cthd ms vao` cai cu
                            chitiet.SanPhamId = chiTietMoi.SanPhamId;
                            chitiet.SoLuong = chiTietMoi.SoLuong;
                            chitiet.DVT = chiTietMoi.DVT;
                            var sanPham = dbContext.SanPham.Find(chiTietMoi.SanPhamId);
                            chitiet.ThanhTien = sanPham.GiaThanh * chiTietMoi.SoLuong;//sua thanh tien moi  
                            dbContext.Update(chitiet);
                            dbContext.SaveChanges();
                        }
                    }
                    dbContext.RemoveRange(lstCTHDDelete);
                    dbContext.SaveChanges();
                    foreach (var chiTiet in hoaDon.chiTietHoaDons)
                    {
                        if (!lstCTHDHienTai.Any(x => x.HoaDonId == chiTiet.HoaDonId))
                        {
                            chiTiet.HoaDonId = hoaDon.HoaDonId;
                        }
                    }
                }
                var tongTienMoi = dbContext.ChiTietHoaDon.Where(x => x.HoaDonId == hoaDon.HoaDonId).Sum(x => x.ThanhTien);
                hoaDon.TongTien = tongTienMoi;
                hoaDon.ThoiGianCapNhat = DateTime.Now;
                hoaDon.chiTietHoaDons = null;
                dbContext.Update(hoaDon);
                dbContext.SaveChanges();
                tran.Commit();
                return hoaDon;
            }
        }

        public void XoaHoaDon(int hoaDonId)
        {
            using (var tran = dbContext.Database.BeginTransaction())
            {

                if (dbContext.HoaDon.Any(x => x.HoaDonId == hoaDonId))
                {
                    var lstCTHDHienTai = dbContext.ChiTietHoaDon.Where(x => x.HoaDonId == hoaDonId);
                    dbContext.RemoveRange(lstCTHDHienTai);
                    dbContext.SaveChanges();
                    var hoaDon = dbContext.HoaDon.Find(hoaDonId);
                    dbContext.Remove(hoaDon);
                    dbContext.SaveChanges();
                    tran.Commit();
                }
                else
                {
                    tran.Rollback();
                    throw new Exception("hoa don khong ton tai");
                }
            }
        }

        public string TaoMaGiaoDich()
        {
            var res = DateTime.Now.ToString("yyyyMMdd");
            var countSoGiaoDichHomNay = dbContext.HoaDon.Count(x => x.ThoiGianTao.Date == DateTime.Now.Date);
            if (countSoGiaoDichHomNay > 0)
            {
                int tmp = countSoGiaoDichHomNay + 1;
                if (tmp < 10) return res + "00" + tmp.ToString();
                else if (tmp < 100) return res + "0" + tmp.ToString();
                else return res + tmp.ToString();
            }
            else return res + "001";
        }
        public HoaDon ThemHoaDon(HoaDon hoaDon)
        {
            using (var tran = dbContext.Database.BeginTransaction())
            {
              
                    hoaDon.MaGiaoDich = TaoMaGiaoDich();
                    hoaDon.ThoiGianTao = DateTime.Now;
                    var lstchiTietHoaDon = hoaDon.chiTietHoaDons;
                    hoaDon.chiTietHoaDons = null;
                    dbContext.Add(hoaDon);
                    dbContext.SaveChanges();
                    foreach (var chitiet in lstchiTietHoaDon)
                    {
                        if (dbContext.SanPham.Any(x => x.SanPhamId == chitiet.SanPhamId))
                        {
                            chitiet.HoaDonId = hoaDon.HoaDonId;
                            var sanPham = dbContext.SanPham.FirstOrDefault(x => x.SanPhamId == chitiet.SanPhamId);
                            chitiet.ThanhTien = chitiet.SoLuong * sanPham.GiaThanh;
                            dbContext.ChiTietHoaDon.Add(chitiet);
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            tran.Rollback();
                            throw new Exception("San pham khong ton tai moi them lai");
                        }
                    }
                    hoaDon.TongTien = lstchiTietHoaDon.Sum(x => x.ThanhTien);
                    dbContext.SaveChanges();
                    tran.Commit();
                    return hoaDon;
            }

        }

        public IEnumerable<HoaDon> LayHoaDon(string keyword, int? year = null,
                                             int? month = null,
                                             DateTime? tuNgay = null,
                                             DateTime? denNgay = null,
                                             int? giaTu = null,
                                             int? giaDen = null)
        {
            var hd = dbContext.HoaDon.Include(x=>x.chiTietHoaDons)
                                     .OrderByDescending(x => x.ThoiGianTao)
                                     .AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                hd = hd.Where(x => x.TenHangHoa.ToLower().Contains(keyword.ToLower())||x.MaGiaoDich.Contains(keyword.ToLower()));
            }
            if (year.HasValue)
            {
                hd = hd.Where(x => x.ThoiGianTao.Year == year);
            }
            if (month.HasValue)
            {
                hd = hd.Where(x => x.ThoiGianTao.Month == month);
            }
            if (tuNgay.HasValue)
            {
                hd = hd.Where(x => x.ThoiGianTao.Date >= tuNgay.Value.Date);
            
            }
            if (denNgay.HasValue)
            {
                hd = hd.Where(x => x.ThoiGianTao.Date <= denNgay.Value.Date);
            }
            if (giaTu.HasValue)
            {
                hd = hd.Where(x => x.TongTien >= giaTu);
            }
            if (year.HasValue)
            {
                hd = hd.Where(x => x.TongTien <= giaDen);
            }
            return hd; 
        }
    }
}
