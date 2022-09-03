<%@ Page Title="" Language="C#" MasterPageFile="~/yonetim.Master" AutoEventWireup="true" CodeBehind="yonetim-giris.aspx.cs" Inherits="ProjeKitapDTS.yonetim_giris" %>
<asp:Content ID="Content1" ContentPlaceHolderID="yoneticiHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="yoneticiBody" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="yoneticiNotLoggedin" runat="server">
    <div class="col-6" style="height: 100%; min-height: 1000px;">
        <h4 style="margin: 20px 0px 0px 20px;">Giriş Yap</h4>
        <div style="margin: 20px 0px 0px 20px;">
            <form>
                <div class="form-group row">
                    <label for="inputKullaniciAdi" class="col-sm-2 col-form-label">Kullanıcı Adı</label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" type="text" class="form-control" ID="inputKullaniciAdi" placeholder="SuperHero1"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="inputSifre" class="col-sm-2 col-form-label">Şifre</label>
                    <div class="col-sm-10">
                        <asp:TextBox runat="server" type="password" class="form-control" ID="inputSifre" placeholder="Şifre"></asp:TextBox>
                    </div>
                </div>
                <asp:Button Text="Giriş Yap" runat="server" class="btn btn-primary float-right" ID="girisBtn" OnClick="girisBtn_Click1" />
            </form>
        </div>
        
    </div>
</asp:Content>
