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


public partial class DC_MttoAgrupaciones : System.Web.UI.Page
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
    #endregion

    #region EVENTOS
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SM.IsActiveSession(this.Page))
        {
            Valores_InicioPag();
            if (!IsPostBack)
            {
                hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString();
                InicioPagina();
                LlenaComboTiposAgrupacion();
            }
        }
    }

    //=====> EVENTOS CONTROLES
    protected void rGdv_Agrupaciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
        var dataItem = rGdv_Agrupaciones.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            rTxtAgrupacionCve.Text = dataItem["agrCve"].Text;
            rTxtAgrupacionDes.Text = dataItem["agrDes"].Text;
            hdfAccAgr.Value = "1";

            DataSet ds = new DataSet();
            ds = dsAgrupacionesDato();
            FnCtlsFillIn.RadGrid(ref this.rGdv_AgrupacionesDato, ds);
            rGdv_AgrupacionesDato.ClientSettings.Selecting.AllowRowSelect = true;
            rGdv_AgrupacionesDato.AllowMultiRowSelection = true;
            
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                hdfAccAgr.Value = "2";
                rGdv_AgrupacionesDato.ClientSettings.Selecting.AllowRowSelect = false;
                rGdv_AgrupacionesDato.AllowMultiRowSelection = false ;
                rGdv_AgrupacionesDato.MasterTableView.ClearSelectedItems();

                rTxtAgrupacionCve.Enabled = false;
                rTxtAgrupacionDes.Enabled = false;
                rtxtAgrupacionDatoCve.Enabled = true;
                rtxtAgrupacionDatoDes.Enabled = true;
                rtxtAgrupacionDatoCve.Text = "";
                rtxtAgrupacionDatoDes.Text = "";
            }


            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rGdv_AgrupacionesDato.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_AgrupacionesDato.AllowMultiRowSelection = false;
                rGdv_AgrupacionesDato.MasterTableView.ClearSelectedItems();

                rTxtAgrupacionCve.Enabled = false;
                rTxtAgrupacionDes.Enabled = true;

                rtxtAgrupacionDatoCve.Text = "";
                rtxtAgrupacionDatoDes.Text = "";
                rtxtAgrupacionDatoCve.Enabled = false;
                rtxtAgrupacionDatoDes.Enabled = false;
            }


        }
        else
        {
            hdfAccAgr.Value = "";
        }

    }
    protected void rGdv_AgrupacionesDato_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rGdv_Agrupaciones.SelectedItems.Count > 1)
        {
            ShowAlert("2", "Para seleccionar los datos de agrupacion debe tener seleccionada solo una agrupación.");
            var dataItem = rGdv_AgrupacionesDato.SelectedItems[0] as GridDataItem;
            dataItem.Selected = false;
        }
        else {
            var dataItem = rGdv_AgrupacionesDato.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                hdfAccAgr.Value = "2";

                //MODIFICAR
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    rTxtAgrupacionCve.Enabled = false;
                    rTxtAgrupacionDes.Enabled = false;

                    rtxtAgrupacionDatoCve.Text = dataItem["agrDatoCve"].Text;
                    rtxtAgrupacionDatoDes.Text = dataItem["agrDatoDes"].Text;
                    rtxtAgrupacionDatoCve.Enabled = false;
                    rtxtAgrupacionDatoDes.Enabled = true;
                }
            }
        }
    }
    protected void rCboTiposAgrupacion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        DataSet ds = new DataSet();

        ds = dsAgrupaciones();
        FnCtlsFillIn.RadGrid(ref rGdv_Agrupaciones, ds);

        InicioPagina();
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
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)///-----------------------------------------------------
    {
        EjecutaAccionLimpiar();
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

    #endregion


    #region METODOS
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
    }
    void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        hdfAccAgr.Value = "";
        ControlesAccion();

        rTxtAgrupacionCve.Text = "";
        rTxtAgrupacionDes.Text = "";
        rtxtAgrupacionDatoCve.Text = "";
        rtxtAgrupacionDatoDes.Text = "";

        rTxtAgrupacionCve.Enabled = false;
        rTxtAgrupacionDes.Enabled = false;
        rGdv_Agrupaciones.MasterTableView.ClearSelectedItems();
        rGdv_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Agrupaciones.AllowMultiRowSelection = true;

        rtxtAgrupacionDatoCve.Enabled = false;
        rtxtAgrupacionDatoDes.Enabled = false;
        rGdv_AgrupacionesDato.MasterTableView.ClearSelectedItems();
        rGdv_AgrupacionesDato.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_AgrupacionesDato.AllowMultiRowSelection = true;
        rGdv_AgrupacionesDato.DataSource = null;
        rGdv_AgrupacionesDato.Rebind();
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
    private void ControlesAccion() 
    {
        string sMSGTip = "";
        string msgValidacion = "";


        //===> CONTROLES GENERAL
        ControlesAccionGral();

        if (hdfBtnAccion.Value != "") {
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
    }

    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                hdfAccAgr.Value = "1";

                rGdv_Agrupaciones.MasterTableView.ClearSelectedItems();
                rGdv_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_Agrupaciones.AllowMultiRowSelection = false;

                rGdv_AgrupacionesDato.MasterTableView.ClearSelectedItems();
                rGdv_AgrupacionesDato.ClientSettings.Selecting.AllowRowSelect =false;
                rGdv_AgrupacionesDato.AllowMultiRowSelection = false;


                rTxtAgrupacionCve.Text = "";
                rTxtAgrupacionDes.Text = "";
                rtxtAgrupacionDatoCve.Text = "";
                rtxtAgrupacionDatoDes.Text = "";
                rTxtAgrupacionCve.Enabled = true;
                rTxtAgrupacionDes.Enabled = true;
                rtxtAgrupacionDatoCve.Enabled = false;
                rtxtAgrupacionDatoDes.Enabled = false;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }


            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                hdfAccAgr.Value = "1";
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";

                rGdv_Agrupaciones .AllowMultiRowSelection = false;
                var dataItem = rGdv_Agrupaciones.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    rTxtAgrupacionCve.Text = dataItem["agrCve"].Text;
                    rTxtAgrupacionDes.Text = dataItem["agrDes"].Text;
                }


                rGdv_AgrupacionesDato.ClientSettings.Selecting.AllowRowSelect = true;
                rGdv_AgrupacionesDato.AllowMultiRowSelection = false;

                if (rGdv_AgrupacionesDato.SelectedItems.Count > 0) {

                    hdfAccAgr.Value = "2";
                    var dataItem2 = rGdv_AgrupacionesDato.SelectedItems[0] as GridDataItem;
                    if (dataItem2 != null)
                    {
                        rtxtAgrupacionDatoCve.Text = dataItem2["agrDatoCve"].Text;
                        rtxtAgrupacionDatoDes.Text = dataItem2["agrDatoDes"].Text;
                    }
                }



                if (hdfAccAgr.Value == "1") {

                    rTxtAgrupacionCve.Enabled = false;
                    rTxtAgrupacionDes.Enabled = true;

                    rtxtAgrupacionDatoCve.Enabled = false;
                    rtxtAgrupacionDatoDes.Enabled = false;

                }
                if (hdfAccAgr.Value == "2")
                {

                    rTxtAgrupacionCve.Enabled = false;
                    rTxtAgrupacionDes.Enabled = false;

                    rtxtAgrupacionDatoCve.Enabled = false;
                    rtxtAgrupacionDatoDes.Enabled = true;

                }





                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;


            }




            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                hdfAccAgr.Value = "1";
                if (rGdv_AgrupacionesDato.SelectedItems.Count > 0)
                {
                    hdfAccAgr.Value = "2";
                }

                var dataItem = rGdv_Agrupaciones.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    EjecutaAccion();
                }
            }

        }


        if (Result == false)
        {
            //InicioPagina();
            hdfBtnAccion.Value = "";
        }


    }


    private void EjecutaAccion()
    {
        string sMSGTip = "";
        string msgValidacion = validaEjecutaAccion(ref sMSGTip);
        if (msgValidacion == "")
        {

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString()||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                EjecutaSpAcciones();
            }

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionesEliminar();
            }

        } 
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }
    private void EjecutaAccionLimpiar()
    {
        InicioPagina();

    }
    private void EjecutaSpAcciones()
    {
        try
        {
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString()||
                hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                if (hdfAccAgr.Value == "1")
                {
                    EjecutaSp_Agr();
                }
                if (hdfAccAgr.Value == "2")
                { 
                    EjecutaSp_AgrDato();
                }
            }
        }
        catch (Exception ex)
        { 
            string MsgError = ex.Message.Trim();
        }
    }
    private void EjecutaSp_Agr() {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Agrupaciones";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 15, ParameterDirection.Input, rCboTiposAgrupacion.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 15, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 15, ParameterDirection.Input, rTxtAgrupacionCve.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@agrDes", DbType.String, 200, ParameterDirection.Input, rTxtAgrupacionDes.Text.Trim());
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);

                if (sEjecEstatus == "1")
                {
                    rTxtAgrupacionCve.Text = "";
                    rTxtAgrupacionDes.Text = "";
                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                    {
                        rTxtAgrupacionCve.Enabled = true;
                        rTxtAgrupacionDes.Enabled = true;
                    }

                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                    {
                        this.rTxtAgrupacionCve.Text = "";
                        this.rTxtAgrupacionDes.Text = "";
                        this.rTxtAgrupacionCve.Enabled = false;
                        this.rTxtAgrupacionDes.Enabled = false;
                        hdfAccAgr.Value = "";
                        hdfBtnAccion.Value = "";

                        rGdv_Agrupaciones.MasterTableView.ClearSelectedItems();
                        rGdv_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
                        rGdv_Agrupaciones.AllowMultiRowSelection = true;

                        rGdv_AgrupacionesDato.MasterTableView.ClearSelectedItems();
                        rGdv_AgrupacionesDato.ClientSettings.Selecting.AllowRowSelect = true;
                        rGdv_AgrupacionesDato.AllowMultiRowSelection = true;

                        rTxtAgrupacionCve.CssClass = "cssTxtEnabled";
                        rTxtAgrupacionDes.CssClass = "cssTxtEnabled";
                        rtxtAgrupacionDatoCve.CssClass = "cssTxtEnabled";
                        rtxtAgrupacionDatoDes.CssClass = "cssTxtEnabled";

                        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                        rBtnGuardar.Enabled = false;
                        rBtnCancelar.Enabled = false;
                    }

                    ds = dsAgrupaciones();
                    FnCtlsFillIn.RadGrid(ref rGdv_Agrupaciones, ds);

                    rtxtAgrupacionDatoCve.Text = "";
                    rtxtAgrupacionDatoDes.Text = "";
                    rtxtAgrupacionDatoCve.Enabled = false;
                    rtxtAgrupacionDatoDes.Enabled = false;
                    ds = dsAgrupacionesDato();
                    FnCtlsFillIn.RadGrid(ref this.rGdv_AgrupacionesDato, ds);

                }
            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }

    }
    private void EjecutaSp_AgrDato()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_AgrupacionesDato";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToString(rCboTiposAgrupacion.SelectedValue));
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input,rTxtAgrupacionCve.Text);
            ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, rtxtAgrupacionDatoCve.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@agrDatoDes", DbType.String, 50, ParameterDirection.Input, rtxtAgrupacionDatoDes.Text.Trim());
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
      
            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                if (sEjecEstatus == "1")
                {

                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString()) {
                        this.rtxtAgrupacionDatoCve.Text = "";
                        this.rtxtAgrupacionDatoDes.Text = "";
                        this.rtxtAgrupacionDatoCve.Enabled = true;
                        this.rtxtAgrupacionDatoDes.Enabled = true;

                    }

                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar ).ToString())
                    {
                        this.rtxtAgrupacionDatoCve.Text = "";
                        this.rtxtAgrupacionDatoDes.Text = "";
                        this.rtxtAgrupacionDatoCve.Enabled = false;
                        this.rtxtAgrupacionDatoDes.Enabled = false;
                        hdfAccAgr.Value = "";
                        hdfBtnAccion.Value = ""; 

                        //rGdv_Agrupaciones.MasterTableView.ClearSelectedItems();
                        rGdv_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true ;
                        rGdv_Agrupaciones.AllowMultiRowSelection = true ;

                        rGdv_AgrupacionesDato.MasterTableView.ClearSelectedItems();
                        rGdv_AgrupacionesDato.ClientSettings.Selecting.AllowRowSelect = true;
                        rGdv_AgrupacionesDato.AllowMultiRowSelection = true;

                        rTxtAgrupacionCve.CssClass = "cssTxtEnabled";
                        rTxtAgrupacionDes.CssClass = "cssTxtEnabled";
                        rtxtAgrupacionDatoCve.CssClass = "cssTxtEnabled";
                        rtxtAgrupacionDatoDes.CssClass = "cssTxtEnabled";

                        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                        rBtnGuardar.Enabled = false;
                        rBtnCancelar.Enabled = false; 
                    }

           

                   ds = dsAgrupacionesDato();
                    FnCtlsFillIn.RadGrid(ref this.rGdv_AgrupacionesDato, ds);
                    
                }

                ShowAlert(sEjecEstatus, sEjecMSG);
            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }
    private void EjecutaSpAccionesEliminar() 
    {
        if (hdfAccAgr.Value == "1")
        {
            EjecutaSpElimina_Agr();
        }

        if (hdfAccAgr.Value == "2")
        {
            EjecutaSpElimina_AgrDatos();
            hdfAccAgr.Value = "1";
        }
    }
    private void EjecutaSpElimina_Agr() {

        try
        {

            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";
            
            foreach (GridDataItem i in rGdv_Agrupaciones.SelectedItems)
            {

                var dataItem = rGdv_Agrupaciones.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_Agrupaciones";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, rCboTiposAgrupacion.SelectedValue);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, dataItem["agrCve"].Text);
                        ProcBD.AgregarParametrosProcedimiento("@agrDes", DbType.String, 50, ParameterDirection.Input, dataItem["agrDes"].Text);
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
                else {
                    sMsgAlert = MsgItemsElim;
                }


               DataSet ds = dsAgrupaciones();
               FnCtlsFillIn.RadGrid(ref rGdv_Agrupaciones, ds);
                ShowAlert(sEstatusAlert, sMsgAlert);

                InicioPagina();


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

                DataSet ds = dsAgrupaciones();
                FnCtlsFillIn.RadGrid(ref rGdv_Agrupaciones, ds);
                ShowAlert(sEstatusAlert, sMsgAlert);

                InicioPagina();

            }

        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }
    }
    private void EjecutaSpElimina_AgrDatos()
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

            foreach (GridDataItem i in rGdv_AgrupacionesDato.SelectedItems)
            {

                var dataItem = rGdv_AgrupacionesDato.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    //rtxtAgrupacionDatoCve.Text = dataItem["agrDatoCve"].Text;
                    //rtxtAgrupacionDatoDes.Text = dataItem["agrDatoDes"].Text;
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_AgrupacionesDato";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, rCboTiposAgrupacion.SelectedValue);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input,rTxtAgrupacionCve.Text );
                        ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, dataItem["agrDatoCve"].Text);
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
                else {
                    sMsgAlert = MsgItemsElim;
                }

                DataSet ds =dsAgrupacionesDato ();
                FnCtlsFillIn.RadGrid(ref rGdv_AgrupacionesDato , ds);
                ShowAlert(sEstatusAlert, sMsgAlert);



            }
            else if (CountItems > 1)//CANTIDAD DE ITEMS MAYOR A 1 ELIMINADOS
            {


                if (CantItemsElimTrue > 0)
                {
                    sEstatusAlert = "1";
                }
               
                if (CantItemsElimTrue > 0)
                {
                    //sMsgAlert = "Registros eliminados" + " " + CantItemsElimTrue.ToString();
                    string sMSGTip = "";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "ABC0003", ref sMSGTip, ref sMsgAlert);
                    sMsgAlert += " " + CantItemsElimTrue.ToString();
                }

                if (CantItemsElimFalse > 0)//ITEMS CANTIDAD NO ELIMINADA
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


                DataSet ds = dsAgrupacionesDato();
                FnCtlsFillIn.RadGrid(ref rGdv_AgrupacionesDato, ds);
                ShowAlert(sEstatusAlert, sMsgAlert);



            }
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    private void LlenaComboTiposAgrupacion()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_selAgrupacionTipos";
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboTiposAgrupacion, ds, "agrTipId", "agrTipDes", false, false);
        ((Literal)rCboTiposAgrupacion.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboTiposAgrupacion.Items.Count);

    }
    private void Limpiar()
    {

 
        rTxtAgrupacionCve.Text = "";
        rTxtAgrupacionDes.Text = "";
        rtxtAgrupacionDatoCve.Text = "";
        rtxtAgrupacionDatoDes.Text = "";


        rTxtAgrupacionCve.CssClass = "cssTxtEnabled";
        rTxtAgrupacionDes.CssClass = "cssTxtEnabled";
        rtxtAgrupacionDatoCve.CssClass = "cssTxtEnabled";
        rtxtAgrupacionDatoDes.CssClass = "cssTxtEnabled";

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtAgrupacionCve.Enabled = false;
        rTxtAgrupacionDes.Enabled = false;
        rtxtAgrupacionDatoCve.Enabled = false;
        rtxtAgrupacionDatoDes.Enabled = false;

        rGdv_Agrupaciones.MasterTableView.ClearSelectedItems();
        rGdv_Agrupaciones.ClientSettings.Selecting.AllowRowSelect = true;
        rGdv_Agrupaciones.AllowMultiRowSelection = false;

        rGdv_AgrupacionesDato.MasterTableView.ClearSelectedItems();
        rGdv_AgrupacionesDato.DataSource = null;
        rGdv_AgrupacionesDato.Rebind();

        rGdv_AgrupacionesDato.ClientSettings.Selecting.AllowRowSelect = false;




        //===> Botones GUARDAR - CANCELAR
        if ((hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() ||
            hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString()) &&
            rCboTiposAgrupacion.SelectedIndex != -1
           )
        {
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }
        else
        {
            rBtnGuardar.Enabled = false;
            rBtnCancelar.Enabled = false;
        }


    }
    private void ControlesAccionGral() {

        rTxtAgrupacionCve.CssClass = "cssTxtEnabled";
        rTxtAgrupacionDes.CssClass = "cssTxtEnabled";
        rtxtAgrupacionDatoCve.CssClass = "cssTxtEnabled";
        rtxtAgrupacionDatoDes.CssClass = "cssTxtEnabled";

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtAgrupacionCve.Enabled = false;
        rTxtAgrupacionDes.Enabled = false;
        rtxtAgrupacionDatoCve.Enabled = false;
        rtxtAgrupacionDatoDes.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManager1.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png"); 
    }

#endregion

#   region FUNCIONES
    DataSet dsAgrupaciones()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Agrupaciones";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, rCboTiposAgrupacion.SelectedValue.ToString());
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 15, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    DataSet dsAgrupacionesDato( )
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_AgrupacionesDato";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, rCboTiposAgrupacion.SelectedValue);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 15, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, rTxtAgrupacionCve.Text);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;

        if (this.rCboTiposAgrupacion.SelectedIndex == -1) {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL1005", ref sMSGTip, ref sResult);
            return sResult;
        }

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (hdfAccAgr.Value == "1")
            {
                //---> Registro Agrupación
                if (rTxtAgrupacionCve.Text.Trim() == "")
                {
                    rTxtAgrupacionCve.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                } else {rTxtAgrupacionCve.CssClass = "cssTxtEnabled"; }

                if (rTxtAgrupacionDes.Text.Trim() == "")
                {
                    rTxtAgrupacionDes.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }else { rTxtAgrupacionDes.CssClass = "cssTxtEnabled"; }


                if (camposInc > 0)
                {
                    //sResult = "Tiene campos pendientes de completar.";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
                }
                return sResult;
            }

            if (hdfAccAgr.Value == "2")
            {

                //---> Registro Dato Agrupación
                if (rtxtAgrupacionDatoCve.Text.Trim() == "")
                {
                    rtxtAgrupacionDatoCve.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rtxtAgrupacionDatoCve.CssClass = "cssTxtEnabled"; }

                if (rtxtAgrupacionDatoDes.Text.Trim() == "")
                {
                    rtxtAgrupacionDatoDes.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rtxtAgrupacionDatoDes.CssClass = "cssTxtEnabled"; }

                if (camposInc > 0)
                {
                    //sResult = "Tiene campos pendientes de completar.";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
                }
                return sResult;
            }
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            if (hdfAccAgr.Value == "1")
            {
                if (rGdv_Agrupaciones.SelectedItems.Count == 0)
                {
                    //sResult = "No se han seleccionado registros por modificar.";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }

                if (rTxtAgrupacionCve.Text.Trim() == "")
                {
                    rTxtAgrupacionCve.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

                if (rTxtAgrupacionDes.Text.Trim() == "")
                {
                    rTxtAgrupacionDes.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

                if (camposInc > 0)
                {
                    //sResult = "Tiene campos pendientes de completar.";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
                }
                return sResult;
            }
            else {

                if (rGdv_AgrupacionesDato.SelectedItems.Count == 0)
                {
                    //sResult = "No se han seleccionado registros por modificar.";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }

                if (rtxtAgrupacionDatoCve.Text.Trim() == "")
                {
                    rtxtAgrupacionDatoCve.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

                if (rtxtAgrupacionDatoDes.Text.Trim() == "")
                {
                    rtxtAgrupacionDatoDes.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

                if (camposInc > 0)
                {
                    //sResult = "Tiene campos pendientes de completar.";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
                }
                return sResult;
            }
 
        }


        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
        }


        return sResult;
    }
    private string ValidaControlesAccion_SelectRowGrid(ref string sMSGTip)
    {
        string sResult = "";
        string sResultAux = "";
        int GvSelectItem = rGdv_Agrupaciones.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (this.rCboTiposAgrupacion.SelectedIndex == -1)
            {
                sMSGTip = "2";
                sResult = "Debe seleccionar el tipo de agrupación";
                return sResult;
            }
        }

        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };


            hdfAccAgr.Value = "1";
            if (rGdv_AgrupacionesDato.SelectedItems.Count > 0)
            {
                hdfAccAgr.Value = "2";
            }


            if (hdfAccAgr.Value == "1")
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Agrupaciones, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    return sResult;
                }
            }
            else if (hdfAccAgr.Value == "2")
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_AgrupacionesDato, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    return sResult;
                }
            }
            else {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Agrupaciones, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_AgrupacionesDato, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

            }
            //    }


            //if (hdfAccAgr.Value == "1")
            //{
            //    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Agrupaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            //    {
            //        return sResult;
            //    }

            //}
            //else if (hdfAccAgr.Value == "2")
            //{
            //    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Agrupaciones, GvVAS, ref sMSGTip, ref sResultAux) == false)
            //    {
            //        return sResultAux;
            //    }


            //    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_AgrupacionesDato, GvVAS, ref sMSGTip, ref sResult) == false)
            //    {
            //        if (sResultAux != "") {
            //            return sResult;
            //        }

            //    }



            //}
            //else {
            //    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Agrupaciones, GvVAS, ref sMSGTip, ref sResult) == false)
            //    {
            //        if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_AgrupacionesDato, GvVAS, ref sMSGTip, ref sResult) == false)
            //        {
            //            return sResult;
            //        }
            //    }
            //}





        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            hdfAccAgr.Value = "1";
            if (rGdv_AgrupacionesDato.SelectedItems.Count > 0)
            {
                hdfAccAgr.Value = "2";
            }


            if (hdfAccAgr.Value == "1")
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Agrupaciones, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    return sResult;
                }
            }
            else if (hdfAccAgr.Value == "2")
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_AgrupacionesDato, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    return sResult;
                }
            }
            else
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_Agrupaciones, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdv_AgrupacionesDato, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

            }
        }
        return "";
    }

    #endregion



}