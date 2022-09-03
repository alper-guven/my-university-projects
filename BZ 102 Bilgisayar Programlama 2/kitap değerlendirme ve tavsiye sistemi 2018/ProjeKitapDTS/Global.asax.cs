using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ProjeKitapDTS
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            SaveRoutes(RouteTable.Routes);
        }

        void SaveRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("yonetimDashboard",
                "yonetim/",
                "~/yonetim.aspx");
            routes.MapPageRoute("yonetimSayfalar",
                "yonetim/{Sayfa}",
                "~/yonetim-{Sayfa}.aspx");
            routes.MapPageRoute("TekParcaURI",
                "{uri1}/",
                "~/{uri1}.aspx");
            routes.MapPageRoute("kullanici",
                "kullanici/{KullaniciAdi}",
                "~/uye.aspx");
            routes.MapPageRoute("kitap",
                "kitap/{KitapID}",
                "~/kitap.aspx");
            routes.MapPageRoute("yazar",
                "yazar/{YazarID}",
                "~/yazar.aspx");
            routes.MapPageRoute("mesaj",
                "mesajlar/{KullaniciID}",
                "~/mesaj.aspx");


        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}