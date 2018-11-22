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

public partial class DC_MttoCtaContableTipo : System.Web.UI.Page
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
            }
        }
    }

    //=====> EVENTOS CONTROLES
    protected void rGdvCtaTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GridDataItem dataItem = (GridDataItem)RadGrid1.SelectedItems[0];
        var dataItem = rGdvCtaTipo.SelectedItems[0] as GridDataItem;
        if (dataItem != null)
        {
            rTxtCtaTipo.Text = dataItem["ctaContTip"].Text;
            rTxtCtaTipoDes.Text = dataItem["ctaContTipDes"].Text;
            hdfAccAgr.Value = "1";

            DataSet ds = new DataSet();
            ds = dsCtasSubtipo();
            FnCtlsFillIn.RadGrid(ref this.rGdvCtaSubTipo, ds);
            rGdvCtaSubTipo.ClientSettings.Selecting.AllowRowSelect = true;
            rGdvCtaSubTipo.AllowMultiRowSelection = true;

            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                hdfAccAgr.Value = "2";
                rGdvCtaSubTipo.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvCtaSubTipo.AllowMultiRowSelection = false;
                rGdvCtaSubTipo.MasterTableView.ClearSelectedItems();

                rTxtCtaTipo.Enabled = false;
                rTxtCtaTipoDes.Enabled = false;
                rTxtCtaSubTipo.Enabled = true;
                rTxtCtaSubTipoDes.Enabled = true;
                rTxtCtaSubTipo.Text = "";
                rTxtCtaSubTipoDes.Text = "";
            }


            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rGdvCtaSubTipo.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvCtaSubTipo.AllowMultiRowSelection = false;
                rGdvCtaSubTipo.MasterTableView.ClearSelectedItems();

                rTxtCtaTipo .Enabled = false;
                rTxtCtaTipoDes.Enabled = true;

                rTxtCtaSubTipo .Text = "";
                rTxtCtaSubTipoDes.Text = "";
                rTxtCtaSubTipo.Enabled = false;
                rTxtCtaSubTipoDes.Enabled = false;
            }


        }
        else
        {
            hdfAccAgr.Value = "";
        }

    }
    protected void rGdvCtaSubTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rGdvCtaTipo.SelectedItems.Count > 1)
        {
            ShowAlert("2", "Para seleccionar los subtipos debe tener seleccionada solo un tipo de cuenta.");
            var dataItem = rGdvCtaSubTipo.SelectedItems[0] as GridDataItem;
            dataItem.Selected = false;
        }
        else
        {
            var dataItem = rGdvCtaSubTipo.SelectedItems[0] as GridDataItem;
            if (dataItem != null)
            {
                hdfAccAgr.Value = "2";

                //MODIFICAR
                if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                {
                    rTxtCtaTipo .Enabled = false;
                    rTxtCtaTipoDes.Enabled = false;

                    rTxtCtaSubTipo .Text = dataItem["ctaContSubTip"].Text;
                    rTxtCtaSubTipoDes.Text = dataItem["ctaContSubDes"].Text;
                    rTxtCtaSubTipo.Enabled = false;
                    rTxtCtaSubTipoDes.Enabled = true;
                }
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

        rTxtCtaTipo.Text = "";
        rTxtCtaTipoDes.Text = "";
        rTxtCtaSubTipo.Text = "";
        rTxtCtaSubTipoDes.Text = "";

        rTxtCtaTipo.Enabled = false;
        rTxtCtaTipoDes.Enabled = false;
        rGdvCtaTipo.MasterTableView.ClearSelectedItems();
        rGdvCtaTipo.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvCtaTipo.AllowMultiRowSelection = true;

        rTxtCtaSubTipo.Enabled = false;
        rTxtCtaSubTipoDes.Enabled = false;
        rGdvCtaSubTipo.MasterTableView.ClearSelectedItems();
        rGdvCtaSubTipo.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvCtaSubTipo.AllowMultiRowSelection = true;
        rGdvCtaSubTipo.DataSource = null;
        rGdvCtaSubTipo.Rebind();

        DataSet ds = new DataSet();
        ds = dsCtasTipo();
        FnCtlsFillIn.RadGrid(ref rGdvCtaTipo, ds);
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

        if (hdfBtnAccion.Value != "")
        {
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

                rGdvCtaTipo.MasterTableView.ClearSelectedItems();
                rGdvCtaTipo.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvCtaTipo.AllowMultiRowSelection = false;

                rGdvCtaSubTipo.MasterTableView.ClearSelectedItems();
                rGdvCtaSubTipo.ClientSettings.Selecting.AllowRowSelect = false;
                rGdvCtaSubTipo.AllowMultiRowSelection = false;


                rTxtCtaTipo .Text = "";
                rTxtCtaTipoDes.Text = "";
                rTxtCtaSubTipo .Text = "";
                rTxtCtaSubTipoDes.Text = "";
                rTxtCtaTipo.Enabled = true;
                rTxtCtaTipoDes.Enabled = true;
                rTxtCtaSubTipo.Enabled = false;
                rTxtCtaSubTipoDes.Enabled = false;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }


            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                hdfAccAgr.Value = "1";
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";

                rGdvCtaTipo .AllowMultiRowSelection = false;
                var dataItem = rGdvCtaTipo.SelectedItems[0] as GridDataItem;
                if (dataItem != null)
                {
                    rTxtCtaTipo.Text = dataItem["ctaContTip"].Text;
                    rTxtCtaTipoDes.Text = dataItem["ctaContTipDes"].Text;
                }


                rGdvCtaSubTipo.ClientSettings.Selecting.AllowRowSelect = true;
                rGdvCtaSubTipo.AllowMultiRowSelection = false;

                if (rGdvCtaSubTipo.SelectedItems.Count > 0)
                {

                    hdfAccAgr.Value = "2";
                    var dataItem2 = rGdvCtaSubTipo.SelectedItems[0] as GridDataItem;
                    if (dataItem2 != null)
                    {
                        rTxtCtaSubTipo .Text = dataItem2["ctaContSubTip"].Text;
                        rTxtCtaSubTipoDes.Text = dataItem2["ctaContSubDes"].Text;
                    }
                }



                if (hdfAccAgr.Value == "1")
                {

                    rTxtCtaTipo.Enabled = false;
                    rTxtCtaTipoDes.Enabled = true;

                    rTxtCtaSubTipo.Enabled = false;
                    rTxtCtaSubTipoDes.Enabled = false;

                }
                if (hdfAccAgr.Value == "2")
                {

                    rTxtCtaTipo.Enabled = false;
                    rTxtCtaTipoDes.Enabled = false;

                    rTxtCtaSubTipo.Enabled = false;
                    rTxtCtaSubTipoDes.Enabled = true;

                }





                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;


            }




            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                hdfAccAgr.Value = "1";
                if (rGdvCtaSubTipo.SelectedItems.Count > 0)
                {
                    hdfAccAgr.Value = "2";
                }

                var dataItem = rGdvCtaTipo.SelectedItems[0] as GridDataItem;
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

            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
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
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() ||
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
    private void EjecutaSp_Agr()
    {

        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_CuentaContableTipo";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 15, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ctaContTip", DbType.String, 1, ParameterDirection.Input, rTxtCtaTipo.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@ctaContTipDes", DbType.String, 200, ParameterDirection.Input, rTxtCtaTipoDes.Text.Trim());
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

             
            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();
                ShowAlert(sEjecEstatus, sEjecMSG);

                if (sEjecEstatus == "1")
                {
                    rTxtCtaTipo.Text = "";
                    rTxtCtaTipoDes.Text = "";
                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                    {
                        rTxtCtaTipo.Enabled = true;
                        rTxtCtaTipoDes.Enabled = true;
                    }

                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                    {
                        this.rTxtCtaTipo.Text = "";
                        this.rTxtCtaTipoDes.Text = "";
                        this.rTxtCtaTipo.Enabled = false;
                        this.rTxtCtaTipoDes.Enabled = false;
                        hdfAccAgr.Value = "";
                        hdfBtnAccion.Value = "";


                        rGdvCtaTipo.MasterTableView.ClearSelectedItems();
                        rGdvCtaTipo.ClientSettings.Selecting.AllowRowSelect = true;
                        rGdvCtaTipo.AllowMultiRowSelection = true;

                        rGdvCtaSubTipo.MasterTableView.ClearSelectedItems();
                        rGdvCtaSubTipo.ClientSettings.Selecting.AllowRowSelect = true;
                        rGdvCtaSubTipo.AllowMultiRowSelection = true;

                        rTxtCtaTipo.CssClass = "cssTxtEnabled";
                        rTxtCtaTipoDes.CssClass = "cssTxtEnabled";
                        rTxtCtaSubTipo.CssClass = "cssTxtEnabled";
                        rTxtCtaSubTipoDes.CssClass = "cssTxtEnabled";

                         

                        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                        rBtnGuardar.Enabled = false;
                        rBtnCancelar.Enabled = false;
                    }

                    ds = dsCtasTipo();
                    FnCtlsFillIn.RadGrid(ref rGdvCtaTipo, ds);

                    rTxtCtaSubTipo.Text = "";
                    rTxtCtaSubTipoDes.Text = "";
                    rTxtCtaSubTipo.Enabled = false;
                    rTxtCtaSubTipoDes.Enabled = false;
                    ds = dsCtasSubtipo ();
                    FnCtlsFillIn.RadGrid(ref this.rGdvCtaSubTipo, ds);

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
            ProcBD.NombreProcedimiento = "sp_CuentaContableSubTipo";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ctaContTip", DbType.String, 1, ParameterDirection.Input, rTxtCtaTipo.Text);
            ProcBD.AgregarParametrosProcedimiento("@ctaContSubTip", DbType.String, 20, ParameterDirection.Input, rTxtCtaSubTipo.Text.Trim());
            ProcBD.AgregarParametrosProcedimiento("@ctaContSubTipDes", DbType.String, 50, ParameterDirection.Input, rTxtCtaSubTipoDes.Text.Trim());
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

                if (sEjecEstatus == "1")
                {

                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
                    {
                        this.rTxtCtaSubTipo.Text = "";
                        this.rTxtCtaSubTipoDes.Text = "";
                        this.rTxtCtaSubTipo.Enabled = true;
                        this.rTxtCtaSubTipoDes.Enabled = true;

                    }

                    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
                    {
                        this.rTxtCtaSubTipo.Text = "";
                        this.rTxtCtaSubTipoDes.Text = "";
                        this.rTxtCtaSubTipo.Enabled = false;
                        this.rTxtCtaSubTipoDes.Enabled = false;
                        hdfAccAgr.Value = "";
                        hdfBtnAccion.Value = "";

                        //rGdv_Agrupaciones.MasterTableView.ClearSelectedItems();
                        rGdvCtaTipo.ClientSettings.Selecting.AllowRowSelect = true;
                        rGdvCtaTipo.AllowMultiRowSelection = true;

                        rGdvCtaSubTipo.MasterTableView.ClearSelectedItems();
                        rGdvCtaSubTipo.ClientSettings.Selecting.AllowRowSelect = true;
                        rGdvCtaSubTipo.AllowMultiRowSelection = true;

                        rTxtCtaTipo.CssClass = "cssTxtEnabled";
                        rTxtCtaTipoDes.CssClass = "cssTxtEnabled";
                        rTxtCtaSubTipo.CssClass = "cssTxtEnabled";
                        rTxtCtaSubTipoDes.CssClass = "cssTxtEnabled";

                        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
                        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
                        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

                        rBtnGuardar.Enabled = false;
                        rBtnCancelar.Enabled = false;
                    }



                    ds = dsCtasSubtipo();
                    FnCtlsFillIn.RadGrid(ref this.rGdvCtaSubTipo, ds);

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

    private void EjecutaSpElimina_Agr()
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

            foreach (GridDataItem i in rGdvCtaTipo.SelectedItems)
            {

                var dataItem = rGdvCtaTipo.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_CuentaContableTipo";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@ctaContTip", DbType.String, 1, ParameterDirection.Input, dataItem["ctaContTip"].Text);
                        ProcBD.AgregarParametrosProcedimiento("@ctaContTipDes", DbType.String, 50, ParameterDirection.Input, dataItem["ctaContTipDes"].Text);
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


                DataSet ds = dsCtasTipo();
                FnCtlsFillIn.RadGrid(ref rGdvCtaTipo, ds);
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

                DataSet ds = dsCtasTipo();
                FnCtlsFillIn.RadGrid(ref rGdvCtaTipo, ds);
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

            foreach (GridDataItem i in rGdvCtaSubTipo.SelectedItems)
            {

                var dataItem = rGdvCtaSubTipo.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {

                    //rtxtAgrupacionDatoCve.Text = dataItem["agrDatoCve"].Text;
                    //rtxtAgrupacionDatoDes.Text = dataItem["agrDatoDes"].Text;
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_CuentaContableSubTipo";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@ctaContTip", DbType.String, 1, ParameterDirection.Input, rTxtCtaTipo.Text);
                        ProcBD.AgregarParametrosProcedimiento("@ctaContSubTip", DbType.String, 20, ParameterDirection.Input, dataItem["ctaContSubTip"].Text);
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

                DataSet ds = dsCtasSubtipo();
                FnCtlsFillIn.RadGrid(ref rGdvCtaSubTipo, ds);
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


                DataSet ds = dsCtasSubtipo();
                FnCtlsFillIn.RadGrid(ref rGdvCtaSubTipo, ds);
                ShowAlert(sEstatusAlert, sMsgAlert);



            }
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }


    private void ControlesAccionGral()
    {
        rTxtCtaTipo.CssClass = "cssTxtEnabled";
        rTxtCtaTipoDes.CssClass = "cssTxtEnabled";
        rTxtCtaSubTipo .CssClass = "cssTxtEnabled";
        rTxtCtaSubTipoDes.CssClass = "cssTxtEnabled";

        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rTxtCtaTipo.Enabled = false;
        rTxtCtaTipoDes.Enabled = false;
        rTxtCtaSubTipo.Enabled = false;
        rTxtCtaSubTipoDes.Enabled = false;

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;
    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManagerPage.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }

    #endregion

    #region FUNCIONES

    DataSet dsCtasTipo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentaContableTipo";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 15, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    DataSet dsCtasSubtipo()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_CuentaContableSubTipo";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 15, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@ctaContTip", DbType.String, 1, ParameterDirection.Input, rTxtCtaTipo.Text);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }
    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
 
        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (hdfAccAgr.Value == "1")
            {
                //---> Registro Agrupación
                if (rTxtCtaTipo .Text.Trim() == "")
                {
                    rTxtCtaTipo.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtCtaTipo.CssClass = "cssTxtEnabled"; }

                if (rTxtCtaTipoDes.Text.Trim() == "")
                {
                    rTxtCtaTipoDes.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtCtaTipoDes.CssClass = "cssTxtEnabled"; }


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
                if (rTxtCtaSubTipo.Text.Trim() == "")
                {
                    rTxtCtaSubTipo.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtCtaSubTipo.CssClass = "cssTxtEnabled"; }

                if (rTxtCtaSubTipoDes.Text.Trim() == "")
                {
                    rTxtCtaSubTipoDes.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }
                else { rTxtCtaSubTipoDes.CssClass = "cssTxtEnabled"; }

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
                if (rGdvCtaTipo.SelectedItems.Count == 0)
                {
                    //sResult = "No se han seleccionado registros por modificar.";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }

                if (rTxtCtaTipo.Text.Trim() == "")
                {
                    rTxtCtaTipo.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

                if (rTxtCtaTipoDes.Text.Trim() == "")
                {
                    rTxtCtaTipoDes.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

                if (camposInc > 0)
                {
                    //sResult = "Tiene campos pendientes de completar.";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
                }
                return sResult;
            }
            else
            {

                if (rGdvCtaSubTipo.SelectedItems.Count == 0)
                {
                    //sResult = "No se han seleccionado registros por modificar.";
                    FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
                    return sResult;
                }

                if (rTxtCtaSubTipo.Text.Trim() == "")
                {
                    rTxtCtaSubTipo.CssClass = "cssTxtInvalid";
                    camposInc += 1;
                }

                if (rTxtCtaSubTipoDes.Text.Trim() == "")
                {
                    rTxtCtaSubTipoDes.CssClass = "cssTxtInvalid";
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
        int GvSelectItem = rGdvCtaTipo.SelectedItems.Count;
        string[] GvVAS;

        //NUEVO


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {
            GvVAS = new string[] { "VAL0003", "VAL0008" };


            hdfAccAgr.Value = "1";
            if (rGdvCtaSubTipo.SelectedItems.Count > 0)
            {
                hdfAccAgr.Value = "2";
            }


            if (hdfAccAgr.Value == "1")
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvCtaTipo, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    return sResult;
                }
            }
            else if (hdfAccAgr.Value == "2")
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvCtaSubTipo, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    return sResult;
                }
            }
            else
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvCtaTipo, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvCtaSubTipo, GvVAS, ref sMSGTip, ref sResult) == false)
                    {
                        return sResult;
                    }
                }

            }
            
        }

        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            GvVAS = new string[] { "VAL0003" };
            hdfAccAgr.Value = "1";
            if (rGdvCtaSubTipo.SelectedItems.Count > 0)
            {
                hdfAccAgr.Value = "2";
            }


            if (hdfAccAgr.Value == "1")
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvCtaTipo, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    return sResult;
                }
            }
            else if (hdfAccAgr.Value == "2")
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvCtaSubTipo, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    return sResult;
                }
            }
            else
            {
                if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvCtaTipo, GvVAS, ref sMSGTip, ref sResult) == false)
                {
                    if (FNValida.bAcciones_ValidacionesGVSelect(Pag_sConexionLog, rGdvCtaSubTipo, GvVAS, ref sMSGTip, ref sResult) == false)
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