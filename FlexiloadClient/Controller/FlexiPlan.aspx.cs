using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlexiloadClient
{
    public partial class FlexiPlan : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["GPSDBConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlPhone.DataBind();
            }
        }

        public void Refresh()
        {
            gvFlexiplan.DataSource = null;
            gvFlexiplan.DataBind();
        }

        public bool IsNumber(string s)
        {
            return s.All(char.IsDigit);
        }

        public void bindRecord()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.
            ConnectionStrings["GPSDBConnectionString"].ConnectionString.ToString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT strTEID AS [Terminal ID], strCarNum AS [Car No.], strTESim AS [Vehicle SIM], strGroupName AS [Group Name], strOwnerName AS [ Owner Name], strOwnerAddress AS [Validity] FROM Table_Car WHERE strTESim LIKE '%" + ddlPhone.SelectedItem.ToString().Trim() + "%' AND strCarNum NOT LIKE '%Off'";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmd.ExecuteNonQuery();
            gvFlexiplan.DataSource = ds;
            gvFlexiplan.DataBind();
            cmd.Dispose();
            con.Close();
        }

        protected void lnkShow_Click(object sender, EventArgs e)
        {
            if (ddlPhone.Text.Trim() == string.Empty)
            {
                d.Attributes["class"] = "alert alert-danger";
                d.InnerHtml = "" + button + "<strong>Failure!</strong> required a Phone no.";
            }
            else
            {
                bindRecord();
            }
        }

        protected void gvFlexiplan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "plan")
            {
                //get a single record data by row index
                string strPhone = string.Empty;
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                strPhone = gvFlexiplan.Rows[index].Cells[3].Text.TrimEnd();
                if (strPhone.Contains("+88"))
                {
                    if (strPhone.Length > 13)
                    {
                        strPhone = strPhone.Substring(3, 11);
                    }
                    else
                    {
                        d.Attributes["class"] = "alert alert-danger";
                        d.InnerHtml = "" + button + "<strong>Failure!</strong> Terminal SIM is not valid, contact with admin";
                    }
                }
                Response.Redirect("~/Controller/FlexiplanSubmit.aspx?phone=" + strPhone + "");
            }
        }
    }
}