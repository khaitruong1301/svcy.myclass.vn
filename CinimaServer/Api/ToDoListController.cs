using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Threading.Tasks;
using System.Web.Http;
using CinimaServer.Models;
using CinimaServer.Models.Task;
using CinimaServer.Tools;

namespace CinimaServer.Api
{
    public class ToDoListController : ApiController
    {
        private todolistEntities db = new todolistEntities();

        // GET: api/SinhVienApi
        [HttpGet]
        public  IHttpActionResult GetAllTask()
        {
            return Ok(db.Tasks.Select(n => new { taskName = n.taskName, status = n.status }));
        }

        [HttpGet]
        public async System.Threading.Tasks.Task<IHttpActionResult> GetTask(string taskName = "")
        {
            string alias = FuncUtilities.BestLower(taskName);

            Task task =  db.Tasks.SingleOrDefault(n => n.alias == alias);

            if(task == null)
            {
                return Content(HttpStatusCode.NotFound, "task name is not found !");
            }

            return Ok(task);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> AddTask(TaskModel taskModel)
        {
            string alias = FuncUtilities.BestLower(taskModel.taskName);

            Task task = db.Tasks.SingleOrDefault(n => n.alias == alias);

            if (task != null)
            {
                return Content(HttpStatusCode.InternalServerError, "task name is already exists!");
            }

            Task newTask = new Task();
            newTask.taskName = taskModel.taskName;
            newTask.alias = alias;
            newTask.status = false;

            db.Tasks.Add(newTask);
            db.SaveChanges();

            return Ok(new { taskName = newTask.taskName, status = newTask.status });
        }

        public class TaskModel
        {
            public string taskName { get; set; }
        }


        [HttpDelete]
        public async System.Threading.Tasks.Task<IHttpActionResult> deleteTask(string taskName = "")
        {
            string alias = FuncUtilities.BestLower(taskName);

            Task task = db.Tasks.SingleOrDefault(n => n.alias == alias);

            if (task == null)
            {
                return Content(HttpStatusCode.InternalServerError, "task name is not found !");
            }
            db.Tasks.Remove(task);
            db.SaveChanges();
            return Content(HttpStatusCode.OK, "task has been deleted!");
        }

        [HttpPut]
        public async System.Threading.Tasks.Task<IHttpActionResult> doneTask(string taskName = "")
        {
            string alias = FuncUtilities.BestLower(taskName);

            Task task = db.Tasks.SingleOrDefault(n => n.alias == alias);

            if (task == null)
            {
                return Content(HttpStatusCode.InternalServerError, "task name is not found !");
            }
            task.status = true;
            db.SaveChanges();
            return Content(HttpStatusCode.OK, "updated successfully !");
        }

        [HttpPut]
        public async System.Threading.Tasks.Task<IHttpActionResult> rejectTask(string taskName = "")
        {
            string alias = FuncUtilities.BestLower(taskName);

            Task task = db.Tasks.SingleOrDefault(n => n.alias == alias);

            if (task == null)
            {
                return Content(HttpStatusCode.InternalServerError, "task name is not found !");
            }
            task.status = false;
            db.SaveChanges();
            return Content(HttpStatusCode.OK, "updated successfully !");
        }


        //    // GET: api/SinhVien/5

        //    [HttpGet]
        //    [ResponseType(typeof(SinhVienApi))]
        //    public async Task<IHttpActionResult> LayThongTinSinhVien(int? maSinhVien)
        //    {
        //        SinhVienApi sinhVien = await db.SinhVienApis.SingleOrDefaultAsync(n => n.maSinhVien == maSinhVien);
        //        if (sinhVien == null)
        //        {
        //            return Content(HttpStatusCode.NotFound, "Không tìm thấy mã sinh viên!");

        //        }

        //        return Ok(sinhVien);
        //    }

        //    // PUT: api/SinhVienApi/5
        //    [HttpPut]
        //    [ResponseType(typeof(void))]
        //    public async Task<IHttpActionResult> CapNhatThongTinSinhVien(string maSinhVien, SinhVienApiModel sinhVien)
        //    {


        //        int maSv = 0;
        //        bool flag = int.TryParse(maSinhVien, out maSv);
        //        if (!flag || string.IsNullOrEmpty(maSinhVien))
        //        {
        //            Content(HttpStatusCode.BadRequest, "Mã sinh viên không hợp lệ! Mã sinh viên phải là số !");

        //        }
        //        SinhVienApi sv = db.SinhVienApis.SingleOrDefault(n => n.maSinhVien == maSv);

        //        if (sv == null)
        //        {
        //            Content(HttpStatusCode.BadRequest, "Không tìm thấy sinh viên có mã " + maSinhVien);
        //        }

        //        sv.tenSinhVien = sinhVien.tenSinhVien;
        //        sv.loaiSinhVien = sinhVien.loaiSinhVien;
        //        sv.email = sinhVien.email;
        //        sv.diemToan = sinhVien.diemToan;
        //        sv.diemLy = sinhVien.diemLy;
        //        sv.diemHoa = sinhVien.diemRenLuyen;
        //        sv.diemRenLuyen = sinhVien.diemRenLuyen;

        //        db.SaveChanges();


        //        return Content(HttpStatusCode.OK, "Cập nhật dữ liệu thành công!");

        //    }

        //    // POST: api/SinhVien
        //    [HttpPost]
        //    [ResponseType(typeof(SinhVienApi))]
        //    public async Task<IHttpActionResult> ThemSinhVien(SinhVienApi sinhVien)
        //    {
        //        int maSv = 0;
        //        bool flag = int.TryParse(sinhVien.maSinhVien.ToString(), out maSv);
        //        if (string.IsNullOrEmpty(sinhVien.maSinhVien.ToString()) || !flag)
        //        {
        //            return Content(HttpStatusCode.InternalServerError, "Mã sinh viên không được bỏ trống và phải là số!");

        //        }


        //        SinhVienApi sv = db.SinhVienApis.SingleOrDefault(n => n.maSinhVien == sinhVien.maSinhVien);
        //        if (sv != null)
        //        {

        //            //HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //            return Content(HttpStatusCode.InternalServerError, "Mã sinh viên đã tồn tại!");
        //        }
        //        try
        //        {
        //            db.SinhVienApis.Add(sinhVien);
        //            db.SaveChanges();
        //            return Content(HttpStatusCode.OK, "Thêm sinh viên thành công!");
        //        }
        //        catch (Exception ex)
        //        {
        //            return Content(HttpStatusCode.InternalServerError, "Dữ liệu không hợp lệ!");
        //        }
        //    }
        //    [HttpDelete]

        //    // DELETE: api/SinhVien/5
        //    public IHttpActionResult XoaSinhVien(string maSinhVien)
        //    {
        //        int maSv = 0;
        //        bool flag = int.TryParse(maSinhVien, out maSv);
        //        SinhVienApi sinhVien = db.SinhVienApis.SingleOrDefault(n => n.maSinhVien == maSv);
        //        if (string.IsNullOrEmpty(sinhVien.maSinhVien.ToString()) || !flag)
        //        {
        //            return Content(HttpStatusCode.InternalServerError, "Mã sinh viên không được bỏ trống và phải là số!");

        //        }
        //        if (sinhVien == null)
        //        {
        //            return Content(HttpStatusCode.NotFound, "Không tìm thấy mã sinh viên!");
        //        }

        //        db.SinhVienApis.Remove(sinhVien);
        //        db.SaveChanges();

        //        return Content(HttpStatusCode.OK, "Xóa sinh viên thành công!");

        //    }

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }

        //    private bool SinhVienExists(string id)
        //    {
        //        return db.SinhVienApis.Count(e => e.maSinhVien == int.Parse(id)) > 0;
        //    }
        //}
    }
}
