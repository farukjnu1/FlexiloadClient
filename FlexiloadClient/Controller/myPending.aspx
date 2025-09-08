<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="myPending.aspx.cs" Inherits="FlexiloadClient.Controller.myPending" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover table-responsive" DataSourceID="SqlDataSource1"></asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FlexiloadConnectionString %>" SelectCommand="SELECT * FROM [Pending] WHERE [Request_From] = @Request_From AND [Status] = 0">
        <SelectParameters>
            <asp:SessionParameter Name="Request_From" SessionField="requestFrom" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
