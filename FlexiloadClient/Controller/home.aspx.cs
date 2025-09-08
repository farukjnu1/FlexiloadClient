using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlexiloadClient.Controller
{
    public partial class home : System.Web.UI.Page
    {
        string FlexiConStr = ConfigurationManager.ConnectionStrings["FlexiloadConnectionString"].ConnectionString;
        string GpsConStr = ConfigurationManager.ConnectionStrings["GPSDBConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rd;
        SqlDataReader rd1;

        SqlConnection con1;
        SqlCommand cmd1;
        string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Uname"] != null)
            {
                ShowBalance(Session["Uname"].ToString());
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        public void ShowBalance(string username)
        {
            con = new SqlConnection(FlexiConStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Amount FROM [tbl_User] WHERE USR_ID='" + username + "'";
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                Label1.ForeColor = Color.Green;
                Label1.Text = rd.GetInt32(0).ToString();
                rd.Close();
                con.Close();
            }
            else
            {
                rd.Close();
                con.Close();
            }
        }

        public int GetBalance(string username)
        {
            con = new SqlConnection(FlexiConStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Amount FROM [tbl_User] WHERE USR_ID='" + username + "'";
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string strPhone = tbPhone.Text.Trim();
            string strAmount = tbAmount.Text.Trim();
            int nAmount = 0;
            if (strPhone.Contains("'"))
            {
                d.Attributes["class"] = "alert alert-danger";
                d.InnerHtml = "" + button + "<strong>Failure!</strong> Input a valid Terminal/Device SIM number";
            }
            else if (!Int32.TryParse(strAmount, out nAmount))
            {
                d.Attributes["class"] = "alert alert-danger";
                d.InnerHtml = "" + button + "<strong>Failure!</strong> Input a valid amount";
            }
            else
            {
                con1 = new SqlConnection(GpsConStr);
                cmd1 = new SqlCommand();
                cmd1.Connection = con1;
                con1.Open();
                cmd1.CommandText = "SELECT nID FROM [Table_Car] WHERE strTESim LIKE '%" + strPhone + "%' AND strCarNum NOT LIKE '%off%'";
                rd1 = cmd1.ExecuteReader();
                rd1.Read();

                if (Session["Uname"] != null)
                {
                    if (strPhone == string.Empty)
                    {
                        d.Attributes["class"] = "alert alert-danger";
                        d.InnerHtml = "" + button + "<strong>Failure!</strong> phone is required";
                    }
                    else if (strAmount == string.Empty)
                    {
                        d.Attributes["class"] = "alert alert-danger";
                        d.InnerHtml = "" + button + "<strong>Failure!</strong> amount is required";
                    }
                    else if (strPhone.Length > 14)
                    {
                        d.Attributes["class"] = "alert alert-danger";
                        d.InnerHtml = "" + button + "<strong>Failure!</strong> maximum characters for phone is 14";
                    }
                    else if (!IsNumber(strAmount))
                    {
                        d.Attributes["class"] = "alert alert-danger";
                        d.InnerHtml = "" + button + "<strong>Failure!</strong> input number in amount field";
                    }
                    else if (nAmount < 10)
                    {
                        d.Attributes["class"] = "alert alert-danger";
                        d.InnerHtml = "" + button + "<strong>Failure!</strong> minimum recharge amount 10 tk.";
                    }
                    else if (GetBalance(Session["Uname"].ToString()) < nAmount)
                    {
                        d.Attributes["class"] = "alert alert-danger";
                        d.InnerHtml = "" + button + "<strong>Failure!</strong> You do not have sufficient balance to complete this transaction.";
                    }
                    else if (GetOperator(strPhone) == "0")
                    {
                        rd1.Close();
                        con1.Close();

                        d.Attributes["class"] = "alert alert-danger";
                        d.InnerHtml = "" + button + "<strong>Failure!</strong> This terminal sim does not belong to Akash TrackMe";
                    }
                    else if (!rd1.HasRows)
                    {
                        rd1.Close();
                        con1.Close();

                        d.Attributes["class"] = "alert alert-danger";
                        d.InnerHtml = "" + button + "<strong>Failure!</strong> This Car No. is 'OFF' or does not belong to Akash TrackMe";
                    }
                    else
                    {
                        rd1.Close();
                        con1.Close();

                        // 22-Sep-2016
                        if (GetOperator(strPhone) == "bl")
                        {
                            con = new SqlConnection(FlexiConStr);
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            con.Open();
                            cmd.CommandText = "INSERT INTO [PendingB]  (Phone_Number,Amount,Status,Request_From,Date_Time)VALUES (@ph, @am, 0, @uid, @dtime)";
                            cmd.Parameters.Add(new SqlParameter("ph", strPhone));
                            cmd.Parameters.Add(new SqlParameter("am", nAmount));
                            cmd.Parameters.Add(new SqlParameter("uid", Session["Uname"].ToString()));
                            cmd.Parameters.Add(new SqlParameter("dtime", DateTime.Now.ToString()));
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else if (GetOperator(strPhone) == "gp")
                        {
                            con = new SqlConnection(FlexiConStr);
                            cmd = new SqlCommand();
                            cmd.Connection = con;
                            con.Open();
                            cmd.CommandText = "INSERT INTO [Pending]  (Phone_Number,Amount,Status,Request_From,Date_Time)VALUES (@ph, @am, 0, @uid, @dtime)";
                            cmd.Parameters.Add(new SqlParameter("ph", strPhone));
                            cmd.Parameters.Add(new SqlParameter("am", nAmount));
                            cmd.Parameters.Add(new SqlParameter("uid", Session["Uname"].ToString()));
                            cmd.Parameters.Add(new SqlParameter("dtime", DateTime.Now.ToString()));
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        

                        con = new SqlConnection(FlexiConStr);
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = "UPDATE [tbl_User] SET Amount = Amount-" + nAmount + " WHERE USR_ID ='" + Session["Uname"].ToString().Trim() + "'";
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // update in Table_Car
                        string date;
                        string validity;
                        int validityLength = 0;
                        con = new SqlConnection(GpsConStr);
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = "SELECT strOwnerAddress FROM Table_Car WHERE strTESim LIKE '%" + strPhone + "%'";
                        rd = cmd.ExecuteReader();
                        rd.Read();
                        if (rd.HasRows)
                        {
                            // 22-Sep-2016
                            int nValid = 0;
                            if (GetOperator(strPhone) == "bl")
                            {
                                con1 = new SqlConnection(FlexiConStr);
                                cmd1 = new SqlCommand();
                                cmd1.Connection = con1;
                                con1.Open();
                                cmd1.CommandText = "SELECT Days FROM NextRefill WHERE ID = 2";
                                rd1 = cmd1.ExecuteReader();
                                rd1.Read();
                                if (rd1.HasRows)
                                {
                                    nValid = rd1.GetInt32(0);
                                }
                                con1.Close();
                            }
                            else
                            {
                                con1 = new SqlConnection(FlexiConStr);
                                cmd1 = new SqlCommand();
                                cmd1.Connection = con1;
                                con1.Open();
                                cmd1.CommandText = "SELECT Days FROM NextRefill WHERE ID = 1";
                                rd1 = cmd1.ExecuteReader();
                                rd1.Read();
                                if (rd1.HasRows)
                                {
                                    nValid = rd1.GetInt32(0);
                                }
                                con1.Close();
                            }

                            DateTime today = DateTime.Today;
                            today = today.AddDays(nValid);
                            date = today.ToString("yyyy-MM-dd");
                            validity = rd.GetString(0);
                            validityLength = validity.Length;
                            validityLength = validityLength - 21;
                            validity = validity.Substring(21, validityLength);
                            validity = validity.Insert(0, "" + date + "," + date + "");

                            con1 = new SqlConnection(GpsConStr);
                            cmd1 = new SqlCommand();
                            cmd1.Connection = con1;
                            con1.Open();
                            cmd1.CommandText = "UPDATE [Table_Car] SET strOwnerAddress = '" + validity + "' WHERE strTESim LIKE '%" + strPhone + "%'";
                            cmd1.ExecuteNonQuery();
                            con1.Close();

                            // in FlexiPlan table
                            if (GetOperator(strPhone) == "gp") // 22-Sep-2016
                            {
                                con1 = new SqlConnection(GpsConStr);
                                cmd1 = new SqlCommand();
                                cmd1.Connection = con1;
                                con1.Open();
                                cmd1.CommandText = "INSERT INTO [FlexiPlan] VALUES('" + strPhone + "', NULL, NULL, '" + DateTime.Now + "', NULL, 0, '" + Session["Uname"].ToString() + "', NULL)";
                                cmd1.ExecuteNonQuery();
                                con1.Close();
                            }
                        }
                        rd.Close();
                        con.Close();

                        Refresh();

                        ShowBalance(Session["Uname"].ToString());
                        d.Attributes["class"] = "alert alert-success";
                        d.InnerHtml = "" + button + "<strong>Success!</strong> balance has been sent to " + strPhone + "";
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        private string GetOperator(string mobile)
        {
            mobile = mobile.Substring(2, 1);
            if (mobile == "9")
            {
                return "bl";
            }
            else if (mobile == "7" || mobile == "3")
            {
                return "gp";
            }
            else
            {
                return "0";
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
    }
}