<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="arama-yazar.aspx.cs" Inherits="ProjeKitapDTS.arama_yazar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
    <div class="col-9" style="height: 100%; min-height: 1000px;">
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h4>Yazar Arama</h4>
        </div>
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <form class="col-10">
                <div class="form-group row">
                    <label for="inputArananKelime" class="col-sm-4 col-form-label">Yazar adı ya da soyadı:</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" type="text" class="form-control" id="inputArananKelime" placeholder="J. K. Rowling" required></asp:TextBox>
                    </div>
                    <div class="col-sm-2">
                        <asp:Button runat="server" type="submit" class="btn btn-primary" ID="AraBtn" Text="Ara" OnClick="AraBtn_Click"></asp:Button>
                    </div>
                </div>
            </form>
        </div>
        <div class="row" style="padding: 20px 0px 0px 20px;">

            <asp:GridView ID="Yazarlar1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="YazarID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="YazarID"
                        DataNavigateUrlFormatString="/yazar/{0}"
                        DataTextField="AdSoyad" HeaderText="Yazarın Adı" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="kullaniciNotLoggedin" runat="server">
</asp:Content>