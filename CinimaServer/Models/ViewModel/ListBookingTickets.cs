using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinimaServer.Models.ViewModel
{
    public class ListBookingTickets
    {
        public string ShowTimeID { get; set; }
        public ICollection<Tickets> Tickets { get; set; }
        public DateTime DateView { get; set; }
        public string UserID { get; set; }
        public string GroupID { get; set; }
    }
}