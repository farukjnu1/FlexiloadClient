<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="transactionReport.aspx.cs" Inherits="FlexiloadClient.Controller.transactionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn-primary:hover {
            border-color: #286090;
            background-color: #EEEEEE;
            color: #286090;
        }
        h3 {
            text-align:center;
            color:blue
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <h3><i class="glyphicon glyphicon-book"></i>&nbsp;Transaction Report</h3>
            <hr />
            <label for="tbStartTime">Start Time</label>
            <asp:TextBox ID="tbStartTime" runat="server" TextMode="Date" required="true" Width="180px" Height="32"></asp:TextBox>&nbsp;&nbsp;
            <label for="tbEndTime">End Time</label>
            <asp:TextBox ID="tbEndTime" runat="server" TextMode="Date" required="true" Width="180px" Height="32"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-primary" OnClick="btnShow_Click">
                        <i aria-hidden="true" class="glyphicon glyphicon-eye-open"></i> SHOW
            </asp:LinkButton>
        </div>
        <div class="col-md-2"></div>
    </div>
    <br />
    <div id="d" runat="server" class="" role="alert">
    </div>

    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover table-responsive"></asp:GridView>
</asp:Content>
