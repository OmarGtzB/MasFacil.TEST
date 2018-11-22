using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using Telerik.Windows;
using System.Web.UI.HtmlControls;
 
using System.Configuration;
 
using System.Data.SqlClient;
 
public partial class RPT_VisorAcumulados : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();

    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();

            if (!IsPostBack)
            {
                //llenagrid();
            }
        }
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }



    private DataTable GetDataTable() {
        DataSet ds = new DataSet();


        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_rptDocumentoRegistroPartidas";
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        //rPivoGrid.DataSource = ds.Tables[0];
        //rPivoGrid.Rebind();

        //      objRadGrid.DataSource = ds.Tables[0];
        //objRadGrid.DataBind();

        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        //rPivoGrid.DataSource = dt.DefaultView;
        return dt;
    }

    protected void rPivoGrid_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
    {

        rPivoGrid.DataSource = GetDataTable();

    }
}