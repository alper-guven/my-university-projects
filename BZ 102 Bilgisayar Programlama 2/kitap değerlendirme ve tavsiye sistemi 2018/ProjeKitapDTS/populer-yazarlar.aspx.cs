using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class populer_yazarlar : System.Web.UI.Page
    {
        YazarDal _yazarDal = new YazarDal();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        public void BindGrid()
        {
            DataSet yazarlar = _yazarDal.PopulerYazarlarCek();
            Yazarlar1.DataSource = yazarlar.Tables[0];
            Yazarlar1.DataBind();
        }
    }
}