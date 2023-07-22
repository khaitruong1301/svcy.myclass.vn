using CinimaServer.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace CinimaServer.Api
{

    public class QuanLyTrungTamController : ApiController
    {
        
        private QuanLyTrungTamEntities db = new QuanLyTrungTamEntities();

        // GET: api/QuanLyTrungTam
        [HttpGet]
        public IQueryable<object> DanhSachKhoaHoc()
        {
            return db.KhoaHocs.Select(n => new { MaKhoaHoc = n.MaKhoaHoc, n.TenKhoaHoc, n.LuotXem, n.MoTa, n.HinhAnh, NguoiTao = n.NguoiTao });
    
        }

        [HttpGet]
        // GET: api/QuanLyTrungTam/5
        [ResponseType(typeof(KhoaHoc))]
        public async Task<IHttpActionResult> ChiTietKhoaHoc(string id)
        {
            var khoaHoc = await db.KhoaHocs.Select(n => new { MaKhoaHoc = n.MaKhoaHoc, n.TenKhoaHoc, n.LuotXem, n.HinhAnh, n.MoTa, NguoiTao = n.NguoiDung.HoTen }).SingleOrDefaultAsync(n => n.MaKhoaHoc == id);
            if (khoaHoc == null)
            {
                return NotFound();
            }

            return Ok(khoaHoc);
        }

        [HttpPut]
        // PUT: api/QuanLyTrungTam/5
        [ResponseType(typeof(void))]
        public IHttpActionResult CapNhatKhoaHoc(KhoaHocVM khoaHoc)
        {
            KhoaHoc kh = db.KhoaHocs.SingleOrDefault(n => n.MaKhoaHoc.ToLower().Trim() == khoaHoc.MaKhoaHoc.ToLower().Trim());
            if (kh == null)
            {
                return Ok("Khong tim thay!");
            }
            kh.TenKhoaHoc = khoaHoc.TenKhoaHoc;
            kh.MoTa = khoaHoc.MoTa;
            kh.LuotXem = khoaHoc.LuotXem;
            kh.HinhAnh = khoaHoc.HinhAnh;
            
            if (!string.IsNullOrEmpty(khoaHoc.NguoiTao))
            {
                kh.NguoiTao = khoaHoc.NguoiTao;
            }
            db.SaveChanges();
            return Ok(khoaHoc);
        }

        [HttpPost]
        // POST: api/QuanLyTrungTam
        [ResponseType(typeof(KhoaHocVM))]
        public IHttpActionResult ThemKhoaHoc(KhoaHocVM KhoaHoc)
        { 
            KhoaHoc kh = new KhoaHoc();
            kh.MaKhoaHoc = KhoaHoc.MaKhoaHoc;
            kh.TenKhoaHoc = KhoaHoc.TenKhoaHoc;
            kh.MoTa = KhoaHoc.MoTa;
            kh.LuotXem = KhoaHoc.LuotXem;
            kh.NguoiTao = KhoaHoc.NguoiTao;
            kh.HinhAnh = KhoaHoc.HinhAnh;
            db.KhoaHocs.Add(kh);
            db.SaveChanges();
            return Ok(true);
        }

        [HttpDelete]
        // DELETE: api/QuanLyTrungTam/5
        [ResponseType(typeof(KhoaHoc))]
        public IHttpActionResult XoaKhoaHoc(string id)
        {
            KhoaHoc khoaHoc = db.KhoaHocs.SingleOrDefault(n => n.MaKhoaHoc == id);
            if (khoaHoc == null)
            {
                return NotFound();
            }
            var lstGhiDanh = db.HocVienKhoaHocs.Where(n => n.MaKhoaHoc == id);
            if (lstGhiDanh.Count()>0)
            {
                db.HocVienKhoaHocs.RemoveRange(lstGhiDanh);
                db.SaveChanges();
            }
            db.KhoaHocs.Remove(khoaHoc);
            db.SaveChanges();

            return Ok(khoaHoc);
        }

        /// <summary>
        /// Lấy danh sách sinh viên từ server
        /// </summary>
        //Lấy danh sách người dùng hoc vien
        [HttpGet]
        public IQueryable<object> DanhSachHocvien()
        {
            return db.NguoiDungs.Where(n => n.MaLoaiNguoiDung == LoaiNguoiDungHT.HocVien).Select(n => new { n.TaiKhoan, n.HoTen, n.Email, n.SoDT, n.MaLoaiNguoiDung, n.LoaiNguoiDung.TenLoaiNguoiDung });
        }

        [HttpGet]
        public IQueryable<object> DanhSachNguoiDung()
        {
            var lstNguoiDung = db.NguoiDungs.Select(n => new { n.TaiKhoan,n.MatKhau, n.HoTen, n.Email, n.SoDT, n.MaLoaiNguoiDung, n.LoaiNguoiDung.TenLoaiNguoiDung });
            return lstNguoiDung;
        }
       

        [HttpGet]
        public IQueryable<object> ThongTinNguoiDung(string taikhoan)
        {
            return db.NguoiDungs.Where(n => n.MaLoaiNguoiDung == LoaiNguoiDungHT.HocVien && n.TaiKhoan == taikhoan).Select(n => new { n.TaiKhoan, n.HoTen, n.Email, n.SoDT, n.MaLoaiNguoiDung, n.LoaiNguoiDung.TenLoaiNguoiDung });
        }

        [HttpPost]
        // POST: api/QuanLyTrungTam
        [ResponseType(typeof(NguoiDung))]
        public IHttpActionResult ThemNguoiDung(NguoiDung nguoidung)
        {

            if (db.NguoiDungs.Any(n => n.TaiKhoan == nguoidung.TaiKhoan))
            {
                return Ok("tai khoan da ton tai !");
            }

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

        [HttpPut]
        // PUT: api/QuanLyTrungTam/5
        [ResponseType(typeof(void))]
        public IHttpActionResult CapNhatThongTinNguoiDung(NguoiDung nguoidung)
        {
            NguoiDung nd = db.NguoiDungs.SingleOrDefault(n => n.TaiKhoan.ToLower().Trim() == nguoidung.TaiKhoan.ToLower().Trim());
            if (nd == null)
            {
                return NotFound();
            }
            nd.HoTen = nguoidung.HoTen;
            nd.MatKhau = nguoidung.MatKhau;
            nd.SoDT = nguoidung.SoDT;
            nd.MaLoaiNguoiDung = nguoidung.MaLoaiNguoiDung;
            nd.Email = nguoidung.Email;
            db.SaveChanges();
            return Ok(nguoidung);
        }

        [HttpDelete]
        // DELETE: api/QuanLyTrungTam/5

        public IHttpActionResult XoaNguoiDung(string id)
        {
            NguoiDung nguoidung = db.NguoiDungs.SingleOrDefault(n => n.TaiKhoan == id);
            if (nguoidung == null)
            {
                return NotFound();
            }
            var lstGhiDanh = db.HocVienKhoaHocs.Where(n => n.TaiKhoan == id);
            if (lstGhiDanh.Count() > 0)
            {
                db.HocVienKhoaHocs.RemoveRange(lstGhiDanh);
                db.SaveChanges();
            }
            db.NguoiDungs.Remove(nguoidung);
            db.SaveChanges();

            return Ok(nguoidung);
        }

        //dang ky dang nhap dangky dangnhap
        [HttpPost]
        // POST: api/QuanLyTrungTam
        [ResponseType(typeof(NguoiDung))]
        public IHttpActionResult DangKy(NguoiDung nguoidung)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //     public string TaiKhoan { get; set; }
            //public string MatKhau { get; set; }
            //public string HoTen { get; set; }
            //public string Email { get; set; }
            //public string SoDT { get; set; }
            //public string MaLoaiNguoiDung { get; set; }
            try
            {
                nguoidung.MaLoaiNguoiDung = "HV";
                db.NguoiDungs.Add(nguoidung);
                db.SaveChanges();
                return Ok(true);
            }
            catch  {
                return Ok(false);
            }
        

        }

        [HttpGet]
        public IHttpActionResult DangNhap(string taikhoan, string matkhau)
        {
            var nguoidung = db.NguoiDungs.Where(n => n.TaiKhoan.ToLower() == taikhoan.ToLower().Trim() && n.MatKhau.ToLower().Trim() == matkhau.ToLower().Trim()).Select(n => new { n.TaiKhoan, n.HoTen, n.Email, n.SoDT, TenLoaiNguoiDung = n.LoaiNguoiDung.TenLoaiNguoiDung, MaLoaiNguoiDung = n.MaLoaiNguoiDung });
            if (nguoidung.Count() == 0)
            {
                return Ok("failed to login");
            }
            return Ok(nguoidung);
        }

        [HttpPost]
        // POST: api/QuanLyTrungTam
        [ResponseType(typeof(GhiDanh))]
        public IHttpActionResult GhiDanhKhoaHoc(GhiDanh ghidanh)
        {
            NguoiDung nguoidung = db.NguoiDungs.SingleOrDefault(n => n.TaiKhoan == ghidanh.TaiKhoan);
            KhoaHoc khoahoc = db.KhoaHocs.SingleOrDefault(n => n.MaKhoaHoc == ghidanh.MaKhoaHoc);

            if (nguoidung == null || khoahoc == null)
            {
                return Ok("Failure");
            }
            HocVienKhoaHoc hvkh = new HocVienKhoaHoc();
            hvkh.MaKhoaHoc = ghidanh.MaKhoaHoc;
            hvkh.TaiKhoan = ghidanh.TaiKhoan;
            hvkh.NgayGhiDanh = DateTime.Now;
            db.HocVienKhoaHocs.Add(hvkh);
            db.SaveChanges();
            return Ok("Sucessfully");
        }

        //Xem danh sách khóa học của người dùng
        [HttpGet]
        // POST: api/QuanLyTrungTam
        [ResponseType(typeof(GhiDanh))]
        public IHttpActionResult LayThongtinKhoaHoc(string taikhoan)
        {
            var hvkh = db.HocVienKhoaHocs.Where(n => n.TaiKhoan == taikhoan).Select(n => new { n.MaKhoaHoc, n.KhoaHoc.TenKhoaHoc, n.NgayGhiDanh, GiaoVu = n.NguoiDung.HoTen,n.KhoaHoc.HinhAnh,n.KhoaHoc.MoTa });
            if (hvkh.Count() == 0)
            {
                return Ok("Did not find the course");
            }
            return Ok(hvkh);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KhoaHocExists(string id)
        {
            return db.KhoaHocs.Count(e => e.MaKhoaHoc == id) > 0;
        }
    }

    public static class LoaiNguoiDungHT
    {
        public static string HocVien = "HV";
        public static string GiaoVu = "GV";
    }

    public class TaiKhoanDN
    {
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }

        private TaiKhoanDN()
        {
        }
    }

    public class GhiDanh
    {
        public string TaiKhoan { get; set; }
        public string MaKhoaHoc { get; set; }

        private GhiDanh()
        {
        }
    }

    public class KhoaHocVM
    {
        public string MaKhoaHoc { get; set; }
        public string TenKhoaHoc { get; set; }
        public string MoTa { get; set; }
        public Nullable<int> LuotXem { get; set; }
        public string NguoiTao { get; set; }
        public string HinhAnh { get; set; }
    }
}