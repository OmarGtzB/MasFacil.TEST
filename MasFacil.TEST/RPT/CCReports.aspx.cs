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


public partial class RPT_CCReports : System.Web.UI.Page
{

    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();

    ws.Servicio oWS = new ws.Servicio();
    wsRpt.Service oWSRpt = new wsRpt.Service();

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
                 
            }
        }
    }


    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        DataSet dsGral = new DataSet();
        DataSet dsDatosDetail = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptDatosGenerales";
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        dsGral = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptCCCLIELIST";
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@TIPO", DbType.Int64, 0, ParameterDirection.Input, 1);
        dsDatosDetail = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

       string nameRpt = "RPTVISOR" + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
        "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";

        byte[] bytes = oWSRpt.byte_Report(dsGral, dsDatosDetail);

        string apppath = Server.MapPath("~/Temp") + "\\" + nameRpt;
        System.IO.File.WriteAllBytes(apppath, bytes);

        Session["RPTVISOR_name"] = nameRpt; 

    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {

        string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }
}