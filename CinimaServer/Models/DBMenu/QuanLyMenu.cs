using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinimaServer.Models.DBMenu
{
    public class MenuFood
    {
        public string MaDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public string HinhAnh { get; set; }
        public List<FoodItem> DanhSachMonAn = new List<FoodItem>();

    }

    public class FoodItem
    {
        public string MaMonAn { get; set; }
        public string TenMonAn { get; set; }
        public string HinhAnh { get; set; }
        public string Gia { get; set; }
    }
}