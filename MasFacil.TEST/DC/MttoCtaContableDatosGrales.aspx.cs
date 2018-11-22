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
public partial class DC_MttoCtaContableDatosGrales : System.Web.UI.Page
{

    #region VARIABLES
    ws.Servicio oWS = new ws.Servicio();
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_smaUser;
    private string PagLoc_Folio;
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
    protected void rCboTipoCta_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaCboCuentaContableSubTipo();
    }

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
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
        Pag_smaUser = LM.sValSess(this.Page, 1);

        PagLoc_Folio = Convert.ToString(Session["folio_Selection"]);
     
    
    }
    private void InicioPagina() {

        if (PagLoc_Folio == "")
        {
            hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        }
        else
        {
            hdfBtnAccion.Value = "";
        }

        LlenaCboCuentaContableTipo();
        LimpiaDatosCuentaContable();
        CargaDatosCuentaContable();
        ControlesAccion();
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
        FNBtn.MAPerfiles_Operacion_Acciones(Page, Pag_sConexionLog, Pag_sCompania, maUser, Pag_sidM);
    }
    private void LlenaCboCuentaContableTipo() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentaContableTipo";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref rCboTipoCta, ds, "ctaContTip", "ctaContTipDes", true, false);
        ((Literal)rCboTipoCta.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboTipoCta.Items.Count);
    }
    private void LlenaCboCuentaContableSubTipo() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentaContableSubTipo";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 15, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@ctaContTip", DbType.String, 3, ParameterDirection.Input, rCboTipoCta.SelectedValue.ToString());
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref rCboSubTipoCta, ds, "ctaContSubTip", "ctaContSubDes", true, false);
        ((Literal)rCboSubTipoCta.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboSubTipoCta.Items.Count);
    }
    private void LimpiaDatosCuentaContable()
    {
        //Cuenta
        rTxtCodigo.Text = "";
        rTxtDescripcion.Text = "";
        rTxtAbreviatura.Text = "";
        //Datos de la cuenta
        rBtnNat_Acredora.Checked = true;
        rBtnNat_Deudora.Checked = false;
        rCboTipoCta.ClearSelection();
        rCboSubTipoCta.ClearSelection();
        //Manejo de la cuenta
        rBtnApliMov_Si.Checked = true;
        rBtnApliMov_No.Checked = false;
        rBtnIntSaldo_Si.Checked = true;
        rBtnIntSaldo_No.Checked = false;
        rBtnSaldoProm_Si.Checked = true;
        rBtnSaldoProm_No.Checked = false;
        rBtnCC_Si.Checked = true;
        rBtnCC_No.Checked = false;
        rBtnTipoCambio_Corr.Checked = true;
        rBtnTipoCambio_Prom.Checked = false;
        rBtnTipoCambio_Hist.Checked = false;
    }
    private void CargaDatosCuentaContable()
    {

        DataSet ds = dsCuentasContables();
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            //Cuenta
            rTxtCodigo.Text = Convert.ToString(ds.Tables[0].Rows[0]["ctaContCveFotmat"]);
            rTxtDescripcion.Text = Convert.ToString(ds.Tables[0].Rows[0]["ctaContNom"]);
            rTxtAbreviatura.Text = Convert.ToString(ds.Tables[0].Rows[0]["ctaContAbr"]);

            //Datos de la Cuenta
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ctaContNat"]) == 1)
            {
                rBtnNat_Acredora.Checked = true;
                rBtnNat_Deudora.Checked = false;
            }
            else
            {
                rBtnNat_Acredora.Checked = false;
                rBtnNat_Deudora.Checked = true;
            }
            rCboTipoCta.SelectedValue = ds.Tables[0].Rows[0]["ctaContTip"].ToString();
            LlenaCboCuentaContableSubTipo();
            rCboSubTipoCta.SelectedValue = ds.Tables[0].Rows[0]["ctaContSubTip"].ToString();


            //Manejo de la cuenta
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ctaContApliMov"]) == 1)
            {
                rBtnApliMov_Si.Checked = true;
                rBtnApliMov_No.Checked = false;
            }
            else
            {
                rBtnApliMov_Si.Checked = false;
                rBtnApliMov_No.Checked = true;
            }

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ctaContManInte"])==1)
            {
                rBtnIntSaldo_Si.Checked = true;
                rBtnIntSaldo_No.Checked = false;
            }
            else
            {
                rBtnIntSaldo_Si.Checked = false;
                rBtnIntSaldo_No.Checked = true;
            }

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ctaContManProm"])==1)
            {
                rBtnSaldoProm_Si.Checked = true;
                rBtnSaldoProm_No.Checked = false;
            }
            else
            {
                rBtnSaldoProm_Si.Checked = false;
                rBtnSaldoProm_No.Checked = true;
            }

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["ctaContManCCO"])==1)
            {
                rBtnCC_Si.Checked = true;
                rBtnCC_No.Checked = false;
            }
            else
            {
                rBtnCC_Si.Checked = false;
                rBtnCC_No.Checked = true;
            }

            Int32 iTipoCambio = Convert.ToInt32(ds.Tables[0].Rows[0]["ctaContManTipCam"]);
            if (iTipoCambio == 1)
            {
                rBtnTipoCambio_Corr.Checked = true;
                rBtnTipoCambio_Prom.Checked = false;
                rBtnTipoCambio_Hist.Checked = false;
            }
            else if (iTipoCambio == 2)
            {
                rBtnTipoCambio_Corr.Checked = false;
                rBtnTipoCambio_Prom.Checked = true;
                rBtnTipoCambio_Hist.Checked = false;
            }
            else if (iTipoCambio == 3)
            {
                rBtnTipoCambio_Corr.Checked = false;
                rBtnTipoCambio_Prom.Checked = false;
                rBtnTipoCambio_Hist.Checked = true;
            }

        }

    }
    private void InicioCtrlsEnabled()
    {
        Boolean bEnabled = false;
        rTxtCodigo.Enabled = false;

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            bEnabled = true;
            rTxtCodigo.Enabled = true;
        }

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            bEnabled = true;
            rTxtCodigo.Enabled = false;
        }

        rTxtDescripcion.Enabled = bEnabled;
        rTxtAbreviatura.Enabled = bEnabled;

        //Datos de la Cuenta
        rBtnNat_Deudora.Enabled = bEnabled;
        rBtnNat_Acredora.Enabled = bEnabled;
        rCboTipoCta.Enabled = bEnabled;
        rCboSubTipoCta.Enabled = bEnabled;

        //Manejo de la cuenta
        rBtnApliMov_Si.Enabled = bEnabled;
        rBtnApliMov_No.Enabled = bEnabled;
        rBtnIntSaldo_Si.Enabled = bEnabled;
        rBtnIntSaldo_No.Enabled = bEnabled;
        rBtnSaldoProm_Si.Enabled = bEnabled;
        rBtnSaldoProm_No.Enabled = bEnabled;
        rBtnCC_Si.Enabled = bEnabled;
        rBtnCC_Si.Enabled = bEnabled;
        rBtnTipoCambio_Corr.Enabled = bEnabled;
        rBtnTipoCambio_Prom.Enabled = bEnabled;
        rBtnTipoCambio_Hist.Enabled = bEnabled;

    }
    private void ControlesAccion()
    {

        //===> CONTROLES GENERAL
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;

        InicioCtrlsEnabled(); 

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() )
        {           
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }


        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }
    }
    
    private void EjecutaAccion()
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
    private void EjecutaSpAcciones() {
        DataSet ds = new DataSet();

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentasContables";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
        //Cuenta
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, rTxtCodigo.Text.Trim());
        ProcBD.AgregarParametrosProcedimiento("@ctaContNom", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text.Trim());
        ProcBD.AgregarParametrosProcedimiento("@ctaContAbr", DbType.String, 20, ParameterDirection.Input,rTxtAbreviatura.Text.Trim());
        //Datos de la Cuenta
        ProcBD.AgregarParametrosProcedimiento("@ctaContNat", DbType.Int64, 0, ParameterDirection.Input, iValorInt(rBtnNat_Acredora.Checked));
        ProcBD.AgregarParametrosProcedimiento("@ctaContTip", DbType.String, 1, ParameterDirection.Input, rCboTipoCta.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@ctaContSubTip", DbType.String, 2, ParameterDirection.Input, rCboSubTipoCta.SelectedValue);
        //Manejo de la Cuenta
        ProcBD.AgregarParametrosProcedimiento("@ctaContApliMov", DbType.Int64, 0, ParameterDirection.Input, iValorInt(rBtnApliMov_Si.Checked));
        ProcBD.AgregarParametrosProcedimiento("@ctaContManInte", DbType.Int64, 0, ParameterDirection.Input, iValorInt(rBtnIntSaldo_Si.Checked));
        ProcBD.AgregarParametrosProcedimiento("@ctaContManProm", DbType.Int64, 0, ParameterDirection.Input, iValorInt(rBtnSaldoProm_Si.Checked));
        ProcBD.AgregarParametrosProcedimiento("@ctaContManCCO", DbType.Int64, 0, ParameterDirection.Input, iValorInt(rBtnCC_Si.Checked));
        ProcBD.AgregarParametrosProcedimiento("@ctaContManTipCam", DbType.Int64, 0, ParameterDirection.Input, iValorIntTipoCambio());

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sEjecEstatus == "1")
            {
                PagLoc_Folio = rTxtCodigo.Text.Trim();
                Session["folio_Selection"] = ds.Tables[0].Rows[0]["ctaContCve"].ToString();
                Valores_InicioPag();
                InicioPagina();
            }
            ShowAlert(sEjecEstatus, sEjecMSG);
        }
    }
    private void EjecutaAccionLimpiar() {
        LimpiaDatosCuentaContable();
        CargaDatosCuentaContable();
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
        int camposInc = 0;

        if (rTxtCodigo.Text.Trim() == "")
        {
            rTxtCodigo.CssClass = "cssTxtInvalid";
            camposInc += 1;
        }
        else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }

        if (rTxtDescripcion.Text.Trim() == "")
        {
            rTxtDescripcion.CssClass = "cssTxtInvalid";
            camposInc += 1;
        }
        else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }

        if (rCboTipoCta.SelectedIndex == -1)
        {
            rCboTipoCta.BorderWidth = Unit.Pixel(1);
            rCboTipoCta.BorderColor = System.Drawing.Color.Red;
            camposInc += 1;
        }
        else
        {
            rCboTipoCta.BorderColor = System.Drawing.Color.Transparent;
        }

        if (rCboSubTipoCta.SelectedIndex == -1)
        {
            rCboSubTipoCta.BorderWidth = Unit.Pixel(1);
            rCboSubTipoCta.BorderColor = System.Drawing.Color.Red;
            camposInc += 1;
        }
        else
        {
            rCboSubTipoCta.BorderColor = System.Drawing.Color.Transparent;
        }

        if (camposInc > 0)
        {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
        }

        return sResult;
    }
    DataSet dsCuentasContables()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentasContables";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, PagLoc_Folio);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    Boolean bValorBoolean(int iValor) {
        bool bValor = true;
        if (iValor != 1) {
            bValor = false; 
        }
        return bValor; 
    }
    int iValorInt(Boolean bValor)
    {
        int iValor = 1;
        if (bValor != true) {
            iValor = 2;
        }
        return iValor;
    }
    int iValorIntTipoCambio()
    {
        int iValor = 1;

        if (rBtnTipoCambio_Corr.Checked) {
            iValor = 1;
        }

        if (rBtnTipoCambio_Prom.Checked)
        {
            iValor = 2;
        }

        if (rBtnTipoCambio_Hist.Checked)
        {
            iValor = 3;
        }
        
        return iValor;
    }
    #endregion

}