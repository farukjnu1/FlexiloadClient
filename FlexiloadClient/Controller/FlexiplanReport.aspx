<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="FlexiplanReport.aspx.cs" Inherits="FlexiloadClient.Controller.FlexiplanReport" %>

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
    <div id="d" runat="server" class="" role="alert"></div>
    <div class="height-10"></div>
    <h3><i class="glyphicon glyphicon-book"></i>&nbsp;Flexiplan Report</h3>
    <hr />
    <label for="tbStartTime">Start Time</label>
    <asp:TextBox ID="tbStartTime" runat="server" TextMode="Date" required="true" Height="32px" Width="180px"></asp:TextBox>&nbsp;&nbsp;
    <label for="tbEndTime">End Time</label>
    <asp:TextBox ID="tbEndTime" runat="server" TextMode="Date" required="true" Height="32px" Width="180px"></asp:TextBox>&nbsp;&nbsp;
    <label for="tbEndTime">Plan Status</label>
    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" Height="32px" Width="180px">
        <asp:ListItem>done</asp:ListItem>
        <asp:ListItem>undone</asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-primary" OnClick="btnShow_Click">
        <i aria-hidden="true" class="glyphicon glyphicon-eye-open"></i> SHOW
    </asp:LinkButton>
    <div class="height-10"></div>
    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover table-responsive" 
        ShowHeaderWhenEmpty="true" EmptyDataText="date not nount" EmptyDataRowStyle-BackColor="Beige">
    </asp:GridView>
</asp:Content>
