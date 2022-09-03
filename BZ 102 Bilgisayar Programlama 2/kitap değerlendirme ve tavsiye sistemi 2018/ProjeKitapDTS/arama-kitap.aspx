<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="arama-kitap.aspx.cs" Inherits="ProjeKitapDTS.arama_kitap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
    <div class="col-9" style="height: 100%; min-height: 1000px;">
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h4>Kitap Arama</h4>
        </div>
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <form class="col-10">
                <div class="form-group row">
                    <label for="inputArananKelime" class="col-sm-4 col-form-label">Kitabın tam adı ya da kitap adının içerdiği kelime:</label>
                    <div class="col-sm-6">
                        <asp:TextBox runat="server" type="text" class="form-control" id="inputArananKelime" placeholder="A Game of Thrones" required></asp:TextBox>
                    </div>
                    <div class="col-sm-2">
                        <asp:Button runat="server" type="submit" class="btn btn-primary" ID="AraBtn" Text="Ara" OnClick="AraBtn_Click"></asp:Button>
                    </div>
                </div>
            </form>
        </div>
        <div class="row" style="padding: 20px 0px 0px 20px;">

            <asp:GridView ID="Kitaplar1" runat="server"
                AutoGenerateColumns="False" DataKeyNames="KitapID">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="KitapID"
                        DataNavigateUrlFormatString="/kitap/{0}"
                        DataTextField="Ad" HeaderText="Kitabın Adı" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="kullaniciNotLoggedin" runat="server">
</asp:Content>
