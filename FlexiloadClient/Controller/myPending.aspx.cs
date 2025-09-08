using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FlexiloadClient.Controller
{
    public partial class myPending : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Uname"] != null)
            {
                if (Session["privilege"].ToString() == "2")
                {
                    Response.Redirect("~/Controller/Pending.aspx");
                }
                else
                {
                    this.Session["requestFrom"] = Session["Uname"];
                }
            }
        }
    }
}