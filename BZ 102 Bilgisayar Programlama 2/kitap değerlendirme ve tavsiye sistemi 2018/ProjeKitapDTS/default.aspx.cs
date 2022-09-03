using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjeKitapDTS
{
    public partial class _default : System.Web.UI.Page
    {
        UyeDal _uyeDal = new UyeDal();
        TavsiyeDal _tavsiyeDal = new TavsiyeDal();
        List<int> _onerilenKullanicilarIDs = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["isLogged"]))
            {
                KullaniciOneri1();
                KullaniciOneri2();
                KitapOneri();
            }
        }
        public void KullaniciOneri1()
        {
            int kullaniciID = _uyeDal.GetIDbyKullaniciAdi(Session["KullaniciAdi"].ToString());
            List<int> okuduklari = _tavsiyeDal.OkunmusKitaplarByID(kullaniciID);
            if (okuduklari.Count == 0)
            {

            }
            else
            {
                DataSet onerilenler1 = _tavsiyeDal.KullaniciOneri1(kullaniciID, okuduklari);
                GW_Oneri1.DataSource = onerilenler1.Tables[0];
                GW_Oneri1.DataBind();
                foreach (DataRow row in onerilenler1.Tables[0].Rows)
                {
                    int currentKullanici = Convert.ToInt32(row["KullaniciID"]);
                    _onerilenKullanicilarIDs.Add(currentKullanici);
                }
            }

            
        }
        public void KullaniciOneri2()
        {
            int kullaniciID = _uyeDal.GetIDbyKullaniciAdi(Session["KullaniciAdi"].ToString());
            List<KitapPuan> puanladiklari = _tavsiyeDal.PuanlanmisKitaplarVePuanlariByID(kullaniciID);
            if (puanladiklari.Count == 0)
            {

            }
            else
            {
                DataSet onerilenler2 = _tavsiyeDal.KullaniciOneri2(kullaniciID, puanladiklari);
                onerilenler2.Tables[0].DefaultView.Sort = "KullaniciAdi ASC";
                GW_Oneri2.DataSource = onerilenler2.Tables[0];
                GW_Oneri2.DataBind();
                foreach (DataRow row in onerilenler2.Tables[0].Rows)
                {
                    int currentKullanici = Convert.ToInt32(row["KullaniciID"]);
                    _onerilenKullanicilarIDs.Add(currentKullanici);
                }
            }
        }
        public void KitapOneri()
        {
            int kullaniciID = _uyeDal.GetIDbyKullaniciAdi(Session["KullaniciAdi"].ToString());
            List<int> okuduklari = _tavsiyeDal.OkunmusKitaplarByID(kullaniciID);
            if(okuduklari.Count == 0){

            }else {
                DataSet onerilenKitaplar = _tavsiyeDal.KitapOneri(okuduklari, _onerilenKullanicilarIDs);
               // onerilenKitaplar.Tables[0].DefaultView.Sort = "Ad ASC";
                GW_KitapOneri.DataSource = onerilenKitaplar.Tables[0];
                GW_KitapOneri.DataBind();
            }
            
        }
    }
}