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

public partial class RPT_Visor : System.Web.UI.Page
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


    #region EVENTOS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            addRadWin();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    protected void rBtnExpXLS_Click(object sender, ImageButtonClickEventArgs e)
    {
        byte[] bytes = (byte[])((DataSet)Session["Reports_dsByte"]).Tables[0].Rows[0]["ColumRtpXLS"];
        if (bytes.Length > 0)
        {
            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment; filename = " + "reporte.xls");
            Response.BinaryWrite(bytes);
            Response.End();
        }
    }
    protected void rBtnExpPDF_Click(object sender, ImageButtonClickEventArgs e)
    {
        byte[] bytes = (byte[])((DataSet)Session["Reports_dsByte"]).Tables[0].Rows[0]["ColumRtpPDF"];
        if (bytes.Length > 0)
        {
            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment; filename = " + "reporte.pdf");
            Response.BinaryWrite(bytes);
            Response.End();
        }
    }
    #endregion


    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }
    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
    }

    public void InicioPagina()
    {
        TituloPagina();
        VisualizaReporte();
    }
    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Visor Reportes", "PnlMPFormTituloApartado");
    }

    private void VisualizaReporte() {

        DataSet dsReports = new DataSet();
        byte[] bytes = (byte[])((DataSet)Session["Reports_dsByte"]).Tables[0].Rows[0]["ColumRtpPDF"];

        string nameRpt = "RPTVISOR" + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
       "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";
        string apppath = Server.MapPath("~/Temp") + "\\" + nameRpt;
        System.IO.File.WriteAllBytes(apppath, bytes);
        iframeReport.Attributes["src"] = "..//Temp//" + nameRpt;
    }


    #endregion


    #region FUNCIONES


    #endregion

}