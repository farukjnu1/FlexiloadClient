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
    public partial class userCreate : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["FlexiloadConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;

        string priv;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Uname"] != null)
            {
                if (Session["privilege"].ToString() == "3")
                {
                    Response.Redirect("~/Controller/home.aspx");
                }
                else if (Session["privilege"].ToString() == "2")
                {
                    SqlDataSource1.SelectCommand = "SELECT * FROM [tbl_User] WHERE Privilege = 3";
                }
                else if (Session["privilege"].ToString() == "1")
                {
                    SqlDataSource1.SelectCommand = "SELECT * FROM [tbl_User] WHERE Privilege = 2";
                }
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (Session["Uname"] != null)
            {
                string pw = Encrypt(tbPw.Text.Trim());
                string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";

                if (tbUn.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> username is required.";
                }
                else if (tbPw.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> password is required.";
                }
                else if (tbUn.Text.Trim().Length < 5)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> minimum character 5 for user name.";
                }
                else if (pw.Length < 18)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> minimum character 6 for password.";
                }
                else if (tbUn.Text.Trim().Length > 25)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> maximum character 25 for user name.";
                }
                else if (pw.Length > 100)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> maximum character 25 for password.";
                }
                else
                {
                    con = new SqlConnection(conStr);
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    if (Session["privilege"].ToString() == "1")
                    {
                        priv = "2";
                    }
                    else
                    {
                        priv = "3";
                    }
                    cmd.CommandText = "insert into tbl_User values(@un, @pw, @am, @priv, @super)";
                    cmd.Parameters.Add(new SqlParameter("un", tbUn.Text));
                    cmd.Parameters.Add(new SqlParameter("pw", pw));
                    cmd.Parameters.Add(new SqlParameter("am", "0"));
                    cmd.Parameters.Add(new SqlParameter("priv", priv));
                    cmd.Parameters.Add(new SqlParameter("super", Session["Uname"].ToString()));
                    
                    cmd.ExecuteNonQuery();
                    con.Close();

                    GridView1.DataBind();
                    Refresh();

                    d.Attributes["class"] = "alert alert-success";
                    d.InnerHtml = "" + button + "<strong>Success!</strong> user created.";
                }
            }
        }

        public void Refresh()
        {
            tbUn.Text = "";
            tbPw.Text = "";
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                System.Security.Cryptography.Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, encryptor.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        protected void lnkInactive_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string un = grdrow.Cells[0].Text;
            con = new SqlConnection(conStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "update tbl_User set Privilege = @pri where USR_ID = @uname";
            cmd.Parameters.Add(new SqlParameter("uname", un));
            cmd.Parameters.Add(new SqlParameter("pri", "0"));
            cmd.ExecuteNonQuery();
            con.Close();

            GridView1.DataBind();
        }
    }
}