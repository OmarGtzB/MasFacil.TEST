﻿using System.Windows.Forms;
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

public partial class XP_ConsultaSaldosMov : System.Web.UI.Page
{
    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_pPerAnio;
    private string Pag_pPerNum;
    private string Pag_ProvCve;
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
    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

        if (Request.QueryString["pPerAnio"] != null && Request.QueryString["pPerAnio"] != "")
        {
            Pag_pPerAnio = Request.QueryString["pPerAnio"];
        }

        if (Request.QueryString["pPerNum"] != null && Request.QueryString["pPerNum"] != "")
        {
            Pag_pPerNum = Request.QueryString["pPerNum"];
        }

        if (Request.QueryString["provCve"] != null && Request.QueryString["provCve"] != "")
        {
            Pag_ProvCve = Request.QueryString["provCve"];
        }
    }
    private void InicioPagina()
    {
        Titulo();
        LlenarUiProveedor(Pag_ProvCve.Trim());
        LlenaGrid();
    }
    private void Titulo()
    {
        DataSet ds = new DataSet();

        Int64 iOpc = 71;
        if (Request.QueryString["pMovAcum"] != null && Request.QueryString["pMovAcum"] == "1")
        {
            iOpc = 72;
        }

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Periodos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, iOpc);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerAnio));
        ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerNum));
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            lblTitlePage.Text = ds.Tables[0].Rows[0]["Titulo"].ToString();
        }
    }
    private void LlenaGrid()
    {
        if (Request.QueryString["pPerAnio"] != null && Request.QueryString["pPerAnio"] != "" &&
            Request.QueryString["pPerNum"] != null && Request.QueryString["pPerNum"] != "" &&
            Request.QueryString["provCve"] != null && Request.QueryString["provCve"] != "")
        {
            Int64 iOpc = 42;
            if (Request.QueryString["pMovAcum"] != null && Request.QueryString["pMovAcum"] == "1")
            {
                iOpc = 43;
            }

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConsultaSaldosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, iOpc);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@perAnio", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerAnio));
            ProcBD.AgregarParametrosProcedimiento("@perNum", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(Pag_pPerNum));
            ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, Pag_ProvCve.Trim());
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            FnCtlsFillIn.RadGrid(ref rGdvSaldos, ds);
        }
    }
    private void LlenarUiProveedor(string provCve)
    {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ProveedorContactos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
            ProcBD.AgregarParametrosProcedimiento("@provCveClie", DbType.String, 20, ParameterDirection.Input, provCve.Trim());
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSRowsIsFill(ds))
            {
                LblProvNombre.Text = ds.Tables[0].Rows[0]["provCve"].ToString() + " " + ds.Tables[0].Rows[0]["provNom"].ToString();
            }
            else
            {
                LblProvNombre.Text = "";
            }
        }
        catch (Exception ex)
        {
            LblProvNombre.Text = "";
            string MsgError = ex.Message.Trim();
        }

    }

    #endregion

}