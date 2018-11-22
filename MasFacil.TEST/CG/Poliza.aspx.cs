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

public partial class CG_Poliza : System.Web.UI.Page
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
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_smaUser;
    private string Pag_sRefCto;
    private string Pag_cptoTip;
    private string Pag_RawUrl_Return;
    #endregion

    #region EVENTOS

    //=====> EVENTOS CONTROLES
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            if (!IsPostBack)
            {
                InicioNotIsPostBack();
                InicioPagina();
            }
        }
    }
    protected void rCboConcepto_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

        FoliadorAsientoContable();
    }
    protected void rTxtAsientoContable_TextChanged(object sender, EventArgs e)
    {

        FoliadorAsientoContable();

        //rTxtAsientoContable.DataBind();
        //Session["FolioSession"] = rTxtFolio.Text;

        //if (rTxtAsientoContable.Text != "")
        //{
        //    ActualizaFolioDataset();
        //}

    }
    protected void RdDateFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
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
        else
        {
            RdDateFecha.Clear();
        }
    }
    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsAsientosSession"];
        rGdvPolizasDet.DataBind();
        FnCtlsFillIn.RadGrid(ref rGdvPolizasDet, (DataSet)Session["dsAsientosSession"]);
        Suma_TotalCargosAbono();
        hdfBtnAccionDet.Value = "";
        rBtnNuevoDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificarDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminarDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";
    }

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnNuevoDet_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
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
        rBtnNuevoDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificarDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminarDet.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnLimpiar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnLimpiar.png";
        rGdvPolizasDet.MasterTableView.ClearSelectedItems();
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();
    }
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        if (Pag_cptoTip == "")
        {
            Response.Redirect(Pag_RawUrl_Return);
            //Response.Redirect("~/CG/Polizas.aspx");
        }
        else if (Pag_cptoTip == "AdmonCC")
        {
            Response.Redirect(Pag_RawUrl_Return);
            //Response.Redirect("~/CC/AdmonCobros.aspx");
        }
        else if (Pag_cptoTip == "AdmonXP")
        {
            Response.Redirect(Pag_RawUrl_Return);
            //Response.Redirect("~/XP/AdmonPagos.aspx");
        }
        else
        {
            Response.Redirect(Pag_RawUrl_Return);
            //Response.Redirect("~/DC/RegOperaciones.aspx?cptoTip=" + Pag_cptoTip);
        }  
    }

    #endregion


    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_smaUser = LM.sValSess(this.Page, 1);
        Pag_RawUrl_Return = "~" + Convert.ToString(Session["RawUrl_Return"]);

        if (Request.QueryString["REF"] != null && Request.QueryString["REF"] != "")
        {
            Pag_sRefCto = Request.QueryString["REF"].ToString();
        }
        else
        {
            Pag_sRefCto = "CG";
        }

        if (Request.QueryString["cptoTip"] != null && Request.QueryString["cptoTip"] != "")
        {
            Pag_cptoTip = Request.QueryString["cptoTip"].ToString();
        }
        else
        {
            Pag_cptoTip = "";
        }
    }

    private void InicioNotIsPostBack() {
        hdfBtnAccion.Value = "";
        hdfBtnAccionDet.Value = "";
        hdfPag_asiContEncId.Value = "";
    }
    public void InicioPagina()
    {
        InicioPaginaValores();
        TituloPagina();
        FnCtlsFillIn.RadComboBox_ConceptosSeguridad(Pag_sConexionLog, Pag_sCompania, Pag_sRefCto, Pag_smaUser, 1, ref rCboConcepto, true, false, "");
        AsientoContable();
    }
    private void InicioPaginaValores() {
        if (hdfPag_asiContEncId.Value == "")
        {
            if (Request.QueryString["AC"] != null && Request.QueryString["AC"] != "")
            {
                hdfPag_asiContEncId.Value = Request.QueryString["AC"];
            }
            
        }

        if (hdfPag_asiContEncId.Value == ""){
            hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        }
        else {
            hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        }
        
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rCboConcepto.Enabled = false;
            rTxtAsientoContable.Enabled = false;
        }
    }
    private void TituloPagina()
    {
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            FNGrales.bTitleDesc(Page, "Nueva Poliza", "PnlMPFormTituloApartado");
        }
        else {
            FNGrales.bTitleDesc(Page, "Editar Poliza", "PnlMPFormTituloApartado");
        }
        
    }
    private void AsientoContable()
    {
        if (hdfPag_asiContEncId.Value != "")
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_asientoContable";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int64, 0, ParameterDirection.Input, hdfPag_asiContEncId.Value);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            if (FnValAdoNet.bDSRowsIsFill(ds))
            {
                rCboConcepto.SelectedValue = ds.Tables[0].Rows[0]["cptoId"].ToString();
                rTxtAsientoContable.Text = ds.Tables[0].Rows[0]["polCve"].ToString();
                rTxtDescripcion.Text = ds.Tables[0].Rows[0]["polDes"].ToString();
                RdDateFecha.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["polFec"].ToString());
                rNumCifraControl.Text = ds.Tables[0].Rows[0]["polCifCtrl"].ToString();          
                lblTotalCargos.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["polSumCgo"]).ToString("C2");
                lblTotalAbonos.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["polSumAbo"]).ToString("C2");
                RdBnryImagenSituacion.ImageUrl = ds.Tables[0].Rows[0]["imgSit"].ToString();
            }
        }
        LlenaGrid();
    }
    private void LlenaGrid() {

        try
        {
            string sAasiContEncId = "0";
            if (hdfPag_asiContEncId.Value != "")
            {
                sAasiContEncId = hdfPag_asiContEncId.Value;
            }

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AsientoContableDetalle";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String , 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int64, 0, ParameterDirection.Input, sAasiContEncId);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            Session["dsAsientosSession"] = ds;
            FnCtlsFillIn.RadGrid(ref rGdvPolizasDet, (DataSet)Session["dsAsientosSession"]);

        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
        }
    }
    private void ControlesAccion()
    {

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
                rGdvPolizasDet.MasterTableView.ClearSelectedItems();
                RadWindowPolizaDetalle.NavigateUrl = "~/CG/PolizaDetalle.aspx?hdfBtnAccionDet="+ hdfBtnAccionDet.Value + "&asiContEncId=" + hdfPag_asiContEncId.Value;

                string script = "function f(){$find(\"" + RadWindowPolizaDetalle.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            //MODIFICAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                var dataItem = rGdvPolizasDet.SelectedItems[0] as GridDataItem;
                string sAsiContDetId = dataItem.GetDataKeyValue("asiContDetId").ToString();

                RadWindowPolizaDetalle.NavigateUrl = "~/CG/PolizaDetalle.aspx?hdfBtnAccionDet=" + hdfBtnAccionDet.Value + "&asiContDetId=" + sAsiContDetId;
                string script = "function f(){$find(\"" + RadWindowPolizaDetalle.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }

            //ELIMINAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccionDetalle();
            }
        }

    }

    private void EjecutaAccionDetalle()
    {
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            DataSet dsTransDet = new DataSet();
            dsTransDet = (DataSet)Session["dsAsientosSession"];

            string stransDetId = "";
            var dataItem = rGdvPolizasDet.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                stransDetId = dataItem.GetDataKeyValue("asiContDetId").ToString();
            }

            for (int i = 0; i < rGdvPolizasDet.Items.Count; i++)
            {
                if (rGdvPolizasDet.Items[i].Selected == true)
                {
                    dsTransDet.Tables[0].Rows[i].Delete();

                }
            }
            dsTransDet.Tables[0].AcceptChanges();
            Session["dsAsientosSession"] = dsTransDet;
            FnCtlsFillIn.RadGrid(ref rGdvPolizasDet, (DataSet)Session["dsAsientosSession"]);

            Suma_TotalCargosAbono();
            hdfBtnAccionDet.Value = "";
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

        try
        {

            //=====>> POLIZA ENCABEZADO <<=====
            string sFechaACont = sFecha();
            if (lblTotalCargos.Text == "") {lblTotalCargos.Text = "0";}
            if (lblTotalAbonos.Text == "") {lblTotalAbonos.Text = "0";}
            if (rNumCifraControl.Text == "") { rNumCifraControl.Text = "0"; }

            decimal dTotalCargos = decimal.Parse(lblTotalCargos.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));
            decimal dTotalAbonos= decimal.Parse(lblTotalAbonos.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));
            decimal dCifraCntrol = decimal.Parse(rNumCifraControl.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AsientoContable";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@polCve", DbType.String, 10, ParameterDirection.Input, rTxtAsientoContable.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@polDes", DbType.String, 50, ParameterDirection.Input, rTxtDescripcion.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, rCboConcepto.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@polFec", DbType.String, 100, ParameterDirection.Input, sFechaACont);
            ProcBD.AgregarParametrosProcedimiento("@polSumCgo", DbType.Decimal, 19, ParameterDirection.Input, dTotalCargos);
            ProcBD.AgregarParametrosProcedimiento("@polSumAbo", DbType.Decimal, 19, ParameterDirection.Input, dTotalAbonos);
            ProcBD.AgregarParametrosProcedimiento("@polCifCtrl", DbType.Decimal, 19, ParameterDirection.Input, dCifraCntrol);
            ProcBD.AgregarParametrosProcedimiento("@maUsuCve", DbType.String, 20, ParameterDirection.Input, Pag_smaUser);
            if (hdfPag_asiContEncId.Value != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(hdfPag_asiContEncId.Value));
            }
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                //=====>> DETALLE TRANSACCIONES / MOVIMIENTOS <<=====
                if (sEjecEstatus == "1")
                {
                    hdfPag_asiContEncId.Value = ds.Tables[0].Rows[0]["asiContEncId"].ToString();
                    hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
                    GuardarPolizaDetalle();

                    //DataSet dsValidaPoliza = new DataSet();
                    //Int32 folioId = Convert.ToInt32( hdfPag_asiContEncId.Value);
                    //string sitCve = "R";
                    //string sValiProcCve = "CGASICONT";
                    //string maUser = LM.sValSess(this.Page, 1);
                    //dsValidaPoliza = FNValida.dsEXPROOpe_ValidacionesProcesos(Pag_sConexionLog, Pag_sCompania, folioId, sValiProcCve, maUser, sitCve);
                    //if (FnValAdoNet.bDSIsFill(ds))
                    //{
                    //    string smaMSGTip = dsValidaPoliza.Tables[0].Rows[0]["maMSGTip"].ToString();
                    //    string smaMSGDes = dsValidaPoliza.Tables[0].Rows[0]["maMSGDes"].ToString();
                    //}

                    ShowAlert(sEjecEstatus, sEjecMSG);
                    InicioPagina();
                }
                else
                {
                    ShowAlert(sEjecEstatus, sEjecMSG);
                }
            }
 


        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.ToString());
        }



    }

    private void GuardarPolizaDetalle()
    {

        int ValEliminar = 0;
        DataSet dsTransDet = new DataSet();
        dsTransDet = (DataSet)Session["dsAsientosSession"];

        DataSet ds = new DataSet();
        if (dsTransDet.Tables[0].Rows.Count == 0)
        {
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AsientoContableDetalle";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int64, 0, ParameterDirection.Input, hdfPag_asiContEncId.Value);
            ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, 1);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        }


        foreach (DataRow drConfCpto in dsTransDet.Tables[0].Rows)
        {
            ValEliminar += 1;

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AsientoContableDetalle";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ValEliminar", DbType.Int32, 0, ParameterDirection.Input, ValEliminar);

            ProcBD.AgregarParametrosProcedimiento("@asiContEncId", DbType.Int64, 0, ParameterDirection.Input, hdfPag_asiContEncId.Value);
            if (drConfCpto["transDetSec"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@transDetSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["transDetSec"]));
            }
            
            ProcBD.AgregarParametrosProcedimiento("@polDetSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetSec"]));

            if (drConfCpto["ctaContCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@ctaContCve", DbType.String, 20, ParameterDirection.Input, Convert.ToString(drConfCpto["ctaContCve"]));
            }
            if (drConfCpto["polDetDes"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetDes", DbType.String, 50, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetDes"]));
            }
            if (drConfCpto["CCCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@CCCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["CCCve"]));
            }
            if (drConfCpto["polDetRef01"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetRef01", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetRef01"]));
            }
            if (drConfCpto["polDetRef02"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetRef02", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetRef02"]));
            }
            if (drConfCpto["polDetRef03"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetRef03", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetRef03"]));
            }
            if (drConfCpto["polDetRef04"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetRef04", DbType.String, 10, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetRef04"]));
            }

            //FECHAS
            if (drConfCpto["polDetFecMov"].ToString() != "")
            {
                DateTime date1 = new DateTime();
                string sFecMov;
                date1 = Convert.ToDateTime(drConfCpto["polDetFecMov"]);
                sFecMov = date1.ToString("yyyyMMdd");
                ProcBD.AgregarParametrosProcedimiento("@polDetFecMov", DbType.String, 100, ParameterDirection.Input, sFecMov);
            }

            if (drConfCpto["polDetFecVenc"].ToString() != "")
            {
                DateTime date2 = new DateTime();
                string sFecVenc;
                date2 = Convert.ToDateTime(drConfCpto["polDetFecVenc"]);
                sFecVenc = date2.ToString("yyyyMMdd");
                ProcBD.AgregarParametrosProcedimiento("@polDetFecVenc", DbType.String, 100, ParameterDirection.Input, sFecVenc);
            }

            ProcBD.AgregarParametrosProcedimiento("@polDetCoA", DbType.Int32, 0, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetCoA"]));

            if (drConfCpto["polDetImp"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetImp", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToDecimal(drConfCpto["polDetImp"]));
            }
            if (drConfCpto["monCve"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, Convert.ToString(drConfCpto["monCve"]));
            }
            if (drConfCpto["polDetTipCam"].ToString() != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@polDetTipCam", DbType.Decimal, 19, ParameterDirection.Input, Convert.ToString(drConfCpto["polDetTipCam"]));
            }

            ProcBD.AgregarParametrosProcedimiento("@polDetSit", DbType.String, 3, ParameterDirection.Input, "R");


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
    private void FoliadorAsientoContable() {

        DataSet ds = new DataSet();
        ds = ConceptoDefinicion();

        rTxtAsientoContable.Text = "";
        rTxtAsientoContable.Enabled = false;

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            string cptoDefFolTip = ds.Tables[0].Rows[0]["cptoDefFolTip"].ToString();

            //GRET
            //Se comenta codigo para que ya no traiga foliador de Transacción, y en su lugar traiga foliador con prefijo
            //string cptoDefFolVal = ds.Tables[0].Rows[0]["cptoDefFolVal"].ToString();
            string cptoDefFolVal = ds.Tables[0].Rows[0]["cptoDefAsiConFolVal"].ToString();
            if (cptoDefFolTip == "2")
            {
                rTxtAsientoContable.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, cptoDefFolVal, Convert.ToInt32(cptoDefFolTip), "");
                rTxtAsientoContable.Enabled = false;
            }
            else
            {

                if (rTxtAsientoContable.Text.Trim() != "")
                {
                    rTxtAsientoContable.Text = FNGrales.sFoliosAutMan(Pag_sConexionLog, Pag_sCompania, cptoDefFolVal, Convert.ToInt32(cptoDefFolTip), rTxtAsientoContable.Text.Trim());
                }
                rTxtAsientoContable.Enabled = true;
            }
        }
        else {

        }
   }
    private void Suma_TotalCargosAbono()
    {
        decimal cargo = 0, abono = 0;
        DataSet ds = new DataSet();
        ds = (DataSet)Session["dsAsientosSession"];

        foreach (DataRow drConfCpto in ds.Tables[0].Rows)
        {
            if (drConfCpto["polDetCoA"].ToString() == "1")
            {
                cargo = cargo + Convert.ToDecimal(drConfCpto["polDetImp"]);
            }
            if (drConfCpto["polDetCoA"].ToString() == "2")
            {
                abono = abono + Convert.ToDecimal(drConfCpto["polDetImp"]);
            }
        }
        lblTotalCargos.Text = cargo.ToString("C2");
        lblTotalAbonos.Text = abono.ToString("C2");
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

    private string validaEjecutaAccion(ref string sMSGTip)
    {
        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //Concepto
            if (rCboConcepto.SelectedValue == "")
            {
                rCboConcepto.CssClass = "cssTxtInvalid";
                rCboConcepto.BorderWidth = Unit.Pixel(1);
                rCboConcepto.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboConcepto.BorderColor = System.Drawing.Color.Transparent; }

            //Asiento Contable
            if(rTxtAsientoContable.Text == "")
            {
                rTxtAsientoContable.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAsientoContable.CssClass = "cssTxtEnabled"; }

            //Descripcion
            if (rTxtDescripcion.Text == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }
            
            //Fecha
            if (RdDateFecha.SelectedDate.ToString() == "")
            {
                RdDateFecha.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { RdDateFecha.CssClass = "cssTxtEnabled"; }

            //Cifra Control
            if (rNumCifraControl.Text == "")
            {
                rNumCifraControl.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rNumCifraControl.CssClass = "cssTxtEnabled"; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            else
            {
                if (rGdvPolizasDet.Items.Count == 0)
                {
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1019", ref sMSGTip, ref sResult);
                }
            }

            return sResult;
        }


        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvPolizasDet.SelectedItems.Count == 0)
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
        RadGrid objRGrid = new RadGrid();
        objRGrid = rGdvPolizasDet;
 
        string sResult = "";
        int camposInc = 0;
   
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (this.rCboConcepto.SelectedIndex == -1)
            {
                sMSGTip = "2";
                sResult = "Para ejecutar la acción debe seleccionar el concepto.";
                return sResult;
            }
      
            if (rTxtAsientoContable.Text.Trim() == "")
            {
                rTxtAsientoContable.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAsientoContable.CssClass = "cssTxtEnabled"; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;

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

    public string sFecha()
    {
        string Val_TransFec = "";
        DateTime dt = Convert.ToDateTime(RdDateFecha.SelectedDate);

        Val_TransFec = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        return Val_TransFec;



        //string Val_Fec_Inicio = RdDateFecha_Inicio.SelectedDate.ToString();
        //if (Val_Fec_Inicio != "")
        //{
        //    DateTime dt = Convert.ToDateTime(Val_Fec_Inicio);
        //    Val_Fec_Inicio = dt.Year + "/" + dt.Month.ToString().PadLeft(2, '0') + "/" + dt.Day.ToString().PadLeft(2, '0');
        //    ProcBD.AgregarParametrosProcedimiento("@transFecIni", DbType.String, 10, ParameterDirection.Input, Val_Fec_Inicio);
        //}
    }

    #endregion
}