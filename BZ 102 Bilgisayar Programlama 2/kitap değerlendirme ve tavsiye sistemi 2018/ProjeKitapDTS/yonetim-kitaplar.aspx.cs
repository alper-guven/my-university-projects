using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class yonetim_kitaplar : System.Web.UI.Page
    {
        KitapDal _kitapDal = new KitapDal();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }

        }

        public void BindGrid()
        {
            DataSet kitaplar = _kitapDal.kitaplarCek();
            Kitaplar1.DataSource = kitaplar.Tables[0];
            Kitaplar1.DataBind();
        }

        protected void Kitaplar1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Kitaplar1.EditIndex = e.NewEditIndex;

            BindGrid();
        }

        protected void Kitaplar1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)

        {

            Kitaplar1.EditIndex = -1;

            BindGrid();

        }


        protected void Kitaplar1_RowDeleting(object sender, GridViewDeleteEventArgs e)

        {

            int KitapID = (Int32)Kitaplar1.DataKeys[e.RowIndex].Value;
            _kitapDal.Sil(KitapID);

        }

        protected void Kitaplar1_RowUpdating(object sender, GridViewUpdateEventArgs e)

        {
            int KitapID = int.Parse(Kitaplar1.DataKeys[e.RowIndex].Value.ToString());
            int YazarID = int.Parse(((TextBox)Kitaplar1.Rows[e.RowIndex].Cells[1].Controls[0]).Text);
            string Ad = ((TextBox)Kitaplar1.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string Yayinevi = ((TextBox)Kitaplar1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;        
            string TanitimBilgisi = ((TextBox)Kitaplar1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            

            Kitap kitap = new Kitap {
                KitapID = KitapID,
                YazarID = YazarID,
                Ad = Ad,
                Yayinevi = Yayinevi,
                TanitimBilgisi = TanitimBilgisi
            };

            _kitapDal.Guncelle(kitap);

            Kitaplar1.EditIndex = -1;

            BindGrid();
        }

    }
}