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
using CinimaServer.Models;
using CinimaServer.Models.ViewModel;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;

namespace CinimaServer.Api
{
    [AllowAnonymous]
    public class UserController : ApiController
    {
        private HomeCinemaEntities db = new HomeCinemaEntities();

        // GET: api/Users
        public HttpResponseMessage GetCustomers()
        {
            List<UserRegister> lstUser = new List<UserRegister>();
            var lstCustomer = db.Customers;
            foreach (var user in lstCustomer)
            {
                UserRegister cus = new UserRegister();

                cus.FirstName = user.FirstName;
                cus.LastName = user.LastName;
                cus.Email = user.Email;
                cus.DateOfBirth = user.DateOfBirth;
                cus.Phone = user.Mobile;
                cus.DateRegister = user.RegistrationDate;
                cus.GroupID = user.GroupID;
                cus.UserName = user.UserName;
                cus.PassWord = user.PassWord;
                lstUser.Add(cus);
            }
            var respone = Request.CreateResponse(HttpStatusCode.OK, lstUser);
            return respone;
        }

        public HttpResponseMessage GetUser(string groupID)
        {
            List<UserRegister> lstUser = new List<UserRegister>();
            var lstCustomer = db.Customers.Where(n => n.GroupID.ToLower().Contains(groupID.ToLower()));
            foreach (var user in lstCustomer)
            {
                UserRegister cus = new UserRegister();
                
                cus.FirstName = user.FirstName;
                cus.LastName = user.LastName;
                cus.Email = user.Email;
                cus.DateOfBirth = user.DateOfBirth;
                cus.Phone = user.Mobile;
                cus.DateRegister = user.RegistrationDate;
                cus.GroupID = user.GroupID;
                cus.UserName = user.UserName;
                cus.PassWord = user.PassWord;
                lstUser.Add(cus);
            }
            var respone = Request.CreateResponse(HttpStatusCode.OK, lstUser);
            return respone;
        }




      

        [HttpPost]
        [AcceptVerbs()]
        
        public IHttpActionResult RegisterUser(JsonData data)
        {
            Customer cus = new Customer();
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var user = parser.Deserialize<UserRegister>(data.data);
            cus.FirstName = user.FirstName;
            cus.LastName = user.LastName;
            cus.Email = user.Email;
            cus.DateOfBirth = user.DateOfBirth;
            cus.Mobile = user.Phone;
            cus.RegistrationDate = DateTime.Now ;         
            cus.GroupID = user.GroupID;
            cus.UserName = user.UserName;
            cus.PassWord = user.PassWord;
            var usercheck = db.Customers.SingleOrDefault(n => n.UserName == user.UserName && n.GroupID == user.GroupID);
            if (!ModelState.IsValid || usercheck != null)
            {
                return Ok("Username already exists");/* BadRequest(ModelState);*/
            }
            db.Customers.Add(cus);
            db.SaveChanges();
            return Ok(user);
        }


        [HttpPost]
        [AcceptVerbs()]
     
        public IHttpActionResult Login(JsonData data)
        {
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var user = parser.Deserialize<UserLogin>(data.data);
            Customer cus = db.Customers.SingleOrDefault(n => n.UserName.Trim() == user.UserName.Trim() && n.PassWord.Trim() == user.PassWord.Trim() && n.GroupID.Trim() == user.GroupID.Trim());

            if (cus != null)
            {
                UserLogin us = new UserLogin();
                us.UserName = cus.UserName;
                us.PassWord = cus.PassWord;
                us.Status = true;
                us.GroupID = cus.GroupID;
                us.Email = cus.Email;
                us.FullName = cus.FirstName + cus.LastName;
                return Ok(us);
            }
            else
            {
                return Ok("The account or password is incorrect");
            }
           
        }

        [HttpPost]
        [AcceptVerbs()]
        public IHttpActionResult RegisterUserFrontEnd(JsonData data)
        {
            Customer cus = new Customer();
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var user = parser.Deserialize<UserRegister>(data.data);
            cus.UserName = user.UserName;
            cus.FirstName = user.FirstName;
            cus.LastName = user.LastName;
            cus.Email = user.Email;
            cus.GroupID = "FrontEndXX";
            var usercheck = db.Customers.SingleOrDefault(n => n.UserName == user.UserName && n.GroupID == "FrontEndXX");
            if (!ModelState.IsValid || usercheck != null)
            {
                return Ok("Username already exists");/* BadRequest(ModelState);*/
            }
            db.Customers.Add(cus);
            db.SaveChanges();
            //Trả về danh sách user của csdl
            List<USERFRONTENDXX> lstUser1 = new List<USERFRONTENDXX>();
            var lstCustomer = db.Customers.Where(n => n.GroupID == "FrontEndXX");
            foreach (var user1 in lstCustomer)
            {
                USERFRONTENDXX cus1 = new USERFRONTENDXX();

                cus1.FirstName = user1.FirstName;
                cus1.LastName = user1.LastName;
                cus1.Email = user1.Email;
               
                lstUser1.Add(cus1);
            }
            var respone = Request.CreateResponse(HttpStatusCode.OK, lstUser1);
            return Ok(lstUser1);
          
        }
      



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.ID == id) > 0;
        }

    }
}
