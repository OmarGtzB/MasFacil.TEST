using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DC_Confirm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button_Click(object sender, EventArgs e)
    {
        RadButton btn = sender as RadButton;
        switch (btn.ID)
        {
            case "btnStandardConfirm":
                Label1.Text = "The <strong>StandardButton</strong> submitted the page at: " + DateTime.Now.ToString();
                break;
            case "btnRadConfirm":
                Label2.Text = "The <strong>RadConfirm Button</strong> submitted the page at: " + DateTime.Now.ToString();
                break;
            case "btnCustomRadWindowConfirm":
                Label3.Text = "The <strong>Custom RadWindow Button</strong> submitted the page at: " + DateTime.Now.ToString();
                break;
        }
    }
}