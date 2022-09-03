using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class yonetim_kitapEkle : System.Web.UI.Page
    {
        KitapDal _kitapDal = new KitapDal();
        YazarDal _yazarDal = new YazarDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Yazar> yazarlar = _yazarDal.GetAll();

            foreach (Yazar yazar in yazarlar)
            {
                string yazarAdSoyad = yazar.Isim + " " + yazar.Soyisim;
                ListItem yazarX = new ListItem(yazarAdSoyad, Convert.ToString(yazar.YazarID));
                selectYazarID.Items.Add(yazarX);
            }
        }

        protected void kitapEkleBtn_Click(object sender, EventArgs e)
        {
            _kitapDal.Ekle(new Kitap
            {
                Ad = inputAd.Text,
                YazarID = Convert.ToInt32(selectYazarID.SelectedValue),
                Yayinevi = inputYayinevi.Text,
                TanitimBilgisi = inputTanitimBilgisi.Text,
                ResimURL = inputResimURL.Text
            });

            string filename = inputResimURL.Text + ".jpg";
            uploadResim.SaveAs(Server.MapPath("../fotograf/kitap/") + filename);

            Response.Redirect("/yonetim/");
        }
    }
}