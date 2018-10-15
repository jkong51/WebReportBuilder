using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            staffNamelbl.Text = (string)Session["staffName"];
            Label2.Text = (string)Session["faculty"];
            Label3.Text = (string)Session["department"];
        }

    }
}