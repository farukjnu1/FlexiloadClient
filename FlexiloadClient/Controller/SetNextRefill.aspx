<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="SetNextRefill.aspx.cs" Inherits="FlexiloadClient.Controller.SetNextRefill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Set Next Refill</h1>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" CssClass="table table-bordered table-responsive">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Operator" HeaderText="Operator" SortExpression="Operator" />
            <asp:BoundField DataField="Days" HeaderText="Days" SortExpression="Days" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FlexiloadConnectionString %>" SelectCommand="SELECT * FROM [NextRefill]" UpdateCommand="UPDATE [NextRefill] SET [Operator] = @Operator, [Days] = @Days WHERE [ID] = @ID">
        <UpdateParameters>
            <asp:Parameter Name="Operator" Type="String" />
            <asp:Parameter Name="Days" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
