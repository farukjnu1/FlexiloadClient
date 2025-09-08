<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="RefillMany.aspx.cs" Inherits="FlexiloadClient.Controller.RefillMany" %>

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
        Remaining Account Balance
                <asp:Label ID="Label1" runat="server" Text="" ForeColor="Green"></asp:Label>
        Tk.
                Note: Uncheck SIM number will be refilled
    </div>
    <h3><img src="../images/my-gp-logo.png" height="25px" />&nbsp;Bulk Refill by Grameenphone</h3>
            <hr />
    <div class="row">
        <div class="col-md-8">
            <label for="tbAm">Amount</label>
                <asp:TextBox ID="tbAm" runat="server" placeholder="amount" TextMode="Number" Text="10" required="true" Width="180px" Height="32px"></asp:TextBox>&nbsp;&nbsp;
            <label for="tbDate">Date</label>
                <asp:TextBox ID="tbDate" runat="server" placeholder="input a date" TextMode="Date" required="true" Width="180px" Height="32px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkShow" runat="server" CommandName="Send" CssClass="btn btn-primary" OnClick="lnkShow_Click">
                    <i aria-hidden="true" class="glyphicon glyphicon-eye-open"></i> Show
                </asp:LinkButton>
        </div>
        <div class="col-md-4">
            Today <strong><%= DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss") %></strong>
        </div>
    </div>
    <br /><br />
    <div id="div" runat="server" role="alert" class=""></div>
    <asp:GridView ID="gridserach" runat="server" AutoGenerateColumns="false"
        ShowFooter="true" DataKeyNames="strTESim"
        EmptyDataText="data not found ! keep trying" EmptyDataRowStyle-BackColor="Beige" CssClass="table table-bordered table-hover table-responsive">
        <EmptyDataRowStyle BackColor="Beige"></EmptyDataRowStyle>
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkheader" runat="server" Text="All"
                        AutoPostBack="true" OnCheckedChanged="chkheader_CheckedChanged" />&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkSend" runat="server" CommandName="Send" CssClass="btn btn-primary" OnClick="lnkSend_Click">
                         <i aria-hidden="true" class="glyphicon glyphicon-send"></i> Send
                    </asp:LinkButton>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkinside" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Car No." DataField="strCarNum" />
            <asp:BoundField HeaderText="SIM" DataField="strTESim" />
            <asp:BoundField HeaderText="Validity" DataField="strOwnerAddress" />
            <asp:BoundField HeaderText="Owner Name" DataField="strOwnerName" />
        </Columns>
    </asp:GridView>
</asp:Content>
