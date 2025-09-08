using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlexiloadClient.Controller
{
    public partial class PwRecover : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["privilege"] == null)
            {
                Response.Redirect("~/Controller/home.aspx");
            }
            else
            {
                var privilege = Session["privilege"].ToString();
                if (privilege == "3")
                {
                    Response.Redirect("~/Controller/home.aspx");
                }
                else if (privilege == "2" || privilege == "1")
                {
                    var un = Request.QueryString["un"].ToString();
                    var msService = new Services.MsSqlService();
                    var tbl_User = msService.Get("FlexiloadConnectionString", "SELECT * FROM [tbl_User] WHERE USR_ID='" + un + "'");
                    foreach (System.Data.DataRow row in tbl_User.Rows)
                    {
                        var criptoService = new Services.CriptoService();

                        USR_ID.InnerText = row["USR_ID"].ToString();
                        Password.InnerText = row["Password"].ToString();
                        clearPassword.InnerText = criptoService.Decrypt(row["Password"].ToString());
                        Amount.InnerText = row["Amount"].ToString();
                        Privilege.InnerText = row["Privilege"].ToString();
                        Supervision.InnerText = row["Supervision"].ToString();
                    }
                }

            }

            
        }
    }
}