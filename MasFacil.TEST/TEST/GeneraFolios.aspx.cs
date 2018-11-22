using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TEST_GeneraFolios : System.Web.UI.Page
{

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;


    protected void Page_Load(object sender, EventArgs e)
    {
        Valores_InicioPag();
    }

    protected void rBtnObtenFolio_Click(object sender, EventArgs e)
    {
        //rTxtResultado.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, "10Iz", 1, "SS")
        rTxtResultado.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, txtClaveFolio.Text , Convert.ToInt16(txtManejoFolio.Text) , txtValor.Text );
    }


    private void Valores_InicioPag()
    {
        Pag_sCompania =  Convert.ToString(Session["Compania"]);
        Pag_sConexionLog =   Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog =   Convert.ToString(Session["SessionLog"]);

            
    }
}