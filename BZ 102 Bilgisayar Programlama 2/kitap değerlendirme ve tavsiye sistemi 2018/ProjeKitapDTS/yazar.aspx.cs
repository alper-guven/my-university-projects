using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class yazar : System.Web.UI.Page
    {
        YazarDal _yazarDal = new YazarDal();
        KitapDal _kitapDal = new KitapDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            YazarCek();
            KitaplarCek();
        }

        public void YazarCek()
        {
            DataSet yazar = _yazarDal.GetByID(Convert.ToInt32(Page.RouteData.Values["YazarID"]));
            Yazar1.DataSource = yazar.Tables[0];
            Yazar1.DataBind();
        }

        public void KitaplarCek()
        {
            DataSet kitaplar = _kitapDal.GetByYazarID(Convert.ToInt32(Page.RouteData.Values["YazarID"]));
            Kitaplar1.DataSource = kitaplar.Tables[0];
            Kitaplar1.DataBind();
        }
    }
}