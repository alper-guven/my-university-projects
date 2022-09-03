using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class mesaj : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            MesajlarYaz();
        }
        public void MesajlarYaz()
        {
            
            int GonderenID = Convert.ToInt32(Page.RouteData.Values["KullaniciID"]);
            int GidenID = Convert.ToInt32(Session["KullaniciID"]);
            DataSet incelemeler = _uyeDal.GelenMesajlarCek(GonderenID, GidenID);
            Mesajlar1.DataSource = incelemeler.Tables[0];
            Mesajlar1.DataBind();
        }

        protected void btnMesajGonder_Click(object sender, EventArgs e)
        {
            int GonderenID = Convert.ToInt32(Session["KullaniciID"]);
            int GidenID = Convert.ToInt32(Page.RouteData.Values["KullaniciID"]);
            string baslik = inputBaslik.Text;
            string mesaj = inputMesaj.Text;
            DateTime tarih = DateTime.Now;
            _uyeDal.MesajGonder(GonderenID, GidenID, baslik, mesaj, tarih);

            Response.Redirect("/mesajlar/" + Page.RouteData.Values["KullaniciID"]);
        }
    }
}