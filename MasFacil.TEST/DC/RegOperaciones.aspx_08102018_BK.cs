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

public partial class DC_RegOperaciones : System.Web.UI.Page
{

    #region VARIABLES

    MGMSecurity.SessionMangemer SM = new MGMSecurity.SessionMangemer();
    MGMSecurity.LoginManeger LM = new MGMSecurity.LoginManeger();

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

    //=====> EVENTOS CONTROLES
    protected void rGdv_Documentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString() ||
        //   hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString())
        //{
        //    var dataItem = this.rGdvOperaciones.SelectedItems[0] as GridDataItem;
        //    if (dataItem != null)
        //    {
        //        string sAccId = dataItem["accId"].Text;
        //        if (sAccId != hdfBtnAccion.Value)
        //        {
        //            dataItem.Selected = false;
        //        }
        //    }
        //}

        //string sTransSit = "";
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
        //{

        //    var dataItem = this.rGdvOperaciones.SelectedItems[0] as GridDataItem;
        //    if (dataItem != null)
        //    {
        //        sTransSit = dataItem["transSit"].Text;
        //        if (sTransSit == "E" || sTransSit == "GE" || sTransSit == "IE")
        //        {
        //            dataItem.Selected = true;
        //        }
        //        else
        //        {
        //            dataItem.Selected = false;
        //        }
        //    }
        //}
    }
    protected void rCboTipoConcepto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string maUser = LM.sValSess(this.Page, 1);
        FnCtlsFillIn.RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania,1, maUser, rCboTipoConcepto.SelectedValue.ToString(), ref rCboConcepto, true, false);
        EnableFiltros();

        rGdvOperaciones.DataSource = null;
        rGdvOperaciones.DataBind();

        TituloPagina();
    }
    protected void rCboConcepto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        EnableFiltros();
        LlenaGridOperaciones();
    }
    protected void rCboSituacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaGridOperaciones();
    }
    protected void RdDateFecha_Inicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
         string Val_Fec_Inicio = "";
         DateTime dt = Convert.ToDateTime(RdDateFecha_Inicio.SelectedDate);
         Val_Fec_Inicio = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            LlenaGridOperaciones();
            EnableFiltros();
        }
        else
        {
            RdDateFecha_Inicio.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Inicial no puede ser mayor a la Final", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
        }



    }
    protected void RdDateFecha_Final_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        string Val_Fec_Fin = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha_Final.SelectedDate);
        Val_Fec_Fin = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            LlenaGridOperaciones();
            EnableFiltros();
        }
        else
        {
            RdDateFecha_Final.Clear();
            RadWindowManagerPage.RadAlert("La Fecha Final no puede ser Menor a la Inical", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");

        }


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
    protected void rBtnImprimir_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Imprimir).ToString();
        ControlesAccion();
    }
    protected void rBtnPolizaEdit_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString();
        ControlesAccion();
    }
    protected void rBtnPolizaImp_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
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


        if (Request.QueryString["cptoTip"] != null && Request.QueryString["cptoTip"] != "")
        {
            Pag_cptoTip = Request.QueryString["cptoTip"];
        }
        else {
            Pag_cptoTip = "0";
        }
    }
    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
    }
    public void InicioPagina()
    {
        string sCptoId = "";
        if (Request.QueryString["cptoId"] != null && Request.QueryString["cptoId"] != "")
        {
            sCptoId = Request.QueryString["cptoId"].ToString();
        }

        Session["RawUrl_Return"] = "";
        hdfRawUrl.Value = this.Page.Request.RawUrl.ToString();

        hdfRawUrl.Value = hdfRawUrl.Value.ToString().Replace("&cptoId=" + sCptoId, "");

        hdfBtnAccion.Value = "";
        ControlesAccion();

         string sValue = Convert.ToString(Session["TipoCptoOpe"]);
        if (sValue != "")
        {
            rCboTipoConcepto.Enabled = false;
            FnCtlsFillIn.RadComboBox_ContabilidadReferenciaTipo(Pag_sConexionLog, Pag_sCompania, ref rCboTipoConcepto, true, true, sValue);
            string maUser = LM.sValSess(this.Page, 1);

            if (rCboTipoConcepto.SelectedValue.ToString() == "CC")
            {
                FnCtlsFillIn.RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, 1, maUser, rCboTipoConcepto.SelectedValue.ToString(), ref rCboConcepto, true,false, sCptoId);
            }
            else {
                if (Pag_cptoTip != "0")
                {
                    FnCtlsFillIn.RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, 1, maUser, rCboTipoConcepto.SelectedValue.ToString(), ref rCboConcepto, true, false, sCptoId, Convert.ToInt32(Pag_cptoTip));
                }
                else
                {
                    FnCtlsFillIn.RabComboBox_ConceptoReferenciaTipo_SegUsuario(Pag_sConexionLog, Pag_sCompania, 1, maUser, rCboTipoConcepto.SelectedValue.ToString(), ref rCboConcepto, true, false, sCptoId);
                }

            }




        }
        else {
            FnCtlsFillIn.RadComboBox_ContabilidadReferenciaTipo(Pag_sConexionLog, Pag_sCompania, ref rCboTipoConcepto, true,false);
        }

        FnCtlsFillIn.RadComboBox_Situaciones(Pag_sConexionLog, Pag_sCompania,1, ref rCboSituacion, true, false, "");

        RdDateFecha_Inicio.SelectedDate = null;
        RdDateFecha_Final.SelectedDate = null;

        EnableFiltros();
        LlenaGridOperaciones();

        TituloPagina();

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

    private void TituloPagina()
    {
        if (rCboTipoConcepto.SelectedIndex == -1) {
            FNGrales.bTitleDesc(Page, "Administración de Operaciones", "PnlMPFormTituloApartado");
        }
        else { 
        FNGrales.bTitleDesc(Page, "Administración de Operaciones " + rCboTipoConcepto.Text.ToString(), "PnlMPFormTituloApartado");
        }
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        ////===> CONTROLES GENERAL
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnValidacion.Image.Url = "~/Imagenes/IcoBotones/IcoBtnValidacion.png";
        rBtVerErr.Image.Url = "~/Imagenes/IcoBotones/IcoBtnVerErrores.png";
        rBtnGenera.Image.Url = "~/Imagenes/IcoBotones/IcoBtnProcesar.png";
        rBtnAplica.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";
        rBtnImprimir.Image.Url = "~/Imagenes/IcoBotones/IcoBtnImprimir.png";
        rBtnPolizaEdit.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarPoliza.png";
        rBtnPolizaImp.Image.Url = "~/Imagenes/IcoBotones/IcoBtnImprimir.png";

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
            //NUEVO  //MODIFICAR //ELIMIAR //VALIDACION   //VER ERRORES
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString()
                )
            {
                EjecutaAccion();
            }

            //GENERA  //APLICA //POLIZA EDITAR //POLIZA IMPRIMIR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Imprimir).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaImp).ToString()
                )
            {
                EjecutaAccion();
            }
        }
    }
    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);


        if (msgValidacion == "")
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                Session["RawUrl_Return"] = hdfRawUrl.Value;
                if (rCboConcepto.SelectedIndex != -1)
                {
                    Session["RawUrl_Return"] += "&cptoId=" + rCboConcepto.SelectedValue.ToString();
                }

                if (Pag_cptoTip == "0")
                {
                    Response.Redirect("~/DC/RegOperacionesDet.aspx?Acc=" + Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString());
                }
                else
                {
                    Response.Redirect("~/DC/RegOperacionesDet.aspx?Acc=" + Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() + "&cptoTip=" + Pag_cptoTip  );
                }
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    string sOpe = dataItem.GetDataKeyValue("transId").ToString();
                    Session["RawUrl_Return"] = hdfRawUrl.Value;
                    if (rCboConcepto.SelectedIndex != -1)
                    {
                        Session["RawUrl_Return"] += "&cptoId=" + rCboConcepto.SelectedValue.ToString();
                    }

                    if (Pag_cptoTip == "0")
                    {
                        Response.Redirect("~/DC/RegOperacionesDet.aspx?Acc=" + Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() + "&Ope=" + sOpe);
                    }
                    else
                    {
                        Response.Redirect("~/DC/RegOperacionesDet.aspx?Acc=" + Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() + "&Ope=" + sOpe + "&cptoTip=" + Pag_cptoTip);
                    }
                }
            }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
            }

            //VALIDACION
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString())
            {
                EjecutaSpAccionValidacion();

            }

            //VER ERRORES
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.VerError).ToString())
            {
                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem.GetDataKeyValue("transId").ToString();

                RadWindowVerErrores.NavigateUrl = "VerErrores.aspx?ErrorId=" + stransDetId + "&ValCmb=" + rCboTipoConcepto.SelectedValue + "&ProCve=" + "OPE";
                string script = "function f(){$find(\"" + RadWindowVerErrores.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            //GENERA / APLICA  
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString() ||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Aplica).ToString())
            {
                EjecutaSpAccion();
            }

            //IMPRIMIR TRANSACCION
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Imprimir).ToString())
            {
                impTransaccion();
            }

            //EDITA POLIZA
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.PolizaEdit).ToString())
            {
                var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
                Session["RawUrl_Return"] = hdfRawUrl.Value;

                string sAsiContEncId = dataItem["asiContEncId"].Text; 
                Response.Redirect("~/CG/Poliza.aspx?AC=" + sAsiContEncId + "&REF=" + rCboTipoConcepto.SelectedValue + "&cptoTip=" + Pag_cptoTip);
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
    private void EjecutaAccionLimpiar()
    {
        this.rCboSituacion.ClearSelection();
        this.rCboConcepto.ClearSelection();
        this.rCboDescripcion.ClearSelection();
        RdDateFecha_Inicio.SelectedDate = null;
        RdDateFecha_Final.SelectedDate = null;
        LlenaGridOperaciones();
    }
    private void EjecutaSpAccion()
    {
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
                    Int32 RegId = Convert.ToInt32(dataItem["transId"].Text);
                    string sValiProcCve = Convert.ToString(Session["TipoCptoOpe"]) + "OPE";
                    string sitCve = dataItem["transSit"].Text;
                    string maUser = LM.sValSess(this.Page, 1);

                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_EXPROOpe";
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@RegId", DbType.Int64, 0, ParameterDirection.Input, RegId);
                    ProcBD.AgregarParametrosProcedimiento("@valiProcCve", DbType.String, 10, ParameterDirection.Input, sValiProcCve);
                    ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, maUser);
                    ProcBD.AgregarParametrosProcedimiento("@SitCve", DbType.String, 3, ParameterDirection.Input, sitCve);
                    ProcBD.AgregarParametrosProcedimiento("@acciId", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value.ToString());
                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Genera).ToString())
                    {
                        ProcBD.AgregarParametrosProcedimiento("@polCve", DbType.String, 10, ParameterDirection.Input, sFolioPoliza());
                    }

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (FnValAdoNet.bDSIsFill(ds))
                    {
                        smaMSGTip = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                        smaMSGDes = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                        //Correctos
                        if (smaMSGTip == "1")
                        {
                            CountItems_Sit01 += 1;
                        }

                        //Errores
                        if (smaMSGTip == "2")
                        {
                            CountItems_Sit02 += 1;
                        }
                        
                        //No validas
                        if (smaMSGTip == "0")
                        {
                            CountItems_Sit00 += 1;
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

                //if (CountItems_Sit01 > 0)
                //{
                //    smaMSGDes = smaMSGDes + "<br /> Correctos: " + Convert.ToString(CountItems_Sit01);
                //}
                //if (CountItems_Sit02 > 0)
                //{
                //    smaMSGDes = smaMSGDes + "<br /> Errores: " + Convert.ToString(CountItems_Sit02);
                //}
                //if (CountItems_Sit00 > 0)
                //{
                //    smaMSGDes = smaMSGDes + "<br /> No validas: " + Convert.ToString(CountItems_Sit00);
                //}
                ShowAlert(smaMSGTip, smaMSGDes);
            }


            if (CountItems_Sit01 + CountItems_Sit02 > 0)
            {
                LlenaGridOperaciones();
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

                    string folioId = dataItem["transId"].Text;
                    //

                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@folioId", DbType.Int64, 0, ParameterDirection.Input, folioId);
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
                    LlenaGridOperaciones();
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
                }
                else
                {

                    LlenaGridOperaciones();
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
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
                    LlenaGridOperaciones();
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
                }
                else
                {
                    LlenaGridOperaciones();
                    hdfBtnAccion.Value = "";
                    ControlesAccion();

                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
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
                Int32 folioId = Convert.ToInt32(dataItem["transId"].Text);
                string sitCve = dataItem["transSit"].Text;
                string sValiProcCve = Convert.ToString(Session["TipoCptoOpe"]) + "OPE";
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

        LlenaGridOperaciones(); 
    }
    private void LlenaGridOperaciones()
    {

        //if (this.rCboConcepto.SelectedIndex !=-1)
        //{

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_RegistroOperaciones";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@contRefTipCve", DbType.String, 2, ParameterDirection.Input, rCboTipoConcepto.SelectedValue);
            if (rCboConcepto.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            }

            if (rCboSituacion.SelectedIndex != -1)
            {
                ProcBD.AgregarParametrosProcedimiento("@transSit", DbType.String, 3, ParameterDirection.Input, rCboSituacion.SelectedValue);
            }


            string Val_Fec_Inicio = RdDateFecha_Inicio.SelectedDate.ToString();
            if (Val_Fec_Inicio != "")
            {
                DateTime dt = Convert.ToDateTime(Val_Fec_Inicio);
                Val_Fec_Inicio = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
                ProcBD.AgregarParametrosProcedimiento("@transFecIni", DbType.String, 10, ParameterDirection.Input, Val_Fec_Inicio);
            }


            string Val_Fec_Fin = RdDateFecha_Final.SelectedDate.ToString();
            if (Val_Fec_Fin != "")
            {
                DateTime dt2 = Convert.ToDateTime(Val_Fec_Fin);
                Val_Fec_Fin = dt2.Year + "/" + dt2.Month.ToString().PadLeft(2, '0') + "/" + dt2.Day.ToString().PadLeft(2, '0');
                ProcBD.AgregarParametrosProcedimiento("@transFecFin", DbType.String, 10, ParameterDirection.Input, Val_Fec_Fin);
            }

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            FnCtlsFillIn.RadGrid(ref rGdvOperaciones, ds);

        //}
        //else
        //{
        //    rGdvOperaciones.DataSource = null;
        //    rGdvOperaciones.DataBind();
        //}

    }
    private void EnableFiltros()
    {

        if (rCboTipoConcepto.SelectedIndex == -1)
        {
            rCboConcepto.Items.Clear();
            rCboConcepto.Enabled = false;
            rCboSituacion.Enabled = false;
            RdDateFecha_Inicio.Enabled = false;
            RdDateFecha_Final.Enabled = false;
        }
        else {
            rCboConcepto.Enabled = true;
            if (rCboConcepto.SelectedIndex == -1)
            {
                //Se habilitan los combos situacion, Fec.Ini., Fec.Fin. para que se muestren habilitados siempre
                rCboSituacion.Enabled = true;
                RdDateFecha_Inicio.Enabled = true;
                RdDateFecha_Final.Enabled = true;
            }
            else {
                rCboSituacion.Enabled = true;
                RdDateFecha_Inicio.Enabled = true;
                RdDateFecha_Final.Enabled = true;
            }
        }

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

    private void impPolizas()
    {
        string UrlPdf = "";
        string fil = "";
        string doc = "poliza";

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

    }

    private void impTransaccion() {

        string UrlPdf = "";
        string fil = "";
        string doc = "Trans";

        var dataItem = rGdvOperaciones.SelectedItems[0] as GridDataItem;
        string stransDetId = dataItem.GetDataKeyValue("transId").ToString();

        //DSdatos Grales
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_RptTRANS";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        DataSet ds1 = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD1 = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD1.NombreProcedimiento = "sp_RptTRANS";
        ProcBD1.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD1.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD1.AgregarParametrosProcedimiento("@transId", DbType.Int64, 0, ParameterDirection.Input, stransDetId);

        ds1 = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD1.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        byte[] bytes = oWSRpt.byte_FormatoDoc(ds, ds1);


        //if (customerOrders.Columns.Count == 0)
        //{
        //    customerOrders.Clear();
        //    customerOrders.Columns.Add("urlDocs");
        //}

        //DataRow rowTmp = customerOrders.NewRow();

        UrlPdf = doc + "-" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() +
        "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".pdf";

        //rowTmp["urlDocs"] = UrlPdf;

        //customerOrders.Rows.Add(rowTmp);

        string apppath = Server.MapPath("~/Temp") + "\\" + UrlPdf;
        System.IO.File.WriteAllBytes(apppath, bytes);

        string script = @"<script type='text/javascript'> openRadWinRptPol('" + UrlPdf + "'); </script>";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);

    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion


    #region FUNCIONES
    String sFolioPoliza( ) {
        string sResult = "";

        try
        {
            DataSet ds = new DataSet();
            ds = ConceptoDefinicion();

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

    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
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

        //VALIDAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Validacion).ToString())
        {
            GvVAS = new string[] { "VAL0003"};
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
            GvVAS = new string[] { "VAL0003" };
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
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }

            if (ValidaAccionesSituacionesOpe(ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }



        //IMPRIMiR TRANSACCION
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Imprimir).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvOperaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }


        //EDITA POLIZA
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
        int CountItems = 0;
        string sFoliosAccNoValido = "";
        string sMsgNoValido = "";

        foreach (GridDataItem i in rGdvOperaciones.SelectedItems)
        {

            var dataItem = rGdvOperaciones.SelectedItems[CountItems] as GridDataItem;
            if (dataItem != null)
            {
                string sSitCve = dataItem["transSit"].Text;
                int itransId = Convert.ToInt32(dataItem["transId"].Text);
                int iAccId = Convert.ToInt32(hdfBtnAccion.Value);
                string sFolio = dataItem["transFolio"].Text;

                if (FNValida.bAcciones_ValidaAccionesSituacionesOpe(Pag_sConexionLog, Pag_sCompania, iAccId, sSitCve, itransId, ref sMSGTip, ref sResult) == false)
                {
                    string separador = ", ";
                    if (sFoliosAccNoValido == "") {
                        separador = "";
                    }
                    sMsgNoValido = sResult;
                    sFoliosAccNoValido += separador + sFolio;                  
                }
                CountItems += 1;
            }

        }

        if (sFoliosAccNoValido != "") {
            bResult = false;
            sResult = sMsgNoValido;
            sResult += "<br /> Folio: " + sFoliosAccNoValido;
        }

        return bResult;
    }

    #endregion

}