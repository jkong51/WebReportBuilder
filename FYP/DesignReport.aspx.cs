using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class DesignReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tdate = DateTime.Now.ToString("yyyy-MM-dd");
            string txtTitle = Session["rptTitle"].ToString();
            string txtDesc = Session["rptDesc"].ToString();
            string wantDate = Session["wantDate"].ToString();

            if(wantDate == "yes")
            {
                lblDate.Text = tdate;
            }
            else if(wantDate == "no")
            {
                lblDate.Text = " ";
            }
            
            lblRptTitle.Text = txtTitle;
            lblRptDesc.Text = txtDesc;

            txtRptTitle.Text = txtTitle;
            txtRptDesc.Text = txtDesc;

            Session.Remove("rptTitle");
            Session.Remove("rptDesc");

        }
    }
}