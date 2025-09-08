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
    public partial class transactionReport : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["FlexiloadConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rd;
        SqlDataAdapter da;
        DataSet ds;

        int am;
        string fromDate;
        string toDate;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["privilege"]!=null)
            {
                if (Session["privilege"].ToString() == "3")
                {
                    Response.Redirect("~/Controller/home.aspx");
                }
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
            if (tbStartTime.Text == string.Empty)
            {
                d.Attributes["class"] = "alert alert-danger";
                d.InnerHtml = "" + button + "<strong>Failure!</strong> start date is required.";
            }
            else if (tbEndTime.Text == string.Empty)
            {
                d.Attributes["class"] = "alert alert-danger";
                d.InnerHtml = "" + button + "<strong>Failure!</strong> end date is required.";
            }
            else
            {
                con = new SqlConnection(conStr);
                cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select * from Successfull where Date_Time between '" + tbStartTime.Text + "' and '" + tbEndTime.Text + "' order by Date_Time desc";
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                con.Close();
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();

                con = new SqlConnection(conStr);
                cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select sum(Amount) from Successfull where Date_Time between '" + tbStartTime.Text + "' and '" + tbEndTime.Text + "'";
                rd = cmd.ExecuteReader();
                rd.Read();
                if (!rd.IsDBNull(0))
                {
                    am = rd.GetInt32(0);
                }
                fromDate = tbStartTime.Text;
                toDate = tbEndTime.Text;
                d.Attributes["class"] = "alert alert-info";
                d.InnerHtml = "" + button + "Transaction summery <strong>" + am + "</strong> Tk. From " + tbStartTime.Text + " To " + tbEndTime.Text + "";
            }
        }
    }
}