using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class kitap : System.Web.UI.Page
    {
        KitapDal _kitapDal = new KitapDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            KitapCek();
            IncelemelerYaz();
            AlintilarYaz();

            if (!IsPostBack)
            {
                HttpCookie cookieRepData = Request.Cookies["FlushData"];
                if (cookieRepData != null)
                {
                    string strCookieVal = (string)cookieRepData.Value;      //strCookieVal is NOT null!
                    strCookieVal = HttpUtility.UrlDecode(strCookieVal);
                    if (strCookieVal != null)
                    {
                        if(strCookieVal != "")
                        {
                            areaBilgilendirme.InnerHtml = ElementHazirla(strCookieVal);
                        }
                    }
                }
            }

            //string flushData = "";
            //if (Request.Cookies["FlushData"] != null) {
            //    flushData = Server.HtmlEncode(Response.Cookies["FlushData"].Value);
            //    areaBilgilendirme.InnerHtml = ElementHazirla(flushData);
            //}


            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            int kitapID = Convert.ToInt32(Page.RouteData.Values["KitapID"]);

            int isOkunma = _kitapDal.KontrolOkunmaKayit(kullaniciID, kitapID);
            if (isOkunma > 0)
            {
                areaOkunma.InnerHtml = ElementHazirla("Bu kitabı okudunuz.");
            }

            int isPuanlama = _kitapDal.KontrolPuanVer(kullaniciID, kitapID);
            if (isPuanlama>0)
            {
                areaPuan.InnerHtml = ElementHazirla("Zaten puanladınız."); ;
            }

            int isInceleme = _kitapDal.KontrolIncelemeYaz(kullaniciID, kitapID);
            if (isInceleme > 0)
            {
                areaInceleme.InnerHtml = ElementHazirla("Zaten inceleme yazdınız."); ;
                areaIncelemeButon.InnerHtml = "";
            }

        }

        public string ElementHazirla(string mesaj)
        {
            string elementHazirla = "";
            string elementText = mesaj;
            elementHazirla += "<div class=";
            elementHazirla += "'alert alert-primary' ";
            elementHazirla += " role=";
            elementHazirla += "'alert'";
            elementHazirla += ">" + elementText + "</div>";

            return elementHazirla;
        }

        public void SetFlushData(string strMsg, int timeSeconds)
        {
            Response.Cookies["FlushData"].Value = HttpUtility.UrlEncode(strMsg);
            Response.Cookies["FlushData"].Expires = DateTime.Now.AddSeconds(timeSeconds);
        }

        public void KitapCek()
        {
            Kitap kitap = new Kitap();
            int kitapID = Convert.ToInt32(Page.RouteData.Values["KitapID"]);
            kitap = _kitapDal.GetByID(kitapID);
            kitapAdi.InnerText = kitap.Ad;
            yazar.InnerText = kitap.YazarID.ToString();
            okunma.InnerText = _kitapDal.OkunmaSayisiByKitapID(kitapID).ToString();
            ortalamaPuan.InnerText = _kitapDal.OrtalamaPuanByKitapID(kitapID).ToString();
            yayinevi.InnerText = kitap.Yayinevi;
            tanitimBilgisi.InnerText = kitap.TanitimBilgisi;
            resim.Src = "/fotograf/kitap/" + kitap.ResimURL + ".jpg";
        }

        public void IncelemelerYaz()
        {
            DataSet incelemeler = _kitapDal.IncelemelerCek(Convert.ToInt32(Page.RouteData.Values["KitapID"]));
            Incelemeler1.DataSource = incelemeler.Tables[0];
            Incelemeler1.DataBind();
        }

        public void AlintilarYaz()
        {
            DataSet alintilar = _kitapDal.AlintilarCek(Convert.ToInt32(Page.RouteData.Values["KitapID"]));
            Alintilar1.DataSource = alintilar.Tables[0];
            Alintilar1.DataBind();
        }

        protected void btnPuan_Click(object sender, EventArgs e)
        {
            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            int kitapID = Convert.ToInt32(Page.RouteData.Values["KitapID"]);            
            decimal puan = Convert.ToDecimal(selectPuan.Items[selectPuan.SelectedIndex].Text);
            _kitapDal.PuanVer(kullaniciID, kitapID, puan);

            SetFlushData("Kitabı başarıyla puanladınız!", 5);

            Response.Redirect("/kitap/" + Page.RouteData.Values["KitapID"]);
        }

        protected void btnInceleme_Click(object sender, EventArgs e)
        {
            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            int kitapID = Convert.ToInt32(Page.RouteData.Values["KitapID"]);
            string inceleme = inputInceleme.Text;
            _kitapDal.IncelemeYaz(kullaniciID, kitapID, inceleme);

            SetFlushData("İncelemeniz başarıyla kaydedildi!", 5);

            Response.Redirect("/kitap/" + Page.RouteData.Values["KitapID"]);

        }

        protected void btnAlinti_Click(object sender, EventArgs e)
        {
            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            int kitapID = Convert.ToInt32(Page.RouteData.Values["KitapID"]);
            int sayfaNo = Convert.ToInt32(inputAlintiSayfa.Text);
            string cumle = inputAlintiCumle.Text;
            _kitapDal.AlintiYap(kullaniciID, kitapID, sayfaNo, cumle);

            SetFlushData("Alıntınız başarıyla kaydedildi!", 5);

            Response.Redirect("/kitap/" + Page.RouteData.Values["KitapID"]);
        }

        protected void btnOkundu_Click(object sender, EventArgs e)
        {
            int kullaniciID = Convert.ToInt32(Session["KullaniciID"]);
            int kitapID = Convert.ToInt32(Page.RouteData.Values["KitapID"]);
            _kitapDal.OkunmaKayit(kullaniciID, kitapID);

            SetFlushData("Okuma kaydınız başarıyla oluşturuldu!", 5);

            Response.Redirect("/kitap/" + Page.RouteData.Values["KitapID"]);
        }
    }
}