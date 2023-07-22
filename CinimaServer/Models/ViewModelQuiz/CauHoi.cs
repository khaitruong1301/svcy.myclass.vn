using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinimaServer.Models.ViewModelQuiz
{
    public class CauHoi
    {
       public string LoaiCauHoi { get; set; }
       public int MaCauHoi { get; set; }
        
       public string NoiDungCauHoi { get; set; }

       public List<CauTraLoi> DanhSachCauTraLoi { get; set; }

    }
}