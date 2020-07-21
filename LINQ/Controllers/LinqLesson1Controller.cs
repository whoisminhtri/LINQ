using LINQ.Models;
using LINQ.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LINQ.Controllers
{
    public class LinqLesson1Controller : Controller
    {
        PracticeSQLEntities db = new PracticeSQLEntities();
        // GET: LinqLesson1
        //1. Sql khong ket noi
        public ActionResult Index()
        {
            return View();
        }
        //1. Hiển thị danh sách gồm: MaSV, HoTen, MaLop, NgaySinh
        //(dd/mm/yyyy), GioiTinh(Nam, Nữ) , Namsinh của những sinh viên có họ tên
        //không bắt đầu bằng chữ L.
        public JsonResult Bai1()
        {
            var query = (from sv in db.DMSINHVIENs
                         where !sv.HoTen.Contains("L")
                         select new StudentVm
                         {
                             MaSV = sv.MaSV,
                             HoTen = sv.HoTen,
                             MaLop = sv.MaLop,
                             NgaySinh = sv.NgaySinh.Value,
                             GioiTinh = sv.GioiTinh.Value
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        //        2. Hiển thị danh sách gồm: MaSV, HoTen, MaLop, NgaySinh
        //(dd/mm/yyyy), GioiTinh(Nam, Nữ) , Namsinh của những sinh viên nam
        //học lớp CT11.
        public JsonResult Bai2()
        {
            var query = (from sv in db.DMSINHVIENs
                         where sv.GioiTinh == false && sv.MaLop == "CT11"
                         select new StudentVm
                         {
                             MaSV = sv.MaSV,
                             HoTen = sv.HoTen,
                             MaLop = sv.MaLop,
                             NgaySinh = sv.NgaySinh.Value,
                             GioiTinh = sv.GioiTinh.Value
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        //3. Hiển thị danh sách gồm: MaSV, HoTen, MaLop, NgaySinh
        //(dd/mm/yyyy), GioiTinh(Nam, Nữ) của những sinh viên học lớp
        //CT11,CT12,CT13.
        public JsonResult Bai3()
        {
            var query = (from sv in db.DMSINHVIENs
                         where sv.MaLop == "CT11" || sv.MaLop == "CT12" || sv.MaLop == "CT13"
                         select new StudentVm
                         {
                             MaSV = sv.MaSV,
                             HoTen = sv.HoTen,
                             MaLop = sv.MaLop,
                             NgaySinh = sv.NgaySinh.Value,
                             GioiTinh = sv.GioiTinh.Value
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        //4. Hiển thị danh sách gồm: MaSV, HoTen, MaLop, NgaySinh
        //(dd/mm/yyyy), GioiTinh(Nam, Nữ), Tuổi của những sinh viên có tuổi từ
        //19-21.
        public JsonResult Bai4()
        {
            var query = (from sv in db.DMSINHVIENs
                         where (DateTime.Now.Year - sv.NgaySinh.Value.Year) >= 19 && (DateTime.Now.Year - sv.NgaySinh.Value.Year) <= 21
                         select new StudentVm
                         {
                             MaSV = sv.MaSV,
                             HoTen = sv.HoTen,
                             MaLop = sv.MaLop,
                             NgaySinh = sv.NgaySinh.Value,
                             GioiTinh = sv.GioiTinh.Value,
                             Tuoi = DateTime.Now.Year - sv.NgaySinh.Value.Year
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        //2. SQL CO KET NOI
        //1. Hiển thị danh sách gồm MaSV, HoTên, MaLop, DiemHP, MaHP của
        //  những sinh viên có điểm HP >= 5.
        public JsonResult Bai5()
        {
            var query = (from diem in db.DMDIEMHPs
                         join sv in db.DMSINHVIENs
                         on diem.MaSV equals sv.MaSV
                         where diem.DiemHP.Value >= 5.0
                         select new
                         {
                             MaSV = sv.MaSV,
                             HoTen = sv.HoTen,
                             DiemHP = diem.DiemHP,
                             MaHP = diem.MaHP
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        //2. Hiển thị danh sách MaSV, HoTen , MaLop, MaHP, DiemHP được sắp
        //xếp theo ưu tiên Mã lớp, Họ tên tăng dần.
        public JsonResult Bai6()
        {
            var query = (from diem in db.DMDIEMHPs
                         join sv in db.DMSINHVIENs
                         on diem.MaSV equals sv.MaSV
                         orderby sv.MaLop, sv.HoTen ascending
                         select new
                         {
                             MaSV = sv.MaSV,
                             HoTen = sv.HoTen,
                             MaLop = sv.MaLop,
                             DiemHP = diem.DiemHP,
                             MaHP = diem.MaHP
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        //3. Hiển thị danh sách gồm MaSV, HoTen, MaLop, DiemHP, MaHP của
        //những sinh viên có điểm HP từ 5 đến 7 ở học kỳ I.
        public JsonResult Bai7()
        {
            var query = (from diem in db.DMDIEMHPs
                         join sv in db.DMSINHVIENs
                         on diem.MaSV equals sv.MaSV
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         where (diem.DiemHP >= 5 && diem.DiemHP <= 7) && hp.HocKy == 1
                         select new
                         {
                             MaSV = sv.MaSV,
                             HoTen = sv.HoTen,
                             MaLop = sv.MaLop,
                             DiemHP = diem.DiemHP,
                             MaHP = diem.MaHP
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        //4. Hiển thị danh sách sinh viên gồm MaSV, HoTen, MaLop, TenLop,
        //MaKhoa của Khoa có mã CNTT.
        public JsonResult Bai8()
        {
            var query = (from sv in db.DMSINHVIENs
                         join lop in db.DMLOPs
                         on sv.MaLop equals lop.MaLop
                         join ng in db.DMNGANHs
                         on lop.MaNganh equals ng.MaNganh
                         where ng.MaKhoa == "CNTT"
                         select new
                         {
                             MaSV = sv.MaSV,
                             HoTen = sv.HoTen,
                             MaLop = lop.MaLop,
                             MaKhoa = ng.MaKhoa,
                             TenLop = lop.TenLop,
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        // CAU LENH TRUY VAN CO PHAN NHOM
        //1. Cho biết MaLop, TenLop, tổng số sinh viên của mỗi lớp.
        public JsonResult Bai9()
        {
            var query = (from sv in db.DMSINHVIENs
                         group sv by sv.DMLOP into lop
                         select new
                         {
                             MaLop = lop.Key.MaLop,
                             TenLop = lop.Key.TenLop,
                             SoSinhVien = lop.Count()
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        //2. Cho biết điểm trung bình chung của mỗi sinh viên, xuất ra bảng mới có
        //tên DIEMTBC, biết rằng công thức tính DiemTBC như sau:
        //DiemTBC =  (DiemHP* SoDvht) /  (SoDvht)
        public JsonResult Bai10()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         group diem by diem.DMSINHVIEN into test
                         select new
                         {
                             HoTen = test.Key.HoTen,
                             //DiemTBC = test.Sum()/db.DMDIEMHPs.Where(x=>x.MaSV==test.Key.MaSV).Sum(x=>x.DMHOCPHAN.Sodvht)
                             DiemTBC=test.Sum(x=>x.DiemHP*x.DMHOCPHAN.Sodvht)/test.Sum(x=>x.DMHOCPHAN.Sodvht)
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        //3. Cho biết điểm trung bình chung của mỗi sinh viên ở mỗi học kỳ.
        public JsonResult Bai11()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         group diem by new { hp.HocKy,diem.MaSV} into test
                         select new
                         {
                             HoTen = test.Key.MaSV,
                             HocKy=test.Key.HocKy,
                             //DiemTBC = test.Sum()/db.DMDIEMHPs.Where(x=>x.MaSV==test.Key.MaSV).Sum(x=>x.DMHOCPHAN.Sodvht)
                             DiemTBC = test.Sum(x => x.DiemHP * x.DMHOCPHAN.Sodvht) / test.Sum(x => x.DMHOCPHAN.Sodvht)
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }

        //4. Cho biết MaLop, TenLop, số lượng nam nữ theo từng lớp.
        public JsonResult Bai12()
        {
            var query = (from sv in db.DMSINHVIENs
                         join lop in db.DMLOPs
                         on sv.MaLop equals lop.MaLop
                         group sv by lop into res
                         select new
                         {
                             MaLop = res.Key.MaLop,
                             TenLop = res.Key.TenLop,
                             SVNam = res.Count(x => x.GioiTinh == false),
                             SVNu = res.Count(x => x.GioiTinh == true)
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }

        //Bài số 2: Câu lệnh SQL có từ khoá GROUP BY với điều kiện lọc.

        //1. Cho biết điểm trung bình chung của mỗi sinh viên ở học kỳ 1.
        public JsonResult Bai13()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         where hp.HocKy==1
                         group diem by new { hp.HocKy, diem.MaSV } into test
                         select new
                         {
                             HoTen = test.Key.MaSV,
                             HocKy = test.Key.HocKy,
                             //DiemTBC = test.Sum()/db.DMDIEMHPs.Where(x=>x.MaSV==test.Key.MaSV).Sum(x=>x.DMHOCPHAN.Sodvht)
                             DiemTBC = test.Sum(x => x.DiemHP * x.DMHOCPHAN.Sodvht) / test.Sum(x => x.DMHOCPHAN.Sodvht)
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        //2. Cho biết MaSV, HoTen, Số các học phần thiếu điểm (DiemHP<5) của
        //mỗi sinh viên.
        public JsonResult Bai14()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         where diem.DiemHP < 5
                         group diem by diem.DMSINHVIEN into res
                         select new
                         {
                             MaSV = res.Key.MaSV,
                             HoTen = res.Key.HoTen,
                             SoHocPhanDiemNhoHon5 = res.Count()
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        //3. Đếm số sinh viên có điểm HP <5 của mỗi học phần.
        public JsonResult Bai15()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         where diem.DiemHP < 5
                         group diem.DMSINHVIEN by hp into res
                         select new
                         {
                             TenHP=res.Key.MaHP,
                             MaHP=res.Key.TenHP,
                             SoSinhVienDiemNhoHon5 = res.Count()
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        //4. Tính tổng số đơn vị học trình có điểm HP <5 của mỗi sinh viên.
        public JsonResult Bai16()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         where diem.DiemHP < 5
                         group diem by diem.DMSINHVIEN into res
                         select new
                         {
                             MaSV = res.Key.MaSV,
                             HoTen = res.Key.HoTen,
                             SoDVHTCoDiemNhoHon5 = res.Sum(x=>x.DMHOCPHAN.Sodvht)
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }


        //Bài số 3: Câu lệnh SQL có từ khoá GROUP BY với điều kiện nhóm.

        //1. Cho biết MaLop, TenLop có tổng số sinh viên <10
        public JsonResult Bai17()
        {
            var query = (from sv in db.DMSINHVIENs
                         join lop in db.DMLOPs
                         on sv.MaLop equals lop.MaLop
                         group sv by lop into res
                         where res.Count() < 10
                         select new
                         {
                             MaLop = res.Key.MaLop,
                             TenLop = res.Key.TenLop,
                             SoSinhVien = res.Count()
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }

        //2. Cho biết HoTen sinh viên có điểm Trung bình chung các học phần <3.
        public JsonResult Bai18()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         group diem by diem.DMSINHVIEN into test
                         where test.Sum(x => x.DiemHP * x.DMHOCPHAN.Sodvht) / test.Sum(x => x.DMHOCPHAN.Sodvht)<3
                         select new
                         {
                             HoTen = test.Key.HoTen,
                             DiemTBC = test.Sum(x => x.DiemHP * x.DMHOCPHAN.Sodvht) / test.Sum(x => x.DMHOCPHAN.Sodvht)
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }

        //3. Cho biết HoTen sinh viên có ít nhất 2 học phần có điểm <5
        public JsonResult Bai19()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         group diem by diem.DMSINHVIEN into test
                         where test.Count(x=>x.DiemHP<5)>=2
                         select new
                         {
                             HoTen = test.Key.HoTen,
                             SoHocPhan= test.Count(x => x.DiemHP < 5)
                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        //4. Cho biết HoTen sinh viên học TẤT CẢ các học phần ở ngành 140902.
        public JsonResult Bai20()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         where hp.MaNganh=="140902"
                         group diem.MaHP by diem.DMSINHVIEN into res
                         where res.Count()==(from hp1 in db.DMHOCPHANs where hp1.MaNganh=="140902" select new { MaHP=hp1.MaHP}).Count()
                         select new
                         {
                             HoTen = res.Key.HoTen,

                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }

        //5. Cho biết HoTen sinh viên học ít nhất 3 học phần mã ‘001’, ‘002’,
        //‘003’.
        public JsonResult Bai21()
        {
            string[] listHP = { "001", "002", "003" };
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         where listHP.Contains(hp.MaHP)
                         group diem.MaHP by diem.DMSINHVIEN into res
                         select new
                         {
                             HoTen = res.Key.HoTen,

                         }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);

        }


        //Bài số 4: Câu lệnh SQL có từ khoá TOP.

        //1. Cho biết MaSV, HoTen sinh viên có điểm TBC cao nhất ở học kỳ 1
        public JsonResult Bai22()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         where hp.HocKy == 1
                         group diem by new { hp.HocKy, diem.MaSV } into test
                         select new
                         {
                             HoTen = test.Key.MaSV,
                             HocKy = test.Key.HocKy,
                             //DiemTBC = test.Sum()/db.DMDIEMHPs.Where(x=>x.MaSV==test.Key.MaSV).Sum(x=>x.DMHOCPHAN.Sodvht)
                             DiemTBC = test.Sum(x => x.DiemHP * x.DMHOCPHAN.Sodvht) / test.Sum(x => x.DMHOCPHAN.Sodvht)
                         }).OrderByDescending(x=>x.DiemTBC).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        //2. Cho biết MaSV, HoTen sinh viên có số học phần điểm HP <5 nhiều
        //nhất.
        public JsonResult Bai23()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         group diem by diem.DMSINHVIEN into test
                         select new
                         {
                             HoTen = test.Key.HoTen,
                             SoHocPhan = test.Count(x => x.DiemHP < 5)
                         }).OrderByDescending(x=>x.SoHocPhan).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        //3. Cho biết MaHP, TenHP có số sinh viên điểm HP <5 nhiều nhất.
        public JsonResult Bai24()
        {
            var query = (from diem in db.DMDIEMHPs
                         join hp in db.DMHOCPHANs
                         on diem.MaHP equals hp.MaHP
                         where diem.DiemHP < 5
                         group diem.DMSINHVIEN by hp into res
                         select new
                         {
                             TenHP = res.Key.MaHP,
                             MaHP = res.Key.TenHP,
                             SoSinhVienDiemNhoHon5 = res.Count()
                         }).OrderByDescending(x=>x.SoSinhVienDiemNhoHon5).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        //thử gọi thủ tục
        [HttpGet]
        public JsonResult DiemTBC(int Hocky, string Malop)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Hocky",Hocky),
                new SqlParameter("@Malop",Malop)
            };
            IEnumerable<TBCViewModel> query =db.Database.SqlQuery<TBCViewModel>("TinhTBCUpdate @Hocky,@Malop", parameters).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

    }
}
