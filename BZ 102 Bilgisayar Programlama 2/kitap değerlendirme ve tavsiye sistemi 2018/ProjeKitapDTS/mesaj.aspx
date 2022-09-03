<%@ Page Title="" Language="C#" MasterPageFile="~/kullanici.Master" AutoEventWireup="true" CodeBehind="mesaj.aspx.cs" Inherits="ProjeKitapDTS.mesaj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="kullaniciHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="kullaniciBody" runat="server">
    <div class="col-6" style="height: 100%; min-height: 1000px;">
        <div class="row" style="margin: 10px 0px 10px 0px;">
           <asp:GridView ID="Mesajlar1" runat="server"
            AutoGenerateColumns="False" DataKeyNames="MesajID">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="KullaniciAdi"
                    DataNavigateUrlFormatString="/kullanici/{0}"
                    DataTextField="KullaniciAdi" HeaderText="Kimden" />
                <asp:BoundField DataField="Baslik" HeaderText="Başlık" />
                <asp:BoundField DataField="Mesaj" HeaderText="Mesaj" />
                <asp:BoundField DataField="Tarih" HeaderText="Tarih" />
            </Columns>
        </asp:GridView>         
        </div>

        <div class="row" style="padding: 0px 0px 0px 20px; border-top: 15px solid #1976D2;">
            <h4>Mesaj Yaz</h4>
        </div>
        <div class="row">
            <div class="w-100" style="padding: 10px 20px 0px 20px;">
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
