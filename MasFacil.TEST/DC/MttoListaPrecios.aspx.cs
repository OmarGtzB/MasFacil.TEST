using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Telerik.Web.UI;
using Telerik.Windows;
using System.Web.UI.HtmlControls;
using System.Data.Entity;
using System.Windows.Forms;
using System.Globalization;
using Telerik.Web.UI.GridExcelBuilder;
using Telerik.Web.UI.ExportInfrastructure;



public partial class DC_MttoListaPrecios : System.Web.UI.Page
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
    MGMFnGrales.FnParametros FNParam = new MGMFnGrales.FnParametros();
    MGMFnGrales.FnValidaciones FNValida = new MGMFnGrales.FnValidaciones();
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();

    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;
    private string Pag_BtnAccion;
    
    //DataTable dtTmpDet = new DataTable();
    //private static int opcTabStc = 1;
    //private static Int64 cveTabStc = 0;
    
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
    
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //if (hdfBtnAccion.Value != "" && hdfBtnAccionDet.Value == "" && pnlHead.Visible)
        //{
        //    EjecutaAccion();
        //}
        //else if (hdfBtnAccion.Value != "" && hdfBtnAccionDet.Value != "")
        //{
        //    ejecutaAccionDetalle();
        //} 
        if (hdfBtnAccion.Value != "" && hdfBtnAccionDet.Value == "" && pnlHead.Visible)
        {
            EjecutaAccion();
        }
        else if (hdfBtnAccionDet.Value != "")
        {
            ejecutaAccionDetalle();
        }
    }
    
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        cleanUi();
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();

        //lgdForm.InnerText = "Nueva Lista de Precios";

        //divForm.Visible = true;
        //divGrid.Visible = false;

        //rTxtClave.Enabled = true;

        //hdfBtnAccion.Value = "1";
        
    }

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //cleanUi();
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        ControlesAccion();
        
    }

    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        cleanUi();
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click1(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccionLimpiar();
    }





    protected void rImgBtnAceptarP_Click(object sender, ImageButtonClickEventArgs e)
    {
        //ejecutaAccionDetalle();
    }
    
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        
        InicioPagina();
       
    }
    
    protected void RdDateFecha_Final_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //Val_Fec_Fin = "";
        //DateTime dt = Convert.ToDateTime(RdDateFecha_Final.SelectedDate);
        //Val_Fec_Fin = dt.Year + "-" + dt.Month.ToString().PadLeft(2, '0') + "-" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            //loadGridFltr();
            //rCboMoneda.Focus();
        }
        else
        {
            RdDateFecha_Final.Clear();
            //RdDateFecha_Final.Focus();
            RadWindowManagerPage.RadAlert("La Fecha Final no puede ser Menor a la Inical", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");

        }
    }

    protected void RdDateFecha_Inicio_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        //Val_Fec_Inicio = "";
        //DateTime dt = Convert.ToDateTime(RdDateFecha_Inicio.SelectedDate);
        //Val_Fec_Inicio = dt.Year + "-" + dt.Month.ToString().PadLeft(2, '0') + "-" + dt.Day.ToString().PadLeft(2, '0');

        if (compararFechas() == true)
        {
            //RdDateFecha_Final.Focus();
        }
        else
        {
            RdDateFecha_Inicio.Clear();
            //RdDateFecha_Inicio.Focus();
            RadWindowManagerPage.RadAlert("La Fecha Inicial no puede ser mayor a la Final", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
        }
    }
    
    protected void rImgBtnCancelarP_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = "";
        ControlesAccionP();
        cleanUiFrmPart();
    }

    protected void rBtnNuevoP_Click(object sender, ImageButtonClickEventArgs e)
    {
        //cleanUiFrmPart();
        hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccionP();

    }

    protected void rBtnModificarP_Click(object sender, ImageButtonClickEventArgs e)
    {

        //cleanUiFrmPart();
        hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        //ControlesAccionP();
        ControlesAccionP();
    }

    protected void rBtnEliminarP_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccionDet.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccionP();
    }
    
    protected void rBtnLimpiarP_Click(object sender, ImageButtonClickEventArgs e)
    {
        //cleanUiFrmPart();
        //ControlesAccionP();
        EjecutaAccionLimpiarP();
    }
    
    protected void rGdvListDetalle_SelectedIndexChanged(object sender, EventArgs e)
    {
        object maxSec = 0;
        //controlesAccionDet();
        rTxtMinimo.Enabled = false;
        rTxtMaximo.Enabled = false;


        rCboArticulo.Enabled = false;

        DataTable dtTmpDet = new DataTable();
        dtTmpDet = (DataTable)Session["dtTmpDetLP"];

        var dataItem = rGdvListDetalle.SelectedItems[0] as GridDataItem;
        //if (dataItem != null && hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        //{

            rCboArticulo.SelectedValue = dataItem.Cells[3].Text.Trim();
            rTxtMinimo.Text = dataItem.Cells[5].Text.Trim();
            rTxtMaximo.Text = dataItem.Cells[6].Text.Trim();
            rTxtPartPrec.Text = dataItem.Cells[7].Text.Trim();


            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())

            {
            if (dtTmpDet.Rows.Count > 0)
            {
                // maxSec = Convert.ToInt32(dtTmpDet.Compute("max(lisPreSec)", ""));
                maxSec = dtTmpDet.Compute("max(lisPreSec)", "artCve =  '" + dataItem.Cells[3].Text.Trim().ToString() + "'");
                

                if (rBtnModificarP.Image.Url == "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png")
                {
                    if (Convert.ToInt32(dataItem.Cells[2].Text) == Convert.ToInt32(maxSec))
                    {
                        rTxtMinimo.Enabled = false;
                        rTxtMaximo.Enabled = true;
                        rTxtPartPrec.Enabled = true;
                        rCboDescuentos.Enabled = true;
                        rCboImpuestos.Enabled = true;
                    }
                    else
                    {
                        rTxtMinimo.Enabled = false;
                        rTxtMaximo.Enabled = false;
                        rTxtPartPrec.Enabled = true;
                        rCboDescuentos.Enabled = true;
                        rCboImpuestos.Enabled = true;
                    }
                }
            }
        }




            if (dataItem.Cells[10].Text.Trim() != "&nbsp;")
            {
                rCboDescuentos.SelectedValue = dataItem.Cells[10].Text.Trim();
            }
            else
            {
                rCboDescuentos.ClearSelection();
            }

            if (dataItem.Cells[11].Text.Trim() != "&nbsp;")
            {
                rCboImpuestos.SelectedValue = dataItem.Cells[11].Text.Trim();
            }
            else
            {
                rCboImpuestos.ClearSelection();
            }

            // controlesAccionDet();
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())

            {
                fldDetalle.Visible = true;
                fldDetalle.Attributes.Remove("hidden");
            }


            dataItem.Selected = true;
        //}
        //else
        //{
        //    fldDetalle.Visible = true;
        //    fldDetalle.Attributes.Remove("hidden");
        //}


        Session["dtTmpDetLP"] = dtTmpDet;

    }

    protected void rGdvList_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
        //    hdfBtnAccion.Value == "")
        //{

            //rBtnArticulos.Visible = true;
            
            Int64 pLisCve = 0;

            var dataItem = rGdvList.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {

                pLisCve = Convert.ToInt64(dataItem.GetDataKeyValue("lisPreCve").ToString());

                //loadGridDetalle(pLisCve);
                loadUi(pLisCve);

                //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                //{
                //    rTxtAbreviatura.Enabled = true;
                //    rTxtDescripcion.Enabled = true;
                //    RdDateFecha_Inicio.Enabled = true;
                //    RdDateFecha_Final.Enabled = true;
                //    rCboMoneda.Enabled = true;

                //}

               // ControlesAccionP();

                //if (hdfBtnAccion.Value == "")
                //{
                //    rBtnNuevoP.Enabled = false;
                //    rBtnModificarP.Enabled = false;
                //    rBtnEliminarP.Enabled = false;
                //    rBtnLimpiarP.Enabled = false;

                //    rBtnCancelar.Enabled = true;
                //}else
                //{
                //    rBtnNuevoP.Enabled = true;
                //    rBtnModificarP.Enabled = true;
                //    rBtnEliminarP.Enabled = true;
                //    rBtnLimpiarP.Enabled = true;

                //    //rBtnGuardar.Enabled = true;
                //}


            }
            
        //}
        //else
        //{
        //    //rBtnArtLisPre.Visible = false;
        //}


    }

    public void ConfigureExport()
    {
        rGdvList.ExportSettings.ExportOnlyData = true;
        rGdvList.ExportSettings.IgnorePaging = true;
        rGdvList.ExportSettings.OpenInNewWindow = true;
    }

    protected void rGdvList_ItemCommand(object sender, GridCommandEventArgs e)
    {

        if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
            e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
            e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
        {
            ConfigureExport();
        }

        if (e.CommandName == "RowSingleClick")
        {
            //RadLabel1.Text = "Single click of row which has datakey (ID) :" + e.CommandArgument;
            if (hdfBtnAccion.Value != "")
            {
                loadUi(Convert.ToInt64(e.CommandArgument.ToString()));
            }

        }
        else if (e.CommandName == "RowDoubleClick")
        {
            //RadLabel1.Text = "Double click of row which has datakey (ID) :" + e.CommandArgument;


            hdfBtnAccion.Value.ToString();
            loadUi(Convert.ToInt64(e.CommandArgument.ToString()));

            showTab("2", e.CommandArgument.ToString());

            //ShowAlert("1", "Double");

            if (hdfBtnAccion.Value == "")
            {
                rBtnCancelar.Enabled = true;
                fldDetalle.Disabled = true;

                rBtnNuevoP.Enabled = false;
                rBtnModificarP.Enabled = false;
                rBtnEliminarP.Enabled = false;
                rBtnLimpiarP.Enabled = false;

                

            }
            else
            {

                rBtnNuevoP.Enabled = true;
                rBtnModificarP.Enabled = true;
                rBtnEliminarP.Enabled = true;
                rBtnLimpiarP.Enabled = true;
                
            }

        }


        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            rGdvList.ExportSettings.Excel.Format = GridExcelExportFormat.Biff;
            rGdvList.ExportSettings.IgnorePaging = false;
            rGdvList.ExportSettings.ExportOnlyData = true;
            rGdvList.ExportSettings.OpenInNewWindow = true;
        }


    }

    protected void rTxtMinimo_TextChanged(object sender, EventArgs e)
    {
        if (rTxtMaximo.Text != "")
        {
            if (PosIni_Fin())
            {
                //rTxtMaximo.Focus();
            }
            else
            {
                rTxtMinimo.Text = "";
                RadWindowManagerPage.RadAlert("La Cantidad Minima no puede ser Mayor al Maximo", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
                //rTxtMinimo.Focus();
            }
        }
        else
        {
            //rTxtMaximo.Focus();
        }

    }

    protected void rTxtMaximo_TextChanged(object sender, EventArgs e)
    {
        if (PosIni_Fin())
        {
            //rTxtPartPrec.Focus();
        }
        else
        {
            rTxtMaximo.Text = "";
            RadWindowManagerPage.RadAlert("La Cantidad Maxima no puede ser Menor al Minimo", 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + 3 + ".png");
            //rTxtMaximo.Focus();
        }
    }
    
    protected void rBtnMonedaCve_Click(object sender, ImageButtonClickEventArgs e)
    {

    }

    protected void rBtnArtLisPre_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "rBtnArtLisPre";
        ControlesAccion();


        //Int64 pLisCve = 0;

        //if (rGdvList.SelectedItems.Count > 0)
        //{
        //    var dataItem = rGdvList.SelectedItems[0] as GridDataItem;
        //    if (dataItem != null)
        //    {

        //        pLisCve = Convert.ToInt64(dataItem.GetDataKeyValue("lisPreCve").ToString());
        //        showTab("2", pLisCve.ToString());

        //        opcTabStc.Value = "2";
        //        cveTabStc.Value = pLisCve.ToString();

        //        radlb_ClaveLista.Text = rTxtClave.Text + " " + rTxtDescripcion.Text;


        //        rBtnGuardar.Enabled = false;
        //        rBtnCancelar.Enabled=true;

        //        resetStyleControls();

        //    }

        //}
        //else
        //{

        //    string sResult = "", sMSGTip = "";
        //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1022", ref sMSGTip, ref sResult);
        //    ShowAlert(sMSGTip, sResult);
        //}

        //string sResult = "", sMSGTip = "";
        //if (rGdvList.SelectedItems.Count > 0)
        //{

        //    if (rGdvList.SelectedItems.Count > 1)
        //    {
        //        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0008", ref sMSGTip, ref sResult);
        //        ShowAlert(sMSGTip, sResult);
        //    }
        //    else
        //    {

        //        var dataItem = rGdvList.SelectedItems[0] as GridDataItem;
        //        if (dataItem != null)
        //        {

        //            pLisCve = Convert.ToInt64(dataItem.GetDataKeyValue("lisPreCve").ToString());
        //            showTab("2", pLisCve.ToString());

        //            opcTabStc.Value = "2";
        //            cveTabStc.Value = pLisCve.ToString();

        //            radlb_ClaveLista.Text = rTxtClave.Text + " " + rTxtDescripcion.Text;


        //            rBtnGuardar.Enabled = false;
        //            rBtnCancelar.Enabled = true;

        //            resetStyleControls();

        //        }


        //    }


        //}
        //else
        //{

        //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
        //    ShowAlert(sMSGTip, sResult);
        //}

    }


    protected void rCboArticulo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            DataTable dtTmpDet = new DataTable();
            dtTmpDet = (DataTable)Session["dtTmpDetLP"];

            int secToRow = 1;
            decimal valMax = 0, val = 0;
            DataRow row = dtTmpDet.NewRow();

            //Conseguir la Secuencia del Articulo

            foreach (DataRow rowSec in dtTmpDet.Rows)
            {
                if (rowSec["artCve"].ToString() == rCboArticulo.SelectedValue)
                {
                    secToRow++;
                    valMax = Convert.ToDecimal(rowSec["lisPreCanMax"]);
                }
            }
            valMax++;

            rTxtMinimo.Enabled = false;
            rTxtMinimo.Text = valMax.ToString();
        }
    }

    protected void rBtnDocumentosListaPrecios_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "rBtnDocumentosListaPrecios";
        ControlesAccion();
        //if (FNGrales.bListPrecio(Pag_sConexionLog, Pag_sCompania))
        //{
        //    if (rGdvList.SelectedItems.Count > 0)
        //    {
        //        var dataItem = rGdvList.SelectedItems[0] as GridDataItem;
        //        string stransDetId = dataItem.GetDataKeyValue("lisPreCve").ToString();

        //        Response.Redirect( "~/DC/MttoDocumentoListaP.aspx?" + "lisPreCve=" + stransDetId);
        //       // string script = "function f(){$find(\"" + FNMttoDocDescuentos.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        //        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);
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

        //string sResult = "", sMSGTip = "";
        //if (rGdvList.SelectedItems.Count > 0)
        //{

        //    if (rGdvList.SelectedItems.Count > 1)
        //    {
        //        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0008", ref sMSGTip, ref sResult);
        //        ShowAlert(sMSGTip, sResult);
        //    }
        //    else
        //    {

        //        var dataItem = rGdvList.SelectedItems[0] as GridDataItem;
        //        string stransDetId = dataItem.GetDataKeyValue("lisPreCve").ToString();

        //        Response.Redirect("~/DC/MttoDocumentoListaP.aspx?" + "lisPreCve=" + stransDetId);
         

        //}


        //}
        //else
        //{

        //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
        //    ShowAlert(sMSGTip, sResult);
        //}

    }

    protected void rBtnListaP_Agrupaciones_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = "rBtnListaP_Agrupaciones";
        ControlesAccion();
        //if (rGdvList.SelectedItems.Count > 0)
        //{
        //    var dataItem = rGdvList.SelectedItems[0] as GridDataItem;
        //    string stransDetId = dataItem.GetDataKeyValue("lisPreCve").ToString();
        //    Response.Redirect("~/DC/MttoListaPAgrupacion.aspx?" + "lisPreCve=" + stransDetId);
        //}
        //else
        //{
        //    string sResult = "", sMSGTip = "";
        //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1022", ref sMSGTip, ref sResult);
        //    ShowAlert(sMSGTip, sResult);
        //}


        //string sResult = "", sMSGTip = "";
        //if (rGdvList.SelectedItems.Count > 0)
        //{
        //    if (rGdvList.SelectedItems.Count > 1)
        //    {
        //        FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0008", ref sMSGTip, ref sResult);
        //        ShowAlert(sMSGTip, sResult);
        //    }
        //    else
        //    {

        //        var dataItem = rGdvList.SelectedItems[0] as GridDataItem;
        //        string stransDetId = dataItem.GetDataKeyValue("lisPreCve").ToString();
        //        Response.Redirect("~/DC/MttoListaPAgrupacion.aspx?" + "lisPreCve=" + stransDetId);
        //    }
        //}
        //else
        //{
        //    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
        //    ShowAlert(sMSGTip, sResult);
        //}
    }

    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        Pag_BtnAccion = Convert.ToString(Session["BtnAccion"]);

        //opcTabStc.Value = "1";
        //cveTabStc.Value = "0";

    }


    public void InicioPagina()
    {
        Session["dtTmpDetLP"] = null;
        hdfBtnAccion.Value = "";
        hdfBtnAccionDet.Value = "";
        loadGrid();
        llenaCombos();
        opcTabStc.Value = "1";
        cveTabStc.Value = "0";
        showTab(opcTabStc.Value, cveTabStc.Value);
        cleanUi();
        ControlesAccionP();
        ControlesAccion();
        resetStyleControls();
        rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvList.AllowMultiRowSelection = true;
        rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvListDetalle.AllowMultiRowSelection = true;
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

    private void cleanUi()
    {
        rTxtClave.Text = "";
        rTxtAbreviatura.Text = "";
        rTxtDescripcion.Text = "";

        RdDateFecha_Inicio.Clear();
        RdDateFecha_Final.Clear();

        rCboMoneda.ClearSelection();

        cleanUiFrmPart();

    }

    private void cleanUiFrmPart()
    {

        rTxtMaximo.Text = "";
        rTxtMinimo.Text = "";
        rTxtPartPrec.Text = "1";

        rCboArticulo.ClearSelection();
        rCboDescuentos.ClearSelection();
        rCboImpuestos.ClearSelection();

    }

    private void llenaCombos()
    {
        FnCtlsFillIn.RadComboBox_Monedas(Pag_sConexionLog, Pag_sCompania, ref rCboMoneda, true, false);
        FnCtlsFillIn.RabComboBox_Articulos(Pag_sConexionLog, Pag_sCompania, ref rCboArticulo, true, false, "");
        fillCboDescuentos();
        fillCboImpuestos();

    }
    
    private void fillCboDescuentos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Descuentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@desCve", DbType.String, 10, ParameterDirection.Input, "");

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboDescuentos, ds, "DesCve", "DesDes", true, false);
        ((Literal)rCboDescuentos.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboDescuentos.Items.Count);

    }
    
    private void fillCboImpuestos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Impuestos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@impCve", DbType.String, 10, ParameterDirection.Input, "");

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadComboBox(ref rCboImpuestos, ds, "impCve", "impDes", true, false);
        ((Literal)rCboImpuestos.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboImpuestos.Items.Count);

    }
    
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    private void loadUi(Int64 lisPreCve)
    {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ListaPrecios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 61);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, lisPreCve);
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {

                rTxtClave.Text = ds.Tables[0].Rows[0]["lisPreCve"].ToString();
                rTxtAbreviatura.Text = ds.Tables[0].Rows[0]["lisPreAbr"].ToString();
                rTxtDescripcion.Text = ds.Tables[0].Rows[0]["lisPreDes"].ToString();
                RdDateFecha_Inicio.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["lisPreIniVigen"].ToString());
                RdDateFecha_Final.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["lisPreFinVigen"].ToString());
                rCboMoneda.SelectedValue = ds.Tables[0].Rows[0]["monCve"].ToString();

            }

        }
        catch (Exception ex)
        {
            ex.ToString();
            throw;
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


                if (opcTabStc.Value == "1")
                {
                    cleanUi();
                    hdfBtnAccion.Value = "";
                    ControlesAccion();
                    loadGrid();
                }
                else
                {
                    cleanUiFrmPart();
                    hdfBtnAccionDet.Value = "";
                    ControlesAccion();
                    ControlesAccionP();
                }



            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
            }

        }
        else
        {
            ShowAlert("2", msgValidacion);
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




            foreach (GridDataItem i in rGdvList.SelectedItems)
            {

                var dataItem = rGdvList.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    string artEstCodElem = dataItem["lisPreCve"].Text;
                    try
                    {

                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_ListaPrecios";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(artEstCodElem));
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
                    //LlenaGridAlmacenes();
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
                    sMsgAlert = "Registros eliminados" + " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "")
                    {
                        sMsgAlert = sMsgAlert + "</br>";
                    }

                    sMsgAlert = sMsgAlert + "Registros no eliminados" + " " + CantItemsElimFalse.ToString();
                }


                ShowAlert(sEstatusAlert, sMsgAlert);
                if (CountItems == CantItemsElimTrue)
                {
                    InicioPagina();
                }
                else
                {
                    //LlenaGridAlmacenes();
                    InicioPagina();
                }

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void loadGrid()
    {

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ListaPrecios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);


            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds != null)
            {
                rGdvList.DataSource = ds;
                rGdvList.DataBind();
            }

        }
        catch (Exception ex)
        {
            ShowAlert("1", ex.ToString());
            throw;
        }
    }

    private void loadGridDetalle(Int64 pLisPreCve)
    {

        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ListaPreciosDetalle";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, pLisPreCve);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (ds != null)
            {
                DataTable dtTmpDet = new DataTable();
                dtTmpDet = (DataTable)Session["dtTmpDetLP"];

                dtTmpDet = ds.Tables[0];

                rGdvListDetalle.DataSource = dtTmpDet;
                rGdvListDetalle.DataBind();

                Session["dtTmpDetLP"] = dtTmpDet;
            }

        }
        catch (Exception ex)
        {
            ShowAlert("1", ex.ToString());
            throw;
        }
    }

    private void EjecutaSpAcciones()
    {

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
            ProcBD.NombreProcedimiento = "sp_ListaPrecios";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);

            ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(rTxtClave.Text));
            ProcBD.AgregarParametrosProcedimiento("@lisPreAbr", DbType.String, 10, ParameterDirection.Input, rTxtAbreviatura.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@lisPreDes", DbType.String, 40, ParameterDirection.Input, rTxtDescripcion.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@lisPreIniVigen", DbType.String, 100, ParameterDirection.Input, Val_Fec_Inicio);
            ProcBD.AgregarParametrosProcedimiento("@lisPreFinVigen", DbType.String, 100, ParameterDirection.Input, Val_Fec_Fin);
            ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, rCboMoneda.SelectedValue);

            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                if (sEjecEstatus == "1" && hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    //EjecutaSpAccionDet(rTxtClave.Text);

                }

                ShowAlert(sEjecEstatus, sEjecMSG);
            }


        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }

    private void EjecutaSpAccionDet(string lisPreCve)
    {

        try
        {
            if (rGdvListDetalle.Items.Count > 0)
            {
                for (int i = 0; i < rGdvListDetalle.Items.Count; i++)
                {


                    //Primero Borrar en primer registro

                    if (i == 0)
                    {
                        DataSet dsE = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBDE = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBDE.NombreProcedimiento = "sp_ListaPreciosDetalle";
                        ProcBDE.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                        ProcBDE.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBDE.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(lisPreCve));
                        dsE = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBDE.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    }

                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_ListaPreciosDetalle";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 1);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(lisPreCve));
                    ProcBD.AgregarParametrosProcedimiento("@lisPreSec", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(rGdvListDetalle.Items[i].Cells[2].Text.Trim()));
                    ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, rGdvListDetalle.Items[i].Cells[3].Text.Trim());

                    ProcBD.AgregarParametrosProcedimiento("@lisPreCanMin", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rGdvListDetalle.Items[i].Cells[5].Text.Trim()));
                    ProcBD.AgregarParametrosProcedimiento("@lisPreCanMax", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rGdvListDetalle.Items[i].Cells[6].Text.Trim()));
                    ProcBD.AgregarParametrosProcedimiento("@lisPrecio", DbType.Decimal, 15, ParameterDirection.Input, Convert.ToDecimal(rGdvListDetalle.Items[i].Cells[7].Text.Trim()));

                    if (rGdvListDetalle.Items[i].Cells[10].Text.Trim() != "&nbsp;" && rGdvListDetalle.Items[i].Cells[10].Text.Trim() != "0")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@DesCve", DbType.String, 10, ParameterDirection.Input, rGdvListDetalle.Items[i].Cells[10].Text.Trim());
                    }

                    if (rGdvListDetalle.Items[i].Cells[11].Text.Trim() != "&nbsp;")
                    {
                        ProcBD.AgregarParametrosProcedimiento("@impCve", DbType.String, 5, ParameterDirection.Input, rGdvListDetalle.Items[i].Cells[11].Text.Trim());
                    }

                    ProcBD.AgregarParametrosProcedimiento("@monCve", DbType.String, 2, ParameterDirection.Input, rCboMoneda.SelectedValue);
                    //Obtener Unidad de Medida Atumaticamente
                    ProcBD.AgregarParametrosProcedimiento("@uniMedCve", DbType.String, 6, ParameterDirection.Input, getUniMed(rGdvListDetalle.Items[i].Cells[3].Text.Trim()));

                    ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                    if (ds != null)
                    {

                    }

                }
            }else
            {

                if (true)
                {
                    DataSet dsE = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBDE = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBDE.NombreProcedimiento = "sp_ListaPreciosDetalle";
                    ProcBDE.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 3);
                    ProcBDE.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBDE.AgregarParametrosProcedimiento("@lisPreCve", DbType.Int64, 0, ParameterDirection.Input, Convert.ToInt64(lisPreCve));
                    dsE = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBDE.ObtenerXmlProcedimiento(), Pag_sConexionLog);

                }

            }
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    private void ejecutaAccionDetalle()
    {

        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccionDetalle(ref sMSGTip);

        if (msgValidacion == "")
        {

            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                addRow();

                EjecutaSpAccionDet(rTxtClave.Text);

                string sResult = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0001", ref sMSGTip, ref sResult);
                ShowAlert(sMSGTip, sResult);

            }
            else if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                ediRow();

                EjecutaSpAccionDet(rTxtClave.Text);

                string sResult = "";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0002", ref sMSGTip, ref sResult);
                ShowAlert(sMSGTip, sResult);

            }
            else if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                delRow();
                EjecutaSpAccionDet(rTxtClave.Text);
            }

            hdfBtnAccionDet.Value = "";

            ControlesAccionP();
            cleanUiFrmPart();

        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }

        
        
    }

    private void delRow()
    {

        int CountItems = 0;
        int CantItemsElimTrue = 0;
        int CantItemsElimFalse = 0;
        string EstatusItemsElim = "";
        string MsgItemsElim = "";
        string MsgItemsElimTrue = "";
        string MsgItemsElimFalse = "";

        if (rGdvListDetalle.Items.Count > 0)
        {

            //dtTmpDet.AcceptChanges();
            DataTable dtTmpDet = new DataTable();
            dtTmpDet = (DataTable)Session["dtTmpDetLP"];

            dtTmpDet.AcceptChanges();

            for (int i = 0; i < rGdvListDetalle.Items.Count; i++)
            {

                if (rGdvListDetalle.Items[i].Selected == true && chkLastSecArt(rGdvListDetalle.Items[i].Cells[3].Text, rGdvListDetalle.Items[i].Cells[2].Text))
                {
                    dtTmpDet.Rows[i].Delete();
                    CantItemsElimTrue++;
                    EstatusItemsElim = "1";
                    CountItems += 1;
                }
                else if(rGdvListDetalle.Items[i].Selected == true && chkLastSecArt(rGdvListDetalle.Items[i].Cells[3].Text, rGdvListDetalle.Items[i].Cells[2].Text) == false)
                {
                    CantItemsElimFalse++;
                    //EstatusItemsElim = "2";

                    //Mensaje en tabla
                    
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1024", ref EstatusItemsElim, ref MsgItemsElim);
                    
                    CountItems += 1;
                }

                

            }
            
            dtTmpDet.AcceptChanges();

            string sEstatusAlert = "2";
            string sMsgAlert = "";

            if (CountItems == 1)
            {

                sEstatusAlert = EstatusItemsElim;
                if (sEstatusAlert == "1")
                {
                    sMsgAlert = "MSG: El registro se elimino correctamente.";
                }
                else
                {
                    sMsgAlert = MsgItemsElim;
                }


                

                if (sEstatusAlert == "1")
                {
                    rGdvListDetalle.DataSource = dtTmpDet;
                    rGdvListDetalle.DataBind();


                    ShowAlert(sEstatusAlert, sMsgAlert);

                }
                else
                {
                    rGdvListDetalle.DataSource = dtTmpDet;
                    rGdvListDetalle.DataBind();

                    ShowAlert(sEstatusAlert, sMsgAlert);
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
                    sMsgAlert = "Registros eliminados" + " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)
                {
                    if (sMsgAlert != "")
                    {
                        sMsgAlert = sMsgAlert + "</br>";
                    }

                    sMsgAlert = sMsgAlert + "Registros no eliminados" + " " + CantItemsElimFalse.ToString();
                }


                ShowAlert(sEstatusAlert, sMsgAlert);
                if (CountItems == CantItemsElimTrue)
                {
                    rGdvListDetalle.DataSource = dtTmpDet;
                    rGdvListDetalle.DataBind();
                }
                else
                {
                    //LlenaGridAlmacenes();
                    rGdvListDetalle.DataSource = dtTmpDet;
                    rGdvListDetalle.DataBind();
                }

            }
            
            //dtTmpDet.AcceptChanges();
            Session["dtTmpDetLP"] = dtTmpDet;

        }


    }

    private bool chkLastSecArt(string pArtCve, string pArtSec)
    {
        bool response = false;


        try
        {
            DataTable dtTmpDet = new DataTable();
            dtTmpDet = (DataTable)Session["dtTmpDetLP"];
            DataView dv = dtTmpDet.DefaultView;
            dv.Sort = "artCve";
            DataTable sortedDT = dv.ToTable();

            int contSec = 0;

            foreach (DataRow sortRow in sortedDT.Rows)
            {

                if (sortRow["artCve"].ToString() == pArtCve)
                {
                    contSec++;
                }

            }

            if (pArtSec != contSec.ToString())
            {
                response = false;
            }else
            {
                response = true;
            }

        }
        catch (Exception ex)
        {
            response = false;
            
        }



        return response;

    }
    
    private void ediRow()
    {
        int maxSec = 1;
        int maxMovDetID = 1;

        int secToRow = 1;
        decimal valMax = 0, val = 0;

        if (rGdvListDetalle.Items.Count > 0)
        {

            DataTable dtTmpDet = new DataTable();
            dtTmpDet = (DataTable)Session["dtTmpDetLP"];

            for (int i = 0; i < rGdvListDetalle.Items.Count; i++)
            {

                if (rGdvListDetalle.Items[i].Selected == true)
                {
                    dtTmpDet.Rows[i]["artCve"] = rCboArticulo.SelectedValue;

                    dtTmpDet.Rows[i]["artDes"] = rCboArticulo.Text;

                    dtTmpDet.Rows[i]["lisPrecio"] = rTxtPartPrec.Text;

                    int finalRowChechk = 1;


                    //Buscar si e sla ultima secuencia del articulo seleccionado

                    DataView dv = dtTmpDet.DefaultView;
                    dv.Sort = "artCve";
                    DataTable sortedDT = dv.ToTable();

                    int contSec = 0;

                    foreach (DataRow sortRow in sortedDT.Rows)
                    {

                        if (sortRow["artCve"].ToString() == rGdvListDetalle.SelectedItems[0].Cells[3].Text)
                        {
                            contSec++;
                        }

                    }

                    foreach (DataRow rowSec in dtTmpDet.Rows)
                    {

                        if (rowSec["artCve"].ToString() == rCboArticulo.SelectedValue && rowSec["lisPreSec"].ToString() != contSec.ToString())
                        {
                            secToRow++;
                            valMax = Convert.ToDecimal(rowSec["lisPreCanMax"]);
                        }

                        finalRowChechk++;



                    }
                    valMax++;

                    if (rTxtMinimo.Enabled)
                    {
                        if (valMax < Convert.ToDecimal(rTxtMaximo.Text))
                        {
                            dtTmpDet.Rows[i]["lisPreCanMin"] = valMax;
                            dtTmpDet.Rows[i]["lisPreCanMax"] = rTxtMaximo.Text;
                        }
                        else
                        {
                            dtTmpDet.Rows[i]["lisPreCanMin"] = valMax;

                            val = valMax + 1;

                            dtTmpDet.Rows[i]["lisPreCanMax"] = val;

                        }

                    }


                    if (rCboDescuentos.SelectedValue != "")
                    {
                        dtTmpDet.Rows[i]["desDes"] = rCboDescuentos.Text;
                        dtTmpDet.Rows[i]["DesCve"] = rCboDescuentos.SelectedValue;
                    }
                    else
                    {
                        dtTmpDet.Rows[i]["desDes"] = "";
                        dtTmpDet.Rows[i]["DesCve"] = 0;
                    }

                    if (rCboImpuestos.SelectedValue != "")
                    {
                        dtTmpDet.Rows[i]["impDes"] = rCboImpuestos.Text;
                        dtTmpDet.Rows[i]["impCve"] = rCboImpuestos.SelectedValue;
                    }
                    else
                    {
                        dtTmpDet.Rows[i]["impDes"] = "";
                        dtTmpDet.Rows[i]["impCve"] = "";
                    }

                }
            }

            rGdvListDetalle.DataSource = dtTmpDet;
            rGdvListDetalle.DataBind();

            Session["dtTmpDetLP"] = dtTmpDet;

        }
    }

    private void addRow()
    {
        try
        {

            DataTable dtTmpDet = new DataTable();
            dtTmpDet = (DataTable)Session["dtTmpDetLP"];

            int secToRow = 1;
            decimal valMax = 0, val = 0;
            DataRow row = dtTmpDet.NewRow();

            //Conseguir la Secuencia del Articulo

            foreach (DataRow rowSec in dtTmpDet.Rows)
            {
                if (rowSec["artCve"].ToString() == rCboArticulo.SelectedValue)
                {
                    secToRow++;
                    valMax = Convert.ToDecimal(rowSec["lisPreCanMax"]);
                }
            }
            valMax++;

            if (valMax < Convert.ToDecimal(rTxtMaximo.Text))
            {
                row["lisPreCanMin"] = valMax;
                row["lisPreCanMax"] = rTxtMaximo.Text;
            }
            else
            {
                row["lisPreCanMin"] = valMax;

                val = valMax + 1;

                row["lisPreCanMax"] = val;

            }



            row["lisPreSec"] = secToRow;
            row["artCve"] = rCboArticulo.SelectedValue;

            row["artDes"] = rCboArticulo.Text;

            //row["lisPreCanMin"] = rTxtMinimo.Text;
            //row["lisPreCanMax"] = rTxtMaximo.Text;
            row["lisPrecio"] = rTxtPartPrec.Text;


            if (rCboDescuentos.SelectedValue != "")
            {
                row["desDes"] = rCboDescuentos.Text;
                row["DesCve"] = rCboDescuentos.SelectedValue;
            }

            if (rCboImpuestos.SelectedValue != "")
            {
                row["impDes"] = rCboImpuestos.Text;
                row["impCve"] = rCboImpuestos.SelectedValue;
            }

            dtTmpDet.Rows.Add(row);
            //asdfghjkljhgfdsdfghjk
            DataView dv = dtTmpDet.DefaultView;
            dv.Sort = "artCve";
            DataTable sortedDT = dv.ToTable();
            //dtTmpDet.DefaultView.Sort = "artCve";

            rGdvListDetalle.DataSource = sortedDT;
            rGdvListDetalle.DataBind();

            Session["dtTmpDetLP"] = dtTmpDet;
            

        }
        catch (Exception ex)
        {
            ex.ToString();
            throw;
        }

    }

    //private void controlesAccionDet()
    //{

    //    ////===> CONTROLES GENERAL

    //    rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = true;
    //    rGdvListDetalle.AllowMultiRowSelection = true;
    //    rGdvListDetalle.MasterTableView.ClearSelectedItems();
    //    //frmNewPartida.Visible = false;

    //    rBtnNuevoP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificarP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminarP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    fldDetalle.Visible = true;
    //    fldDetalle.Attributes.Remove("hidden");



    //    rCboArticulo.Enabled = false;
    //    rTxtMinimo.Enabled = false;
    //    rTxtMaximo.Enabled = false;
    //    rTxtPartPrec.Enabled = false;
    //    rCboDescuentos.Enabled = false;
    //    rCboImpuestos.Enabled = false;

    //    if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevoP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        //frmNewPartida.Visible = true;

    //        rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = false;
    //        rGdvListDetalle.AllowMultiRowSelection = false;
    //        rGdvListDetalle.MasterTableView.ClearSelectedItems();

    //        fldDetalle.Visible = true;
    //        fldDetalle.Attributes.Remove("hidden");

    //        rCboArticulo.Enabled = true;
    //        rTxtMinimo.Enabled = true;
    //        rTxtMaximo.Enabled = true;
    //        rTxtPartPrec.Enabled = true;
    //        rCboDescuentos.Enabled = true;
    //        rCboImpuestos.Enabled = true;



    //    }
    //    else if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rBtnModificarP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        //frmNewPartida.Visible = true;

    //        rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdvListDetalle.AllowMultiRowSelection = false;
    //        rGdvListDetalle.MasterTableView.ClearSelectedItems();

    //        fldDetalle.Visible = true;
    //        fldDetalle.Attributes.Remove("hidden");

    //        //rCboArticulo.Enabled = true;
    //        ////rTxtMinimo.Enabled = true;
    //        ////rTxtMaximo.Enabled = true;
    //        //rTxtPartPrec.Enabled = true;
    //        //rCboDescuentos.Enabled = true;
    //        //rCboImpuestos.Enabled = true;

    //    }
    //    else if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminarP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        //frmNewPartida.Visible = true;

    //        rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdvListDetalle.AllowMultiRowSelection = true;
    //        rGdvListDetalle.MasterTableView.ClearSelectedItems();

    //        fldDetalle.Visible = true;
    //        fldDetalle.Attributes.Remove("hidden");

    //        rCboArticulo.Enabled = false;
    //        rTxtMinimo.Enabled = false;
    //        rTxtMaximo.Enabled = false;
    //        rCboDescuentos.Enabled = false;
    //        rTxtPartPrec.Enabled = false;
    //        rCboImpuestos.Enabled = false;


    //    }


    //    if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //            hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //            hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //           )
    //    {
    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }
    //    else if (hdfBtnAccionDet.Value == "" && pnlDetail.Visible)
    //    {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = true;
    //    }

    //    resetStyleControls();

    //}

    //private void controlesAccion()
    //{

    //    //rBtnArtLisPre.Visible = false;

    //    //===> CONTROLES GENERAL
    //    rTxtClave.Enabled = false;
    //    rTxtAbreviatura.Enabled = false;
    //    rTxtDescripcion.Enabled = false;
    //    RdDateFecha_Inicio.Enabled = false;
    //    RdDateFecha_Final.Enabled = false;
    //    rCboMoneda.Enabled = false;

    //    rGdvList.MasterTableView.ClearSelectedItems();
    //    rGdvList.ClientSettings.Selecting.AllowRowSelect = true;

    //    for (int i = 0; i < rGdvList.Items.Count; i++)
    //    {
    //        this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
    //    }

    //    rGdvList.AllowMultiRowSelection = false;

    //    rBtnGuardar.Enabled = false;
    //    rBtnCancelar.Enabled = false;

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {

    //        this.rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
    //        for (int i = 0; i < rGdvList.Items.Count; i++)
    //        {
    //            this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
    //        }
    //        for (int i = 0; i < rGdvList.Items.Count; i++)
    //        {
    //            this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.None;
    //        }

    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        rGdvList.MasterTableView.ClearSelectedItems();
    //        rGdvList.ClientSettings.Selecting.AllowRowSelect = false;
    //        rGdvList.AllowMultiRowSelection = false;
    //        rTxtClave.Enabled = true;
    //        rTxtAbreviatura.Enabled = true;
    //        rTxtDescripcion.Enabled = true;
    //        RdDateFecha_Inicio.Enabled = true;
    //        RdDateFecha_Final.Enabled = true;
    //        rCboMoneda.Enabled = true;

    //    }

    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {

    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";

    //        //rBtnArtLisPre.Visible = true;

    //        rGdvList.MasterTableView.ClearSelectedItems();
    //        rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdvList.AllowMultiRowSelection = false;
            
    //        for (int i = 0; i < rGdvList.Items.Count; i++)
    //        {
    //            this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
    //        }

    //    }

    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";

    //        rGdvList.MasterTableView.ClearSelectedItems();
    //        rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
    //        rGdvList.AllowMultiRowSelection = true;

    //        this.rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
    //        for (int i = 0; i < rGdvList.Items.Count; i++)
    //        {
    //            this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
    //        }
    //    }

    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //           )
    //    {
    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }

    //    resetStyleControls();

    //}

    private void showTab(string pOpc, string ppLisPreCve)
    {
        //Hacer invisible todo control de la pantalla

        int opc = Convert.ToInt32(pOpc);
        Int64 pLisPreCve = Convert.ToInt64(ppLisPreCve);


        pnlBtnsAcciones.Visible = false;
        fldEncabezado.Visible = false;
        rGdvList.Visible = false;
        fldEncabezado.Attributes["hidden"] = "hidden";


        fldDetalle.Visible = false;
        fldDetalle.Attributes["hidden"] = "hidden";
        pnlBtnsAccionesDetalle.Visible = false;
        rGdvListDetalle.Visible = false;

        if (opc == 1)
        {
            pnlBtnsAcciones.Visible = true;
            fldEncabezado.Visible = true;
            fldEncabezado.Attributes.Remove("hidden");
            rGdvList.Visible = true;

            pnlHead.Visible = true;
            pnlDetail.Visible = false;

        }
        else if (opc == 2)
        {

            fldDetalle.Visible = true;
            fldDetalle.Attributes.Remove("hidden");
            pnlBtnsAccionesDetalle.Visible = true;
            rGdvListDetalle.Visible = true;
            loadGridDetalle(pLisPreCve);
            
          
            pnlDetail.Visible = true;

        }

    }


    private void resetStyleControls()
    {

        rTxtClave.CssClass = "cssTxtEnabled";
        rTxtAbreviatura.CssClass = "cssTxtEnabled";
        rTxtDescripcion.CssClass = "cssTxtEnabled";
        rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
        RdDateFecha_Inicio.CssClass = "cssTxtEnabled";
        RdDateFecha_Inicio.BorderWidth = Unit.Pixel(1);
        RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Transparent;
        RdDateFecha_Final.CssClass = "cssTxtEnabled";
        RdDateFecha_Final.BorderWidth = Unit.Pixel(1);
        RdDateFecha_Final.BorderColor = System.Drawing.Color.Transparent;

        rCboArticulo.BorderColor = System.Drawing.Color.Transparent;
        rTxtMaximo.CssClass = "cssTxtEnabled";
        rTxtMinimo.CssClass = "cssTxtEnabled";
        rTxtPartPrec.CssClass = "cssTxtEnabled";

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

            if (rGdvList.SelectedItems.Count == 0 && hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rTxtClave.Text.Trim() == "")
            {
                rTxtClave.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtClave.CssClass = "cssTxtEnabled"; }

            if (rTxtAbreviatura.Text.Trim() == "")
            {
                rTxtAbreviatura.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtAbreviatura.CssClass = "cssTxtEnabled"; }

            if (rTxtDescripcion.Text.Trim() == "")
            {
                rTxtDescripcion.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtDescripcion.CssClass = "cssTxtEnabled"; }


            if (rCboMoneda.SelectedValue.Trim() == "")
            {
                rCboMoneda.CssClass = "cssTxtInvalid";
                rCboMoneda.BorderWidth = Unit.Pixel(1);
                rCboMoneda.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboMoneda.BorderColor = System.Drawing.Color.Transparent; }

            if (RdDateFecha_Inicio.SelectedDate.ToString() == "")
            {
                RdDateFecha_Inicio.CssClass = "cssTxtInvalid";
                RdDateFecha_Inicio.BorderWidth = Unit.Pixel(1);
                RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else
            {
                RdDateFecha_Inicio.CssClass = "cssTxtEnabled";
                RdDateFecha_Inicio.BorderWidth = Unit.Pixel(1);
                RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Transparent;
            }

            if (RdDateFecha_Final.SelectedDate.ToString() == "")
            {
                RdDateFecha_Final.CssClass = "cssTxtInvalid";
                RdDateFecha_Final.BorderWidth = Unit.Pixel(1);
                RdDateFecha_Final.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else
            {
                RdDateFecha_Final.CssClass = "cssTxtEnabled";
                RdDateFecha_Final.BorderWidth = Unit.Pixel(1);
                RdDateFecha_Final.BorderColor = System.Drawing.Color.Transparent;
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

            if (rGdvList.SelectedItems.Count == 0)
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

        //NUEVO
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (rGdvListDetalle.SelectedItems.Count == 0 && hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rCboArticulo.SelectedValue.Trim() == "")
            {
                rCboArticulo.CssClass = "cssTxtInvalid";
                rCboArticulo.BorderWidth = Unit.Pixel(1);
                rCboArticulo.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboArticulo.BorderColor = System.Drawing.Color.Transparent; }

            if (rTxtMaximo.Text.Trim() == "")
            {
                rTxtMaximo.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtMaximo.CssClass = "cssTxtEnabled"; }

            if (rTxtMinimo.Text.Trim() == "")
            {
                rTxtMinimo.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtMinimo.CssClass = "cssTxtEnabled"; }

            if (rTxtPartPrec.Text.Trim() == "")
            {
                rTxtPartPrec.CssClass = "cssTxtInvalid";
                camposInc += 1;
            }
            else { rTxtPartPrec.CssClass = "cssTxtEnabled"; }


            

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }

            return sResult;
        }


        //ELIMINAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            if (rGdvListDetalle.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            //Buscar si e sla ultima secuencia del articulo seleccionado
            //DataTable dtTmpDet = new DataTable();
            //dtTmpDet = (DataTable)Session["dtTmpDetLP"];
            //DataView dv = dtTmpDet.DefaultView;
            //dv.Sort = "artCve";
            //DataTable sortedDT = dv.ToTable();

            //int contSec = 0;

            //foreach (DataRow sortRow in sortedDT.Rows)
            //{

            //    if (sortRow["artCve"].ToString() == rGdvListDetalle.SelectedItems[0].Cells[3].Text)
            //    {
            //        contSec++;
            //    }

            //}

            //if (rGdvListDetalle.SelectedItems[0].Cells[2].Text != contSec.ToString())
            //{
            //    sMSGTip = "2";
            //    sResult = "La secuencia del Articulo tiene Dependencias";

            //    return sResult;

            //}



            return sResult;
        }

        return sResult;
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

    private bool PosIni_Fin()
    {
        decimal ini, fin;
        if (rTxtMinimo.Text != "")
        {
            ini = Convert.ToDecimal(rTxtMinimo.Text);
        }
        else
        {
            ini = 0;
        }

        if (rTxtMaximo.Text != "")
        {
            fin = Convert.ToDecimal(rTxtMaximo.Text);
        }
        else
        {
            fin = 999999999999999;
        }


        if (ini > fin)
        {
            return false;
        }
        else if (fin < ini)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    private string getUniMed(string pArtCve)
    {

        string response = "";

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Articulos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@artCve", DbType.String, 20, ParameterDirection.Input, pArtCve);

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    response = ds.Tables[0].Rows[0]["uniMedCve"].ToString();
                }

            }
        }

        return response;
        
    }

    #endregion


    //FUNCIONES GENERALES
    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";
        rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvList.AllowMultiRowSelection = true;


        for (int i = 0; i < rGdvList.Items.Count; i++)
        {
            this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
        }
        //for (int i = 0; i < rGdvList.Items.Count; i++)
        //{
        //    this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.None;
        //}

        //===> CONTROLES GENERAL  rGdvList
        this.rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


        rTxtClave.CssClass = "cssTxtEnabled";
        rTxtAbreviatura.CssClass = "cssTxtEnabled";
        rCboMoneda.BorderColor = System.Drawing.Color.Transparent; 
        rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
        RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Transparent;
        RdDateFecha_Final.BorderColor = System.Drawing.Color.Transparent;

        rTxtClave.Enabled = false;
        rTxtAbreviatura.Enabled = false;
        rTxtDescripcion.Enabled = false;
        RdDateFecha_Inicio.Enabled = false;
        RdDateFecha_Final.Enabled = false;
        rCboMoneda.Enabled = false;

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
            if (hdfBtnAccion.Value != "rBtnArtLisPre")
            {
                rTxtClave.Enabled = false;
                rTxtAbreviatura.Enabled = false;
                rTxtDescripcion.Enabled = false;
                RdDateFecha_Inicio.Enabled = false;
                RdDateFecha_Final.Enabled = false;
                rCboMoneda.Enabled = false;

                //rTxtClave.Text = "";
                rTxtAbreviatura.Text = "";
                rTxtDescripcion.Text = "";
                RdDateFecha_Inicio.Clear();
                RdDateFecha_Final.Clear();
                rCboMoneda.ClearSelection();
            }
        }
    }

    private void ControlesAccionEjecucion(bool Result)
    {

        rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvList.AllowMultiRowSelection = true;
        if (Result == true)
        {
            for (int i = 0; i < rGdvList.Items.Count; i++)
            {
                this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
            }

            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                this.rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
                for (int i = 0; i < rGdvList.Items.Count; i++)
                {
                    this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                }
                for (int i = 0; i < rGdvList.Items.Count; i++)
                {
                    this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.None;
                }
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdvList.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvList.MasterTableView.ClearSelectedItems();

                rTxtClave.Enabled = true;
                rTxtAbreviatura.Enabled = true;
                rTxtDescripcion.Enabled = true;
                RdDateFecha_Inicio.Enabled = true;
                RdDateFecha_Final.Enabled = true;
                rCboMoneda.Enabled = true;

                rTxtClave.Text = "";
                rTxtAbreviatura.Text = "";
                rTxtDescripcion.Text = "";
                RdDateFecha_Inicio.Clear();
                RdDateFecha_Final.Clear();
                rCboMoneda.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                for (int i = 0; i < rGdvList.Items.Count; i++)
                {
                    this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                }
                rGdvList.AllowMultiRowSelection = false;

                rTxtClave.Enabled = false;
                rTxtAbreviatura.Enabled = true;
                rTxtDescripcion.Enabled = true;
                RdDateFecha_Inicio.Enabled = true;
                RdDateFecha_Final.Enabled = true;
                rCboMoneda.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                
        
                for (int i = 0; i < rGdvList.Items.Count; i++)
                {
                    this.rGdvList.Items[i].SelectableMode = GridItemSelectableMode.ServerAndClientSide;
                }
                EjecutaAccion();
            }






            //ARTICULOS LISTA DE PRECIOS
            if (hdfBtnAccion.Value == "rBtnArtLisPre")
            {
                Int64 pLisCve = 0;
                var dataItem = rGdvList.SelectedItems[0] as GridDataItem;
                pLisCve = Convert.ToInt64(dataItem.GetDataKeyValue("lisPreCve").ToString());
               
                opcTabStc.Value = "2";
                cveTabStc.Value = pLisCve.ToString();
                radlb_ClaveLista.Text = rTxtClave.Text + " " + rTxtDescripcion.Text;
                rBtnGuardar.Enabled = false;
                rBtnCancelar.Enabled = true;
                resetStyleControls();
                
                rBtnNuevo.DataBind();
                if (rBtnNuevo.Visible == true)
                {
                    rBtnNuevoP.Visible = true;
                }
                else
                {
                    rBtnNuevoP.Visible = false;
                }

                rBtnModificar.DataBind();
                if (rBtnModificar.Visible == true)
                {
                    rBtnModificarP.Visible = true;
                }
                else
                {
                    rBtnModificarP.Visible = false;
                }

                rBtnEliminar.DataBind();
                if (rBtnEliminar.Visible == true)
                {
                    rBtnEliminarP.Visible = true;
                }
                else
                {
                    rBtnEliminarP.Visible = false;
                }
                showTab("2", pLisCve.ToString());
            }








            //DOCUMENTOS LISTA DE PRECIOS
            if (hdfBtnAccion.Value == "rBtnDocumentosListaPrecios")
            {
                if (FNGrales.bListPrecio(Pag_sConexionLog, Pag_sCompania)) {

                    Int64 Pag_sidM = 0;
                    if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                    {
                        Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                    }

                    var dataItem = rGdvList.SelectedItems[0] as GridDataItem;
                    string stransDetId = dataItem.GetDataKeyValue("lisPreCve").ToString();
                    Response.Redirect("~/DC/MttoDocumentoListaP.aspx?" + "lisPreCve=" + stransDetId + "&idM=" + Pag_sidM);
                }else {
                    string sResult = "", sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1023", ref sMSGTip, ref sResult);
                    ShowAlert(sMSGTip, sResult);
                }
            }

            //LISTA DE PRECIOS AGRUPACIONES
            if (hdfBtnAccion.Value == "rBtnListaP_Agrupaciones")
            {
                Int64 Pag_sidM = 0;
                if (Request.QueryString["idM"] != null && Request.QueryString["idM"] != "")
                {
                    Pag_sidM = Convert.ToInt64(Request.QueryString["idM"]);
                }
                var dataItem = rGdvList.SelectedItems[0] as GridDataItem;
                string stransDetId = dataItem.GetDataKeyValue("lisPreCve").ToString();
                Response.Redirect("~/DC/MttoListaPAgrupacion.aspx?" + "lisPreCve=" + stransDetId + "&idM=" + Pag_sidM);
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvList.AllowMultiRowSelection = true;
                rGdvList.MasterTableView.ClearSelectedItems();


                rTxtClave.Enabled = false;
                rTxtAbreviatura.Enabled = false;
                rTxtDescripcion.Enabled = false;
                RdDateFecha_Inicio.Enabled = false;
                RdDateFecha_Final.Enabled = false;
                rCboMoneda.Enabled = false;

                rTxtClave.Text = "";
                rTxtAbreviatura.Text = "";
                rTxtDescripcion.Text = "";
                RdDateFecha_Inicio.Clear();
                RdDateFecha_Final.Clear();
                rCboMoneda.ClearSelection();
            }
        }


        if (Result == false)
        {
            rTxtClave.Enabled = false;
            rTxtAbreviatura.Enabled = false;
            rTxtDescripcion.Enabled = false;
            RdDateFecha_Inicio.Enabled = false;
            RdDateFecha_Final.Enabled = false;
            rCboMoneda.Enabled = false;

            rTxtClave.Text = "";
            rTxtAbreviatura.Text = "";
            rTxtDescripcion.Text = "";
            RdDateFecha_Inicio.Clear();
            RdDateFecha_Final.Clear();
            rCboMoneda.ClearSelection();
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvList.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvList, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvList, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //ARTICULO L PRECIOS
        if (hdfBtnAccion.Value == "rBtnArtLisPre")
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvList, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //DOCUMENTOS LISTA DE PRECIOS
        if (hdfBtnAccion.Value == "rBtnDocumentosListaPrecios")
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvList, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        //AGRUPACIONES
        if (hdfBtnAccion.Value == "rBtnListaP_Agrupaciones")
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvList, GvVAS, ref sMSGTip, ref sResult) == false)
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
            rTxtClave.Text = "";
            rTxtAbreviatura.Text = "";
            rTxtDescripcion.Text = "";
            RdDateFecha_Inicio.Clear();
            RdDateFecha_Final.Clear();
            rCboMoneda.ClearSelection();
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvList.ClientSettings.Selecting.AllowRowSelect = true;

            rGdvList.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rTxtClave.CssClass = "cssTxtEnabled";
            rTxtAbreviatura.CssClass = "cssTxtEnabled";
            rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
            rCboMoneda.BorderColor = System.Drawing.Color.Transparent;
            RdDateFecha_Inicio.BorderColor = System.Drawing.Color.Transparent;
            RdDateFecha_Final.BorderColor = System.Drawing.Color.Transparent;

            rTxtClave.Enabled = false;
            rTxtAbreviatura.Enabled = false;
            rTxtDescripcion.Enabled = false;
            RdDateFecha_Inicio.Enabled = false;
            RdDateFecha_Final.Enabled = false;
            rCboMoneda.Enabled = false;

            rTxtClave.Text = "";
            rTxtAbreviatura.Text = "";
            rTxtDescripcion.Text = "";
            RdDateFecha_Inicio.Clear();
            RdDateFecha_Final.Clear();
            rCboMoneda.ClearSelection();
            hdfBtnAccion.Value = "";

            rGdvList.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvList.AllowMultiRowSelection = true;

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {

            }
            
    }

    
    //FUNCIONES DETALLE
    private void ControlesAccionP()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = true;

        rBtnNuevoP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificarP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminarP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";


        rCboArticulo.BorderColor = System.Drawing.Color.Transparent;
        rTxtMaximo.CssClass = "cssTxtEnabled";
        rTxtMinimo.CssClass = "cssTxtEnabled";
        rTxtPartPrec.CssClass = "cssTxtEnabled";

        rCboArticulo.Enabled = false;
        rTxtMinimo.Enabled = false;
        rTxtMaximo.Enabled = false;
        rTxtPartPrec.Enabled = false;
        rCboDescuentos.Enabled = false;
        rCboImpuestos.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = true;


        /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
        //Validacion
        msgValidacion = ValidaControlesAccion_SelectRowGridP(ref sMSGTip);
        if (msgValidacion == "")
        {
            ControlesAccionEjecucionP(true);
        }
        else
        {
            ControlesAccionEjecucionP(false);
            ShowAlert(sMSGTip, msgValidacion);
        }

        //INICIO / CANCELAR
        if (hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccionDet.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            rCboArticulo.Enabled = false;
            rTxtMinimo.Enabled = false;
            rTxtMaximo.Enabled = false;
            rTxtPartPrec.Enabled = false;
            rCboDescuentos.Enabled = false;
            rCboImpuestos.Enabled = false;

            rCboArticulo.ClearSelection();
            rTxtMinimo.Text = "";
            rTxtMaximo.Text = "";
            rTxtPartPrec.Text = "";
            rCboDescuentos.ClearSelection();
            rCboImpuestos.ClearSelection();
        }
    }

    private void ControlesAccionEjecucionP(bool Result)
    {
        rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvListDetalle.AllowMultiRowSelection = true;
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevoP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvListDetalle.MasterTableView.ClearSelectedItems();

                rCboArticulo.Enabled = true;
                rTxtMinimo.Enabled = true;
                rTxtMaximo.Enabled = true;
                rTxtPartPrec.Enabled = true;
                rCboDescuentos.Enabled = true;
                rCboImpuestos.Enabled = true;

                rCboArticulo.ClearSelection();
                rTxtMinimo.Text = "";
                rTxtMaximo.Text = "";
                rTxtPartPrec.Text = "";
                rCboDescuentos.ClearSelection();
                rCboImpuestos.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificarP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvListDetalle.AllowMultiRowSelection = false;

                rCboArticulo.Enabled = false;
                rTxtMinimo.Enabled = false;
                rTxtMaximo.Enabled = true;
                rTxtPartPrec.Enabled = true;
                rCboDescuentos.Enabled = true;
                rCboImpuestos.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;

                DataTable dtTmpDet = new DataTable();
                dtTmpDet = (DataTable)Session["dtTmpDetLP"];

                var dataItem = rGdvListDetalle.SelectedItems[0] as GridDataItem;
                if (dtTmpDet.Rows.Count > 0)
                {
                    object maxSec = 0;
                    // maxSec = Convert.ToInt32(dtTmpDet.Compute("max(lisPreSec)", ""));
                    maxSec = dtTmpDet.Compute("max(lisPreSec)", "artCve =  '" + dataItem.Cells[3].Text.Trim().ToString() + "'");

                    if (Convert.ToInt32(dataItem.Cells[2].Text) == Convert.ToInt32(maxSec))
                    {
                        rTxtMinimo.Enabled = false;
                        rTxtMaximo.Enabled = true; 
                        rTxtPartPrec.Enabled = true;
                        rCboDescuentos.Enabled = true;
                        rCboImpuestos.Enabled = true;
                    }
                    else
                    {
                        rTxtMinimo.Enabled = false;
                        rTxtMaximo.Enabled = false;
                        rTxtPartPrec.Enabled = true;
                        rCboDescuentos.Enabled = true;
                        rCboImpuestos.Enabled = true;
                    }
                }
            }

            //ELIMIAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                //EjecutaAccion();

                ejecutaAccionDetalle();
            }

            //LIMPIAR
            if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvListDetalle.AllowMultiRowSelection = true;
                rGdvListDetalle.MasterTableView.ClearSelectedItems();

                rCboArticulo.Enabled = false;
                rTxtMinimo.Enabled = false;
                rTxtMaximo.Enabled = false;
                rTxtPartPrec.Enabled = false;
                rCboDescuentos.Enabled = false;
                rCboImpuestos.Enabled = false;

                rCboArticulo.ClearSelection();
                rTxtMinimo.Text = "";
                rTxtMaximo.Text = "";
                rTxtPartPrec.Text = "";
                rCboDescuentos.ClearSelection();
                rCboImpuestos.ClearSelection();
            }
        }


        if (Result == false)
        {
            rCboArticulo.Enabled = false;
            rTxtMinimo.Enabled = false;
            rTxtMaximo.Enabled = false;
            rTxtPartPrec.Enabled = false;
            rCboDescuentos.Enabled = false;
            rCboImpuestos.Enabled = false;

            rCboArticulo.ClearSelection();
            rTxtMinimo.Text = "";
            rTxtMaximo.Text = "";
            rTxtPartPrec.Text = "";
            rCboDescuentos.ClearSelection();
            rCboImpuestos.ClearSelection();
            hdfBtnAccionDet.Value = "";
        }
        
    }

    private string ValidaControlesAccion_SelectRowGridP(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvListDetalle.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvListDetalle, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvListDetalle, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }
        return sResult;
    }

    private void EjecutaAccionLimpiarP()
    {
        //NUEVO
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rCboArticulo.ClearSelection();
            rTxtMinimo.Text = "";
            rTxtMaximo.Text = "";
            rTxtPartPrec.Text = "";
            rCboDescuentos.ClearSelection();
            rCboImpuestos.ClearSelection();
        }

        //MODIFICAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = true;

            rGdvListDetalle.MasterTableView.ClearSelectedItems();

            rBtnNuevoP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificarP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminarP.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rCboArticulo.BorderColor = System.Drawing.Color.Transparent;
            rTxtMaximo.CssClass = "cssTxtEnabled";
            rTxtMinimo.CssClass = "cssTxtEnabled";
            rTxtPartPrec.CssClass = "cssTxtEnabled";


            rCboArticulo.Enabled = false;
            rTxtMinimo.Enabled = false;
            rTxtMaximo.Enabled = false;
            rTxtPartPrec.Enabled = false;
            rCboDescuentos.Enabled = false; ;
            rCboImpuestos.Enabled = false;

            rCboArticulo.ClearSelection();
            rTxtMinimo.Text = "";
            rTxtMaximo.Text = "";
            rTxtPartPrec.Text = "";
            rCboDescuentos.ClearSelection();
            rCboImpuestos.ClearSelection();
            rGdvListDetalle.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvListDetalle.AllowMultiRowSelection = true;
            hdfBtnAccionDet.Value = "";

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = true;
        }

        //ELIMINAR
        if (hdfBtnAccionDet.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }
    }

    
    protected void rBtnImportar_Click(object sender, ImageButtonClickEventArgs e)
    {
        string script = "function f(){$find(\"" + RwExportar.ClientID + "\").show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

    }
    

    protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        loadGrid();
    }

    protected void rBtnExportar_Click(object sender, ImageButtonClickEventArgs e)
    {

        string alternateText = (sender as RadImageButton).Value;
        rGdvList.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), alternateText);
        rGdvList.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.Html;
        rGdvList.ExportSettings.IgnorePaging = true;
        rGdvList.ExportSettings.ExportOnlyData = true;
        rGdvList.ExportSettings.OpenInNewWindow = true;

       

        if (rGdvList.Visible == true)
        {
            rGdvList.MasterTableView.ExportToExcel();
        } else if (rGdvListDetalle.Visible == true) {
            rGdvListDetalle.MasterTableView.ExportToExcel();
        }

        
       
    }

    

    protected void rGdvList_ExcelMLWorkBookCreated(object sender, GridExcelMLWorkBookCreatedEventArgs e)
    {

    }



    protected void rGdvList_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridCommandItem)
        {
            ImageButton ibExportToExcel = (e.Item as GridCommandItem).FindControl("bXls") as ImageButton;
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(ibExportToExcel);
        }
    }



    protected void rGdvList_HTMLExporting(object sender, GridHTMLExportingEventArgs e)
    {
        e.Styles.Append("@page table .lisPreCve { background-color: #d3d3d3; }");
       // e.Styles.Append("body { border:solid 0.1pt #CCCCCC; }");
    }



    protected void rGdvList_BiffExporting(object sender, GridBiffExportingEventArgs e)
    {

    }


}