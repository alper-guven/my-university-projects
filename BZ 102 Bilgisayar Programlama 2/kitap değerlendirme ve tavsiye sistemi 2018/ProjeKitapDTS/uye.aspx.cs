using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class kullanici2 : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();

        KitapDal _kitapDal = new KitapDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            UyeCek();
            IncelemelerYaz();
            AlintilarYaz();
            OkunanlarYaz();
            PuanlarYaz();

        }
        Uye uye = new Uye();
        public void UyeCek()
        {            
            string kullaniciAdi = Convert.ToString(Page.RouteData.Values["KullaniciAdi"]);
            uye = _uyeDal.GetByKAdi(kullaniciAdi);
            adSoyad.InnerText = uye.Isim + " " + uye.Soyisim;
            profilFotografi.Src = "/fotograf/kullanici/" + kullaniciAdi + ".jpg";
            btnMesajGonder.PostBackUrl = "~/mesajlar/" + uye.KullaniciID;
        }

        public void IncelemelerYaz()
        {
            int kullaniciID = _uyeDal.GetIDbyKullaniciAdi(Page.RouteData.Values["KullaniciAdi"].ToString());
            DataSet incelemeler = _kitapDal.IncelemelerCekByKID(kullaniciID);
            Incelemeler1.DataSource = incelemeler.Tables[0];
            Incelemeler1.DataBind();
        }
        public void AlintilarYaz()
        {
            int kullaniciID = _uyeDal.GetIDbyKullaniciAdi(Page.RouteData.Values["KullaniciAdi"].ToString());
            DataSet alintilar = _kitapDal.AlintilarCekByKID(kullaniciID);
            Alintilar1.DataSource = alintilar.Tables[0];
            Alintilar1.DataBind();
        }
        public void OkunanlarYaz()
        {
            int kullaniciID = _uyeDal.GetIDbyKullaniciAdi(Page.RouteData.Values["KullaniciAdi"].ToString());
            DataSet okunanlar = _kitapDal.OkunanlarCekByKID(kullaniciID);
            Okunanlar1.DataSource = okunanlar.Tables[0];
            Okunanlar1.DataBind();
        }
        public void PuanlarYaz()
        {
            int kullaniciID = _uyeDal.GetIDbyKullaniciAdi(Page.RouteData.Values["KullaniciAdi"].ToString());
            DataSet puanlar = _kitapDal.PuanlarCekByKID(kullaniciID);
            Puanlar1.DataSource = puanlar.Tables[0];
            Puanlar1.DataBind();
        }

        protected void btnMesajGonder_Click(object sender, EventArgs e)
        {
            Response.Redirect("/mesajlar/" + uye.KullaniciID);
        }
    }
}