using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class uye_giris : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void girisBtn_Click1(object sender, EventArgs e)
        {
            string KullaniciAdi = inputKullaniciAdi.Text;
            string Sifre = inputSifre.Text;

            bool sessionCtrl = _uyeDal.GirisYap(KullaniciAdi,Sifre);

            if (sessionCtrl == false)
                Response.Write("Yanlis kullanıcı adı ve/veya sifre");
            else
            {

                Session["isLogged"] = true;
                Session["KullaniciAdi"] = KullaniciAdi;
                Session["KullaniciID"] = _uyeDal.GetIDbyKullaniciAdi(KullaniciAdi);
                Response.Redirect("/");
            }
        }
    }
}