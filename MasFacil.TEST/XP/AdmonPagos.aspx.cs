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

public partial class XP_AdmonPagos : System.Web.UI.Page
{

    #region VARIABLES

    ws.Servicio oWS = new ws.Servicio();
    wsRpt.Service oWSRpt = new wsRpt.Service();

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();    
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
        Response.Redirect("~/XP/AdmonPagosRegistro.aspx?idM=" + Pag_sidM);
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

    protected void rBtnGeneraCheque_Click(object sender, ImageButtonClickEventArgs e)
    {      

        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.GeneraCheque).ToString();
        ControlesAccion();
    }

    protected void rBtnReversaCheque_Click(object sender, ImageButtonClickEventArgs e)
    {

        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.ReversaCheque).ToString();
        ControlesAccion();
    }

    protected void rBtnGenera_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString();
        ControlesAccion();
    }

    protected void rbtnImpPoliChe_Click(object sender, ImageButtonClickEventArgs e)
    {

        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaCheImp).ToString();
        ControlesAccion();
    }

    //GRET, se agrega evento para boton de cancela cheque
    protected void rBtnCancelaCheque_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.CancelaCheque).ToString();
        ControlesAccion();
    }
    //

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
        cleanUi();
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

    protected void rBtnGuardar_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }

    protected void rCboCuenta_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        loadGridFltr();
    }
    
    //Se agrega evento para filtrar por beneficiario
   protected void rCboBeneficiario2_SelectedIndexChanged1(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
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

    //protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    //{

    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
    //    {
    //        rGdvOperaciones.MasterTableView.ClearSelectedItems();

    //    }

    //    //GRET, Se agrega llamada al metodo que busca folio del registro o registros 


    //    String pagXPFolio = consultaSessionXPFolio();
    //    borrarSessionXPFolio();
    //    loadGridFltrFolio(pagXPFolio);

    //    //
    //    //loadGridFltr();


    //}


    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        {
            rGdvOperaciones.MasterTableView.ClearSelectedItems();

        }

        //GRET, Se agrega llamada al metodo que busca folio del registro o registros 

        DataSet ds = new DataSet();
        string Folios = "";
        string pagXPFolio = "";
        int cont = 0;
        ds = consultaSessionXPFolio();
        DataTable dt = ds.Tables[0];
        //if (FnValAdoNet.bDSIsFill(ds))
        if (dt.Rows.Count>0)
        {

        foreach (DataRow row in dt.Rows)
        {
            if (dt.Rows.Count > 1)
            {
                Folios = Convert.ToString(row["pagXPFolio"]);
                pagXPFolio = pagXPFolio +","+ Folios ;                    
            }

            else
            {
                pagXPFolio = Convert.ToString(row["pagXPFolio"]);
            }

        }

            borrarSessionXPFolio();
            
            if (pagXPFolio.StartsWith(","))
            {
                pagXPFolio = pagXPFolio.Substring(1);
            }

            loadGridFltrFolio(pagXPFolio);

        }
        else
        {
            loadGrid();
        }
       
    }
    protected void rBtnMasivo_Click(object sender, ImageButtonClickEventArgs e)
    {
        try
        {
            string script = "function f(){$find(\"" + RadWindowMasivos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        }
        catch (Exception ex)
        {
            ex.ToString();
            throw;
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
        RdDateFecha_Inicio.SelectedDate = null;
        RdDateFecha_Final.SelectedDate = null;
        ControlesAccion();
        llenaComboProv(Pag_sConexionLog, Pag_sCompania, ref rCboBeneficiario, true, false);
        //Se agrega llamada a metodo para llenar el filtro Beneficiario
        llenaComboBeneficiario(Pag_sConexionLog, Pag_sCompania, ref rCboBeneficiario2, true, false);
        //
        loadGrid();
        FNGrales.bTitleDesc(Page, "Administración de Pagos", "PnlMPFormTituloApartado");
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

                Session["Valor_btn"] = "1";
                Int64 Pag_sidM = 0;
                if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                {
                    Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                }

                Response.Redirect("~/XP/AdmonPagosRegistro.aspx?idM=" + Pag_sidM);

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
                    Session["Valor_DocCve"] = dataItem.GetDataKeyValue("pagXPId").ToString();
                }
                Session["Valor_btn"] = "2";
                Int64 Pag_sidM = 0;
                if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                {
                    Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                }
                Response.Redirect("~/XP/AdmonPagosRegistro.aspx?idM=" + Pag_sidM);

            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                InicioPagina();
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
                string stransDetId = dataItem.GetDataKeyValue("pagXPId").ToString();

                RadWindowVerErrores.NavigateUrl = "../DC/VerErrores.aspx?ErrorId=" + stransDetId + "&ValCmb=" + "XP" + "&ProCve=" + "PAGOS";
                string script = "function f(){$find(\"" + RadWindowVerErrores.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            //GENERA CHEQUE
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.GeneraCheque).ToString())
            {
                foreach (GridDataItem item in rGdvOperaciones.SelectedItems)
                {
                    String Folio = item["pagXPFolio"].Text;
                    String Cuenta = item["ctaDepDes"].Text;
                    String pagXPId = item.GetDataKeyValue("pagXPId").ToString();
                    String cptoId = item["cptoId"].Text;
                    

                    EjecutaSpAccionGeneraCheque(Cuenta, Pag_sCompania, Folio,pagXPId,cptoId);


                }
            }

            //REVERSA CHEQUE
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.ReversaCheque).ToString())
            {
                foreach (GridDataItem item in rGdvOperaciones.SelectedItems)
                {
                    String Folio = item["pagXPFolio"].Text;
                    String Cuenta = item["ctaDepDes"].Text;
                    String pagXPId = item.GetDataKeyValue("pagXPId").ToString();
                    String cptoId = item["cptoId"].Text;
                    

                    EjecutaSpAccionReversaCheque(Cuenta, Pag_sCompania, Folio,pagXPId,cptoId);


                }
            }

            //GENERA / APLICA  
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString()
                || hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString()
                )
            {
                EjecutaSpAccion();
                //InicioPagina();
            }

            //IMPRIME CHEQUE POLIZA
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaCheImp).ToString())
            { 
                impChequePoliza();

                foreach (GridDataItem item in rGdvOperaciones.SelectedItems)
                {
                    String Folio = item["pagXPFolio"].Text;                    

                    EjecutaSpAccionImprimeCheque(Pag_sCompania, Folio);
                }
            }

            //CANCELA CHEQUE POLIZA 
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.CancelaCheque).ToString())
            {
                foreach (GridDataItem item in rGdvOperaciones.SelectedItems)
                {
                    String Folio = item["pagXPFolio"].Text;
                    String Cuenta = item["ctaDepDes"].Text;
                    String pagXPId = item.GetDataKeyValue("pagXPId").ToString();
                    String cptoId = item["cptoId"].Text;


                    EjecutaSpAccionCancelaCheque(Cuenta, Pag_sCompania, Folio, pagXPId, cptoId);


                }
            }

            //Edita Poliza
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString())
            {
                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                Session["RawUrl_Return"] = hdfRawUrl.Value;

                string sAsiContEncId = dataItem["asiContEncId"].Text;
                Response.Redirect("~/CG/Poliza.aspx?AC=" + sAsiContEncId + "&REF=" + "XP" + "&cptoTip=AdmonXP");

                //string stransDetId = dataItem["transId"].Text;
                //Response.Redirect("~/DC/MttoAsientosContables.aspx?stransDetId=" + stransDetId + "&cptoTip=AdmonXP");
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
        RdDateFecha_Final.Clear();
        RdDateFecha_Inicio.Clear();
        rCboBeneficiario.ClearSelection();
        //Se agrega combo de Beneficiario para el boton de limpiar
        rCboBeneficiario2.ClearSelection();
        rCboSituacion.ClearSelection();
        loadGrid();
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
                Response.Redirect("~/XP/AdmonPagosRegistro.aspx?idM=" + Pag_sidM);
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    //Session["Valor_DocCve"] = dataItem["docRegid"].Text;
                    Session["Valor_DocCve"] = dataItem.GetDataKeyValue("pagXPId").ToString();
                }
                Session["Valor_btn"] = "2";
                 Int64 Pag_sidM = 0;
                if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                {
                    Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                }
                Response.Redirect("~/XP/AdmonPagosRegistro.aspx?idM=" + Pag_sidM);

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
                string stransDetId = dataItem.GetDataKeyValue("pagXPId").ToString();

                RadWindowVerErrores.NavigateUrl = "../DC/VerErrores.aspx?ErrorId=" + stransDetId + "&ValCmb=" + "XP" + "&ProCve=" + "PAGOS";
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
                Response.Redirect("~/DC/MttoAsientosContables.aspx?stransDetId=" + stransDetId + "&cptoTip=AdmonXP");

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

                Int32 folioId = Convert.ToInt32(dataItem.GetDataKeyValue("pagXPId").ToString());
                string sitCve = dataItem["pagXPSit"].Text;
                //string sitCve = "R";
                string sValiProcCve = "XPPAGOS";
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
                     
                    Int32 RegId = Convert.ToInt32(dataItem.GetDataKeyValue("pagXPId").ToString());
                    string sValiProcCve = "XPPAGOS";
                    string sitCve = dataItem["pagXPSit"].Text;
                    string maUser = LM.sValSess(this.Page, 1);
                    int  icptoId = Convert.ToInt16( dataItem["cptoId"].Text);

                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_EXPROAdmon";
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, RegId);
                    ProcBD.AgregarParametrosProcedimiento("@valiProcCve", DbType.String, 10, ParameterDirection.Input, sValiProcCve);
                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                    ProcBD.AgregarParametrosProcedimiento("@SitCve", DbType.String, 3, ParameterDirection.Input, sitCve);
                    ProcBD.AgregarParametrosProcedimiento("@acciId", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value.ToString());
                    ProcBD.AgregarParametrosProcedimiento("@modulo", DbType.String, 10, ParameterDirection.Input, "XP");
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
                loadGrid();
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

                    string docRegId = dataItem.GetDataKeyValue("pagXPId").ToString();
                    //

                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_PagosXP";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@pagXPId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(docRegId));
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

                    loadGrid();
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
                    loadGrid();

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

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void loadGrid()
    {
        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PagosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
           
            FnCtlsFillIn.RadGrid(ref rGdvOperaciones, ds);

        }
        catch (Exception)
        {

            throw;
        }



    }

    //GRET. se agrega metodo para filtrar por registros seleccionados en administracions de pagos
    private void loadGridFltrFolio(String pagXPFolio)
    {
        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PagosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 58);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFolio", DbType.String, 500, ParameterDirection.Input, pagXPFolio);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            FnCtlsFillIn.RadGrid(ref rGdvOperaciones, ds);

        }
        catch (Exception)
        {

            throw;
        }



    }

    private void borrarSessionXPFolio()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Session_XPFolio";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@pag_sConexionLog", DbType.String, 200, ParameterDirection.Input, Pag_sConexionLog);
            ProcBD.AgregarParametrosProcedimiento("@pag_sSessionLog", DbType.String, 200, ParameterDirection.Input, Pag_sSessionLog);     

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        }
        catch (Exception e)
        {

        }
    }

    //

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
            ProcBD.NombreProcedimiento = "sp_PagosXP";
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

            //Se agrega parametro para combo Beneficiario
            if (rCboBeneficiario2.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@pagXPBenef", DbType.String, 50, ParameterDirection.Input, rCboBeneficiario2.SelectedValue);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@pagXPBenef", DbType.String, 10, ParameterDirection.Input, "");
            }
            

            ProcBD.AgregarParametrosProcedimiento("@provCve", DbType.String, 50, ParameterDirection.Input, rCboBeneficiario.SelectedValue);


            ProcBD.AgregarParametrosProcedimiento("@pagXPSit", DbType.String, 3, ParameterDirection.Input, rCboSituacion.SelectedValue);


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


    private void impChequePoliza()
    {

        string UrlPdf = "";
        //string fil = Session["Filtro_Vis_Doc"].ToString();
        //string doc = Session["docCve"].ToString();
        string fil = "";
        string doc = "PolizaCheque";
        string Uno = "";
        string dos = "";

        var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
        string pagXPId = dataItem.GetDataKeyValue("pagXPId").ToString();

        //DSdatos Grales
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptPolizas";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@pagXPId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(pagXPId));
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        int contador = 0;
        foreach (GridDataItem dItem in rGdvOperaciones.SelectedItems)
        {
            Int64 iAsiContEncId = Convert.ToInt64(dItem["asiContEncId"].Text);
            fil += iAsiContEncId.ToString();
        }


        //DSdatos DeteilReport
        DataSet ds1 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_RptPolizas";
        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 53);
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


        //DSdatos DeteilReport
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


    // CHEQUE
  
    private void EjecutaSpAccionGeneraCheque(String Cuenta, String Pag_sCompania, String Folio,String pagXPId, String cptoId)
    {
        String MsgTip;
        String MsgDes;

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PagosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ctaDepDes", DbType.String, 100, ParameterDirection.Input, Cuenta);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFolio", DbType.String, 10, ParameterDirection.Input, Folio);
            ProcBD.AgregarParametrosProcedimiento("@pagXPId", DbType.String, 10, ParameterDirection.Input, pagXPId);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 10, ParameterDirection.Input, cptoId);           

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);          
            
            if (ds.Tables[0].Rows.Count >0)
            {
                MsgTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                if (MsgTip == "1")
                {
                    MsgDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    ShowAlert(MsgTip, MsgDes);
                }

                else
                {
                    MsgDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    ShowAlert(MsgTip, MsgDes);
                }
            }


                loadGrid();

        }
        catch (Exception)
        {

            throw;
        }



    }

    private void EjecutaSpAccionReversaCheque(String Cuenta, String Pag_sCompania, String Folio, String pagXPId, String cptoId)
    {
        String MsgTip;
        String MsgDes;

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PagosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 59);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ctaDepDes", DbType.String, 100, ParameterDirection.Input, Cuenta);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFolio", DbType.String, 10, ParameterDirection.Input, Folio);
            ProcBD.AgregarParametrosProcedimiento("@pagXPId", DbType.String, 10, ParameterDirection.Input, pagXPId);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 10, ParameterDirection.Input, cptoId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {
                MsgTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                if (MsgTip == "1")
                {
                    MsgDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    ShowAlert(MsgTip, MsgDes);
                }

                else
                {
                    MsgDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    ShowAlert(MsgTip, MsgDes);
                }
            }


            loadGrid();

        }
        catch (Exception)
        {

            throw;
        }



    }

    private void EjecutaSpAccionImprimeCheque(String Pag_sCompania, String Folio)
    {
        String MsgTip;
        String MsgDes;

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PagosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ProcBD.AgregarParametrosProcedimiento("@pagXPFolio", DbType.String, 10, ParameterDirection.Input, Folio);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);    

            if (ds.Tables[0].Rows.Count > 0)
            {
                MsgTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                if (MsgTip == "1")
                {
                    MsgDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    ShowAlert(MsgTip, MsgDes);
                }

                else
                {
                    MsgDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    ShowAlert(MsgTip, MsgDes);
                }
            }


            loadGrid();

        }
        catch (Exception)
        {

            throw;
        }



    }


    //GRET, se agrega metodo para Cancela Cheque
    private void EjecutaSpAccionCancelaCheque(String Cuenta, String Pag_sCompania, String Folio, String pagXPId, String cptoId)
    {
        String MsgTip;
        String MsgDes;

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_PagosXP";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 59);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ctaDepDes", DbType.String, 100, ParameterDirection.Input, Cuenta);
            ProcBD.AgregarParametrosProcedimiento("@pagXPFolio", DbType.String, 10, ParameterDirection.Input, Folio);
            ProcBD.AgregarParametrosProcedimiento("@pagXPId", DbType.String, 10, ParameterDirection.Input, pagXPId);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.String, 10, ParameterDirection.Input, cptoId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds.Tables[0].Rows.Count > 0)
            {
                MsgTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                if (MsgTip == "1")
                {
                    MsgDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    ShowAlert(MsgTip, MsgDes);
                }

                else
                {
                    MsgDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                    ShowAlert(MsgTip, MsgDes);
                }
            }


            loadGrid();

        }
        catch (Exception)
        {

            throw;
        }



    }
    // 

    // FIN CHEQUE 

    //GRET, Se agrega metodo para busqueda de folio, para filtar en GRID en base a los registros seleccionados en Pagos Masivos

    private DataSet consultaSessionXPFolio()
    {
        String Folio="";
        DataSet ds = new DataSet();
        try
        {
            
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Session_XPFolio";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@pag_sConexionLog", DbType.String, 200, ParameterDirection.Input, Pag_sConexionLog);
            ProcBD.AgregarParametrosProcedimiento("@pag_sSessionLog", DbType.String, 200, ParameterDirection.Input, Pag_sSessionLog);            
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            //if (FnValAdoNet.bDSIsFill(ds))
            //{
            //    Folio = ds.Tables[0].Rows[0]["pagXPFolio"].ToString();              
            //}

        }
        catch(Exception e)
        {
            //Folio = "";
            ds = null;
        }

        return ds;
    }
        //
    #endregion

    #region FUNCIONES
    private bool llenaComboProv(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MttProvDatosGenerales";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref objRadComboBox, ds, "provCve", "provNom", Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }

    //Se agrega metodo para llenado del combo Beneficiario
    private bool llenaComboBeneficiario(string sPag_sConexionLog, string sCiaCve, ref RadComboBox objRadComboBox, bool Filtro, bool selected)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_PagosXP";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 57);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, sCiaCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), sPag_sConexionLog);
        FnCtlsFillIn.RadComboBoxBeneficiario(ref objRadComboBox, ds, "pagXPBenef", "pagXPBenef",Filtro, selected);
        ((Literal)objRadComboBox.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(objRadComboBox.Items.Count);
        return true;
    }
    //
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
    DataSet ConceptoDefinicion( int icptoId)
    {
        DataSet ds = new DataSet();

        //if (rCboConcepto.SelectedIndex != -1)
        //{
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ConceptoDefinicion";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, icptoId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        //}

        return ds;
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

        //GENERA CHEQUE
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.GeneraCheque).ToString())
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


        //REVERSA CHEQUE
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.ReversaCheque).ToString())
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

        //IMPRIME CHEQUE POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaCheImp).ToString())
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

        //CANCELA CHEQUE POLIZA
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.CancelaCheque).ToString())
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
            string sSitCve = dataItem["pagXPSit"].Text;
            int RegId = Convert.ToInt32(dataItem.GetDataKeyValue("pagXPId").ToString());
            int iAccId = Convert.ToInt32(hdfBtnAccion.Value);

            if (FNValida.bAcciones_ValidaAccionesSituacionesOpe_Pagos(Pag_sConexionLog, Pag_sCompania, iAccId, sSitCve, RegId, ref sMSGTip, ref sResult) == false)
            {
                return false;
            }
        }

        return bResult;
    }
    #endregion

}