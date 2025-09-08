using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlexiloadClient
{
    public partial class SiteOuter : System.Web.UI.MasterPage
    {
        string conStr = ConfigurationManager.ConnectionStrings["FlexiloadConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader rd;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            
            string pw = Encrypt(tbPassword.Text.Trim());
            //string decr = Decrypt(pw);

            if (tbUsername.Text.Trim() == string.Empty)
            { }
            else if (tbPassword.Text.Trim() == string.Empty)
            { }
            else if (tbUsername.Text.Trim().Length > 25)
            { }
            else if (pw.Length > 100)
            { }
            else
            {
                bool isValid = false;
                con = new SqlConnection(conStr);
                cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT * FROM [tbl_User] WHERE USR_ID='" + tbUsername.Text + "' AND Password='" + pw + "' AND Privilege <> 0";
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows)
                {
                    Session["Uname"] = rd.GetString(0);
                    Session["privilege"] = rd.GetString(3);
                    isValid = true;
                }
                rd.Close();
                con.Close();

                if (isValid)
                {
                    con = new SqlConnection(conStr);
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "INSERT INTO [Login_Info] VALUES (@uname, @At) ";
                    cmd.Parameters.Add(new SqlParameter("uname", Session["Uname"]));
                    cmd.Parameters.Add(new SqlParameter("At", DateTime.Now));
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("Controller/home.aspx");
                }

            }
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

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
            {
                System.Security.Cryptography.Rfc2898DeriveBytes pdb = new System.Security.Cryptography.Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, encryptor.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = System.Text.Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}