using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FYP
{
    public partial class ChooseTemplate1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String rptTitle = txtRptTitle.Text;
            String rptDesc = txtRptDesc.Text;            
            Session.Add("rptTitle", rptTitle);
            Session.Add("rptDesc", rptDesc);
            String tDate = "yes";
            if (chkDate.Checked)
            {
                tDate = "yes";
            }
            else
            {
                tDate = "no";
            }
            Session.Add("wantDate", tDate);
            Response.Redirect("~/DesignReport.aspx");
        }
    }
}