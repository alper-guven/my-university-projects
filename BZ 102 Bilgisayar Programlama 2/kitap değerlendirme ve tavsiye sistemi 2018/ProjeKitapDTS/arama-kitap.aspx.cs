using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class arama_kitap : System.Web.UI.Page
    {
        KitapDal _kitapDal = new KitapDal();
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
            DataSet kitaplar = _kitapDal.Arama(arananKelime);
            Kitaplar1.DataSource = kitaplar.Tables[0];
            Kitaplar1.DataBind();
        }

        protected void AraBtn_Click(object sender, EventArgs e)
        {

        }
    }
}