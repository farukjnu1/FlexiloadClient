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
    public partial class passwordChange : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["FlexiloadConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rd;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (Session["Uname"] != null)
            {
                string button = "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>";
                string oldPw = Encrypt(tbOldPw.Text.Trim());
                string newPw = Encrypt(tbNewPw.Text.Trim());

                if (tbOldPw.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> all fields are required.";
                }
                else if (tbNewPw.Text.Trim() == string.Empty)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> all fields are required.";
                }
                else if (oldPw.Trim().Length > 100)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> max lenght is 25 characters.";
                }
                else if (newPw.Trim().Length < 18)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> min lenght is 6 characters.";
                }
                else if (newPw.Trim().Length > 100)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> max lenght is 25 characters.";
                }
                else if (tbNewPw.Text != tbConNewPw.Text)
                {
                    d.Attributes["class"] = "alert alert-danger";
                    d.InnerHtml = "" + button + "<strong>Failure!</strong> new and cofirm password didn't match.";
                }
                else
                {
                    con = new SqlConnection(conStr);
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "select * from tbl_User where [Password] = @pw and USR_ID = @un";
                    cmd.Parameters.Add(new SqlParameter("un", Session["Uname"].ToString()));
                    cmd.Parameters.Add(new SqlParameter("pw", oldPw));
                    rd = cmd.ExecuteReader();
                    rd.Read();
                    if (rd.HasRows)
                    {
                        con.Close();

                        con = new SqlConnection(conStr);
                        cmd = new SqlCommand();
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = "update tbl_User set [Password] = @pwNew where USR_ID = @uname";
                        cmd.Parameters.Add(new SqlParameter("uname", Session["Uname"].ToString()));
                        cmd.Parameters.Add(new SqlParameter("pwNew", newPw));
                        cmd.ExecuteNonQuery();
                        con.Close();

                        Refresh();

                        d.Attributes["class"] = "alert alert-success";
                        d.InnerHtml = "" + button + "<strong>Success!</strong> password changed.";
                    }
                    else
                    {
                        d.Attributes["class"] = "alert alert-danger";
                        d.InnerHtml = "" + button + "<strong>Failure!</strong> old password is not valid.";
                    }
                }
            }
        }

        public void Refresh()
        {
            tbOldPw.Text = "";
            tbNewPw.Text = "";
            tbConNewPw.Text = "";
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
    }
}