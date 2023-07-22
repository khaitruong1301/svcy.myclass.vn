using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using CinimaServer.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

using CinimaServer.Models;

using CinimaServer.Models.DBMenu;
using System.Threading.Tasks;
using CinimaServer.Models.DBMenu;
using Newtonsoft.Json;

namespace CinimaServer.Api
{
    public class QuanLyMenuController : ApiController
    {
        dbMenuFoodEntities db = new dbMenuFoodEntities();

        [HttpGet]
        public async Task<IHttpActionResult> LayDanhSachMenu()
        {
            IEnumerable<Menu> lst = db.Menus;
            List<MenuFood> lstResult = new List<MenuFood>();
            if (lst.Count() > 0 ) {

                MenuFood menuFood;
                foreach (var menu in lst) {
                    menuFood = new MenuFood();
                    menuFood.MaDanhMuc = menu.MaDanhMuc;
                    menuFood.TenDanhMuc = menu.TenDanhMuc;
                    menuFood.HinhAnh = menu.HinhAnh;
                    if (!string.IsNullOrEmpty(menu.DanhSachMonAn))
                    {
                        IEnumerable< FoodItem> lstItem = JsonConvert.DeserializeObject<IEnumerable< FoodItem>>(menu.DanhSachMonAn);

                        foreach (var item in lstItem)
                        {
                            menuFood.DanhSachMonAn.Add(item);
                        }

                    }
                    lstResult.Add(menuFood);
                }
           
            }
            return Ok(lstResult);
        } 


        [HttpPost]
        public async Task<IHttpActionResult> TaoMenu(MenuFood foodMenu)
        {
            Menu menuCheck = db.Menus.SingleOrDefault(n => n.MaDanhMuc == foodMenu.MaDanhMuc);
            if (menuCheck != null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Created);
                //response.Content = new StringContent("Mã danh mục đã tồn tại");
                //HttpResponseMessage mess =  Request.CreateResponse<string>(HttpStatusCode.Created);
                //mess. =   "Mã danh mục đã tồn tại !";

                return  Content(HttpStatusCode.InternalServerError,"MaDanhMuc đã tồn tại !");
            }
            Menu model = new Menu();
            model.MaDanhMuc = foodMenu.MaDanhMuc;
            model.TenDanhMuc = foodMenu.TenDanhMuc;
            model.HinhAnh = foodMenu.HinhAnh;
            if(foodMenu.DanhSachMonAn.Count()>0)
            {
                 model.DanhSachMonAn = JsonConvert.SerializeObject(foodMenu.DanhSachMonAn);
            }
            db.Menus.Add(model);
            db.SaveChanges();

            return Ok(foodMenu);

        }
        [HttpPut]
        public async Task<IHttpActionResult> CapNhatMenu(MenuFood foodMenu)
        {
            Menu model = db.Menus.SingleOrDefault(n => n.MaDanhMuc == foodMenu.MaDanhMuc);
            if (model == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Created);
                //response.Content = new StringContent("Mã danh mục đã tồn tại");
                //HttpResponseMessage mess =  Request.CreateResponse<string>(HttpStatusCode.Created);
                //mess. =   "Mã danh mục đã tồn tại !";

                return Content(HttpStatusCode.NotFound, "Không tìm thấy MaDanhMuc!");
            }
            //Menu model = new Menu();
            //model.MaDanhMuc = foodMenu.MaDanhMuc;
            model.TenDanhMuc = foodMenu.TenDanhMuc;
            model.HinhAnh = foodMenu.HinhAnh;
            if (foodMenu.DanhSachMonAn.Count() > 0)
            {
                model.DanhSachMonAn = JsonConvert.SerializeObject(foodMenu.DanhSachMonAn);
            }
            //db.Menus.Add(model);
            db.SaveChanges();

            return Ok(foodMenu);

        }

        [HttpGet]
        public async Task<IHttpActionResult> LayThongTinMenu(string maDanhMuc)
        {
            Menu model = db.Menus.SingleOrDefault(n => n.MaDanhMuc == maDanhMuc);
            if(model == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Created);
                //response.Content = new StringContent("Mã danh mục đã tồn tại");
                //HttpResponseMessage mess =  Request.CreateResponse<string>(HttpStatusCode.Created);
                //mess. =   "Mã danh mục đã tồn tại !";

                return Content(HttpStatusCode.NotFound, "Không tìm thấy MaDanhMuc!");
            }

            MenuFood menu = new MenuFood();
            menu.MaDanhMuc = model.MaDanhMuc;
            menu.TenDanhMuc = model.TenDanhMuc;
            menu.HinhAnh = model.HinhAnh;
            
            if (!string.IsNullOrEmpty(model.DanhSachMonAn)) { 
                menu.DanhSachMonAn = JsonConvert.DeserializeObject<List<FoodItem>>(model.DanhSachMonAn);
            }
            return Ok(menu);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string maDanhMuc)
        {
            Menu model = db.Menus.SingleOrDefault(n => n.MaDanhMuc == maDanhMuc);
            if (model == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Created);
                //response.Content = new StringContent("Mã danh mục đã tồn tại");
                //HttpResponseMessage mess =  Request.CreateResponse<string>(HttpStatusCode.Created);
                //mess. =   "Mã danh mục đã tồn tại !";

                return Content(HttpStatusCode.NotFound, "Không tìm thấy MaDanhMuc!");
            }
            //Menu model = new Menu();
            //model.MaDanhMuc = foodMenu.MaDanhMuc;
            db.Menus.Remove(model);
            //db.Menus.Add(model);
            db.SaveChanges();

            return Ok("Xóa thành công !");

        }






    }
}
