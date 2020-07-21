using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINQ.ViewModel
{
    public class StudentVm
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public string MaLop { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set;}
        public int Tuoi { get; set; }
        public string NgaySinhStr
        {
            get
            {
                return NgaySinh.ToString("dd/MM/yyyy");
            }
        }
        public string GioiTinhStr
        {
            get
            {
                if (GioiTinh == true)
                {
                    return "Nữ";
                }
                else
                {
                    return "Nam";
                }
            }
        }
    }
}