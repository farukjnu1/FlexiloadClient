<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="FlexiplanSubmit.aspx.cs" Inherits="FlexiloadClient.Controller.FlexiplanSubmit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn-primary:hover { border-color: #286090; background-color: #EEEEEE; color:#286090 }
        .btn-warning:hover { border-color: #F0AD4E; background-color: white; color:#F0AD4E }
        h3 {
            text-align:center;
            color:blue
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3><i class="glyphicon glyphicon-thumbs-up"></i>&nbsp;Flexi Plan Submit</h3>
    <hr />
    <div id="d" runat="server" class="" role="alert">
    </div>
    <div class="row">
        <div class="col-md-4">
            <div class="form-group">
                <label for="tbCode">Phone</label>
                <label id="lblPhone" runat="server" class="form-control"></label>
            </div>
            <div class="form-group">
                <label for="tbCode">Order No.</label>
                <asp:TextBox ID="tbCode" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tbTaka">Plan Cost</label>
                <asp:TextBox ID="tbTaka" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-inline">
                <asp:LinkButton ID="lnkSubmit" runat="server" CommandName="Send" CssClass="btn btn-primary" OnClick="lnkSubmit_Click">
                    <i aria-hidden="true" class="glyphicon glyphicon-thumbs-up"></i> SUBMIT
                </asp:LinkButton>
                <a href="FlexiPlan.aspx" class="btn btn-warning"><i class="glyphicon glyphicon-remove-circle"></i>&nbsp;Cancel</a>
            </div>
        </div>
        <div class="col-md-4"></div>
        <div class="col-md-4"></div>
    </div>
</asp:Content>
