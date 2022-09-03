using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class yonetim_giris : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void girisBtn_Click1(object sender, EventArgs e)
        {
            string AdminAdi = inputKullaniciAdi.Text;
            string Sifre = inputSifre.Text;

            bool sessionCtrl = _uyeDal.YoneticiGirisYap(AdminAdi, Sifre);

            if (sessionCtrl == false)
                Response.Write("Yanlis kullanıcı adı ve/veya sifre");
            else
            {

                Session["isLoggedasAdmin"] = true;
                Session["AdminAdi"] = AdminAdi;
                Response.Redirect("/yonetim/");
            }
        }
    }
}