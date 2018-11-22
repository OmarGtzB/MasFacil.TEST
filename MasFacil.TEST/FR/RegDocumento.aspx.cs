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

public partial class FR_RegDocumento : System.Web.UI.Page
{
    #region VARIABLES

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

    comprobante.Datos doc = new comprobante.Datos();

    oWSCFDI.Service oWSTimbre = new oWSCFDI.Service();
    ws.Servicio oWS = new ws.Servicio();
    wsRpt.Service oWSRpt = new wsRpt.Service();

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

    private string Pag_sIdDocReg;
    private string DocCve;
    private string Pag_sUsrCve;

    //FILTROS
    private static string Val_Documento = "";
    private static Int64 Val_Situacion = 0;
    private static string Val_Fec_Inicio = "";
    private static string Val_Fec_Fin = "";
    private static string Val_Cliente = "";
    private static string Val_Subcliente = "";

    private static int contador = 1;
    private string idformdocform;
    DataTable customerOrders = new DataTable();


    private static DataTable VisualizarTipDocs = new DataTable();
    DataRow drTmpAlm;
    private static string Filtro_Vis_Doc = "";

    private static Int64 docCmpTrnId = 0;

    #endregion

    #region EVENTOS

    //=====> EVENTOS CONTROLES
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
    protected void rGdv_Documentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataList1.DataBind();
        DataList2.DataBind();
        DataList3.DataBind();
        RastreaDocumento();
    }
    protected void rCboDocumento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Val_Documento = "";
        Val_Documento = rCboDocumento.SelectedValue;
        LlenaGridDocumentosFiltros();
    }
    protected void rCboSituacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Val_Situacion = 0;
        Val_Situacion = Convert.ToInt64(rCboSituacion.SelectedValue);
        LlenaGridDocumentosFiltros();
    }
    protected void RdDateFecha_Inicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        Val_Fec_Inicio = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Inicio.SelectedDate);
        Val_Fec_Inicio = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            LlenaGridDocumentosFiltros();
        }
        else
        {
            RdDateFecha_Inicio.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Inicial no puede ser mayor a la Final", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
        }
    }
    protected void RdDateFecha_Final_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        Val_Fec_Fin = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Final.SelectedDate);
        Val_Fec_Fin = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            LlenaGridDocumentosFiltros();
        }
        else
        {
            RdDateFecha_Final.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Final no puede ser Menor a la Inical", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");

        }
    }
    protected void rCboClientes_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Val_Cliente = "";
        Val_Cliente = rCboClientes.SelectedValue;
        LlenaGridDocumentosFiltros();
        SubClientesCombo();
    }
    protected void rCboSubClientes_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        Val_Subcliente = "";
        Val_Subcliente = rCboClientes.SelectedValue + rCboSubClientes.SelectedValue;
        LlenaGridDocumentosFiltros();
    }

    protected void btnPadre_Click(object sender, EventArgs e)
    {
        RadButton btn = (RadButton)sender;
        btnIdActual.Value = btn.Value;

        RastrearDocumentoBnt();
    }
    protected void btnRastreoActual_Click(object sender, EventArgs e)
    {
        RadButton btn = (RadButton)sender;
        btnIdActual.Value = btn.Value;
    }
    protected void btnDerivado_Click1(object sender, EventArgs e)
    {
        RadButton btn = (RadButton)sender;
        btnIdActual.Value = btn.Value;
        RastrearDocumentoBnt();
    }


    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }
    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }
    protected void rBtnValidacion_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString();
        ControlesAccion();
    }
    protected void rBtVerErr_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString();
        ControlesAccion();
    }
    protected void rBtnAutorizar_Click(object sender, ImageButtonClickEventArgs e) {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Autorizado).ToString();
        ControlesAccion();
    }
    protected void rBtnProcesar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Procesado).ToString();
        ControlesAccion();
    }
    protected void rBtnCancelarSit_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Cancelado).ToString();
        ControlesAccion();
    }
    protected void rBtnVizualizarDoc_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Visualizar).ToString();
        ControlesAccion();
    }
    protected void rBtnImprimir_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Imprimir).ToString();
        ControlesAccion();
    }
    protected void rBtnCopiar_Click(object sender, ImageButtonClickEventArgs e)///-----------------------------------------------------
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString();
        ControlesAccion();
    }
    protected void rBtnCFDITimbre_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.CFDITimbre).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e) 
    {
        EjecutaAccionLimpiar();
    }
    protected void RAJAXMAN1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        {
            rGdv_Documentos.MasterTableView.ClearSelectedItems();
        }

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString()||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Cancelado).ToString()
            )
        {
            InicioPagina();
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
        Pag_sUsrCve = Convert.ToString(Session["User"]);

    }
    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
    }
    public void InicioPagina()
    {
        Session["RawUrl_Return"] = "";
        hdfRawUrl.Value = this.Page.Request.RawUrl.ToString();
        
        hdfBtnAccion.Value = "";

        TituloPagina();
        ControlesAccion();

        llenaFiltros();
        LlenaGridDocumentos();


        if (VisualizarTipDocs.Columns.Count == 0)
        {
            VisualizarTipDocs.Columns.Add("DocCve");
        }

        rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Documentos.AllowMultiRowSelection = true;

        PermisoBotones();
    }
    private void TituloPagina()
    {
        FNGrales.bTitleDesc(Page, "Registro de Documentos", "PnlMPFormTituloApartado");
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
    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        ////===> CONTROLES GENERAL
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtVerErr.Image.Url = "~/Imagenes/IcoBotones/IcoBtnVerErrores.png";
        rBtnAutorizar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";
        rBtnProcesar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnProcesar.png";
        rBtnCancelarSit.Image.Url = "~/Imagenes/IcoBotones/IcoBtnCancela.png";
        rBtnVizualizar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnVisualizar.png";
        rBtnImprimir.Image.Url = "~/Imagenes/IcoBotones/IcoBtnImprimir.png";
        rBtnRastreo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnRastreo.png";
        rBtnCopiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnCopiar.png";

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
            //NUEVO  //MODIFICAR //ELIMIAR //VALIDACION //COPIAR //VER ERRORES
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString()
                )
            {
                EjecutaAccion();
            }

            //AUTORIZAR  //PROCESAR //CANCELAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Autorizado).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Procesado).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Cancelado).ToString()  
                )
            {
                EjecutaAccion();
            }

            //VISUALIZAR //IMPRIMIR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Visualizar).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Imprimir).ToString()
                )
            {
                EjecutaAccion();
            }

            //CFDI TIMBRE
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.CFDITimbre).ToString()) {
                EjecutaAccion();
            }


        }
        if (Result == false)
        {
            //rGdv_Documentos.MasterTableView.ClearSelectedItems();
        }
        
    }
    private void EjecutaAccionLimpiar()
    {
        LlenaGridDocumentos();

        RdDateFecha_Inicio.Clear();
        RdDateFecha_Final.Clear();
        rCboDocumento.ClearSelection();
        rCboSituacion.ClearSelection();
        rCboClientes.ClearSelection();
        rCboSubClientes.ClearSelection();
        rCboSubClientes.Enabled = false;
        DataList1.DataBind();
        DataList2.DataBind();
        DataList3.DataBind();
    }
    private void EjecutaAccion()
    {

        string sMSGTip = "";
        string sMsgAlert = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);

        if (msgValidacion == "")
        {

            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                Session["Valor_btn"] = "1";
                Session["RawUrl_Return"] = hdfRawUrl.Value; 
                Response.Redirect("~/FR/RegDocumentoNuevo.aspx");
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    Session["Valor_DocCve"] = dataItem["docRegid"].Text;
                    Session["Valor_btn"] = "2";
                    Session["RawUrl_Return"] = hdfRawUrl.Value;
                                        
                    Response.Redirect("~/FR/RegDocumentoNuevo.aspx");
                }
            }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                InicioPagina();
            }

            //VALIDACION
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString())
            {
                EjecutaSpAccionValidacion();
            }

            //VER ERRORES
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
            {
                var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                string docRegid = dataItem.GetDataKeyValue("docRegid").ToString();


                RadWindowVerErrores.NavigateUrl = "../DC/VerErrores.aspx?ErrorId=" + docRegid + "&ValCmb=" + "" + "&ProCve=" + "DOC";
                string script = "function f(){$find(\"" + RadWindowVerErrores.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            //AUTORIZAR // PROCESAR 
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Autorizado).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Procesado).ToString())
            {
                EjecutaSpAccion();
            }

            // CANCELAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Cancelado).ToString())
            {
                var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                string docRegid = dataItem.GetDataKeyValue("docRegid").ToString();

                RadWindowCancelarDoc.NavigateUrl = "CancelaDoc.aspx?Regid=" + docRegid;
                string script = "function f(){$find(\"" + RadWindowCancelarDoc.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            //VISUZALIZAR DOCUMENTO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Visualizar).ToString())
            {
                VisualizarDocumento();
            }

            //IMPRIMIR DOCUMENTOS
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Imprimir).ToString())
            {
                ImprimirDocumento();
            }

            //Copiar
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
            {
                var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                string docRegid = dataItem.GetDataKeyValue("docRegid").ToString();

                RadWindowCopiaDoc.NavigateUrl = "CopiaDoc.aspx?Regid=" + docRegid;

                string script = "function f(){$find(\"" + RadWindowCopiaDoc.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            //CFDI TIMBRE
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.CFDITimbre).ToString())
            {
                EjecutaCDFTimbre();
            }




        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }




    }
    private void EjecutaSpAccion()
    {

        string maUser = LM.sValSess(this.Page, 1);
        try
        {
            var dataItem = this.rGdv_Documentos.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {

                string sdocRegid = dataItem["docRegid"].Text;
                Int32 idocRegid = Convert.ToInt32(sdocRegid);

                string sdocRegSit = dataItem["docRegSit"].Text;
                Int32 idocRegSit = Convert.ToInt32(sdocRegSit);

                string sdocCve = dataItem["docCve"].Text;

                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_EXPRODoc";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, idocRegid);
                ProcBD.AgregarParametrosProcedimiento("@accId", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, sdocCve);
                ProcBD.AgregarParametrosProcedimiento("@docRegSitId", DbType.Int64, 0, ParameterDirection.Input, idocRegSit);
                ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                ProcBD.AgregarParametrosProcedimiento("@MuestraMgs", DbType.Int64, 0, ParameterDirection.Input, 1);

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                string sEjecEstatus = "", sEjecMSG = "", sClieMailI = "", sDocCve = "";
                int iGenCFDI = 0;

                if (FnValAdoNet.bDSIsFill(ds))
                {
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    if (sEjecEstatus == "1")
                    {
                        sDocCve = ds.Tables[0].Rows[0]["docCve"].ToString();
                        sClieMailI = ds.Tables[0].Rows[0]["clieMail"].ToString();
                        iGenCFDI = Convert.ToInt32(ds.Tables[0].Rows[0]["GenCFDI"]);

                        if (iGenCFDI == 1)
                        {
                            string pathToCFDI = Server.MapPath("~/Temp");
                            pathToCFDI = pathToCFDI + "\\";

                            string sResultSit = "", sResultDes = "";
                            DataSet dsResult;
                            dsResult = oWSTimbre.CFDRemote("A17092018", idocRegid.ToString());
                            if (FnValAdoNet.bDSRowsIsFill(dsResult))
                            {
                                sResultSit = dsResult.Tables[0].Rows[0]["resultSit"].ToString();
                                sResultDes = dsResult.Tables[0].Rows[0]["resultDes"].ToString();
                                if (sResultSit == "SUCCES")
                                {
                                    Byte[] xml = null;
                                    Byte[] pdf = null;
                                    Byte[] qr = null;

                                    qr = System.Text.Encoding.UTF8.GetBytes(dsResult.Tables[0].Rows[0]["QR"].ToString());
                                    xml = System.Text.Encoding.UTF8.GetBytes(dsResult.Tables[0].Rows[0]["XML"].ToString());
                                    //pdf = System.Text.Encoding.UTF8.GetBytes(dsResult.Tables[0].Rows[0]["PDF"].ToString());
                                    Documento_Timbrado(Pag_sConexionLog, Pag_sCompania, idocRegid, xml, pdf, qr);

                                    LlenaGridDocumentos();
                                    ShowAlert("1", sResultDes);
                                }
                                else
                                {
                                    Documento_Timbrado_Error(Pag_sConexionLog, Pag_sCompania, idocRegid, sResultDes, maUser);
                                    ShowAlert("2", sResultDes);
                                }
                            }
                            else
                            {
                                ShowAlert("2", "Comprobante no timbrado.");
                            }




                        }
                        else {
                            ShowAlert(sEjecEstatus, sEjecMSG);
                        }


                    }
                    else {
                        ShowAlert(sEjecEstatus, sEjecMSG);
                    }
                   
                }

                if (sEjecEstatus == "1")
                {
                    InicioPagina();
                }
                else
                {
                    LlenaGridDocumentos();
                }




            }
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

    } 

    private void EjecutaSpAccionCDFI() {


        

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

            foreach (GridDataItem i in rGdv_Documentos.SelectedItems)
            {

                var dataItem = rGdv_Documentos.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string docRegId = dataItem["docRegid"].Text;
                    //

                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, docRegId);
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
                    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                }
                else
                {

                    LlenaGridDocumentos();
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
                    LlenaGridDocumentos();

                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

        this.rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = false;
    }
    private void EjecutaSpAccionValidacion()
    {
   
        string maUser = LM.sValSess(this.Page, 1);
        int CountItems = 0;
        string smaMSGTip = "";
        string smaMSGDes = "";

        foreach (GridDataItem i in rGdv_Documentos.SelectedItems)
        {
            var dataItem = rGdv_Documentos.SelectedItems[CountItems] as GridDataItem;
            if (dataItem != null)
            {
                int folioId = Convert.ToInt32(dataItem["docRegid"].Text);
                int  iSitCve = Convert.ToInt32( dataItem["docRegSit"].Text);
                string sDocCve = dataItem["docCve"].Text;
                try
                {
                        DataSet ds = new DataSet();
                        ds = FNValida.dsEXPRODoc_ValidacionesProcesos(Pag_sConexionLog, Pag_sCompania, folioId, sDocCve, maUser, iSitCve);
                        if (FnValAdoNet.bDSIsFill(ds))
                        {
                            smaMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                            smaMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                        }
                }
                catch (Exception ex)
                {
                    string MsgError = ex.Message.Trim();
                }

                CountItems += 1;
            }
        }

        if (CountItems == 1)
        {
            ShowAlert(smaMSGTip, smaMSGDes);
        }
        else
        {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VALI000", ref smaMSGTip, ref smaMSGDes);
            ShowAlert(smaMSGTip, smaMSGDes);
        }

        LlenaGridDocumentos();
    }
    private void VisualizarDocumento() {
        customerOrders.Clear();
        VisualizarTipDocs.Clear();

        int CountItems = 0;
        foreach (GridDataItem i in rGdv_Documentos.SelectedItems)
        {

            var dataItem = rGdv_Documentos.SelectedItems[CountItems] as GridDataItem;


            string docRegCve = dataItem["docCve"].Text;


            DataRow[] filteredRows =
            (VisualizarTipDocs.Select("DocCve like '%" + docRegCve + "%'"));

            if (filteredRows.Length > 0)
            {
                // MessageBox.Show("ya se agrego");
            }
            else
            {
                DataRow rowTmp = VisualizarTipDocs.NewRow();
                rowTmp["DocCve"] = docRegCve;
                VisualizarTipDocs.Rows.Add(rowTmp);
            }




            CountItems += 1;
        }
        CountItems = 0;

        foreach (DataRow row in VisualizarTipDocs.Rows)
        {
 

            foreach (GridDataItem i in rGdv_Documentos.SelectedItems)

            {

                var dataItem = rGdv_Documentos.SelectedItems[CountItems] as GridDataItem;
                string docRegId = dataItem["docRegid"].Text;
                string docRegFolio = dataItem["docRegFolio"].Text;
                string docRegCve = dataItem["docCve"].Text;
                int    iTimbre = Convert.ToInt32( dataItem["Timbre"].Text);


                if (iTimbre == 1)
                {
              
                    string maUser = LM.sValSess(this.Page, 1);
                    string UrlXML = "Timbrado"  + docRegId  + ".xml";
                    string apppath = Server.MapPath("~/Temp") + "\\" + UrlXML;
                    doc.Comprobante_Paso(Pag_sConexionLog, Pag_sCompania, Convert.ToInt32(docRegId), apppath, maUser);
                    

                }



                if (row["DocCve"].ToString() == docRegCve)
                {
                    Filtro_Vis_Doc += docRegId;
                    idformdocform = docRegId;
                    Session["docCve"] = docRegCve;
                }

                if (contador != 0 && contador < rGdv_Documentos.SelectedItems.Count && row["DocCve"].ToString() == docRegCve)

                {
                    Filtro_Vis_Doc += ",";
                }




                if (contador == rGdv_Documentos.SelectedItems.Count)

                {

                    Session["Filtro_Vis_Doc"] = Filtro_Vis_Doc;




                    VisualizaDoc();


                    Filtro_Vis_Doc = "";

                }

                else

                {


                }





                contador++;

                CountItems++;



            }


            CountItems = 0;
            contador = 1;





            // Server.Transfer("/FR/DocsPdf.aspx");



        }
        string cadenotasurl = "";
        foreach (DataRow fila in customerOrders.Rows)
        {

            cadenotasurl += fila["urlDocs"].ToString() + "|";


        }
        ///eliminar
        ///


        Cdena_Url(cadenotasurl, customerOrders.Rows.Count);

    }

 


    private void ImprimirDocumento() {

        customerOrders.Clear();
        VisualizarTipDocs.Clear();
        int CountItems = 0;

        foreach (GridDataItem i in rGdv_Documentos.Items)
        {

            var dataItem = rGdv_Documentos.Items[CountItems] as GridDataItem;


            string docRegCve = dataItem["docCve"].Text;


            DataRow[] filteredRows =
            (VisualizarTipDocs.Select("DocCve like '%" + docRegCve + "%'"));

            if (filteredRows.Length > 0)
            {
                // MessageBox.Show("ya se agrego");
            }
            else
            {
                DataRow rowTmp = VisualizarTipDocs.NewRow();
                rowTmp["DocCve"] = docRegCve;
                VisualizarTipDocs.Rows.Add(rowTmp);
            }




            //MessageBox.Show(docRegCve.ToString());
            // VisualizaDoc();
            CountItems += 1;
        }
        CountItems = 0;

        foreach (DataRow row in VisualizarTipDocs.Rows)
        {



            foreach (GridDataItem i in rGdv_Documentos.Items)

            {

                var dataItem = rGdv_Documentos.Items[CountItems] as GridDataItem;
                string docRegId = dataItem["docRegid"].Text;
                string docRegFolio = dataItem["docRegFolio"].Text;
                string docRegCve = dataItem["docCve"].Text;
                int iTimbre = Convert.ToInt32(dataItem["Timbre"].Text);

                if (iTimbre == 1)
                {

                    string maUser = LM.sValSess(this.Page, 1);
                    string UrlXML = "Timbrado" + docRegId + ".xml";
                    string apppath = Server.MapPath("~/Temp") + "\\" + UrlXML;
                    doc.Comprobante_Paso(Pag_sConexionLog, Pag_sCompania, Convert.ToInt32(docRegId), apppath, maUser);
                }


                if (row["DocCve"].ToString() == docRegCve)

                {
                    Filtro_Vis_Doc += docRegId;
                    idformdocform = docRegId;
                    Session["docCve"] = docRegCve;
                }

                if (contador != 0 && contador < rGdv_Documentos.Items.Count && row["DocCve"].ToString() == docRegCve)

                {

                    Filtro_Vis_Doc += ",";

                }




                if (contador == rGdv_Documentos.Items.Count)

                {

                    Session["Filtro_Vis_Doc"] = Filtro_Vis_Doc;




                    VisualizaDoc();


                    Filtro_Vis_Doc = "";

                }

                else

                {


                }





                contador++;

                CountItems++;



            }


            CountItems = 0;
            contador = 1;





            // Server.Transfer("/FR/DocsPdf.aspx");



        }
        string cadenotasurl = "";
        foreach (DataRow fila in customerOrders.Rows)
        {

            cadenotasurl += fila["urlDocs"].ToString() + "|";


        }
        ///eliminar
        ///


        Cdena_Url(cadenotasurl, customerOrders.Rows.Count);


    }

    private void EjecutaCDFTimbre() {
        try
        {
            string maUser = LM.sValSess(this.Page, 1);
            var dataItem = this.rGdv_Documentos.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                string pathToCFDI = Server.MapPath("~/Temp");
                pathToCFDI = pathToCFDI + "\\";
                Int32 idocRegid = Convert.ToInt32(dataItem["docRegid"].Text);
                string sDocCve = Convert.ToString(dataItem["docCve"].Text);

                string sResultSit = "", sResultDes = "";
                DataSet dsResult;
                dsResult = oWSTimbre.CFDRemote("A17092018", idocRegid.ToString());
                if (FnValAdoNet.bDSRowsIsFill(dsResult))
                {
                    sResultSit = dsResult.Tables[0].Rows[0]["resultSit"].ToString();
                    sResultDes = dsResult.Tables[0].Rows[0]["resultDes"].ToString();
                    if (sResultSit == "SUCCES")
                    {
                        Byte[] xml = null;  
                        Byte[] pdf = null;
                        Byte[] qr = null;

                        qr = System.Text.Encoding.UTF8.GetBytes(dsResult.Tables[0].Rows[0]["QR"].ToString());
                        xml = System.Text.Encoding.UTF8.GetBytes(dsResult.Tables[0].Rows[0]["XML"].ToString());
                        //pdf = System.Text.Encoding.UTF8.GetBytes(dsResult.Tables[0].Rows[0]["PDF"].ToString());
                        Documento_Timbrado(Pag_sConexionLog, Pag_sCompania, idocRegid, xml, pdf, qr);

                        LlenaGridDocumentos();
                        ShowAlert("1", sResultDes);
                    }
                    else
                    {
                        Documento_Timbrado_Error(Pag_sConexionLog, Pag_sCompania, idocRegid, sResultDes, maUser);
                        ShowAlert("2", sResultDes);
                    }
                }
                else
                {
                    ShowAlert("2", "Comprobante no timbrado.");
                }
     
            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }



    private void BotonesProcesosFlujo(bool visible, Int32 IdocRegid = 0, Int32 iDocRegSit = 0, string scveDoc = "")
    {
        if (visible == false)
        {

            rBtnModificar.Enabled = visible;
            rBtnEliminar.Enabled = visible;
            rBtnAutorizar.Enabled = visible;
            rBtnProcesar.Enabled = visible;
            rBtnCancelarSit.Enabled = visible;
            rBtnValidacion.Enabled = visible;
        }
        else
        {

 
            rBtnModificar.Enabled = false;
            rBtnEliminar.Enabled = false;
            rBtnAutorizar.Enabled = false;
            rBtnProcesar.Enabled = false;
            rBtnCancelarSit.Enabled = false;
            rBtnValidacion.Enabled = false;

            DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_DocumentoProcesosEjecucion";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                ProcBD.AgregarParametrosProcedimiento("@docRegid", DbType.Int64, 0, ParameterDirection.Input, IdocRegid);
            
                ProcBD.AgregarParametrosProcedimiento("@docProcID", DbType.Int64, 0, ParameterDirection.Input, iDocRegSit);
                ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, scveDoc);
                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    string valor = dr["docProcOpcBtn"].ToString();
                    if (valor == "rBtnModificar")
                    {
                        rBtnModificar.Enabled = true;
                    }
                    if (valor == "rBtnEliminar")
                    {
                        rBtnEliminar.Enabled = true;
                    }

                    if (valor == "rBtnAutorizar")
                    {
                        rBtnAutorizar.Enabled = true;
                    }

                    if (valor == "rBtnProcesar")
                    {
                        rBtnProcesar.Enabled = true;
                    }

                    if (valor == "rBtnCancelarSit")
                    {
                        rBtnCancelarSit.Enabled = true;
                    }

                    if (valor == "rBtnValidacion")
                    {
                        rBtnValidacion.Enabled = true;
                    }
            }


        }


    }
    private void BotonesProcesosPermisos(string scveDoc) {
        DataSet ds = new DataSet(); 
        string maUser = LM.sValSess(this.Page, 1);

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SeguridadDocumentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, scveDoc);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 10, ParameterDirection.Input, maUser);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            Boolean bSegDocReg = Convert.ToBoolean(ds.Tables[0].Rows[0]["segDocReg"]);
            //rBtnNuevo.Visible = bSegDocReg;
            rBtnModificar.Visible = bSegDocReg;
            rBtnEliminar.Visible = bSegDocReg;

            Boolean bSegDocAut = Convert.ToBoolean(ds.Tables[0].Rows[0]["segDocAut"]);
            rBtnAutorizar.Visible = bSegDocAut;

            Boolean bSegDocProc = Convert.ToBoolean(ds.Tables[0].Rows[0]["segDocProc"]);
            rBtnProcesar.Visible = bSegDocProc;

            Boolean bSegDocCanc = Convert.ToBoolean(ds.Tables[0].Rows[0]["segDocCanc"]);
            rBtnCancelarSit.Visible = bSegDocCanc;

            Boolean bSegDocVal = Convert.ToBoolean(ds.Tables[0].Rows[0]["segDocVal"]);
            rBtnValidacion.Visible = bSegDocVal;
        }
        else {
            BotonesProcesosVisible(false);
        }
         
            
    }
    private void BotonesProcesosVisible( Boolean visible) {
        //rBtnNuevo.Visible = visible;
        rBtnModificar.Visible = visible;
        rBtnEliminar.Visible = visible;

        rBtnAutorizar.Visible = visible;
        rBtnProcesar.Visible = visible;
        rBtnCancelarSit.Visible = visible;
        rBtnValidacion.Visible = visible;
    }
    private void LlenaGridDocumentos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_Documentos, ds);
    }
    private void LlenaGridDocumentosFiltros()
    {
        if (Val_Cliente == "")
        {

        }
        Val_Cliente = rCboClientes.SelectedValue;
        Val_Documento = rCboDocumento.SelectedValue;

        if (rCboSituacion.SelectedIndex != -1)
        {
            Val_Situacion = Convert.ToInt64(rCboSituacion.SelectedValue);
        }else
        {
            Val_Situacion = 0;
        }



        Val_Fec_Inicio = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Inicio.SelectedDate);
        Val_Fec_Inicio = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        Val_Subcliente = "";
        Val_Subcliente = rCboClientes.SelectedValue + rCboSubClientes.SelectedValue;

        try
        {
            

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, Val_Documento);
            ProcBD.AgregarParametrosProcedimiento("@docRegSit", DbType.Int64, 0, ParameterDirection.Input, Val_Situacion);
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, Val_Subcliente);
            ProcBD.AgregarParametrosProcedimiento("@SubClieCve", DbType.String, 20, ParameterDirection.Input, Val_Subcliente);


          
            if (Val_Fec_Inicio != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@docRegFec1", DbType.String, 100, ParameterDirection.Input, Val_Fec_Inicio + " 00:00:00");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docRegFec1", DbType.String, 100, ParameterDirection.Input, "1900-01-01 00:00:00");
            }

          
            if (Val_Fec_Fin != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@docRegFec2", DbType.String, 100, ParameterDirection.Input, Val_Fec_Fin + " 00:00:00");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docRegFec2", DbType.String, 100, ParameterDirection.Input, "2070-12-31 00:00:00");
            }

     


            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            FnCtlsFillIn.RadGrid(ref rGdv_Documentos, ds);


        }
        catch (Exception ex)
        {

 
        }


        Val_Documento = "";
        Val_Situacion = 0;
        Val_Cliente = "";
        Val_Subcliente = "";
        Val_Fec_Inicio = "1900-01-01";
        Val_Fec_Fin = "2070-12-31";



    }
    private void llenaFiltros()
    {
        if (FNGrales.bManejoSubCliente(Pag_sConexionLog, Pag_sCompania))
        {
            rLblSubClie.Visible = true;
            rCboSubClientes.Visible = true;
        }
        else
        {
            rLblSubClie.Visible = false;
            rCboSubClientes.Visible = false;
        }

        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento, true, false, "");
        FnCtlsFillIn.RadComboBox_SituacionDocumento(Pag_sConexionLog, ref rCboSituacion, true, false, "");
        ClientesCombo();
    }
    public void ClientesCombo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboClientes, ds, "cliCveClie", "clieNom", true, false, "");
        ((Literal)rCboClientes.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboClientes.Items.Count);

    }
    public void SubClientesCombo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cliCveClie", DbType.String, 20, ParameterDirection.Input, rCboClientes.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboSubClientes, ds, "cliCveSubClie", "clieNom", true, false, "");
        ((Literal)rCboSubClientes.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboSubClientes.Items.Count);
        rCboSubClientes.Enabled = true;

    }
    public void RastrearDocumentoBnt()
    {
        //btnActual.Visible = true;
        string docRegidActual = "", ClaveActual = "", foliActualo = "", fechaActual = "";
        //var dataItem = this.rGdv_Documentos.SelectedItems[0] as GridDataItem;

        //btnActual.Title.Text = dataItem["docCve"].Text;
        docRegidActual = btnIdActual.Value;


        //dataset traer datos del documento actual
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 56);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docRegid", DbType.Int64, 0, ParameterDirection.Input, btnIdActual.Value);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        DataList3.DataSource = ds;
        DataList3.DataBind();



        ClaveActual = ds.Tables[0].Rows[0]["docCve"].ToString();
        foliActualo = ds.Tables[0].Rows[0]["docRegFolio"].ToString();
        fechaActual = ds.Tables[0].Rows[0]["docRegFec"].ToString();
        btnIdActual.Value = ds.Tables[0].Rows[0]["docRegId"].ToString();


        //dataset traer datos del documento derivado
        DataSet ds1 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_DocumentoRegistro";
        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 57);
        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD1.AgregarParametrosProcedimiento("@docRegid", DbType.Int64, 0, ParameterDirection.Input, btnIdActual.Value);
        ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        DataList1.DataSource = ds1;
        DataList1.DataBind();


        //dataset traer datos del documento padre
        DataSet ds2 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD2 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD2.NombreProcedimiento = "sp_DocumentoRegistro";
        ProcBD2.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 58);
        ProcBD2.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD2.AgregarParametrosProcedimiento("@docRegid", DbType.Int64, 0, ParameterDirection.Input, btnIdActual.Value);
        ds2 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD2.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        DataList2.DataSource = ds2;
        DataList2.DataBind();

    }
    public void RastreaDocumento()
    {
        // btnRastreo.Visible = true;
        string docRegidActual = "", ClaveActual = "", foliActualo = "", fechaActual = "";
        var dataItem = this.rGdv_Documentos.SelectedItems[0] as GridDataItem;

        // radLb_Nombre_Actual.Text = dataItem["docCve"].Text;
        docRegidActual = dataItem["docRegid"].Text;


        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoRegistro";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 56);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docRegid", DbType.Int64, 0, ParameterDirection.Input, docRegidActual);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        DataList3.DataSource = ds;
        DataList3.DataBind();

        ClaveActual = ds.Tables[0].Rows[0]["docCve"].ToString();
        foliActualo = ds.Tables[0].Rows[0]["docRegFolio"].ToString();
        fechaActual = ds.Tables[0].Rows[0]["docRegFec"].ToString();
        btnIdActual.Value = ds.Tables[0].Rows[0]["docRegId"].ToString();

        DataSet ds1 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_DocumentoRegistro";
        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 57);
        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD1.AgregarParametrosProcedimiento("@docRegid", DbType.Int64, 0, ParameterDirection.Input, docRegidActual);
        ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        DataList1.DataSource = ds1;
        DataList1.RepeatColumns = 3;
        DataList1.RepeatDirection = RepeatDirection.Horizontal;
        DataList1.DataBind();


        DataSet ds2 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD2 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD2.NombreProcedimiento = "sp_DocumentoRegistro";
        ProcBD2.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 58);
        ProcBD2.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD2.AgregarParametrosProcedimiento("@docRegid", DbType.Int64, 0, ParameterDirection.Input, docRegidActual);
        ds2 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD2.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        DataList2.DataSource = ds2;
        DataList2.DataBind();



    }


    private Boolean GeneraCFDI(int idocRegid, string TipoDoc, string sClieMailI,ref DataTable dtLogProc) {

        try
        {
            Boolean Result = false;

           


            string fileNameX = "";
        string pathToSaveTxt = "";
        string pathToSavePdf = "";
        string pathToSaveXml = "";

        string apppathClass = MapPath("~");
        apppathClass += "Temp\\";

        fileNameX = "MGMDOC" + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
            "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();

        pathToSaveTxt = apppathClass + fileNameX + ".txt";
        pathToSavePdf = apppathClass + fileNameX + ".pdf";
        pathToSaveXml = apppathClass + fileNameX + ".xml";

            string maUser = LM.sValSess(this.Page, 1);
            csStamps stampDoc = new csStamps();
        Result = stampDoc.stampDoc(idocRegid.ToString(), TipoDoc, Pag_sCompania, Pag_sConexionLog, sClieMailI, pathToSaveTxt, pathToSavePdf, pathToSaveXml, ref dtLogProc,Convert.ToInt32(docCmpTrnId), maUser);

        return Result;

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
            return false;
        }

    }
    private void setSitTrnCmlDoc(int opc, int sit)
    {

        try
        {
            DataSet dsP = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_DocumentoComprobanteTransacciones";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, Pag_sUsrCve);

            ProcBD.AgregarParametrosProcedimiento("@docCmpTrnSit", DbType.Int64, 0, ParameterDirection.Input, sit);

            dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            if (dsP != null)
            {

            }


        }
        catch (Exception)
        {

            throw;
        }


    }
    private string chkSitTrnTmbUsr()
    {

        string response = "";

        try
        {

            DataSet dsP = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();

            ProcBD.NombreProcedimiento = "sp_DocumentoComprobanteTransacciones";

            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, Pag_sUsrCve);

            dsP = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (dsP.Tables.Count > 0)
            {
                if (dsP.Tables[0].Rows.Count > 0)
                {
                    response = "1";
                    docCmpTrnId = Convert.ToInt64(dsP.Tables[0].Rows[0]["docCmpTrnId"].ToString());
                }
                else
                {

                    response = "0";
                    docCmpTrnId = 0;

                }

            }
            else
            {
                response = "0";
            }


        }
        catch (Exception ex)
        {
            response = "0";
            throw;
        }


        return response;

    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    private bool compararFechas()
    {

        if (RdDateFecha_Inicio.SelectedDate > RdDateFecha_Final.SelectedDate)
        {           
            return false;
        }
        else if (RdDateFecha_Final.SelectedDate < RdDateFecha_Inicio.SelectedDate)
        {
            return false;
        }else
        {
            return true;
        }

    }
    private void VisualizaDoc()
    {

 
        string UrlPdf = "";
        string fil = Session["Filtro_Vis_Doc"].ToString();
        string doc = Session["docCve"].ToString();
        string Uno = "";
        string dos = "";

        string maUser = LM.sValSess(this.Page, 1);




        DataSet dsDatosGrales = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptDataSetDocumento";
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, idformdocform);
        dsDatosGrales = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        DataSet dsDatos = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_RptDataSetDoc";

        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        if (Pag_sIdDocReg != "")
        {
            ProcBD1.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, Pag_sIdDocReg);
        }
        ProcBD1.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, doc);
        ProcBD1.AgregarParametrosProcedimiento("@fltrDocRegIds", DbType.String, 2048, ParameterDirection.Input, fil);
        ProcBD1.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 10, ParameterDirection.Input, maUser);
        dsDatos = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);



        byte[] bytes = oWSRpt.byte_FormatoDoc(dsDatosGrales, dsDatos);



        if (customerOrders.Columns.Count == 0)
        {
            customerOrders.Clear();
            customerOrders.Columns.Add("urlDocs");
        }





        DataRow rowTmp = customerOrders.NewRow();

        UrlPdf = doc + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
        "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";

        rowTmp["urlDocs"] = UrlPdf;

        customerOrders.Rows.Add(rowTmp);


        string apppath = Server.MapPath("~/Temp") + "\\" + UrlPdf;
        System.IO.File.WriteAllBytes(apppath, bytes);





         


    }
    public void Cdena_Url(string UrlPdf, int celdas)
    {
        string script = @"<script type='text/javascript'> openRadWin('" + UrlPdf + "','" + celdas + "'); </script>";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);
    }
    private string sObtenerCorreoClienteContactoPrincipal( string cliCve) {
        string sCorreo = "";

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptDataSetDocumento";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 10, ParameterDirection.Input, cliCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds)) {

        }

        return sCorreo; 
    }

    private Boolean opccionSituacion()
    {
        Boolean bResult = false;


        var dataItem = this.rGdv_Documentos.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {

            string sSit = dataItem["docRegSit"].Text;
            if ("1" == sSit)
            {
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
                    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() ||
                    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString() ||
                    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Autorizado).ToString())
                {
                    dataItem.Selected = true;
                    return true;
                }
                else
                {
                    dataItem.Selected = false;
                    return false;
                }
            }

            if ("2" == sSit)
            {
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Procesado).ToString() ||
                    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
                    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() ||
                    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString()
                    )
                {
                    dataItem.Selected = true;
                    return true;
                }
                else
                {
                    dataItem.Selected = false;
                    return false;
                }
            }


            if ("3" == sSit)
            {
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Cancelado).ToString())
                {
                    dataItem.Selected = true;
                    return true;
                }
                else
                {
                    dataItem.Selected = false;
                    return false;
                }
            }

            if ("4" == sSit)
            {
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Cancelado).ToString())
                {
                    dataItem.Selected = true;
                    return true;
                }
                else
                {
                    dataItem.Selected = false;
                    return false;
                }
            }

            if ("9" == sSit)
            {
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
                {
                    dataItem.Selected = true;
                    return true;
                }
                else
                {
                    dataItem.Selected = false;
                    return false;
                }
            }



        }


        return bResult;
    }
    private Boolean bBotonesProcesosPermisos(string scveDoc)
    {
        Boolean bResult = true;

        DataSet ds = new DataSet();
        string maUser = LM.sValSess(this.Page, 1);

        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_SeguridadDocumentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, scveDoc);
        ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 10, ParameterDirection.Input, maUser);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {

                bResult = Convert.ToBoolean(ds.Tables[0].Rows[0]["segDocReg"]);
                //rBtnNuevo.Visible = bResult;
                //rBtnModificar.Visible = bResult;
                //rBtnEliminar.Visible = bResult;
                return bResult;
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Autorizado).ToString())
            {
                bResult = Convert.ToBoolean(ds.Tables[0].Rows[0]["segDocAut"]);
                //rBtnAutorizar.Visible = bResult;
                return bResult;
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Procesado).ToString())
            {
                bResult = Convert.ToBoolean(ds.Tables[0].Rows[0]["segDocProc"]);
                //rBtnProcesar.Visible = bSegDocProc;
                return bResult;
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Cancelado).ToString())
            {
                bResult = Convert.ToBoolean(ds.Tables[0].Rows[0]["segDocCanc"]);
                //rBtnCancelarSit.Visible = bResult;
                return bResult;
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString())
            {
                bResult = Convert.ToBoolean(ds.Tables[0].Rows[0]["segDocVal"]);
                //rBtnValidacion.Visible = bResult;
                return bResult;
            }


        }

        return bResult;
    }
    private void opc1_seleeciongrid()
    {


        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        {


            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Visualizar).ToString())
            {
                var dataItem1 = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                Session["Valor_DocCve"] = dataItem1["docRegid"].Text;
            }
            else
            {

                RastreaDocumento();

                hdfBtnAccion.Value = "";
                ControlesAccion();

                var dataItem = this.rGdv_Documentos.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    string sdocRegSit = dataItem["docRegSit"].Text;
                    Int32 idocRegSit = Convert.ToInt32(sdocRegSit);
                    string sdocCve = dataItem["docCve"].Text;
                    Int32 docRegid = Convert.ToInt32(dataItem["docRegid"].Text);


                    BotonesProcesosPermisos(sdocCve);
                    BotonesProcesosFlujo(true, docRegid, idocRegSit, sdocCve);

                }
            }

        }


    }

    private void Documento_Timbrado(string sPag_sConexionLog, string sCiaCve, int iDocRegId, Byte[] byteXML, Byte[] bytePDF, Byte[] byteQR)
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentoCFDI";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, iDocRegId);
            if (byteXML != null)
            {
                string base64StringXML = Convert.ToBase64String(byteXML, 0, byteXML.Length);
                ProcBD.AgregarParametrosProcedimiento("@docCFDIXML", DbType.Binary, 0, ParameterDirection.Input, base64StringXML);
            }
            if (bytePDF != null)
            {
                string base64StringPDF = Convert.ToBase64String(bytePDF, 0, bytePDF.Length);
                ProcBD.AgregarParametrosProcedimiento("@docCFDIPDF", DbType.Binary, 0, ParameterDirection.Input, base64StringPDF);
            }
            if (byteQR != null)
            {
                string base64StringQR = Convert.ToBase64String(byteQR, 0, byteQR.Length);
                ProcBD.AgregarParametrosProcedimiento("@docCFDIQR", DbType.Binary, 0, ParameterDirection.Input, base64StringQR);
            }

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }

    private void Documento_Timbrado_Error(string sPag_sConexionLog, string sCiaCve, int iDocRegId, string sErrorDesc, string maUsuCve)
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_SATCFDI_Documento_Error";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
            ProcBD.AgregarParametrosProcedimiento("@valiProcCve", DbType.String, 10, ParameterDirection.Input, "DOC");
            ProcBD.AgregarParametrosProcedimiento("@valiCve", DbType.String, 15, ParameterDirection.Input, "DOC_TIMBRE");
            ProcBD.AgregarParametrosProcedimiento("@docRegId", DbType.Int64, 0, ParameterDirection.Input, iDocRegId);
            ProcBD.AgregarParametrosProcedimiento("@maMSGTip", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@maMSGDes", DbType.String, 200, ParameterDirection.Input, sErrorDesc);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUsuCve);
            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }
    #endregion

    #region FUNCIONES
    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";



        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdv_Documentos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdv_Documentos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        //VER ERRORES
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        {
            if (rGdv_Documentos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        //VIZUALIZAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Visualizar).ToString())
        {

            if (rGdv_Documentos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        //CFDI TIMBRE
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.CFDITimbre).ToString())
        {
            if (rGdv_Documentos.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }



            return sResult;
    }
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdv_Documentos.SelectedItems.Count;
        string[] GvVAS;
  
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesDoc(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesDoc(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //VALIDAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesDoc(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //VER ERRORES
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesDoc(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //AUTORIZAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Autorizado).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008"};
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesDoc(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //PROCESAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Procesado).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008"};
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesDoc(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //CANCELAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Cancelado).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008"};
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesDoc(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //VISUALIZAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Visualizar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //COPIAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Copiar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }


        //CFDI TIMBRE
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.CFDITimbre).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesDoc(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //IMPRIMIR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Imprimir).ToString())
        {
        }




        return sResult;
    }
    private Boolean ValidaAccionesSituacionesDoc(ref string sMSGTip, ref string sResult, int iDocRegSit = 0)
    {
        Boolean  bResult = true;
        int CountItems = 0;
        string sDocRegFolios = "";

        int iAccId = Convert.ToInt32(hdfBtnAccion.Value);

        if (iDocRegSit == 0)
        {

            foreach (GridDataItem i in rGdv_Documentos.SelectedItems)
            {
                var dataItem = rGdv_Documentos.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {
                    sDocRegFolios = "";
                    iDocRegSit = Convert.ToInt32(dataItem["docRegSit"].Text);
                    
                    if (FNValida.bAcciones_ValidaAccionesSituacionesDoc(Pag_sConexionLog, iAccId, iDocRegSit, ref sMSGTip, ref sResult) == false)
                    {
                        sDocRegFolios = dataItem["docRegFolio"].Text;
                        sResult += " Folio: " + sDocRegFolios;
                        return false;
                    }

                }

                CountItems += 1;
            }



        }
        else {
            if (FNValida.bAcciones_ValidaAccionesSituacionesDoc(Pag_sConexionLog, iAccId, iDocRegSit, ref sMSGTip, ref sResult) == false)
            {
                return false;
            }
        }

        return bResult;
    }

    #endregion
}