using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class populer_yuksekPuanliKitaplar : System.Web.UI.Page
    {
        KitapDal _kitapDal = new KitapDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        public void BindGrid()
        {
            DataSet kitaplar = _kitapDal.YuksekPuanliKitaplarCek();
            Kitaplar1.DataSource = kitaplar.Tables[0];
            Kitaplar1.DataBind();
        }
    }
}