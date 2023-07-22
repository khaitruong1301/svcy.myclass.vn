using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CinimaServer.Models.ViewModel
{
    public class CinimaRoomDetail
    {
        public int ShowTimeID { get;set;}
        public List<Seats> Seats { get; set; } 
    }
}