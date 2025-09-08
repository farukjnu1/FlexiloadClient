using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlexiloadClient.Controller
{
    public partial class FlexiplanReport : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["GPSDBConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (tbStartTime.Text.Trim() == string.Empty)
            {
                d.Attributes["class"] = "alert alert-danger";
                d.InnerHtml = "" + button + "<strong>Failure!</strong> Start Date is required.";
            }
            else if (tbEndTime.Text.Trim() == string.Empty)
            {
                d.Attributes["class"] = "alert alert-danger";
                d.InnerHtml = "" + button + "<strong>Failure!</strong> End Date is required.";
            }
            else if (ddlStatus.Text == string.Empty)
            {
                d.Attributes["class"] = "alert alert-danger";
                d.InnerHtml = "" + button + "<strong>Failure!</strong> Status is required.";
            }
            else
            {
                string status;
                string label;
                if (ddlStatus.Text == "done")
                {
                    status = "1";
                    label = "done";
                }
                else
                {
                    status = "0";
                    label = "undone";
                }

                con = new SqlConnection(conStr);
                cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT * FROM [FlexiPlan] WHERE [Status] = " + status + " AND [RechargedOn] BETWEEN '" + tbStartTime.Text + "' AND '" + tbEndTime.Text + "'";
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                cmd.ExecuteNonQuery();
                GridView1.DataSource = ds;
                GridView1.DataBind();

                d.Attributes["class"] = "alert alert-info";
                d.InnerHtml = "" + button + "Flexiplan <i>" + label + "</i> From <strong>" + Convert.ToDateTime(tbStartTime.Text).ToString("yyyy-MMM-dd") + "</strong> To <strong>" + Convert.ToDateTime(tbEndTime.Text).ToString("yyyy-MMM-dd") + "</strong>";
            }
        }
    }
}