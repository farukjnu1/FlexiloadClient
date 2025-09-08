<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="Pending.aspx.cs" Inherits="FlexiloadClient.Controller.Pending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn-danger {
            border-color: #edb6b6;
            background-color: whitesmoke;
            color: #C9302C;
        }
        .btn-danger:hover {
            border-color: #f7e3e3;
            background-color: #C9302C;
            color: #f7e3e3;
        }
        h3 {
            text-align:center;
            color:blue
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><i class="glyphicon glyphicon-list"></i>&nbsp;Flexiload in the Pending List</h3>
    <hr />
    <div id="d" runat="server" class="" role="alert"></div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass="table table-bordered table-hover table-responsive" AllowPaging="True" AllowSorting="True" ShowHeaderWhenEmpty="True" EmptyDataText="data not fount" EmptyDataRowStyle-BackColor="Beige" DataKeyNames="ID">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Phone_Number" HeaderText="Phone_Number" SortExpression="Phone_Number" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
            <asp:BoundField DataField="Request_From" HeaderText="Request_From" SortExpression="Request_From" />
            <asp:BoundField DataField="Date_Time" HeaderText="Date_Time" SortExpression="Date_Time" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" CssClass="btn btn-danger">
                        <i aria-hidden="true" class="glyphicon glyphicon-remove"></i> Delete
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
<EmptyDataRowStyle BackColor="Beige"></EmptyDataRowStyle>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FlexiloadConnectionString %>"
        DeleteCommand="DELETE FROM [Pending] WHERE [ID] = @ID" SelectCommand="SELECT * FROM [Pending] WHERE Status = 0">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>
