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


public partial class CC_AdmonCobros : System.Web.UI.Page
{

    #region VARIABLES

    ws.Servicio oWS = new ws.Servicio();
    wsRpt.Service oWSRpt = new wsRpt.Service();

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();
    MGMControls.FillIn FnCtlsFillIn = new MGMControls.FillIn();
    MGMControls.RadWindows FnCtrlsRadWindows = new MGMControls.RadWindows();
    MGMFnGrales.FNGrales FNGrales = new MGMFnGrales.FNGrales();
    MGMFnGrales.ObjAdoNet FnValAdoNet = new MGMFnGrales.ObjAdoNet();
    MGMFnGrales.FNDatos FNDatos = new MGMFnGrales.FNDatos();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
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

        Session["Valor_btn"] = "1";
        Int64 Pag_sidM = 0;
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
        }
        Response.Redirect("~/CC/AdmonCobrosRegistro.aspx?idM=" + Pag_sidM);
    }

    protected void rBtnModificar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
    }

    protected void rBtnEliminar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
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

    protected void rBtnGenera_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString();
        ControlesAccion();
    }

    protected void rBtnAplica_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString();
        ControlesAccion();
    }

    protected void rBtnPolizaeEdit_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString();
        ControlesAccion();
    }

    protected void rBtnPolizaImp_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString();
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        //InicioPagina();
        cleanUi();
    }

    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rBtnCancelar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    protected void RdDateFecha_Final_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {

        if (compararFechas() == true)
        {
            loadGridFltr();
        }
        else
        {
            RdDateFecha_Final.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Final no puede ser Menor a la Inical", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");

        }
    }

    protected void RdDateFecha_Inicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {

        if (compararFechas() == true)
        {
            loadGridFltr();
        }
        else
        {
            RdDateFecha_Inicio.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Inicial no puede ser mayor a la Final", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
        }
    }

    protected void rCboCuenta_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        loadGridFltr();
    }
    //Se agrega Evento para combo de Descripción
    protected void rCboDescripcion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        loadGridFltr();
    }

    protected void rTxtBeneficiario_TextChanged(object sender, EventArgs e)
    {
        loadGridFltr();
    }

    protected void rCboSituacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        loadGridFltr();
    }

    protected void rCboConcepto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        loadGridFltr();
    }

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        {
            rGdvOperaciones.MasterTableView.ClearSelectedItems();
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
    public void InicioPagina()
    {
        Session["RawUrl_Return"] = "";
        hdfRawUrl.Value = this.Page.Request.RawUrl.ToString();
        hdfBtnAccion.Value = "";
        FnCtlsFillIn.RadComboBox_Situaciones(Pag_sConexionLog, Pag_sCompania, 1, ref rCboSituacion, true, false, "");
        RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, ref rCboConcepto, true, false);

        RadComboBox_CuentasDeposito(Pag_sConexionLog, Pag_sCompania, ref rCboCuenta, true, false);
		//Se invoca metodo para llenado de combo descripción
        RadComboBox_Descripcion(Pag_sConexionLog, Pag_sCompania, 50, rCboDescripcion.SelectedValue, ref rCboDescripcion, true, false, "");
        RdDateFecha_Inicio.SelectedDate = null;
        RdDateFecha_Final.SelectedDate = null;
        ControlesAccion();
        LlenaGridCobros();

        FNGrales.bTitleDesc(Page, "Administración de Cobros", "PnlMPFormTituloApartado");
        rGdvOperaciones.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvOperaciones.AllowMultiRowSelection = true;
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
    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rGdvOperaciones
        this.rGdvOperaciones.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        //rTxtCve.CssClass = "cssTxtEnabled";
        //rTxtDes.CssClass = "cssTxtEnabled";
        //rTxtAbr.CssClass = "cssTxtEnabled";

        //this.rTxtCve.Enabled = false;
        //this.rTxtDes.Enabled = false;
        //this.rTxtAbr.Enabled = false;

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
            //this.rTxtCve.Enabled = false;
            //this.rTxtDes.Enabled = false;
            //this.rTxtAbr.Enabled = false;

            this.rCboConcepto.ClearSelection();
            this.rCboCuenta.ClearSelection();
            this.RdDateFecha_Inicio.Clear();
            this.RdDateFecha_Final.Clear();
            this.rCboSituacion.ClearSelection();
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
                this.rGdvOperaciones.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvOperaciones.MasterTableView.ClearSelectedItems();

                //this.rTxtCve.Enabled = true;
                //this.rTxtDes.Enabled = true;
                //this.rTxtAbr.Enabled = true;

                //this.rTxtCve.Text = "";
                //this.rTxtDes.Text = "";
                //this.rTxtAbr.Text = "";

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvOperaciones.AllowMultiRowSelection = false;

                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    //Session["Valor_DocCve"] = dataItem["docRegid"].Text;
                    Session["Valor_DocCve"] = dataItem.GetDataKeyValue("cobCCId").ToString();
                }
                Int64 Pag_sidM = 0;
                if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                {
                    Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                }
                Session["Valor_btn"] = "2";
                Response.Redirect("~/CC/AdmonCobrosRegistro.aspx?idM=" + Pag_sidM);
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvOperaciones.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvOperaciones.AllowMultiRowSelection = true;
                rGdvOperaciones.MasterTableView.ClearSelectedItems();

                //this.rCboConcepto.Enabled = false;
                //this.rCboCuenta.Enabled = false;
                //this.RdDateFecha_Inicio.Enabled = false;
                //this.RdDateFecha_Final.Enabled = false;
                //this.rCboSituacion.Enabled = false;

                this.rCboConcepto.ClearSelection();
                this.rCboCuenta.ClearSelection();
                this.RdDateFecha_Inicio.Clear();
                this.RdDateFecha_Final.Clear();
                this.rCboSituacion.ClearSelection();
            }
            //VALIDACION
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString())
            {
                EjecutaSpAccionValidacion();
                InicioPagina();
            }

            //VER ERRORES
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
            {
                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem.GetDataKeyValue("cobCCId").ToString();

                RadWindowVerErrores.NavigateUrl = "../DC/VerErrores.aspx?ErrorId=" + stransDetId + "&ValCmb=" + "CC" + "&ProCve=" + "COBROS";
                string script = "function f(){$find(\"" + RadWindowVerErrores.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }


            //GENERA / APLICA  
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString()
                || hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString()
                )
            {
                EjecutaSpAccion();
                //InicioPagina();
            }


            //Edita Poliza
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString())
            {
                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                Session["RawUrl_Return"] = hdfRawUrl.Value;

                string sAsiContEncId = dataItem["asiContEncId"].Text;
                Response.Redirect("~/CG/Poliza.aspx?AC=" + sAsiContEncId + "&REF=" + "CC" + "&cptoTip=AdmonCC");

                //string stransDetId = dataItem["transId"].Text;
                //Response.Redirect("~/DC/MttoAsientosContables.aspx?stransDetId=" + stransDetId + "&cptoTip=AdmonCC");
            }


            //IMPRIME POLIZA
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString())
            {
                impPolizas();
            }







        }


        if (Result == false)
        {
            //this.rTxtCve.Enabled = false;
            //this.rTxtDes.Enabled = false;
            //this.rTxtAbr.Enabled = false;

            this.rCboConcepto.ClearSelection();
            this.rCboCuenta.ClearSelection();
            this.RdDateFecha_Inicio.Clear();
            this.RdDateFecha_Final.Clear();
            this.rCboSituacion.ClearSelection();
        }


    }
    private void cleanUi()
    {
        rCboConcepto.ClearSelection();
        rCboCuenta.ClearSelection();
		//Se agrega comboDescripción para metodo limpiar
        rCboDescripcion.ClearSelection();
        RdDateFecha_Final.Clear();
        RdDateFecha_Inicio.Clear();
        rCboSituacion.ClearSelection();
        LlenaGridCobros();
    }
    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);

        if (msgValidacion == "")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                Session["Valor_btn"] = "1";
                Int64 Pag_sidM = 0;
                if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                {
                    Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                }
                Response.Redirect("~/CC/AdmonCobrosRegistro.aspx?idM=" + Pag_sidM);
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    //Session["Valor_DocCve"] = dataItem["docRegid"].Text;
                    Session["Valor_DocCve"] = dataItem.GetDataKeyValue("cobCCId").ToString();
                }
                Session["Valor_btn"] = "2";
                Int64 Pag_sidM = 0;
                if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                {
                    Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                }
                Response.Redirect("~/CC/AdmonCobrosRegistro.aspx?idM=" + Pag_sidM);

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
                InicioPagina();
            }

            //VER ERRORES
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
            {
                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem.GetDataKeyValue("cobCCId").ToString();

                RadWindowVerErrores.NavigateUrl = "../DC/VerErrores.aspx?ErrorId=" + stransDetId + "&ValCmb=" + "CC" + "&ProCve=" + "COBROS";
                string script = "function f(){$find(\"" + RadWindowVerErrores.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }


            //GENERA / APLICA  
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString()
                //|| hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString()
                )
            {
                EjecutaSpAccion();
                //InicioPagina();
            }


            //Edita Poliza
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString())
            {
                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem["transId"].Text;
                Response.Redirect("~/DC/MttoAsientosContables.aspx?stransDetId=" + stransDetId + "&cptoTip=AdmonCC");
            }


            //IMPRIME POLIZA
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString())
            {
                impPolizas();
            }

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }

    }
    private void EjecutaSpAccion() {

        try
        {
            int CountItems = 0;
            int CountItems_Sit01 = 0;
            int CountItems_Sit02 = 0;
            int CountItems_Sit00 = 0;

            string smaMSGTip = "";
            string smaMSGDes = "";


            foreach (GridDataItem i in rGdvOperaciones.SelectedItems)
            {

                var dataItem = this.rGdvOperaciones.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {
                    //
                    Int32 RegId = Convert.ToInt32(dataItem.GetDataKeyValue("cobCCId").ToString());
                    string sValiProcCve = "CCCOBROS";
                    string sitCve = dataItem["cobCCSit"].Text;
                    string maUser = LM.sValSess(this.Page, 1);
                    int icptoId = Convert.ToInt16(dataItem["cptoId"].Text);

                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_EXPROAdmon";
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, RegId);
                    ProcBD.AgregarParametrosProcedimiento("@valiProcCve", DbType.String, 10, ParameterDirection.Input, sValiProcCve);
                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                    ProcBD.AgregarParametrosProcedimiento("@SitCve", DbType.String, 3, ParameterDirection.Input, sitCve);
                    ProcBD.AgregarParametrosProcedimiento("@acciId", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value.ToString());
                    ProcBD.AgregarParametrosProcedimiento("@modulo", DbType.String, 10, ParameterDirection.Input, "CC");
                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString())
                    {
                        ProcBD.AgregarParametrosProcedimiento("@polCve", DbType.String, 10, ParameterDirection.Input, sFolioPoliza(icptoId));
                    }

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (FnValAdoNet.bDSIsFill(ds))
                    {
                        smaMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                        smaMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                        if (smaMSGTip == "0")
                        {
                            CountItems_Sit00 += 1;
                        }
                        if (smaMSGTip == "1")
                        {
                            CountItems_Sit01 += 1;
                        }
                        if (smaMSGTip == "2")
                        {
                            CountItems_Sit02 += 1;
                        }
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
                FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0000", ref smaMSGTip, ref smaMSGDes);

                if (CountItems_Sit01 > 0)
                {
                    smaMSGDes = smaMSGDes + "<br /> Correctos: " + Convert.ToString(CountItems_Sit01);
                }
                if (CountItems_Sit02 > 0)
                {
                    smaMSGDes = smaMSGDes + "<br /> Errores: " + Convert.ToString(CountItems_Sit02);
                }
                if (CountItems_Sit00 > 0)
                {
                    smaMSGDes = smaMSGDes + "<br /> No validas: " + Convert.ToString(CountItems_Sit00);
                }
                ShowAlert(smaMSGTip, smaMSGDes);
            }


            if (CountItems_Sit01 + CountItems_Sit02 > 0)
            {
                LlenaGridCobros();
            }



        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
        
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

            foreach (GridDataItem i in rGdvOperaciones.SelectedItems)
            {

                var dataItem = rGdvOperaciones.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string docRegId = dataItem.GetDataKeyValue("cobCCId").ToString();
                    //

                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_CobrosCC";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@cobCCId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));
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

                    LlenaGridCobros();
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
                    LlenaGridCobros();

                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
        rGdvOperaciones.ClientSettings.Selecting.AllowRowSelect = false;

    }
    private void EjecutaSpAccionValidacion()
    {
        int CountItems = 0;
        string smaMSGTip = "";
        string smaMSGDes = "";


        foreach (GridDataItem i in rGdvOperaciones.SelectedItems)
        {
            var dataItem = rGdvOperaciones.SelectedItems[CountItems] as GridDataItem;
            if (dataItem != null)
            {
                //Int32 folioId = Convert.ToInt32(dataItem["transId"].Text);

                Int32 folioId = Convert.ToInt32(dataItem.GetDataKeyValue("cobCCId").ToString());
                string sitCve = dataItem["cobCCSit"].Text;
                //string sitCve = "R";
                string sValiProcCve = "CCCOBROS";
                string maUser = LM.sValSess(this.Page, 1);

                try
                {

                    DataSet ds = new DataSet();
                    ds = FNValida.dsEXPROOpe_ValidacionesProcesos(Pag_sConexionLog, Pag_sCompania, folioId, sValiProcCve, maUser, sitCve);
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

    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    private void LlenaGridCobros()
    {
        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CobrosCC";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            FnCtlsFillIn.RadGrid(ref rGdvOperaciones, ds);

        }
        catch (Exception)
        {

            throw;
        }



    }
    private void loadGridFltr()
    {
        string prmConcepto = rCboConcepto.SelectedValue;
        string prmCuenta = rCboCuenta.SelectedValue;
        //string prmFchIni = RdDateFecha_Inicio.SelectedDate.Value.Year.ToString() + "-" + RdDateFecha_Inicio.SelectedDate.Value.Month.ToString().PadLeft(2,'0') + "-" + RdDateFecha_Inicio.SelectedDate.Value.Day.ToString().PadLeft(2, '0') + " 00:00:00";
        //string prmFchFin = RdDateFecha_Final.SelectedDate.Value.Year.ToString() + "-" + RdDateFecha_Final.SelectedDate.Value.Month.ToString().PadLeft(2, '0') + "-" + RdDateFecha_Final.SelectedDate.Value.Day.ToString().PadLeft(2, '0') + " 00:00:00";
        string prmBeneficiario = rTxtBeneficiario.Text;

        string Val_Fec_Inicio = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Inicio.SelectedDate);

        if (RdDateFecha_Inicio.SelectedDate != null)
        {
            Val_Fec_Inicio = dt.Year + "-" + dt.Month.ToString().PadLeft(2, '0') + "-" + dt.Day.ToString().PadLeft(2, '0');
        }
        else
        {
            Val_Fec_Inicio = "";
        }

        string Val_Fec_Fin = "";
        dt = Convert.ToDateTime(RdDateFecha_Final.SelectedDate);

        if (RdDateFecha_Final.SelectedDate != null)
        {
            Val_Fec_Fin = dt.Year + "-" + dt.Month.ToString().PadLeft(2, '0') + "-" + dt.Day.ToString().PadLeft(2, '0');
        }
        else
        {
            Val_Fec_Fin = "";
        }

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CobrosCC";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


            if (rCboConcepto.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(rCboConcepto.SelectedValue));
            }
            else
            {
                //ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, "");
            }

            if (rCboCuenta.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@ctaDepCve", DbType.String, 10, ParameterDirection.Input, rCboCuenta.SelectedValue);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@ctaDepCve", DbType.String, 10, ParameterDirection.Input, "");
            }
            //Se agrega condicionante para combo descripción en seleccion de filtros
            if (rCboDescripcion.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cobCCFolio", DbType.String, 10, ParameterDirection.Input, rCboDescripcion.SelectedValue);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@cobCCFolio", DbType.String, 10, ParameterDirection.Input, "");
            }



            //ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 50, ParameterDirection.Input, rCboBeneficiario.SelectedValue);


            ProcBD.AgregarParametrosProcedimiento("@cobCCSit", DbType.String, 3, ParameterDirection.Input, rCboSituacion.SelectedValue);


            if (Val_Fec_Inicio != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@pagXPFec1", DbType.String, 100, ParameterDirection.Input, Val_Fec_Inicio + " 00:00:00");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@pagXPFec1", DbType.String, 100, ParameterDirection.Input, "1900-01-01 00:00:00");
            }


            if (Val_Fec_Fin != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@pagXPFec2", DbType.String, 100, ParameterDirection.Input, Val_Fec_Fin + " 00:00:00");
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@pagXPFec2", DbType.String, 100, ParameterDirection.Input, "2070-12-31 00:00:00");
            }


            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            FnCtlsFillIn.RadGrid(ref rGdvOperaciones, ds);

        }
        catch (Exception)
        {

            throw;
        }





    }
    private void impPolizas()
    {

        string UrlPdf = "";
        //string fil = Session["Filtro_Vis_Doc"].ToString();
        //string doc = Session["docCve"].ToString();
        string fil = "";
        string doc = "poliza";
        string Uno = "";
        string dos = "";


        //DSdatos Grales
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptPolizas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        int contador = 0;
        foreach (GridDataItem dItem in rGdvOperaciones.SelectedItems)
        {
            Int64 iAsiContEncId = Convert.ToInt64(dItem["asiContEncId"].Text);
            fil += iAsiContEncId.ToString();
        }

        DataSet ds1 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_RptPolizas";
        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD1.AgregarParametrosProcedimiento("@fltrIds", DbType.String, 2048, ParameterDirection.Input, fil);
        ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds) && FnValAdoNet.bDSRowsIsFill(ds1))
        {
            byte[] bytes = oWSRpt.byte_FormatoDoc(ds, ds1);

            UrlPdf = doc + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
            "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";

            string apppath = Server.MapPath("~/Temp") + "\\" + UrlPdf;
            System.IO.File.WriteAllBytes(apppath, bytes);

            string script = @"<script type='text/javascript'> openRadWinRptPol('" + UrlPdf + "'); </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);
        }
        //int contador = 0;

        //foreach (GridDataItem dItem in rGdvOperaciones.SelectedItems)
        //{
        //    Int64 pTranId = Convert.ToInt64(dItem["transId"].Text);

        //    if (contador != 0 && contador < rGdvOperaciones.SelectedItems.Count)
        //    {
        //        fil += ",";
        //    }

        //    fil += getIdPol(pTranId);

        //    contador++;
        //}


        ////DSdatos DeteilReport
        //DataSet ds1 = new DataSet();
        //MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        //ProcBD1.NombreProcedimiento = "sp_RptPolizas";
        //ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        //ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        //ProcBD1.AgregarParametrosProcedimiento("@fltrIds", DbType.String, 2048, ParameterDirection.Input, fil);
        //ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        //if (FnValAdoNet.bDSRowsIsFill(ds) && FnValAdoNet.bDSRowsIsFill(ds1))
        //{
        //    byte[] bytes = oWSRpt.byte_FormatoDoc(ds, ds1);

        //    UrlPdf = doc + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
        //    "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";

        //    string apppath = Server.MapPath("~/Temp") + "\\" + UrlPdf;
        //    System.IO.File.WriteAllBytes(apppath, bytes);

        //    string script = @"<script type='text/javascript'> openRadWinRptPol('" + UrlPdf + "'); </script>";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);
        //}


    }
    private string getIdPol(Int64 pTransId)
    {
        string response = "";

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_RptPolizas";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 0, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, pTransId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSRowsIsFill(ds))
            {
                response = ds.Tables[0].Rows[0]["asiContEncId"].ToString();
            }
        }
        catch (Exception ex)
        {
            response = "";
            throw;
        }

        return response;
    }


    #endregion

    #region FUNCIONES
    String sFolioPoliza(int icptoId)
    {
        string sResult = "";

        try
        {
            DataSet ds = new DataSet();
            ds = ConceptoDefinicion(icptoId);

            if (FnValAdoNet.bDSRowsIsFill(ds))
            {

                string cptoDefAsiConFolTip = ds.Tables[0].Rows[0]["cptoDefAsiConFolTip"].ToString();
                string cptoDefAsiConFolVal = ds.Tables[0].Rows[0]["cptoDefAsiConFolVal"].ToString();

                if (cptoDefAsiConFolTip == "2")
                {
                    sResult = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, cptoDefAsiConFolVal, Convert.ToInt32(cptoDefAsiConFolTip), "");
                }


            }
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
            sResult = "";
        }

        return sResult;
    }
    DataSet ConceptoDefinicion(int icptoId)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, icptoId);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private bool llenaComboClie(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Clientes";

        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 61);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "cliCve", "clieNom", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }
    public bool RabComboBox_ConceptoReferenciaTipo_SegUsuario(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 103);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

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

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "ctaDepCve", "ctaDepDes", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }

    //Se agrega metodo para llenar combo de descripcion
    public bool RadComboBox_Descripcion(string sPag_sConexionLog, string sCiaCve, int Opc, String tipoConcepto, ref RadComboBox objRadComboBox, bool Filtro, bool selected, string SelectedValue = "")
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CobrosCC";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "copCCpFolio", "cobCCDes", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }
    //
    private bool compararFechas()
    {

        if (RdDateFecha_Inicio.SelectedDate > RdDateFecha_Final.SelectedDate)
        {
            return false;
        }
        else if (RdDateFecha_Final.SelectedDate < RdDateFecha_Inicio.SelectedDate)
        {
            return false;
        }
        else
        {
            return true;
        }

    }


    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvOperaciones.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvOperaciones.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }


        //VALIDACION
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString())
        {
            if (rGdvOperaciones.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        //VER ERRORES
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        {
            if (rGdvOperaciones.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        //GENERA / APLICA  
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString())
        {
            if (rGdvOperaciones.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        //EDITA POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString())
        {
            if (rGdvOperaciones.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            return sResult;
        }

        //IMPRIMIRPOL
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString())
        {
            if (rGdvOperaciones.SelectedItems.Count == 0)
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
        int GvSelectItem = rGdvOperaciones.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //VALIDACION
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //VER ERRORES
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //GENERA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //APLICA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //EDITA--POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //IMPRIME POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }
    private Boolean ValidaAccionesSituacionesOpe(ref string sMSGTip, ref string sResult)
    {
        Boolean bResult = true;

        var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {          
            string sSitCve = dataItem["cobCCSit"].Text;
            int RegId = Convert.ToInt32(dataItem.GetDataKeyValue("cobCCId").ToString());
            int iAccId = Convert.ToInt32(hdfBtnAccion.Value);

            if (FNValida.bAcciones_ValidaAccionesSituacionesOpe_Cobros(Pag_sConexionLog, Pag_sCompania, iAccId, sSitCve, RegId, ref sMSGTip, ref sResult) == false)
            {
                return false;
            }
        }

        return bResult;
    }
    #endregion





}