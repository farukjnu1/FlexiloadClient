using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlexiloadClient.Controller
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Uname"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                lblUsername.Text = "Welcome, " + Session["Uname"].ToString() + " !";
            }
        }

        protected void lBtnSingOut_Click(object sender, EventArgs e)
        {
            Session.Remove("Uname");
            Session.Remove("privilege");
            Response.Redirect("~/Default.aspx");
        }
    }
}