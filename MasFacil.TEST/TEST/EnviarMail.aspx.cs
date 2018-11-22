using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TEST_EnviarMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEnviarMail_Click(object sender, EventArgs e)
    {
        MGMEMail.EMail Mail = new MGMEMail.EMail();

        Mail.Enviar("","","");
           
    }
}