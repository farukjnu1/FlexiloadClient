<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="OtherLoad.aspx.cs" Inherits="FlexiloadClient.Controller.OtherLoad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn-primary:hover { border-color: #286090; background-color: #EEEEEE; color:#286090 }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-4"></div>
        <div class="col-sm-4">
            <h3>Entry your Refill Info</h3>
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
                <label for="tbOrderNo">Order No.</label>
                <asp:TextBox ID="tbOrderNo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tbPlanCost">Plan Cost</label>
                <asp:TextBox ID="tbPlanCost" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="tbValidity">Validity</label>
                <asp:TextBox ID="tbValidity" runat="server" CssClass="form-control" TextMode="DateTime"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary" OnClick="btnSave_Click">
                    <i aria-hidden="true" class="glyphicon glyphicon-floppy-saved"></i> Save
                </asp:LinkButton>
            </div>
        </div>
        <div class="col-sm-4"></div>
    </div>
    <div id="d" runat="server" class="" role="alert">
        
    </div>
</asp:Content>
