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
    public partial class OtherLoad : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["FlexiloadConnectionString"].ConnectionString;
        string conStr1 = ConfigurationManager.ConnectionStrings["GPSDBConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rd;

        SqlConnection con1;
        SqlCommand cmd1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Uname"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        public void Refresh()
        {
            tbPhone.Text = "";
            tbAmount.Text = "";
        }

        public bool IsNumber(string s)
        {
            return s.All(char.IsDigit);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["Uname"] != null)
            {
                string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                if (tbPhone.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> phone is required";
                }
                else if (tbAmount.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> amount is required";
                }
                else if (tbOrderNo.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> order no. is required";
                }
                else if (tbPlanCost.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> plan cost is required";
                }
                else if (tbValidity.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> plan cost is required";
                }
                else if (tbPhone.Text.Trim().Length > 14)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> maximum characters for phone is 14";
                }
                else if (!IsNumber(tbAmount.Text.Trim()))
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> input number in amount field";
                }
                else if (Convert.ToInt32(tbAmount.Text.Trim()) < 10)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> minimum recharge amount 10 tk.";
                }
                else if (tbOrderNo.Text.Length > 20)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> maximum characters for phone is 20";
                }
                else if (tbPlanCost.Text.Length > 20)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> input a TK. in Plan Cost field.";
                }
                else
                {
                    // update in Table_Car
                    string date;
                    string validity;
                    int validityLength = 0;
                    con = new SqlConnection(conStr1);
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "SELECT strOwnerAddress FROM Table_Car WHERE strTESim LIKE '%" + tbPhone.Text.Trim() + "%'";
                    rd = cmd.ExecuteReader();
                    rd.Read();
                    if (rd.HasRows)
                    {
                        date = Convert.ToDateTime(tbValidity.Text).ToString("yyyy-MM-dd");
                        validity = rd.GetString(0);
                        validityLength = validity.Length;
                        validityLength = validityLength - 21;
                        validity = validity.Substring(21, validityLength);
                        validity = validity.Insert(0, "" + date + "," + date + "");

                        // in FlexiPlan table
                        if (GetOperator(tbPhone.Text.Trim()) == "gp") // 22-Sep-2016
                        {
                            con1 = new SqlConnection(conStr1);
                            cmd1 = new SqlCommand();
                            cmd1.Connection = con1;
                            con1.Open();
                            cmd1.CommandText = "UPDATE [Table_Car] SET strOwnerAddress = '" + validity + "' where strTESim LIKE '%" + tbPhone.Text.Trim() + "%'";
                            cmd1.ExecuteNonQuery();
                            con1.Close();

                            // in FlexiPlan table
                            con1 = new SqlConnection(conStr1);
                            cmd1 = new SqlCommand();
                            cmd1.Connection = con1;
                            con1.Open();
                            cmd1.CommandText = "INSERT INTO [FlexiPlan] VALUES('" + tbPhone.Text.Trim() + "', '" + tbOrderNo.Text.Trim() + "', " + tbAmount.Text.Trim() + ", '" + DateTime.Now + "', '" + DateTime.Now + "', 1, '" + Session["Uname"].ToString() + "', '" + Session["Uname"].ToString() + "')";
                            cmd1.ExecuteNonQuery();
                            con1.Close();
                        }
                    }
                    con.Close();
                    Refresh();
                    d.Attributes["class"] = "alert alert-success";
                    d.InnerHtml = "" + button + "<strong>Success!</strong> balance has been sent to " + tbPhone.Text + "";
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private string GetOperator(string mobile)
        {
            mobile = mobile.Substring(2, 1);
            if (mobile == "9")
            {
                return "bl";
            }
            else if (mobile == "7")
            {
                return "gp";
            }
            else
            {
                return "0";
            }
        }
    }
}