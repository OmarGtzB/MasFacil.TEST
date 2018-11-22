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
public partial class CC_AdmonComplementoPagosRegistro : System.Web.UI.Page
{


    #region Variables
    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    ws.Servicio oWS = new ws.Servicio();

    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FNPeriodosCalendario FNPeriodo = new MGMFnGrales.FNPeriodosCalendario();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;

    private string Pag_sIdDocReg;
    private string Pag_cptoTip;

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

    //Botones Detalle
    protected void rBtnNuevo_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        //hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();

        //if (rBtnRegTransaccion.Checked == true)
        //{
        //    ControlesAccionTrans();
        //}
        //if (rBtnRegMovimiento.Checked == true)
        //{
        //    ControlesAccionMov();
        //}
    }

    protected void rBtnModificar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        //hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        //if (rBtnRegTransaccion.Checked == true)
        //{
        //    ControlesAccionTrans();
        //}
        //if (rBtnRegMovimiento.Checked == true)
        //{
        //    ControlesAccionMov();
        //}
    }

    protected void rBtnEliminar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        //hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        //if (rBtnRegTransaccion.Checked == true)
        //{
        //    ControlesAccionTrans();
        //}
        //if (rBtnRegMovimiento.Checked == true)
        //{
        //    ControlesAccionMov();
        //}
    }

    protected void rBtnLimpiar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        //hdfBtnAccionDet.Value = "";

        //rGdvOperacionesTrans.MasterTableView.ClearSelectedItems();
        //rGdvOperacionesMovCC.MasterTableView.ClearSelectedItems();
        //rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
        //rGdvOperacionesTrans.AllowMultiRowSelection = true;
        //rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
        //rGdvOperacionesMovCC.AllowMultiRowSelection = true;
    }

    //Botones Ejecuta Accion
    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        if (hdfBtnAccionDet.Value == "")
        {
            EjecutaAccion();
        }
        else
        {
            //EjecutaAccionDetalle();
        }
    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        //Int64 Pag_sidM = 0;
        //if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        //{
        //    Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        //}
        //Response.Redirect("~/CC/AdmonCobros.aspx?idM=" + Pag_sidM);
    }

    //Grids
    protected void rGdvOperacionesTrans_SelectedIndexChanged(object sender, EventArgs e)
    {
        //var dataItem = this.rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;
        //if (dataItem != null)
        //{
        //    hdf_transDetSec.Value = dataItem["transDetSec"].Text;
        //}
    }



    // Radio Buttons
    protected void rBtnRegTransaccion_CheckedChanged(object sender, EventArgs e)
    {
        //CheckRegistro();
    }

    protected void rBtnRegMovimiento_CheckedChanged(object sender, EventArgs e)
    {
        if (hdfPag_sOpe.Value != "" && rGdvOperacionesTrans.Items.Count > 0)
        {

            if (rGdvOperacionesTrans.SelectedItems.Count == 0)
            {
                rBtnRegTransaccion.Checked = true;
                rBtnRegMovimiento.Checked = false;

                string sMSGTip = "";
                string sResult = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                ShowAlert(sMSGTip, sResult);
            }
            else
            {
                CheckRegistro();
            }

        }
        else
        {
            rBtnRegTransaccion.Checked = true;
            rBtnRegMovimiento.Checked = false;

            string sMSGTip = "";
            string sResult = "";
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1019", ref sMSGTip, ref sResult);
            ShowAlert(sMSGTip, sResult);

        }
    }


    //Combos 
    protected void rCboConcepto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FolioAut_Trans();

        //getInvisibleGrid();
        //Session["dsTransDetCCSession"] = null;
        //loadGrid();
        //getFolios();
        //Transacciones_VizualizaColumGrid();
    }
    protected void rCboBancoOrdenante_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_InstitucionesDeposito";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@insDepCve", DbType.String, 20, ParameterDirection.Input, rCboBancoOrdenante.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        rTxtRFCCuentaOrdenante.Text = "";
        rTxtNombreBancoOrdenante.Text = "";
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rTxtRFCCuentaOrdenante.Text = ds.Tables[0].Rows[0]["insDepRFC"].ToString();
            rTxtNombreBancoOrdenante.Text = ds.Tables[0].Rows[0]["insDepRazSoc"].ToString();
        }
    }
    protected void rCboCuentaDestino_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentasDeposito";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cDepCve", DbType.String, 10, ParameterDirection.Input, rCboCuentaDestino.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        rTxtRFCCuentaDestino.Text = "";
        rTxtCuentaDondeSeRecibePago.Text = ""; 
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rTxtRFCCuentaDestino.Text = ds.Tables[0].Rows[0]["insDepRFC"].ToString();
            rTxtCuentaDondeSeRecibePago.Text = ds.Tables[0].Rows[0]["ctaDepNoCta"].ToString();
        }
    }

    //Fecha
    protected void RdDateFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        DataSet ValPeriodo = new DataSet();
        ValPeriodo = FNPeriodo.dsValidaPeriodoFecha(Pag_sConexionLog, Pag_sCompania, Convert.ToDateTime(RdDateFecha.SelectedDate));

        string maMSGTip = ValPeriodo.Tables[0].Rows[0]["maMSGTip"].ToString().Trim();
        string maMSGDes = ValPeriodo.Tables[0].Rows[0]["maMSGDes"].ToString().Trim();

        if (maMSGTip != "1")
        {
            RdDateFecha.Clear();
            ShowAlert(maMSGTip, maMSGDes);
        }
        else
        {
            Session["fchMovCC"] = RdDateFecha.SelectedDate;
            setFecMovToGrid();
        }
    }


    //RedAjaxManager
    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        //if (rBtnRegTransaccion.Checked == true)
        //{

        //    rGdvOperacionesTrans.DataBind();
        //    FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, (DataSet)Session["dsTransDetCCSession"]);

        //}
        //if (rBtnRegMovimiento.Checked == true)
        //{

        //    FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, (DataSet)Session["dsMovDetSession"]);

        //    //FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, (DataSet)Session["dsTransMovXPSession"]);
        //    rGdvOperacionesMovCC.DataBind();

        //}
        //hdfBtnAccionDet.Value = "";
        //ControlesAccion();

        //rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
        //rGdvOperacionesTrans.AllowMultiRowSelection = true;
        //rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
        //rGdvOperacionesMovCC.AllowMultiRowSelection = true;
    }


    #endregion

    #region METODOS
    private void Valores_InicioPag()
        {
            Pag_sCompania = Convert.ToString(Session["Compania"]);
            Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
            Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);

            hdfBtnAccion.Value = Convert.ToString(Session["Valor_btn"]);
            Session["TipoCptoOpe"] = "XP";
        }
    public void InicioPagina()
    {
        //hdfPag_sOpe.Value = "";
        //if (Request.QueryString["Ope"] != null && Request.QueryString["Ope"] != "")
        //{
        //    hdfPag_sOpe.Value = Request.QueryString["Ope"];
        //}

        //if (Request.QueryString["cptoTip"] != null && Request.QueryString["cptoTip"] != "")
        //{
        //    Pag_cptoTip = Request.QueryString["cptoTip"];
        //}
        //else
        //{
        //    Pag_cptoTip = "0";
        //}


        TituloPagina();
        //Valores_InicioPag();
        LlenaControles();


        //Session["dsTransDetCCSession"] = null;

        //if (hdfBtnAccion.Value == "2")
        //{
        //    LlenarUi(Pag_sIdDocReg);

        //    RdDateFecha.Enabled = false;
        //    rCboBeneficiario.Enabled = false;
        //    rCboConcepto.Enabled = false;

        //}
        //else
        //{
        //    cleanUi();
        //}

        //loadGrid();
        //ControlesAccion();
        CheckRegistro();


        //rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
        //rGdvOperacionesTrans.AllowMultiRowSelection = true;
        //rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
        //rGdvOperacionesMovCC.AllowMultiRowSelection = true;

    }

    private void TituloPagina()
    {
        if (hdfBtnAccion.Value == "1")
        {
            FNGrales.bTitleDesc(Page, "Nuevo Registro - Administración Complemento de Pagos", "PnlMPFormTituloApartado");
        }
        else if (hdfBtnAccion.Value == "2")
        {
            FNGrales.bTitleDesc(Page, "Editar Registro - Administración Complemento de Pagos", "PnlMPFormTituloApartado");
        }

    }

    private void LlenaControles()
    {
        RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, ref rCboConcepto, true, false);
        FnCtlsFillIn.RabComboBox_Clientes(Pag_sConexionLog, Pag_sCompania, ref rCboCliente, true, false);
        FnCtlsFillIn.RadComboBox_InstitucionesDeposito(Pag_sConexionLog, Pag_sCompania, ref rCboBancoOrdenante, true, false);
        RadComboBox_CuentasDeposito(Pag_sConexionLog, Pag_sCompania, ref rCboCuentaDestino, true, false);

        FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false);
        FnCtlsFillIn.RadComboBox_Sucursales(Pag_sConexionLog, Pag_sCompania,50, ref rCboSucursal, true, false); 
    }
    
    private void CheckRegistro()
    {
        // CheckControlespnlBtnsAcciones();

        //Transacciones_DatosGenerales();

        if (rBtnRegTransaccion.Checked == true)
        {

            DataSet dsTransDet = new DataSet();
            dsTransDet = (DataSet)Session["dsTransDetCCSession"];

            if (dsTransDet == null)
            {
                Transacciones_DatosDetalle();
            }
            else
            {

                FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, dsTransDet);
                //Transacciones_VizualizaColumGrid();
                rGdvOperacionesTrans.Visible = true;
                rGdvOperacionesMovCC.Visible = false;
            }

        }

        if (rBtnRegMovimiento.Checked == true)
        {

            //Movimientos_DatosDetalle();
        }

    }
    private void Transacciones_DatosDetalle()
    {
        string Pag_sOpe = "0";


        DataSet dsTransDet = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@folioId", DbType.Int64, 0, ParameterDirection.Input, Pag_sOpe);
        dsTransDet = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        Session["dsTransDetCCSession"] = dsTransDet;


        FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, dsTransDet);
        Transacciones_VizualizaColumGrid();
        rGdvOperacionesTrans.Visible = true;
        rGdvOperacionesMovCC.Visible = false;


    }
    private void Transacciones_VizualizaColumGrid()
    {
        DataSet dsConfigConcepto = new DataSet();
        dsConfigConcepto = ConceptoConfiguracion();
        if (dsConfigConcepto.Tables.Count > 0)
        {

            foreach (DataRow dr in dsConfigConcepto.Tables[0].Rows)
            {
                rGdvOperacionesTrans.MasterTableView.GetColumn(dr["ColumTransDet"].ToString()).Display = true;
                rGdvOperacionesTrans.MasterTableView.GetColumn(dr["ColumTransDet"].ToString()).HeaderText = dr["cptoConfDes"].ToString();
                rGdvOperacionesTrans.DataBind();
            }

        }
    }
    
    private void FolioAut_Trans()
    {

        DataSet ds = new DataSet();
        ds = ConceptoDefinicion();

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            string cptoDefFolTip = ds.Tables[0].Rows[0]["cptoDefFolTip"].ToString();
            string cptoDefFolVal = ds.Tables[0].Rows[0]["cptoDefFolVal"].ToString();

            if (cptoDefFolTip == "2")
            {
                rTxtFolio.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, cptoDefFolVal, Convert.ToInt32(cptoDefFolTip), "");
                rTxtFolio.Enabled = false;
            }
            else
            {

                if (rTxtFolio.Text.Trim() != "")
                {
                    rTxtFolio.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, cptoDefFolVal, Convert.ToInt32(cptoDefFolTip), rTxtFolio.Text.Trim());
                }
                rTxtFolio.Enabled = true;
            }



        }
        else
        {
            rTxtFolio.Text = "";
            rTxtFolio.Enabled = false;
        }

    }

    private void setFecMovToGrid()
    {
        //if (ObtenerFechaHead() == "1/01/01")
        //{
        //    //dr["movFec_Mov"] = DBNull.Value;
        //}
        //else
        //{

        //    DataSet dsTransMov = new DataSet();
        //    dsTransMov = (DataSet)Session["dsTransDetCCSession"];

        //    if (dsTransMov != null)
        //    {
        //        if (dsTransMov.Tables[0].Rows.Count != 0)
        //        {
        //            foreach (DataRow drConfCpto in dsTransMov.Tables[0].Rows)
        //            {

        //                drConfCpto[getColumn("cptoConfId_Fec_Mov")] = RdDateFecha.SelectedDate;
        //            }
        //        }
        //        dsTransMov = (DataSet)Session["dsTransDetCCSession"];
        //        FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, (DataSet)Session["dsTransDetCCSession"]);
        //        rGdvOperacionesTrans.DataBind();
        //    }

        //}

    }



    //Ejecucion
    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                EjecutaSpAcciones();
            }
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }

    }

    private void EjecutaSpAcciones()
    {
        string Val_TransFec = ObtenerFecha();
        string maUser = LM.sValSess(this.Page, 1);
        //string Folio = ObtenFolio();

        try
        {

        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
        }

    }


    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES
    DataSet ConceptoDefinicion()
    {
        DataSet ds = new DataSet();

        if (rCboConcepto.SelectedIndex != -1)
        {
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        }

        return ds;
    }
    DataSet ConceptoConfiguracion()
    {
        DataSet ds = new DataSet();

        if (rCboConcepto.SelectedIndex != -1)
        {
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        }
        return ds;
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            // ====== Valores Requeridos Datos Generales  ====== 
            //Concepto
            if (rCboConcepto.SelectedValue == "")
            {
                rCboConcepto.CssClass = "cssTxtInvalid";
                rCboConcepto.BorderWidth = Unit.Pixel(1);
                rCboConcepto.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboConcepto.BorderColor = System.Drawing.Color.Transparent; }
            //Folio
            if (rTxtFolio.Text == "")
            {
                rTxtFolio.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtFolio.CssClass = "cssTxtEnabled";
            }
            //CLiente
            if (rCboCliente.SelectedValue == "")
            {
                rCboCliente.CssClass = "cssTxtInvalid";
                rCboCliente.BorderWidth = Unit.Pixel(1);
                rCboCliente.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboCliente.BorderColor = System.Drawing.Color.Transparent; }



            // ====== Valores Requeridos Recepcion del Pago ====== 
            //Fecha de Pago
            if (RdDateFecha.SelectedDate.ToString() == "")
            {
                RdDateFecha.CssClass = "cssTxtInvalid";
                RdDateFecha.BorderWidth = Unit.Pixel(1);
                RdDateFecha.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else
            {
                RdDateFecha.CssClass = "cssTxtEnabled";
                RdDateFecha.BorderWidth = Unit.Pixel(1);
                RdDateFecha.BorderColor = System.Drawing.Color.Transparent;
            }
            //Monto
            if (rTxtMonto.Text == "")
            {
                rTxtMonto.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtMonto.CssClass = "cssTxtEnabled";
            }
            //Moneda
            if (rCboMoneda.SelectedValue == "")
            {
                rCboMoneda.CssClass = "cssTxtInvalid";
                rCboMoneda.BorderWidth = Unit.Pixel(1);
                rCboMoneda.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboMoneda.BorderColor = System.Drawing.Color.Transparent; }
            //Tipo de Cambio
            if (rTxtTipoCambio.Text == "")
            {
                rTxtTipoCambio.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtTipoCambio.CssClass = "cssTxtEnabled";
            }
            //Banco Ordenante
            if (rCboBancoOrdenante.SelectedValue == "")
            {
                rCboBancoOrdenante.CssClass = "cssTxtInvalid";
                rCboBancoOrdenante.BorderWidth = Unit.Pixel(1);
                rCboBancoOrdenante.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboBancoOrdenante.BorderColor = System.Drawing.Color.Transparent; }
            //RFC Cuenta Ordenante
            if (rTxtRFCCuentaOrdenante.Text == "")
            {
                rTxtRFCCuentaOrdenante.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtRFCCuentaOrdenante.CssClass = "cssTxtEnabled";
            }
            //Nombre Banco Ordenante
            if (rTxtNombreBancoOrdenante.Text == "")
            {
                rTxtNombreBancoOrdenante.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtNombreBancoOrdenante.CssClass = "cssTxtEnabled";
            }
            //Cuenta de Destino
            if (rCboCuentaDestino.SelectedValue == "")
            {
                rCboCuentaDestino.CssClass = "cssTxtInvalid";
                rCboCuentaDestino.BorderWidth = Unit.Pixel(1);
                rCboCuentaDestino.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboCuentaDestino.BorderColor = System.Drawing.Color.Transparent; }
            //RFC Cuenta Destino
            if (rTxtRFCCuentaDestino.Text == "")
            {
                rTxtRFCCuentaDestino.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtRFCCuentaDestino.CssClass = "cssTxtEnabled";
            }
            //Cta. donde Recibio el Pago
            if (rTxtCuentaDondeSeRecibePago.Text == "")
            {
                rTxtCuentaDondeSeRecibePago.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtCuentaDondeSeRecibePago.CssClass = "cssTxtEnabled";
            }

            if (rGdvOperacionesTrans.Items.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1019", ref sMSGTip, ref sResult);
                return sResult;
            }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvOperacionesTrans.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }


        return sResult;
    }



    public string ObtenerFecha()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha.SelectedDate);
        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }

    public bool RabComboBox_ConceptoReferenciaTipo_SegUsuario(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 103);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "cptoId", "cptoDes", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }
    public bool RadComboBox_CuentasDeposito(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentasDeposito";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "ctaDepCve", "ctaDepDes", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }
    #endregion



}