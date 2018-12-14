using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        int pressNumberOfTimes;
        Label lbl_homeCarouselAdd = new Label();
        static StringBuilder strDiv = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void add_click(object sender, EventArgs e)
        {
            // Add the panel
            pressNumberOfTimes++;
            // Set the label's Text and ID properties.
            lbl_homeCarouselAdd.ID = "lbl_homeCarouselAdd" + pressNumberOfTimes;


            strDiv.Append(string.Format(@"<div class='test1'><hr /></div>"));

            lbl_homeCarouselAdd.Text += strDiv.ToString();


            Panel1.Controls.Add(lbl_homeCarouselAdd);
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {

            Label1.Text = "Processing";
            System.Threading.Thread.Sleep(5000);//here u can set anytime according to ur //refereshing time period 1000 means 1 second so as here 5000 means 5 secs
            Label1.Text = "Completed";
        }
        //protected void DropDown1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Label1.Text = DropDown1.SelectedValue;
        //}

    }
}