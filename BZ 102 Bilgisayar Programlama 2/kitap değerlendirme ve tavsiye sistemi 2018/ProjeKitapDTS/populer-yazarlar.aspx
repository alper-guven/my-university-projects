<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="populer-yazarlar.aspx.cs" Inherits="ProjeKitapDTS.populer_yazarlar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
    <div class="col-9" style="height: 100%; min-height: 1000px;">
                <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>Popüler Yazarlar</h1>
        </div>
        <div class="row" style="padding: 20px 0px 0px 20px;">
            
            <asp:GridView ID="Yazarlar1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="YazarID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="YazarID"
                                        DataNavigateUrlFormatString="/yazar/{0}"
                                        DataTextField="AdSoyad" HeaderText="Yazarın Adı"/>
                    <asp:BoundField DataField="YazarOkunma" HeaderText="Toplam Okunma"/>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="kullaniciNotLoggedin" runat="server">
</asp:Content>

