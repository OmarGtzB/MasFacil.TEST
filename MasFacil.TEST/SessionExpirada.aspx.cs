using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SessionExpirada : System.Web.UI.Page
{
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    protected void Page_Load(object sender, EventArgs e)
    {
        SM.DeactivateSession(this.Page);
    }
 
}