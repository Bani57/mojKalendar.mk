using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace mojKalendarfinal
{
    public partial class Kalendar : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Path.EndsWith("Register.aspx"))
            {
                lblRegister.Attributes["class"] = "pageCurrent";
                lblLogin.Attributes["class"] = "page";
                lblCalendar.Attributes["class"] = "page";
                form1.Attributes["class"] = "lightgray";
            }
            else if(Request.Path.EndsWith("Login.aspx"))
            {
                lblRegister.Attributes["class"] = "page";
                lblLogin.Attributes["class"] = "pageCurrent";
                lblCalendar.Attributes["class"] = "page";
                form1.Attributes["class"] = "lightgray";
            }
            else
            {
                lblRegister.Attributes["class"] = "page";
                lblLogin.Attributes["class"] = "page";
                lblCalendar.Attributes["class"] = "pageCurrent";
            }
        }
    }
}