<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="populer-yuksekPuanliKitaplar.aspx.cs" Inherits="ProjeKitapDTS.populer_yuksekPuanliKitaplar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
    <div class="col-9" style="height: 100%; min-height: 1000px;">
                <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>Popüler Kitaplar</h1>
        </div>
        <div class="row" style="padding: 20px 0px 0px 20px;">
            
            <asp:GridView ID="Kitaplar1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="KitapID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="KitapID"
                                        DataNavigateUrlFormatString="/kitap/{0}"
                                        DataTextField="Ad" HeaderText="Kitabın Adı"/>
                    <asp:BoundField DataField="OrtalamaPuan" HeaderText="Ortalama Puan" DataFormatString="{0:0.00}"/>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="kullaniciNotLoggedin" runat="server">
</asp:Content>

