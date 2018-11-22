
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

public partial class DC_MttoMapeoDocumentos : System.Web.UI.Page
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
    private string folio_Selection;
    //private string conConfSec;
    private string listTipDatoCptoCve;



    #endregion

    #region EVENTOS

    protected void Page_Load(object sender, EventArgs e)
    {

        if (SM.IsActiveSession(this.Page))
        {
            //Recuperar Valores de Sesion
            Valores_InicioPag();
            if (!IsPostBack)
            {
                //Iniciar Formulario
                InicioPagina();

            }
        }

    }

    protected void rCboTipoDato_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FnCtlsFillIn.RabComboBox_ValorDic(Pag_sConexionLog, Pag_sCompania, rCboTipoDato.SelectedValue.ToString(), Convert.ToInt32(folio_Selection), ref rCboValorDic, true, true);
        rCboValorDic.ClearSelection();
        rCboValorDic.Enabled = true;

        LlenaComboSecuenc();
        rCboValorSecuenc.Enabled = true;
    }

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION

    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString();
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
        //rBtnModificar.Enabled = true;
        //rBtnLimpiar.Enabled = true;
        ControlesAccion();

    }
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
        ControlesAccion();
    }

    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
    {
        //ControlesAccion();
        //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
        //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString()

        //   )
        //{
        //    LimpiarUi();
        //}
        //else
        //{

        //}
        EjecutaAccionLimpiar();
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();

    }
    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();

    }

    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
        this.rCboTipoDato.ClearSelection();
        this.rCboValorDic.ClearSelection();
        rGdvInformacion.MasterTableView.ClearSelectedItems();
        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
        this.rGdvInformacion.AllowMultiRowSelection = false;
        this.rCboTipoDato.Enabled = false;
        this.rCboValorDic.Enabled = false;
        this.rBtnCancelar.Enabled = false;
        this.rBtnGuardar.Enabled = false;
        //this.RadTxtSecuenc.Enabled = false;
        //this.RadTxtSecuenc.Text = "";
        rCboValorSecuenc.Enabled = false;
        rCboValorSecuenc.ClearSelection();
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvInformacion.AllowMultiRowSelection = true;
        

    }

    protected void rGdvInformacion_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        var dataItem = rGdvInformacion.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            string skey = dataItem.GetDataKeyValue("cptoId").ToString();
            //if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())

                //this.rCboValorDic.Enabled = true;
                //this.rCboTipoDato.Enabled = false;
                //this.RadTxtSecuenc.Enabled = false;

               rCboTipoDato.SelectedValue = dataItem["listTipDatoCptoCve"].Text.ToString();
            FnCtlsFillIn.RabComboBox_ValorDic(Pag_sConexionLog, Pag_sCompania, dataItem["listTipDatoCptoCve"].Text, Convert.ToInt32(folio_Selection), ref rCboValorDic, true, true);

            rCboValorDic.SelectedValue = dataItem["transDicCve"].Text;
            LlenaComboSecuenc();
            //this.RadTxtSecuenc.Text = dataItem["listTipDatoCptoSec"].Text;
            rCboValorSecuenc.SelectedValue = dataItem["listTipDatoCptoSec"].Text;

        }

         if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            //this.rCboValorDic.Enabled = false;
            //this.rCboTipoDato.Enabled = false;
            //this.rCboTipoDato.ClearSelection();
            //this.rCboValorDic.ClearSelection();
            //this.rCboTipoDato.Text="";
            //this.rCboValorDic.Text="";
            //this.RadTxtSecuenc.Enabled = false;
            //this.RadTxtSecuenc.Text = "";
            
        }
    }





    #endregion

    #region METODOS

    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);


        folio_Selection = Convert.ToString(Session["folio_Selection"]);

        //conConfSec = Convert.ToString(Session["conConfSec"]);

        listTipDatoCptoCve = Convert.ToString(Session["listTipDatoCptoCve"]);


    }

    private void InicioPagina()
    {
        ControlesAccion();
        if (folio_Selection.ToString() != "")
        {
            llenaDatCpto();
            LlenaComboTiposDato();
            llenadata_Grid();

            hdfBtnAccion.Value = "";

            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.AllowMultiRowSelection = true;
        }
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


    private void llenaDatCpto()
    {


        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 54);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);

        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        if (ds != null)
        {
            rLblCptoId.Text = folio_Selection;
            rLblcptoDes.Text = Convert.ToString(ds.Tables[0].Rows[0]["cptoDes"]);
        }
        else
        {

        }
    }


    private void disEnableUi(int opc)
    {
        //1 = New - A 
        if (opc == 1)
        {
        
        }
        else if (opc == 2) // Modificar Cliente
        {
            this.rCboValorDic.Enabled = true;
        }
        if (opc == 3)
        {
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }
    }

    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rCboTipoDato.BorderColor = System.Drawing.Color.Transparent;
        //RadTxtSecuenc.CssClass = "cssTxtEnabled";
        rCboValorDic.BorderColor = System.Drawing.Color.Transparent;
        rCboValorSecuenc.BorderColor = System.Drawing.Color.Transparent;

        rCboTipoDato.Enabled = false;
        //RadTxtSecuenc.Enabled = false;
        rCboValorSecuenc.Enabled = false;
        rCboValorDic.Enabled = false;

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
            rCboTipoDato.Enabled = false;
            //RadTxtSecuenc.Enabled = false;
            rCboValorSecuenc.Enabled = false;
            rCboValorDic.Enabled = false;

            rCboTipoDato.ClearSelection();
            //RadTxtSecuenc.Text = "";
            rCboValorSecuenc.ClearSelection();
            rCboValorDic.ClearSelection();
        }
    }

    //private void ControlesAccion()
    //{

    //    rCboTipoDato.BorderColor = System.Drawing.Color.Transparent;
    //    rCboValorDic.BorderColor = System.Drawing.Color.Transparent;
    //    RadTxtSecuenc.CssClass = "cssTxtEnabled";
    //    this.rCboTipoDato.ClearSelection();
    //    this.rCboValorDic.ClearSelection();
    //    RadTxtSecuenc.Text = "";

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
    //    //===> CONTROLES POR ACCION
    //    // LIMPIAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
    //    {
    //        LimpiarUi();
    //    }

    //    //NUEVO
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
    //        rGdvInformacion.MasterTableView.ClearSelectedItems();
    //        this.rCboValorDic.Enabled = true;
    //        this.rCboTipoDato.Enabled = true;
    //        this.RadTxtSecuenc.Enabled = true;
    //        this.rCboTipoDato.ClearSelection();
    //        this.rCboValorDic.ClearSelection();
    //        this.RadTxtSecuenc.Text = "";
    //        this.rCboTipoDato.Text = "";
    //        this.rCboValorDic.Text = "";
    //        RadTxtSecuenc.CssClass = "cssTxtEnabled";

    //        //LimpiarUi();
    //    }

    //    //MODIFICAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {

    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
    //        rCboTipoDato.BorderColor = System.Drawing.Color.Transparent;
    //        rCboValorDic.BorderColor = System.Drawing.Color.Transparent;
    //        rGdvInformacion.MasterTableView.ClearSelectedItems();
    //        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
    //        this.rGdvInformacion.AllowMultiRowSelection = false;
    //        this.rCboTipoDato.Enabled = false;
    //        this.RadTxtSecuenc.Enabled = false;
    //        this.rCboValorDic.Enabled = false;;
    //        this.rCboTipoDato.ClearSelection();
    //        this.rCboValorDic.ClearSelection();
    //        this.rCboTipoDato.Text = "";
    //        this.rCboValorDic.Text = "";
    //        this.RadTxtSecuenc.Text = "";
    //        RadTxtSecuenc.CssClass = "cssTxtEnabled";
    //    }


    //    //ELIMINAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        rCboTipoDato.BorderColor = System.Drawing.Color.Transparent;
    //        rCboValorDic.BorderColor = System.Drawing.Color.Transparent;
    //        rGdvInformacion.MasterTableView.ClearSelectedItems();
    //        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
    //        this.rGdvInformacion.AllowMultiRowSelection = true;
    //        this.rCboValorDic.Enabled = false;
    //        this.rCboTipoDato.Enabled = false;
    //        this.rCboTipoDato.ClearSelection();
    //        this.rCboValorDic.ClearSelection();
    //        this.rCboTipoDato.Text = "";
    //        this.rCboValorDic.Text = "";
    //        this.RadTxtSecuenc.Enabled = false;
    //        this.RadTxtSecuenc.Text = "";
    //        RadTxtSecuenc.CssClass = "cssTxtEnabled";
    //    }

    //    //===> Botones GUARDAR - CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
    //          )
    //    {
    //        this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
    //        this.rCboValorDic.Enabled = false;
    //        this.rCboTipoDato.Enabled = false;
    //        this.RadTxtSecuenc.Enabled = false;
    //    }
    //    //===> Botones GUARDAR - CANCELAR
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
    //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
    //    hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()
    //   )
    //    {
    //        rBtnGuardar.Enabled = true;
    //        rBtnCancelar.Enabled = true;
    //    }
    //    else
    //    {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }


    //}

    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
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
                llenadata_Grid();
                this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
                rBtnCancelar.Enabled = false;
                rBtnGuardar.Enabled = false;
                this.rCboValorDic.Enabled = false;
                this.rCboValorDic.Enabled = false;
                //this.RadTxtSecuenc.Enabled = false;
                //this.RadTxtSecuenc.Text = "";

                rCboValorSecuenc.Enabled = false;
                rCboValorSecuenc.ClearSelection();

                rCboTipoDato.Enabled = false;
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                this.rCboTipoDato.ClearSelection();
                this.rCboValorDic.ClearSelection();
                rGdvInformacion.MasterTableView.ClearSelectedItems();
                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = true;


            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
                hdfBtnAccion.Value = "";
                InicioPagina();


                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = true;
                llenadata_Grid();
                rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
                rBtnCancelar.Enabled = false;
                rBtnGuardar.Enabled = false;
                this.rCboValorDic.Enabled = false;
                this.rCboValorDic.Enabled = false;
                rCboTipoDato.Enabled = false;
                //this.RadTxtSecuenc.Enabled = false;
                rCboValorSecuenc.Enabled = false;
                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = true;
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

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_MapeoDocumentos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(folio_Selection));
            ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, Convert.ToString(rCboTipoDato.SelectedValue));
            ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoSec", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(rCboValorSecuenc.SelectedValue));
            ProcBD.AgregarParametrosProcedimiento("@transDicCve", DbType.String, 30, ParameterDirection.Input,Convert.ToString(rCboValorDic.SelectedValue));

            ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {

                string sEjecEstatus, sEjecMSG = "";

                if (ds.Tables.Count == 1)
                {
                    sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                }
                else
                {
                    sEjecEstatus = ds.Tables[1].Rows[0]["maMSGTip"].ToString();
                    sEjecMSG = ds.Tables[1].Rows[0]["maMSGDes"].ToString();
                }

                if (sEjecEstatus == "1")
                {
                    hdfBtnAccion.Value = "";
                }





                ShowAlert(sEjecEstatus, sEjecMSG);
            }


        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }



    private void EjecutaSpAccionEliminar()
    {
        //int rBtnValCaptur = 0;
        //int rBtnJust = 0;
        //int rBtnChkDesc = 0;

        //try
        //{
        // int conConfSec; 
        int CountItems = 0;
        int CantItemsElimTrue = 0;
        int CantItemsElimFalse = 0;
        string EstatusItemsElim = "";
        string MsgItemsElim = "";
        string MsgItemsElimTrue = "";
        string MsgItemsElimFalse = "";

        foreach (GridDataItem i in rGdvInformacion.SelectedItems)
        {
            var dataItem = rGdvInformacion.SelectedItems[CountItems] as GridDataItem;
            //var dataItem = rGdvInformacion.SelectedItems[CountItems] as GridDataItem;
            if (dataItem != null)
            {

                //rCboTipoDato.SelectedValue = dataItem["listTipDatoCptoCve"].Text;
                //rCboValorDic.SelectedValue = dataItem["transDicCve"].Text;
                //this.RadTxtSecuenc.Text = dataItem["listTipDatoCptoSec"].Text;
                folio_Selection = dataItem["cptoId"].Text;
                string listTipDatoCptoCve = dataItem["listTipDatoCptoCve"].Text;
                string listTipDatoCptoSec = dataItem["listTipDatoCptoSec"].Text;

                try
                {
                    DataSet ds = new DataSet();
                    MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                    ProcBD.NombreProcedimiento = "sp_MapeoDocumentos";
                    ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                    ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                    ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int32, 0, ParameterDirection.Input, folio_Selection);
                    ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, listTipDatoCptoCve);
                    ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoSec", DbType.Int32, 0, ParameterDirection.Input, Convert.ToInt32(listTipDatoCptoSec));

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
                //InicioPagina();
            }
            else
            {
                //LlenaGridAlmacenes();
                //InicioPagina();
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
                //InicioPagina();
            }
            else
            {
                //LlenaGridAlmacenes();
                //InicioPagina();
            }

        }

    }





    private void LimpiarUi()
    {

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            //rTxtAgente.Text = "";
            this.rCboTipoDato.ClearSelection();
            this.rCboValorDic.ClearSelection();
            rGdvInformacion.MasterTableView.ClearSelectedItems();
            //this.RadTxtSecuenc.Text = "";
            rCboValorSecuenc.ClearSelection();

        }
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            //LlenarDatosEDITAR();
            this.rCboTipoDato.Enabled = false;
            this.rCboValorDic.Enabled = false;
            rGdvInformacion.MasterTableView.ClearSelectedItems();
            this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            this.rGdvInformacion.AllowMultiRowSelection = false;
            //this.RadTxtSecuenc.Enabled = false;
            //this.RadTxtSecuenc.Text = "";
            this.rCboValorSecuenc.Enabled = false;
            this.rCboValorSecuenc.Text = "";
            this.rCboTipoDato.ClearSelection();
            this.rCboValorDic.ClearSelection();
            rGdvInformacion.MasterTableView.ClearSelectedItems();
        }

    }

    private void LlenaComboTiposDato()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboTipoDato, ds, "listTipDatoCptoCve", "listTipDatoCptoDes", true, false);
        ((Literal) rCboTipoDato.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboTipoDato.Items.Count);

        //FnCtlsFillIn.RadComboBox_TiposDatos(Pag_sConexionLog, listTipoDatoCve, ref rCboTipoDato, true, true, "");
   
    }

    private void LlenaComboSecuenc()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ConceptoConfiguracion";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 62);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);
        ProcBD.AgregarParametrosProcedimiento("@listTipDatoCptoCve", DbType.String, 10, ParameterDirection.Input, rCboTipoDato.SelectedValue.Trim().ToString());
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboValorSecuenc, ds, "cptoConfSec", "cptoConfDes", true, false);
        ((Literal)rCboValorSecuenc.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboValorSecuenc.Items.Count);


    }
    public void llenadata_Grid()
    {
        //string cptoConfTipCap = Convert.ToString(ds.Tables[0].Rows[0]["cptoConfTipCap"]).Text;
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_MapeoDocumentos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cptoId", DbType.Int64, 0, ParameterDirection.Input, folio_Selection);

        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        FnCtlsFillIn.RadGrid(ref rGdvInformacion, ds);

    }

    #endregion

    #region FUNCIONES

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";


        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            //if (RadTxtSecuenc.Text.Trim() == "")
            //{
            //    RadTxtSecuenc.CssClass = "cssTxtInvalid";
            //    RadTxtSecuenc.DataBind();
            //    RadTxtSecuenc.Focus();
            //    camposInc += 1;
            //}
            //else { RadTxtSecuenc.CssClass = "cssTxtEnabled"; }
            if (rCboValorSecuenc.SelectedIndex == -1)
            {
                rCboValorSecuenc.BorderWidth = Unit.Pixel(1);
                rCboValorSecuenc.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboValorSecuenc.BorderColor = System.Drawing.Color.Transparent; }



            if (rCboTipoDato.SelectedIndex == -1)
            {
                rCboTipoDato.BorderWidth = Unit.Pixel(1);
                rCboTipoDato.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboTipoDato.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboValorDic.SelectedIndex == -1)
            {
                rCboValorDic.BorderWidth = Unit.Pixel(1);
                rCboValorDic.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboValorDic.BorderColor = System.Drawing.Color.Transparent; }


            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }

            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (rGdvInformacion.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (rCboValorSecuenc.SelectedIndex == -1)
            {
                rCboValorSecuenc.BorderWidth = Unit.Pixel(1);
                rCboValorSecuenc.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboValorSecuenc.BorderColor = System.Drawing.Color.Transparent; }



            //if (RadTxtSecuenc.Text.Trim() == "")
            //{
            //    RadTxtSecuenc.CssClass = "cssTxtInvalid";
            //    RadTxtSecuenc.DataBind();
            //    RadTxtSecuenc.Focus();
            //    camposInc += 1;
            //}
            //else { RadTxtSecuenc.CssClass = "cssTxtEnabled"; }


            if (rCboTipoDato.SelectedIndex == -1)
            {
                rCboTipoDato.BorderWidth = Unit.Pixel(1);
                rCboTipoDato.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboTipoDato.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboValorDic.SelectedIndex == -1)
            {
                rCboValorDic.BorderWidth = Unit.Pixel(1);
                rCboValorDic.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboValorDic.BorderColor = System.Drawing.Color.Transparent; }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

            if (rGdvInformacion.SelectedItems.Count == 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                return sResult;
            }

            if (camposInc > 0)
            {
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }
        return sResult;
    }


    private DataSet llenadatalistVarRef(Int32 revaTip)
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Varia_Ref";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int32, (1), ParameterDirection.Input, 55);
        //ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        //ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 20, ParameterDirection.Input, PagLoc_CliCve);
        //ProcBD.AgregarParametrosProcedimiento("@revaCve", DbType.String, (5), ParameterDirection.Input, "AGENT");
        //ProcBD.AgregarParametrosProcedimiento("@revaTip", DbType.Int32, (5), ParameterDirection.Input, revaTip);

        //ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        return ds;

        //dt_referencias.DataSource = ds;
        //dt_referencias.DataBind();

    }

    #endregion

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvInformacion.MasterTableView.ClearSelectedItems();

                rCboTipoDato.Enabled = true;
                //rCboValorSecuenc.Enabled = true;
                rCboValorDic.Enabled = false;

                rCboTipoDato.ClearSelection();
                rCboValorSecuenc.ClearSelection();
                rCboValorDic.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                rGdvInformacion.AllowMultiRowSelection = false;

                rCboTipoDato.Enabled = true;
                rCboValorSecuenc.Enabled = true;
                rCboValorDic.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvInformacion.AllowMultiRowSelection = true;
                rGdvInformacion.MasterTableView.ClearSelectedItems();

                rCboTipoDato.Enabled = false;
                rCboValorSecuenc.Enabled = false;
                rCboValorDic.Enabled = false;

                rCboTipoDato.ClearSelection();
                rCboValorSecuenc.ClearSelection();
                rCboValorDic.ClearSelection();
            }
        }


        if (Result == false)
        {
            rCboTipoDato.Enabled = false;
            rCboValorSecuenc.Enabled = false;
            rCboValorDic.Enabled = false;

            rCboTipoDato.ClearSelection();
            rCboValorSecuenc.ClearSelection();
            rCboValorDic.ClearSelection();
            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.AllowMultiRowSelection = true;
            hdfBtnAccion.Value = "";
        }


    }

    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        int GvSelectItem = rGdvInformacion.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvInformacion, GvVAS, ref sMSGTip, ref sResult) == false)
            {
                return sResult;
            }
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvInformacion, GvVAS, ref sMSGTip, ref sResult) == false)
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
            rCboTipoDato.ClearSelection();
            rCboValorSecuenc.ClearSelection();
            rCboValorDic.ClearSelection();
            rCboValorSecuenc.Enabled = false;
            rCboValorDic.Enabled = false;

        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.MasterTableView.ClearSelectedItems();

            rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
            rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
            rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

            rCboTipoDato.BorderColor = System.Drawing.Color.Transparent;
            rCboValorSecuenc.BorderColor = System.Drawing.Color.Transparent;
            rCboValorDic.BorderColor = System.Drawing.Color.Transparent;

            rCboTipoDato.Enabled = false;
            rCboValorSecuenc.Enabled = false;
            rCboValorDic.Enabled = false;

            rCboTipoDato.ClearSelection();
            rCboValorSecuenc.ClearSelection();
            rCboValorDic.ClearSelection();

            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
            rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvInformacion.AllowMultiRowSelection = true;
            hdfBtnAccion.Value = "";
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }



    }



}