using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FYP
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        int pressNumberOfTimes;
        Label lbl_homeCarouselAdd = new Label();
        static StringBuilder strDiv = new StringBuilder();

        string positionX, positionY;
        
        protected void Page_Load(object sender, EventArgs e)
        {
                InitControls();
        }

        protected void InitControls() {
            positionX = posX.Text;
            positionY = posY.Text;

            top.Text = positionX;
            left.Text = positionY;
        }
        //protected void add_click(object sender, EventArgs e)
        //{
        //    pressNumberOfTimes = 0;
        //    // Add the panel
        //    pressNumberOfTimes++;
        //    // Set the label's Text and ID properties.
        //    lbl_homeCarouselAdd.ID = "lbl_homeCarouselAdd" + pressNumberOfTimes;


        //    strDiv.Append(string.Format(@"<div class='test1'><hr /></div>"));

        //    lbl_homeCarouselAdd.Text += strDiv.ToString();


        //    Panel1.Controls.Add(lbl_homeCarouselAdd);
        //}
    }
}