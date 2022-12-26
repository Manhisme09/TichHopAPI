using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Thi.Models;

namespace Thi.Controllers
{
    public class SanPhamController : ApiController
    {
        QLBanHangEntities db = new QLBanHangEntities();

        [HttpGet]
        public List<SanPham> DanhSach()
        {
            return db.SanPhams.ToList();
        }

        [HttpGet]
        public SanPham TimSP(string masp)
        {
            return db.SanPhams.FirstOrDefault(x => x.MaSP == masp);
        }

        [HttpPost]
        public bool ThemSP(string ma, string ten, string dvtinh, int soluong, int dongia)
        {
            SanPham sp = db.SanPhams.FirstOrDefault(x => x.MaSP == ma);
            if (sp == null)
            {
                SanPham sp1 = new SanPham();
                sp1.MaSP = ma;
                sp1.TenSP = ten;
                sp1.DVTinh = dvtinh;
                sp1.SoLuong = soluong;
                sp1.Gia = dongia;
                db.SanPhams.Add(sp1);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpPut]
        public bool CapNhap(string ma, string ten, string dvtinh, int soluong, int dongia)
        {
            SanPham sp = db.SanPhams.FirstOrDefault(x => x.MaSP == ma);
            {
                if (sp != null)
                {
                    sp.MaSP = ma;
                    sp.TenSP = ten;
                    sp.DVTinh = dvtinh;
                    sp.SoLuong = soluong;
                    sp.Gia = dongia;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        [HttpDelete]

        public bool xoa(string id)
        {
            SanPham sp = db.SanPhams.FirstOrDefault(x => x.MaSP == id);
            if (sp != null)
            {
                db.SanPhams.Remove(sp);
                db.SaveChanges();
                return true;
            }
            return false;
        }

	<system.webServer>
         <modules>
           <remove name="WebDAVModule" />
         </modules>
        <handlers>
         <remove name="WebDAV" />
        </handlers>
       </system.webServer>
    }
}
