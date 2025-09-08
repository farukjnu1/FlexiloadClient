using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlexiloadClient.Controller
{
    public partial class RefillMany : System.Web.UI.Page
    {
        SqlConnection gpsdbCon = new SqlConnection(ConfigurationManager.ConnectionStrings["GPSDBConnectionString"].ConnectionString);
        SqlConnection flexiCon = new SqlConnection(ConfigurationManager.ConnectionStrings["FlexiloadConnectionString"].ConnectionString);
        SqlDataReader rd;
        string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Uname"] != null)
            {
                if (!IsPostBack)
                {
                    ShowBalance(Session["Uname"].ToString());
                }
            }
        }

        public void ShowBalance(string username)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.Connection = flexiCon;
            cmd.Connection.Open();
            cmd.CommandText = "SELECT Amount FROM [dbo].[tbl_User] WHERE USR_ID='" + username + "'";
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                Label1.ForeColor = Color.Green;
                Label1.Text = rd.GetInt32(0).ToString();
                rd.Close();
                cmd.Connection.Close();
            }
            else
            {
                rd.Close();
                cmd.Connection.Close();
            }
        }

        private void BindGv()
        {
            string query = "select strCarNum, strTESim, strOwnerAddress, strOwnerName from Table_Car where strCarNum NOT LIKE '%Off%' AND strOwnerAddress LIKE '%" + tbDate.Text.Trim() + ",201%' AND strTESim NOT LIKE '+88019%'";
            SqlDataAdapter adp = new SqlDataAdapter(query, gpsdbCon);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            gridserach.DataSource = dt;
            gridserach.DataBind();
            //gpsdbCon.Close();
        }

        protected void gridserach_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void chkheader_CheckedChanged(object sender, EventArgs e)
        {
            //Get Checkbox value from HeaderTemplate
            CheckBox chh = (CheckBox)gridserach.HeaderRow.FindControl("chkheader");
            foreach (GridViewRow row in gridserach.Rows)
            {
                //Get Checkbox value from ItemTemplate
                CheckBox chh1 = (CheckBox)row.FindControl("chkinside");
                if (chh.Checked == true)
                {
                    chh1.Checked = true;
                }
                else
                {
                    chh1.Checked = false;
                }
            }
        }

        public int GetBalance(string username)
        {
            string query6 = "SELECT Amount FROM [tbl_User] WHERE USR_ID='" + username + "'";
            SqlCommand cmd2 = new SqlCommand(query6, flexiCon);
            cmd2.Connection = flexiCon;
            cmd2.Connection.Open();
            rd = cmd2.ExecuteReader();
            rd.Read();
            int b = 0;
            if (rd.HasRows)
            {
                b = rd.GetInt32(0);
                rd.Close();
                cmd2.Connection.Close();
                return b;
            }
            else
            {
                rd.Close();
                cmd2.Connection.Close();
                return b;
            }
        }

        protected void lnkSend_Click(object sender, EventArgs e)
        {
            if (Session["Uname"] == null)
            { }
            else if (tbAm.Text.Trim() == string.Empty)
            {
                div.Attributes["class"] = "alert alert-danger";
                div.InnerHtml = "" + button + "<strong>Failure!</strong> amount is required.";
            }
            else if (GetBalance(Session["Uname"].ToString()) < Convert.ToInt32(tbAm.Text.Trim()))
            {
                div.Attributes["class"] = "alert alert-danger";
                div.InnerHtml = "" + button + "<strong>Failure!</strong> You do not have sufficient balance to complete this transaction.";
            }
            else
            {
                // 24 Sep 2016
                int nValid = 0;
                string strQuery = "SELECT Days FROM NextRefill WHERE ID = 1";
                SqlCommand cmd5 = new SqlCommand(strQuery, flexiCon);
                cmd5.Connection = flexiCon;
                cmd5.Connection.Open();
                cmd5.ExecuteNonQuery();
                rd = cmd5.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    nValid = rd.GetInt32(0);
                }
                cmd5.Connection.Close();

                string sim;
                int totalAm = 0;
                string validity = "";
                string d;
                int validityLength = 0;
                foreach (GridViewRow row in gridserach.Rows)
                {
                    CheckBox chh1 = (CheckBox)row.FindControl("chkinside");
                    if (chh1.Checked == false)
                    {
                        //Get Value of Row Id using DataKeyNames
                        sim = Convert.ToString(gridserach.DataKeys[row.RowIndex].Values["strTESim"]);
                        sim = sim.Trim();
                        sim = sim.Substring(3, 11);

                        // 22-Sep-2016
                        if (GetOperator(sim) == "gp")
                        {
                            string query1 = "INSERT INTO [Pending]  (Phone_Number,Amount,Status,Request_From,Date_Time) VALUES ('" + sim + "', '" + tbAm.Text.Trim() + "', '0', '" + Session["Uname"].ToString() + "', '" + DateTime.Now.ToString() + "')";
                            SqlCommand cmd = new SqlCommand(query1, flexiCon);
                            cmd.Connection = flexiCon;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();

                            // amount to be deducted
                            totalAm += Convert.ToInt32(tbAm.Text.Trim());
                        }

                        // code here OwnerAddress in Table_Car
                        string query3 = "select strOwnerAddress from Table_Car where strTESim LIKE '%" + sim + "%'";
                        SqlCommand cmd2 = new SqlCommand(query3, gpsdbCon);
                        cmd2.Connection = gpsdbCon;
                        cmd2.Connection.Open();
                        rd = cmd2.ExecuteReader();
                        rd.Read();
                        if (rd.HasRows)
                        {
                            // 22-Sep-2016

                            DateTime today = DateTime.Today;
                            today = today.AddDays(nValid);
                            d = today.ToString("yyyy-MM-dd");
                            validity = rd.GetString(0);
                            validityLength = validity.Length;
                            validityLength = validityLength - 21;
                            validity = validity.Substring(21, validityLength);
                            validity = validity.Insert(0, "" + d + "," + d + "");
                        }
                        rd.Close();
                        cmd2.Connection.Close();

                        if (nValid > 0)
                        {
                            string query4 = "UPDATE [Table_Car] SET strOwnerAddress = '" + validity + "' where strTESim LIKE '%" + sim + "%'";
                            SqlCommand cmd3 = new SqlCommand(query4, gpsdbCon);
                            cmd3.Connection = gpsdbCon;
                            cmd3.Connection.Open();
                            cmd3.ExecuteNonQuery();
                            cmd3.Connection.Close();
                        }

                        // in FlexiPlan table
                        if (GetOperator(sim.Trim()) == "gp") // 22-Sep-2016
                        {
                            string query5 = "INSERT INTO [FlexiPlan] VALUES('" + sim.Trim() + "', NULL, NULL, '" + DateTime.Now + "', NULL, 0, '" + Session["Uname"].ToString() + "', NULL)";
                            SqlCommand cmd4 = new SqlCommand(query5, gpsdbCon);
                            cmd4.Connection = gpsdbCon;
                            cmd4.Connection.Open();
                            cmd4.ExecuteNonQuery();
                            cmd4.Connection.Close();
                        }
                    }
                }
                string query2 = "UPDATE [tbl_User] SET Amount = Amount-" + totalAm + " WHERE USR_ID ='" + Session["Uname"].ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(query2, flexiCon);
                cmd1.Connection = flexiCon;
                cmd1.Connection.Open();
                cmd1.ExecuteNonQuery();
                cmd1.Connection.Close();

                BindGv();

                div.Attributes["class"] = "alert alert-success";
                div.InnerHtml = "" + button + "<strong>Success!</strong> account refilled";
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

        protected void lnkShow_Click(object sender, EventArgs e)
        {
            if (tbDate.Text.Trim() == string.Empty)
            {
                div.Attributes["class"] = "alert alert-danger";
                div.InnerHtml = "" + button + "<strong>Failure!</strong> input a date to query";
            }
            else
            {
                BindGv();
            }
        }
    }
}