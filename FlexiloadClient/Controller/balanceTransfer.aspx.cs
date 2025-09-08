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
    public partial class balanceTransfer : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["FlexiloadConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["privilege"].ToString() == "3")
            {
                Response.Redirect("~/Controller/home.aspx");
            }
            else if (Session["Uname"] != null)
            {
                this.Session["userName"] = Session["Uname"];
            }
        }

        public bool IsNumber(string s)
        {
            return s.All(char.IsDigit);
        }

        public int GetBalance(string username)
        {
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Amount FROM [dbo].[tbl_User] WHERE USR_ID='" + username + "'";
            rd = cmd.ExecuteReader();
            rd.Read();
            int b = 0;
            if (rd.HasRows)
            {
                b = rd.GetInt32(0);
                rd.Close();
                con.Close();
                return b;
            }
            else
            {
                rd.Close();
                con.Close();
                return b;
            }
        }

        protected void btnTrans_Click(object sender, EventArgs e)
        {
            if (Session["Uname"] != null)
            {
                string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                if (ddlUser.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> user is required.";
                }
                else if (tbAm.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> amount is required.";
                }
                else if (!IsNumber(tbAm.Text))
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> input number in amount";
                }
                else if (Convert.ToInt32(tbAm.Text) < 10)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> minimum recharge amount 10 Tk.";
                }
                else if (GetBalance(Session["Uname"].ToString()) < Convert.ToInt32(tbAm.Text.Trim()))
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> You do not have sufficient balance to complete this transaction.";
                }
                else
                {
                    int am;
                    string un;
                    con = new SqlConnection(conStr);
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "select USR_ID, Amount from tbl_User where USR_ID = @un";
                    cmd.Parameters.Add(new SqlParameter("un", Session["Uname"].ToString()));
                    rd = cmd.ExecuteReader();
                    rd.Read();
                    if (!rd.IsDBNull(0))
                    {
                        un = rd.GetString(0);
                        am = rd.GetInt32(1);
                        rd.Close();
                        con.Close();
                        if (am >= Convert.ToInt32(tbAm.Text))
                        {
                            con = new SqlConnection(conStr);
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            con.Open();
                            cmd.CommandText = "update tbl_User set Amount = Amount - @am where USR_ID = @un";
                            cmd.Parameters.Add(new SqlParameter("am", tbAm.Text));
                            cmd.Parameters.Add(new SqlParameter("un", un));
                            cmd.ExecuteNonQuery();
                            con.Close();

                            con = new SqlConnection(conStr);
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            con.Open();
                            cmd.CommandText = "update tbl_User set Amount = Amount + @amo where USR_ID = @user";
                            cmd.Parameters.Add(new SqlParameter("amo", tbAm.Text));
                            cmd.Parameters.Add(new SqlParameter("user", ddlUser.Text));
                            cmd.ExecuteNonQuery();
                            con.Close();
                            d.Attributes["class"] = "alert alert-success";
                            d.InnerHtml = "" + button + "<strong>Success!</strong> balance transfered.";

                            GridView1.DataBind();
                            Refresh();
                        }
                    }
                }
            }
        }

        public void Refresh()
        {
            tbAm.Text = "";
        }
    }
}