<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="kitap.aspx.cs" Inherits="ProjeKitapDTS.kitap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">

    <div class="col-6" style="height: 100%; min-height: 1000px;">
        <div id="areaBilgilendirme" runat="server" class="row">
        </div>
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>
                <div id="kitapAdi" runat="server" style=""></div>
            </h1>
        </div>
        <div class="row" style="padding: 10px; border-bottom: 15px solid #1976D2; margin-bottom: 10px; border-top: 15px solid #1976D2; margin-top: 10px;">
            <div class="col-4">
                <img class="" style="width: 176px; height: 232px; margin-bottom: 35px;" id="resim" runat="server">
                <div id="areaOkunma" runat="server">
                    <asp:Button runat="server" type="button" class="btn btn-primary" Style="width: 176px; margin-bottom: 10px; background: #1976D2;" ID="btnOkundu" Text="+ Okunanlara Ekle" OnClick="btnOkundu_Click"></asp:Button>
                </div>
                <!--  <button type="button" class="btn btn-primary" style="width: 176px; margin-bottom: 10px;background: #448AFF;">+ İnceleme Yaz</button>
              <button type="button" class="btn btn-primary" style="width: 176px; margin-bottom: 10px; background: #448AFF;">+ Alıntı Yap</button>   -->
            </div>
            <div class="col-8">
                <h4 class="">Yazar:<span id="yazar" runat="server" class="font-weight-normal"> Steve Pavlina</span></h4>
                <h4>Ortalama Puanı: <span id="ortalamaPuan" runat="server" class="badge badge-primary">9.2</span></h4>
                <h4>Okunma: <span id="okunma" runat="server" class="badge badge-primary">546</span></h4>
                <h4 class="">Yayınevi: <span id="yayinevi" runat="server" class="font-weight-normal">Xyz Yayıncılık</span></h4>
                <h4 class="">Kitap Tanıtım Bilgisi:</h4>
                <h4 class="font-weight-normal" id="tanitimBilgisi" runat="server"></h4>
            </div>
        </div>

        <div class="row" style="padding: 0px 0px 0px 0px;">
            <div class="col-3">
                <h4>Puan Ver</h4>
            </div>
            <div class="col-6">
            </div>
        </div>
        <div class="row" style="padding: 0px 20px 0px 20px; border-bottom: 15px solid #1976D2; margin-bottom: 10px;">
            <div class="col" id="areaPuan" runat="server">
                <form class="w-50" style="padding: 10px 20px 0px 20px;">
                    <div class="form-group">
                        <select class="custom-select mr-sm-2" runat="server" id="selectPuan" name="selectPuan">
                            <option selected>Puan Seçin...</option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                            <option value="5">5</option>
                            <option value="6">6</option>
                            <option value="7">7</option>
                            <option value="8">8</option>
                            <option value="9">9</option>
                            <option value="10">10</option>
                        </select>
                    </div>
                    <div class="col-3">
                        <asp:Button runat="server" type="button" class="btn btn-primary align-middle" Style="width: 165px; margin-bottom: 10px; background: #448AFF;" ID="btnPuan" Text="Puan Ver" OnClick="btnPuan_Click"></asp:Button>
                    </div>
                </form>
            </div>
        </div>

        <div class="row" style="padding: 0px 0px 0px 20px;">
            <h4>İnceleme Yaz</h4>
        </div>
        <div class="row">
            <div class="w-100" style="padding: 10px 20px 0px 20px;" id="areaInceleme" runat="server">
                <div class="form-group">
                    <asp:TextBox runat="server" class="form-control" TextMode="multiline" ID="inputInceleme" Rows="7"></asp:TextBox>
                </div>
            </div>
        </div>
        <div id="areaIncelemeButon" runat="server" class="row" style="padding: 0px 20px 0px 20px; border-bottom: 15px solid #1976D2; margin-bottom: 10px;">
            <div class="col"></div>
            <div class="col-3">
                    <asp:Button runat="server" type="submit" class="btn btn-primary" Style="width: 165px; margin-bottom: 10px; background: #448AFF;" ID="btnInceleme" Text="İncelemeyi Paylaş" OnClick="btnInceleme_Click"></asp:Button>
            </div>            
        </div>

        <div class="row" style="padding: 0px 0px 0px 20px;">
            <h4>Alıntı Yap</h4>
        </div>
        <div class="row">
            <div class="w-100" style="padding: 10px 20px 0px 20px;" id="areaAlinti" runat="server">
                <div class="form-group row">
                    <label for="staticEmail" class="col-3 col-form-label text-right">Alıntılanacak cümle</label>
                    <div class="col-9">
                        <asp:TextBox runat="server" class="form-control" ID="inputAlintiCumle" TextMode="multiline" Rows="4"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="staticEmail" class="col-3 col-form-label text-right">Bu cümle kaçıncı sayfada?</label>
                    <div class="col-9">
                        <asp:TextBox runat="server" class="form-control w-25" ID="inputAlintiSayfa" Rows="1"></asp:TextBox>

                    </div>
                </div>
            </div>

        </div>
        <div class="row" style="padding: 0px 20px 0px 20px; border-bottom: 15px solid #1976D2; margin-bottom: 10px;">
            <div class="col"></div>
            <div class="col-3">
                <asp:Button runat="server" type="submit" class="btn btn-primary" Style="width: 165px; margin-bottom: 10px; background: #448AFF;" ID="btnAlinti" Text="Alıntı Yap" OnClick="btnAlinti_Click"></asp:Button>
            </div>
        </div>
    </div>

    <div class="col-4" style="background: #BBDEFB; padding-top: 10px;">
        <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
            <h4>İncelemeler</h4>
        </div>
        <asp:GridView ID="Incelemeler1" runat="server"
            AutoGenerateColumns="False" DataKeyNames="KitapID">
            <Columns>
                
                <asp:HyperLinkField DataNavigateUrlFields="KullaniciAdi"
                        DataNavigateUrlFormatString="/kullanici/{0}"
                        DataTextField="KullaniciAdi" HeaderText="Kullanıcı Adı" />
                <asp:BoundField DataField="Inceleme" HeaderText="İnceleme" />
            </Columns>
        </asp:GridView>
        <div class="row" style="padding: 10px 0px 10px 20px; color: #212121; border-bottom: 1px solid #BDBDBD; margin-bottom: 15px;">
            <h4>Alıntılar</h4>
        </div>
        <asp:GridView ID="Alintilar1" runat="server"
            AutoGenerateColumns="False" DataKeyNames="KitapID">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="KullaniciAdi"
                    DataNavigateUrlFormatString="/kullanici/{0}"
                    DataTextField="KullaniciAdi" HeaderText="Kullanıcı Adı" />
                <asp:BoundField DataField="SayfaNo" HeaderText="Sayfa No" />
                <asp:BoundField DataField="Cumle" HeaderText="Cümle" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="kullaniciNotLoggedin" runat="server">
</asp:Content>
