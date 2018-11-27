using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class ChooseRetrieveUses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Timeout = 60;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseReportUpdate.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            Response.Redirect("RetrieveReport.aspx");
        }
    }
}