<%@ Page Title="" Language="C#" MasterPageFile="~/yonetim.Master" AutoEventWireup="true" CodeBehind="yonetim-kitaplar.aspx.cs" Inherits="ProjeKitapDTS.yonetim_kitaplar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="yoneticiHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="yoneticiBody" runat="server">
    <div class="col-9" style="height: 100%; min-height: 1000px;">
        <div class="row" style="padding: 10px 0px 0px 20px;">
            <h1>Kitaplar</h1>
        </div>
        <div class="row" style="padding: 20px 0px 0px 20px;">
            
            <asp:GridView ID="Kitaplar1" runat="server"
                AutoGenerateColumns="False" OnRowCancelingEdit="Kitaplar1_RowCancelingEdit" OnRowDeleting="Kitaplar1_RowDeleting" OnRowEditing="Kitaplar1_RowEditing" OnRowUpdating="Kitaplar1_RowUpdating"
                DataKeyNames="KitapID">
                <Columns>
                    <asp:BoundField DataField="KitapID" HeaderText="Kitap ID"/>
                    <asp:BoundField DataField="YazarID" HeaderText="Yazar"/>
                    <asp:BoundField DataField="Ad" HeaderText="Ad"/>
                    <asp:BoundField DataField="Yayinevi" HeaderText="Yayınevi"/>
                    <asp:BoundField DataField="TanitimBilgisi" HeaderText="Tanıtım Bilgisi"/>
                    <asp:CommandField ShowDeleteButton="True"
                        ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>