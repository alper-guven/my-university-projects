<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="yazar.aspx.cs" Inherits="ProjeKitapDTS.yazar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
    <div class="col-6" style="height: 100%; min-height: 1000px;">
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>Yazar</h1>
        </div>      
        <div class="row" style="padding: 10px; border-bottom: 15px solid #1976D2; margin-bottom: 10px; border-top: 15px solid #1976D2; margin-top: 10px;">
            <asp:GridView ID="Yazar1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="YazarID">
                <Columns>
                    <asp:BoundField DataField="AdSoyad" HeaderText="Yazarın Adı"/>
                    <asp:BoundField DataField="DogumTarihi" HeaderText="Doğum Tarihi"/>
                    <asp:BoundField DataField="DogumYeri" HeaderText="Doğum Yeri"/>
                    <asp:BoundField DataField="OlumTarihi" HeaderText="Ölüm Tarihi"/>               
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="col-4" style="background: #BBDEFB; padding-top: 10px;">
        <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
            <h4>Yazarın Kitapları</h4>
        </div>
        <asp:GridView ID="Kitaplar1" runat="server"
            AutoGenerateColumns="False" DataKeyNames="KitapID">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="KitapID"
                    DataNavigateUrlFormatString="/kitap/{0}"
                    DataTextField="Ad" HeaderText="Kitabın Adı" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="kullaniciNotLoggedin" runat="server">
</asp:Content>
