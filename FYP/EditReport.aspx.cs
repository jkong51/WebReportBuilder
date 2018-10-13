using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class EditReport : System.Web.UI.Page
    {
        private Style primaryStyle = new Style();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtRptTitle.Text = lblRptTitle.Text;
            txtRptDesc.Text = lblRptDesc.Text;
            if (!Page.IsPostBack)
            {
                foreach (FontFamily font in FontFamily.Families)
                {
                    fontFamilyDrpDwnList.Items.Add(font.Name.ToString());
                }
                lblRptDesc.Text = txtRptDesc.Text;
                lblRptTitle.Text = txtRptTitle.Text;
            }
        }

        protected void ChangeFont(object sender, EventArgs e)
        {
            primaryStyle.Font.Name =
                fontFamilyDrpDwnList.SelectedItem.Text;
            lblRptTitle.ApplyStyle(primaryStyle);
            lblRptDesc.ApplyStyle(primaryStyle);
            lblDate.ApplyStyle(primaryStyle);
        }
    }
}