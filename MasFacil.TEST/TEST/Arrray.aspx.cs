using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TEST_Arrray : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String[,] datos;
        int Row = 1;
        int col = 2;

        datos = new String[Row, col];
        int x=datos.Length/2;

        for (int i = 0; i < x ; i++)
        {
           string  archivo = datos[i,0];
           string nombre = datos[i, 1];
        }

    }




    private void correo() {

        MGM.Correo.CorreoElectronico MGMMail = new MGM.Correo.CorreoElectronico();

        String[,] datos;
        datos = new String[0, 0];
     string envio =    MGMMail.Enviar("smtp.gmail.com", 587,true, "Management", "mgm.soporte.desarrollo@gmail.com", "Soporte2017@", "CORREO TEST","ehernandez@Inso.com.mx,", "ohernandez@Inso.com.mx,", "mensaje ", datos, "Probando la clase de correos de Management."); 


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        correo();
    }
}