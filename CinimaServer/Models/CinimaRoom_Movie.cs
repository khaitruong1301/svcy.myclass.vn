//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinimaServer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CinimaRoom_Movie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CinimaRoom_Movie()
        {
            this.CinimaBookings = new HashSet<CinimaBooking>();
        }
    
        public int CinimaRoomMovieID { get; set; }
        public string CinimaRoomID { get; set; }
        public Nullable<int> MovieID { get; set; }
        public Nullable<System.TimeSpan> StartDate { get; set; }
        public Nullable<System.TimeSpan> EndDate { get; set; }
        public string Price { get; set; }
        public Nullable<System.DateTime> DateReady { get; set; }
        public string GroupID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CinimaBooking> CinimaBookings { get; set; }
        public virtual CinimaRoom CinimaRoom { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
