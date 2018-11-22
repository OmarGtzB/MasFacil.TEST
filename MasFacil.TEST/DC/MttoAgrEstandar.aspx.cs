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
using System.Windows.Forms;

public partial class DC_MttoAgrEstandar : System.Web.UI.Page
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
        private string PagLoc_CveSeleccion;
        private string Pag_agrTipId;
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

    //=====> EVENTOS CONTROLES
    protected void rCboAgrupaciones_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LlenaCboDatosAgrupacion();
    }
    protected void rCboAgrupacionesDatos_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {

    }

    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }
    protected void rBtnEliminar_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        ControlesAccion();
    }
    protected void rBtnLimpiar_Click(object sender, ImageButtonClickEventArgs e)
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
        PagLoc_CveSeleccion = Convert.ToString(Session["folio_Selection"]);

        if (Request.QueryString["agrTipId"] != null && Request.QueryString["agrTipId"] != "")
        {
            Pag_agrTipId = Request.QueryString["agrTipId"];
        }
        else {
            Pag_agrTipId = "0";
        }       
    }
    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";

        ControlesAccion();
        LlenaDatosSeleccion();
        LlenaCboAgrupaciones();
        LlenaGrvInformacion();

        rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rGdvInformacion.AllowMultiRowSelection = true;

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
    private void ControlesAccion() {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";
        rCboAgrupaciones.ClearSelection();
        rCboAgrupacionesDatos.ClearSelection();
        rCboAgrupacionesDatos.Items.Clear();
        rCboAgrupaciones.Enabled = false ;
        rCboAgrupacionesDatos.Enabled = false;

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

 

    }
    private void ControlesAccionEjecucion(bool Result)
    {
        if (Result == true)
        {
            //NUEVO
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
            {
                rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
                this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
                this.rCboAgrupaciones.Enabled = true;
                this.rCboAgrupacionesDatos.Enabled = true;

                rCboAgrupaciones.ClearSelection();
                rCboAgrupacionesDatos.ClearSelection();
                rCboAgrupacionesDatos.Items.Clear();
                rGdvInformacion.MasterTableView.ClearSelectedItems();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

        }


        if (Result == false)
        {
            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                this.rCboAgrupaciones.Enabled = false;
                this.rCboAgrupacionesDatos.Enabled = false;
            }
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
                InicioPagina();
            }
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaSpAccionEliminar();
            }
        }
        else
        {
            ShowAlert(sMSGTip, msgValidacion);
        }
    }
    private void EjecutaAccionLimpiar()
    {
        rGdvInformacion.MasterTableView.ClearSelectedItems();

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            rCboAgrupaciones.ClearSelection();
            rCboAgrupacionesDatos.ClearSelection();
            rCboAgrupacionesDatos.Items.Clear();
        }

    }
    private void EjecutaSpAcciones()
    {
        try
        {
            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_Agrupaciones_agrupacionTipos";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToUInt64(Pag_agrTipId));
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@cveFolio", DbType.String, 30, ParameterDirection.Input, PagLoc_CveSeleccion);
            ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, this.rCboAgrupaciones.SelectedValue.ToString());
            ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, this.rCboAgrupacionesDatos.SelectedValue.ToString());
            ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

            if (FnValAdoNet.bDSIsFill(ds))
            {
                string sEjecEstatus, sEjecMSG = "";
                sEjecEstatus = ds.Tables[0].Rows[0]["maMSGTip"].ToString();
                sEjecMSG = ds.Tables[0].Rows[0]["maMSGDes"].ToString();

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

        try
        {
            int CountItems = 0;
            int CantItemsElimTrue = 0;
            int CantItemsElimFalse = 0;
            string EstatusItemsElim = "";
            string MsgItemsElim = "";
            string MsgItemsElimTrue = "";
            string MsgItemsElimFalse = "";

            foreach (GridDataItem i in this.rGdvInformacion.SelectedItems)
            {
                var dataItem = rGdvInformacion.SelectedItems[CountItems] as GridDataItem;
                if (dataItem != null)
                {
                    string sagrCve = dataItem["agrCve"].Text;
                    string sagrDatoCve = dataItem["agrDatoCve"].Text;
                    try
                    {
                        DataSet ds = new DataSet();
                        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                        ProcBD.NombreProcedimiento = "sp_Agrupaciones_agrupacionTipos";
                        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToUInt64(Pag_agrTipId));
                        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                        ProcBD.AgregarParametrosProcedimiento("@cveFolio", DbType.String, 30, ParameterDirection.Input, PagLoc_CveSeleccion);
                        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 3, ParameterDirection.Input, sagrCve);
                        ProcBD.AgregarParametrosProcedimiento("@agrDatoCve", DbType.String, 20, ParameterDirection.Input, sagrDatoCve);
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


    }
    private void LlenaGrvInformacion()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Agrupaciones_agrupacionTipos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToUInt64(Pag_agrTipId));
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cveFolio", DbType.String, 30, ParameterDirection.Input, PagLoc_CveSeleccion);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadGrid(ref this.rGdvInformacion, ds);
    }

    private void LlenaDatosSeleccion() {
        LlenaDatos();
    }
    private void LlenaDatos()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Agrupaciones_agrupacionTipos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 52);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToUInt64(Pag_agrTipId));
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@cveFolio", DbType.String, 30, ParameterDirection.Input, PagLoc_CveSeleccion);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rLblClave.Text = Convert.ToString(ds.Tables[0].Rows[0]["Cve"]);
            rLblDescripcion.Text = Convert.ToString(ds.Tables[0].Rows[0]["Descripcion"]);
        }
        else {
            rLblClave.Text = "";
            rLblDescripcion.Text = "";
        }
    }
    private void LlenaCboAgrupaciones() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_Agrupaciones_agrupacionTipos";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64 , 0, ParameterDirection.Input, Convert.ToUInt64(Pag_agrTipId));
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboAgrupaciones, ds, "agrCve", "agrDes", true, false);
        ((Literal)rCboAgrupaciones.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboAgrupaciones.Items.Count);

    }
    private void LlenaCboDatosAgrupacion() {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_AgrupacionesDato";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@agrTipId", DbType.Int64, 0, ParameterDirection.Input, Convert.ToUInt64(Pag_agrTipId));
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@agrCve", DbType.String, 10, ParameterDirection.Input, this.rCboAgrupaciones.SelectedValue.ToString());
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboAgrupacionesDatos, ds, "agrDatoCve", "agrDatoDes", true, false);
        ((Literal)rCboAgrupacionesDatos.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboAgrupacionesDatos.Items.Count);

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

        //NUEVO
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {
            if (this.rCboAgrupaciones.SelectedIndex == -1)
            {
                camposInc += 1;
            }

            if (this.rCboAgrupacionesDatos.SelectedIndex == -1)
            {
                camposInc += 1;
            }


            if (camposInc > 0)
            {
                //sResult = "Tiene campos pendientes de completar.";
                FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
            }
            return sResult;
        }


        //ELIMINAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            if (rGdvInformacion.SelectedItems.Count == 0)
            {
                //sResult = "No se han seleccionado registros.";
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
        int GvSelectItem = rGdvInformacion.SelectedItems.Count;
        string[] GvVAS;

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

                                                                                                                                                                                                                                                                                                                                                                                                                                
    #endregion



}