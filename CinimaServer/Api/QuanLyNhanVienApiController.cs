using CinimaServer.Models;
using CinimaServer.Models.QLNV;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CinimaServer.Api
{
    public class QuanLyNhanVienApiController : ApiController
    {
        private QuanLyNhanVienEntities db = new QuanLyNhanVienEntities();

        [HttpGet]
        public IQueryable<NhanVien> LayDanhSachNhanVien()
        {
            return db.NhanViens;
        }


        [HttpGet]
        [ResponseType(typeof(NhanVien))]
        public async Task<IHttpActionResult> LayThongTinNhanVien(string maNhanVien)
        {
            int a = 0;
            bool b = int.TryParse(maNhanVien.ToString(),out a);

            if(!b)
            {
                return Content(HttpStatusCode.NotFound, "Mã nhân viên không hợp lệ!");


            }



            NhanVien nv = await db.NhanViens.SingleOrDefaultAsync(n => n.maNhanVien == a);
            if (nv == null)
            {
                return Content(HttpStatusCode.NotFound, "Không tìm thấy mã nhân viên!");

            }

            return Ok(nv);
        }

        // PUT: api/SinhVienApi/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> CapNhatThongTinNhanVien(string maNhanVien, NhanVien nv)
        {


            int maSv = 0;
            bool flag = int.TryParse(maNhanVien, out maSv);
            if (!flag || string.IsNullOrEmpty(maNhanVien))
            {
                Content(HttpStatusCode.InternalServerError, "Mã nhân viên không hợp lệ mã nhân viên phải là số!");

            }
            NhanVien sv = db.NhanViens.SingleOrDefault(n => n.maNhanVien == maSv);

            if (sv == null)
            {
                Content(HttpStatusCode.BadRequest, "Không tìm thấy nhân viên có mã " + maSv);
            }

            sv.tenNhanVien = nv.tenNhanVien;
            sv.chucVu = nv.chucVu;
            sv.heSoChucVu = nv.heSoChucVu;
            sv.luongCoBan = nv.luongCoBan;
            sv.soGioLamTrongThang = nv.soGioLamTrongThang;
            
            db.SaveChanges();


            return Content(HttpStatusCode.OK, "Cập nhật dữ liệu thành công!");

        }

        //// POST: api/SinhVien
        [HttpPost]
        [ResponseType(typeof(NhanVien))]
        public async Task<IHttpActionResult> ThemNhanVien(NhanVien nhanVien)
        {
            int maNhanVien = 0;
            bool flag = int.TryParse(nhanVien.maNhanVien.ToString(), out maNhanVien);
            if (string.IsNullOrEmpty(nhanVien.maNhanVien.ToString()) || !flag)
            {
                return Content(HttpStatusCode.InternalServerError, "Mã nhân viên không được bỏ trống và phải là số!");

            }


            NhanVien sv = db.NhanViens.SingleOrDefault(n => n.maNhanVien == nhanVien.maNhanVien);
            if (sv != null)
            {

                //HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return Content(HttpStatusCode.InternalServerError, "Mã sinh viên đã tồn tại!");
            }
            try
            {
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return Content(HttpStatusCode.OK, "Thêm nhân  viên thành công!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Dữ liệu không hợp lệ!");
            }
        }
        [HttpDelete]

        // DELETE: api/SinhVien/5
        public IHttpActionResult XoaNhanVien(string maSinhVien)
        {
            int maSv = 0;
            bool flag = int.TryParse(maSinhVien, out maSv);
            NhanVien sinhVien = db.NhanViens.SingleOrDefault(n => n.maNhanVien == maSv);
            if (string.IsNullOrEmpty(sinhVien.maNhanVien.ToString()) || !flag)
            {
                return Content(HttpStatusCode.InternalServerError, "Mã nhân viên không được bỏ trống và phải là số!");

            }
            if (sinhVien == null)
            {
                return Content(HttpStatusCode.NotFound, "Không tìm thấy mã nhân viên!");
            }

            db.NhanViens.Remove(sinhVien);
            db.SaveChanges();

            return Content(HttpStatusCode.OK, "Xóa nhân viên thành công!");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool SinhVienExists(string id)
        //{
        //    return db.SinhVienApis.Count(e => e.maSinhVien == int.Parse(id)) > 0;
        //}
    }
}