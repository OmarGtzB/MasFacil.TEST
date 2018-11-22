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
using System.Configuration;

public partial class RPT_rptConfigAcum : System.Web.UI.Page
{


    #region VARIABLES
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

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
            //addRadWin();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        Controles();
    }
    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        if (rGdv_ReporteColumnas.SelectedItems.Count > 0 && rGdv_Reporte.SelectedItems.Count > 0)
        {
            ControlesAccionColumnas();
        }
        else
        if (rGdv_Reporte.SelectedItems.Count > 0)
        {
            ControlesAccion();
        }
        else if (rGdv_Reporte.SelectedItems.Count == 0)
        {
            ControlesAccion();
        }
        rGdv_ReporteColumnas.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_ReporteColumnas.AllowMultiRowSelection = true;


        if (rGdv_ReporteColumnas.SelectedItems.Count == 0)
        {
            //LlenaGridReporteColumnas("");
            rGdv_ReporteColumnas.MasterTableView.ClearSelectedItems();
            rTxtNoColumna.Text = "";
            rTxtTitulo.Text = "";
            rTxtNoSubtitulo.Text = "";
            rCboTipoColumn.ClearSelection();
            rBtnValCreEntero.Checked = false;
            rBtnValCreDecimal.Checked = false;
            rBtnValCreNoAplica.Checked = true;
            CheckMiles.Checked = false;
            rTxtFormula.Text = "";

            rTxtNoColumna.Enabled = false;
            rTxtTitulo.Enabled = false;
            rTxtNoSubtitulo.Enabled = false;
            rCboTipoColumn.Enabled = false;
            rBtnValCreEntero.Enabled = false;
            rBtnValCreDecimal.Enabled = false;
            rBtnValCreNoAplica.Enabled = false;
            CheckMiles.Enabled = false;
            rTxtFormula.Enabled = false;

        }

    }
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rGdv_ReporteColumnas.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_ReporteColumnas.AllowMultiRowSelection = true;

            rTxtNoColumna.Enabled = false;
            rTxtTitulo.Enabled = false;
            rTxtNoSubtitulo.Enabled = false;
            rCboTipoColumn.Enabled = false;
            rBtnValCreEntero.Enabled = false;
            rBtnValCreDecimal.Enabled = false;
            rBtnValCreNoAplica.Enabled = false;
            CheckMiles.Enabled = false;
            rTxtFormula.Enabled = false;
        }
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        if (rGdv_ReporteColumnas.SelectedItems.Count > 0 && rGdv_Reporte.SelectedItems.Count > 0)
        {
            ControlesAccionColumnas();
        }
        else
        if (rGdv_Reporte.SelectedItems.Count > 0)
        {
            ControlesAccion();
        }
        else if (rGdv_Reporte.SelectedItems.Count == 0)
        {
            ControlesAccion();
        }
    }
    protected void rBtnCopiar_Click(object sender, ImageButtonClickEventArgs e)///-----------------------------------------------------
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }
    protected void rCboTipoColumn_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (rCboTipoColumn.SelectedValue != "")
        { 

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptAcumReporteColumnas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@idTipoColumna", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rCboTipoColumn.SelectedValue));
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                bool valDs;
                valDs = Convert.ToBoolean(ds.Tables[0].Rows[0]["permiteOperaciones"].ToString());

                if (valDs == false)
                {
                    rBtnValCreEntero.Checked = false;
                    rBtnValCreDecimal.Checked = false;
                    rBtnValCreNoAplica.Checked = true;
                    CheckMiles.Checked = false;
                    rTxtFormula.Text = "";

                    rBtnValCreEntero.Enabled = false;
                    rBtnValCreDecimal.Enabled = false;
                    rBtnValCreNoAplica.Enabled = false;
                    CheckMiles.Enabled = false;
                    rTxtFormula.Enabled = false;
                    rTxtFormula.Text = "";
                }
                else
                {

                    rBtnValCreEntero.Enabled = true;
                    rBtnValCreDecimal.Enabled = true;
                    rBtnValCreNoAplica.Enabled = true;
                    CheckMiles.Enabled = true;
                    rTxtFormula.Enabled = false;

                    rBtnValCreEntero.Checked = false;
                    rBtnValCreDecimal.Checked = false;
                    rBtnValCreNoAplica.Checked = true;
                    CheckMiles.Checked = false;
                    rTxtFormula.Text = "";
                }
                if (valDs == true && rCboTipoColumn.SelectedValue == "0") {

                    rBtnValCreEntero.Enabled = true;
                    rBtnValCreDecimal.Enabled = true;
                    rBtnValCreNoAplica.Enabled = true;
                    CheckMiles.Enabled = true;
                    rTxtFormula.Enabled = true;
                    rTxtFormula.Text = "";
                }
            }

        }
    }
    protected void rGdv_Documentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdv_Reporte.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            string Cve = dataItem["idReporte"].Text.Trim();
            string Des = dataItem["nombre"].Text.Trim();

            if (Cve != "")
            {
                rTxtCve.Text = Cve;
            }
            else
            {
                rTxtCve.Text = "";
            }

            if (Des != "")
            {
                rTxtDes.Text = Des;
            }
            else
            {
                rTxtDes.Text = "";
            }

            LlenaGridReporteColumnas(Cve);

            rTxtNoColumna.Text = "";
            rTxtTitulo.Text = "";
            rTxtNoSubtitulo.Text = "";
            rCboTipoColumn.ClearSelection();
            rBtnValCreEntero.Checked = true;
            rBtnValCreDecimal.Checked = true;
            rBtnValCreNoAplica.Checked = true;
            CheckMiles.Checked = true;
            rTxtFormula.Text = "";

        }

        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        //{
        //    rTxtCve.Enabled = false;
        //    rTxtDes.Enabled = false;

        //    rGdv_ReporteColumnas.MasterTableView.ClearSelectedItems();
            

        //    rTxtNoColumna.Text = "";
        //    rTxtTitulo.Text = "";
        //    rTxtNoSubtitulo.Text = "";
        //    rCboTipoColumn.ClearSelection();
        //    rBtnValCreEntero.Checked = true;
        //    rBtnValCreDecimal.Checked = true;
        //    rBtnValCreNoAplica.Checked = true;
        //    CheckMiles.Checked = true;
        //    rTxtFormula.Text = "";
        //}


    }
    protected void rGdv_Referencias_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rTxtCve.Enabled = false;
            rTxtDes.Enabled = false;

            rTxtNoColumna.Enabled = false;
            rTxtTitulo.Enabled = true;
            rTxtNoSubtitulo.Enabled = true;
            rCboTipoColumn.Enabled = true;
            rBtnValCreEntero.Enabled = true;
            rBtnValCreDecimal.Enabled = true;
            rBtnValCreNoAplica.Enabled = true;
            CheckMiles.Enabled = true;
            rTxtFormula.Enabled = true;
        }

    
        var dataItem = rGdv_ReporteColumnas.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            rTxtNoColumna.Text = dataItem["numeroColumna"].Text.Trim();

            rTxtTitulo.Text = dataItem["tituloColumna"].Text.Trim();

            rTxtNoSubtitulo.Text = dataItem["subtituloColumna"].Text.Trim().Replace("&nbsp;", "");

            rCboTipoColumn.SelectedValue = dataItem["idTipoColumna"].Text.Trim();


            if (dataItem["formato"].Text.Trim() == "1")
            {
                rBtnValCreEntero.Checked = true;
                rBtnValCreDecimal.Checked = false;
                rBtnValCreNoAplica.Checked = false;
            }
            else if (dataItem["formato"].Text.Trim() == "2")
            {

                rBtnValCreDecimal.Checked = true;
                rBtnValCreEntero.Checked = false;
                rBtnValCreNoAplica.Checked = false;
            }
            else
            {
                rBtnValCreNoAplica.Checked = true;
                rBtnValCreEntero.Checked = false;
                rBtnValCreDecimal.Checked=false;

              
            }

            if (dataItem["miles"].Text.Trim() == "1")
            {

                CheckMiles.Checked = true;

            }
            else
            {

                CheckMiles.Checked = false;
            }

            if (dataItem["formula"].Text.Trim() != "&nbsp;")
            {
                rTxtFormula.Text = dataItem["formula"].Text.Trim();
            }
            else
            {
                rTxtFormula.Text = "";
            }

            // Aqui
            if ((hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()))
            {
                if (ValComboBool() == true)
                {
                    rBtnValCreNoAplica.Enabled = true;
                    rBtnValCreEntero.Enabled = true;
                    rBtnValCreDecimal.Enabled = true;
                    CheckMiles.Enabled = true;
                    rTxtFormula.Enabled = false;
                }
                else
                {
                    rBtnValCreNoAplica.Enabled = false;
                    rBtnValCreEntero.Enabled = false;
                    rBtnValCreDecimal.Enabled = false;
                    CheckMiles.Enabled = false;
                    rTxtFormula.Enabled = false;
                }
                if (ValComboBool() == true && rCboTipoColumn.SelectedValue == "0")
                {
                    rTxtFormula.Enabled = true;
                }
            }

        }

    }
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "";
        InicioPagina();
        ControlesAccionColumnas();
        LlenaGridReporteColumnas("");
    }
    protected void RAJAXMAN1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        string Estatus;
        Estatus = Session["msgEstatus"].ToString();

        if (Estatus == "1")
        {
            hdfBtnAccion.Value = "";
            InicioPagina();
            LlenaGridReporteColumnas("");

            string sResult = "", sMSGTip = "";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1045", ref sMSGTip, ref sResult);
            ShowAlert(sMSGTip, sResult);
        }
        else
        {
            string sResult = "", sMSGTip = "";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0004", ref sMSGTip, ref sResult);
            ShowAlert(sMSGTip, sResult);
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

    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        LlenaCombo();
        LlenaGridReporte();
        ControlesAccion();
        TituloPagina();
        rGdv_Reporte.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Reporte.AllowMultiRowSelection = true;

        rGdv_ReporteColumnas.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_ReporteColumnas.AllowMultiRowSelection = true;
        PermisoBotones();
    }

    private void PermisoBotones()
    {
        Int64 Pag_sidM = 0;
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        }
        string maUser = LM.sValSess(this.Page, 1);
        FNBtn.MAPerfiles_Operacion_Acciones(pnlBtnsAcciones, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }

    private void LlenaCombo()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ObtenTipoColumnaReporte";
        ProcBD.AgregarParametrosProcedimiento("@aliasTabla", DbType.String, 20, ParameterDirection.Input, "");
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboTipoColumn, ds, "idTipoColumna", "descripcion", true, true);
        ((Literal)rCboTipoColumn.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboTipoColumn.Items.Count);

    }

    private void LlenaGridReporte()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = " sp_RptAcumReporte";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadGrid(ref rGdv_Reporte, ds);
    }

    private void LlenaGridReporteColumnas(string idReporte)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = " sp_RptAcumReporteColumnas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@idReporte", DbType.String, 20, ParameterDirection.Input, idReporte);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadGrid(ref rGdv_ReporteColumnas, ds);
    }

    private void Controles()
    {

        if (rGdv_Reporte.SelectedItems.Count == 0)
        {
            ControlesAccion();
        }
        else
        if (rGdv_Reporte.SelectedItems.Count > 0)
        {
            ControlesAccionColumnas();
        }

    }

    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                if (rGdv_Reporte.SelectedItems.Count != 0)
                {
                    GuardaReporteColumnas();
                }
                else
                {
                    EjecutaSpAcciones();
                }

            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                if (rGdv_Reporte.SelectedItems.Count != 0 && rGdv_ReporteColumnas.SelectedItems.Count == 0)
                {

                    EjecutaSpAcciones();
                }
                else
                {
                    GuardaReporteColumnas();
                }

            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {

                if (rGdv_ReporteColumnas.SelectedItems.Count > 0)
                {
                    EjecutaSpAccionEliminarColumnas();
                    LlenaGridReporteColumnas(rTxtCve.Text);
                    rTxtNoColumna.Text = "";
                    rTxtTitulo.Text="";
                    rTxtNoSubtitulo.Text = "";
                    rCboTipoColumn.ClearSelection();
                    rBtnValCreEntero.Checked = false;
                    rBtnValCreDecimal.Checked = false;
                    rBtnValCreNoAplica.Checked = true;
                    rTxtFormula.Text = "";
                    hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
                }
                else
                {
                    EjecutaSpAccionEliminar();
                    LlenaGridReporteColumnas("");

                    rGdv_Reporte.ClientSettings.Selecting.AllowRowSelect = true;
                    rGdv_Reporte.AllowMultiRowSelection = true;

                    rGdv_ReporteColumnas.ClientSettings.Selecting.AllowRowSelect = true;
                    rGdv_ReporteColumnas.AllowMultiRowSelection = true;
                }

            }

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }

    }

    private void EjecutaSpAcciones()
    {

        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptAcumReporte";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@idReporte", DbType.String, 20, ParameterDirection.Input, rTxtCve.Text.Trim());
        ProcBD.AgregarParametrosProcedimiento("@nombre", DbType.String, 200, ParameterDirection.Input, rTxtDes.Text.Trim());
        ProcBD.AgregarParametrosProcedimiento("@estatus", DbType.Int32, 0, ParameterDirection.Input, 1);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sEjecEstatus == "1")
            {
                InicioPagina();

                LlenaGridReporteColumnas("");
                rGdv_ReporteColumnas.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_ReporteColumnas.AllowMultiRowSelection = true;
            }
            ShowAlert(sEjecEstatus, sEjecMSG);
        }


    }

    private void GuardaReporteColumnas()
    {

        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptAcumReporteColumnas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
        ProcBD.AgregarParametrosProcedimiento("@idReporte", DbType.String, 20, ParameterDirection.Input, rTxtCve.Text.Trim());

        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ProcBD.AgregarParametrosProcedimiento("@numeroColumna", DbType.Int32, 0, ParameterDirection.Input, rTxtNoColumna.Text.Trim());
        

        ProcBD.AgregarParametrosProcedimiento("@idTipoColumna", DbType.Int32, 0, ParameterDirection.Input, rCboTipoColumn.SelectedValue.Trim());

        ProcBD.AgregarParametrosProcedimiento("@tituloColumna", DbType.String, 200, ParameterDirection.Input, rTxtTitulo.Text);

        ProcBD.AgregarParametrosProcedimiento("@subtituloColumna", DbType.String, 200, ParameterDirection.Input, rTxtNoSubtitulo.Text);

        if (rTxtFormula.Text != "")
        {
            ProcBD.AgregarParametrosProcedimiento("@formula", DbType.String, 200, ParameterDirection.Input, rTxtFormula.Text);
        }
        else
        {
            ProcBD.AgregarParametrosProcedimiento("@formula", DbType.String, 200, ParameterDirection.Input, "");
        }


        ProcBD.AgregarParametrosProcedimiento("@formato", DbType.Int16, 0, ParameterDirection.Input, ObtenerFormato());

        ProcBD.AgregarParametrosProcedimiento("@miles", DbType.Int16, 0, ParameterDirection.Input, ObtenerMiles());

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sEjecEstatus == "1")
            {
                // InicioPagina();
                LlenaGridReporteColumnas(rTxtCve.Text);

                //hdfBtnAccion.Value = "";
                //ControlesAccionColumnas();
                //ControlesAccion();
                //rGdv_Reporte.MasterTableView.ClearSelectedItems();
                //LlenaGridReporteColumnas("");
                //rGdv_ReporteColumnas.ClientSettings.Selecting.AllowRowSelect = true;
                //rGdv_ReporteColumnas.AllowMultiRowSelection = true;

                rTxtNoColumna.Text = "";
                rTxtTitulo.Text = "";
                rTxtNoSubtitulo.Text = "";
                rCboTipoColumn.ClearSelection();
                rBtnValCreEntero.Checked = false;
                rBtnValCreDecimal.Checked = false;
                rBtnValCreNoAplica.Checked = true;
                CheckMiles.Checked = false;
                rTxtFormula.Text = "";

                
            }
            ShowAlert(sEjecEstatus, sEjecMSG);
        }

    }

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void EjecutaSpAccionEliminar()
    {
        try
        {
            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";


            foreach (GridDataItem i in rGdv_Reporte.SelectedItems)
            {

                var dataItem = rGdv_Reporte.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string idReporte = dataItem.GetDataKeyValue("idReporte").ToString();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_RptAcumReporte";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@idReporte", DbType.String, 20, ParameterDirection.Input, idReporte.Trim());
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


                        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                        if (FnValAdoNet.bDSIsFill(ds))
                        {

                            EstatusItemsElim = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                            if (EstatusItemsElim == "1")
                            {
                                CantItemsElimTrue += 1;
                                MsgItemsElimTrue = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            }
                            else
                            {
                                CantItemsElimFalse += 1;
                                MsgItemsElimFalse = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            }

                            MsgItemsElim = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                        }


                    }
                    catch (Exception ex)
                    {
                        string MsgError = ex.Message.Trim();
                    }


                }

                CountItems += 1;
            }





            string sEstatusAlert = "2";
            string sMsgAlert = "";

            if (CountItems == 1)
            {

                sEstatusAlert = EstatusItemsElim;
                if (sEstatusAlert == "1")
                {
                    sMsgAlert = MsgItemsElim + " " + CountItems.ToString();
                }
                else
                {
                    sMsgAlert = MsgItemsElim;
                }


                ShowAlert(sEstatusAlert, sMsgAlert);

                if (sEstatusAlert == "1")
                {
                    InicioPagina();
                }
                else
                {
                    //LlenaGrid();
                    InicioPagina();
                }


            }
            else if (CountItems > 1)
            {


                if (CantItemsElimTrue > 0)
                {
                    sEstatusAlert = "1";
                }

                if (CantItemsElimTrue > 0)
                {
                    string sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0003", ref sMSGTip, ref sMsgAlert);
                    sMsgAlert += " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "")
                    {
                        sMsgAlert = sMsgAlert + "</br>";
                    }

                    string sMSGTip = "";
                    string sMSGRegNoElim = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0006", ref sMSGTip, ref sMSGRegNoElim);
                    sMsgAlert = sMsgAlert + sMSGRegNoElim + " " + CantItemsElimFalse.ToString();
                }


                ShowAlert(sEstatusAlert, sMsgAlert);
                if (CountItems == CantItemsElimTrue)
                {
                    InicioPagina();
                }
                else
                {
                    InicioPagina();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void EjecutaSpAccionEliminarColumnas()
    {
        try
        {
            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";


            foreach (GridDataItem i in rGdv_ReporteColumnas.SelectedItems)
            {

                var dataItem = rGdv_ReporteColumnas.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string idReporte = dataItem.GetDataKeyValue("idReporte").ToString();
                    string numeroColumna = dataItem["numeroColumna"].Text.Trim();
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_RptAcumReporteColumnas";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@idReporte", DbType.String, 20, ParameterDirection.Input, idReporte.Trim());
                        ProcBD.AgregarParametrosProcedimiento("@numeroColumna", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(numeroColumna));
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


                        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                        if (FnValAdoNet.bDSIsFill(ds))
                        {

                            EstatusItemsElim = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                            if (EstatusItemsElim == "1")
                            {
                                CantItemsElimTrue += 1;
                                MsgItemsElimTrue = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            }
                            else
                            {
                                CantItemsElimFalse += 1;
                                MsgItemsElimFalse = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                            }

                            MsgItemsElim = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                        }


                    }
                    catch (Exception ex)
                    {
                        string MsgError = ex.Message.Trim();
                    }


                }

                CountItems += 1;
            }





            string sEstatusAlert = "2";
            string sMsgAlert = "";

            if (CountItems == 1)
            {

                sEstatusAlert = EstatusItemsElim;
                if (sEstatusAlert == "1")
                {
                    sMsgAlert = MsgItemsElim + " " + CountItems.ToString();
                }
                else
                {
                    sMsgAlert = MsgItemsElim;
                }


                ShowAlert(sEstatusAlert, sMsgAlert);

                if (sEstatusAlert == "1")
                {
                    //InicioPagina();
                }
                else
                {
                    //LlenaGrid();
                    //InicioPagina();
                }


            }
            else if (CountItems > 1)
            {


                if (CantItemsElimTrue > 0)
                {
                    sEstatusAlert = "1";
                }

                if (CantItemsElimTrue > 0)
                {
                    string sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0003", ref sMSGTip, ref sMsgAlert);
                    sMsgAlert += " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "")
                    {
                        sMsgAlert = sMsgAlert + "</br>";
                    }

                    string sMSGTip = "";
                    string sMSGRegNoElim = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0006", ref sMSGTip, ref sMSGRegNoElim);
                    sMsgAlert = sMsgAlert + sMSGRegNoElim + " " + CantItemsElimFalse.ToString();
                }


                ShowAlert(sEstatusAlert, sMsgAlert);
                if (CountItems == CantItemsElimTrue)
                {
                    InicioPagina();
                }
                else
                {
                    InicioPagina();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void EjecutaAccionLimpiar()
    {

        if (rGdv_ReporteColumnas.SelectedItems.Count > 0 && rGdv_Reporte.SelectedItems.Count > 0)
        {

            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {

                rTxtNoColumna.Text = "";
                rTxtTitulo.Text = "";
                rTxtNoSubtitulo.Text = "";
                rTxtFormula.Text = "";
                rCboTipoColumn.ClearSelection();
                CheckMiles.Checked = false;
                rBtnValCreEntero.Checked = false;
                rBtnValCreDecimal.Checked = false;
                rBtnValCreNoAplica.Checked = true;
            }
            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rGdv_Reporte.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_Reporte.MasterTableView.ClearSelectedItems();

                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                rTxtCve.CssClass = "cssTxtEnabled";
                rTxtDes.CssClass = "cssTxtEnabled";
                rTxtNoSubtitulo.CssClass = "cssTxtEnabled";
                rTxtFormula.CssClass = "cssTxtEnabled";
                rCboTipoColumn.BorderColor = System.Drawing.Color.Transparent;

                rTxtNoColumna.Text = "";
                rTxtTitulo.Text = "";
                rTxtNoSubtitulo.Text = "";
                rTxtFormula.Text = "";
                rCboTipoColumn.ClearSelection();
                CheckMiles.Checked = false;
                rBtnValCreEntero.Checked = false;
                rBtnValCreDecimal.Checked = false;
                rBtnValCreNoAplica.Checked = true;

                rTxtNoColumna.Enabled = false;
                rTxtTitulo.Enabled = false;
                rTxtNoSubtitulo.Enabled = false;
                rTxtFormula.Enabled = false;
                rCboTipoColumn.Enabled = false;
                CheckMiles.Enabled = false;
                rBtnValCreEntero.Enabled = false;
                rBtnValCreDecimal.Enabled = false;
                rBtnValCreNoAplica.Enabled = false;

                
                rBtnGuardar.Enabled = false;
                rBtnCancelar.Enabled = false;
            }
            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {

            }







        }
        else if (rGdv_Reporte.SelectedItems.Count > 0)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rTxtNoColumna.Text = "";
                rTxtTitulo.Text = "";
                rTxtNoSubtitulo.Text = "";
                rCboTipoColumn.ClearSelection();
                rBtnValCreEntero.Checked = false;
                rBtnValCreDecimal.Checked = false;
                rBtnValCreNoAplica.Checked = true;
                CheckMiles.Checked = false;
                rTxtFormula.Text = "";

            }
            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rGdv_Reporte.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_Reporte.MasterTableView.ClearSelectedItems();

                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                rTxtCve.CssClass = "cssTxtEnabled";
                rTxtDes.CssClass = "cssTxtEnabled";

                rTxtCve.Enabled = false;
                rTxtDes.Enabled = false;

                rTxtCve.Text = "";
                rTxtDes.Text = "";

                LlenaGridReporteColumnas("");

                rBtnGuardar.Enabled = false;
                rBtnCancelar.Enabled = false;
            }
            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {

            }
        }
        else if (rGdv_Reporte.SelectedItems.Count == 0)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                this.rTxtCve.Text = "";
                this.rTxtDes.Text = "";
            }
            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rGdv_Reporte.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_Reporte.MasterTableView.ClearSelectedItems();

                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                rTxtCve.CssClass = "cssTxtEnabled";
                rTxtDes.CssClass = "cssTxtEnabled";

                rTxtCve.Enabled = false;
                rTxtDes.Enabled = false;

                rTxtCve.Text = "";
                rTxtDes.Text = "";

                rBtnGuardar.Enabled = false;
                rBtnCancelar.Enabled = false;
            }
            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {

            }
        }
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rGdv_Ayuda
        this.rGdv_Reporte.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rCboTipoColumn.BorderColor = System.Drawing.Color.Transparent;

        rTxtCve.CssClass = "cssTxtEnabled";
        rTxtDes.CssClass = "cssTxtEnabled";

        rTxtNoColumna.CssClass = "cssTxtEnabled";
        rTxtTitulo.CssClass = "cssTxtEnabled";
        rTxtTitulo.CssClass = "cssTxtEnabled";
        rTxtNoSubtitulo.CssClass = "cssTxtEnabled";
        rTxtFormula.CssClass = "cssTxtEnabled";

        this.rTxtCve.Enabled = false;
        this.rTxtDes.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        
        /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
        //Validacion
        msgValidacion = ValidaControlesAccion_SelectRowGrid(ref sMSGTip);
        if (msgValidacion == "")
        {
            ControlesAccionEjecucion(true);
        }
        else
        {
            ControlesAccionEjecucion(false);
            ShowAlert(sMSGTip, msgValidacion);
        }

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            this.rTxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;

            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";
        }
    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdv_Reporte.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_Reporte.MasterTableView.ClearSelectedItems();

                this.rTxtCve.Enabled = true;
                this.rTxtDes.Enabled = true;

                this.rTxtCve.Text = "";
                this.rTxtDes.Text = "";


                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_Reporte.AllowMultiRowSelection = false;

                this.rTxtCve.Enabled = false;
                this.rTxtDes.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
              //  EjecutaAccion();
                EjecutaSpAccionEliminar();
                LlenaGridReporteColumnas("");
            }


            //Copiar
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
            {
                var dataItem = rGdv_Reporte.SelectedItems[0] as GridDataItem;
                string idReporte = dataItem.GetDataKeyValue("idReporte").ToString();

                RadWindowCopiaConfigDocAcum.NavigateUrl = "CopiaConfigAcum.aspx?idReporte=" + idReporte;

                string script = "function f(){$find(\"" + RadWindowCopiaConfigDocAcum.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }


            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdv_Reporte.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_Reporte.AllowMultiRowSelection = true;
                rGdv_Reporte.MasterTableView.ClearSelectedItems();
                this.rTxtCve.Enabled = false;
                this.rTxtDes.Enabled = false;

                this.rTxtCve.Text = "";
                this.rTxtDes.Text = "";

            }
        }


        if (Result == false)
        {
            this.rTxtCve.Enabled = false;
            this.rTxtDes.Enabled = false;

            this.rTxtCve.Text = "";
            this.rTxtDes.Text = "";
        }


    }

    private void ControlesAccionColumnas()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rGdv_Ayuda
        this.rGdv_Reporte.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtNoColumna.CssClass = "cssTxtEnabled";
        rTxtTitulo.CssClass = "cssTxtEnabled";
        rTxtNoSubtitulo.CssClass = "cssTxtEnabled";
        rTxtFormula.CssClass = "cssTxtEnabled";
        rCboTipoColumn.CssClass = "cssTxtEnabled";
        CheckMiles.CssClass = "cssTxtEnabled";
        rBtnValCreEntero.CssClass = "cssTxtEnabled";
        rBtnValCreDecimal.CssClass = "cssTxtEnabled";

        rTxtNoColumna.Enabled = false;
        rTxtTitulo.Enabled = false;
        rTxtNoSubtitulo.Enabled = false;
        rTxtFormula.Enabled = false;
        rCboTipoColumn.Enabled = false;
        CheckMiles.Enabled = false;
        rBtnValCreEntero.Enabled = false;
        rBtnValCreDecimal.Enabled = false;
        rBtnValCreNoAplica.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;


        /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
        //Validacion
        msgValidacion = ValidaControlesAccion_SelectRowGridColumnas(ref sMSGTip);
        if (msgValidacion == "")
        {
            ControlesAccionEjecucionColumnas(true);
        }
        else
        {
            ControlesAccionEjecucionColumnas(false);
            ShowAlert(sMSGTip, msgValidacion);
        }

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            rTxtNoColumna.Enabled = false;
            rTxtTitulo.Enabled = false;
            rTxtNoSubtitulo.Enabled = false;
            rTxtFormula.Enabled = false;
            rCboTipoColumn.Enabled = false;
            CheckMiles.Enabled = false;
            rBtnValCreEntero.Enabled = false;
            rBtnValCreDecimal.Enabled = false;
            rBtnValCreNoAplica.Enabled = false;

            rTxtNoColumna.Text = "";
            rTxtTitulo.Text = "";
            rTxtNoSubtitulo.Text = "";
            rTxtFormula.Text = "";
            rCboTipoColumn.ClearSelection(); ;
            CheckMiles.Checked = false;
            rBtnValCreEntero.Checked = false;
            rBtnValCreDecimal.Checked = false;
            rBtnValCreNoAplica.Checked = true;
        }
    }

    private void ControlesAccionEjecucionColumnas(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdv_ReporteColumnas.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_ReporteColumnas.MasterTableView.ClearSelectedItems();

                rTxtNoColumna.Enabled = true;
                rTxtTitulo.Enabled = true;
                rTxtNoSubtitulo.Enabled = true;
                rTxtFormula.Enabled = true;
                rCboTipoColumn.Enabled = true;
                CheckMiles.Enabled = true;
                rBtnValCreEntero.Enabled = true;
                rBtnValCreDecimal.Enabled = true;
                rBtnValCreNoAplica.Enabled = true;


                rTxtNoColumna.Text = "";
                rTxtTitulo.Text = "";
                rTxtNoSubtitulo.Text = "";
                rTxtFormula.Text = "";
                rCboTipoColumn.ClearSelection(); ;
                CheckMiles.Checked = false;
                rBtnValCreEntero.Checked = false;
                rBtnValCreDecimal.Checked = false;
                rBtnValCreNoAplica.Checked = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_ReporteColumnas.AllowMultiRowSelection = false;

                rTxtNoColumna.Enabled = false;
                rTxtTitulo.Enabled = true;
                rTxtNoSubtitulo.Enabled = true;
                rCboTipoColumn.Enabled = true;
                rBtnValCreNoAplica.Enabled = true;

                rTxtFormula.Enabled = true;
                CheckMiles.Enabled = true;
                rBtnValCreEntero.Enabled = true;
                rBtnValCreDecimal.Enabled = true;
                rBtnValCreNoAplica.Enabled = true;


                // Aqui
                if (ValComboBool() == true)
                {
                    rBtnValCreNoAplica.Enabled = true;
                    rBtnValCreEntero.Enabled = true;
                    rBtnValCreDecimal.Enabled = true;
                    CheckMiles.Enabled = true;

                    if (ValComboBool() == true && rCboTipoColumn.SelectedValue == "0")
                    {
                        rTxtFormula.Enabled = true;
                    }else {
                        rTxtFormula.Enabled = false;
                    }
                  //  rTxtFormula.Enabled = true;
                }
                else
                {
                    rBtnValCreNoAplica.Enabled = false;
                    rBtnValCreEntero.Enabled = false;
                    rBtnValCreDecimal.Enabled = false;
                    CheckMiles.Enabled = false;
                    rTxtFormula.Enabled = false;
                }


                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdv_ReporteColumnas.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_ReporteColumnas.AllowMultiRowSelection = true;
                rGdv_ReporteColumnas.MasterTableView.ClearSelectedItems();

                rTxtNoColumna.Enabled = false;
                rTxtTitulo.Enabled = false;
                rTxtNoSubtitulo.Enabled = false;
                rTxtFormula.Enabled = false;
                rCboTipoColumn.Enabled = false;
                CheckMiles.Enabled = false;
                rBtnValCreEntero.Enabled = false;
                rBtnValCreDecimal.Enabled = false;
                rBtnValCreNoAplica.Enabled = false;

                rTxtNoColumna.Text = "";
                rTxtTitulo.Text = "";
                rTxtNoSubtitulo.Text = "";
                rTxtFormula.Text = "";
                rCboTipoColumn.ClearSelection(); ;
                CheckMiles.Checked = false;
                rBtnValCreEntero.Checked = false;
                rBtnValCreDecimal.Checked = false;
                rBtnValCreNoAplica.Checked = true;

            }
        }


        if (Result == false)
        {
            rTxtNoColumna.Enabled = false;
            rTxtTitulo.Enabled = false;
            rTxtNoSubtitulo.Enabled = false;
            rTxtFormula.Enabled = false;
            rCboTipoColumn.Enabled = false;
            CheckMiles.Enabled = false;
            rBtnValCreEntero.Enabled = false;
            rBtnValCreDecimal.Enabled = false;
            rBtnValCreNoAplica.Enabled = false;

            rTxtNoColumna.Text = "";
            rTxtTitulo.Text = "";
            rTxtNoSubtitulo.Text = "";
            rTxtFormula.Text = "";
            rCboTipoColumn.ClearSelection(); ;
            CheckMiles.Checked = false;
            rBtnValCreEntero.Checked = false;
            rBtnValCreDecimal.Checked = false;
            rBtnValCreNoAplica.Checked = true;
        }


    }

    private string ValidaControlesAccion_SelectRowGridColumnas(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_ReporteColumnas.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_ReporteColumnas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_ReporteColumnas, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }

    #endregion
    
    #region FUNCIONES

    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        if (rTxtDes.Enabled==true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {

                if (rTxtCve.Text.Trim() == "")
                {
                    rTxtCve.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtCve.CssClass = "cssTxtEnabled"; }

                if (rTxtDes.Text.Trim() == "")
                {
                    rTxtDes.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtDes.CssClass = "cssTxtEnabled"; }

                if (camposInc > 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
                }
                return sResult;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {

                if (rGdv_Reporte.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }

                if (rTxtCve.Text.Trim() == "")
                {
                    rTxtCve.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtCve.CssClass = "cssTxtEnabled"; }

                if (rTxtDes.Text.Trim() == "")
                {
                    rTxtDes.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtDes.CssClass = "cssTxtEnabled"; }


                if (camposInc > 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
                }
                return sResult;

            }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {

                if (rGdv_Reporte.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }

                return sResult;
            }

        }
        else
        {
            //COLUMNAS
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {

                if (rTxtNoColumna.Text.Trim() == "")
                {
                    rTxtNoColumna.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtNoColumna.CssClass = "cssTxtEnabled"; }

                if (rTxtTitulo.Text.Trim() == "")
                {
                    rTxtTitulo.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtTitulo.CssClass = "cssTxtEnabled"; }


                //if (rTxtNoSubtitulo.Text.Trim() == "")
                //{
                //    rTxtNoSubtitulo.CssClass = "cssTxtInvalid";
                //    camposInc += 1;
                //}
                //else { rTxtNoSubtitulo.CssClass = "cssTxtEnabled"; }

                if (rCboTipoColumn.SelectedIndex == -1)
                {
                    rCboTipoColumn.CssClass = "cssTxtInvalid";
                    rCboTipoColumn.BorderWidth = Unit.Pixel(1);
                    rCboTipoColumn.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    rCboTipoColumn.BorderColor = System.Drawing.Color.Transparent;
                }

                if (rCboTipoColumn.SelectedIndex != -1)
                {
                    if (ValCombo() == 0)
                    {
                        if (rTxtFormula.Text.Trim() == "")
                        {
                            rTxtFormula.CssClass = "cssTxtInvalid";
                            camposInc += 1;
                        }
                        else { rTxtFormula.CssClass = "cssTxtEnabled"; }
                    }
                    else
                    {
                        rTxtFormula.CssClass = "cssTxtEnabled";
                    }
                }

                


                if (camposInc > 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
                }
                return sResult;
            }


            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {

                if (rGdv_ReporteColumnas.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }
                if (rTxtNoColumna.Text.Trim() == "")
                {
                    rTxtNoColumna.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtNoColumna.CssClass = "cssTxtEnabled"; }

                if (rTxtTitulo.Text.Trim() == "")
                {
                    rTxtTitulo.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtTitulo.CssClass = "cssTxtEnabled"; }


                //if (rTxtNoSubtitulo.Text.Trim() == "")
                //{
                //    rTxtNoSubtitulo.CssClass = "cssTxtInvalid";
                //    camposInc += 1;
                //}
                //else { rTxtNoSubtitulo.CssClass = "cssTxtEnabled"; }

                if (rCboTipoColumn.SelectedIndex == -1)
                {
                    rCboTipoColumn.CssClass = "cssTxtInvalid";
                    rCboTipoColumn.BorderWidth = Unit.Pixel(1);
                    rCboTipoColumn.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    rCboTipoColumn.BorderColor = System.Drawing.Color.Transparent;
                }
                //if (rTxtFormula.Text.Trim() == "")
                //{
                //    rTxtFormula.CssClass = "cssTxtInvalid";
                //    camposInc += 1;
                //}
                //else { rTxtFormula.CssClass = "cssTxtEnabled"; }

                if (camposInc > 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
                }
                return sResult;

            }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {

                if (rGdv_ReporteColumnas.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }

                return sResult;
            }




            return sResult;
        }
        return sResult;
    }

    private int ObtenerFormato()
    {

        if (rBtnValCreEntero.Checked == true)
        {
            return 1;
        }
        else if (rBtnValCreDecimal.Checked == true)
        {
            return 2;
        }
        else
        {
            return 0;

        }
    }

    private int ObtenerMiles()
    {

        if (CheckMiles.Checked == true)
        {
            return 1;
        }
        else
        {
            return 0;

        }

    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_Reporte.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Reporte, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Reporte, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //Copiar
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Reporte, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }
    
    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Configuración Acumulados", "PnlMPFormTituloApartado");
    }
    
    private int ValCombo()
    {
        if (rCboTipoColumn.SelectedValue == "0")
        {
            // El id de Formulas es 0
            return  0;
        }
        return 1;
    }

    private bool ValComboBool()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptAcumReporteColumnas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@idTipoColumna", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rCboTipoColumn.SelectedValue));
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {
           return  Convert.ToBoolean(ds.Tables[0].Rows[0]["permiteOperaciones"].ToString());
        }
   
        return false;
    }
    #endregion

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

        LlenaGridReporte();
        LlenaGridReporteColumnas("");

    }


}