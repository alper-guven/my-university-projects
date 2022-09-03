<%@ Page Title="" Language="C#" MasterPageFile="~/yonetim.Master" AutoEventWireup="true" CodeBehind="yonetim-kitapEkle.aspx.cs" Inherits="ProjeKitapDTS.yonetim_kitapEkle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="yoneticiHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="yoneticiBody" runat="server">

      <div class="col-6" style="height: 100%; min-height: 1000px;">
            <h4 style="margin: 20px 0px 0px 20px;">Kitap Ekle</h4>
            <div style="margin: 20px 0px 0px 20px;">
              <form>
                <div class="form-group row">
                  <label for="inputAd" class="col-sm-2 col-form-label">Ad</label>
                  <div class="col-sm-10">
                    <asp:TextBox runat="server" type="text" class="form-control" id="inputAd" placeholder="John Doe" required></asp:TextBox>
                  </div>
                </div>
                <div class="form-group row">
                  <label for="inputYazar" class="col-sm-2 col-form-label">Yazar</label>
                  <div class="col-sm-10">
                    <asp:DropDownList runat="server" class="custom-select" ID="selectYazarID">

                    </asp:DropDownList>
                  </div>
                </div>                 
                <div class="form-group row">
                  <label for="inputYayinevi" class="col-sm-2 col-form-label">Yayınevi</label>
                  <div class="col-sm-10">
                    <asp:TextBox runat="server" type="text" class="form-control" id="inputYayinevi" placeholder="" required></asp:TextBox>
                  </div>
                </div>
                <div class="form-group row">
                  <label for="inputTanitimBilgisi" class="col-sm-2 col-form-label">Tanıtım Bilgisi</label>
                  <div class="col-sm-10">
                      <asp:TextBox runat="server" class="form-control" id="inputTanitimBilgisi" rows="4" required></asp:TextBox>
                  </div>
                </div>          
                <div class="form-group row">
                  <label for="inputResimURL" class="col-sm-2 col-form-label">ResimURL</label>
                  <div class="col-sm-10">
                    <asp:TextBox runat="server" type="text" class="form-control" id="inputResimURL" placeholder="<resim adı> (uzantı girilmeyecek)" required></asp:TextBox>
                  </div>
                </div>
                  <div class="form-group row">
                      <label for="uploadResim" class="col-sm-2 col-form-label">Resim Seçin</label>
                      <div class="col-sm-10">
                          <asp:FileUpload ID="uploadResim" runat="server" />
                      </div>
                  </div>
                <asp:Button runat="server" type="submit" class="btn btn-primary float-right" ID="kitapEkleBtn" Text="Kitap Ekle" OnClick="kitapEkleBtn_Click"></asp:Button>
              </form>
            </div>
      </div>

</asp:Content>
