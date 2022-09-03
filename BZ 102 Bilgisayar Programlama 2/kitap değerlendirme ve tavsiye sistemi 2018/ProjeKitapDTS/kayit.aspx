<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="kayit.aspx.cs" Inherits="ProjeKitapDTS.kayit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciNotLoggedin" runat="server">

    <div class="col-12" style="background: url('fotograf/bg-light.jpg'); background-repeat: no-repeat;
    background-size: 100% 100%">
        <div class="row d-flex justify-content-center" style="margin: 15px 0px 15px 0px;">
            <h4>Kayıt Ol</h4>
        </div>
        <div class="row d-flex justify-content-center" style="margin-bottom: 20px">

            <div>

                <form>
                    <div class="form-group row">
                        <label for="inputIsim" class="col-sm-2 col-form-label">İsim</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" type="text" class="form-control" ID="inputIsim" placeholder="John" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputSoyisim" class="col-sm-2 col-form-label">Soyad</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" type="text" class="form-control" ID="inputSoyisim" placeholder="Doe" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputKullaniciAdi" class="col-sm-2 col-form-label">Kullanıcı Adı</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" type="text" class="form-control" ID="inputKullaniciAdi" placeholder="SuperHero1" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputDogumTarihi" class="col-sm-2 col-form-label">Doğum Tarihi</label>
                        <div class="col-sm-10">
                            <asp:Calendar ID="inputDogumTarihi1" runat="server" required></asp:Calendar>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="uploadResim" class="col-sm-2 col-form-label">Profil Fotoğrafı</label>
                        <div class="col-sm-10">
                            <asp:FileUpload ID="uploadResim" runat="server" />
                        </div>
                    </div>
                    <fieldset class="form-group">
                        <div class="row">
                            <legend class="col-form-label col-sm-2 pt-0">Cinsiyet</legend>
                            <div class="col-sm-10">
                                <div class="form-check">
                                    <asp:RadioButton GroupName="grupCinsiyet" runat="server" class="form-check-input" type="radio" name="gridRadios" ID="rdbErkek" Checked></asp:RadioButton>
                                    <label class="form-check-label" for="gridRadios1">
                                        Erkek
                                    </label>
                                </div>
                                <div class="form-check">
                                    <asp:RadioButton GroupName="grupCinsiyet" runat="server" class="form-check-input" type="radio" name="gridRadios" ID="rdbKadin"></asp:RadioButton>
                                    <label class="form-check-label" for="gridRadios2">
                                        Kadın
                                    </label>
                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <div class="form-group row">
                        <label for="inputSifre" class="col-sm-2 col-form-label">Şifre</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" type="password" class="form-control" ID="inputSifre" placeholder="Şifre" required></asp:TextBox>
                        </div>
                    </div>

                    <asp:Button Text="Kayıt Ol" runat="server" class="btn btn-primary float-right" ID="kayitBtn" OnClick="kayitBtn_Click1" />
                </form>
            </div>
        </div>
    </div>

</asp:Content>
