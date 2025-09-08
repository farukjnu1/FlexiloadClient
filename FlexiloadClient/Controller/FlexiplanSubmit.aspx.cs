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
    public partial class FlexiplanSubmit : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["GPSDBConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetValues();
            }
        }

        private void SetValues()
        {
            if (Request.QueryString["phone"] == null)
            {
                Response.Redirect("~/Controller/Flexiplan.aspx");
            }
            else
            {
                string strPhone = Request.QueryString["phone"].Trim();
                lblPhone.InnerText = strPhone;
            }
        }

        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            if (Session["Uname"] != null)
            {
                if (tbCode.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> Order No. is required.";
                }
                else if (tbTaka.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> Taka is required.";
                }
                else if (tbCode.Text.Length > 20)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> maximum characters for order no. is 20";
                }
                else if (tbTaka.Text.Trim().Length > 20)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> input valid amount as Taka.";
                }
                else if (Request.QueryString["phone"] == null)
                {
                    Response.Redirect("~/Controller/Flexiplan.aspx");
                }
                else
                {
                    // update in FlexiPlan table
                    con = new SqlConnection(conStr);
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "UPDATE [FlexiPlan] SET [Code] = '" + tbCode.Text + "', [Amount] = " + tbTaka.Text + ", [PlannedOn] = '" + DateTime.Now.ToString() + "', [Status] = 1, [PlannedBy] = '" + Session["Uname"].ToString() + "' WHERE [Phone] LIKE '%" + lblPhone.InnerText.Trim() + "%'";
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Response.Redirect("~/Controller/Flexiplan.aspx");

                    d.Attributes["class"] = "alert alert-success";
                    d.InnerHtml = "" + button + "<strong>Success!</strong> flexiplan completed.";
                }
            }
        }
    }
}