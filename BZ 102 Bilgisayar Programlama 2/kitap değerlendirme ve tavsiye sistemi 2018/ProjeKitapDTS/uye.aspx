<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="uye.aspx.cs" Inherits="ProjeKitapDTS.kullanici2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">

    <div class="col-6" style="height: 100%; min-height: 1000px;">
        <div class="row" style="border-bottom: 15px solid #1976D2; background: #000428; /* fallback for old browsers */
                                  background: -webkit-linear-gradient(to left, #BBDEFB, #1976D2); /* chrome 10-25, safari 5.1-6 */
                                  background: linear-gradient(to left, #BBDEFB, #1976D2); /* w3c, ie 10+/ edge, firefox 16+, chrome 26+, opera 12+, safari 7+ */">
            <div class="col-4"></div>
            <div class="col-4" style="margin-top: 20px; margin-bottom: 10px;">
                <img class="" style="width: 100%; margin-bottom: 10px;" runat="server" id="profilFotografi">
                <h3 runat="server" id="adSoyad" class="text-center" style="color: white;">Alper Güven</h3>
                <asp:Button runat="server" class="btn btn-primary" Style="width: 100%; margin-bottom: 10px; background: #1976D2;" ID="btnMesajGonder" Text="Mesaj Gönder" OnClick="btnMesajGonder_Click"></asp:Button>
            </div>
            <div class="col-4"></div>
        </div>
        <div>
            <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
                <h4>İncelemeleri</h4>
            </div>
            <asp:GridView ID="Incelemeler1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="KitapID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="KitapID"
                        DataNavigateUrlFormatString="/kitap/{0}"
                        DataTextField="Ad" HeaderText="Kitabın Adı" />
                    <asp:BoundField DataField="Inceleme" HeaderText="İnceleme" />
                </Columns>
            </asp:GridView>
            <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
                <h4>Alıntıladıkları</h4>
            </div>
            <asp:GridView ID="Alintilar1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="KitapID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="KitapID"
                        DataNavigateUrlFormatString="/kitap/{0}"
                        DataTextField="Ad" HeaderText="Kitabın Adı" />
                    <asp:BoundField DataField="SayfaNo" HeaderText="Sayfa No" />
                    <asp:BoundField DataField="Cumle" HeaderText="Cümle" />
                </Columns>
            </asp:GridView>
            <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
                <h4>Okuduğu Kitaplar</h4>
            </div>
            <asp:GridView ID="Okunanlar1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="KitapID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="KitapID"
                        DataNavigateUrlFormatString="/kitap/{0}"
                        DataTextField="Ad" HeaderText="Kitabın Adı" />
                </Columns>
            </asp:GridView>
            <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
                <h4>Verdiği Puanlar</h4>
            </div>
            <asp:GridView ID="Puanlar1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="KitapID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="KitapID"
                        DataNavigateUrlFormatString="/kitap/{0}"
                        DataTextField="Ad" HeaderText="Kitabın Adı" />
                    <asp:BoundField DataField="Puan" HeaderText="Puan" />
                </Columns>
            </asp:GridView>
        </div>

    </div>
</asp:Content>
