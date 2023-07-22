namespace CinimaServer.Models.dbSinhVien
{
    public class SinhVienApiModel
    {
        public string maSinhVien { get; set; }
        public string tenSinhVien { get; set; }
        public string loaiSinhVien { get; set; }
        public int diemToan { get; set; }
        public int diemLy { get; set; }
        public int diemHoa { get; set; }
        public int diemRenLuyen { get; set; }
        public string email { get; set; }
        public string soDienThoai { get; set; }
    }
}