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
using System.Drawing;

public partial class IN_CierreIN : System.Web.UI.Page
{


    #region VARIABLES
    ws.Servicio oWS = new ws.Servicio();
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();

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
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {
            EjecutaSpAcciones();
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }


    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    #endregion



    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }

    public void InicioPagina()
    {
        FnCtlsFillIn.RabComboBox_PeriodoAnios(Pag_sConexionLog, Pag_sCompania, ref rCboAnioPeriodo, true, true);
        LlenaGrid();
    }

    private void EjecutaSpAcciones()
    {
        try
        {
            DataSet ds = new DataSet();
            int CierraPeriodos = 2;
            if (CheckCierraPeriodos.Checked == true)
            {
                CierraPeriodos = 1;
            }


            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPROOpe_CierreAnualIN";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@perAnioCierre", DbType.Int64, 0, ParameterDirection.Input, rCboAnioPeriodo.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@CierraPeriodos", DbType.Int64, 0, ParameterDirection.Input, CierraPeriodos);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                LlenaGrid();
                ShowAlert(sEjecEstatus, sEjecMSG); 
            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }


    private void LlenaGrid()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_PeriodoCierre";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@moduCve", DbType.String, 2, ParameterDirection.Input, "IN");
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref this.rGdvInformacion, ds);
    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES
    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
        sMSGTip = "";
        if (rCboAnioPeriodo.SelectedIndex == -1)
        {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
        }

        return sResult;
    }
    #endregion
}