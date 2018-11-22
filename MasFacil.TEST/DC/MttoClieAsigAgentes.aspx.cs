
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

public partial class DC_MttoClieAsigAgentes : System.Web.UI.Page
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
    MGMControls.PermisosBTN FNBtn = new MGMControls.PermisosBTN();


    private string Pag_sCompania;
    private string Pag_sConexionLog;
    private string Pag_sSessionLog;

    private string PagLoc_cliCve;
    private int Pag_agrTipId;

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


    //=====> EVENTOS BOTONES SELECCION DE LA ACCION
    protected void rBtnNuevo_Click(object sender, ImageButtonClickEventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString();
        ControlesAccion();
    }
    protected void rBtnModificar_Click(object sender, ImageButtonClickEventArgs e)//editar
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
        ClienteAsignacion_txt();
    }

    //=====> EVENTOS BOTONES EJECUCION DE LA ACCION
    protected void rBtnCancelar_Click(object sender, ImageButtonClickEventArgs e)
    {
        InicioPagina();
    }

    protected void rBtnGuardar_Click(object sender, ImageButtonClickEventArgs e)
    {
        EjecutaAccion();

    }

    protected void rbtnGuardarAgrupacion_Click(object sender, EventArgs e)
    {
        EjecutaSpAcciones();

    }
    protected void rbtnBorrarAgrupacion_Click(object sender, EventArgs e)
    {
        hdfBtnAccion.Value = Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString();
        this.rBtnGuardar.Enabled = true;
        this.rBtnCancelar.Enabled = true;
    }



    protected void rGdv_ClieAsgAgent_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {

        }
        else if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())

        {
            rad_to_combo();
        }
        else
        {
            rad_to_combo();
        }

    }
    public void rad_to_combo()
    {

        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())

        {

            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }


        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
        {
            rBtnGuardar.Enabled = true;
            rBtnCancelar.Enabled = true;
        }


    }

    //}

    #endregion
    #region METODOS
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
                InicioPagina();
            }

        }
        else
        {
            RadWindowManager1.RadAlert(msgValidacion, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert2.png");
        }
    }

    public void ClienteAsignacion_txt()

    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteAsignacionAgentes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_cliCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSRowsIsFill(ds))
        {

            string s = ds.Tables[0].Rows[0]["ageCveVen"].ToString();
            if (ds.Tables[0].Rows[0]["ageCveVen"].ToString() != "")
            {


                rCboAgeVentas.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ageCveVen"]);
                rCboCobranza.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ageCveCob"]);
                this.rBtnNuevo.Enabled = false;
            }
            else {

                rCboAgeVentas.ClearSelection();
                rCboCobranza.ClearSelection();
                this.rBtnNuevo.Enabled = true;
            }


        }
        else {
            rCboAgeVentas.ClearSelection();
            rCboCobranza.ClearSelection();
            this.rBtnNuevo.Enabled = true;
        }




    }

    private void EjecutaSpAcciones()
    {
        try
        {

            DataSet ds = new DataSet();
            MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
            ProcBD.NombreProcedimiento = "sp_ClienteAsignacionAgentes";
            ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
            ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_cliCve);
            ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
            ProcBD.AgregarParametrosProcedimiento("@ageCveVen", DbType.String, 10, ParameterDirection.Input, rCboAgeVentas.SelectedValue);
            ProcBD.AgregarParametrosProcedimiento("@ageCveCob", DbType.String, 10, ParameterDirection.Input, rCboCobranza.SelectedValue);
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



    //private void ControlesAccion()
    //{

    //    rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
    //    rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
    //    rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
    //    {
    //        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevoSelected.png";
    //        rCboAgeVentas.Enabled = true;
    //        rCboCobranza.Enabled = true;
    //        rCboAgeVentas.ClearSelection();
    //        rCboCobranza.ClearSelection();


    //    }
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
    //    {
    //        //InicioPagina();
    //        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminarSelected.png";
    //        rCboAgeVentas.Enabled = false;
    //        rCboCobranza.Enabled = false;
    //        if (RegistroDatosCartera() == false)
    //        {
    //            rCboAgeVentas.ClearSelection();
    //            rCboCobranza.ClearSelection();
    //        }
    //        else {
    //            DatosCartera_txt();
    //        }
    //    }
    //    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
    //    {
    //        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";

    //        if (RegistroDatosCartera() == false)
    //        {

    //            this.rCboAgeVentas.Enabled = false;
    //            this.rCboCobranza.Enabled = false;
    //            rCboAgeVentas.ClearSelection();
    //            rCboCobranza.ClearSelection();
    //        }
    //        else {

    //            this.rCboAgeVentas.Enabled = true;
    //            this.rCboCobranza.Enabled = true;
    //            DatosCartera_txt();
    //        }

    //    }

    //    //INICIO / CANCELAR
    //    if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
    //        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
    //           hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
    //    {

    //        this.rCboAgeVentas.Enabled = false;
    //        this.rCboCobranza.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //        rBtnGuardar.Enabled = false;
    //        rCboAgeVentas.CssClass = "cssTxtEnabled";
    //        rCboCobranza.CssClass = "cssTxtEnabled";
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
    //    else {
    //        rBtnGuardar.Enabled = false;
    //        rBtnCancelar.Enabled = false;
    //    }

    //}

    public void Limpiartxt()
    {
        rTxtagrCve.Text = "";
        rTxtagrDatoCve.Text = "";
        rTxtciaCve.Text = "";
    }


    private void LlenaComboVentas()//BIENE DE LA TABLA AGRUPACIONES
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteAsignacionAgentes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 0);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboAgeVentas, ds, "ageCve", "ageNom", true, false);
        ((Literal)rCboAgeVentas.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboAgeVentas.Items.Count);
    }
    private void LlenaCombo2()//BIENE DE LA TABLA AGRUPACIONES
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteAsignacionAgentes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        FnCtlsFillIn.RadComboBox(ref this.rCboCobranza, ds, "ageCve", "ageNom", true, false);
        ((Literal)rCboCobranza.Footer.FindControl("RadComboItemsCount")).Text = "Total de registros " + Convert.ToString(rCboCobranza.Items.Count);
    }
    //ok
    private void Valores_InicioPag()
    {
        Pag_sCompania = Convert.ToString(Session["Compania"]);
        Pag_sConexionLog = Convert.ToString(Session["Conexion"]);
        Pag_sSessionLog = Convert.ToString(Session["SessionLog"]);
        PagLoc_cliCve = Convert.ToString(Session["folio_Selection"]);
    }
    private void InicioPagina()
    {
        hdfBtnAccion.Value = "";
        ControlesAccion();

        LlenaDatosCliente();
        LlenaComboVentas();
        LlenaCombo2();
        ClienteAsignacion_txt();

        

        if (FNGrales.bManejoSubCliente(Pag_sConexionLog, Pag_sCompania))
        {
            rLblSubClie.Visible = true;
            rLblSubClient.Visible = true;
        }
        else
        {
            rLblSubClie.Visible = false;
            rLblSubClient.Visible = false;
        }

        if (rBtnNuevo.Enabled == true)
        {
            rBtnModificar.Enabled = false;
        }
        else
        {
            rBtnModificar.Enabled = true;
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
    private void LlenaDatosCliente()
    {
        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteAsignacionAgentes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 50);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_cliCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);

        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            rLblClave.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliCveClie"]);
            rLblSubClient.Text = Convert.ToString(ds.Tables[0].Rows[0]["cliCveSubClie"]);
            rLblDescripcion.Text = Convert.ToString(ds.Tables[0].Rows[0]["clieNom"]);
        }
        else {
            rLblClave.Text = "";
            rLblSubClient.Text = "";
            rLblDescripcion.Text = "";
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







            //rCboAgeVentas.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ageCveVen"]);

            //rCboCobranza.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ageCveCob"]);


            try
            {
                DataSet ds = new DataSet();
                MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
                ProcBD.NombreProcedimiento = "sp_ClienteAsignacionAgentes";
                ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, hdfBtnAccion.Value);
                ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_cliCve);
                ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
                ProcBD.AgregarParametrosProcedimiento("@ageCveVen", DbType.String, 10, ParameterDirection.Input, rCboAgeVentas.SelectedValue);
                ProcBD.AgregarParametrosProcedimiento("@ageCveCob", DbType.String, 10, ParameterDirection.Input, rCboCobranza.SelectedValue);
                //ProcBD.AgregarParametrosProcedimiento("@ageTip", DbType.String, 10, ParameterDirection.Input, rTxtagrCve.Text.Trim());
                //ProcBD.AgregarParametrosProcedimiento("@AGNOM", DbType.Int64, 0, ParameterDirection.Input, rTxtagrDatoCve.Text.Trim());
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


            //}

            CountItems += 1;
            {





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


                    ShowAlert(sEstatusAlert, sMsgAlert);

                    if (sEstatusAlert == "1")
                    {
                        InicioPagina();
                    }
                    else {
                        InicioPagina();
                        //    LlenaGridClieAsgAgent();
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
                    else {
                        //    LlenaGridClieAsgAgent();
                        InicioPagina();
                    }

                }

            }
        }
        catch (Exception ex)
        {
            string MsgError = ex.Message.Trim();
        }


    }
    private void ShowAlert(string Estatus, string MSG)
    {
        RadWindowManager1.RadAlert(MSG, 300, 150, "Aviso", "", "../Imagenes/IcoMensajes/IcoAlert" + Estatus + ".png");
    }
    #endregion
    #region FUNCIONES
    DataSet dsAgrupacionesDatos()//LLENA AGRUPACIONES DATOS    
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteAsignacionAgentes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 51);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ProcBD.AgregarParametrosProcedimiento("@ageCve", DbType.String, 10, ParameterDirection.Input, rCboAgeVentas.SelectedValue);
        ds = oWS.ObtenerDatasetDesdeProcedimiento(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);
        return ds;
    }

    private string validaEjecutaAccion(ref string sMSGTip)
    {

        string sResult = "";
        int camposInc = 0;
        sMSGTip = "";

    //NUEVO
    if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString())
        {

            if (rCboAgeVentas.SelectedValue == "")
            {
                rCboAgeVentas.BorderWidth = Unit.Pixel(1);
                rCboAgeVentas.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboAgeVentas.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboCobranza.SelectedValue == "")
            {
                rCboCobranza.BorderWidth = Unit.Pixel(1);
                rCboCobranza.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboCobranza.BorderColor = System.Drawing.Color.Transparent; }


            if (camposInc > 0)
            {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
        }
            return sResult;
        }


        //MODIFICAR
        if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
        {

            if (RegistroDatosCartera() == false)
            {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
            }
            if (rCboAgeVentas.SelectedValue == "")
            {
                rCboAgeVentas.BorderWidth = Unit.Pixel(1);
                rCboAgeVentas.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboAgeVentas.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboCobranza.SelectedValue == "")
            {
                rCboCobranza.BorderWidth = Unit.Pixel(1);
                rCboCobranza.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboCobranza.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboAgeVentas.SelectedValue == "")
            {
                rCboAgeVentas.BorderWidth = Unit.Pixel(1);
                rCboAgeVentas.BorderColor = System.Drawing.Color.Red;
                camposInc += 1;
            }
            else { rCboAgeVentas.BorderColor = System.Drawing.Color.Transparent; }

            if (rCboCobranza.SelectedValue == "")
            {
                rCboCobranza.BorderWidth = Unit.Pixel(1);
                rCboCobranza.BorderColor = System.Drawing.Color.Red;

                camposInc += 1;
            }
            else { rCboCobranza.BorderColor = System.Drawing.Color.Transparent; }

            if (camposInc > 0)
            {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0001", ref sMSGTip, ref sResult);
        }
            return sResult;
        }

            //ELIMINAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                if (RegistroDatosCartera() == false)
                {
            FNDatos.dsMAMensajes(Pag_sConexionLog, "VAL0003", ref sMSGTip, ref sResult);
        }
            }

                return sResult;
    }


    private bool RegistroDatosCartera()
    {

        DataSet ds = new DataSet();
        MGM.ProcedimientoAlmacenado.Procedimiento ProcBD = new MGM.ProcedimientoAlmacenado.Procedimiento();
        ProcBD.NombreProcedimiento = "sp_ClienteAsignacionAgentes";
        ProcBD.AgregarParametrosProcedimiento("@opc", DbType.Int64, 0, ParameterDirection.Input, 55);
        ProcBD.AgregarParametrosProcedimiento("@cliCve", DbType.String, 20, ParameterDirection.Input, PagLoc_cliCve);
        ProcBD.AgregarParametrosProcedimiento("@ciaCve", DbType.String, 10, ParameterDirection.Input, Pag_sCompania);
        ds = oWS.ObtenerDatasetDesdeProcedimientoConError(ProcBD.ObtenerXmlProcedimiento(), Pag_sConexionLog);


        if (FnValAdoNet.bDSRowsIsFill(ds))
        {
            string s = ds.Tables[0].Rows[0]["ageCveVen"].ToString();
            if (ds.Tables[0].Rows[0]["ageCveCob"].ToString() != "")
            {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }

    }








    private void ControlesAccion()
    {
        string sMSGTip = "";
        string msgValidacion = "";

        //===> CONTROLES GENERAL
        //this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
        rBtnNuevo.Image.Url = "~/Imagenes/IcoBotones/IcoBtnNuevo.png";
        rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificar.png";
        rBtnEliminar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnEliminar.png";

        rCboAgeVentas.BorderColor = System.Drawing.Color.Transparent;
        rCboCobranza.BorderColor = System.Drawing.Color.Transparent;

        rCboAgeVentas.Enabled = false;
        rCboCobranza.Enabled = false;
        

        rBtnGuardar.Enabled = false;
        rBtnCancelar.Enabled = false;


        /*>>>>>>>>>>>>>>>>> CONTROLES POR ACCION <<<<<<<<<<<<<<<<<<  */
        //Validacion
        //msgValidacion = ValidaControlesAccion_SelectRowGrid(ref sMSGTip);
        //if (msgValidacion == "")
        //{
        ControlesAccionEjecucion(true);
        //}
        //else
        //{
        //    ControlesAccionEjecucion(false);
        //    ShowAlert(sMSGTip, msgValidacion);
        //}

        //INICIO / CANCELAR
        if (hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Nuevo).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString() &&
        hdfBtnAccion.Value != Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString()
          )
        {
            rCboAgeVentas.Enabled = false;
            rCboCobranza.Enabled = false;


            rCboAgeVentas.ClearSelection();
            rCboCobranza.ClearSelection();

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
                //this.rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = false;
                //rGdvInformacion.MasterTableView.ClearSelectedItems();
                
                rCboAgeVentas.Enabled = true;
                rCboCobranza.Enabled = true;

                rCboAgeVentas.ClearSelection();
                rCboCobranza.ClearSelection();

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //MODIFICAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Modificar).ToString())
            {
                rBtnModificar.Image.Url = "~/Imagenes/IcoBotones/IcoBtnModificarSelected.png";
                //rGdvInformacion.AllowMultiRowSelection = false;


                rCboAgeVentas.Enabled = true;
                rCboCobranza.Enabled = true;

                rBtnGuardar.Enabled = true;
                rBtnCancelar.Enabled = true;
            }

            //ELIMIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Eliminar).ToString())
            {
                EjecutaAccion();
            }

            //LIMPIAR
            if (hdfBtnAccion.Value == Convert.ToUInt16(MGMValores.Constantes.btnAccion.Limpiar).ToString())
            {
                //rGdvInformacion.ClientSettings.Selecting.AllowRowSelect = true;
                //rGdvInformacion.AllowMultiRowSelection = true;
                //rGdvInformacion.MasterTableView.ClearSelectedItems();

                rCboAgeVentas.Enabled = false;
                rCboCobranza.Enabled = false;


                rCboAgeVentas.ClearSelection();
                rCboCobranza.ClearSelection();

            }
        }


        if (Result == false)
        {
            rCboAgeVentas.Enabled = false;
            rCboCobranza.Enabled = false;


            rCboAgeVentas.ClearSelection();
            rCboCobranza.ClearSelection();
        }


    }
}


#endregion

