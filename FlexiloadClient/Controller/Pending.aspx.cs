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
    public partial class Pending : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Uname"] != null) 
            {
                if (Session["privilege"].ToString() == "3")
                {
                    Response.Redirect("~/Controller/myPending.aspx");
                }
            }
        }

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (Session["privilege"] != null) 
        //    {
        //        if (Session["privilege"].ToString() == "2")
        //        {
        //            if (e.CommandName == "Del")
        //            {
        //                int index = Convert.ToInt32(e.CommandArgument);
        //                GridViewRow selectedRow = GridView1.Rows[index];
        //                TableCell tcId = selectedRow.Cells[0];

        //                con = new SqlConnection(conStr);

        //                cmd = new SqlCommand();
        //                cmd.Connection = con;
        //                con.Open();
        //                cmd.CommandText = "DELETE FROM [Pending] WHERE ID = @id";
        //                cmd.Parameters.Add(new SqlParameter("id", tcId.Text));
        //                cmd.ExecuteNonQuery();
        //                con.Close();
        //                GridView1.DataBind();

        //                d.Attributes["class"] = "alert alert-success";
        //                d.InnerHtml = "" + button + "<strong>Success!</strong> record deleted.";
        //            }
        //        }
        //        else
        //        {
        //            d.Attributes["class"] = "alert alert-danger";
        //            d.InnerHtml = "" + button + "<strong>Failure!</strong> You do not have permission to delete Pending List.";
        //        }
        //    }
        //}
    }
}