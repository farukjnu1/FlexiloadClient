<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="userCreate.aspx.cs" Inherits="FlexiloadClient.Controller.userCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn-primary:hover {
            border-color: #286090;
            background-color: #EEEEEE;
            color: #286090;
        }

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
    <div class="row">
        <div class="col-md-4"></div>
        <div class="col-md-4">
            <h3><i class="glyphicon glyphicon-user"></i>&nbsp;Create New User</h3>
            <hr />
            <div class="form-group">
                <label for="tbUn">User name</label>
                <asp:TextBox ID="tbUn" runat="server" CssClass="form-control" placeholder="user name" required="" MaxLength="25"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tbPw">Password</label>
                <asp:TextBox ID="tbPw" runat="server" CssClass="form-control" placeholder="password" TextMode="Password" required="" MaxLength="25"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:LinkButton ID="btnCreate" runat="server" CssClass="btn btn-primary" OnClick="btnCreate_Click">
                        <i aria-hidden="true" class="glyphicon glyphicon-user"></i> CREATE
                </asp:LinkButton>
            </div>
        </div>
        <div class="col-md-4"></div>
    </div>
    <div id="d" runat="server" class="" role="alert">
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="USR_ID" DataSourceID="SqlDataSource1" CssClass="table table-bordered table-hover table-responsive">
        <Columns>
            <asp:BoundField DataField="USR_ID" HeaderText="USR_ID" ReadOnly="True" SortExpression="USR_ID" />
            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
            <asp:BoundField DataField="Privilege" HeaderText="Privilege" SortExpression="Privilege" />
            <asp:BoundField DataField="Supervision" HeaderText="Supervision" SortExpression="Supervision" />
            <asp:TemplateField HeaderText="View">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkInactive" OnClick="lnkInactive_Click" CssClass="btn btn-danger">
                        <i aria-hidden="true" class="glyphicon glyphicon-remove-circle"></i> inActive
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:FlexiloadConnectionString %>"
        SelectCommand=""></asp:SqlDataSource>
</asp:Content>
