using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class arama_kullanici : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();
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
            DataSet uyeler = _uyeDal.Arama(arananKelime);
            Uyeler1.DataSource = uyeler.Tables[0];
            Uyeler1.DataBind();
        }

        protected void AraBtn_Click(object sender, EventArgs e)
        {

        }
    }
}