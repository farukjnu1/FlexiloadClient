<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="passwordChange.aspx.cs" Inherits="FlexiloadClient.Controller.passwordChange" %>

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
            <h3><i class="glyphicon glyphicon-lock"></i>&nbsp;Change Password</h3>
            <div class="form-group">
                <label for="tbOldPw">Old Password</label>
                <asp:TextBox ID="tbOldPw" runat="server" CssClass="form-control" placeholder="old password" TextMode="Password" MaxLength="25" required=""></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tbNewPw">New Password</label>
                <asp:TextBox ID="tbNewPw" runat="server" CssClass="form-control" placeholder="new password" TextMode="Password" MaxLength="25" required=""></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tbConNewPw">Confirm New Password</label>
                <asp:TextBox ID="tbConNewPw" runat="server" CssClass="form-control" placeholder="confirm new password" TextMode="Password" MaxLength="25" required=""></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:LinkButton ID="btnCreate" runat="server" CssClass="btn btn-primary" OnClick="btnCreate_Click">
                        <i aria-hidden="true" class="glyphicon glyphicon-edit"></i> UPDATE
                </asp:LinkButton>
            </div>
        </div>
        <div class="col-md-4"></div>
    </div>
    <div id="d" runat="server" class="" role="alert">
    </div>
</asp:Content>
