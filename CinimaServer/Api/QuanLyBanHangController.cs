using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CinimaServer.Models;

namespace CinimaServer.Api
{
    public class QuanLyBanHangController : ApiController
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        [HttpGet]
        public IQueryable<object> DanhSachSanPham()
        {
            return db.SanPhams.Select(n => new { MaSP = n.MaSP, n.TenSP, n.DonGia,HinhAnh = "http://sv.myclass.vn/images/sanpham/"+n.HinhAnh, n.SoLuongTon }).Take(20);
        }


    }
}
