using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class mesajlar : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            MesajAtanlarCek();
        }

        public void MesajAtanlarCek()
        {
            int GidenID = Convert.ToInt32(Session["KullaniciID"]);
            DataSet mesajAtanlar = _uyeDal.MesajAtanlarCek(GidenID);
            MesajAtanlar1.DataSource = mesajAtanlar.Tables[0];
            MesajAtanlar1.DataBind();
        }

        protected void btnMesajGonder_Click(object sender, EventArgs e)
        {
            int GonderenID = Convert.ToInt32(Session["KullaniciID"]);
            int GidenID = _uyeDal.GetIDbyKullaniciAdi(inputKime.Text);
            string baslik = inputBaslik.Text;
            string mesaj = inputMesaj.Text;
            DateTime tarih = DateTime.Now;
            _uyeDal.MesajGonder(GonderenID, GidenID, baslik, mesaj, tarih);

            Response.Redirect("/mesajlar/");
        }
    }
}