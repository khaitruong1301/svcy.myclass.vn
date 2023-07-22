using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinimaServer.Models.ViewModel
{
    public class Seats
    {
        public string SeatID { get; set; }
        public string UserName {get;set;}
        public bool Status { get; set; }
        public string SeatName { get; set; }
        public string SeatType { get; set; }
        public string CinimaRoomID { get; set; }
        public Nullable<int> NumOrder { get; set; }
        public string GroupID { get; set; }
        public int Price { get; set; }
    }
}