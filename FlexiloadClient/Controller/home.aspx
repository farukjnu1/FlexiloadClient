<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="FlexiloadClient.Controller.home" %>

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
    <div class="alert alert-info" role="alert">
        Remaining Account Balance:
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        Tk. Note: Plz do not use '<span style="color:red">+88</span>' in Mobile No.
    </div>
    <div class="row">
        <div class="col-sm-4"></div>
        <div class="col-sm-4">
            <h3><i class="glyphicon glyphicon-th-large"></i>&nbsp;Single Refill</h3>
            <hr />
            <div class="form-group">
                <label for="tbPhone" class="control-label">Phone No.</label>
                <asp:TextBox ID="tbPhone" runat="server" class="form-control"
                    placeholder="phone number" ValidationGroup="v1"
                    required="" MaxLength="14" data-minlength="5"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tbAmount" class="control-label">Amount</label>
                <asp:TextBox ID="tbAmount" runat="server" class="form-control"
                    placeholder="amount" ValidationGroup="v1"
                    required="" TextMode="Number"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:LinkButton ID="btnSend" runat="server" CssClass="btn btn-primary" OnClick="btnSend_Click">
                    <i aria-hidden="true" class="glyphicon glyphicon-send"></i> SEND
                </asp:LinkButton>
            </div>
        </div>
        <div class="col-sm-4"></div>
    </div>
    <div id="d" runat="server" class="" role="alert">
        
    </div>
</asp:Content>
