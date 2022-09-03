using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class arama_yazar : System.Web.UI.Page
    {
        YazarDal _yazarDal = new YazarDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
            else
            {
                string Aranan = inputArananKelime.Text;
                AramaSonuclari(Aranan);
            }
        }

        public void AramaSonuclari(string arananKelime)
        {
            DataSet yazarlar = _yazarDal.Arama(arananKelime);
            Yazarlar1.DataSource = yazarlar.Tables[0];
            Yazarlar1.DataBind();
        }

        protected void AraBtn_Click(object sender, EventArgs e)
        {

        }
    }
}