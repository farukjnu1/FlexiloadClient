<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="balanceTransfer.aspx.cs" Inherits="FlexiloadClient.Controller.balanceTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn-primary:hover { border-color: #286090; background-color: #EEEEEE; color:#286090 }
        h3 {
            text-align:center;
            color:blue
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-4">
            <h3><i class="glyphicon glyphicon-send"></i>&nbsp;Transfer balance to User</h3>
            <hr />
            <div class="form-group">
                <label for="ddlUser">select a User</label>
                <asp:DropDownList ID="ddlUser" runat="server" DataSourceID="SqlDataSource1" DataTextField="USR_ID" DataValueField="USR_ID" CssClass="form-control "></asp:DropDownList>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FlexiloadConnectionString %>" SelectCommand="SELECT [USR_ID] FROM [tbl_User] WHERE Supervision = @username">
                    <SelectParameters>
                        <asp:SessionParameter Name="userName" SessionField="username" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </div>
            <div class="form-group">
                <label for="tbAm">Amount</label>
                <asp:TextBox ID="tbAm" runat="server" CssClass="form-control" placeholder="amount" TextMode="Number" required=""></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:LinkButton ID="btnTrans" runat="server" CssClass="btn btn-primary" OnClick="btnTrans_Click">
                        <i aria-hidden="true" class="glyphicon glyphicon-transfer"></i> TRANSFER
                </asp:LinkButton>
            </div>
        </div>
        <div class="col-md-4"></div>
    </div>
    <div id="d" runat="server" class="" role="alert">
        
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="USR_ID" DataSourceID="SqlDataSource2" CssClass="table table-bordered table-hover table-responsive">
        <Columns>
            <asp:BoundField DataField="USR_ID" HeaderText="USR_ID" ReadOnly="True" SortExpression="USR_ID" />
            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
            <asp:BoundField DataField="Privilege" HeaderText="Privilege" SortExpression="Privilege" />
            <asp:BoundField DataField="Supervision" HeaderText="Supervision" SortExpression="Supervision" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:FlexiloadConnectionString %>" SelectCommand="SELECT * FROM [tbl_User] WHERE Supervision = @username">
        <SelectParameters>
            <asp:SessionParameter Name="userName" SessionField="username" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
