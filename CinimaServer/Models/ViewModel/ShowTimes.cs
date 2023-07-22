using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinimaServer.Models.ViewModel
{
    public class ShowTimes
    {
        public int ShowTimeID{ get; set; }
        public string CinimaRoomID { get; set; }
        public Nullable<int> MovieID { get; set; }
        public Nullable<System.TimeSpan> StartDate { get; set; }
        public Nullable<System.TimeSpan> EndDate { get; set; }
        public string Price { get; set; }
        public Nullable<System.DateTime> DateReady { get; set; }
        public string GroupID { get; set; }

        public virtual CinimaRooms CinimaRoom { get; set; }
    
    }
}