using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class kayit : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();
        string _cinsiyet = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void kayitBtn_Click1(object sender, EventArgs e)
        {
            if (rdbErkek.Checked)
                _cinsiyet = "erkek";
            else
                _cinsiyet = "kadin";

            string filename = inputKullaniciAdi.Text +".jpg";
            uploadResim.SaveAs(Server.MapPath("fotograf/kullanici/") + filename);


            _uyeDal.Ekle(new Uye
            {
                Isim = inputIsim.Text,
                Soyisim = inputSoyisim.Text,
                KullaniciAdi = inputKullaniciAdi.Text,
                Cinsiyet = _cinsiyet,
                DogumTarihi = inputDogumTarihi1.SelectedDate,
                // DogumTarihi = inputDogumTarihi.Text,
                Sifre = inputSifre.Text
            });

            Response.Redirect("/");
        }

    }
}