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

public partial class XP_AdmonPagosRegistro : System.Web.UI.Page
{
    #region VARIABLES

    ws.Servicio oWS = new ws.Servicio();

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
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
            addRadWin();
            if (!IsPostBack)
            {
                InicioPagina();
            }
        }
    }

    protected void rBtnNuevo_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();

        if (rBtnRegTransaccion.Checked == true)
        {
            ControlesAccionTrans();
        }
        if (rBtnRegMovimiento.Checked == true)
        {
            ControlesAccionMov();
        }
    }

    protected void rBtnModificar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();

        if (rBtnRegTransaccion.Checked == true)
        {
            ControlesAccionTrans();
        }
        if (rBtnRegMovimiento.Checked == true)
        {
            ControlesAccionMov();
        }
    }

    protected void rBtnEliminar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();

        if (rBtnRegTransaccion.Checked == true)
        {
            ControlesAccionTrans();
        }
        if (rBtnRegMovimiento.Checked == true)
        {
            ControlesAccionMov();
        }
    }

    protected void rBtnLimpiar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = "";
        ControlesAccion();
        rGdvOperacionesTrans.MasterTableView.ClearSelectedItems();
        rGdvOperacionesMovCC.MasterTableView.ClearSelectedItems();
        rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvOperacionesTrans.AllowMultiRowSelection = true;
        rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvOperacionesMovCC.AllowMultiRowSelection = true;
    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        Int64 Pag_sidM = 0;
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        }
        Response.Redirect("~/XP/AdmonPagos.aspx?idM=" + Pag_sidM); ;

    }

    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {

        if (hdfBtnAccionDet.Value == "")
        {
            EjecutaAccion();
        }
        else
        {
            EjecutaAccionDetalle();
        }
        
    }

    protected void RdDateFecha_Final_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {

    }

    protected void RdDateFecha_Inicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {

    }

    protected void RdDateFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {

        DataSet ValPeriodo = new DataSet();
        ValPeriodo = FNPeriodo.dsValidaPeriodoFecha(Pag_sConexionLog, Pag_sCompania, Convert.ToDateTime(RdDateFecha.SelectedDate));

        string maMSGTip = ValPeriodo.Tables[0].Rows[0]["maMSGTip"].ToString().Trim();
        string maMSGDes = ValPeriodo.Tables[0].Rows[0]["maMSGDes"].ToString().Trim();

        if (maMSGTip != "1")
        {
            RdDateFecha.Clear();
            ShowAlert("2", maMSGDes);
        }else
        {
            setFecMovToGrid();
        }
        
    }
    
    protected void rBtnRegTransaccion_CheckedChanged(object sender, EventArgs e)
    {
        CheckRegistro();
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

    protected void rCboConcepto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        getInvisibleGrid();
        Session["dsTransDetXPSession"] = null;
        loadGrid();
        getFolios();
        Transacciones_VizualizaColumGrid();
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

        if (rBtnRegTransaccion.Checked == true)
        {

            rGdvOperacionesTrans.DataBind();
            FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, (DataSet)Session["dsTransDetXPSession"]);

        }
        if (rBtnRegMovimiento.Checked == true)
        {

            FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, (DataSet)Session["dsMovDetSession"]);

            //FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, (DataSet)Session["dsTransMovXPSession"]);
            rGdvOperacionesMovCC.DataBind();

        }
        hdfBtnAccionDet.Value = "";
        ControlesAccion();

        rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvOperacionesTrans.AllowMultiRowSelection = true;
        rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvOperacionesMovCC.AllowMultiRowSelection = true;


    }

    protected void rCboBeneficiario_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        getInvisibleGrid();
        Session["dsTransDetXPSession"] = null;
        loadGrid();
    }

    protected void rGdvOperacionesMovCC_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rGdvOperacionesTrans_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = this.rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            hdf_transDetSec.Value = dataItem["transDetSec"].Text;
        }
    }

    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_sIdDocReg = Convert.ToString(Session["Valor_DocCve"]);
        hdfBtnAccion.Value = Convert.ToString(Session["Valor_btn"]);
        Session["TipoCptoOpe"] = "XP";
    }

    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
    }

    public void InicioPagina()
    {
        hdfPag_sOpe.Value = "";
        if (Request.QueryString["Ope"] != null && Request.QueryString["Ope"] != "")
        {
            hdfPag_sOpe.Value = Request.QueryString["Ope"];
        }

        if (Request.QueryString["cptoTip"] != null && Request.QueryString["cptoTip"] != "")
        {
            Pag_cptoTip = Request.QueryString["cptoTip"];
        }
        else
        {
            Pag_cptoTip = "0";
        }

        //rBtnRegTransaccion.Checked = true;
        //rBtnRegMovimiento.Checked = false;

        TituloPagina();
        Valores_InicioPag();
        llenaCombos();
        

        Session["dsTransDetXPSession"] = null;

        if (hdfBtnAccion.Value == "2")
        {
            LlenarUi(Pag_sIdDocReg);
            
            
            RdDateFecha.Enabled = false;
            rCboBeneficiario.Enabled = false;
            rCboConcepto.Enabled = false;

        }
        else
        {
            cleanUi();
        }

        
        loadGrid();
        ControlesAccion();


        CheckRegistro();

        //Transacciones_VizualizaColumGrid();


        rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvOperacionesTrans.AllowMultiRowSelection = true;
        rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvOperacionesMovCC.AllowMultiRowSelection = true;


    }

    private void TituloPagina()
    {
        if (hdfBtnAccion.Value == "1")
        {
            FNGrales.bTitleDesc(Page, "Nuevo Registro - Administración de Pagos", "PnlMPFormTituloApartado");
        }
        else if (hdfBtnAccion.Value == "2")
        {
            FNGrales.bTitleDesc(Page, "Editar Registro - Administración de Pagos", "PnlMPFormTituloApartado");
        }
    }

    private void llenaCombos()
    {
        RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, ref rCboConcepto, true, false);
        RadComboBox_CuentasDeposito(Pag_sConexionLog, Pag_sCompania, ref rCboCuenta, true, false);
        llenaComboProv(Pag_sConexionLog, Pag_sCompania, ref rCboBeneficiario, true, false);
    }

    private void CheckRegistro()
    {
        Valores_InicioPag();

        string sValue = Convert.ToString(Session["TipoCptoOpe"]);
        if (sValue != "")
        {

            if (sValue == "CG")
            {
                rBtnRegTransaccion.Visible = false;
                rBtnRegMovimiento.Visible = false;
            }
            else
            {
                rBtnRegTransaccion.Visible = true;
                rBtnRegMovimiento.Visible = true;
            }
        }

        CheckControlespnlBtnsAcciones();
        Transacciones_DatosGenerales();

        if (rBtnRegTransaccion.Checked == true)
        {
            DataSet dsTransDet = new DataSet();
            dsTransDet = (DataSet)Session["dsTransDetXPSession"];

            if (dsTransDet == null)
            {
                Transacciones_DatosDetalle();
            }
            else
            {
                FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, dsTransDet);
                Transacciones_VizualizaColumGrid();
                rGdvOperacionesTrans.Visible = true;
                rGdvOperacionesMovCC.Visible = false;
            }
        }

        if (rBtnRegMovimiento.Checked == true)
        {
            Movimientos_DatosDetalle();
        }
        rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvOperacionesTrans.AllowMultiRowSelection = true;
        rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvOperacionesMovCC.AllowMultiRowSelection = true;
    }

    private void LlenarUi(string docRegId)
    {

        try
        {
            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PagosXP";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@pagXPId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                rCboCuenta.SelectedValue = ds.Tables[0].Rows[0]["ctaDepCve"].ToString();
                RdDateFecha.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["pagXPFec"].ToString());
                rCboConcepto.SelectedValue = ds.Tables[0].Rows[0]["cptoId"].ToString();
                rCboBeneficiario.SelectedValue = ds.Tables[0].Rows[0]["provCve"].ToString();
                rTxtDescripcion.Text = ds.Tables[0].Rows[0]["pagXPDes"].ToString();
                rTxtImporte.Text = ds.Tables[0].Rows[0]["pagXPImp"].ToString();
                rTxtImporte.Value = Convert.ToDouble(ds.Tables[0].Rows[0]["pagXPImp"].ToString());
                rTxtFolio.Text = ds.Tables[0].Rows[0]["pagXPFolio"].ToString();
                rTxtBeneficiario.Text = ds.Tables[0].Rows[0]["pagXPBenef"].ToString();
                rTxtImporte.DataBind();

                hdfPag_sOpe.Value = ds.Tables[0].Rows[0]["transId"].ToString();

                //GRET, se asigna folio de cheque a campo oculto
                hdfPag_PagXPNum.Value = ds.Tables[0].Rows[0]["pagXPNum"].ToString();
                //

                if (ds.Tables[0].Rows[0]["transDetId"].ToString().Trim() != "")
                {
                    Session["dsTransDetXPSession"] = ds;
                }

            }
        }
        catch (Exception)
        {

            throw;
        }

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

    private void cleanUi()
    {
        rTxtImporte.Text = "0.00";
        rTxtBeneficiario.Text = "";
        rTxtDescripcion.Text = "";
        rCboConcepto.ClearSelection();
        rCboCuenta.ClearSelection();
        RdDateFecha.Clear();
    }

    private void loadGrid()
    {
        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsTransDetXPSession"];

        if (dsTransDet == null)
        {
            Transacciones_DatosDetalle();
        }
        else
        {
            FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, dsTransDet);
            rGdvOperacionesTrans.Visible = true;
        }
    }

    private void CheckControlespnlBtnsAcciones()
    {
        if (hdfBtnAccion.Value == "")
        {
            pnlBtnsAcciones.Visible = false;
            rBtnGuardar.Visible = false;
        }
        else
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                pnlBtnsAcciones.Visible = false;
                rBtnGuardar.Visible = false;
                if (rBtnRegTransaccion.Checked == true)
                {
                    pnlBtnsAcciones.Visible = true;
                    rBtnGuardar.Visible = true;
                }
                else
                {
                    string sValue = Convert.ToString(Session["TipoCptoOpe"]);
                    if (sValue != "")
                    {
                        if (sValue == "XP")
                        {
                            pnlBtnsAcciones.Visible = true;
                            rBtnGuardar.Visible = true;
                        }
                        if (sValue == "CC")
                        {
                            pnlBtnsAcciones.Visible = true;
                            rBtnGuardar.Visible = true;
                        }
                    }
                }

            }



            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                pnlBtnsAcciones.Visible = false;
                rBtnGuardar.Visible = false;
                if (rBtnRegTransaccion.Checked == true)
                {
                    pnlBtnsAcciones.Visible = true;
                    rBtnGuardar.Visible = true;
                }
                else
                {
                    string sValue = Convert.ToString(Session["TipoCptoOpe"]);
                    if (sValue != "")
                    {
                        if (sValue == "XP")
                        {
                            pnlBtnsAcciones.Visible = true;
                            rBtnGuardar.Visible = true;
                        }
                        if (sValue == "CC")
                        {
                            pnlBtnsAcciones.Visible = true;
                            rBtnGuardar.Visible = true;
                        }
                    }
                }
            }
        }
    }

    private void Transacciones_DatosGenerales()
    {
        if (hdfPag_sOpe.Value != "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@folioId", DbType.Int64, 0, ParameterDirection.Input, hdfPag_sOpe.Value);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (this.rCboConcepto.Items.Count > 0)
            {
                rCboConcepto.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoId"]);
            }
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
        Session["dsTransDetXPSession"] = dsTransDet;

        FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, dsTransDet);
        Transacciones_VizualizaColumGrid();
        rGdvOperacionesTrans.Visible = true;
        rGdvOperacionesMovCC.Visible = false;
    }

    private void Movimientos_DatosDetalle()
    {
        string Pag_sOpe = "0";
        string sCptoId = "0";

        if (hdfPag_sOpe.Value != "")
        {
            Pag_sOpe = hdfPag_sOpe.Value;
        }

        if (rCboConcepto.SelectedIndex != -1)
        {
            sCptoId = rCboConcepto.SelectedValue.ToString();
        }

        DataSet dsMovDet = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, sCptoId); //sCptoId
        ProcBD.AgregarParametrosProcedimiento("@folioId", DbType.Int64, 0, ParameterDirection.Input, Pag_sOpe);

        ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, hdf_transDetSec.Value);

        dsMovDet = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, dsMovDet);
        
        Session["dsMovDetSession"] = dsMovDet;
        
        rGdvOperacionesTrans.Visible = false;
        rGdvOperacionesMovCC.Visible = true;
    }

    private void ControlesAccion()
    {

        ////===> CONTROLES GENERAL

        rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvOperacionesTrans.AllowMultiRowSelection = false;


        this.rGdvOperacionesTrans.MasterTableView.ClearSelectedItems();
        this.rGdvOperacionesTrans.AllowMultiRowSelection = false;

        rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = false;
        rGdvOperacionesMovCC.MasterTableView.ClearSelectedItems();
        rGdvOperacionesMovCC.AllowMultiRowSelection = false;

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";

            rGdvOperacionesTrans.MasterTableView.ClearSelectedItems();
            rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = false;
            rGdvOperacionesTrans.AllowMultiRowSelection = false;

            rGdvOperacionesMovCC.MasterTableView.ClearSelectedItems();
            rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = false;
            rGdvOperacionesMovCC.AllowMultiRowSelection = false;
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                Session["FolioSession"] = rTxtFolio.Text;
            }

        }



        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";

            rGdvOperacionesTrans.MasterTableView.ClearSelectedItems();
            rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvOperacionesTrans.AllowMultiRowSelection = false;

            rGdvOperacionesMovCC.MasterTableView.ClearSelectedItems();
            rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvOperacionesMovCC.AllowMultiRowSelection = false;

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                Session["FolioSession"] = rTxtFolio.Text;
            }


        }

        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";

            rGdvOperacionesTrans.MasterTableView.ClearSelectedItems();
            rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvOperacionesTrans.AllowMultiRowSelection = true;

            rGdvOperacionesMovCC.MasterTableView.ClearSelectedItems();
            rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvOperacionesMovCC.AllowMultiRowSelection = true;

        }


    }

    private void EjecutaAccionDetalle()
    {
        string sMSGTip = "";
        if (rBtnRegTransaccion.Checked == true)
        {

            string msgValidacion = validaEjecutaAccionDetalle(ref sMSGTip);
            if (msgValidacion == "")
            {
                EjecutaAccionDetalle_Trans();
            }
            else
            {
                ShowAlert(sMSGTip, msgValidacion);
            }
        }

        if (rBtnRegMovimiento.Checked == true)
        {
            string msgValidacion = validaEjecutaAccionDetalle(ref sMSGTip);
            if (msgValidacion == "")
            {
                EjecutaAccionDetalle_Mov();
            }
            else
            {
                ShowAlert(sMSGTip, msgValidacion);
            }

        }

    }

    private void EjecutaAccionDetalle_Mov()
    {
        if (Session["TipoCptoOpe"].ToString() == "XP")
        {
            MovXP();
        }
    }

    public void MovXP()
    {
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            Session[("hdfBtnAccionMov")] = hdfBtnAccionDet.Value;
            string script = "function f(){$find(\"" + rdWindowMov.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //Opcion para mostrar la ventana
            Session[("hdfBtnAccionMov")] = hdfBtnAccionDet.Value;

            var dataItem = rGdvOperacionesMovCC.SelectedItems[0] as GridDataItem;
            string sMovDetId = dataItem.GetDataKeyValue("movID").ToString();
            Session["movIDSession"] = sMovDetId;

            string script = "function f(){$find(\"" + rdWindowMov.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            DataSet dsTransMov = new DataSet();
            dsTransMov = (DataSet)Session["dsMovDetSession"];

            EliminaMov();
            dsTransMov.Tables[0].AcceptChanges();

            Session["dsMovDetSession"] = dsTransMov;

            FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, (DataSet)Session["dsMovDetSession"]);
            rGdvOperacionesMovCC.DataBind();
            hdfBtnAccionDet.Value = "";

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
            rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";
        }
    }

    private void EliminaMov()
    {
        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsMovDetSession"];

        string stransDetId = "";
        var dataItem = rGdvOperacionesMovCC.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //se le asigna el id del row seleccionado del grid
            stransDetId = dataItem.GetDataKeyValue("movID").ToString();
        }

        dsTransDet.Tables[0].AcceptChanges();


        //for (int i = 0; i < dsTransDet.Tables[0].Rows.Count; i++)
        for (int i = 0; i < rGdvOperacionesMovCC.Items.Count; i++)
        {
            if (rGdvOperacionesMovCC.Items[i].Selected == true)
            {
                //DataRow dr = dsTransDet.Tables[0].Rows[i];
                //dsTransDet.Tables[0].Rows.Remove(dr);
                dsTransDet.Tables[0].Rows[i].Delete();

            }

        }
        dsTransDet.Tables[0].AcceptChanges();


        Session["dsMovDetSession"] = dsTransDet;
        FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, (DataSet)Session["dsMovDetSession"]);


        hdfBtnAccionDet.Value = "";
        ControlesAccion();
    }

    private void EjecutaAccionDetalle_Trans()
    {

        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsTransDetXPSession"];

        Session[("hdfBtnAccionDetXP")] = hdfBtnAccionDet.Value;

        Session["cptoId"] = rCboConcepto.SelectedValue;


        Session["refPrincXP"] = rCboBeneficiario.SelectedValue;

        Session["fchMovXP"] = ObtenerFecha();


        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            //VisualizaModal();
            //string script = "function f(){$find(\"" + rdWindowTrans.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);


            if (rCboConcepto.SelectedValue != "")
            {

                rCboConcepto.CssClass = "cssTxtInvalid";
                rCboConcepto.BorderWidth = Unit.Pixel(1);
                rCboConcepto.BorderColor = System.Drawing.Color.Transparent;

                Session[("hdfBtnAccionDetXP")] = "1";
                Session["cptoId"] = rCboConcepto.SelectedValue;

                //Transacciones_DatosDetalle();

                string script = "function f(){$find(\"" + rdWindowTrans.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

            }
            else
            {
                rCboConcepto.CssClass = "cssTxtInvalid";
                rCboConcepto.BorderWidth = Unit.Pixel(1);
                rCboConcepto.BorderColor = System.Drawing.Color.Red;
                rCboConcepto.Focus();
            }


        }
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //VisualizaModal();

            var dataItem = rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;
            string stransDetId = dataItem.GetDataKeyValue("transDetId").ToString();
            Session["dataItemSessionXP"] = stransDetId;


            string script = "function f(){$find(\"" + rdWindowTrans.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            Eliminar();
        }

    }

    public void Eliminar()
    {

        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsTransDetXPSession"];

        string stransDetId = "";
        var dataItem = rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //se le asigna el id del row seleccionado del grid
            stransDetId = dataItem.GetDataKeyValue("transDetId").ToString();
        }

        dsTransDet.Tables[0].AcceptChanges();


        //for (int i = 0; i < dsTransDet.Tables[0].Rows.Count; i++)
        for (int i = 0; i < rGdvOperacionesTrans.Items.Count; i++)
        {
            if (rGdvOperacionesTrans.Items[i].Selected == true)
            {
                //DataRow dr = dsTransDet.Tables[0].Rows[i];
                //dsTransDet.Tables[0].Rows.Remove(dr);
                dsTransDet.Tables[0].Rows[i].Delete();

            }

        }
        dsTransDet.Tables[0].AcceptChanges();


        Session["dsTransDetXPSession"] = dsTransDet;
        FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, (DataSet)Session["dsTransDetXPSession"]);


        hdfBtnAccionDet.Value = "";
        ControlesAccion();
    }

    private void setFecMovToGrid()
    {
        if (ObtenerFechaHead() == "1/01/01")
        {
            //dr["movFec_Mov"] = DBNull.Value;
        }
        else
        {

            DataSet dsTransMov = new DataSet();
            dsTransMov = (DataSet)Session["dsTransDetXPSession"];

            if (dsTransMov != null)
            {
                if (dsTransMov.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow drConfCpto in dsTransMov.Tables[0].Rows)
                    {

                        drConfCpto[getColumn("cptoConfId_Fec_Mov")] = RdDateFecha.SelectedDate;
                    }
                }
                dsTransMov = (DataSet)Session["dsTransDetXPSession"];
                FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, (DataSet)Session["dsTransDetXPSession"]);
                rGdvOperacionesTrans.DataBind();
            }

        }

    }

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
        string Folio = ObtenFolio();

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_TransaccionesEncabezado";

            if (hdfPag_sOpe.Value != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transid", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(hdfPag_sOpe.Value));
            }
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@transFolio", DbType.String, 10, ParameterDirection.Input, rTxtFolio.Text);
            ProcBD.AgregarParametrosProcedimiento("@transFec", DbType.String, 100, ParameterDirection.Input, Val_TransFec);
            ProcBD.AgregarParametrosProcedimiento("@transDes", DbType.String, 100, ParameterDirection.Input, rTxtDescripcion.Text.Trim());
            //ProcBD.AgregarParametrosProcedimiento("@transPolCve", DbType.String, 10, ParameterDirection.Input, this.rTxtAsientoCont.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@maUsuCveReg", DbType.String, 20, ParameterDirection.Input, maUser);
            ProcBD.AgregarParametrosProcedimiento("@transFecReg", DbType.String, 100, ParameterDirection.Input, Val_TransFec);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                //GRET, se agrega variable para cheque
                string sEjecEstatus, sEjectransId, sEjecMSG = "", sPagXPNum="";
                //
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                if (Session["Valor_btn"].ToString() == "1")
                {
                    sEjectransId = ds.Tables[0].Rows[0]["transId"].ToString();
                }
                else if (Session["Valor_btn"].ToString() == "2")
                {
                    sEjectransId = hdfPag_sOpe.Value;
                    sPagXPNum = hdfPag_PagXPNum.Value;
                }
                else
                {
                    sEjectransId = "";
                }

                if (sEjecEstatus == "1")
                {

                    if (rBtnRegTransaccion.Checked == true)
                    {
                        GuardaTransaccionDetalle(sEjectransId, rTxtFolio.Text, sPagXPNum);
                        GuardaMovimientos(sEjectransId);
                        EliminaAsiContDeOpeyAdmon(sEjectransId);
                    }
                    else if (rBtnRegMovimiento.Checked == true)
                    {
                        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                        {
                            sEjectransId = ds.Tables[0].Rows[0]["transId"].ToString();
                            hdfPag_sOpe.Value = ds.Tables[0].Rows[0]["transId"].ToString();
                            GuardaMovimientos_Captura(sEjectransId,"");

                        }
                        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                        {
                            GuardaMovimientos_Captura(hdfPag_sOpe.Value,hdfPag_PagXPNum.Value);
                            EliminaAsiContDeOpeyAdmon(hdfPag_sOpe.Value);
                        }

                        //ShowAlert(sEjecEstatus, sEjecMSG);

                    }



                }
            }
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

    private void GuardaMovimientos(string transId)
    {

        try
        {
            string maUser = LM.sValSess(this.Page, 1);
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXMovimientos";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@transFolio", DbType.String, 10, ParameterDirection.Input, this.rTxtFolio.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, transId);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        }
        catch (Exception ex)
        {
        }
    }

    private void GuardaTransaccionDetalle(string transId, string Folio, string pagXPNum)
    {
        int ValEliminar = 0;
        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsTransDetXPSession"];

        DataSet ds = new DataSet();
        if (dsTransDet.Tables[0].Rows.Count == 0)
        {
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_TransaccionesDetalle";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(transId));
            //ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, 1);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        }

        foreach (DataRow drConfCpto in dsTransDet.Tables[0].Rows)
        {
            ValEliminar += 1;

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_TransaccionesDetalle";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["ciaCve"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetId"]));
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(transId));
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["cptoId"]));
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(drConfCpto["monCve"]));
            ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, ValEliminar);
            ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetSec"]));

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetSit", DbType.String, 3, ParameterDirection.Input, "R");
            }
            else
            {
                if (Convert.ToString(drConfCpto["transDetSit"]) == "")
                {
                    ProcBD.AgregarParametrosProcedimiento("@transDetSit", DbType.String, 3, ParameterDirection.Input, "R");
                }
                else
                {
                    ProcBD.AgregarParametrosProcedimiento("@transDetSit", DbType.String, 3, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetSit"]));
                }
            }


            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_01", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr10_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_02", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr10_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr10_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr10_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_05", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr10_05"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_06", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr10_06"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_07", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr10_07"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_08", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr10_08"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_09", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr10_09"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr10_10", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr10_10"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_01", DbType.String, 20, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr20_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_02", DbType.String, 20, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr20_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_03", DbType.String, 20, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr20_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_04", DbType.String, 20, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr20_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr20_05", DbType.String, 20, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr20_05"]));

            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_01", DbType.String, 40, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr40_01"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_02", DbType.String, 40, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr40_02"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_03", DbType.String, 40, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr40_03"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_04", DbType.String, 40, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr40_04"]));
            ProcBD.AgregarParametrosProcedimiento("@transDetStr40_05", DbType.String, 40, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetStr40_05"]));


            if (drConfCpto["transDetImp_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetImp_01"]));
            }
            if (drConfCpto["transDetImp_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetImp_02"]));
            }
            if (drConfCpto["transDetImp_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetImp_03"]));
            }
            if (drConfCpto["transDetImp_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetImp_04"]));
            }
            if (drConfCpto["transDetImp_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetImp_05"]));
            }

            if (drConfCpto["transDetImp_06"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_06", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetImp_06"]));
            }
            if (drConfCpto["transDetImp_07"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_07", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetImp_07"]));
            }
            if (drConfCpto["transDetImp_08"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_08", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetImp_08"]));
            }
            if (drConfCpto["transDetImp_09"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_09", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetImp_09"]));
            }
            if (drConfCpto["transDetImp_10"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetImp_10", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetImp_10"]));
            }

            //FECHAS
            if (drConfCpto["transDetFec_01"].ToString() != "")
            {
                DateTime date1 = new DateTime();
                string DetFec_01;
                date1 = Convert.ToDateTime(drConfCpto["transDetFec_01"]);
                DetFec_01 = date1.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_01", DbType.String, 100, ParameterDirection.Input, DetFec_01.Substring(0, 10));

            }

            if (drConfCpto["transDetFec_02"].ToString() != "")
            {
                DateTime date2 = new DateTime();
                string DetFec_02;
                date2 = Convert.ToDateTime(drConfCpto["transDetFec_02"]);
                DetFec_02 = date2.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_02", DbType.String, 100, ParameterDirection.Input, DetFec_02.Substring(0, 10));
            }

            if (drConfCpto["transDetFec_03"].ToString() != "")
            {
                DateTime date3 = new DateTime();
                string DetFec_03;
                date3 = Convert.ToDateTime(drConfCpto["transDetFec_03"]);
                DetFec_03 = date3.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_03", DbType.String, 100, ParameterDirection.Input, DetFec_03.Substring(0, 10));
            }

            if (drConfCpto["transDetFec_04"].ToString() != "")
            {
                DateTime date4 = new DateTime();
                string DetFec_04;
                date4 = Convert.ToDateTime(drConfCpto["transDetFec_04"]);
                DetFec_04 = date4.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_04", DbType.String, 100, ParameterDirection.Input, DetFec_04.Substring(0, 10));
            }

            if (drConfCpto["transDetFec_05"].ToString() != "")
            {
                DateTime date5 = new DateTime();
                string DetFec_05;
                date5 = Convert.ToDateTime(drConfCpto["transDetFec_05"]);
                DetFec_05 = date5.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@transDetFec_05", DbType.String, 100, ParameterDirection.Input, DetFec_05.Substring(0, 10));
            }

            if (drConfCpto["transDetFact_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_01", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetFact_01"]));
            }
            if (drConfCpto["transDetFact_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_02", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetFact_02"]));
            }
            if (drConfCpto["transDetFact_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_03", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetFact_03"]));
            }
            if (drConfCpto["transDetFact_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_04", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetFact_04"]));
            }
            if (drConfCpto["transDetFact_05"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetFact_05", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["transDetFact_05"]));
            }


            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        }
        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            if (sEjecEstatus.ToString() == "1")
            {
                PagosXp(Folio, transId,pagXPNum);
            }
        }

    }
    
    public void PagosXp(string Folio, string prmTranId, string pagXPNum)
    {
        try
        {
            string fchPrmSp;

            fchPrmSp = "";
            fchPrmSp += RdDateFecha.SelectedDate.Value.Year.ToString();
            fchPrmSp += "-";
            fchPrmSp += RdDateFecha.SelectedDate.Value.Month.ToString().PadLeft(2, '0');
            fchPrmSp += "-";
            fchPrmSp += RdDateFecha.SelectedDate.Value.Day.ToString().PadLeft(2, '0');
            fchPrmSp += " 00:00:00";

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PagosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            if (hdfBtnAccion.Value == "2")
            {
                ProcBD.AgregarParametrosProcedimiento("@pagXPId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(Pag_sIdDocReg));
            }

            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFolio", DbType.String, 10, ParameterDirection.Input, Folio);
            ProcBD.AgregarParametrosProcedimiento("@ctaDepCve", DbType.String, 10, ParameterDirection.Input, rCboCuenta.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFec", DbType.String, 100, ParameterDirection.Input, fchPrmSp);
            if (rCboBeneficiario.SelectedIndex != -1) {
                ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 10, ParameterDirection.Input, rCboBeneficiario.SelectedValue);
            }


            ProcBD.AgregarParametrosProcedimiento("@pagXPBenef", DbType.String, 50, ParameterDirection.Input, rTxtBeneficiario.Text);
            ProcBD.AgregarParametrosProcedimiento("@pagXPDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text);
            ProcBD.AgregarParametrosProcedimiento("@pagXPNum", DbType.String, 10, ParameterDirection.Input, pagXPNum);
            ProcBD.AgregarParametrosProcedimiento("@pagXPImp", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(rTxtImporte.Text));
            ProcBD.AgregarParametrosProcedimiento("@pagXPSitEnt", DbType.String, 10, ParameterDirection.Input, "");
            ProcBD.AgregarParametrosProcedimiento("@pagXPSit", DbType.String, 10, ParameterDirection.Input, "R");


            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(prmTranId));
            //ProcBD.AgregarParametrosProcedimiento("@pagXPFecReg", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjectransId, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                if (sEjecEstatus == "1")
                {

                    if (hdfBtnAccion.Value == "1")
                    {
                        Session["Valor_DocCve"] = ds.Tables[0].Rows[0]["pagXPId"].ToString();
                        Session["Valor_btn"] = "2";
                        hdfPag_sOpe.Value = prmTranId;
                    }
                    else if (hdfBtnAccion.Value == "2")
                    {
                        Session["Valor_DocCve"] = Pag_sIdDocReg;
                        hdfPag_sOpe.Value = prmTranId;
                    }
                    else
                    {
                        Session["Valor_DocCve"] = "";
                        Session["Valor_btn"] = "1";
                    }


                    ShowAlert(sEjecEstatus, sEjecMSG);
                    InicioPagina();
                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
    
    private void getInvisibleGrid()
    {

        foreach (GridColumn column in rGdvOperacionesTrans.Columns)
        {
            column.Display = false;
        }

    }

    public void getFolios()
    {

        string tipFolio = "", valFolio = "", tipFolioAsi = "", valFolioAsi = "", Response = "";

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count > 0)
        {
            tipFolio = ds.Tables[0].Rows[0]["cptoDefFolTip"].ToString();
            valFolio = ds.Tables[0].Rows[0]["cptoDefFolVal"].ToString();

            tipFolioAsi = ds.Tables[0].Rows[0]["cptoDefAsiConFolTip"].ToString();
            valFolioAsi = ds.Tables[0].Rows[0]["cptoDefAsiConFolVal"].ToString();

            rTxtFolio.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, valFolio, Convert.ToInt32(tipFolio), "");
            //rTxtAsientoCont.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, valFolioAsi, Convert.ToInt32(tipFolio), "");

            if (tipFolio == "1")
            {
                rTxtFolio.Enabled = true;
            }
            else if (tipFolio == "2")
            {
                rTxtFolio.Enabled = false;
            }

            if (tipFolioAsi == "1")
            {
                rTxtAsientoCont.Enabled = true;
            }
            else if (tipFolioAsi == "2")
            {
                rTxtAsientoCont.Enabled = false;
            }

        }


    }
    
    private void GuardaMovimientos_Captura(string transId,string pagXpNum)
    {


        int ValEliminar = 0;
        DataSet dsTransMov = new DataSet();
        dsTransMov = (DataSet)Session["dsMovDetSession"];

        DataSet ds = new DataSet();
        if (dsTransMov.Tables[0].Rows.Count == 0)
        {
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Movimientos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(transId));
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, hdf_transDetSec.Value);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        }


        foreach (DataRow drConfCpto in dsTransMov.Tables[0].Rows)
        {
            ValEliminar += 1;
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Movimientos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(transId));
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, ValEliminar);
            ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, hdf_transDetSec.Value);


            //Tipo de Aplicacion
            if (drConfCpto["movTipApli"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movMapSecApli", DbType.Int16, 10, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movTipApli"]));
            }

            if (drConfCpto["movTipApli"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movTipApli", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movTipApli"]));
            }



            if (drConfCpto["movFolio"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movFolio", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movFolio"]));
            }

            if (drConfCpto["movSec"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movSec", DbType.Int64, 10, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movSec"]));
            }

            if (drConfCpto["monCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(drConfCpto["monCve"]));
            }

            if (drConfCpto["movCoA"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movCoA", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movCoA"]));
            }



            ProcBD.AgregarParametrosProcedimiento("@movSit", DbType.String, 3, ParameterDirection.Input, "R");

            if (drConfCpto["movRef10_CodApli"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_CodApli", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef10_CodApli"]));
            }

            if (drConfCpto["movRef10_Princ"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_Princ", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef10_Princ"]));
            }

            if (drConfCpto["movRef10_Apli"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_Apli", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef10_Apli"]));
            }


            if (drConfCpto["movRef10_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef10_03"]));
            }

            if (drConfCpto["movRef10_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef10_04"]));
            }

            if (drConfCpto["movRef40_Des"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef40_Des", DbType.String, 40, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef40_Des"]));
            }


            if (drConfCpto["movImp_Imp"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movImp_Imp", DbType.Decimal, 10, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["movImp_Imp"]));
            }

            if (drConfCpto["movFac_TipCam"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movFac_TipCam", DbType.Decimal, 10, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["movFac_TipCam"]));
            }



            if (drConfCpto["movOrdComp"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movOrdComp", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movOrdComp"]));
            }

            if (drConfCpto["movProv"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movProv", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movProv"]));
            }

            if (drConfCpto["movApliAntGast"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movApliAntGast", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movApliAntGast"]));
            }

            if (drConfCpto["movContRecCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movContRecCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movContRecCve"]));
            }

            if (drConfCpto["movContFec"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movContFec", DbType.String, 100, ParameterDirection.Input, Convert.ToString(drConfCpto["movContFec"]));
            }

            if (drConfCpto["movDatClasif"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movDatClasif", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movDatClasif"]));
            }

            if (drConfCpto["movPolCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movPolCve", DbType.String, 40, ParameterDirection.Input, Convert.ToString(drConfCpto["movPolCve"]));
            }


            //FECHAS
            if (drConfCpto["movFec_Mov"].ToString() != "")
            {
                DateTime date1 = new DateTime();
                string DetFec_01;
                date1 = Convert.ToDateTime(drConfCpto["movFec_Mov"]);
                DetFec_01 = date1.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@movFec_Mov", DbType.String, 100, ParameterDirection.Input, DetFec_01.Substring(0, 10));

            }

            if (drConfCpto["movFec_Venc"].ToString() != "")
            {
                DateTime date2 = new DateTime();
                string DetFec_02;
                date2 = Convert.ToDateTime(drConfCpto["movFec_Venc"]);
                DetFec_02 = date2.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@movFec_Venc", DbType.String, 100, ParameterDirection.Input, DetFec_02.Substring(0, 10));
            }

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            
        }

        if (FnValAdoNet.bDSIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sEjecEstatus.ToString() == "1")
            {
                PagosXp(rTxtFolio.Text, transId,pagXpNum);
            }

        }

    }
    private void EliminaAsiContDeOpeyAdmon(string transId)
    {
        try
        {
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_EXPRO_EliminaAsiContDeOpeyAdmon";
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(transId));
            ProcBD.AgregarParametrosProcedimiento("@tipoElim", DbType.String, 5, ParameterDirection.Input, "ADMON");
            ProcBD.AgregarParametrosProcedimiento("@Modulo", DbType.String, 5, ParameterDirection.Input, "XP");
            oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        }
        catch (Exception ex)
        {
            string MsgError;
            MsgError = ex.ToString();
        }

    }

    #endregion

    #region FUNCIONES

    private DataSet ConceptoConfiguracion()
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

    private void llenaComboProv(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttProvDatosGenerales";

        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "provCve", "provNom", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
    }

    public bool RabComboBox_ConceptoReferenciaTipo_SegUsuario(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 102);
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

    public string ObtenerFechaHead()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha.SelectedDate);
        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
    }

    private string getColumn(string cptoConfRef)
    {

        string response = "";

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MovimientosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 10, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@refColumnMov", DbType.String, 100, ParameterDirection.Input, cptoConfRef);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        response = ds.Tables[0].Rows[0]["columnName"].ToString();
                    }
                    else
                    {
                        response = "";
                    }

                }
                else
                {
                    response = "";
                }
            }
            else
            {
                response = "";
            }


        }
        catch (Exception ex)
        {
            response = "";

        }

        return response;

    }

    public string ObtenFolio()
    {
        string tipFolio = "", valFolio = "", Response = "";

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds.Tables[0].Rows.Count > 0)
        {
            tipFolio = ds.Tables[0].Rows[0]["cptoDefFolTip"].ToString();
            valFolio = ds.Tables[0].Rows[0]["cptoDefFolVal"].ToString();

            Response = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, valFolio, Convert.ToInt32(tipFolio), "");
        }

        return Response;

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

            if (rCboConcepto.SelectedValue == "")
            {
                rCboConcepto.CssClass = "cssTxtInvalid";
                rCboConcepto.BorderWidth = Unit.Pixel(1);
                rCboConcepto.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboConcepto.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboCuenta.SelectedValue == "")
            {
                rCboCuenta.CssClass = "cssTxtInvalid";
                rCboCuenta.BorderWidth = Unit.Pixel(1);
                rCboCuenta.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboCuenta.BorderColor = System.Drawing.Color.Transparent; }

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

            if (rTxtBeneficiario.Text.Trim() == "")
            {
                rTxtBeneficiario.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtBeneficiario.CssClass = "cssTxtEnabled";
            }


            if (rTxtImporte.Text == "")
            {
                rTxtImporte.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else
            {
                rTxtImporte.CssClass = "cssTxtEnabled";
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

    private string validaEjecutaAccionDetalle(ref string sMSGTip)
    {
        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //Modificar
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
            hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rCboConcepto.SelectedValue == "")
            {
                rCboConcepto.CssClass = "cssTxtInvalid";
                rCboConcepto.BorderWidth = Unit.Pixel(1);
                rCboConcepto.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboConcepto.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboCuenta.SelectedValue == "")
            {
                rCboCuenta.CssClass = "cssTxtInvalid";
                rCboCuenta.BorderWidth = Unit.Pixel(1);
                rCboCuenta.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboCuenta.BorderColor = System.Drawing.Color.Transparent; }


            if (rCboBeneficiario.SelectedValue == "")
            {
                rCboBeneficiario.CssClass = "cssTxtInvalid";
                rCboBeneficiario.BorderWidth = Unit.Pixel(1);
                rCboBeneficiario.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboBeneficiario.BorderColor = System.Drawing.Color.Transparent; }

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


            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {

                if (rBtnRegTransaccion.Checked)
                {
                    if (rGdvOperacionesTrans.SelectedItems.Count == 0)
                    {
                        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                        return sResult;
                    }
                }
                else
                {
                    if (rGdvOperacionesMovCC.SelectedItems.Count == 0)
                    {
                        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                        return sResult;
                    }
                }


            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }

            return sResult;
        }


        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {


            if (rBtnRegTransaccion.Checked)
            {
                if (rGdvOperacionesTrans.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }
            }
            else
            {
                if (rGdvOperacionesMovCC.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }
            }

        }




        return sResult;
    }

    #endregion



    //TRANSACCIONES
    private void ControlesAccionTrans()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        //rTxtCve.CssClass = "cssTxtEnabled";
        //rTxtDes.CssClass = "cssTxtEnabled";
        //rTxtAbr.CssClass = "cssTxtEnabled";

        //this.rTxtCve.Enabled = false;
        //this.rTxtDes.Enabled = false;
        //this.rTxtAbr.Enabled = false;

        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;


        /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
        //Validacion
        msgValidacion = ValidaControlesAccion_SelectRowGridTrans(ref sMSGTip);
        if (msgValidacion == "")
        {
            ControlesAccionEjecucionTrans(true);
        }
        else
        {
            ControlesAccionEjecucionTrans(false);
            ShowAlert(sMSGTip, msgValidacion);
        }

        //INICIO / CANCELAR
        if (hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            //this.rTxtCve.Enabled = false;
            //this.rTxtDes.Enabled = false;
            //this.rTxtAbr.Enabled = false;
            //this.rTxtCve.Text = "";
            //this.rTxtDes.Text = "";
            //this.rTxtAbr.Text = "";
        }
    }

    private void ControlesAccionEjecucionTrans(bool Result)
    {
        if (Result == true)
        {

            DataSet dsTransDet = new DataSet();
            dsTransDet = (DataSet)Session["dsTransDetXPSession"];

            Session[("hdfBtnAccionDetXP")] = hdfBtnAccionDet.Value;

            Session["cptoId"] = rCboConcepto.SelectedValue;


            Session["refPrincXP"] = rCboBeneficiario.SelectedValue;

            Session["fchMovXP"] = ObtenerFecha();


            //NUEVO
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {

                rGdvOperacionesTrans.MasterTableView.ClearSelectedItems();



                if (rCboConcepto.SelectedValue != "")
                {

                    rCboConcepto.CssClass = "cssTxtInvalid";
                    rCboConcepto.BorderWidth = Unit.Pixel(1);
                    rCboConcepto.BorderColor = System.Drawing.Color.Transparent;

                    Session[("hdfBtnAccionDetXP")] = "1";
                    Session["cptoId"] = rCboConcepto.SelectedValue;

                    //Transacciones_DatosDetalle();

                    string script = "function f(){$find(\"" + rdWindowTrans.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                }
                else
                {
                    rCboConcepto.CssClass = "cssTxtInvalid";
                    rCboConcepto.BorderWidth = Unit.Pixel(1);
                    rCboConcepto.BorderColor = System.Drawing.Color.Red;
                    rCboConcepto.Focus();
                }


                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvOperacionesTrans.AllowMultiRowSelection = false;


                var dataItem = rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem.GetDataKeyValue("transDetId").ToString();
                Session["dataItemSessionXP"] = stransDetId;


                string script = "function f(){$find(\"" + rdWindowTrans.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);


                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                Eliminar();
            }

            //LIMPIAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvOperacionesTrans.AllowMultiRowSelection = true;
                rGdvOperacionesTrans.MasterTableView.ClearSelectedItems();

                //this.rTxtCve.Enabled = false;
                //this.rTxtDes.Enabled = false;
                //this.rTxtAbr.Enabled = false;
                //this.rTxtCve.Text = "";
                //this.rTxtDes.Text = "";
                //this.rTxtAbr.Text = "";
            }
        }


        if (Result == false)
        {
            //this.rTxtCve.Enabled = false;
            //this.rTxtDes.Enabled = false;
            //this.rTxtAbr.Enabled = false;
            //this.rTxtCve.Text = "";
            //this.rTxtDes.Text = "";
            //this.rTxtAbr.Text = "";
            hdfBtnAccionDet.Value = "";
        }


    }

    private string ValidaControlesAccion_SelectRowGridTrans(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvOperacionesTrans.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperacionesTrans, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperacionesTrans, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }







    //MOVIMIENTOS

    private void ControlesAccionMov()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        //rTxtCve.CssClass = "cssTxtEnabled";
        //rTxtDes.CssClass = "cssTxtEnabled";
        //rTxtAbr.CssClass = "cssTxtEnabled";

        //this.rTxtCve.Enabled = false;
        //this.rTxtDes.Enabled = false;
        //this.rTxtAbr.Enabled = false;

        rBtnGuardar.Enabled = true;
        rBtnCancelar.Enabled = true;


        /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
        //Validacion
        msgValidacion = ValidaControlesAccion_SelectRowGridMov(ref sMSGTip);
        if (msgValidacion == "")
        {
            ControlesAccionEjecucionMov(true);
        }
        else
        {
            ControlesAccionEjecucionMov(false);
            ShowAlert(sMSGTip, msgValidacion);
        }

        //INICIO / CANCELAR
        if (hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            //this.rTxtCve.Enabled = false;
            //this.rTxtDes.Enabled = false;
            //this.rTxtAbr.Enabled = false;
            //this.rTxtCve.Text = "";
            //this.rTxtDes.Text = "";
            //this.rTxtAbr.Text = "";
        }
    }

    private void ControlesAccionEjecucionMov(bool Result)
    {
        if (Result == true)
        {

            //DataSet dsTransDet = new DataSet();
            //dsTransDet = (DataSet)Session["dsTransDetCCSession"];

            //Session[("hdfBtnAccionDetCC")] = hdfBtnAccionDet.Value;

            //Session["cptoId"] = rCboConcepto.SelectedValue;


            //Session["refPrincCC"] = rCboBeneficiario.SelectedValue;

            //Session["fchMovCC"] = ObtenerFecha();

            if (rTxtFolio.Text != "")
            {
                Session["FolioSession"] = rTxtFolio.Text.ToString();
            }




            //NUEVO
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                Session[("hdfBtnAccionMov")] = hdfBtnAccionDet.Value;
                string script = "function f(){$find(\"" + rdWindowMov.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                //Opcion para mostrar la ventana
                Session[("hdfBtnAccionMov")] = hdfBtnAccionDet.Value;

                var dataItem = rGdvOperacionesMovCC.SelectedItems[0] as GridDataItem;
                string sMovDetId = dataItem.GetDataKeyValue("movID").ToString();
                Session["movIDSession"] = sMovDetId;

                string script = "function f(){$find(\"" + rdWindowMov.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);


                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                DataSet dsTransMov = new DataSet();
                dsTransMov = (DataSet)Session["dsMovDetSession"];

                EliminaMov();
                dsTransMov.Tables[0].AcceptChanges();

                Session["dsMovDetSession"] = dsTransMov;

                FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, (DataSet)Session["dsMovDetSession"]);
                rGdvOperacionesMovCC.DataBind();
                hdfBtnAccionDet.Value = "";

                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";
            }

            //LIMPIAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvOperacionesMovCC.AllowMultiRowSelection = true;
                rGdvOperacionesMovCC.MasterTableView.ClearSelectedItems();

                //this.rTxtCve.Enabled = false;
                //this.rTxtDes.Enabled = false;
                //this.rTxtAbr.Enabled = false;
                //this.rTxtCve.Text = "";
                //this.rTxtDes.Text = "";
                //this.rTxtAbr.Text = "";
            }
        }


        if (Result == false)
        {
            //this.rTxtCve.Enabled = false;
            //this.rTxtDes.Enabled = false;
            //this.rTxtAbr.Enabled = false;
            //this.rTxtCve.Text = "";
            //this.rTxtDes.Text = "";
            //this.rTxtAbr.Text = "";
            hdfBtnAccionDet.Value = "";
        }


    }

    private string ValidaControlesAccion_SelectRowGridMov(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvOperacionesMovCC.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperacionesMovCC, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperacionesMovCC, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }
}