using System;
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
   
    public class SinhVienController : ApiController
    {
        private dbSinhVienEntities db = new dbSinhVienEntities();

        // GET: api/SinhVienApi
        [HttpGet]
        public IQueryable<SinhVien> LayDanhSachSinhVien()
        {
            return db.SinhViens;
        }

        // GET: api/SinhVien/5

        [HttpGet]
        [ResponseType(typeof(SinhVien))]
        public async Task<IHttpActionResult> LayThongTinSinhVien(string id)
        {
            SinhVien sinhVien = await db.SinhViens.SingleOrDefaultAsync(n=>n.MaSV==id);
            if (sinhVien == null)
            {
                return Content(HttpStatusCode.NotFound, "Không tìm thấy mã sinh viên!");

            }

            return Ok(sinhVien);
        }

        // PUT: api/SinhVien/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> CapNhatThongTinSinhVien( SinhVien sinhVien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Entry(sinhVien).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SinhVienExists(sinhVien.MaSV))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Content(HttpStatusCode.OK, "Cập nhật dữ liệu thành công!");

        }

        // POST: api/SinhVien
        [HttpPost]
        [ResponseType(typeof(SinhVien))]
        public async Task<IHttpActionResult> ThemSinhVien(SinhVien sinhVien)
        {
            if( string.IsNullOrEmpty(sinhVien.MaSV))
            {
                return Content(HttpStatusCode.InternalServerError, "Mã sinh viên không được bỏ trống!");

            }

            SinhVien sv = db.SinhViens.SingleOrDefault(n => n.MaSV == sinhVien.MaSV);
            if(sv!=null)
            {

                //HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return Content(HttpStatusCode.InternalServerError,"Mã sinh viên đã tồn tại!");
            }
            try
            {
                db.SinhViens.Add(sinhVien);
                db.SaveChanges();
                return Content(HttpStatusCode.OK, "Thêm sinh viên thành công!");
            }catch(Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Dữ liệu không hợp lệ!");
            }
        }
        [HttpDelete]

        // DELETE: api/SinhVien/5
        public  IHttpActionResult XoaSinhVien(string id)
        {
            SinhVien sinhVien =  db.SinhViens.SingleOrDefault(n => n.MaSV == id);
            if (sinhVien == null)
            {
                return Content(HttpStatusCode.NotFound, "Không tìm thấy mã sinh viên!");
            }

            db.SinhViens.Remove(sinhVien);
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
            return db.SinhViens.Count(e => e.MaSV == id) > 0;
        }
    }
}