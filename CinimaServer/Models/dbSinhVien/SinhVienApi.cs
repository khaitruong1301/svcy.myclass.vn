//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CinimaServer.Models.dbSinhVien
{
    using System;
    using System.Collections.Generic;
    
    public partial class SinhVienApi
    {
        public int maSinhVien { get; set; }
        public string tenSinhVien { get; set; }
        public string loaiSinhVien { get; set; }
        public Nullable<int> diemToan { get; set; }
        public Nullable<int> diemLy { get; set; }
        public Nullable<int> diemHoa { get; set; }
        public Nullable<int> diemRenLuyen { get; set; }
        public string email { get; set; }
        public string soDienThoai { get; set; }
    }
}
