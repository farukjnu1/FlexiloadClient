<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="refillDetail.aspx.cs" Inherits="FlexiloadClient.Controller.refillDetail" %>
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
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <h3><i class="glyphicon glyphicon-list-alt"></i>&nbsp;Refill Details</h3>
            <hr />
            <div class="row">
                <div class="col-sm-2">
                    <label for="tbStartTime" class="span1">Start Time</label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="tbStartTime" runat="server" TextMode="Date" CssClass="form-control" required=""></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <label for="tbEndTime" class="span1">End Time</label>
                </div>
                <div class="col-sm-3">
                    <asp:TextBox ID="tbEndTime" runat="server" TextMode="Date" CssClass="form-control" required=""></asp:TextBox>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="btnShow" runat="server" CssClass="btn btn-primary" OnClick="btnShow_Click">
                        <i aria-hidden="true" class="glyphicon glyphicon-eye-open"></i> SHOW
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="col-md-8"></div>
    </div>
    <div class="height-10"></div>
    <asp:GridView ID="gvRefillDetail" runat="server" CssClass="table table-bordered table-hover table-responsive" EmptyDataText="data not fount" EmptyDataRowStyle-BackColor="Beige">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
