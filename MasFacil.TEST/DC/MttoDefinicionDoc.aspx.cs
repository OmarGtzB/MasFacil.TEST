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

public partial class DC_MttoDefinicionDoc : System.Web.UI.Page
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
    private string Pag_sidM;


    //-----------------------------------------------
    DataTable dtTmpPart = new DataTable();
    DataTable dtTmpPartTwo = new DataTable();
    DataRow drTmpPart;
    ///-----PARAMETRO MANEJA LISTA DE PRECIOS
    //private static string ParmLPrecios = "";


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

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {

        //if (rGdv_Documentos.SelectedItems.Count > 0 && hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        //{
        //    ValoresRegistros();
        //}
        //else if (rGdv_Documentos.SelectedItems.Count == 0 && hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        //{
        //    InicioControlesAll();
        //    PnlsEnabledGral(true);
        //}
        // EjecutaAccionLimpiar();

        //if (rBtnModificar.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
        //{
        //    InicioPagina();
        //}
        //if (rBtnNuevo.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
        //{
        //    InicioPagina();
        //    hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        //    ControlesAccion();
        //}

        EjecutaAccionLimpiar();

    }


    protected void rBtnSeguridad_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "rBtnSeguridad";
        ControlesAccion();

        //if (rGdv_Documentos.SelectedItems.Count > 0)
        //{
        //    var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
        //    string stransDetId = dataItem.GetDataKeyValue("docCve").ToString();
        //    FNMttoDocPermisos.NavigateUrl = "~/DC/MttoDocAutorizacion.aspx?" + "docCve=" + stransDetId;
        //    string script = "function f(){$find(\"" + FNMttoDocPermisos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        //}
        //else
        //{
        //    string sResult = "", sMSGTip = "";
        //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1022", ref sMSGTip, ref sResult);
        //    ShowAlert(sMSGTip, sResult);
        //}

        //////////////////////////////////////////////////////////////////
        //string sResult = "", sMSGTip = "";
        //if (rGdv_Documentos.SelectedItems.Count > 0)
        //{

        //    if (rGdv_Documentos.SelectedItems.Count > 1)
        //    {
        //        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0008", ref sMSGTip, ref sResult);
        //        ShowAlert(sMSGTip, sResult);
        //    }
        //    else
        //    {
        //        var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
        //        string stransDetId = dataItem.GetDataKeyValue("docCve").ToString();
        //        FNMttoDocPermisos.NavigateUrl = "~/DC/MttoDocAutorizacion.aspx?" + "docCve=" + stransDetId;
        //        string script = "function f(){$find(\"" + FNMttoDocPermisos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        //    }


        //}
        //else
        //{

        //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
        //    ShowAlert(sMSGTip, sResult);
        //}
    }



    protected void rBtnDescuentos_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "rBtnDescuentos";
        ControlesAccion();
        //if (rGdv_Documentos.SelectedItems.Count > 0 )
        ////{
        ////    var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
        ////    string stransDetId = dataItem.GetDataKeyValue("docCve").ToString();

        ////    FNMtto.NavigateUrl = "~/DC/MttoDescuentosDocumentos.aspx?" + "docCve=" + stransDetId;
        ////    FNMtto.Title = "Documento Descuentos";
        ////    string script = "function f(){$find(\"" + FNMtto.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        ////}
        ////else 
        ////{

        ////    string sResult = "", sMSGTip = "";
        ////    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1022", ref sMSGTip, ref sResult);
        ////    ShowAlert(sMSGTip, sResult);
        ////}

        //string sResult = "", sMSGTip = "";
        //if (rGdv_Documentos.SelectedItems.Count > 0)
        //{

        //    if (rGdv_Documentos.SelectedItems.Count > 1)
        //    {
        //        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0008", ref sMSGTip, ref sResult);
        //        ShowAlert(sMSGTip, sResult);
        //    }
        //    else
        //    {
        //        var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
        //        string stransDetId = dataItem.GetDataKeyValue("docCve").ToString();

        //        FNMtto.NavigateUrl = "~/DC/MttoDescuentosDocumentos.aspx?" + "docCve=" + stransDetId;
        //        FNMtto.Title = "Documento Descuentos";
        //        string script = "function f(){$find(\"" + FNMtto.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        //    }


        //}
        //else
        //{

        //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
        //    ShowAlert(sMSGTip, sResult);
        //}

    }

    protected void rBtnListaPrecios_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "rBtnListaPrecios";
        ControlesAccion();


        //if (FNGrales.bListPrecio(Pag_sConexionLog, Pag_sCompania))
        //{
        //    if (rGdv_Documentos.SelectedItems.Count > 0)
        //    {

        //        string sResult = "", sMSGTip = "";
        //        if (rGdv_Documentos.SelectedItems.Count > 0)
        //        {

        //            if (rGdv_Documentos.SelectedItems.Count > 1)
        //            {
        //                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0008", ref sMSGTip, ref sResult);
        //                ShowAlert(sMSGTip, sResult);
        //            }
        //            else
        //            {
        //                var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
        //                string stransDetId = dataItem.GetDataKeyValue("docCve").ToString();

        //                FNMtto.NavigateUrl = "~/DC/MttoDocumentoListaP.aspx?" + "docCve=" + stransDetId;
        //                FNMtto.Title = "Documento Lista de Precios";
        //                string script = "function f(){$find(\"" + FNMtto.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
        //            }


        //        }
        //        else
        //        {

        //            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
        //            ShowAlert(sMSGTip, sResult);
        //        }
        //    }
        //    else
        //    {
        //        string sResult = "", sMSGTip = "";
        //        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1022", ref sMSGTip, ref sResult);
        //        ShowAlert(sMSGTip, sResult);
        //    }
        //}
        //else
        //{
        //    string sResult = "", sMSGTip = "";
        //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1023", ref sMSGTip, ref sResult);
        //    ShowAlert(sMSGTip, sResult);
        //}


    }
    
    protected void rBtnImpuestos_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "rBtnImpuestos";
        ControlesAccion();

    }


    protected void rBtnUsoCFDI_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "rBtnUsoCFDI";
        ControlesAccion();
    }

    protected void rBtnNewRef_Click(object sender, ImageButtonClickEventArgs e)
    {
        rBtnNewRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";

        hdfBtnAccionR.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();

        rBtnEdiRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnDelRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnAceptarR.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";

        txtValRef.Enabled = true;
        txtValRef.Focus();

        //controles accion

        rGdv_Referencias.ClientSettings.Selecting.AllowRowSelect = false;

        for (int i = 0; i < rGdv_Referencias.Items.Count; i++)
        {
            rGdv_Referencias.Items[i].SelectableMode = GridItemSelectableMode.None;
        }

        rGdv_Referencias.AllowMultiRowSelection = false;

    }

    protected void rBtnEdiRef_Click(object sender, ImageButtonClickEventArgs e)
    {
        rBtnNewRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";

        hdfBtnAccionR.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();

        rBtnEdiRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        rBtnDelRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnAceptarR.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";

        txtValRef.Enabled = true;

        rGdv_Referencias.ClientSettings.Selecting.AllowRowSelect = true;

        for (int i = 0; i < rGdv_Referencias.Items.Count; i++)
        {
            rGdv_Referencias.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
        }

        rGdv_Referencias.AllowMultiRowSelection = false;
    }

    protected void rBtnDelRef_Click(object sender, ImageButtonClickEventArgs e)
    {
        rBtnNewRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";

        hdfBtnAccionR.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();

        rBtnEdiRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnDelRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
        rBtnAceptarR.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";

        txtValRef.Enabled = false;

        rGdv_Referencias.ClientSettings.Selecting.AllowRowSelect = true;

        for (int i = 0; i < rGdv_Referencias.Items.Count; i++)
        {
            rGdv_Referencias.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
        }

        rGdv_Referencias.AllowMultiRowSelection = true;

    }

    protected void rBtnAceptarR_Click(object sender, ImageButtonClickEventArgs e)
    {

        if (hdfBtnAccionR.Value != "")
        {


            if (txtValRef.Text.Trim() == "" && (hdfBtnAccionR.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() || hdfBtnAccionR.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()))
            {
                ShowAlert("2", "Ingrese Referencia");
                txtValRef.Focus();
            }
            else
            {
                if (hdfBtnAccionR.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                {
                    addNewRef();
                }
                else if (hdfBtnAccionR.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    ediNewRef();
                }
                else if (hdfBtnAccionR.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
                {
                    delNewRef();
                }
                else if (hdfBtnAccionR.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
                {

                }

                rGdv_Referencias.DataBind();
                txtValRef.Text = "";
                txtValRef.Enabled = false;

                //Limpiar 
                hdfBtnAccionR.Value = "";
                rBtnNewRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                rBtnEdiRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                rBtnDelRef.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                rBtnAceptarR.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";

                rGdv_Referencias.ClientSettings.Selecting.AllowRowSelect = false;
                for (int i = 0; i < rGdv_Referencias.Items.Count; i++)
                {
                    rGdv_Referencias.Items[i].SelectableMode = GridItemSelectableMode.None;
                }
                rGdv_Referencias.AllowMultiRowSelection = false;

            }

        }


    }

    protected void rGdv_Referencias_SelectedIndexChanged(object sender, EventArgs e)
    {

        var dataItem = rGdv_Referencias.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            //if (hdfBtnAccionR.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            //{

                txtValRef.Text = dataItem["revaDes"].Text;
           // }
        }

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


    //=====> Seleccion de Documento
    protected void rGdv_Documentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        //CFDI_Clear();
        ValoresRegistros();
    }


    protected void rCboManejoFolio_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FnCtlsFillIn.RadComboBox_FoliadoresSinDeshabilitar(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboManejoFolio.SelectedValue), ref rcboFoliador, true, false);
    }


    //=====> Check Actualiza Modulos
    protected void rBtnActInvAplica_CheckedChanged(object sender, EventArgs e)
    {
        Check_Inventarios();
    }
    protected void rBtnActInvNo_CheckedChanged(object sender, EventArgs e)
    {
        Check_Inventarios();
        rCboConceptoInventarios.BorderColor = System.Drawing.Color.Transparent;

    }
    protected void rBtnActInvGenera_CheckedChanged(object sender, EventArgs e)
    {
        Check_Inventarios();
    }


    protected void rBtnActCxcAplica_CheckedChanged(object sender, EventArgs e)
    {
        Check_CXC();
    }
    protected void rBtnActCxcNo_CheckedChanged(object sender, EventArgs e)
    {
        Check_CXC();
        rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Transparent;
    }
    protected void rBtnActCxcGenera_CheckedChanged(object sender, EventArgs e)
    {
        Check_CXC();
    }
    
    protected void rBtnActContAplica_CheckedChanged(object sender, EventArgs e)
    {
        Chek_Contabilidad();
    }
    protected void rBtnActContNo_CheckedChanged(object sender, EventArgs e)
    {
        Chek_Contabilidad();
        rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Transparent;
    }
    protected void rBtnActContGenera_CheckedChanged(object sender, EventArgs e)
    {
        Chek_Contabilidad();
    }


    //=====> Documentos Derivados
    protected void rCboGenDoc1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoGenera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docGenId", DbType.String, 10, ParameterDirection.Input, rCboGenDoc1.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (Convert.ToString(ds.Tables[0].Rows[0]["docGenSit"]) == "2")
        {
            rCboDocumento1.ClearSelection();
            rCboDocumento1.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento1.Enabled = false;
        }
        else
        {
            rCboDocumento1.Enabled = true;
        }
    }

    protected void rCboGenDoc2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoGenera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docGenId", DbType.String, 10, ParameterDirection.Input, rCboGenDoc2.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (Convert.ToString(ds.Tables[0].Rows[0]["docGenSit"]) == "2")
        {
            rCboDocumento2.Enabled = false;
            rCboDocumento2.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento2.ClearSelection();
        }
        else
        {
            rCboDocumento2.Enabled = true;
        }
    }

    protected void rCboGenDoc3_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoGenera";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docGenId", DbType.String, 10, ParameterDirection.Input, rCboGenDoc3.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (Convert.ToString(ds.Tables[0].Rows[0]["docGenSit"]) == "2")
        {
            rCboDocumento3.Enabled = false;
            rCboDocumento3.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento3.ClearSelection();
        }
        else
        {
            rCboDocumento3.Enabled = true;
        }
    }



    protected void CheckGenFac_CheckedChanged(object sender, EventArgs e)
    {
        BROWSER_CHECK();
    }

    protected void CheckEnvCorr_CheckedChanged(object sender, EventArgs e)
    {
         if (CheckEnvCorr.Checked == true)
        {
            CheckArchXml.Checked = true;
            CheckArchXml.Enabled = false;
            CheckPDF.Checked = true;
            CheckPDF.Enabled = false;
            RadTxtCopiA.Text="";
            RadTxtCopiA.Enabled = true;
        }else
        {
            CheckArchXml.Checked = false;
            CheckArchXml.Enabled = false;
            CheckPDF.Checked = false;
            CheckPDF.Enabled = false;
            RadTxtCopiA.Text = "";
            RadTxtCopiA.Enabled = false;
        }
    }


    protected void rGdv_Variables_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataItem = rGdv_Variables.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            if (hdfBtnAccionV.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                txtValVar.Text = dataItem["revaDes"].Text;
            }
        }


    }

    protected void rBtnNewVar_Click(object sender, ImageButtonClickEventArgs e)
    {
        rBtnNewVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";

        hdfBtnAccionV.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();

        rBtnEdiVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnDelVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnAceptarV.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";

        txtValVar.Enabled = true;
        txtValVar.Focus();

        //controles accion

        rGdv_Variables.ClientSettings.Selecting.AllowRowSelect = false;

        for (int i = 0; i < rGdv_Variables.Items.Count; i++)
        {
            rGdv_Variables.Items[i].SelectableMode = GridItemSelectableMode.None;
        }

        rGdv_Variables.AllowMultiRowSelection = false;


    }

    protected void rBtnEdiVar_Click(object sender, ImageButtonClickEventArgs e)
    {
        rBtnNewVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";

        hdfBtnAccionV.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();

        rBtnEdiVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        rBtnDelVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rBtnAceptarV.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";

        txtValVar.Enabled = true;

        rGdv_Variables.ClientSettings.Selecting.AllowRowSelect = true;

        for (int i = 0; i < rGdv_Variables.Items.Count; i++)
        {
            rGdv_Variables.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
        }

        rGdv_Variables.AllowMultiRowSelection = false;
    }

    protected void rBtnDelVar_Click(object sender, ImageButtonClickEventArgs e)
    {

        rBtnNewVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";

        hdfBtnAccionV.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();

        rBtnEdiVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnDelVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
        rBtnAceptarV.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";

        txtValVar.Enabled = false;

        rGdv_Variables.ClientSettings.Selecting.AllowRowSelect = true;

        for (int i = 0; i < rGdv_Variables.Items.Count; i++)
        {
            rGdv_Variables.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
        }

        rGdv_Variables.AllowMultiRowSelection = true;

    }

    protected void rBtnAceptarV_Click(object sender, ImageButtonClickEventArgs e)
    {

        if (hdfBtnAccionV.Value != "")
        {


            if (txtValVar.Text.Trim() == "" && (hdfBtnAccionV.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() || hdfBtnAccionV.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()))
            {
                ShowAlert("2", "Ingrese Variable");
                txtValVar.Focus();
            }
            else
            {
                if (hdfBtnAccionV.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                {
                    addNewVar();
                }
                else if (hdfBtnAccionV.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    ediNewVar();
                }
                else if (hdfBtnAccionV.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
                {
                    delNewVar();
                }
                else if (hdfBtnAccionV.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
                {

                }

                rGdv_Variables.DataBind();
                txtValVar.Text = "";
                txtValVar.Enabled = false;

                //Limpiar 
                hdfBtnAccionV.Value = "";
                rBtnNewVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                rBtnEdiVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                rBtnDelVar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                rBtnAceptarV.Image.Url = "~/Imagenes/IcoBotones/IcoBtnAutorizar.png";

                rGdv_Variables.ClientSettings.Selecting.AllowRowSelect = false;
                for (int i = 0; i < rGdv_Variables.Items.Count; i++)
                {
                    rGdv_Variables.Items[i].SelectableMode = GridItemSelectableMode.None;
                }
                rGdv_Variables.AllowMultiRowSelection = false;

            }

        }

    }


    #endregion

    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
        {
            Pag_sidM = Request.QueryString["idM"];
        }
    }

    private void addRadWin()
    {
        FnCtrlsRadWindows.cRadWindowsCompanias(Page, ref RadWindowManagerPage);
    }

    private void InicioPagina()
    {
        TituloPagina();
        hdfBtnAccion.Value = "";
        ControlesAccion();

        LlenaGridDocumentos();
        InicioControlesAll();

        PnlsEnabledGral(false);
        RadTxtCopiA.Text = "";

        rGdv_Referencias.ClientSettings.Selecting.AllowRowSelect = false;

        for (int i = 0; i < rGdv_Referencias.Items.Count; i++)
        {
            rGdv_Referencias.Items[i].SelectableMode = GridItemSelectableMode.None;
        }
        rGdv_Referencias.AllowMultiRowSelection = false;

        rGdv_Variables.ClientSettings.Selecting.AllowRowSelect = false;

        for (int i = 0; i < rGdv_Variables.Items.Count; i++)
        {
            rGdv_Variables.Items[i].SelectableMode = GridItemSelectableMode.None;
        }

        rGdv_Variables.AllowMultiRowSelection = false;

        rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Documentos.AllowMultiRowSelection = true;

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
        FNGrales.bTitleDesc(Page, "Definicion de Documentos", "PnlMPFormTituloApartado");
    }

    private void InicioControles()
    {

        //Datos Generales
        rTxtDocCve.Text = "";
        rTxtDocDes.Text = "";


        rBtnValCreSi.Checked = false;
        rBtnValCreNo.Checked = true;
        rBtnValCreAut.Checked = false;

        rBtnChkProPar.Checked = false;
        //rBtnChkDescGlbl.Checked = false;
        rBtnReqAut.Checked = true;
        rBtnValExt.Checked = true;

        rcboFoliador.ClearSelection();

        //Actualiza Modulos
        rBtnActInvAplica.Checked = false;
        rBtnActInvNo.Checked = true;
        rBtnActInvGenera.Checked = false;
        rCboConceptoInventarios.Enabled = false;
        rCboConceptoInventarios.ClearSelection();

        rBtnActCxcAplica.Checked = false;
        rBtnActCxcNo.Checked = true;
        rBtnActCxcGenera.Checked = false;
        rCboConceptoCuentasxCobrar.Enabled = false;
        rCboConceptoCuentasxCobrar.ClearSelection();

        rBtnActContAplica.Checked = false;
        rBtnActContNo.Checked = true;
        rBtnActContGenera.Checked = false;
        rCboConceptoContabilidad.Enabled = false;
        rCboConceptoContabilidad.ClearSelection();


        rCboGenDoc1.SelectedValue = "2";
        rCboGenDoc2.SelectedValue = "2";
        rCboGenDoc3.SelectedValue = "2";
        rCboDocumento1.SelectedIndex = -1;
        rCboDocumento1.ClearSelection();
        rCboDocumento2.SelectedIndex = -1;
        rCboDocumento2.ClearSelection();
        rCboDocumento3.SelectedIndex = -1;
        rCboDocumento3.ClearSelection();
        rCboDocumento1.Enabled = false;
        rCboDocumento2.Enabled = false;
        rCboDocumento3.Enabled = false;

        rCboConceptoEstadistico.Enabled = false;
        rCboConceptoEstadistico.ClearSelection();
        rCboFormaAplicacion.Enabled = false;

        rCboFormaAplicacion.ClearSelection();
        rCboConceptoEstadistico.BorderWidth = Unit.Pixel(1);
        rCboConceptoEstadistico.BorderColor = System.Drawing.Color.Transparent;

        rcboFoliador.ClearSelection();
        rcboFoliador.BorderWidth = Unit.Pixel(1);
        rcboFoliador.BorderColor = System.Drawing.Color.Transparent;

        rCboManejaDescuento.ClearSelection();
        rCboManejaDescuento.BorderWidth = Unit.Pixel(1);
        rCboManejaDescuento.BorderColor = System.Drawing.Color.Transparent;

        rCboManejaListaPrecios.ClearSelection();
        rCboManejaListaPrecios.BorderWidth = Unit.Pixel(1);
        rCboManejaListaPrecios.BorderColor = System.Drawing.Color.Transparent;
        
        radTotal.Checked = true;
        radSurtido.Checked = false;
        radPedido.Checked = false;
        radPendiente.Checked = false;

        rbtnManejoTextilSi.Checked = false;
        rbtnManejoTextilNo.Checked = true;

        rbtnAplicaIncotermSi.Checked = false;
        rbtnAplicaIncotermNo.Checked = true;

    }

    private void InicioControlesAll()
    {
        InicioControles();
        LlenaRadImageGallery_FormatoImpresion();
        LlenaListaFormatoDocumento_OpcPantalla();
        LlenaComboBox();

        LLenaReferenciasVariables();

        PnlsEnabledGral(false);
    }

   
    private void LlenaGridDocumentos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref rGdv_Documentos, ds);
    }
    private void LlenaRadImageGallery_FormatoImpresion()
    {
        RadImageGallery1.DataSource = GetDataTableFormatosImpresion();
        RadImageGallery1.DataBind();
    }
    private void LlenaListaFormatoDocumento_OpcPantalla()
    {
        this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(51, "");
        DataListDocFormatoRegOpcPantalla.DataBind();
    }
    private void LlenaComboBox()
    {
        FnCtlsFillIn.RabComboBox_Modulo_IN(Pag_sConexionLog, Pag_sCompania, ref rCboConceptoInventarios, true, false);
        FnCtlsFillIn.RabComboBox_Modulo_CXC(Pag_sConexionLog, Pag_sCompania, ref rCboConceptoCuentasxCobrar, true, false);
        FnCtlsFillIn.RabComboBox_Modulo_CG(Pag_sConexionLog, Pag_sCompania, ref rCboConceptoContabilidad, true, false);



        FnCtlsFillIn.RadComboBox_ManejoFolios(Pag_sConexionLog, Pag_sCompania, ref rCboManejoFolio, true, true, "1");

        FnCtlsFillIn.RadComboBox_FoliadoresSinDeshabilitar(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboManejoFolio.SelectedValue), ref rcboFoliador, true, false);

        FnCtlsFillIn.RabComboBox_GeneraDocumentos(Pag_sConexionLog, Pag_sCompania, ref rCboGenDoc1, true, true, "2");
        FnCtlsFillIn.RabComboBox_GeneraDocumentos(Pag_sConexionLog, Pag_sCompania, ref rCboGenDoc2, true, true, "2");
        FnCtlsFillIn.RabComboBox_GeneraDocumentos(Pag_sConexionLog, Pag_sCompania, ref rCboGenDoc3, true, true, "2");

        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento1, true, false);
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento2, true, false);
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento3, true, false);

        FnCtlsFillIn.RabComboBox_ConceptosEstadisticos(Pag_sConexionLog, Pag_sCompania, ref rCboConceptoEstadistico, true, false);

        FnCtlsFillIn.RadComboBox_FormaAplicacion(Pag_sConexionLog, Pag_sCompania, ref rCboFormaAplicacion, true, false);

        FnCtlsFillIn.RadComboBox_ManejaDescuentos(Pag_sConexionLog, Pag_sCompania, ref rCboManejaDescuento, true, false);

        FnCtlsFillIn.RadComboBox_ManejaListaDePrecios(Pag_sConexionLog, Pag_sCompania, ref rCboManejaListaPrecios, true, false);


    }

    private void LLenaReferenciasVariables()
    {

        rGdv_Referencias.DataSource = llenadatalistVarRef(1);
        rGdv_Referencias.DataBind();

        rGdv_Variables.DataSource = llenadatalistVarRef(2);
        rGdv_Variables.DataBind();

    }

    private void PnlsEnabledGral(bool bEnabled)
    {

        pnlDatosGenerales.Enabled = bEnabled;
        pnlActualizaModulos.Enabled = bEnabled;
        pnlDocumentosDerivados.Enabled = bEnabled;
        pnlRefVar.Enabled = bEnabled;
        pnlLayoutImpresion.Enabled = bEnabled;
        pnlFormatoRegistro.Enabled = bEnabled;
        rCboManejaDescuento.Enabled = bEnabled;

        rCboManejaListaPrecios.Enabled = bEnabled;

        rCboManejaDescuento.Enabled = bEnabled;

        //Aplicacion Estadistica
        rCboConceptoEstadistico.Enabled = bEnabled;
        radTotal.Enabled = bEnabled;
        radSurtido.Enabled = bEnabled;
        rCboFormaAplicacion.Enabled = bEnabled;
        radPedido.Enabled = bEnabled;
        radPendiente.Enabled = bEnabled;

        //RefVar

        txtValRef.Enabled = false;
        txtValVar.Enabled = false;


    }



    private void ValoresRegistros()
    {

        pnlCFDI.Enabled = false;
        //if (hdfBtnAccion.Value == "2" || hdfBtnAccion.Value == "")
        //{
            InicioControles();
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                //InicioControles();
                //PnlsEnabledGral(true);
                //rTxtDocCve.Enabled = false;
            }
            var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, dataItem["docCve"].Text);
                ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
                if (FnValAdoNet.bDSRowsIsFill(ds) == true)
                {
                    ValoresDatosGenerales(ds); //
                    ValoresActualizaModulos(ds); //
                    ValoresDocumentosDerivados(ds);//
                    ManListaPrecio(); //
                    GeneraCFDI(ds); //Alfonso Flores
                    ValoresFormatoRegistro(Convert.ToString(ds.Tables[0].Rows[0]["docCve"]));
                    ValoresLayoutImpresion(Convert.ToString(ds.Tables[0].Rows[0]["formImpCve"]));
                    ApliEstadistica(Convert.ToString(ds.Tables[0].Rows[0]["docCve"]));
                    ValoresReferenciasVariables();
                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                    {
                        //pnlCFDI.Enabled = true;
                        //BROWSER_CHECK();
                    }
                }
            }


        //}
    }
    private void ValoresDatosGenerales(DataSet ds)
    {

        rTxtDocCve.Text = Convert.ToString(ds.Tables[0].Rows[0]["docCve"]);
        rTxtDocDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["docDes"]);
        rCboManejoFolio.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docFolTip"]);
        FnCtlsFillIn.RadComboBox_FoliadoresSinDeshabilitar(Pag_sConexionLog, Pag_sCompania, Convert.ToInt64(rCboManejoFolio.SelectedValue), ref rcboFoliador, true, false);
        rcboFoliador.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["folVal"]).ToString();
        rCboManejaDescuento.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docDescGlb"]).ToString();

        if (Convert.ToString(ds.Tables[0].Rows[0]["docValCred"]) == "1")
        {
            rBtnValCreSi.Checked = true;
            rBtnValCreNo.Checked = false;
            rBtnValCreAut.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docValCred"]) == "2")
        {
            rBtnValCreNo.Checked = true;
            rBtnValCreSi.Checked = false;
            rBtnValCreAut.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docValCred"]) == "3")
        {
            rBtnValCreAut.Checked = true;
            rBtnValCreNo.Checked = false;
            rBtnValCreSi.Checked = false;
        }


        if (Convert.ToString(ds.Tables[0].Rows[0]["docTip"]) == "1")
        {
            rBtnTipDocDeb.Checked = true;
            rBtnTipDocCre.Checked = false;

        }
        else
        if (Convert.ToString(ds.Tables[0].Rows[0]["docTip"]) == "2")
        {
            rBtnTipDocCre.Checked = true;
            rBtnTipDocDeb.Checked = false;
        }


        if (Convert.ToString(ds.Tables[0].Rows[0]["docProcParc"]) == "1")
        {
            rBtnChkProPar.Checked = true;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docProcParc"]) == "2")
        {
            rBtnChkProPar.Checked = false;
        }



        if (Convert.ToString(ds.Tables[0].Rows[0]["docTextilApli"]) == "1")
        {
            rbtnManejoTextilSi.Checked = true;
            rbtnManejoTextilNo.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docTextilApli"]) == "2")
        {
            rbtnManejoTextilSi.Checked = false;
            rbtnManejoTextilNo.Checked = true;
        }

        //mash 

        if (Convert.ToString(ds.Tables[0].Rows[0]["docIncoterm"]) == "1")
        {
            rbtnAplicaIncotermSi.Checked = true;
            rbtnAplicaIncotermNo.Checked = false;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docIncoterm"]) == "2")
        {
            rbtnAplicaIncotermSi.Checked = false;
            rbtnAplicaIncotermNo.Checked = true;
        }




        if (Convert.ToString(ds.Tables[0].Rows[0]["docReqAut"]) == "1")
        {
            rBtnReqAut.Checked = true;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docReqAut"]) == "2")
        {
            rBtnReqAut.Checked = false;
        }


        if (Convert.ToString(ds.Tables[0].Rows[0]["docValExis"]) == "1")
        {
            rBtnValExt.Checked = true;
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docValExis"]) == "2")
        {
            rBtnValExt.Checked = false;
        }



    }
    private void ValoresActualizaModulos(DataSet ds)
    {

        if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "1")
        {
            rBtnActInvAplica.Checked = true;
            rBtnActInvNo.Checked = false;
            rBtnActInvGenera.Checked = false;
            //rCboConceptoInventarios.Enabled = true;
            rCboConceptoInventarios.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdInv"]);
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "2")
        {
            rBtnActInvNo.Checked = true;
            rBtnActInvAplica.Checked = false;
            rBtnActInvGenera.Checked = false;
            //rCboConceptoInventarios.Enabled = false;
            rCboConceptoInventarios.ClearSelection();
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActInv"]) == "3")
        {
            rBtnActInvGenera.Checked = true;
            rBtnActInvNo.Checked = false;
            rBtnActInvAplica.Checked = false;
           // rCboConceptoInventarios.Enabled = true;
            rCboConceptoInventarios.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdInv"]);

        }





        if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "1")
        {
            rBtnActCxcAplica.Checked = true;
            rBtnActCxcNo.Checked = false;
            rBtnActCxcGenera.Checked = false;
            //rCboConceptoCuentasxCobrar.Enabled = true;
            rCboConceptoCuentasxCobrar.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdCXC"]);
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "2")
        {
            rBtnActCxcNo.Checked = true;
            rBtnActCxcAplica.Checked = false;
            rBtnActCxcGenera.Checked = false;
           // rCboConceptoCuentasxCobrar.Enabled = false;
            rCboConceptoCuentasxCobrar.ClearSelection();
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCXC"]) == "3")
        {
            rBtnActCxcGenera.Checked = true;
            rBtnActCxcAplica.Checked = false;
            rBtnActCxcNo.Checked = false;
           // rCboConceptoCuentasxCobrar.Enabled = true;
            rCboConceptoCuentasxCobrar.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdCXC"]);
        }


        if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "1")
        {
            rBtnActContAplica.Checked = true;
            rBtnActContNo.Checked = false;
            rBtnActContGenera.Checked = false;
            //rCboConceptoContabilidad.Enabled = true;
            rCboConceptoContabilidad.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdCont"]);
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "2")
        {
            rBtnActContNo.Checked = true;
            rBtnActContAplica.Checked = false;
            rBtnActContGenera.Checked = false;
            //rCboConceptoContabilidad.Enabled = false;
            rCboConceptoContabilidad.ClearSelection();
        }
        else if (Convert.ToString(ds.Tables[0].Rows[0]["docActCont"]) == "3")
        {

            rBtnActContGenera.Checked = true;
            rBtnActContNo.Checked = false;
            rBtnActContAplica.Checked = false;
            //rCboConceptoContabilidad.Enabled = true;
            rCboConceptoContabilidad.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoIdCont"]);
        }



    }
    private void ValoresDocumentosDerivados(DataSet ds)
    {

        rCboGenDoc1.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docGenId1"]);
        rCboGenDoc2.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docGenId2"]);
        rCboGenDoc3.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docGenId3"]);


        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento1, true, false);
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento2, true, false);
        FnCtlsFillIn.RabComboBox_Documentos(Pag_sConexionLog, Pag_sCompania, ref rCboDocumento3, true, false);


        if (Convert.ToString(ds.Tables[0].Rows[0]["docCve1"]) != "")
        {
            rCboDocumento1.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docCve1"]);
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                //rCboDocumento1.Enabled = true;
            }

        }
        if (Convert.ToString(ds.Tables[0].Rows[0]["docCve2"]) != "")
        {
            rCboDocumento2.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docCve2"]);
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                //rCboDocumento2.Enabled = true;
            }

        }
        if (Convert.ToString(ds.Tables[0].Rows[0]["docCve3"]) != "")
        {
            rCboDocumento3.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docCve3"]);
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                //rCboDocumento3.Enabled = true;
            }

        }
       



    }

    private void ManListaPrecio()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rTxtDocCve.Text);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds) == true)
        {
            rCboManejaListaPrecios.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["lisPreCve"]);
        }
        else
        {
            rCboManejaListaPrecios.ClearSelection();
        }


    }




    private void ApliEstadistica(string CveDoc)
    {


        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, CveDoc);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds) == true)
        {
            //Estadistico//
            if (Convert.ToString(ds.Tables[0].Rows[0]["cptoEstId"]) != "")
            {
                rCboConceptoEstadistico.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["cptoEstId"]);
            }
            if (Convert.ToString(ds.Tables[0].Rows[0]["docApliEstApli"]) != "")
            {
                rCboFormaAplicacion.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["docApliEstApli"]);
            }

            if (Convert.ToString(ds.Tables[0].Rows[0]["docApliEstMan"]) != "")
            {
                if (Convert.ToString(ds.Tables[0].Rows[0]["docApliEstMan"]) == "1")
                {
                    radTotal.Checked = true;
                    radSurtido.Checked = false;
                    radPedido.Checked = false;
                    radPendiente.Checked = false;

                }
                else if (Convert.ToString(ds.Tables[0].Rows[0]["docApliEstMan"]) == "2")
                {
                    radTotal.Checked = false;
                    radSurtido.Checked = true;
                    radPedido.Checked = false;
                    radPendiente.Checked = false;
                }
                else if (Convert.ToString(ds.Tables[0].Rows[0]["docApliEstMan"]) == "3")
                {

                    radTotal.Checked = false;
                    radSurtido.Checked = false;
                    radPedido.Checked = true;
                    radPendiente.Checked = false;
                }
                else if (Convert.ToString(ds.Tables[0].Rows[0]["docApliEstMan"]) == "4")
                {
                    radTotal.Checked = false;
                    radSurtido.Checked = false;
                    radPedido.Checked = false;
                    radPendiente.Checked = true;
                }



            }
        }

    }
    private void ValoresFormatoRegistro(string DocCve)
    {


        this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(50, DocCve);
        DataListDocFormatoRegOpcPantalla.DataBind();
    }
    private void ValoresLayoutImpresion(string frmCve)
    {
        int contador = 0;
        foreach (ImageGalleryItem item in RadImageGallery1.Items)
        {
            if (item.Title == frmCve)
            {
                RadImageGallery1.CurrentItemIndex = contador;
            }
            contador++;
        }


    }
    private void ValoresReferenciasVariables()
    {
        rGdv_Referencias.DataSource = llenadatalistVarRef(1);
        rGdv_Referencias.DataBind();

        rGdv_Variables.DataSource = llenadatalistVarRef(2);
        rGdv_Variables.DataBind();
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

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                LlenaListaFormatoDocumento_OpcPantalla();
            }

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }


    }

    private void EjecutaSpAcciones()
    {

        //============== VALORES DATOS GENERALES ================
        int vDocValCred = 0;
        int vTipoDoc = 0;
        //MASH
        int vAplicaIncoterm = 0;

        int vProcParcial = 1;
        int vDescGlobal = 1;
        int vReqAutorizacion = 1;
        int vReqExistencias = 1;

        int vAplicaEstadistica = 0;


        //Valores Valida Credito
        if (rBtnValCreSi.Checked == true)
        {
            vDocValCred = 1;
        }
        else if (rBtnValCreNo.Checked == true)
        {
            vDocValCred = 2;
        }
        else if (rBtnValCreAut.Checked == true)
        {
            vDocValCred = 3;
        }

        //Valores Tipo de Credito

        if (rBtnTipDocCre.Checked == true)
        {
            vTipoDoc = 2;
        }
        else if (rBtnTipDocDeb.Checked == true)
        {
            vTipoDoc = 1;
        }

        //MASH Valida Incoterm


        if (rbtnAplicaIncotermSi.Checked == true)
        {
            vAplicaIncoterm = 2;
        }
        else if (rbtnAplicaIncotermSi.Checked == true)
        {
            vAplicaIncoterm = 1;
        }




        if (rBtnChkProPar.Checked == false)
        {
            vProcParcial = 2;
        }

        //if (rBtnChkDescGlbl.Checked == false)
        //{
        //    //rCboManejaDescuento.SelectedValue = vDescGlobal;
        //    vDescGlobal = 2;
        //}

        if (rBtnReqAut.Checked == false)
        {
            vReqAutorizacion = 2;
        }

        if (rBtnValExt.Checked == false)
        {
            vReqExistencias = 2;
        }


        //============== VALORES FORMATO DE IMPRESION ================
        string sCveFormatoDoc = getValueFormDoc();


        //============== VALORES ACTUALIZA MODULOS ================
        int vDocActInv = 0;
        int vDocActCXC = 0;
        int vDocActCont = 0;


        if (rBtnActInvAplica.Checked == true)
        {
            vDocActInv = 1;
        }
        else if (rBtnActInvNo.Checked == true)
        {
            vDocActInv = 2;
        }
        else if (rBtnActInvGenera.Checked == true)
        {
            vDocActInv = 3; 
        }


        if (rBtnActCxcAplica.Checked == true)
        {
            vDocActCXC = 1;
        }
        else if (rBtnActCxcNo.Checked == true)
        {
            vDocActCXC = 2;
        }
        else if (rBtnActCxcGenera.Checked == true)
        {
            vDocActCXC = 3;
        }

        if (rBtnActContAplica.Checked == true)
        {
            vDocActCont = 1;
        }
        else if (rBtnActContNo.Checked == true)
        {
            vDocActCont = 2;
        }
        else if (rBtnActContGenera.Checked == true)
        {
            vDocActCont = 3;
        }

        //APLICACION ESTADISTICA

        if (radTotal.Checked == true)
        {
            vAplicaEstadistica = 1;
        }
        else if (radSurtido.Checked == true)
        {
            vAplicaEstadistica = 2;
        }
        else if (radPedido.Checked == true)
        {
            vAplicaEstadistica = 3;
        }
        else if (radPendiente.Checked == true)
        {
            vAplicaEstadistica = 4;
        }


        try
        {

            DataSet ds = new DataSet();

            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);

            //===>Datos Generales
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rTxtDocCve.Text);
            ProcBD.AgregarParametrosProcedimiento("@docDes", DbType.String, 50, ParameterDirection.Input, rTxtDocDes.Text);
            ProcBD.AgregarParametrosProcedimiento("@docFolTip", DbType.Int64, 0, ParameterDirection.Input, rCboManejoFolio.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@folVal", DbType.String, 10, ParameterDirection.Input, rcboFoliador.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@docValCred", DbType.Int64, 0, ParameterDirection.Input, vDocValCred);
            ProcBD.AgregarParametrosProcedimiento("@docTip", DbType.Int64, 0, ParameterDirection.Input, vTipoDoc);
            ProcBD.AgregarParametrosProcedimiento("@docProcParc", DbType.Int64, 0, ParameterDirection.Input, vProcParcial);
            ProcBD.AgregarParametrosProcedimiento("@docDescGlb", DbType.Int64, 0, ParameterDirection.Input, rCboManejaDescuento.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@docReqAut", DbType.Int64, 0, ParameterDirection.Input, vReqAutorizacion);
            ProcBD.AgregarParametrosProcedimiento("@docValExis", DbType.Int64, 0, ParameterDirection.Input, vReqExistencias);

            //===>Layout Impresion
            ProcBD.AgregarParametrosProcedimiento("@formImpCve", DbType.String, 10, ParameterDirection.Input, sCveFormatoDoc);

            //===>Actualiza Modulos
            ProcBD.AgregarParametrosProcedimiento("@docActInv", DbType.Int64, 0, ParameterDirection.Input, vDocActInv);
            if (rBtnActInvNo.Checked == false)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoIdInv", DbType.Int64, 0, ParameterDirection.Input, rCboConceptoInventarios.SelectedValue);
            }
            ProcBD.AgregarParametrosProcedimiento("@docActCXC", DbType.Int64, 0, ParameterDirection.Input, vDocActCXC);
            if (rBtnActCxcNo.Checked == false)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoIdCXC", DbType.Int64, 0, ParameterDirection.Input, rCboConceptoCuentasxCobrar.SelectedValue);
            }
            ProcBD.AgregarParametrosProcedimiento("@docActCont", DbType.Int64, 0, ParameterDirection.Input, vDocActCont);
            if (rBtnActContNo.Checked == false)
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoIdCont", DbType.Int64, 0, ParameterDirection.Input, rCboConceptoContabilidad.SelectedValue);
            }

            //===>Documentos Derivados
            ProcBD.AgregarParametrosProcedimiento("@docGenId1", DbType.Int64, 0, ParameterDirection.Input, rCboGenDoc1.SelectedValue);
            if (rCboDocumento1.Enabled == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docCve1", DbType.String, 10, ParameterDirection.Input, rCboDocumento1.SelectedValue);
            }
            ProcBD.AgregarParametrosProcedimiento("@docGenId2", DbType.Int64, 0, ParameterDirection.Input, rCboGenDoc2.SelectedValue);
            if (rCboDocumento2.Enabled == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docCve2", DbType.String, 10, ParameterDirection.Input, rCboDocumento2.SelectedValue);
            }
            ProcBD.AgregarParametrosProcedimiento("@docGenId3", DbType.Int64, 0, ParameterDirection.Input, rCboGenDoc3.SelectedValue);
            if (rCboDocumento3.Enabled == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docCve3", DbType.String, 10, ParameterDirection.Input, rCboDocumento3.SelectedValue);
            }

            //Aplicación Estadística

            if (rCboConceptoEstadistico.SelectedValue != "")
            {
                ProcBD.AgregarParametrosProcedimiento("@cptoEstId", DbType.Int64, 0, ParameterDirection.Input, rCboConceptoEstadistico.SelectedValue);
            }
           


            ProcBD.AgregarParametrosProcedimiento("@docApliEstMan", DbType.Int64, 0, ParameterDirection.Input, vAplicaEstadistica);
            //vAplicaEstadistica
            //if (rCboFormaAplicacion.SelectedValue == "")
            //{

            //}
            //else
            //{
            ProcBD.AgregarParametrosProcedimiento("@docApliEstApli", DbType.String, 10, ParameterDirection.Input, rCboFormaAplicacion.SelectedValue);
            //}

            //rCboFormaAplicacion.SelectedValue.ToString()

            //LISTA DE PRECIOS
           // ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboManejaListaPrecios.SelectedValue);



            //--------controles BROWSER-------------

            if (CheckGenFac.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDIOpc", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDIOpc", DbType.Int64, 0, ParameterDirection.Input, 2);
            }
            if (CheckEnvCorr.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDIMail", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDIMail", DbType.Int64, 0, ParameterDirection.Input, 2);
            }
            if (CheckArchXml.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDIMailXML", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDIMailXML", DbType.Int64, 0, ParameterDirection.Input, 2);
            }
            if (CheckPDF.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDIMailPDF", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDIMailPDF", DbType.Int64, 0, ParameterDirection.Input, 2);
            }
            ProcBD.AgregarParametrosProcedimiento("@docGenCFDICorreo", DbType.String, 50, ParameterDirection.Input, RadTxtCopiA.Text.Trim());
            if (CheckGuaArch.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDISave", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDISave", DbType.Int64, 0, ParameterDirection.Input, 2);
            }
            if (CheckGuaXML.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDISaveXML", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDISaveXML", DbType.Int64, 0, ParameterDirection.Input, 2);
            }
            if (CheckGuaPDF.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDISavePDF", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docGenCFDISavePDF", DbType.Int64, 0, ParameterDirection.Input, 2);
            }
            

            if (rbtnManejoTextilSi.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docTextilApli", DbType.Int64, 0, ParameterDirection.Input, 1);
            }


            // MASH
            if (rbtnAplicaIncotermSi.Checked == true)
            {
                ProcBD.AgregarParametrosProcedimiento("@docAplicaIncoterm", DbType.Int64, 0, ParameterDirection.Input, 1);
            }
            else
            {
                ProcBD.AgregarParametrosProcedimiento("@docAplicaIncoterm", DbType.Int64, 0, ParameterDirection.Input, 0);
            }

            //--------------------------------------------------


            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);

                if (sEjecEstatus == "1")
                {
                    EjecutaSpFormatoRegistroOpcPantalla();
                    //valida_vaciosRV();
                    EjecutaSpRefVar();
                    InicioPagina();


                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }

    //private void EjecutaManejaLPrecios()
    //{

    //    try
    //    {
    //        DataSet ds = new DataSet();
    //        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //        ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
    //        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 4);
    //        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
    //        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, rTxtDocCve.Text);
    //        ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, rCboManejaListaPrecios.SelectedValue);
    //        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
    //    }
    //    catch (Exception ex)
    //    {
    //        MessageBox.Show(ex.ToString());
    //    }

        
    //}


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


                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_DocDatosGrales";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, dataItem["docCve"].Text);
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

        hdfBtnAccion.Value = "";

    }


    private void EjecutaSpRefVar()
    {

        if (rGdv_Referencias.Items.Count == 0 && hdfBtnAccion.Value == "2")
        {
            try
            {

                DataSet ds = new DataSet();

                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ReferenciasVariablesDocumento";

                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, rTxtDocCve.Text);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 1);

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            }
            catch (Exception ex)
            {

                throw;
            }

        }





        if (rGdv_Variables.Items.Count == 0 && hdfBtnAccion.Value == "2")
        {
            try
            {

                DataSet ds = new DataSet();

                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ReferenciasVariablesDocumento";

                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, rTxtDocCve.Text);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 2);

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


            }
            catch (Exception ex)
            {

                throw;
            }

        }



        try
        {

            foreach (GridDataItem dli in rGdv_Referencias.Items)
            {
                Int32 secRef;

                string valRef = dli.Cells[2].Text;
                secRef = dli.ItemIndex + 1;
                DataSet ds = new DataSet();

                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ReferenciasVariablesDocumento";

                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, rTxtDocCve.Text);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 1);
                ProcBD.AgregarParametrosProcedimiento("@revaDes", DbType.String, 50, ParameterDirection.Input, valRef);
                ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }




            foreach (GridDataItem dli in rGdv_Variables.Items)
            {
                Int32 secRef;

                string valVar = dli.Cells[2].Text;
                secRef = dli.ItemIndex + 1;

                //MessageBox.Show(references.Text);

                DataSet ds = new DataSet();

                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ReferenciasVariablesDocumento";

                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, rTxtDocCve.Text);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

                ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, 1, ParameterDirection.Input, 2);
                ProcBD.AgregarParametrosProcedimiento("@revasec", DbType.Int32, 1, ParameterDirection.Input, secRef);

                ProcBD.AgregarParametrosProcedimiento("@revaDes", DbType.String, 50, ParameterDirection.Input, valVar);

                ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            }


        }


        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());

        }

    }

    private void EjecutaSpFormatoRegistroOpcPantalla()
    {
        int Countkeyarray = 0;
        foreach (DataListItem dli in DataListDocFormatoRegOpcPantalla.Items)
        {
            var valrBtn = dli.FindControl("rBtnTog") as RadButton;
            string DocOpcPantCve = DataListDocFormatoRegOpcPantalla.DataKeys[Countkeyarray].ToString();
            bool docFormRegSit = valrBtn.Checked;
            int idocFormRegSit;
            if (docFormRegSit == true)
            {
                idocFormRegSit = 1;
            }
            else
            {
                idocFormRegSit = 2;
            }

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_DocumentosFormatoRegOpcPantalla";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 2);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@DocCve", DbType.String, 10, ParameterDirection.Input, rTxtDocCve.Text);
            ProcBD.AgregarParametrosProcedimiento("@DocOpcPantCve", DbType.String, 15, ParameterDirection.Input, DocOpcPantCve);
            ProcBD.AgregarParametrosProcedimiento("@docFormRegSit", DbType.Int64, 0, ParameterDirection.Input, idocFormRegSit);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            Countkeyarray += 1;
        }



    }

    private void Check_Inventarios()
    {
        if (rBtnActInvAplica.Checked == true && rBtnActInvNo.Checked == false && rBtnActInvGenera.Checked == false)
        {
            rCboConceptoInventarios.Enabled = true;
            rCboConceptoInventarios.ClearSelection();
        }

        if (rBtnActInvAplica.Checked == false && rBtnActInvNo.Checked == true && rBtnActInvGenera.Checked == false)
        {
            rCboConceptoInventarios.ClearSelection();
            rCboConceptoInventarios.Enabled = false;
        }


        if (rBtnActInvAplica.Checked == false && rBtnActInvNo.Checked == false && rBtnActInvGenera.Checked == true)
        {
            rCboConceptoInventarios.Enabled = true;
            rCboConceptoInventarios.ClearSelection();
        }
    }
    private void Check_CXC()
    {

        if (rBtnActCxcAplica.Checked == true && rBtnActCxcNo.Checked == false && rBtnActCxcGenera.Checked == false)
        {
            rCboConceptoCuentasxCobrar.Enabled = true;
            rCboConceptoCuentasxCobrar.ClearSelection();
        }

        if (rBtnActCxcAplica.Checked == false && rBtnActCxcNo.Checked == true && rBtnActCxcGenera.Checked == false)
        {

            rCboConceptoCuentasxCobrar.ClearSelection();
            rCboConceptoCuentasxCobrar.Enabled = false;
        }


        if (rBtnActCxcAplica.Checked == false && rBtnActCxcNo.Checked == false && rBtnActCxcGenera.Checked == true)
        {
            rCboConceptoCuentasxCobrar.Enabled = true;
            rCboConceptoCuentasxCobrar.ClearSelection();
        }
    }
    private void Chek_Contabilidad()
    {

        if (rBtnActContAplica.Checked == true && rBtnActContNo.Checked == false && rBtnActContGenera.Checked == false)
        {
            rCboConceptoContabilidad.Enabled = true;
            rCboConceptoContabilidad.ClearSelection();
        }

        if (rBtnActContAplica.Checked == false && rBtnActContNo.Checked == true && rBtnActContGenera.Checked == false)
        {
            rCboConceptoContabilidad.ClearSelection();
            rCboConceptoContabilidad.Enabled = false;

        }


        if (rBtnActContAplica.Checked == false && rBtnActContNo.Checked == false && rBtnActContGenera.Checked == true)
        {
            rCboConceptoContabilidad.Enabled = true;
            rCboConceptoContabilidad.ClearSelection();
        }
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
        int camposInc = 0;
        sMSGTip = "";





        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            //=====>> Datos Generales <<=====
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                //Clave Documento
                if (rTxtDocCve.Text.Trim() == "")
                {
                    rTxtDocCve.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtDocCve.CssClass = "cssTxtEnabled"; }
            }

            //Descripcion Documento
            if (rTxtDocDes.Text.Trim() == "")
            {
                rTxtDocDes.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDocDes.CssClass = "cssTxtEnabled"; }
            //Manejador de Folio
            if (this.rCboManejoFolio.SelectedIndex == -1)
            {
                camposInc += 1;
            }
            //Foliador
            if (this.rcboFoliador.SelectedIndex == -1)
            {

                rcboFoliador.BorderWidth = Unit.Pixel(1);
                rcboFoliador.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rcboFoliador.BorderColor = System.Drawing.Color.Transparent;
            }

         

            //MANEJA DESCUENTO

            if (rCboManejaDescuento.SelectedValue == "")
            {
                rCboManejaDescuento.BorderWidth = Unit.Pixel(1);
                rCboManejaDescuento.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                rCboManejaDescuento.BorderColor = System.Drawing.Color.Transparent;
            }




            //=====>> ACTUALIZA MODULO <<=====
            if (rBtnActInvNo.Checked == false)
            {
                if (rCboConceptoInventarios.SelectedIndex == -1)
                {

                    rCboConceptoInventarios.BorderWidth = Unit.Pixel(1);
                    rCboConceptoInventarios.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    rCboConceptoInventarios.BorderColor = System.Drawing.Color.Transparent;
                }
            }

            if (rBtnActCxcNo.Checked == false)
            {
                if (rCboConceptoCuentasxCobrar.SelectedIndex == -1)
                {
                    rCboConceptoCuentasxCobrar.BorderWidth = Unit.Pixel(1);
                    rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Transparent;
                }
            }
            if (rBtnActContNo.Checked == false)
            {
                if (rCboConceptoContabilidad.SelectedIndex == -1)
                {
                    rCboConceptoContabilidad.BorderWidth = Unit.Pixel(1);
                    rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Transparent;
                }
            }

            //=====>> DOCUMENTOS DETIVADOS <<=====
            if (rCboDocumento1.Enabled == true)
            {
                if (rCboDocumento1.SelectedValue == "")
                {
                    rCboDocumento1.BorderWidth = Unit.Pixel(1);
                    rCboDocumento1.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    rCboDocumento1.BorderColor = System.Drawing.Color.Transparent;
                }
            }
            if (rCboDocumento2.Enabled == true)
            {
                if (rCboDocumento2.SelectedValue == "")
                {

                    rCboDocumento2.BorderWidth = Unit.Pixel(1);
                    rCboDocumento2.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    rCboDocumento2.BorderColor = System.Drawing.Color.Transparent;
                }
            }

            if (rCboDocumento3.Enabled == true)
            {
                if (rCboDocumento3.SelectedValue == "")
                {

                    rCboDocumento3.BorderWidth = Unit.Pixel(1);
                    rCboDocumento3.BorderColor = System.Drawing.Color.Red;
                    camposInc += 1;
                }
                else
                {
                    rCboDocumento3.BorderColor = System.Drawing.Color.Transparent;
                }
            }

    


            //Genera CFDI







            //---------


            if (CheckGenFac.Checked == true && CheckEnvCorr.Checked == false)
            {
                CheckEnvCorr.BorderWidth = Unit.Pixel(1);
                CheckEnvCorr.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                CheckEnvCorr.BorderColor = System.Drawing.Color.Transparent;
            }


            if (CheckEnvCorr.Checked == true && (CheckArchXml.Checked == false && CheckPDF.Checked == false))
            {
                CheckArchXml.BorderWidth = Unit.Pixel(1);
                CheckArchXml.BorderColor = System.Drawing.Color.Red;

                CheckPDF.BorderWidth = Unit.Pixel(1);
                CheckPDF.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {
                CheckArchXml.BorderColor = System.Drawing.Color.Transparent;
                CheckPDF.BorderColor = System.Drawing.Color.Transparent;
            }



            if ((CheckArchXml.Checked == true || CheckPDF.Checked == true))
            {
                if (RadTxtCopiA.Text == "")
                {
                    RadTxtCopiA.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else
                {
                    RadTxtCopiA.CssClass = "cssTxtEnabled";
                }
            }




            if (CheckGuaArch.Checked == true && CheckGuaXML.Checked == false && CheckGuaPDF.Checked == false)
            {


                CheckGuaXML.BorderWidth = Unit.Pixel(1);
                CheckGuaXML.BorderColor = System.Drawing.Color.Red;

                CheckGuaPDF.BorderWidth = Unit.Pixel(1);
                CheckGuaPDF.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else
            {

                CheckGuaXML.BorderColor = System.Drawing.Color.Transparent;
                CheckGuaPDF.BorderColor = System.Drawing.Color.Transparent;
            }


            //---------






            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        return sResult;
    }

    
    private DataView GetDataTableFormatosImpresion()
    {

        DataSet ds = new DataSet();
        ds = FNDatos.dsFormatosImpresion(Pag_sConexionLog, Pag_sCompania, 1);
        DataView dataView = new DataView(ds.Tables[0]);
        return dataView;

    }
    private DataSet dsDocumentoFormatoRegistoOpcPantalla(int opc, string sDocCve)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentosFormatoRegOpcPantalla";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, opc);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@DocCve", DbType.String, 10, ParameterDirection.Input, sDocCve);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private DataSet llenadatalistVarRef(Int32 revaTip)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ReferenciasVariablesDocumento";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 20, ParameterDirection.Input, rTxtDocCve.Text);
        ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, (5), ParameterDirection.Input, revaTip);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

    }

    private string getValueFormDoc()
    {

        int contador = 0;


        foreach (ImageGalleryItem item in RadImageGallery1.Items)
        {



            if (RadImageGallery1.CurrentItemIndex == contador)
            {

                return item.Title;
            }

            contador++;
        }

        return "";

    }

    private void addNewRef()
    {

        dtTmpPart.Columns.Add("revasec");
        dtTmpPart.Columns.Add("revaDes");

        //Recuperar Valores de Grid

        if (rGdv_Referencias.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_Referencias.Items.Count; i++)
            {

                DataRow row1 = dtTmpPart.NewRow();

                row1["revasec"] = i + 1;
                row1["revaDes"] = rGdv_Referencias.Items[i].Cells[2].Text;

                dtTmpPart.Rows.Add(row1);

            }
        }

        //Añadir Nueva Referencia

        DataRow row = dtTmpPart.NewRow();

        row["revasec"] = dtTmpPart.Rows.Count + 1;
        row["revaDes"] = txtValRef.Text.Trim();

        dtTmpPart.Rows.Add(row);


        rGdv_Referencias.DataSource = dtTmpPart;

    }


    private void ediNewRef()
    {

        dtTmpPart.Columns.Add("revasec");
        dtTmpPart.Columns.Add("revaDes");

        //Recuperar Valores de Grid

        if (rGdv_Referencias.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_Referencias.Items.Count; i++)
            {

                if (rGdv_Referencias.Items[i].Selected == true)
                {
                    DataRow row = dtTmpPart.NewRow();
                    row["revasec"] = i + 1;
                    row["revaDes"] = txtValRef.Text;

                    dtTmpPart.Rows.Add(row);

                }
                else if (rGdv_Referencias.Items[i].Selected == false)
                {


                    DataRow row = dtTmpPart.NewRow();
                    row["revasec"] = i + 1;
                    row["revaDes"] = rGdv_Referencias.Items[i].Cells[2].Text;

                    dtTmpPart.Rows.Add(row);

                }


            }
        }

        rGdv_Referencias.DataSource = dtTmpPart;

    }

    private void delNewRef()
    {

        dtTmpPart.Columns.Add("revasec");
        dtTmpPart.Columns.Add("revaDes");

        if (rGdv_Referencias.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_Referencias.Items.Count; i++)
            {

                if (rGdv_Referencias.Items[i].Selected == true)
                {


                }
                else if (rGdv_Referencias.Items[i].Selected == false)
                {

                    DataRow row = dtTmpPart.NewRow();
                    row["revasec"] = i + 1;
                    row["revaDes"] = rGdv_Referencias.Items[i].Cells[2].Text;

                    dtTmpPart.Rows.Add(row);

                }


            }
        }

        rGdv_Referencias.DataSource = dtTmpPart;

    }
    
    private void addNewVar()
    {

        dtTmpPart.Columns.Add("revasec");
        dtTmpPart.Columns.Add("revaDes");

        //Recuperar Valores de Grid

        if (rGdv_Variables.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_Variables.Items.Count; i++)
            {

                DataRow row1 = dtTmpPart.NewRow();

                row1["revasec"] = i + 1;
                row1["revaDes"] = rGdv_Variables.Items[i].Cells[2].Text;

                dtTmpPart.Rows.Add(row1);

            }
        }

        //Añadir Nueva Referencia

        DataRow row = dtTmpPart.NewRow();

        row["revasec"] = dtTmpPart.Rows.Count + 1;
        row["revaDes"] = txtValVar.Text.Trim();

        dtTmpPart.Rows.Add(row);


        rGdv_Variables.DataSource = dtTmpPart;

    }
    
    private void ediNewVar()
    {

        dtTmpPart.Columns.Add("revasec");
        dtTmpPart.Columns.Add("revaDes");

        //Recuperar Valores de Grid

        if (rGdv_Variables.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_Variables.Items.Count; i++)
            {

                if (rGdv_Variables.Items[i].Selected == true)
                {
                    DataRow row = dtTmpPart.NewRow();
                    row["revasec"] = i + 1;
                    row["revaDes"] = txtValVar.Text;

                    dtTmpPart.Rows.Add(row);

                }
                else if (rGdv_Variables.Items[i].Selected == false)
                {


                    DataRow row = dtTmpPart.NewRow();
                    row["revasec"] = i + 1;
                    row["revaDes"] = rGdv_Variables.Items[i].Cells[2].Text;

                    dtTmpPart.Rows.Add(row);

                }


            }
        }

        rGdv_Variables.DataSource = dtTmpPart;

    }

    //private void enaListPrec()
    //{
    //    ParmLPrecios = "";

    //    DataSet ds = new DataSet();
    //    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
    //    ProcBD.NombreProcedimiento = "sp_parametros";
    //    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
    //    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
    //    ProcBD.AgregarParametrosProcedimiento("@parmCve", DbType.String, 10, ParameterDirection.Input, "PRECVE");

    //    ProcBD.AgregarParametrosProcedimiento("@parmSec", DbType.Int64, 0, ParameterDirection.Input, 1);

    //    ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ParmLPrecios = ds.Tables[0].Rows[0]["parmValInt"].ToString();

    //        if (ParmLPrecios == "1")
    //        {
    //            rCboManejaListaPrecios.Visible = false;
    //            RadLabeMANEJALISTAPRECIOS.Visible = false;


    //        }
    //        else if (ParmLPrecios == "2")
    //        {
    //            rCboManejaListaPrecios.Visible = true;
    //            RadLabeMANEJALISTAPRECIOS.Visible = true;

    //        }
    //        else if (ParmLPrecios == "3")
    //        {
    //            rCboManejaListaPrecios.Visible = true;
    //            RadLabeMANEJALISTAPRECIOS.Visible = true;
    //        }
    //    }
    //    else
    //    {



    //    }

    //}


    private void delNewVar()
    {

        dtTmpPart.Columns.Add("revasec");
        dtTmpPart.Columns.Add("revaDes");

        if (rGdv_Variables.Items.Count > 0)
        {
            for (int i = 0; i < rGdv_Variables.Items.Count; i++)
            {

                if (rGdv_Variables.Items[i].Selected == true)
                {


                }
                else if (rGdv_Variables.Items[i].Selected == false)
                {

                    DataRow row = dtTmpPart.NewRow();
                    row["revasec"] = i + 1;
                    row["revaDes"] = rGdv_Variables.Items[i].Cells[2].Text;

                    dtTmpPart.Rows.Add(row);

                }


            }
        }

        rGdv_Variables.DataSource = dtTmpPart;

    }


    private void GeneraCFDI(DataSet pds)
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_DocumentoGeneraCFDI";
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@docCve", DbType.String, 10, ParameterDirection.Input, pds.Tables[0].Rows[0]["docCve"].ToString());
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {

            /////////////////////////////Browser///////////////////Alfonso Flores
            if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDIOpc"]) == "1")
            {
                CheckGenFac.Checked = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDIOpc"]) == "2")
            {
                CheckGenFac.Checked = false;
            }
            /////
            if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDIMail"]) == "1")
            {
                CheckEnvCorr.Checked = true;

                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    //if (CheckEnvCorr.Checked == true)
                    //{
                    //    CheckEnvCorr.Enabled = true;
                    //    RadTxtCopiA.Enabled = true;
                    //    CheckArchXml.Checked = true;
                    //    CheckArchXml.Enabled = false;
                    //    CheckPDF.Checked = true;
                    //    CheckPDF.Enabled = false;
                    //}
                }



            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDIMail"]) == "2")
            {
                CheckEnvCorr.Checked = false;
            }
            //////
            if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDIMailXML"]) == "1")
            {
                CheckArchXml.Checked = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDIMailXML"]) == "2")
            {
                CheckArchXml.Checked = false;
            }
            ///
            if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDIMailPDF"]) == "1")
            {
                CheckPDF.Checked = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDIMailPDF"]) == "2")
            {
                CheckPDF.Checked = false;
            }
            ////
            RadTxtCopiA.Text = Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDICorreo"]);
            ////
            if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDISave"]) == "1")
            {
                CheckGuaArch.Checked = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDISave"]) == "2")
            {
                CheckGuaArch.Checked = false;
            }
            /////
            if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDISaveXML"]) == "1")
            {
                CheckGuaXML.Checked = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDISaveXML"]) == "2")
            {
                CheckGuaXML.Checked = false;
            }
            //////
            if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDISavePDF"]) == "1")
            {
                CheckGuaPDF.Checked = true;
            }
            else if (Convert.ToString(ds.Tables[0].Rows[0]["docGenCFDISavePDF"]) == "2")
            {
                CheckGuaPDF.Checked = false;
            }
            ////


        }


    }



    private void CFDI_Clear()
    {

        CheckGenFac.Checked = false;
        CheckGenFac.Enabled = true;

        CheckEnvCorr.Checked = false;
        CheckEnvCorr.Enabled = false;

        CheckArchXml.Checked = false;
        CheckArchXml.Enabled = false;

        CheckPDF.Checked = false;
        CheckPDF.Enabled = false;

        CheckGuaArch.Enabled = false;
        CheckGuaArch.Checked = false;

        CheckGuaXML.Enabled = false;
        CheckGuaXML.Checked = false;

        CheckGuaPDF.Enabled = false;
        CheckGuaPDF.Checked = false;

        RadTxtCopiA.Enabled = false;
        RadTxtCopiA.Text = "";

    }


    private void CheckCFDI_GuardaArchivo1(Boolean valor)
    {
        //CheckGuaArch.Checked = valor;
        //CheckGuaXML.Checked = valor;
        //CheckGuaPDF.Checked = valor;
        //CheckGuaXML.Enabled = valor;
        //CheckGuaPDF.Enabled = valor;
    }

    private void BROWSER_CHECK()
    {
        if (CheckGenFac.Checked == true)
        {
            CheckEnvCorr.Enabled = true;
            CheckEnvCorr.Checked = false;

            CheckArchXml.Enabled = false;
            CheckArchXml.Checked = false;

            CheckPDF.Enabled = false;
            CheckPDF.Checked = false;

            RadTxtCopiA.Enabled = false;
            RadTxtCopiA.Text = "";

            CheckGuaArch.Enabled = false;
            CheckGuaArch.Checked = true;

            CheckGuaXML.Enabled = false;
            CheckGuaXML.Checked = true;

            CheckGuaPDF.Enabled = false;
            CheckGuaPDF.Checked = true;
        }
        else
        {

            CheckEnvCorr.Enabled = false;
            CheckEnvCorr.Checked = false;

            CheckArchXml.Enabled = false;
            CheckArchXml.Checked = false;

            CheckPDF.Enabled = false;
            CheckPDF.Checked = false;

            RadTxtCopiA.Enabled = false;
            RadTxtCopiA.Text = "";

            CheckGuaArch.Enabled = false;
            CheckGuaArch.Checked = false;

            CheckGuaXML.Enabled = false;
            CheckGuaXML.Checked = false;

            CheckGuaPDF.Enabled = false;
            CheckGuaPDF.Checked = false;

        }



    }

    #endregion




    private void ControlesAccion()
    {
        #region INICIOCONTROLES

        
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL rGdv_Documentos
        this.rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


        rTxtDocCve.CssClass = "cssTxtEnabled";
        rTxtDocDes.CssClass = "cssTxtEnabled";
        rcboFoliador.BorderColor = System.Drawing.Color.Transparent;
        rCboManejaDescuento.BorderColor = System.Drawing.Color.Transparent;
        rCboConceptoInventarios.BorderColor = System.Drawing.Color.Transparent;
        rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Transparent;
        rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Transparent;
        rCboDocumento1.BorderColor = System.Drawing.Color.Transparent;
        rCboDocumento2.BorderColor = System.Drawing.Color.Transparent;
        rCboDocumento3.BorderColor = System.Drawing.Color.Transparent;
        CheckEnvCorr.BorderColor = System.Drawing.Color.Transparent;
        CheckArchXml.BorderColor = System.Drawing.Color.Transparent;
        CheckPDF.BorderColor = System.Drawing.Color.Transparent;
        RadTxtCopiA.CssClass = "cssTxtEnabled";
        CheckGuaXML.BorderColor = System.Drawing.Color.Transparent;
        CheckGuaPDF.BorderColor = System.Drawing.Color.Transparent;
        
        //GENERA CFDI
        pnlCFDI.Enabled = false;

        //DATOS GENERALES
        rTxtDocCve.Enabled = false;
        rTxtDocDes.Enabled = false;
        rcboFoliador.Enabled = false;
        rCboManejoFolio.Enabled = false;
        rCboManejaDescuento.Enabled = false;
        rCboManejaListaPrecios.Enabled = false;
        rBtnChkProPar.Enabled = false;
        rBtnValExt.Enabled = false;
        rBtnReqAut.Enabled = false;
        rBtnValCreSi.Enabled = false;
        rBtnValCreNo.Enabled = false;
        rBtnValCreAut.Enabled = false;
        rBtnTipDocDeb.Enabled = false;
        rBtnTipDocCre.Enabled = false;

        //Actualiza Modulos
        //INVENTARIOS
        rBtnActInvAplica.Enabled = false;
        rBtnActInvNo.Enabled = false;
        rBtnActInvGenera.Enabled = false;
        rCboConceptoInventarios.Enabled = false;

        //CC
        rBtnActCxcAplica.Enabled = false;
        rBtnActCxcNo.Enabled = false;
        rBtnActCxcGenera.Enabled = false;
        rCboConceptoCuentasxCobrar.Enabled = false;

        //CONTABILDAD
        rBtnActContAplica.Enabled = false;
        rBtnActContNo.Enabled = false;
        rBtnActContGenera.Enabled = false;
        rCboConceptoContabilidad.Enabled = false;

        //DOCUMENTOS DERIVADOS
        rCboGenDoc1.Enabled = false;
        rCboDocumento1.Enabled = false;
        rCboGenDoc2.Enabled = false;
        rCboDocumento2.Enabled = false;
        rCboGenDoc3.Enabled = false;
        rCboDocumento3.Enabled = false;

        //Aplicacion Estadistica
        radTotal.Enabled = false;
        radSurtido.Enabled = false;
        rCboConceptoEstadistico.Enabled = false;
        radPedido.Enabled = false;
        radPendiente.Enabled = false;
        rCboFormaAplicacion.Enabled = false;

        //REFERENCIAS
        Panel1.Enabled = false;
        rGdv_Referencias.Enabled = false;

        //VARIABLES
        Panel2.Enabled = false;
        rGdv_Variables.Enabled = false;

        //LAYOUT IMPRESION
        RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
        RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

        //FORMATO REGISTRO
        //this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(52, "");
        //DataListDocFormatoRegOpcPantalla.DataBind();


        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;

        #endregion
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
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString() &&

        hdfBtnAccion.Value != "rBtnSeguridad" &&
        hdfBtnAccion.Value != "rBtnDescuentos" &&
        hdfBtnAccion.Value != "rBtnListaPrecios" &&
        hdfBtnAccion.Value != "rBtnImpuestos" &&
        hdfBtnAccion.Value != "rBtnUsoCFDI"
          )
        {
            #region INICIOCANCELAR

            
            //GENERA CFDI
            pnlCFDI.Enabled = false;

            //DATOS GENERALES
            rTxtDocCve.Enabled = false;
            rTxtDocDes.Enabled = false;
            rcboFoliador.Enabled = false;
            rCboManejoFolio.Enabled = false;
            rCboManejaDescuento.Enabled = false;
            rCboManejaListaPrecios.Enabled = false;
            rBtnChkProPar.Enabled = false;
            rBtnValExt.Enabled = false;
            rBtnReqAut.Enabled = false;
            rBtnValCreSi.Enabled = false;
            rBtnValCreNo.Enabled = false;
            rBtnValCreAut.Enabled = false;
            rBtnTipDocDeb.Enabled = false;
            rBtnTipDocCre.Enabled = false;

            //Actualiza Modulos
            //INVENTARIOS
            rBtnActInvAplica.Enabled = false;
            rBtnActInvNo.Enabled = false;
            rBtnActInvGenera.Enabled = false;
            rCboConceptoInventarios.Enabled = false;

            //CC
            rBtnActCxcAplica.Enabled = false;
            rBtnActCxcNo.Enabled = false;
            rBtnActCxcGenera.Enabled = false;
            rCboConceptoCuentasxCobrar.Enabled = false;

            //CONTABILDAD
            rBtnActContAplica.Enabled = false;
            rBtnActContNo.Enabled = false;
            rBtnActContGenera.Enabled = false;
            rCboConceptoContabilidad.Enabled = false;

            //DOCUMENTOS DERIVADOS
            rCboGenDoc1.Enabled = false;
            rCboDocumento1.Enabled = false;
            rCboGenDoc2.Enabled = false;
            rCboDocumento2.Enabled = false;
            rCboGenDoc3.Enabled = false;
            rCboDocumento3.Enabled = false;

            //Aplicacion Estadistica
            radTotal.Enabled = false;
            radSurtido.Enabled = false;
            rCboConceptoEstadistico.Enabled = false;
            radPedido.Enabled = false;
            radPendiente.Enabled = false;
            rCboFormaAplicacion.Enabled = false;

            //REFERENCIAS
            Panel1.Enabled = false;
            rGdv_Referencias.Enabled = false;

            //VARIABLES
            Panel2.Enabled = false;
            rGdv_Variables.Enabled = false;

            //LAYOUT IMPRESION
            RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
            RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

            //FORMATO REGISTRO
           


            this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(52,"");
            DataListDocFormatoRegOpcPantalla.DataBind();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //GENERA CFDI
            CheckGenFac.Checked = false;
            BROWSER_CHECK();   //<---- PONE TODOS LOS CHECBOX EN FALSE

            //DATOS GENERALES
            rTxtDocCve.Text = "";
            rTxtDocDes.Text = "";
            rCboManejoFolio.ClearSelection();
            rcboFoliador.ClearSelection();
            rCboManejaDescuento.ClearSelection();
            rCboManejaListaPrecios.ClearSelection();
            rBtnChkProPar.Checked = false;
            rBtnValExt.Checked = false;
            rBtnReqAut.Checked = false;
            rBtnValCreSi.Checked = false;
            rBtnValCreNo.Checked = true;
            rBtnValCreAut.Checked = false;
            rBtnTipDocDeb.Checked = true;
            rBtnTipDocCre.Checked = false;

            //ACTUALIZA MODULOS
            //INVENTARIOS
            rCboConceptoInventarios.BorderColor = System.Drawing.Color.Transparent;
            rBtnActInvAplica.Checked = false;
            rBtnActInvNo.Checked = true;
            rBtnActInvGenera.Checked = false;
            rCboConceptoInventarios.ClearSelection();

            //CC
            rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Transparent;
            rBtnActCxcAplica.Checked = false;
            rBtnActCxcNo.Checked = true;
            rBtnActCxcGenera.Checked = false;
            rCboConceptoCuentasxCobrar.ClearSelection();

            //CONTABILIDAD
            rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Transparent;
            rBtnActContAplica.Checked = false;
            rBtnActContNo.Checked = true;
            rBtnActContGenera.Checked = false;
            rCboConceptoContabilidad.ClearSelection();

            //DOCUMENTOS DERIVADOS
            rCboDocumento1.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento2.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento3.BorderColor = System.Drawing.Color.Transparent;
            rCboGenDoc1.SelectedValue = "2";
            rCboGenDoc2.SelectedValue = "2";
            rCboGenDoc3.SelectedValue = "2";
            rCboDocumento1.SelectedIndex = -1;
            rCboDocumento1.ClearSelection();
            rCboDocumento2.SelectedIndex = -1;
            rCboDocumento2.ClearSelection();
            rCboDocumento3.SelectedIndex = -1;
            rCboDocumento3.ClearSelection();

            //APLICACION ESTADISTICA
            radTotal.Checked = true;
            radSurtido.Checked = false;
            rCboConceptoEstadistico.ClearSelection();
            radPedido.Checked = false;
            radPendiente.Checked = false;
            rCboFormaAplicacion.ClearSelection();

            //REFERENCIAS
            txtValRef.Text = "";
            rGdv_Referencias.MasterTableView.ClearSelectedItems();

            //VARIABLES
            txtValVar.Text = "";
            rGdv_Variables.MasterTableView.ClearSelectedItems();

            //Layout de Impresión
            RadImageGallery1.DataBind();

            //FORMATO REGISTRO
            this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(52, "");
            DataListDocFormatoRegOpcPantalla.DataBind();
            #endregion
        }
    }
    
    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                #region NUEVO
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_Documentos.MasterTableView.ClearSelectedItems();

                ////GENERA CFDI
                pnlCFDI.Enabled = true;

                //DATOS GENERALES
                rTxtDocCve.Enabled = true;
                rTxtDocDes.Enabled = true;
                rcboFoliador.Enabled = true;
                rCboManejoFolio.Enabled = true;
                rCboManejaDescuento.Enabled = true;
                rCboManejaListaPrecios.Enabled = true;
                rBtnChkProPar.Enabled = true;
                rBtnValExt.Enabled = true;
                rBtnReqAut.Enabled = true;
                rBtnValCreSi.Enabled = true;
                rBtnValCreNo.Enabled = true;
                rBtnValCreAut.Enabled = true;
                rBtnTipDocDeb.Enabled = true;
                rBtnTipDocCre.Enabled = true;

                //Actualiza Modulos
                //INVENTARIOS
                rBtnActInvAplica.Enabled = true;
                rBtnActInvNo.Enabled = true;
                rBtnActInvGenera.Enabled = true;
                rCboConceptoInventarios.Enabled = true;

                //CC
                rBtnActCxcAplica.Enabled = true;
                rBtnActCxcNo.Enabled = true;
                rBtnActCxcGenera.Enabled = true;
                rCboConceptoCuentasxCobrar.Enabled = true;

                //CONTABILDAD
                rBtnActContAplica.Enabled = true;
                rBtnActContNo.Enabled = true;
                rBtnActContGenera.Enabled = true;
                rCboConceptoContabilidad.Enabled = true;

                //DOCUMENTOS DERIVADOS
                rCboGenDoc1.Enabled = true;
                rCboDocumento1.Enabled = true;
                rCboGenDoc2.Enabled = true;
                rCboDocumento2.Enabled = true;
                rCboGenDoc3.Enabled = true;
                rCboDocumento3.Enabled = true;

                rCboDocumento1.ClearSelection();
                rCboDocumento2.ClearSelection();
                rCboDocumento3.ClearSelection();

                //Aplicacion Estadistica
                radTotal.Enabled = true;
                radSurtido.Enabled = true;
                rCboConceptoEstadistico.Enabled = true;
                radPedido.Enabled = true;
                radPendiente.Enabled = true;
                rCboFormaAplicacion.Enabled = true;

                //REFERENCIAS
                Panel1.Enabled = true;
                rGdv_Referencias.Enabled = true;

                //VARIABLES
                Panel2.Enabled = true;
                rGdv_Variables.Enabled = true;

                //LAYOUT IMPRESION
                RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = true;
                RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = true;

                //FORMATO REGISTRO
                pnlFormatoRegistro.Enabled = true;


                //LIMPIAR CONTROLES&///////////////////////////////////////////////////////////
                //GENERA CFDI
                CheckGenFac.Checked = false;
                BROWSER_CHECK();   //<---- PONE TODOS LOS CHECBOX EN FALSE

                //DATOS GENERALES
                rTxtDocCve.Text = "";
                rTxtDocDes.Text = "";
                rCboManejoFolio.ClearSelection();
                rcboFoliador.ClearSelection();
                rCboManejaDescuento.ClearSelection();
                rCboManejaListaPrecios.ClearSelection();
                rBtnChkProPar.Checked = false;
                rBtnValExt.Checked = false;
                rBtnReqAut.Checked = false;
                rBtnValCreSi.Checked = false;
                rBtnValCreNo.Checked = true;
                rBtnValCreAut.Checked = false;
                rBtnTipDocDeb.Checked = true;
                rBtnTipDocCre.Checked = false;

                //ACTUALIZA MODULOS
                //INVENTARIOS
                rCboConceptoInventarios.BorderColor = System.Drawing.Color.Transparent;
                rBtnActInvAplica.Checked = false;
                rBtnActInvNo.Checked = true;
                rBtnActInvGenera.Checked = false;
                rCboConceptoInventarios.ClearSelection();
                rCboConceptoInventarios.Enabled = false;


                //CC
                rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Transparent;
                rBtnActCxcAplica.Checked = false;
                rBtnActCxcNo.Checked = true;
                rBtnActCxcGenera.Checked = false;
                rCboConceptoCuentasxCobrar.ClearSelection();
                rCboConceptoCuentasxCobrar.Enabled = false;

                //CONTABILIDAD
                rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Transparent;
                rBtnActContAplica.Checked = false;
                rBtnActContNo.Checked = true;
                rBtnActContGenera.Checked = false;
                rCboConceptoContabilidad.ClearSelection();
                rCboConceptoContabilidad.Enabled = false;


                //DOCUMENTOS DERIVADOS
                rCboDocumento1.BorderColor = System.Drawing.Color.Transparent;
                rCboDocumento2.BorderColor = System.Drawing.Color.Transparent;
                rCboDocumento3.BorderColor = System.Drawing.Color.Transparent;
                rCboGenDoc1.SelectedValue = "2";
                rCboGenDoc2.SelectedValue = "2";
                rCboGenDoc3.SelectedValue = "2";
                rCboDocumento1.SelectedIndex = -1;
                rCboDocumento1.ClearSelection();
                rCboDocumento2.SelectedIndex = -1;
                rCboDocumento2.ClearSelection();
                rCboDocumento3.SelectedIndex = -1;
                rCboDocumento3.ClearSelection();
                rCboDocumento1.Enabled = false;
                rCboDocumento2.Enabled = false;
                rCboDocumento3.Enabled = false;



                //APLICACION ESTADISTICA
                radTotal.Checked = true;
                radSurtido.Checked = false;
                rCboConceptoEstadistico.ClearSelection();
                radPedido.Checked = false;
                radPendiente.Checked = false;
                rCboFormaAplicacion.ClearSelection();

                //REFERENCIAS
                txtValRef.Text = "";
                rGdv_Referencias.MasterTableView.ClearSelectedItems();

                //VARIABLES
                txtValVar.Text = "";
                rGdv_Variables.MasterTableView.ClearSelectedItems();

                //Layout de Impresión
                RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = true;
                RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = true;

                ////FORMATO REGISTRO
                this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(52, "");
                DataListDocFormatoRegOpcPantalla.DataBind();

                PnlsEnabledGral(true);

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
                #endregion
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {

                #region MODIFICAR
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdv_Documentos.AllowMultiRowSelection = false;

                pnlCFDI.Enabled = true;

                //DATOS GENERALES
                rTxtDocCve.Enabled = false;
                rTxtDocDes.Enabled = true;
                rcboFoliador.Enabled = true;
                rCboManejoFolio.Enabled = true;
                rCboManejaDescuento.Enabled = true;
                rCboManejaListaPrecios.Enabled = true;
                rBtnChkProPar.Enabled = true;
                rBtnValExt.Enabled = true;
                rBtnReqAut.Enabled = true;
                rBtnValCreSi.Enabled = true;
                rBtnValCreNo.Enabled = true;
                rBtnValCreAut.Enabled = true;
                rBtnTipDocDeb.Enabled = true;
                rBtnTipDocCre.Enabled = true;
                //mash
                rbtnAplicaIncotermSi.Enabled = true;
                rbtnAplicaIncotermNo.Enabled = true;

                //Actualiza Modulos
                //INVENTARIOS
                rBtnActInvAplica.Enabled = true;
                rBtnActInvNo.Enabled = true;
                rBtnActInvGenera.Enabled = true;
                if (rCboConceptoInventarios.SelectedValue != "")
                {
                    rCboConceptoInventarios.Enabled = true;
                }
                else
                {
                    rCboConceptoInventarios.Enabled = false;
                }


                //CC
                rBtnActCxcAplica.Enabled = true;
                rBtnActCxcNo.Enabled = true;
                rBtnActCxcGenera.Enabled = true;

                if (rCboConceptoCuentasxCobrar.SelectedValue != "")
                {
                    rCboConceptoCuentasxCobrar.Enabled = true;
                }
                else
                {
                    rCboConceptoCuentasxCobrar.Enabled = false;
                }

                //CONTABILDAD
                rBtnActContAplica.Enabled = true;
                rBtnActContNo.Enabled = true;
                rBtnActContGenera.Enabled = true;
                if (rCboConceptoContabilidad.SelectedValue != "")
                {
                    rCboConceptoContabilidad.Enabled = true;
                }
                else
                {
                    rCboConceptoContabilidad.Enabled = false;
                }


                //DOCUMENTOS DERIVADOS


                rCboGenDoc1.Enabled = true;
                if (rCboGenDoc1.SelectedValue == "2")
                {
                    rCboDocumento1.Enabled = false;
                }
                else
                {
                    rCboDocumento1.Enabled = true;
                }

                rCboGenDoc2.Enabled = true;
                if (rCboGenDoc2.SelectedValue == "2")
                {
                    rCboDocumento2.Enabled = false;
                }
                else
                {
                    rCboDocumento2.Enabled = true;
                }

                rCboGenDoc3.Enabled = true;
                if (rCboGenDoc3.SelectedValue == "2")
                {
                    rCboDocumento3.Enabled = false;
                }
                else
                {
                    rCboDocumento3.Enabled = true;
                }



                //Aplicacion Estadistica
                radTotal.Enabled = true;
                radSurtido.Enabled = true;
                rCboConceptoEstadistico.Enabled = true;
                radPedido.Enabled = true;
                radPendiente.Enabled = true;

                rCboFormaAplicacion.Enabled = true;

                //REFERENCIAS
                Panel1.Enabled = true;
                rGdv_Referencias.Enabled = true;

                //VARIABLES
                Panel2.Enabled = true;
                rGdv_Variables.Enabled = true;

                //LAYOUT IMPRESION
                RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = true;
                RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = true;

                //FORMATO REGISTRO
                pnlFormatoRegistro.Enabled = true;
                PnlsEnabledGral(true);

                //FORMATO REGISTRO
                //var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                //this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(52, dataItem["docCve"].Text);
                //DataListDocFormatoRegOpcPantalla.DataBind();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
                #endregion

            }
            
            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }
            //SEGURIDAD
            if (hdfBtnAccion.Value == "rBtnSeguridad")
            {
                Int64 Pag_sidM = 0;
                if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                {
                    Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                }
                
                var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem.GetDataKeyValue("docCve").ToString();
                FNMttoDocPermisos.NavigateUrl = "~/DC/MttoDocAutorizacion.aspx?" + "docCve=" + stransDetId + "&idM=" + Pag_sidM;
                string script = "function f(){$find(\"" + FNMttoDocPermisos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

            }
            //DESCUENTOS
            if (hdfBtnAccion.Value == "rBtnDescuentos")
            {
                Int64 Pag_sidM = 0;
                if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                {
                    Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                }

                var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem.GetDataKeyValue("docCve").ToString();

                FNMtto.NavigateUrl = "~/DC/MttoDescuentosDocumentos.aspx?" + "docCve=" + stransDetId + "&idM=" + Pag_sidM;
                FNMtto.Title = "Documento Descuentos";
                string script = "function f(){$find(\"" + FNMtto.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            //Lista de Precios
            if (hdfBtnAccion.Value == "rBtnListaPrecios")
            {
                if (FNGrales.bListPrecio(Pag_sConexionLog, Pag_sCompania))
                {
                    Int64 Pag_sidM = 0;
                    if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                    {
                        Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                    }

                    var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                    string stransDetId = dataItem.GetDataKeyValue("docCve").ToString();

                    FNMtto.NavigateUrl = "~/DC/MttoDocumentoListaP.aspx?" + "docCve=" + stransDetId + "&idM=" + Pag_sidM; 
                    FNMtto.Title = "Documento Lista de Precios";
                    string script = "function f(){$find(\"" + FNMtto.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
                }else {
                    string sResult = "", sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1023", ref sMSGTip, ref sResult);
                    ShowAlert(sMSGTip, sResult);
                }
                  
            }
            //Impuestos
            if (hdfBtnAccion.Value == "rBtnImpuestos")
            {
                var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem.GetDataKeyValue("docCve").ToString();

                FNMtto.NavigateUrl = "~/DC/MttoDocumentoImpuesto.aspx?" + "docCve=" + stransDetId + "&idM=" + Pag_sidM;
                FNMtto.Title = "Documento Impuestos";
                string script = "function f(){$find(\"" + FNMtto.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            //Uso CFDI
            if (hdfBtnAccion.Value == "rBtnUsoCFDI")
            {
                var dataItem = rGdv_Documentos.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem.GetDataKeyValue("docCve").ToString();

                FNMttoDocCFDI.NavigateUrl = "~/DC/MttoDocumentoCFDI.aspx?" + "docCve=" + stransDetId + "&idM=" + Pag_sidM; 
                FNMttoDocCFDI.Title = "Uso CFDI";
                string script = "function f(){$find(\"" + FNMttoDocCFDI.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
            }
            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                #region LIMPIAR

              



                rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_Documentos.AllowMultiRowSelection = true;
                rGdv_Documentos.MasterTableView.ClearSelectedItems();

                //GENERA CFDI
                pnlCFDI.Enabled = false;

                //DATOS GENERALES
                rTxtDocCve.Enabled = false;
                rTxtDocDes.Enabled = false;
                rcboFoliador.Enabled = false;
                rCboManejoFolio.Enabled = false;
                rCboManejaDescuento.Enabled = false;
                rCboManejaListaPrecios.Enabled = false;
                rBtnChkProPar.Enabled = false;
                rBtnValExt.Enabled = false;
                rBtnReqAut.Enabled = false;
                rBtnValCreSi.Enabled = false;
                rBtnValCreNo.Enabled = false;
                rBtnValCreAut.Enabled = false;
                rBtnTipDocDeb.Enabled = false;
                rBtnTipDocCre.Enabled = false;
                //mash
                rbtnAplicaIncotermSi.Enabled = false;
                rbtnAplicaIncotermNo.Enabled = false;
                //Actualiza Modulos
                //INVENTARIOS
                rBtnActInvAplica.Enabled = false;
                rBtnActInvNo.Enabled = false;
                rBtnActInvGenera.Enabled = false;
                rCboConceptoInventarios.Enabled = false;

                //CC
                rBtnActCxcAplica.Enabled = false;
                rBtnActCxcNo.Enabled = false;
                rBtnActCxcGenera.Enabled = false;
                rCboConceptoCuentasxCobrar.Enabled = false;

                //CONTABILDAD
                rBtnActContAplica.Enabled = false;
                rBtnActContNo.Enabled = false;
                rBtnActContGenera.Enabled = false;
                rCboConceptoContabilidad.Enabled = false;

                //DOCUMENTOS DERIVADOS
                rCboGenDoc1.Enabled = false;
                rCboDocumento1.Enabled = false;
                rCboGenDoc2.Enabled = false;
                rCboDocumento2.Enabled = false;
                rCboGenDoc3.Enabled = false;
                rCboDocumento3.Enabled = false;

                //Aplicacion Estadistica
                radTotal.Enabled = false;
                radSurtido.Enabled = false;
                rCboConceptoEstadistico.Enabled = false;
                radPedido.Enabled = false;
                radPendiente.Enabled = false;
                rCboFormaAplicacion.Enabled = false;

                //REFERENCIAS
                Panel1.Enabled = false;
                rGdv_Referencias.Enabled = false;

                //VARIABLES
                Panel2.Enabled = false;
                rGdv_Variables.Enabled = false;

                //LAYOUT IMPRESION
                RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
                RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

                //FORMATO REGISTRO
                pnlFormatoRegistro.Enabled = false;

                //GENERA CFDI
                CheckGenFac.Checked = false;
                BROWSER_CHECK();   //<---- PONE TODOS LOS CHECBOX EN FALSE

                //DATOS GENERALES
                rTxtDocCve.Text = "";
                rTxtDocDes.Text = "";
                rCboManejoFolio.ClearSelection();
                rcboFoliador.ClearSelection();
                rCboManejaDescuento.ClearSelection();
                rCboManejaListaPrecios.ClearSelection();
                rBtnChkProPar.Checked = false;
                rBtnValExt.Checked = false;
                rBtnReqAut.Checked = false;
                rBtnValCreSi.Checked = false;
                rBtnValCreNo.Checked = true;
                rBtnValCreAut.Checked = false;
                rBtnTipDocDeb.Checked = true;
                rBtnTipDocCre.Checked = false;
                //mash
                rbtnAplicaIncotermSi.Checked = true;
                rbtnAplicaIncotermNo.Checked = false;

                //ACTUALIZA MODULOS
                //INVENTARIOS
                rCboConceptoInventarios.BorderColor = System.Drawing.Color.Transparent;
                rBtnActInvAplica.Checked = false;
                rBtnActInvNo.Checked = true;
                rBtnActInvGenera.Checked = false;
                rCboConceptoInventarios.ClearSelection();

                //CC
                rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Transparent;
                rBtnActCxcAplica.Checked = false;
                rBtnActCxcNo.Checked = true;
                rBtnActCxcGenera.Checked = false;
                rCboConceptoCuentasxCobrar.ClearSelection();

                //CONTABILIDAD
                rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Transparent;
                rBtnActContAplica.Checked = false;
                rBtnActContNo.Checked = true;
                rBtnActContGenera.Checked = false;
                rCboConceptoContabilidad.ClearSelection();

                //DOCUMENTOS DERIVADOS
                rCboDocumento1.BorderColor = System.Drawing.Color.Transparent;
                rCboDocumento2.BorderColor = System.Drawing.Color.Transparent;
                rCboDocumento3.BorderColor = System.Drawing.Color.Transparent;
                rCboGenDoc1.SelectedValue = "2";
                rCboGenDoc2.SelectedValue = "2";
                rCboGenDoc3.SelectedValue = "2";
                rCboDocumento1.SelectedIndex = -1;
                rCboDocumento1.ClearSelection();
                rCboDocumento2.SelectedIndex = -1;
                rCboDocumento2.ClearSelection();
                rCboDocumento3.SelectedIndex = -1;
                rCboDocumento3.ClearSelection();

                //APLICACION ESTADISTICA
                radTotal.Checked = true;
                radSurtido.Checked = false;
                rCboConceptoEstadistico.ClearSelection();
                radPedido.Checked = false;
                radPendiente.Checked = false;
                rCboFormaAplicacion.ClearSelection();

                //REFERENCIAS
                txtValRef.Text = "";
                rGdv_Referencias.MasterTableView.ClearSelectedItems();

                //VARIABLES
                txtValVar.Text = "";
                rGdv_Variables.MasterTableView.ClearSelectedItems();

                //Layout de Impresión
                RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
                RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

                //FORMATO REGISTRO
                pnlFormatoRegistro.DataBind();
                #endregion
            }
        }


        if (Result == false)
        {
            #region RESULTFALSE

           
            //GENERA CFDI
            pnlCFDI.Enabled = false;

            //DATOS GENERALES
            rTxtDocCve.Enabled = false;
            rTxtDocDes.Enabled = false;
            rcboFoliador.Enabled = false;
            rCboManejoFolio.Enabled = false;
            rCboManejaDescuento.Enabled = false;
            rCboManejaListaPrecios.Enabled = false;
            rBtnChkProPar.Enabled = false;
            rBtnValExt.Enabled = false;
            rBtnReqAut.Enabled = false;
            rBtnValCreSi.Enabled = false;
            rBtnValCreNo.Enabled = false;
            rBtnValCreAut.Enabled = false;
            rBtnTipDocDeb.Enabled = false;
            rBtnTipDocCre.Enabled = false;
            //mash
            rbtnAplicaIncotermSi.Enabled = false;
            rbtnAplicaIncotermNo.Enabled = false;

            //Actualiza Modulos
            //INVENTARIOS
            rBtnActInvAplica.Enabled = false;
            rBtnActInvNo.Enabled = false;
            rBtnActInvGenera.Enabled = false;
            rCboConceptoInventarios.Enabled = false;

            //CC
            rBtnActCxcAplica.Enabled = false;
            rBtnActCxcNo.Enabled = false;
            rBtnActCxcGenera.Enabled = false;
            rCboConceptoCuentasxCobrar.Enabled = false;

            //CONTABILDAD
            rBtnActContAplica.Enabled = false;
            rBtnActContNo.Enabled = false;
            rBtnActContGenera.Enabled = false;
            rCboConceptoContabilidad.Enabled = false;

            //DOCUMENTOS DERIVADOS
            rCboGenDoc1.Enabled = false;
            rCboDocumento1.Enabled = false;
            rCboGenDoc2.Enabled = false;
            rCboDocumento2.Enabled = false;
            rCboGenDoc3.Enabled = false;
            rCboDocumento3.Enabled = false;

            rCboDocumento1.ClearSelection();
            rCboDocumento2.ClearSelection();
            rCboDocumento3.ClearSelection();

            //Aplicacion Estadistica
            radTotal.Enabled = false;
            radSurtido.Enabled = false;
            rCboConceptoEstadistico.Enabled = false;
            radPedido.Enabled = false;
            radPendiente.Enabled = false;
            rCboFormaAplicacion.Enabled = false;

            //REFERENCIAS
            Panel1.Enabled = false;
            rGdv_Referencias.Enabled = false;

            //VARIABLES
            Panel2.Enabled = false;
            rGdv_Variables.Enabled = false;

            //LAYOUT IMPRESION
            RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
            RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

            //FORMATO REGISTRO
            pnlFormatoRegistro.Enabled = false;

            //GENERA CFDI
            CheckGenFac.Checked = false;
            BROWSER_CHECK();   //<---- PONE TODOS LOS CHECBOX EN FALSE

            //DATOS GENERALES
            rTxtDocCve.Text = "";
            rTxtDocDes.Text = "";
            rCboManejoFolio.ClearSelection();
            rcboFoliador.ClearSelection();
            rCboManejaDescuento.ClearSelection();
            rCboManejaListaPrecios.ClearSelection();
            rBtnChkProPar.Checked = false;
            rBtnValExt.Checked = false;
            rBtnReqAut.Checked = false;
            rBtnValCreSi.Checked = false;
            rBtnValCreNo.Checked = true;
            rBtnValCreAut.Checked = false;
            rBtnTipDocDeb.Checked = true;
            rBtnTipDocCre.Checked = false;

            //ACTUALIZA MODULOS
            //INVENTARIOS
            rCboConceptoInventarios.BorderColor = System.Drawing.Color.Transparent;
            rBtnActInvAplica.Checked = false;
            rBtnActInvNo.Checked = true;
            rBtnActInvGenera.Checked = false;
            rCboConceptoInventarios.ClearSelection();

            //CC
            rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Transparent;
            rBtnActCxcAplica.Checked = false;
            rBtnActCxcNo.Checked = true;
            rBtnActCxcGenera.Checked = false;
            rCboConceptoCuentasxCobrar.ClearSelection();

            //CONTABILIDAD
            rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Transparent;
            rBtnActContAplica.Checked = false;
            rBtnActContNo.Checked = true;
            rBtnActContGenera.Checked = false;
            rCboConceptoContabilidad.ClearSelection();

            //DOCUMENTOS DERIVADOS
            rCboDocumento1.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento2.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento3.BorderColor = System.Drawing.Color.Transparent;
            rCboGenDoc1.SelectedValue = "2";
            rCboGenDoc2.SelectedValue = "2";
            rCboGenDoc3.SelectedValue = "2";
            rCboDocumento1.SelectedIndex = -1;
            rCboDocumento1.ClearSelection();
            rCboDocumento2.SelectedIndex = -1;
            rCboDocumento2.ClearSelection();
            rCboDocumento3.SelectedIndex = -1;
            rCboDocumento3.ClearSelection();

            //APLICACION ESTADISTICA
            radTotal.Checked = true;
            radSurtido.Checked = false;
            rCboConceptoEstadistico.ClearSelection();
            radPedido.Checked = false;
            radPendiente.Checked = false;
            rCboFormaAplicacion.ClearSelection();

            //REFERENCIAS
            txtValRef.Text = "";
            rGdv_Referencias.MasterTableView.ClearSelectedItems();

            //VARIABLES
            txtValVar.Text = "";
            rGdv_Variables.MasterTableView.ClearSelectedItems();

            //Layout de Impresión
            RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
            RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

            //FORMATO REGISTRO
            LlenaListaFormatoDocumento_OpcPantalla();
            rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_Documentos.AllowMultiRowSelection = true;
            #endregion
            hdfBtnAccion.Value = "";
        }


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
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //SEGURIDAD
        if (hdfBtnAccion.Value == "rBtnSeguridad")
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //DESCUENTOS
        if (hdfBtnAccion.Value == "rBtnDescuentos")
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //Lista de Precios
        if (hdfBtnAccion.Value == "rBtnListaPrecios")
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //Impuestos
        if (hdfBtnAccion.Value == "rBtnImpuestos")
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //USO CFDI
        if (hdfBtnAccion.Value == "rBtnUsoCFDI")
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Documentos, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }

    private void EjecutaAccionLimpiar()
    {
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            #region NUEVO
            //GENERA CFDI
            CheckGenFac.Checked = false;
            BROWSER_CHECK();   //<---- PONE TODOS LOS CHECBOX EN FALSE

            //DATOS GENERALES
            rTxtDocCve.Text = "";
            rTxtDocDes.Text = "";
            rCboManejoFolio.ClearSelection();
            rcboFoliador.ClearSelection();
            rCboManejaDescuento.ClearSelection();
            rCboManejaListaPrecios.ClearSelection();
            rBtnChkProPar.Checked = false;
            rBtnValExt.Checked = false;
            rBtnReqAut.Checked = false;
            rBtnValCreSi.Checked = false;
            rBtnValCreNo.Checked = true;
            rBtnValCreAut.Checked = false;
            rBtnTipDocDeb.Checked = true;
            rBtnTipDocCre.Checked = false;

            //ACTUALIZA MODULOS
            //INVENTARIOS
            rCboConceptoInventarios.BorderColor = System.Drawing.Color.Transparent;
            rBtnActInvAplica.Checked = false;
            rBtnActInvNo.Checked = true;
            rBtnActInvGenera.Checked = false;
            rCboConceptoInventarios.ClearSelection();
            rCboConceptoInventarios.Enabled = false;


            //CC
            rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Transparent;
            rBtnActCxcAplica.Checked = false;
            rBtnActCxcNo.Checked = true;
            rBtnActCxcGenera.Checked = false;
            rCboConceptoCuentasxCobrar.ClearSelection();
            rCboConceptoCuentasxCobrar.Enabled = false;


            //CONTABILIDAD
            rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Transparent;
            rBtnActContAplica.Checked = false;
            rBtnActContNo.Checked = true;
            rBtnActContGenera.Checked = false;
            rCboConceptoContabilidad.ClearSelection();
            rCboConceptoContabilidad.Enabled = false;

            //DOCUMENTOS DERIVADOS
            rCboDocumento1.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento2.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento3.BorderColor = System.Drawing.Color.Transparent;



            rCboGenDoc1.ClearSelection();
            rCboGenDoc2.ClearSelection();
            rCboGenDoc3.ClearSelection();

            rCboGenDoc1.SelectedValue = "2";
            rCboGenDoc2.SelectedValue = "2";
            rCboGenDoc3.SelectedValue = "2";

            rCboDocumento1.ClearSelection();
            rCboDocumento1.Enabled = false;
            rCboDocumento2.ClearSelection();
            rCboDocumento2.Enabled = false;
            rCboDocumento3.ClearSelection();
            rCboDocumento3.Enabled = false;

            //APLICACION ESTADISTICA
            radTotal.Checked = true;
            radSurtido.Checked = false;
            rCboConceptoEstadistico.ClearSelection();
            radPedido.Checked = false;
            radPendiente.Checked = false;
            rCboFormaAplicacion.ClearSelection();

            //REFERENCIAS
            txtValRef.Text = "";
            rGdv_Referencias.MasterTableView.ClearSelectedItems();

            //VARIABLES
            txtValVar.Text = "";
            rGdv_Variables.MasterTableView.ClearSelectedItems();

            //Layout de Impresión
            RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
            RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

            //FORMATO REGISTRO
            // pnlFormatoRegistro.DataBind();
            this.DataListDocFormatoRegOpcPantalla.DataSource = dsDocumentoFormatoRegistoOpcPantalla(52, "");
            DataListDocFormatoRegOpcPantalla.DataBind();
            #endregion
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            #region MODIFICAR
            rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = true;

            rGdv_Documentos.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtDocCve.CssClass = "cssTxtEnabled";
            rTxtDocDes.CssClass = "cssTxtEnabled";
            rcboFoliador.BorderColor = System.Drawing.Color.Transparent;
            rCboManejaDescuento.BorderColor = System.Drawing.Color.Transparent;
            rCboConceptoInventarios.BorderColor = System.Drawing.Color.Transparent;
            rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Transparent;
            rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento1.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento2.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento3.BorderColor = System.Drawing.Color.Transparent;
            CheckEnvCorr.BorderColor = System.Drawing.Color.Transparent;
            CheckArchXml.BorderColor = System.Drawing.Color.Transparent;
            CheckPDF.BorderColor = System.Drawing.Color.Transparent;
            RadTxtCopiA.CssClass = "cssTxtEnabled";
            CheckGuaXML.BorderColor = System.Drawing.Color.Transparent;
            CheckGuaPDF.BorderColor = System.Drawing.Color.Transparent;

            pnlCFDI.Enabled = false;

            //DATOS GENERALES
            rTxtDocCve.Enabled = false;
            rTxtDocDes.Enabled = false;
            rcboFoliador.Enabled = false;
            rCboManejoFolio.Enabled = false;
            rCboManejaDescuento.Enabled = false;
            rCboManejaListaPrecios.Enabled = false;
            rBtnChkProPar.Enabled = false;
            rBtnValExt.Enabled = false;
            rBtnReqAut.Enabled = false;
            rBtnValCreSi.Enabled = false;
            rBtnValCreNo.Enabled = false;
            rBtnValCreAut.Enabled = false;
            rBtnTipDocDeb.Enabled = false;
            rBtnTipDocCre.Enabled = false;

            //Actualiza Modulos
            //INVENTARIOS
            rBtnActInvAplica.Enabled = false;
            rBtnActInvNo.Enabled = false;
            rBtnActInvGenera.Enabled = false;
            rCboConceptoInventarios.Enabled = false;

            //CC
            rBtnActCxcAplica.Enabled = false;
            rBtnActCxcNo.Enabled = false;
            rBtnActCxcGenera.Enabled = false;
            rCboConceptoCuentasxCobrar.Enabled = false;

            //CONTABILDAD
            rBtnActContAplica.Enabled = false;
            rBtnActContNo.Enabled = false;
            rBtnActContGenera.Enabled = false;
            rCboConceptoContabilidad.Enabled = false;

            //DOCUMENTOS DERIVADOS
            rCboGenDoc1.Enabled = false;
            rCboDocumento1.Enabled = false;
            rCboGenDoc2.Enabled = false;
            rCboDocumento2.Enabled = false;
            rCboGenDoc3.Enabled = false;
            rCboDocumento3.Enabled = false;

            //Aplicacion Estadistica
            radTotal.Enabled = false;
            radSurtido.Enabled = false;
            rCboConceptoEstadistico.Enabled = false;
            radPedido.Enabled = false;
            radPendiente.Enabled = false;
            rCboFormaAplicacion.Enabled = false;

            //REFERENCIAS
            Panel1.Enabled = false;
            rGdv_Referencias.Enabled = false;

            //VARIABLES
            Panel2.Enabled = false;
            rGdv_Variables.Enabled = false;

            //LAYOUT IMPRESION
            RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
            RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

            //FORMATO REGISTRO
            pnlFormatoRegistro.Enabled = false;

            //GENERA CFDI
            CheckGenFac.Checked = false;
            BROWSER_CHECK();   //<---- PONE TODOS LOS CHECBOX EN FALSE

            //DATOS GENERALES
            rTxtDocCve.Text = "";
            rTxtDocDes.Text = "";
            rCboManejoFolio.ClearSelection();
            rcboFoliador.ClearSelection();
            rCboManejaDescuento.ClearSelection();
            rCboManejaListaPrecios.ClearSelection();
            rBtnChkProPar.Checked = false;
            rBtnValExt.Checked = false;
            rBtnReqAut.Checked = false;
            rBtnValCreSi.Checked = false;
            rBtnValCreNo.Checked = true;
            rBtnValCreAut.Checked = false;
            rBtnTipDocDeb.Checked = true;
            rBtnTipDocCre.Checked = false;

            //ACTUALIZA MODULOS
            //INVENTARIOS
            rCboConceptoInventarios.BorderColor = System.Drawing.Color.Transparent;
            rBtnActInvAplica.Checked = false;
            rBtnActInvNo.Checked = true;
            rBtnActInvGenera.Checked = false;
            rCboConceptoInventarios.ClearSelection();

            //CC
            rCboConceptoCuentasxCobrar.BorderColor = System.Drawing.Color.Transparent;
            rBtnActCxcAplica.Checked = false;
            rBtnActCxcNo.Checked = true;
            rBtnActCxcGenera.Checked = false;
            rCboConceptoCuentasxCobrar.ClearSelection();

            //CONTABILIDAD
            rCboConceptoContabilidad.BorderColor = System.Drawing.Color.Transparent;
            rBtnActContAplica.Checked = false;
            rBtnActContNo.Checked = true;
            rBtnActContGenera.Checked = false;
            rCboConceptoContabilidad.ClearSelection();

            //DOCUMENTOS DERIVADOS
            rCboDocumento1.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento2.BorderColor = System.Drawing.Color.Transparent;
            rCboDocumento3.BorderColor = System.Drawing.Color.Transparent;

            rCboGenDoc1.SelectedValue = "2";
            rCboGenDoc2.SelectedValue = "2";
            rCboGenDoc3.SelectedValue = "2";

            rCboDocumento1.ClearSelection();
            rCboDocumento2.ClearSelection();
            rCboDocumento3.ClearSelection();

            //APLICACION ESTADISTICA
            radTotal.Checked = true;
            radSurtido.Checked = false;
            rCboConceptoEstadistico.ClearSelection();
            radPedido.Checked = false;
            radPendiente.Checked = false;
            rCboFormaAplicacion.ClearSelection();

            //REFERENCIAS
            txtValRef.Text = "";
            rGdv_Referencias.MasterTableView.ClearSelectedItems();

            //VARIABLES
            txtValVar.Text = "";
            rGdv_Variables.MasterTableView.ClearSelectedItems();

            //Layout de Impresión
            RadImageGallery1.ImageAreaSettings.ShowNextPrevImageButtons = false;
            RadImageGallery1.ImageAreaSettings.ShowDescriptionBox = false;

            //FORMATO REGISTRO
            pnlFormatoRegistro.Enabled = false;

            LlenaListaFormatoDocumento_OpcPantalla();
            rGdv_Documentos.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_Documentos.AllowMultiRowSelection = true;


            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;

            hdfBtnAccion.Value = "";
            #endregion

        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }
    

    }




}