<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="mesajlar.aspx.cs" Inherits="ProjeKitapDTS.mesajlar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
    <div class="col-6" style="height: 100%; min-height: 1000px;">
        <div class="row" style="padding: 0px 0px 0px 20px;">
            <h4>Mesaj Atanlar</h4>
        </div>
        <div class="row" style="padding: 0px 0px 0px 20px; margin-bottom: 15px;">
            <asp:GridView ID="MesajAtanlar1" runat="server"
            AutoGenerateColumns="False" DataKeyNames="GonderenID">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="GonderenID"
                    DataNavigateUrlFormatString="/mesajlar/{0}"
                    DataTextField="KullaniciAdi" HeaderText="Kimden" />
                <asp:BoundField DataField="MesajSayisi" HeaderText="Mesaj Sayısı"/>
            </Columns>
        </asp:GridView>
        </div>
        <div class="row" style="padding: 0px 0px 0px 20px; border-top: 15px solid #1976D2;">
            <h4>Birine Mesaj Yaz</h4>
        </div>
        <div class="row">
            <div class="w-100" style="padding: 10px 20px 0px 20px;">
                <div class="form-group">
                    <asp:TextBox runat="server" class="form-control" TextMode="multiline" ID="inputKime" placeholder="Gönderilecek Kullanıcı Adı" Rows="1"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox runat="server" class="form-control" TextMode="multiline" ID="inputBaslik" placeholder="Başlık" Rows="1"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox runat="server" class="form-control" TextMode="multiline" ID="inputMesaj" placeholder="Mesajınız" Rows="7"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="row" style="padding: 0px 20px 0px 20px; border-bottom: 15px solid #1976D2; margin-bottom: 10px;">
            <div class="col"></div>
            <div class="col-3">
                    <asp:Button runat="server" type="submit" class="btn btn-primary" Style="width: 165px; margin-bottom: 10px; background: #448AFF;" ID="btnMesajGonder" Text="Mesajı Gönder" OnClick="btnMesajGonder_Click"></asp:Button>
            </div>            
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="kullaniciNotLoggedin" runat="server">
</asp:Content>
