using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinimaServer.Models.ViewModel
{
    public class CinimaRooms
    {

        public string CinimaRoomID { get; set; }
        public string CinimaRoomName { get; set; }
        public Nullable<int> NumberOfSeats { get; set; }
        public string GroupID { get; set; }

    }
}