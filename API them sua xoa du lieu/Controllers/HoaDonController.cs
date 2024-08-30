using API_them_sua_xoa_du_lieu.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_them_sua_xoa_du_lieu.Services;
using API_them_sua_xoa_du_lieu.Entities;
using API_them_sua_xoa_du_lieu.Helper;

namespace API_them_sua_xoa_du_lieu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly APIIServices services;

        public HoaDonController()
        {
            services = new APIServices();
        }
        [HttpPost]
        public IActionResult ThemHoaDon([FromBody]HoaDon hoaDon)
        {
            var res = services.ThemHoaDon(hoaDon);
            return Ok(res);
        } 
        [HttpPut]
        public IActionResult SuaHoaDon([FromBody]HoaDon hoaDon)
        {
            var res = services.SuaHoaDon(hoaDon);
            return Ok(res); 
        }
        [HttpDelete]
        public IActionResult XoaHoaDon([FromQuery]int hoaDonId)
        {
            services.XoaHoaDon(hoaDonId);
            return Ok(); 
        }
        [HttpGet]
        public IActionResult LayDuLieuHoaDon([FromQuery] string? keyword,
                                             [FromQuery] int? year = null,
                                             [FromQuery] int? month = null,
                                             [FromQuery] DateTime? tuNgay = null,
                                             [FromQuery] DateTime? denNgay = null,
                                             [FromQuery] int? giaTu = null,
                                             [FromQuery] int? giaDen = null,
                                             [FromQuery] Pagination pagination=null
                                             )
        {
            var dshoadon=services.LayHoaDon(keyword
                                        ,year
                                        ,month
                                        ,tuNgay
                                        ,denNgay
                                        ,giaTu
                                        ,giaDen
                                        );
            var hoaDon = PageResult<HoaDon>.ToPageResult(pagination, dshoadon);
            pagination.TotalCount = dshoadon.Count();
            var res = new PageResult<HoaDon>(pagination, hoaDon);
            return Ok(res);
        }
    }
}
