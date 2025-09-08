<%@ Page Title="" Language="C#" MasterPageFile="~/Controller/Site.Master" AutoEventWireup="true" CodeBehind="FlexiPlan.aspx.cs" Inherits="FlexiloadClient.FlexiPlan" %>

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
    <div id="d" runat="server" class="" role="alert">
    </div>
    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <h3><i class="glyphicon glyphicon-pencil"></i>&nbsp;Flexi Plan</h3>
            <hr />
            <div class="form-inline">
                <label for="ddlPhone">Phone</label>
                <asp:DropDownList ID="ddlPhone" runat="server" 
                    CssClass="form-control" 
                    DataSourceID="SqlDataSource1" 
                    DataValueField="ID" 
                    DataTextField="Phone">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:GPSDBConnectionString %>" 
                    SelectCommand="SELECT [ID], [Phone] FROM [FlexiPlan] WHERE [Status] = 0">
                </asp:SqlDataSource>
                <asp:LinkButton ID="lnkShow" 
                    runat="server" 
                    CommandName="Send" 
                    CssClass="btn btn-primary"
                    OnClick="lnkShow_Click">
                    <i aria-hidden="true" class="glyphicon glyphicon-eye-open"></i> SHOW
                </asp:LinkButton>
            </div>

        </div>
        <div class="col-md-4">
        </div>
    </div>
    <div class="height-10"></div>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="gvFlexiplan" runat="server" CssClass="table table-bordered table-hover table-responsive" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found!" OnRowCommand="gvFlexiplan_RowCommand">
                <Columns>
                    <asp:ButtonField ButtonType="Link" Text="" HeaderText="Flexiplan" HeaderStyle-ForeColor="#437AA9" CommandName="plan" ControlStyle-CssClass="btn glyphicon glyphicon-send" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
