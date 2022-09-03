<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="giris.aspx.cs" Inherits="ProjeKitapDTS.uye_giris" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciNotLoggedin" runat="server">
    <div class="col-12" style="height:675px; background: url('fotograf/bg-light.jpg'); background-repeat: no-repeat;
    background-size: 100% 100%">
        <div class="row justify-content-center" style="margin: 15px 0px 15px 0px;">
            <h4>Giriş Yap</h4>
        </div>
        <div class="row justify-content-center">
            <div>
                <div class="form-group row">
                    <label for="inputKullaniciAdi" class="col-sm-5 col-form-label">Kullanıcı Adı</label>
                    <div class="col-sm-7">
                        <asp:TextBox runat="server" type="text" class="form-control" ID="inputKullaniciAdi" placeholder="SuperHero1"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="inputSifre" class="col-sm-5 col-form-label">Şifre</label>
                    <div class="col-sm-7">
                        <asp:TextBox runat="server" type="password" class="form-control" ID="inputSifre" placeholder="Şifre"></asp:TextBox>
                    </div>
                </div>
                <asp:Button Text="Giriş Yap" runat="server" class="btn btn-primary float-right" ID="girisBtn" OnClick="girisBtn_Click1" />
            </div>
        </div>
    </div>

</asp:Content>
