using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlexiloadClient.Controller
{
    public partial class refillDetail : System.Web.UI.Page
    {
        string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
        string conStr = ConfigurationManager.ConnectionStrings["FlexiloadConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Uname"] != null)
            {
                this.Session["userName"] = Session["Uname"];
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (tbStartTime.Text.Trim() == string.Empty)
            {
                d.Attributes["class"] = "alert alert-danger";
                d.InnerHtml = "" + button + "<strong>Failure!</strong> start date is required.";
            }
            else if (tbEndTime.Text.Trim() == string.Empty)
            {
                d.Attributes["class"] = "alert alert-danger";
                d.InnerHtml = "" + button + "<strong>Failure!</strong> end date is required.";
            }
            else if (Session["privilege"].ToString() == "3")
            {
                //SqlDataSource1.SelectCommand = "SELECT * FROM [Successfull] WHERE Request_From = @username AND Date_Time BETWEEN '" + Convert.ToDateTime(tbStartTime.Text).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(tbEndTime.Text).ToString("yyyy-MM-dd") + "' ORDER BY Date_Time DESC";
                con = new SqlConnection(conStr);
                cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT * FROM [Successfull] WHERE Request_From = " + Session["Uname"].ToString() + " AND Date_Time BETWEEN '" + Convert.ToDateTime(tbStartTime.Text).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(tbEndTime.Text).ToString("yyyy-MM-dd") + "' ORDER BY Date_Time DESC";
                rd = cmd.ExecuteReader();
                //rd.Read();
                gvRefillDetail.DataSource = rd;
                gvRefillDetail.DataBind();
                d.Attributes["class"] = "alert alert-info";
                d.InnerHtml = "" + button + "Refilled by <i>" + Session["Uname"].ToString() + "</i> From <strong>" + Convert.ToDateTime(tbStartTime.Text).ToString("yyyy-MMM-dd") + "</strong> To <strong>" + Convert.ToDateTime(tbEndTime.Text).ToString("yyyy-MMM-dd") + "</strong>";
            }
            else
            {
                //SqlDataSource1.SelectCommand = "SELECT * FROM [Successfull] WHERE Date_Time BETWEEN '" + Convert.ToDateTime(tbStartTime.Text).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(tbEndTime.Text).ToString("yyyy-MM-dd") + "' ORDER BY Date_Time DESC";
                con = new SqlConnection(conStr);
                cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT * FROM [Successfull] WHERE Date_Time BETWEEN '" + Convert.ToDateTime(tbStartTime.Text).ToString("yyyy-MM-dd") + "' AND '" + Convert.ToDateTime(tbEndTime.Text).ToString("yyyy-MM-dd") + "' ORDER BY Date_Time DESC";
                rd = cmd.ExecuteReader();
                rd.Read();
                gvRefillDetail.DataSource = rd;
                gvRefillDetail.DataBind();
                d.Attributes["class"] = "alert alert-info";
                d.InnerHtml = "" + button + "All Refills From <strong>" + Convert.ToDateTime(tbStartTime.Text).ToString("yyyy-MMM-dd") + "</strong> To <strong>" + Convert.ToDateTime(tbEndTime.Text).ToString("yyyy-MMM-dd") + "</strong>";
            }
        }
    }
}