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
using System.Data.Entity;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

public partial class FR_DocsPdf : System.Web.UI.Page
{


    #region VARIABLES

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    wsRpt.Service oWSRpt = new wsRpt.Service();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();


    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_DocCve;
    private Int64 Pag_sIdDocReg;




    #endregion




    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    public void InicioPagina()
    {
        VisDoc();
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_sIdDocReg = Convert.ToInt64(Session["Valor_DocCve"]);
        Pag_DocCve = Convert.ToString(Session["docCve"]);
    }

    private void VisDoc()
    {
        /////////////////////////////////////////////////////
        DataSet ds1 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_RptDataSetDoc";

        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD1.AgregarParametrosProcedimiento("@docRegId", DbType.String, 10, ParameterDirection.Input, Pag_sIdDocReg);
        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD1.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, Pag_DocCve);
        ProcBD1.AgregarParametrosProcedimiento("@fltrDocRegIds", DbType.String, 2048, ParameterDirection.Input, Session["Filtro_Vis_Doc"]);
        ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptDataSetDocumento";
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Pag_sIdDocReg);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        //byte[] bytes = (byte[])ds.Tables[0].Rows[0]["docRegVisArc"];

        byte[] bytes = oWSRpt.byte_FormatoDoc(ds, ds1);


        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "inline; filename=" + "documento" + Pag_sIdDocReg.ToString() + ".pdf");
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
        Response.Close();


       


    }




}