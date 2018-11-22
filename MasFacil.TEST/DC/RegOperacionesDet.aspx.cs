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


public partial class DC_RegOperacionesDet : System.Web.UI.Page
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
    MGMFnGrales.FNPeriodosCalendario FNPeriodo = new MGMFnGrales.FNPeriodosCalendario();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_cptoTip;
    private string TipoCpto;
    private string Pag_RawUrl_Return;

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
    protected void rGdvOperacionesTrans_SelectedIndexChanged(object sender, EventArgs e)
    {
     
            var dataItem = this.rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                hdf_transDetSec.Value = dataItem["transDetSec"].Text;
            }
        
      

    }
    protected void RdDateFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //FechaValidaPeriodo();

        DataSet ds = new DataSet();
        ds = FNPeriodo.dsValidaPeriodoFecha(Pag_sConexionLog, Pag_sCompania, Convert.ToDateTime(RdDateFecha.SelectedDate));
        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

            if (sEjecEstatus != "1")
            {
                ShowAlert(sEjecEstatus, sEjecMSG);
                RdDateFecha.Clear();
            }
        }
        else {
            RdDateFecha.Clear();
        }

    }
    protected void rBtnRegTransaccion_CheckedChanged(object sender, EventArgs e)
    {
        CheckRegistro();
    }
    protected void rBtnRegMovimiento_CheckedChanged(object sender, EventArgs e)
    {

        if (hdfPag_sOpe.Value !="" && rGdvOperacionesTrans.Items.Count > 0)
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
            else {
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
        ControlesAccionInicio();
        CheckRegistro();

        Transacciones_VizualizaColumGrid();

        rTxtFolio.Text = "";
        FolioAut_Trans();
    }
    protected void rTxtFolio_TextChanged(object sender, EventArgs e)
    {

        FolioAut_Trans();

        rTxtFolio.DataBind();
        Session["FolioSession"] = rTxtFolio.Text;

        if (rTxtFolio.Text != "")
        {
            ActualizaFolioDataset();
        }

    }
    protected void rTxtAsientoCont_TextChanged(object sender, EventArgs e)
    {

    }


    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnNuevoDet_Click(object sender, ImageButtonClickEventArgs e)
    {

        if (this.rCboConcepto.SelectedIndex != -1)
        {
            hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
            ControlesAccion();
        }
        else {
            ShowAlert("2","Para ejecutar la acción debe seleccionar el concepto.");
        }
 
    }
    protected void rBtnModificarDet_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }
    protected void rBtnEliminarDet_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = "";
        ControlesAccion();
    }
    protected void rBtnGuardarDet_Click(object sender, ImageButtonClickEventArgs e)
    {
        RecuperarValoresDeDataList();
        hdfBtnAccionDet.Value = "";
        ControlesAccion();
    }
    protected void rBtnCancelarDet_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = "";
        ControlesAccion();
    }
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (Pag_cptoTip == "0")
        {
            //Response.Redirect("~/DC/RegOperaciones.aspx");
            Response.Redirect(Pag_RawUrl_Return);
        }
        else
        {
            Response.Redirect(Pag_RawUrl_Return);
            //Response.Redirect("~/DC/RegOperaciones.aspx" + "?cptoTip=" + Pag_cptoTip);
        }

    }
    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {

        if (rBtnRegTransaccion.Checked == true)
        {
            //DataSet dsTransDet = new DataSet();
            //dsTransDet = (DataSet)Session["dsTransDetSession"];
            //rGdvOperacionesTrans.DataBind();
            FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, (DataSet)Session["dsTransDetSession"]);
        }

        if (rBtnRegMovimiento.Checked == true)
        {
            //DataSet dsTransMov = new DataSet();
            //dsTransMov = (DataSet)Session["dsMovDetSession"];
            FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, (DataSet)Session["dsMovDetSession"]);
            rGdvOperacionesMovCC.DataBind();
        }

        hdfBtnAccionDet.Value = "";
        rBtnNuevoDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificarDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminarDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

    }

    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_RawUrl_Return =   Convert.ToString(Session["RawUrl_Return"]);

        hdfBtnAccion.Value = "";
        if (Request.QueryString["Acc"] != null && Request.QueryString["Acc"] != "")
        {
            hdfBtnAccion.Value = Request.QueryString["Acc"];
        }

        if (Request.QueryString["cptoTip"] != null && Request.QueryString["cptoTip"] != "")
        {
            Pag_cptoTip = Request.QueryString["cptoTip"];
        }
        else
        {
            Pag_cptoTip = "0";
        }
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
 
        TituloPagina();
        ControlesAccionInicio();
        llenaComboCptos();

        CheckRegistro();
    }

    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Operaciones", "PnlMPFormTituloApartado");
    }
    private void ControlesAccionInicio() {

        Session["dsTransDetSession"] = null;
        if (hdfBtnAccion.Value == "")
        {
            rCboConcepto.Enabled = false;
            RdDateFecha.Enabled = false;
            rTxtDesc.Enabled = false;
            rBtnRegTransaccion.Enabled = true;
            rBtnRegMovimiento.Enabled = true;

            rTxtFolio.Enabled = false;
            rTxtAsientoCont.Enabled = false;
            rCboLibro.Enabled = false;
            pnlBtnsAcciones.Visible = false;
            rBtnGuardar.Visible = false;
        }
        else {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString()) {
                rCboConcepto.Enabled = true;
                RdDateFecha.Enabled = true;
                rTxtDesc.Enabled = true;
                rBtnRegTransaccion.Enabled = true;
                rBtnRegMovimiento.Enabled = true;

                rTxtFolio.Enabled = false;
                rTxtAsientoCont.Enabled = false;
                rCboLibro.Enabled = true;
                pnlBtnsAcciones.Visible = false;
                if (rBtnRegTransaccion.Checked==true)
                {
                    pnlBtnsAcciones.Visible = true;
                } 
                rBtnGuardar.Visible = true;
            
            }



            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rCboConcepto.Enabled = false;
                RdDateFecha.Enabled = true;
                rTxtDesc.Enabled = true;
                rBtnRegTransaccion.Enabled = true;
                rBtnRegMovimiento.Enabled = true;

                rTxtFolio.Enabled = false;
                rTxtAsientoCont.Enabled = false;
                rCboLibro.Enabled = true;
                pnlBtnsAcciones.Visible = false;
                if (rBtnRegTransaccion.Checked == true)
                {
                    pnlBtnsAcciones.Enabled = true;
                }
                rBtnGuardar.Visible = true;
            }


        }

    }
    private void ControlesAccion() {

        string sMSGTip = "";
        string msgValidacion = "";

        ////===> CONTROLES GENERAL
        rBtnNuevoDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificarDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminarDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";

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
    }
    private void ControlesAccionEjecucion(bool Result)
    {

        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                if (rCboConcepto.Enabled == false)
                {
                    Session["FolioSession"] = rTxtFolio.Text;
                }
                EjecutaAccionDetalle();
            }

            //MODIFICAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                //rBtnModificarDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                //this.rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
                EjecutaAccionDetalle();
            }

            //ELIMINAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                //rBtnEliminarDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
                //this.rGdvOperacionesTrans.ClientSettings.Selecting.AllowRowSelect = true;
                //rGdvOperacionesMovCC.ClientSettings.Selecting.AllowRowSelect = true;
                //rGdvOperacionesMovCC.AllowMultiRowSelection = true;
                //rGdvOperacionesTrans.AllowMultiRowSelection = true;
                EjecutaAccionDetalle();
            }
        }

    }
    private void llenaComboCptos()
    {
        string maUser = LM.sValSess(this.Page, 1);
        string sTipoCptoOpe = Convert.ToString(Session["TipoCptoOpe"]);

        if (hdfBtnAccion.Value == "")
        {
            if (Pag_cptoTip != "0")
            {
                FnCtlsFillIn.RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, 1, maUser, sTipoCptoOpe, ref rCboConcepto, true, false, "", Convert.ToInt32(Pag_cptoTip));
            }
            else {
                FnCtlsFillIn.RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, 1, maUser, sTipoCptoOpe, ref rCboConcepto, true, false );
            }
                
        }
        else
        {
            if (Pag_cptoTip != "0")
            {
                FnCtlsFillIn.RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, 2, maUser, sTipoCptoOpe, ref rCboConcepto, true, false,"", Convert.ToInt32(Pag_cptoTip));
            }
            else {
                FnCtlsFillIn.RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, 2, maUser, sTipoCptoOpe, ref rCboConcepto, true, false);
            }
                
        }

    }
    private void FolioAut_Trans() {

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

                if (rTxtFolio.Text.Trim() != "") {
                    rTxtFolio.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, cptoDefFolVal, Convert.ToInt32(cptoDefFolTip), rTxtFolio.Text.Trim() );
                }
                rTxtFolio.Enabled = true;
            }



        } else {
            rTxtFolio.Text = "";
            rTxtFolio.Enabled = false;
        }

    }
    private void CheckRegistro() {

        string sValue = Convert.ToString(Session["TipoCptoOpe"]);
        if (sValue != "")
        {

            if (sValue == "CG")
            {
                lblTipoReg.Visible = false;
                rBtnRegTransaccion.Visible = false;
                rBtnRegMovimiento.Visible = false;
            }
            else {
                lblTipoReg.Visible = true;
                rBtnRegTransaccion.Visible = true;
                rBtnRegMovimiento.Visible = true;
            }
        }



            CheckControlespnlBtnsAcciones();
            Transacciones_DatosGenerales();

        if (rBtnRegTransaccion.Checked == true) 
        {
            DataSet dsTransDet = new DataSet();
            dsTransDet = (DataSet)Session["dsTransDetSession"];

            if (dsTransDet == null)
            {
                Transacciones_DatosDetalle();
            }
            else 
            {
                FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, dsTransDet);
                rGdvOperacionesTrans.Visible = true;
                rGdvOperacionesMovCC.Visible = false;
             }
        }

        if (rBtnRegMovimiento.Checked == true)
        {
          Movimientos_DatosDetalle();
        }
    }



    private void CheckControlespnlBtnsAcciones() {
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
                        if (sValue == "IN")
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
                        if (sValue == "IN")
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

            rTxtDesc.Text = Convert.ToString(ds.Tables[0].Rows[0]["transDes"]);
            RdDateFecha.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["transFec"]);

            rTxtFolio.Text = Convert.ToString(ds.Tables[0].Rows[0]["transFolio"]);
            rTxtAsientoCont.Text = Convert.ToString(ds.Tables[0].Rows[0]["transPolCve"]);
            rCboLibro.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["transLibCont"]);

        }

    }

    private void Transacciones_DatosDetalle()
    {
        string Pag_sOpe = "0";
        if (hdfPag_sOpe.Value != "")
        {
            Pag_sOpe = hdfPag_sOpe.Value;
        }

        DataSet dsTransDet = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@folioId", DbType.Int64, 0, ParameterDirection.Input, Pag_sOpe);
        dsTransDet = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        Session["dsTransDetSession"] = dsTransDet;
        
        FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, dsTransDet);
        Transacciones_VizualizaColumGrid();
        rGdvOperacionesTrans.Visible = true;
        rGdvOperacionesMovCC.Visible = false;
    }

    private void Transacciones_VizualizaColumGrid()
    {

        for (int i = 0; i < rGdvOperacionesTrans.MasterTableView.Columns.Count; i++)
        {
            if (i != 4 && i != 40 && i != 47) {
                rGdvOperacionesTrans.MasterTableView.Columns[i].Display = false;
            }
            
        }
         

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
        FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, (DataSet)Session["dsTransDetSession"]);
    }

    private void Movimientos_DatosDetalle() {
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
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, sCptoId);
        ProcBD.AgregarParametrosProcedimiento("@folioId", DbType.Int64, 0, ParameterDirection.Input, Pag_sOpe);
        ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, hdf_transDetSec.Value);
        
        dsMovDet = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        Session["dsMovDetSession"] = dsMovDet;

        TipoCptoXPCCIN();
        if (TipoCpto == "I")
        {
            if (hdf_AddColumns.Value == "0")
            {
                rGdvOperacionesMovCC.Columns.Clear();
                AgregaColumnasRdGrid();
                hdf_AddColumns.Value = "1";

            }
        }


        FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, dsMovDet);
        rGdvOperacionesTrans.Visible = false;
        rGdvOperacionesMovCC.Visible = true;
    }

    private void ActualizaFolioDataset()
    {
  
        if (rBtnRegMovimiento.Checked == true)
        {
            DataSet dsTransMov = new DataSet();
            dsTransMov = (DataSet)Session["dsMovDetSession"];

            if (dsTransMov != null)
            {
                if (dsTransMov.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow drConfCpto in dsTransMov.Tables[0].Rows)
                    {
                        drConfCpto["movFolio"] = rTxtFolio.Text;
                    }
                }
                dsTransMov = (DataSet)Session["dsMovDetSession"];
                FnCtlsFillIn.RadGrid(ref rGdvOperacionesMovCC, (DataSet)Session["dsMovDetSession"]);
                rGdvOperacionesMovCC.DataBind();
            }
        }

    }

    //private void VisualizaModal() {

    //    DataSet ds = new DataSet();
    //    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //    ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
    //    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 61);
    //    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
    //    ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
    //    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

    //    //DataList1.DataSource = ds;
    //    //DataList1.RepeatColumns = 1;
    //    //DataList1.DataBind();

    //    recorreDatalist(ds);

    //    if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()) {
    //        AsinarValoresADataList(ds);
    //    }


    //    string script = "function f(){$find(\"" + TextCombos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

    //}
    //public void recorreDatalist(DataSet dsConfConcepto)
    //{

    //    foreach (DataRow fileTemp in dsConfConcepto.Tables[0].Rows)
    //    {
    //        string ProgCve = "", Row = "";
    //        int controlador = 1;
    //        ProgCve = fileTemp["cptoConfProgCve"].ToString();
    //        Row = fileTemp["Row"].ToString();

    //        foreach (DataListItem DtalistFila in DataList1.Items)
    //        {
    //            var combo = DtalistFila.FindControl("RadComboBox1") as RadComboBox;
    //            if (ProgCve == "" && controlador.ToString() == Row)
    //            {
    //                combo.Visible = false;
    //            }
    //            //var textbox = DtalistFila.FindControl("cptoCve") as HiddenField;

    //            if (combo.Visible == true && controlador.ToString() == Row)
    //            {

    //                DataSet ds1 = new DataSet();
    //                MAAK.Procedimientos.ProcedimientoAlmacenado serProc1 = new MAAK.Procedimientos.ProcedimientoAlmacenado();
    //                serProc1.NombreProcedimiento = "sp_EjecutaProgramaConsulta";
    //                serProc1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
    //                serProc1.AgregarParametrosProcedimiento("@progCve", DbType.String, 15, ParameterDirection.Input, ProgCve);
    //                ds1 = oWS.ObtenerDatasetDesdeProcedimientoConError(serProc1.ObtenerXmlProcedimiento(), Pag_sConexionLog);


    //                combo.EmptyMessage = "Seleccionar";
    //                combo.DataTextField = "Descripcion";
    //                combo.DataValueField = "Clave";
    //                combo.DataSource = ds1.Tables[0];

    //                ((Literal)combo.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(ds1.Tables[0].Rows.Count);


    //                try
    //                {
    //                    combo.DataBind();
    //                }
    //                catch (Exception ex)
    //                {

    //                    //MessageBox.Show(ex.ToString());
    //                }



    //            }

    //            controlador++;
    //        }



    //    }
    //}
    //private void AsinarValoresADataList(DataSet dsConfConcepto)
    //{
    //    DataSet dsTransDet = new DataSet();

    //    //se asigna a una variable si esta seleccionado algun item del grid
    //    var dataItem = rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;
    //    if (dataItem != null)
    //    {
    //        //se le asigna el id del row seleccionado del grid
    //        string stransDetId = dataItem.GetDataKeyValue("transDetId").ToString();

    //        //recorrer configuracion del cpto del dataset de configuracion
    //        foreach (DataRow drConfCpto in dsConfConcepto.Tables[0].Rows)
    //        {
    //            // se obtiene el nombre de la columna
    //            string snameColumConfcpto = drConfCpto["ColumTrans"].ToString();
    //            string snameProgCve = drConfCpto["cptoConfProgCve"].ToString();

    //            //dataset de la variable de sesion a local
    //            dsTransDet = (DataSet)Session["dsTransDetSession"];
    //            DataRow[] drTransDet;
    //            //selecciona el row del dataset de acuerdo al id seleccionado del grid
    //            drTransDet = dsTransDet.Tables[0].Select("transDetId = " + stransDetId);

    //            //se obtiene el valor de la columna
    //            string valorTransDet = drTransDet[0][snameColumConfcpto].ToString();

    //            int Countkeyarray = 0;
    //            //se recorren los items del datalist
    //            foreach (DataListItem dlConf in DataList1.Items)
    //            {
    //                var ObjTxt = dlConf.FindControl("RadTextBox1") as RadTextBox;
    //                var ObjCombo = dlConf.FindControl("RadComboBox1") as RadComboBox;
    //                //se obtiene el datakeys del datalist-- se obtiene el nombre del control dentro del daalist
    //                string keysDataList_colum = DataList1.DataKeys[Countkeyarray].ToString();
    //                //snameColumConfcpto el nombre que se obtiene desde el dataset
    //                if (keysDataList_colum == snameColumConfcpto)
    //                {
    //                    if (snameProgCve == "")
    //                    {
    //                        ObjTxt.Text = valorTransDet;
    //                    }
    //                    else
    //                    {
    //                        ObjCombo.SelectedValue = valorTransDet;
    //                    }

    //                }


    //                Countkeyarray += 1;
    //            }


    //        }


    //    }



    //}
    private void RecuperarValoresDeDataList()
    {
        //Cuando la opcion es nuevo registro se deben crear nuevo Row con los valores
        // y agregar el row la tabla del dataset dsTransDet
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            //RecuperaValoresDataListNuevo();
        }
        
        //Cuando la opcion es modificar el registro se deben actualizar los valores 
        //al Row de la tabla del dataset dsTransDet
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //RecuperaValoresDataListModificar();
        }
        
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() )
        {
            Transacciones_VizualizaColumGrid();
        }
        
    }
    //private void RecuperaValoresDataListNuevo()
    //{

    //    DataSet dsTransDet = new DataSet();

    //    int Countkeyarray = 0;
    //    dsTransDet = (DataSet)Session["dsTransDetSession"];
    //    DataRow dr = dsTransDet.Tables[0].NewRow();


    //    if (Countkeyarray == 0)
    //    {
    //        int maxSec = 1;
    //        int maxTransDetID = 1;
    //        if (dsTransDet.Tables[0].Rows.Count > 0) {
    //             maxSec = Convert.ToInt32(dsTransDet.Tables[0].Compute("max(transDetSec)", "")) + 1;
    //             maxTransDetID = Convert.ToInt32(dsTransDet.Tables[0].Compute("max(transDetId)", "")) + 1;
    //        }

    //        dsTransDet.Tables[0].Rows.Add(dr);
    //        dr["transDetId"] = maxTransDetID;
    //        if (hdfPag_sOpe.Value !="")
    //        {
    //            dr["transId"] = hdfPag_sOpe.Value;
    //        }
    //        dr["monCve"] = rCboMoneda.SelectedValue;
    //        dr["monDes"] = rCboMoneda.Text;
    //        dr["ciaCve"] = Pag_sCompania;
    //        dr["cptoId"] = rCboConcepto.SelectedValue;
    //        dr["transDetSec"] = maxSec;
    //    }


    //    //se recorren los items del datalist
    //    foreach (DataListItem dlConf in DataList1.Items)
    //    {
    //        var ObjTxt = dlConf.FindControl("RadTextBox1") as RadTextBox;
    //        var ObjCombo = dlConf.FindControl("RadComboBox1") as RadComboBox;
    //        var ObjCheckBox = dlConf.FindControl("RadCheckBox1") as RadCheckBox;
    //        var ObjCheckVal = dlConf.FindControl("RadCheckBox2") as RadCheckBox;



    //        //se obtiene el datakeys del datalist-- se obtiene el nombre del control dentro del daalist
    //        string keysDataList_colum = DataList1.DataKeys[Countkeyarray].ToString();
    //        string sValor = "";

    //        //Se obtiene el valor del control de edicion (Textbox o Combo)
    //        if (ObjTxt.Visible == true)
    //        {
    //            sValor = ObjTxt.Text;
    //        }
    //        if (ObjCombo.Visible == true)
    //        {
    //            sValor = ObjCombo.SelectedValue;
    //        }

            
            
    //        //Cuando la opcion es modificar el registro se deben actualizar los valores 
    //        //al Row de la tabla del dataset dsTransDet
    //        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //        {
    //            if (sValor == "")
    //            {
    //                dr[keysDataList_colum] = System.DBNull.Value;
    //            }
    //            else
    //            {
    //                dr[keysDataList_colum] = sValor;
    //            }
    //        }






    //        if (ObjCheckBox.Checked == true)
    //        {
    //            if (sValor == "")
    //            {
    //                ObjTxt.CssClass = "cssTxtInvalid";
    //                ObjCombo.BorderWidth = Unit.Pixel(1);
    //                ObjCombo.BorderColor = System.Drawing.Color.Red;
    //                ObjCheckBox.BorderWidth = Unit.Pixel(1);
    //                ObjCheckBox.BorderColor = System.Drawing.Color.Red;

    //                //return false;
    //            }
    //        }

    //        if (ObjCheckVal.Checked == true)
    //        {
    //            DataSet ds = new DataSet();
    //            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //            ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
    //            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 57);
    //            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
    //            ProcBD.AgregarParametrosProcedimiento("@Cve", DbType.String, 50, ParameterDirection.Input, sValor);
    //            ProcBD.AgregarParametrosProcedimiento("@progCve", DbType.String, 15, ParameterDirection.Input, ObjCheckVal.Value);
    //            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    //            //return false;
    //        }









    //        Countkeyarray += 1;
    //    }
    //    Session["dsTransDetSession"] = dsTransDet;

    //    FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, (DataSet)Session["dsTransDetSession"]);

    //}
    //private void RecuperaValoresDataListModificar() {
    //    DataSet dsTransDet = new DataSet();

    //        string stransDetId = "";

    //        var dataItem = rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;
    //        if (dataItem != null)
    //        {
    //            //se le asigna el id del row seleccionado del grid
    //            stransDetId = dataItem.GetDataKeyValue("transDetId").ToString();
    //        }

    //        int Countkeyarray = 0;
    //        //se recorren los items del datalist
    //        foreach (DataListItem dlConf in DataList1.Items)
    //        {
    //            var ObjTxt = dlConf.FindControl("RadTextBox1") as RadTextBox;
    //            var ObjCombo = dlConf.FindControl("RadComboBox1") as RadComboBox;

    //            //se obtiene el datakeys del datalist-- se obtiene el nombre del control dentro del daalist
    //            string keysDataList_colum = DataList1.DataKeys[Countkeyarray].ToString();
    //            string sValor = "";


    //            //Se obtiene el valor del control de edicion (Textbox o Combo)
    //            if (ObjTxt.Visible == true)
    //            {
    //                sValor = ObjTxt.Text;
    //            }
    //            if (ObjCombo.Visible == true)
    //            {
    //                sValor = ObjCombo.SelectedValue;
    //            }

    //            //Cuando la opcion es modificar el registro se deben actualizar los valores 
    //            //al Row de la tabla del dataset dsTransDet
    //            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //            {

    //                dsTransDet = (DataSet)Session["dsTransDetSession"];
    //                DataRow[] drTransDet;
    //                drTransDet = dsTransDet.Tables[0].Select("transDetId = " + stransDetId);


    //                if (sValor == "")
    //                {
    //                    drTransDet[0][keysDataList_colum] = System.DBNull.Value;
    //                }
    //                else
    //                {
    //                    drTransDet[0][keysDataList_colum] = sValor;
    //                }


    //                Session["dsTransDetSession"] = dsTransDet;

    //                FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, (DataSet)Session["dsTransDetSession"]);

    //            }

    //            Countkeyarray += 1;
    //        }

    //}


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

        try
        {
            //=====>> TRANSACCIONES ENCABEZADO <<=====
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
            ProcBD.AgregarParametrosProcedimiento("@transFolio", DbType.String, 10, ParameterDirection.Input, this.rTxtFolio.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@transFec", DbType.String, 100, ParameterDirection.Input, Val_TransFec);
            ProcBD.AgregarParametrosProcedimiento("@transDes", DbType.String, 100, ParameterDirection.Input, this.rTxtDesc.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@maUsuCveReg", DbType.String, 20, ParameterDirection.Input, maUser);
            ProcBD.AgregarParametrosProcedimiento("@transFecReg", DbType.String, 100, ParameterDirection.Input, Val_TransFec);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);




            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjectransId, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                //=====>> DETALLE TRANSACCIONES / MOVIMIENTOS <<=====
                if (sEjecEstatus == "1")
                {

                    rTxtFolio.Enabled = false;
                    rTxtAsientoCont.Enabled = false;
                    rCboConcepto.Enabled = false;
             

                    //=====>> TRANSACCIONES <<=====
                    if (rBtnRegTransaccion.Checked == true)
                    {
                                                                                                                                                                         
                        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                        {
                            sEjectransId = ds.Tables[0].Rows[0]["transId"].ToString();
                            hdfPag_sOpe.Value = ds.Tables[0].Rows[0]["transId"].ToString();
                            GuardaTransaccionDetalle(sEjectransId);
                            GuardaMovimientos(sEjectransId);
                            Transacciones_DatosDetalle();
                  
                        }
                        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                        {
                            GuardaTransaccionDetalle(hdfPag_sOpe.Value);
                            GuardaMovimientos(hdfPag_sOpe.Value);
                            Transacciones_DatosDetalle();
                            EliminaAsiContDeOpeyAdmon(hdfPag_sOpe.Value);
                        }

                        ShowAlert(sEjecEstatus, sEjecMSG);
                    }



                    //=====>> MOVIMIENTOS <<=====
                    if (rBtnRegMovimiento.Checked == true)
                    {
                        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                        {
                            sEjectransId = ds.Tables[0].Rows[0]["transId"].ToString();
                            hdfPag_sOpe.Value = ds.Tables[0].Rows[0]["transId"].ToString();
                            GuardaMovimientos_Captura(sEjectransId);

                        }
                        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                        {
                            GuardaMovimientos_Captura(hdfPag_sOpe.Value);
                            EliminaAsiContDeOpeyAdmon(hdfPag_sOpe.Value);
                        }

                        ShowAlert(sEjecEstatus, sEjecMSG);
                    }

                }
                else {
                    ShowAlert(sEjecEstatus, sEjecMSG);
                    return;
                }
            }


            //=====>> EJECUTA VALIDACION <<=====
            //if (FnValAdoNet.bDSIsFill(ds))
            //{
            //    if (ds.Tables[0].Rows[0]["maMSGTip"].ToString() == "1") {
            //        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            //        {
            //            DataSet DSvALIDA = new DataSet();
            //            Int32 sEjectransId = Convert.ToInt32(ds.Tables[0].Rows[0]["transId"]);
            //            string svaliProcCve = Convert.ToString(Session["TipoCptoOpe"]) + "OPE";
            //            string sSit = ds.Tables[0].Rows[0]["Sit"].ToString();
            //            DSvALIDA = FNValida.dsEXPROOpe_ValidacionesProcesos(Pag_sConexionLog, Pag_sCompania, sEjectransId, svaliProcCve, maUser, sSit);
            //        }

            //    }
            //}
                
            


        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
        }






    }
    private void GuardaTransaccionDetalle(string transId)
    {
        int ValEliminar = 0;
        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsTransDetSession"];

        DataSet ds = new DataSet();
        if (dsTransDet.Tables[0].Rows.Count == 0) {
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_TransaccionesDetalle";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania );
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(transId));
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

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            }
        }
        
    }



    private void GuardaMovimientos(string transId) {

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
        catch (Exception ex) {
        } 
    }
    private void GuardaMovimientos_CapturaV1(string transId)
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

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                //ShowAlert(sEjecEstatus, sEjecMSG);
            }
        }

    }
    private void GuardaMovimientos_Captura(string transId)
    {   
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

        TipoCptoXPCCIN();
        if (TipoCpto != "I")
        {
            GuardaMovimientos_CapturaGral(dsTransMov, transId);
        }
        else {
            GuardaMovimientos_CapturaIN(dsTransMov, transId);
        }
   }

    private void GuardaMovimientos_CapturaGral(DataSet ds,string transId)
    {
        int ValEliminar = 0;
        foreach (DataRow drConfCpto in ds.Tables[0].Rows)
        {
            ValEliminar += 1;
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Movimientos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(transId));
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);

            //Tipo de Aplicacion
            if (drConfCpto["movTipApli"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movMapSecApli", DbType.Int16, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movTipApli"]));
            }

            if (drConfCpto["movFolio"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movFolio", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movFolio"]));
            }

            if (drConfCpto["movSec"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movSec"]));
            }

            if (drConfCpto["monCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(drConfCpto["monCve"]));
            }

            if (drConfCpto["movCoA"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movCoA", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movCoA"]));
            }

            if (drConfCpto["movTipApli"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movTipApli", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movTipApli"]));
            }

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
                DateTime date2 = new DateTime();
                string DetFec_02;
                date2 = Convert.ToDateTime(drConfCpto["movContFec"]);
                DetFec_02 = date2.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@movContFec", DbType.String, 100, ParameterDirection.Input, DetFec_02.Substring(0, 10));
            }


            if (drConfCpto["movDatClasif"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movDatClasif", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movDatClasif"]));
            }

            if (drConfCpto["movPolCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movPolCve", DbType.String, 40, ParameterDirection.Input, Convert.ToString(drConfCpto["movPolCve"]));
            }
            ProcBD.AgregarParametrosProcedimiento("@movSit", DbType.String, 3, ParameterDirection.Input, "R");
            ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, ValEliminar);
            ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, hdf_transDetSec.Value);
 
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                //ShowAlert(sEjecEstatus, sEjecMSG);
            }
        }
    }
    private void GuardaMovimientos_CapturaIN(DataSet ds,string transId)
    {
        int ValEliminar = 0;
        foreach (DataRow drConfCpto in ds.Tables[0].Rows)
        {
            ValEliminar += 1;
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Movimientos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(transId));
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);

            if (drConfCpto["movTipApli"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movMapSecApli", DbType.Int16, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movTipApli"]));
            }
            if (drConfCpto["movFolio"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movFolio", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movFolio"]));
            }
            if (drConfCpto["movSec"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movSec"]));
            }
            if (drConfCpto["monCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(drConfCpto["monCve"]));
            }
            if (drConfCpto["movCoA"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movCoA", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movCoA"]));
            }
            if (drConfCpto["movTipApli"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movTipApli", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(drConfCpto["movTipApli"]));
            }
            if (drConfCpto["movRef10_01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_01", DbType.String, 10, ParameterDirection.Input, drConfCpto["movRef10_01"]);
            }
            if (drConfCpto["movRef10_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_02", DbType.String, 10, ParameterDirection.Input, drConfCpto["movRef10_02"]);
            }
            if (drConfCpto["movRef10_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef10_03"]));
            }
            if (drConfCpto["movRef10_04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef10_04"]));
            }
            if (drConfCpto["movRef10_Prov"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_Prov", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef10_Prov"]));
            }
            if (drConfCpto["movRef10_Alm"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_Alm", DbType.String, 10, ParameterDirection.Input, drConfCpto["movRef10_Alm"]);
            }
            if (drConfCpto["movRef10_UniMed"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_UniMed", DbType.String, 10, ParameterDirection.Input, drConfCpto["movRef10_UniMed"]);
            }
            if (drConfCpto["movRef10_CC"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_CC", DbType.String, 10, ParameterDirection.Input, drConfCpto["movRef10_CC"]);
            }
            if (drConfCpto["movRef10_OrdComp"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10_OrdComp", DbType.String, 10, ParameterDirection.Input, drConfCpto["movRef10_OrdComp"]);
            }
            if (drConfCpto["movRef20_Art"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef20_Art", DbType.String, 20, ParameterDirection.Input, drConfCpto["movRef20_Art"]);
            }
            if (drConfCpto["movRef20_Lote"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef20_Lote", DbType.String, 20, ParameterDirection.Input, drConfCpto["movRef20_Lote"]);
            }
            if (drConfCpto["movRef20_Serie"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef20_Serie", DbType.String, 20, ParameterDirection.Input, drConfCpto["movRef20_Serie"]);
            }
            if (drConfCpto["movRef40_Des"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef40_Des", DbType.String, 40, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef40_Des"]));
            }
            if (drConfCpto["movFec_Mov"].ToString() != "")
            {
                DateTime date1 = new DateTime();
                string DetFec_01;
                date1 = Convert.ToDateTime(drConfCpto["movFec_Mov"]);
                DetFec_01 = date1.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@movFec_Mov", DbType.String, 100, ParameterDirection.Input, DetFec_01.Substring(0, 10));
            }
            if (drConfCpto["movFec_02"].ToString() != "")
            {
                DateTime date1 = new DateTime();
                string DetFec;
                date1 = Convert.ToDateTime(drConfCpto["movFec_02"]);
                DetFec  = date1.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@movFec_02", DbType.String, 100, ParameterDirection.Input, DetFec.Substring(0, 10));
            }
            if (drConfCpto["movFec_03"].ToString() != "")
            {
                DateTime date1 = new DateTime();
                string DetFec;
                date1 = Convert.ToDateTime(drConfCpto["movFec_03"]);
                DetFec = date1.ToString("yyyy-MM-dd h:mm tt");
                ProcBD.AgregarParametrosProcedimiento("@movFec_03", DbType.String, 100, ParameterDirection.Input, DetFec.Substring(0, 10));
            }
            if (drConfCpto["movImp_Imp"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movImp_Imp", DbType.Decimal, 10, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["movImp_Imp"]));
            }
            if (drConfCpto["movImp_02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movImp_02", DbType.Decimal, 10, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["movImp_02"]));
            }
            if (drConfCpto["movImp_03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movImp_03", DbType.Decimal, 10, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["movImp_03"]));
            }
            if (drConfCpto["movFac_Cant"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movFac_Cant", DbType.Decimal, 10, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["movFac_Cant"]));
            }
            if (drConfCpto["movFac_Prec"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movFac_Prec", DbType.Decimal, 10, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["movFac_Prec"]));
            }
            if (drConfCpto["movFac_Costo"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movFac_Costo", DbType.Decimal, 10, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["movFac_Costo"]));
            }
            if (drConfCpto["movFac_TipCam"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movFac_TipCam", DbType.Decimal, 10, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["movFac_TipCam"]));
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
            if (drConfCpto["movRef10AlmContra"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef10AlmContra", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef10AlmContra"]));
            } 
            if (drConfCpto["movRef20Adu"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@movRef20Adu", DbType.String, 20, ParameterDirection.Input, Convert.ToString(drConfCpto["movRef20Adu"]));
            }
          

            ProcBD.AgregarParametrosProcedimiento("@movSit", DbType.String, 3, ParameterDirection.Input, "R");
            ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, ValEliminar);
            ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, hdf_transDetSec.Value);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                //ShowAlert(sEjecEstatus, sEjecMSG);
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
            ProcBD.AgregarParametrosProcedimiento("@tipoElim", DbType.String , 5, ParameterDirection.Input, "TRANS");
            oWS.EjecutarProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        }
        catch (Exception ex)
        {
            string MsgError;
            MsgError = ex.ToString();
        }

    }




    private void EjecutaAccionDetalle() {
        string sMSGTip = "";


        if (rBtnRegTransaccion.Checked == true) {

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
    private void EjecutaAccionDetalle_Trans() {

        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsTransDetSession"];

        Session[("hdfBtnAccionDet")] = hdfBtnAccionDet.Value;
        Session["cptoId"] = rCboConcepto.SelectedValue;

        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            string script = "function f(){$find(\"" + TextCombos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            var dataItem = rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;
            string stransDetId = dataItem.GetDataKeyValue("transDetId").ToString();
            Session["dataItemSession"] = stransDetId;

            string script = "function f(){$find(\"" + TextCombos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            Eliminar();
        }
    }

    private void EjecutaAccionDetalle_Mov()
    {
        if (Session["TipoCptoOpe"].ToString() == "XP")
        {
            MovXP();
        }
        if (Session["TipoCptoOpe"].ToString() == "CC")
        {
            MovCC();
        }
        if (Session["TipoCptoOpe"].ToString() == "IN")
        {
            MovIN();
        }
    }
    public void MovCC()
    {
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            Session[("hdfBtnAccionMov")] = hdfBtnAccionDet.Value;
            string script = "function f(){$find(\"" + rWinRegOpeDet_MovCC.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //Opcion para mostrar la ventana
            Session[("hdfBtnAccionMov")] = hdfBtnAccionDet.Value;

            var dataItem = rGdvOperacionesMovCC.SelectedItems[0] as GridDataItem;
            string sMovDetId = dataItem.GetDataKeyValue("movID").ToString();
            Session["movIDSession"] = sMovDetId;

            string script = "function f(){$find(\"" + rWinRegOpeDet_MovCC.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            EliminaMov();
        }
    }
    public void MovXP()
    {
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            Session[("hdfBtnAccionMov")] = hdfBtnAccionDet.Value;
            string script = "function f(){$find(\"" + rWinRegOpeDet_MovXP.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //Opcion para mostrar la ventana
            Session[("hdfBtnAccionMov")] = hdfBtnAccionDet.Value;

            var dataItem = rGdvOperacionesMovCC.SelectedItems[0] as GridDataItem;
            string sMovDetId = dataItem.GetDataKeyValue("movID").ToString();
            Session["movIDSession"] = sMovDetId;

            string script = "function f(){$find(\"" + rWinRegOpeDet_MovXP.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            EliminaMov();
        }
    }
    public void Eliminar()
    {
        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsTransDetSession"];

        string stransDetId = "";
        var dataItem = rGdvOperacionesTrans.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //se le asigna el id del row seleccionado del grid
            stransDetId = dataItem.GetDataKeyValue("transDetId").ToString();
        }


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

        Session["dsTransDetSession"] = dsTransDet;
        FnCtlsFillIn.RadGrid(ref rGdvOperacionesTrans, (DataSet)Session["dsTransDetSession"]);

        hdfBtnAccionDet.Value = "";
        ControlesAccion();

 
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
    protected void FechaValidaPeriodo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Periodos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 60);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@perFec", DbType.String, 100, ParameterDirection.Input, RdDateFecha.ValidationDate + " 00:00:00");
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSIsFill(ds))
        {

            string sit = "";

            sit = ds.Tables[0].Rows[0]["perSit"].ToString();
            if (sit == "2")
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);
                RdDateFecha.Clear();
                rBtnGuardar.Enabled = false;
            }
        }
        else
        {
            string sEjecEstatus, sEjecMSG = "";
            sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
            sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
            ShowAlert(sEjecEstatus, sEjecMSG);
            RdDateFecha.Clear();
            rBtnGuardar.Enabled = true;
        }


    }
    private void AgregaColumnasRdGrid()
    {
        GridBoundColumn boundColumn1;
        GridBoundColumn boundColumn2;
        GridBoundColumn boundColumn3;
        GridBoundColumn boundColumn4;
        GridBoundColumn boundColumn5;
        GridBoundColumn boundColumn6;
        GridBoundColumn boundColumn7;
        GridBoundColumn boundColumn8;
        GridBoundColumn boundColumn9;
        GridBoundColumn boundColumn10;
        GridBoundColumn boundColumn11;
        GridBoundColumn boundColumn12;
        GridBoundColumn boundColumn13;
        GridBoundColumn boundColumn14;
        GridBoundColumn boundColumn15;
        GridBoundColumn boundColumn16;
        GridBoundColumn boundColumn17;
        GridBoundColumn boundColumn18;
        GridBoundColumn boundColumn19;
        GridBoundColumn boundColumn20;
        GridBoundColumn boundColumn21;
        GridBoundColumn boundColumn22;
        GridBoundColumn boundColumn23;
        GridBoundColumn boundColumn24;
        GridBoundColumn boundColumn25;
        GridBoundColumn boundColumn26;
        GridBoundColumn boundColumn27;
        GridBoundColumn boundColumn28;
        GridBoundColumn boundColumn29;
        GridBoundColumn boundColumn30;
        GridBoundColumn boundColumn31;
        GridBoundColumn boundColumn32;
        GridBoundColumn boundColumn33;
        GridBoundColumn boundColumn34;
        GridBoundColumn boundColumn35;
        GridBoundColumn boundColumn36;
        GridBoundColumn boundColumn37;
        GridBoundColumn boundColumn38;
        GridBoundColumn boundColumn39;
        GridBoundColumn boundColumn40;
        GridBoundColumn boundColumn41;
        GridBoundColumn boundColumn42;
        GridBoundColumn boundColumn43;
        GridBoundColumn boundColumn44;
        GridBoundColumn boundColumn45;

        boundColumn1 = new GridBoundColumn();
        boundColumn2 = new GridBoundColumn();
        boundColumn3 = new GridBoundColumn();
        boundColumn4 = new GridBoundColumn();
        boundColumn5 = new GridBoundColumn();
        boundColumn6 = new GridBoundColumn();
        boundColumn7 = new GridBoundColumn();
        boundColumn8 = new GridBoundColumn();
        boundColumn9 = new GridBoundColumn();
        boundColumn10 = new GridBoundColumn();
        boundColumn11 = new GridBoundColumn();
        boundColumn12 = new GridBoundColumn();
        boundColumn13 = new GridBoundColumn();
        boundColumn14 = new GridBoundColumn();
        boundColumn15 = new GridBoundColumn();
        boundColumn16 = new GridBoundColumn();
        boundColumn17 = new GridBoundColumn();
        boundColumn18 = new GridBoundColumn();
        boundColumn19 = new GridBoundColumn();
        boundColumn20 = new GridBoundColumn();
        boundColumn21 = new GridBoundColumn();
        boundColumn22 = new GridBoundColumn();
        boundColumn23 = new GridBoundColumn();
        boundColumn24 = new GridBoundColumn();
        boundColumn25 = new GridBoundColumn();
        boundColumn26 = new GridBoundColumn();
        boundColumn27 = new GridBoundColumn();
        boundColumn28 = new GridBoundColumn();
        boundColumn29 = new GridBoundColumn();
        boundColumn30 = new GridBoundColumn();
        boundColumn31 = new GridBoundColumn();
        boundColumn32 = new GridBoundColumn();
        boundColumn33 = new GridBoundColumn();
        boundColumn34 = new GridBoundColumn();
        boundColumn35 = new GridBoundColumn();
        boundColumn36 = new GridBoundColumn();
        boundColumn37 = new GridBoundColumn();
        boundColumn38 = new GridBoundColumn();
        boundColumn39 = new GridBoundColumn();
        boundColumn40 = new GridBoundColumn();
        boundColumn41 = new GridBoundColumn();
        boundColumn42 = new GridBoundColumn();
        boundColumn43 = new GridBoundColumn();
        boundColumn44 = new GridBoundColumn();
        boundColumn45 = new GridBoundColumn();

        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn1);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn2);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn3);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn4);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn5);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn6);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn7);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn8);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn9);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn10);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn11);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn12);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn13);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn14);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn15);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn16);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn17);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn18);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn19);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn20);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn21);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn22);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn23);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn24);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn25);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn26);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn27);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn28);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn29);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn30);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn31);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn32);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn33);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn34);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn35);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn36);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn37);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn38);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn39);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn40);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn41);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn42);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn43);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn44);
        rGdvOperacionesMovCC.MasterTableView.Columns.Add(boundColumn45);


        boundColumn1.DataField = "movID";
        boundColumn1.HeaderText = "movCCID";
        boundColumn1.ItemStyle.Width = 50;
        boundColumn1.HeaderStyle.Width = 50;
        boundColumn1.Display = false;

        boundColumn2.DataField = "transId";
        boundColumn2.HeaderText = "transId";
        boundColumn2.ItemStyle.Width = 50;
        boundColumn2.HeaderStyle.Width = 50;
        boundColumn2.Display = false;

        boundColumn3.DataField = "ciaCve";
        boundColumn3.HeaderText = "ciaCve";
        boundColumn3.ItemStyle.Width = 50;
        boundColumn3.HeaderStyle.Width = 50;
        boundColumn3.Display = false;

        boundColumn4.DataField = "cptoId";
        boundColumn4.HeaderText = "cptoId";
        boundColumn4.ItemStyle.Width = 50;
        boundColumn4.HeaderStyle.Width = 50;
        boundColumn4.Display = false;

        boundColumn5.DataField = "movFolio";
        boundColumn5.HeaderText = "Folio";
        boundColumn5.ItemStyle.Width = 100;
        boundColumn5.HeaderStyle.Width = 100;
        boundColumn5.Display = true;

        boundColumn6.DataField = "movSec";
        boundColumn6.HeaderText = "Secuencia";
        boundColumn6.ItemStyle.Width = 100;
        boundColumn6.HeaderStyle.Width = 100;
        boundColumn6.Display = true;

        boundColumn7.DataField = "movCoA";
        boundColumn7.HeaderText = "movCoA";
        boundColumn7.ItemStyle.Width = 50;
        boundColumn7.HeaderStyle.Width = 50;
        boundColumn7.Display = false;

        boundColumn8.DataField = "movCoADes";
        boundColumn8.HeaderText = "Movimiento";
        boundColumn8.ItemStyle.Width = 100;
        boundColumn8.HeaderStyle.Width = 100;
        boundColumn8.Display = true;

        boundColumn9.DataField = "movTipApli";
        boundColumn9.HeaderText = "movTipApli";
        boundColumn9.ItemStyle.Width = 50;
        boundColumn9.HeaderStyle.Width = 50;
        boundColumn9.Display = false;

        boundColumn10.DataField = "movTipApliADes";
        boundColumn10.HeaderText = "Aplica O.C";
        boundColumn10.ItemStyle.Width = 100;
        boundColumn10.HeaderStyle.Width = 100;
        boundColumn10.Display = true;


        //-----------
        boundColumn11.DataField = "movRef20_Art";
        boundColumn11.HeaderText = "movRef20_Art";
        boundColumn11.ItemStyle.Width = 50;
        boundColumn11.HeaderStyle.Width = 50;
        boundColumn11.Display = false;

        boundColumn12.DataField = "artDes";
        boundColumn12.HeaderText = "Articulo";
        boundColumn12.ItemStyle.Width = 150;
        boundColumn12.HeaderStyle.Width = 150;
        boundColumn12.Display = true;

        boundColumn13.DataField = "movRef10_Alm";
        boundColumn13.HeaderText = "movRef10_Alm";
        boundColumn13.ItemStyle.Width = 50;
        boundColumn13.HeaderStyle.Width = 50;
        boundColumn13.Display = false;

        boundColumn14.DataField = "almDes";
        boundColumn14.HeaderText = "Almacen";
        boundColumn14.ItemStyle.Width = 150;
        boundColumn14.HeaderStyle.Width = 150;
        boundColumn14.Display = true;

        boundColumn15.DataField = "movRef10_UniMed";
        boundColumn15.HeaderText = "movRef10_UniMed";
        boundColumn15.ItemStyle.Width = 50;
        boundColumn15.HeaderStyle.Width = 50;
        boundColumn15.Display = false;

        boundColumn16.DataField = "UniMed_Des";
        boundColumn16.HeaderText = "Unidad de Medida";
        boundColumn16.ItemStyle.Width = 150;
        boundColumn16.HeaderStyle.Width = 150;
        boundColumn16.Display = true;

        boundColumn17.DataField = "movFac_Cant";
        boundColumn17.HeaderText = "Cantidad";
        boundColumn17.DataFormatString = "{0:###,##0.00}";
        boundColumn17.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn17.ItemStyle.Width = 100;
        boundColumn17.HeaderStyle.Width = 100;
        boundColumn17.Display = true;

        boundColumn18.DataField = "movFac_Costo";
        boundColumn18.HeaderText = "Costo";
        boundColumn18.DataFormatString = "{0:###,##0.00}";
        boundColumn18.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn18.ItemStyle.Width = 100;
        boundColumn18.HeaderStyle.Width = 100;
        boundColumn18.Display = true;

        boundColumn19.DataField = "movImp_Imp";
        boundColumn19.HeaderText = "Importe";
        boundColumn19.DataFormatString = "{0:###,##0.00}";
        boundColumn19.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn19.ItemStyle.Width = 100;
        boundColumn19.HeaderStyle.Width = 100;
        boundColumn19.Display = true;

        boundColumn20.DataField = "movFac_Prec";
        boundColumn20.HeaderText = "Precio";
        boundColumn20.DataFormatString = "{0:###,##0.00}";
        boundColumn20.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn20.ItemStyle.Width = 100;
        boundColumn20.HeaderStyle.Width = 100;
        boundColumn20.Display = true;

        boundColumn21.DataField = "movRef20_Lote";
        boundColumn21.HeaderText = "Lote";
        boundColumn21.ItemStyle.Width = 50;
        boundColumn21.HeaderStyle.Width = 50;
        boundColumn21.Display = true;

        boundColumn22.DataField = "movRef20_Serie";
        boundColumn22.HeaderText = "Serie";
        boundColumn22.ItemStyle.Width = 50;
        boundColumn22.HeaderStyle.Width = 50;
        boundColumn22.Display = true;

        boundColumn23.DataField = "monCve";
        boundColumn23.HeaderText = "monCve";
        boundColumn23.ItemStyle.Width = 50;
        boundColumn23.HeaderStyle.Width = 50;
        boundColumn23.Display = false;

        boundColumn24.DataField = "monDes";
        boundColumn24.HeaderText = "Moneda";
        boundColumn24.ItemStyle.Width = 175;
        boundColumn24.HeaderStyle.Width = 175;
        boundColumn24.Display = true;

        boundColumn25.DataField = "movFac_TipCam";
        boundColumn25.HeaderText = "Tipo de Cambio";
        boundColumn25.DataFormatString = "{0:###,##0.00}";
        boundColumn25.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn25.ItemStyle.Width = 150;
        boundColumn25.HeaderStyle.Width = 150;
        boundColumn25.Display = true;

        boundColumn26.DataField = "movRef10_CC";
        boundColumn26.HeaderText = "Centro de Costros";
        boundColumn26.ItemStyle.Width = 130;
        boundColumn26.HeaderStyle.Width = 130;
        boundColumn26.Display = true;

        boundColumn27.DataField = "movRef10_OrdComp";
        boundColumn27.HeaderText = "Orden de Compra";
        boundColumn27.ItemStyle.Width = 130;
        boundColumn27.HeaderStyle.Width = 130;
        boundColumn27.Display = true;

        boundColumn28.DataField = "movRef10_Prov";
        boundColumn28.HeaderText = "Proveedor";
        boundColumn28.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn28.ItemStyle.Width = 100;
        boundColumn28.HeaderStyle.Width = 100;
        boundColumn28.Display = true;

        boundColumn29.DataField = "movFec_Mov";
        boundColumn29.HeaderText = "Fecha";
        boundColumn29.DataFormatString = "{0:d}";
        boundColumn29.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        boundColumn29.ItemStyle.Width = 100;
        boundColumn29.HeaderStyle.Width = 100;
        boundColumn29.Display = true;

        boundColumn30.DataField = "movFec_02";
        boundColumn30.HeaderText = "Fecha 2";
        boundColumn30.DataFormatString = "{0:d}";
        boundColumn30.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        boundColumn30.ItemStyle.Width = 100;
        boundColumn30.HeaderStyle.Width = 100;
        boundColumn30.Display = true;

        boundColumn31.DataField = "movRef10_01";
        boundColumn31.HeaderText = "Referencia 1";
        boundColumn31.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn31.ItemStyle.Width = 100;
        boundColumn31.HeaderStyle.Width = 100;
        boundColumn31.Display = true;

        boundColumn32.DataField = "movRef10_02";
        boundColumn32.HeaderText = "Referencia 2";
        boundColumn32.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn32.ItemStyle.Width = 100;
        boundColumn32.HeaderStyle.Width = 100;
        boundColumn32.Display = true;

        boundColumn33.DataField = "movRef10_03";
        boundColumn33.HeaderText = "Referencia 3";
        boundColumn33.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn33.ItemStyle.Width = 100;
        boundColumn33.HeaderStyle.Width = 100;
        boundColumn33.Display = true;

        boundColumn34.DataField = "movRef10_04";
        boundColumn34.HeaderText = "Referencia 4";
        boundColumn34.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn34.ItemStyle.Width = 100;
        boundColumn34.HeaderStyle.Width = 100;
        boundColumn34.Display = true;

        //-----------

        boundColumn35.DataField = "movRef40_Des";
        boundColumn35.HeaderText = "movRef40_Des";
        boundColumn35.ItemStyle.Width = 50;
        boundColumn35.HeaderStyle.Width = 50;
        boundColumn35.Display = false;

        boundColumn36.DataField = "movFec_03";
        boundColumn36.HeaderText = "movFec_03";
        boundColumn36.DataFormatString = "{0:d}";
        boundColumn36.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        boundColumn36.ItemStyle.Width = 100;
        boundColumn36.HeaderStyle.Width = 100;
        boundColumn36.Display = false;

        boundColumn37.DataField = "movImp_02";
        boundColumn37.HeaderText = "movImp_02";
        boundColumn37.DataFormatString = "{0:###,##0.00}";
        boundColumn37.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn37.ItemStyle.Width = 100;
        boundColumn37.HeaderStyle.Width = 100;
        boundColumn37.Display = false;

        boundColumn38.DataField = "movImp_03";
        boundColumn38.HeaderText = "movImp_03";
        boundColumn38.DataFormatString = "{0:###,##0.00}";
        boundColumn38.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
        boundColumn38.ItemStyle.Width = 100;
        boundColumn38.HeaderStyle.Width = 100;
        boundColumn38.Display = false;

        boundColumn39.DataField = "movContRecCve";
        boundColumn39.HeaderText = "movContRecCve";
        boundColumn39.ItemStyle.Width = 50;
        boundColumn39.HeaderStyle.Width = 50;
        boundColumn39.Display = false;

        boundColumn40.DataField = "movContFec";
        boundColumn40.HeaderText = "movContFec";
        boundColumn40.ItemStyle.Width = 50;
        boundColumn40.HeaderStyle.Width = 50;
        boundColumn40.Display = false;

        boundColumn41.DataField = "movDatClasif";
        boundColumn41.HeaderText = "movDatClasif";
        boundColumn41.ItemStyle.Width = 50;
        boundColumn41.HeaderStyle.Width = 50;
        boundColumn41.Display = false;

        boundColumn42.DataField = "movPolCve";
        boundColumn42.HeaderText = "movPolCve";
        boundColumn42.ItemStyle.Width = 50;
        boundColumn42.HeaderStyle.Width = 50;
        boundColumn42.Display = false;

        boundColumn43.DataField = "movRef10AlmContra";
        boundColumn43.HeaderText = "Almacen Contra CVE";
        boundColumn43.ItemStyle.Width = 100;
        boundColumn43.HeaderStyle.Width = 100;
        boundColumn43.Display = false;

        boundColumn44.DataField = "almContraDes";
        boundColumn44.HeaderText = "Almacen Contra";
        boundColumn44.ItemStyle.Width = 120;
        boundColumn44.HeaderStyle.Width = 120;
        boundColumn44.Display = true;

        boundColumn45.DataField = "movRef20Adu";
        boundColumn45.HeaderText = "Aduana";
        boundColumn45.ItemStyle.Width = 100;
        boundColumn45.HeaderStyle.Width = 100;
        boundColumn45.Display = true;
    }
    public void TipoCptoXPCCIN()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, 1, ParameterDirection.Input, 65);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, (10), ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@CptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt32(rCboConcepto.SelectedValue));
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        TipoCpto = ds.Tables[0].Rows[0]["contRefCve"].ToString();


        TipoCpto = TipoCpto.Substring(0, 1);

    }
    public void MovIN()
    {
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            Session[("hdfBtnAccionMov")] = hdfBtnAccionDet.Value;
            string script = "function f(){$find(\"" + rWinRegOpeDet_MovIN.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //Opcion para mostrar la ventana
            Session[("hdfBtnAccionMov")] = hdfBtnAccionDet.Value;

            var dataItem = rGdvOperacionesMovCC.SelectedItems[0] as GridDataItem;
            string sMovDetId = dataItem.GetDataKeyValue("movID").ToString();
            Session["movIDSession"] = sMovDetId;

            string script = "function f(){$find(\"" + rWinRegOpeDet_MovIN.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }

        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            EliminaMov();
        }
    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    #endregion

    #region FUNCIONES
    DataSet ConceptoConfiguracion() {
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
    DataSet ConceptoDefinicion() {
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
    public string ObtenerFecha()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha.SelectedDate);

        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;
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

            if (RdDateFecha.SelectedDate.ToString() == "")
            {
                RdDateFecha.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdDateFecha.CssClass = "cssTxtEnabled"; }

            if (rTxtDesc.Text == "")
            {
                rTxtDesc.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDesc.CssClass = "cssTxtEnabled"; }

            if (rTxtFolio.Text == "")
            {
                rTxtFolio.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtFolio.CssClass = "cssTxtEnabled"; }

            if (rBtnRegTransaccion.Checked == false && rBtnRegMovimiento.Checked == false)
            {

                camposInc += 1;
            }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            else {

                if (rGdvOperacionesTrans.Items.Count == 0) {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1019", ref sMSGTip, ref sResult);
                }

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
    private string validaEjecutaAccionDetalle(ref string sMSGTip)
    {
        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";


        //Nuevo
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rTxtFolio.Text.Trim() == "")
            {
                rTxtFolio.CssClass = "cssTxtInvalid";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1020", ref sMSGTip, ref sResult);
                return sResult;
            }
            else {
                rTxtFolio.CssClass = "cssTxtEnabled";
            }

        }


        //Modificar
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {


            if (rBtnRegTransaccion.Checked == true)
            {
                if (rGdvOperacionesTrans.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);

                    return sResult;
                }
            }

            if (rBtnRegMovimiento.Checked == true)
            {
                if (rGdvOperacionesMovCC.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }
            }

            return sResult;
        }


        //ELIMINAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rBtnRegTransaccion.Checked == true)
            {
                if (rGdvOperacionesTrans.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);

                    return sResult;
                }


            }


            if (rBtnRegMovimiento.Checked == true)
            {
                if (rGdvOperacionesMovCC.SelectedItems.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);

                    return sResult;
                }
            }


                return sResult;
        }

        return sResult;
    }
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        RadGrid objRGrid = new RadGrid();
        if (rBtnRegTransaccion.Checked == true)
        {
            objRGrid = rGdvOperacionesTrans; 
        }
        if (rBtnRegMovimiento.Checked == true)
        {
            objRGrid = rGdvOperacionesMovCC; 
        }

        string sResult = "";
        int GvSelectItem = objRGrid.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (rTxtFolio.Text.Trim() == "")
            {
                rTxtFolio.CssClass = "cssTxtInvalid";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1020", ref sMSGTip, ref sResult);
                return sResult;
            }
            else
            {
                rTxtFolio.CssClass = "cssTxtEnabled";
            }
        }

        //MODIFICAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        { 
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, objRGrid, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, objRGrid, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        
        return sResult;
    }
    
    #endregion


}