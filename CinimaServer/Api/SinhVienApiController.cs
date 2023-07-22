﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CinimaServer.Models.dbSinhVien;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Swashbuckle.Swagger;
using System.Web.Http.Controllers;


namespace CinimaServer.Api
{
    public class SinhVienApiController : ApiController
    {
        private dbSinhVienEntities db = new dbSinhVienEntities();

        // GET: api/SinhVienApi
        [HttpGet]
        public IQueryable<SinhVienApi> LayDanhSachSinhVien()
        {
            return db.SinhVienApis;
        }

        // GET: api/SinhVien/5

        [HttpGet]
        [ResponseType(typeof(SinhVienApi))]
        public async Task<IHttpActionResult> LayThongTinSinhVien(int? maSinhVien)
        {
            SinhVienApi sinhVien = await db.SinhVienApis.SingleOrDefaultAsync(n => n.maSinhVien == maSinhVien);
            if (sinhVien == null)
            {
                return Content(HttpStatusCode.NotFound, "Không tìm thấy mã sinh viên!");

            }

            return Ok(sinhVien);
        }

        // PUT: api/SinhVienApi/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> CapNhatThongTinSinhVien(string maSinhVien, SinhVienApiModel sinhVien)
        {


            int maSv = 0;
            bool flag = int.TryParse(maSinhVien, out maSv);
            if (!flag || string.IsNullOrEmpty(maSinhVien))
            {
                Content(HttpStatusCode.InternalServerError, "Mã sinh viên không hợp lệ! Mã sinh viên phải là số !");

            }
            SinhVienApi sv = db.SinhVienApis.SingleOrDefault(n => n.maSinhVien == maSv);

            if (sv == null)
            {
                Content(HttpStatusCode.BadRequest, "Không tìm thấy sinh viên có mã " + maSinhVien);
            }

            sv.tenSinhVien = sinhVien.tenSinhVien;
            sv.loaiSinhVien = sinhVien.loaiSinhVien;
            sv.email = sinhVien.email;
            sv.diemToan = sinhVien.diemToan;
            sv.diemLy = sinhVien.diemLy;
            sv.diemHoa = sinhVien.diemRenLuyen;
            sv.diemRenLuyen = sinhVien.diemRenLuyen;
            sv.soDienThoai = sinhVien.soDienThoai;
            db.SaveChanges();


            return Content(HttpStatusCode.OK, "Cập nhật dữ liệu thành công!");

        }

        // POST: api/SinhVien
        [HttpPost]
        [ResponseType(typeof(SinhVienApi))]
        public async Task<IHttpActionResult> ThemSinhVien(SinhVienApi sinhVien)
        {
            int maSv = 0;
            bool flag = int.TryParse(sinhVien.maSinhVien.ToString(), out maSv);
            if (string.IsNullOrEmpty(sinhVien.maSinhVien.ToString()) || !flag)
            {
                return Content(HttpStatusCode.InternalServerError, "Mã sinh viên không được bỏ trống và phải là số!");

            }


            SinhVienApi sv = db.SinhVienApis.SingleOrDefault(n => n.maSinhVien == sinhVien.maSinhVien);
            if (sv != null)
            {

                //HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return Content(HttpStatusCode.InternalServerError, "Mã sinh viên đã tồn tại!");
            }
            try
            {
                db.SinhVienApis.Add(sinhVien);
                db.SaveChanges();
                return Content(HttpStatusCode.OK, "Thêm sinh viên thành công!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Dữ liệu không hợp lệ!");
            }
        }
        [HttpDelete]

        // DELETE: api/SinhVien/5
        public IHttpActionResult XoaSinhVien(string maSinhVien)
        {
            int maSv = 0;
            bool flag = int.TryParse(maSinhVien, out maSv);
            SinhVienApi sinhVien = db.SinhVienApis.SingleOrDefault(n => n.maSinhVien == maSv);
            if (string.IsNullOrEmpty(sinhVien.maSinhVien.ToString()) || !flag)
            {
                return Content(HttpStatusCode.InternalServerError, "Mã sinh viên không được bỏ trống và phải là số!");

            }
            if (sinhVien == null)
            {
                return Content(HttpStatusCode.NotFound, "Không tìm thấy mã sinh viên!");
            }

            db.SinhVienApis.Remove(sinhVien);
            db.SaveChanges();

            return Content(HttpStatusCode.OK, "Xóa sinh viên thành công!");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SinhVienExists(string id)
        {
            return db.SinhVienApis.Count(e => e.maSinhVien == int.Parse(id) )> 0;
        }
    }
}