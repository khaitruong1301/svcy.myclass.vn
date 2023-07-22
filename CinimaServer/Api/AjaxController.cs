using CinimaServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Xml;

namespace CinimaServer.Api
{
    public class AjaxController : ApiController
    {
        private QuanLyTrungTamEntities db = new QuanLyTrungTamEntities();
        // GET: Ajax
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            var lstNguoiDung = db.NguoiDungs.Select(n => new { n.TaiKhoan, n.MatKhau, n.HoTen, n.Email, n.SoDT });
            return Ok(lstNguoiDung);
        }
        [HttpPost]
        public IHttpActionResult AddUser(NguoiDung nguoidung)
        {
            if (db.NguoiDungs.Any(n => n.TaiKhoan == nguoidung.TaiKhoan))
            {
                return Ok("tai khoan da ton tai !");
            }
            nguoidung.MaLoaiNguoiDung = "HV";
            db.NguoiDungs.Add(nguoidung);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return Ok("Loi Server");
            }

            return Ok(nguoidung);
        }       
    }
}