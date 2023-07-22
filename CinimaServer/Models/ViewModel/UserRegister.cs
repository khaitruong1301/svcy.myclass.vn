using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinimaServer.Models.ViewModel
{
    public class UserRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public DateTime? DateRegister { get; set; }
        public string GroupID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }

    }
}