using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;
using Telerik.Web.UI;
using Telerik.Windows;
using System.Web.UI.HtmlControls;

public partial class RPT_CopiaConfigAcum : System.Web.UI.Page
{

    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();
    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_idReporte;
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

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionCopiar();
    }

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        ValorUrl();
    }

    private void EjecutaAccionCopiar()
    {
        Session["msgEstatus"] = "0";
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {

            string maUser = LM.sValSess(this.Page, 1);

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_RptAcumReporte";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 4);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@idReporteCopia", DbType.String, 20, ParameterDirection.Input, rTxtCve.Text);
            ProcBD.AgregarParametrosProcedimiento("@idReporte", DbType.String, 20, ParameterDirection.Input, Pag_idReporte);
            ProcBD.AgregarParametrosProcedimiento("@nombreCopia", DbType.String, 200, ParameterDirection.Input, rTxtDes.Text);
            
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                if (sEjecEstatus == "1")
                {
                    
                    string script = "function f(){Close(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "close", "Close();", true);

                    Session["msgEstatus"] = "1";

                }
                else 
                {
                    Session["msgEstatus"] = "2";
                }
            }

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }

    public void InicioPagina()
    {
        LlenaDatos();
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    public void ValorUrl()
    {
        if (Request.QueryString["idReporte"] != null && Request.QueryString["idReporte"] != "")
        {
            Pag_idReporte = Request.QueryString["idReporte"];
        }
    }

    public void LlenaDatos()
    {
      
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptAcumReporte";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@idReporte", DbType.String, 20, ParameterDirection.Input, Pag_idReporte.Trim());
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {

            lblRep.Text = ds.Tables[0].Rows[0]["idReporte"].ToString();
            lblDes.Text = ds.Tables[0].Rows[0]["nombre"].ToString();

        }
    }


    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
        sMSGTip = "";

        if (rTxtCve.Text == "" && rTxtDes.Text == "")
        {
            rTxtCve.CssClass = "cssTxtInvalid";
            rTxtDes.CssClass = "cssTxtInvalid";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            return sResult;
        }else {
            rTxtCve.CssClass = "cssTxtEnabled";
            rTxtDes.CssClass = "cssTxtEnabled";
        }

        if (rTxtCve.Text == "")
        {
            rTxtCve.CssClass = "cssTxtInvalid";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            return sResult;
        }
        else
        {
            rTxtCve.CssClass = "cssTxtEnabled";
        }


        if (rTxtDes.Text == "")
        {
            rTxtDes.CssClass = "cssTxtInvalid";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            return sResult;
        }else {
            rTxtDes.CssClass = "cssTxtEnabled";
        }
        
        return sResult;
    }

}